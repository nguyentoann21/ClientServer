using Shared.Dtos;
using Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Microsoft.IdentityModel.Tokens;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        private IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignIn()
        {

            ViewBag.Mess = TempData["MessSuccess"];
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                try
                {
                    AccCusDTO useAccount = null;
                    using (var client = new HttpClient())
                    {
                        var accountUse = HttpContext.Session.GetString("user");
                        var url = "https://localhost:7186/api/accounts/GetAccountByEmail/" + accountUse;
                        var result = await client.GetAsync(url);

                        if (result.IsSuccessStatusCode)
                        {
                            useAccount = await result.Content.ReadAsAsync<AccCusDTO>();
                            ViewData["useAccount"] = useAccount;
                            return View();
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                }
                catch (Exception ex)
                {
                    return NotFound();
                }

            }
            else
            {
                return Redirect("/Account/SignIn");
            }
        }

        public async Task<IActionResult> CheckSignIn(SignIn account)
        {

            //    if (account != null)
            //{
            if (account.Email == null || account.Password == null)
            {
                return View("SignIn");
            }
            string data = JsonConvert.SerializeObject(account);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            AccCusDTO useAccount = null;
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.PostAsync("https://localhost:7186/api/accounts/login", content).Result;
                var url = "https://localhost:7186/api/accounts/GetAccountByEmail/" + account.Email;
                var result = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jwtToken = response.Content.ReadAsStringAsync().Result;
                    HttpContext.Session.SetString("token", jwtToken);
                    useAccount = await result.Content.ReadAsAsync<AccCusDTO>();
                    HttpContext.Session.SetString("user", account.Email);
                    HttpContext.Session.SetString("role", useAccount.RoleID.ToString());
                    //HttpContext.Session.SetString("userId", useAccount.AccountID.ToString());
                    //HttpContext.Session.SetString("userAccount", JsonSerializer.Serialize(useAccount));

                    if (useAccount.RoleID == "AD")
                    {
                        return Redirect("/Products/Index");
                    }
                    return Redirect("/Products/Index");

                }
                else
                {
                    TempData["MessSuccess"] = "1";
                    return Redirect("/Account/SignIn");
                }
            }
            //}

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("user");
            HttpContext.Session.Remove("role");

            return RedirectToAction("Index", "Products");
        }
        public IActionResult SignUp()
        {
            return View();
        }
        public async Task<IActionResult> SignupAccount(AccCusDTO customer)
        {

            List<Account> accounts = await GetAllAccounts();
            foreach (var account in accounts)
            {
                if (customer.Email.Equals(account.Email))
                {
                    ViewBag.Mess = "1";
                    return View("SignUp");
                }
            }

            await AddAccount(new AccCusDTO
            {
                Email = customer.Email,
                Password = customer.Password,
                Address = customer.Address,
                Fullname = customer.Fullname,
                PhoneNumber = customer.PhoneNumber,
                RoleID = "CUS"


            });

            return RedirectToAction("SignIn", "Account");
        }

        [HttpPost]
        public async Task AddAccount(AccCusDTO customer)
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync<AccCusDTO>("https://localhost:7186/api/accounts/Register", customer))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
        }

        [HttpGet]
        public async Task<List<Account>> GetAllAccounts()
        {
            List<Account> accounts = new List<Account>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7186/api/accounts/GetAllAccounts"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    accounts = JsonConvert.DeserializeObject<List<Account>>(apiResponse);
                }
            }
            return accounts.ToList();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        public async Task<IActionResult> ChangePasswordAccount()
        {
            var email = HttpContext.Session.GetString("user");
            string password = Request.Form["password"];
            string newpassword = Request.Form["rePassword"];
            string confirm = Request.Form["ConfirmPassword"];
            if (password == null || newpassword == null)
            {
                ViewBag.Mess = "1";
            }
            if(newpassword != confirm)
            {
                ViewBag.Mess = "2";
            }
            if (newpassword ==password)
            {
                ViewBag.Mess = "3";
            }
            await ChangePasswordAction(new ChangePassword
            {
                Email= email,
                CurrentPassword = password,
                NewPassword = newpassword,
                ConfirmPassword= confirm
            });
            
            
            return RedirectToAction("Index", "Products");
        }
        [HttpPut]
        public async Task ChangePasswordAction(ChangePassword changePassword)
        {
            string apiRespose = "";
            using(var httpClient =new HttpClient())
            {
                using(var response=await httpClient.PutAsJsonAsync<ChangePassword>("https://localhost:7186/api/accounts/changePassword", changePassword))
                {
                    apiRespose = await response.Content.ReadAsStringAsync();
                }
            }

        }
        
        
    }
}

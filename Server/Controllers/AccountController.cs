using Azure;
using Shared.DataAccess;
using Shared.Dtos;
using Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers
{
    [Route("api/accounts/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        //key declaration
        private readonly IConfiguration _configuration;
        public AccountController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }


        // Add authorize later 
        //[Authorize]
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<IEnumerable<Account>>> GetPersonalAccount(int id)
        {
            var A = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountID == id);
            // New DTO To Show data 
            AccCusDTO accCusDTO = new AccCusDTO
            {
                AccountID = A.AccountID,
                Email = A.Email,
                Password = A.Password,
                Fullname = A.FullName,
                PhoneNumber = A.PhoneNumber,
                Address = A.Address,
                RoleID = A.RoleID,
                
            };
            if (accCusDTO == null)
            {
                return NoContent();
            }
            return Ok(accCusDTO);
        }

        [HttpGet("[action]/{email}")]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccountByEmail(string email)
        {
            var A = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email);
            // New DTO To Show data 
            AccCusDTO accCusDTO = new AccCusDTO
            {
                AccountID = A.AccountID,
                Email = A.Email,
                Password = A.Password,
                Fullname = A.FullName,
                PhoneNumber = A.PhoneNumber,
                Address = A.Address,
                RoleID = A.RoleID
            };
            if (accCusDTO == null)
            {
                return NoContent();
            }
            return Ok(accCusDTO);
        }
        [HttpGet("[action]/{email}")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            var A = await _context.Accounts.Where(a => a.Email == email)
                .FirstOrDefaultAsync();
            if (A == null)
            {
                return BadRequest();
            }
            else { return Ok(A); }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Post(SignIn account)
        {
            if (account.Email != null && account.Password != null)
            {
                var acc = await _context.Accounts.FirstOrDefaultAsync(x => x.Email == account.Email && x.Password == account.Password);
                if (acc != null)
                {
                    // Create Claim details
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Email, account.Email),
                        new Claim(ClaimTypes.Role, acc.RoleID.ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(50),
                            signingCredentials: signIn
                        );
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] SignUp account)
        {
            try
            {
                if (account != null && account.Email != null && account.Password != null)
                {

                    _context.Add<Account>(new Account
                    {
                        Email = account.Email,
                        Password = account.Password,
                        FullName = account.FullName,
                        PhoneNumber = account.PhoneNumber,
                        Address = account.Address,
                        RoleID = "CUS"
                    });

                    _context.SaveChanges();
                    return Ok(account);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Fail at running");
            }
        }
        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword edited)
        {
                var A = await _context.Accounts.Where(a=> a.Email == edited.Email && a.Password== edited.CurrentPassword).FirstOrDefaultAsync();
                if(A == null)
                {
                    return BadRequest("Wrong Email or Password!");
                }
                if(edited.CurrentPassword== edited.NewPassword)
                {
                    return BadRequest("New Password cannot like Old Password");
                }
                if(edited.NewPassword != edited.ConfirmPassword)
                {
                    return BadRequest("New Password does not match with Confirm Password");
                }
                A.Password = edited.NewPassword;
                _context.Accounts.Update(A);
                _context.SaveChanges();
                
                return Ok(A);
        }
        
        //public async Task<IActionResult> ChangeProfile(AccCusDTO edited)
        //{
        //    try
        //    {
        //        var A = await _context.Accounts.Where(a => a.Email == edited.Email && a.Password== edited.Password)
        //        .FirstOrDefaultAsync();
        //        A.FullName = edited.Fullname;
        //        A.PhoneNumber = edited.PhoneNumber;
        //        A.Address = edited.Address;
                
        //        await _context.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Fail");
        //    }
        //}
    }
}

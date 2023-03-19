using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Shared.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=123456;Database=ToyStores;Trusted_Connection=true;Encrypt=false");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Manufacturer>().HasKey(m => m.ManufacturerID);
            modelBuilder.Entity<Manufacturer>().HasData(
                new Manufacturer { ManufacturerID = "OP", ManufacturerName = "Once Piece" },
                new Manufacturer { ManufacturerID = "DRB", ManufacturerName = "Dragon Ball" },
                new Manufacturer { ManufacturerID = "PO", ManufacturerName = "Pokémon" },
                new Manufacturer { ManufacturerID = "AD", ManufacturerName = "Marvel" },
                new Manufacturer { ManufacturerID = "BR", ManufacturerName = "Bear Brick" }
                );

            modelBuilder.Entity<Product>().HasKey(p => p.ProductID);
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductID = 1,
                    ProductName = "Monkey D. Luffy",
                    ProductDescription = "Estátua Monkey D. Luffy: One Piece Grandista",
                    ProductPrice = 120,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://static3.tcdn.com.br/img/img_prod/460977/estatua_monkey_d_luffy_one_piece_grandista_56949_1_72aa76455699b0d59ee4f4f76c7bf950.jpg",
                    ManufacturerID = "OP"
                },
                new Product
                {
                    ProductID = 2,
                    ProductName = "Monkey D. Luffy [1/6 scale]",
                    ProductDescription = "Legendary Studio - Monkey D. Luffy [1/6 scale]",
                    ProductPrice = 150,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://cdn.shopify.com/s/files/1/0378/9674/9100/products/IMG_2116_300x300.jpg?v=1608576485",
                    ManufacturerID = "OP"
                },
                new Product
                {
                    ProductID = 3,
                    ProductName = "Monkey D. Luffy 15cm",
                    ProductDescription = "NEW hot 15cm One piece Gear fourth Monkey D Luffy action figure toys Christmas toy with box",
                    ProductPrice = 100,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://i.pinimg.com/564x/ec/f9/b6/ecf9b6f56740cfff572fa5e2bca3192c.jpg",
                    ManufacturerID = "OP"
                },
                new Product
                {
                    ProductID = 4,
                    ProductName = "Monkey D. Luffy by Bandai",
                    ProductDescription = "MONKEY D. LUFFY COLLECTIBLE FIGURE BY BANDAI",
                    ProductPrice = 160,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://cdn.shopify.com/s/files/1/0238/7617/products/AAGZ709AAAD54-1_1200x.jpg?v=1645009425",
                    ManufacturerID = "OP"
                },
                new Product
                {
                    ProductID = 5,
                    ProductName = "Monkey D. Luffy Child",
                    ProductDescription = "4.7in hot toys cute Japanese anime Childhood Bandaged Crying monkey d luffy action figure",
                    ProductPrice = 130,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://sc04.alicdn.com/kf/HTB1_2h1Xp67gK0jSZPfq6yhhFXaj.jpg",
                    ManufacturerID = "OP"
                },
                new Product
                {
                    ProductID = 6,
                    ProductName = "Monkey D. Luffy Gear 4 Bounce Man",
                    ProductDescription = "(Super Hot New Arrival) Super Beautiful Luffy Gear 4 Model ( One Piece )",
                    ProductPrice = 180,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://lzd-img-global.slatic.net/g/p/5f3e7fa512941b48f875ddf4f7b79788.jpg_720x720q80.jpg",
                    ManufacturerID = "OP"
                },
                new Product
                {
                    ProductID = 7,
                    ProductName = "Monkey D. Luffy Gear 3",
                    ProductDescription = "Xs Studio X Yang Studio WCF MAX One Piece Gear Third Luffy Resin Statue",
                    ProductPrice = 200,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://devilnesstoys.com/wp-content/uploads/2022/09/%E5%BE%AE%E4%BF%A1%E5%9B%BE%E7%89%87_20220905150902.jpg",
                    ManufacturerID = "OP"
                },
                new Product
                {
                    ProductID = 8,
                    ProductName = "Monkey D. Luffy Gear 4 Snake Man",
                    ProductDescription = "Action Figure Anime One Piece Hot Special Luffy Gear 4 Snake Man Wano",
                    ProductPrice = 250,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://images.tokopedia.net/img/cache/500-square/product-1/2020/2/3/41972150/41972150_9d0c3ea2-d6c8-452d-abd5-0a6ee6e1b9c6_700_700",
                    ManufacturerID = "OP"
                },
                new Product
                {
                    ProductID = 9,
                    ProductName = "Monkey D. Luffy Glow",
                    ProductDescription = "2022 Hot Sale Anime One Piece Gk Luffy Action Figure Monkey D Luffy Glow in Dark PVC Model Toy for Gifts",
                    ProductPrice = 210,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://image.made-in-china.com/2f0j00mnCWHVjqkrop/2022-Hot-Sale-Anime-One-Piece-Gk-Luffy-Action-Figure-Monkey-D-Luffy-Glow-in-Dark-PVC-Model-Toy-for-Gifts.jpg",
                    ManufacturerID = "OP"
                },
                new Product
                {
                    ProductID = 10,
                    ProductName = "Monkey D. Luffy Gear 1",
                    ProductDescription = "Hot Hot Super product Luffy gear 1 (one piece) model is super nice",
                    ProductPrice = 190,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://lzd-img-global.slatic.net/g/p/e346124d0f9cbd8d0b009d546b373009.jpg_1200x1200q80.jpg_.webp",
                    ManufacturerID = "OP"
                },
                new Product
                {
                    ProductID = 11,
                    ProductName = "Pokemon Xiaozhi Pikachu",
                    ProductDescription = "Pokemon Xiaozhi Pikachu Pokemon model boxed Figure",
                    ProductPrice = 110,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://cf.shopee.vn/file/c884c7dc4402cff185e16325b83b9bcf",
                    ManufacturerID = "PO"
                },
                new Product
                {
                    ProductID = 12,
                    ProductName = "Pin",
                    ProductDescription = "Pin by Fukuro on Pokemon",
                    ProductPrice = 150,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://i.pinimg.com/736x/46/db/3a/46db3a4b7a5df2b8a90ba0f1586efb27.jpg",
                    ManufacturerID = "PO"
                },
                new Product
                {
                    ProductID = 13,
                    ProductName = "Pokemon Mew & Mewtwo",
                    ProductDescription = "Estimasi Rilis / ETA : Q2 ( April Mei Juni ) 2021 Deadline / CLOSED PO : SOON / Sampai SLOT habis",
                    ProductPrice = 180,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://scontent.fsgn5-2.fna.fbcdn.net/v/t1.6435-9/123994433_2723755804605275_4171989894033708958_n.jpg?_nc_cat=105&ccb=1-7&_nc_sid=a26aad&_nc_ohc=Gk0DEKdbimoAX9BZWlF&_nc_ht=scontent.fsgn5-2.fna&oh=00_AfCze_yBFvQTqYn_bmQYnOYvbhOohasiz1Fwisl2mC3AOA&oe=643DFD19",
                    ManufacturerID = "PO"
                },
                new Product
                {
                    ProductID = 14,
                    ProductName = "Mega Charizard",
                    ProductDescription = "Anime Mega Charizard Action Figure Toys 13CM Pokemones Charizard Joint Figure Model Hot Toys Gifts for Kids Collection",
                    ProductPrice = 190,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://ae01.alicdn.com/kf/H535160176a914103a47de6cf7da73685E/Anime-Mega-Charizard-Action-Figure-Toys-13CM-Pokemones-Charizard-Joint-Figure-Model-Hot-Toys-Gifts-for.jpg",
                    ManufacturerID = "PO"
                },
                new Product
                {
                    ProductID = 15,
                    ProductName = "Egg Studio Gengar Family",
                    ProductDescription = "Egg Studio Gengar Family Limited Edition Painted Model Figure New Hot Toy Stock",
                    ProductPrice = 130,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://i.ebayimg.com/images/g/MCwAAOSwiWBf5UsC/s-l500.jpg",
                    ManufacturerID = "PO"
                },
                new Product
                {
                    ProductID = 16,
                    ProductName = "Pikachu Captain America",
                    ProductDescription = "PAPWELL Pikachu Captain America Action Figure 3.5 inch Cosplay Hot Toys Marvel Legends Pokemon Infinity War PVC Toy Figures Halloween Christmas Collectable Gifts Collectible Collectibles Gift for Kids",
                    ProductPrice = 180,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://images-na.ssl-images-amazon.com/images/I/41Vpyf6xliL.jpg",
                    ManufacturerID = "PO"
                },
                new Product
                {
                    ProductID = 17,
                    ProductName = "Articuno GK",
                    ProductDescription = "MODEL FANS IN-STOCK 17cm pocket monster Articuno GK resin made figure toy for Collection",
                    ProductPrice = 200,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://i.pinimg.com/736x/4d/a9/a4/4da9a41f720c322449f9dd102c96ea0c.jpg",
                    ManufacturerID = "PO"
                },
                new Product
                {
                    ProductID = 18,
                    ProductName = "Pokemon Scale World Sinnoh",
                    ProductDescription = "POKEMON SCALE WORLD SINNOH REGION CYNTHIA & GARCHOMP W/O GUM",
                    ProductPrice = 250,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://cdn.shopify.com/s/files/1/1367/5823/products/NewProject_1_f907459f-254a-4c84-a8b2-5db5496cab8b.jpg?v=1640858722",
                    ManufacturerID = "PO"
                },
                new Product
                {
                    ProductID = 19,
                    ProductName = "Mewtwo",
                    ProductDescription = "BANDAI D-ARTS: MEWTWO W/ MEW - POKEMON POCKET MONSTERS",
                    ProductPrice = 210,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://www.jhstoys.com/uploads/6/1/3/7/61377571/s737619424575262359_p1306_i824_w454.jpeg",
                    ManufacturerID = "PO"
                },
                new Product
                {
                    ProductID = 20,
                    ProductName = "Anime Action Figure Cosplay Toy Cartoon Statue Ornaments",
                    ProductDescription = "Amazon Hot sale Anime Action Figure Cosplay Toy Cartoon Statue Ornaments Model PVC",
                    ProductPrice = 190,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://sc01.alicdn.com/kf/H45647a1ecd7c4966b8970dc46f7da3eck.jpg",
                    ManufacturerID = "PO"
                },
                new Product
                {
                    ProductID = 21,
                    ProductName = "Spider Man",
                    ProductDescription = "Hot Toys Spider-Man Advanced Suit 1/6 Sixth Scale Figure Marvel Video Game Masterpiece Series Action Figure",
                    ProductPrice = 110,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://m.media-amazon.com/images/W/IMAGERENDERING_521856-T1/images/I/51nPKRglyPL.jpg",
                    ManufacturerID = "AD"
                },
                new Product
                {
                    ProductID = 22,
                    ProductName = "Zombie Hunter Spider Man",
                    ProductDescription = "Marvel: What If...?: Hot Toys Action Figure: Zombie Hunter Spider-Man",
                    ProductPrice = 150,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://media.forbiddenplanet.com/products/f2/d3/6c4e5e8e1ddbd9251b36ac9c4d4646d66f67.png",
                    ManufacturerID = "AD"
                },
                new Product
                {
                    ProductID = 23,
                    ProductName = "Captain America",
                    ProductDescription = "Hot Toys Marvel: Avengers Age of Ultron- Captain America 1/6th Scale Collectible Figure MMS 281",
                    ProductPrice = 180,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://cdn-amz.woka.io/images/I/61uK0d1PVSL.jpg",
                    ManufacturerID = "AD"
                },
                new Product
                {
                    ProductID = 24,
                    ProductName = "Captain Marvel",
                    ProductDescription = "Hot Toys 1:6 Captain Marvel",
                    ProductPrice = 190,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://m.media-amazon.com/images/W/IMAGERENDERING_521856-T1/images/I/61TWBJbHyJL.jpg",
                    ManufacturerID = "AD"
                },
                new Product
                {
                    ProductID = 25,
                    ProductName = "DB Iron Man",
                    ProductDescription = "Marvel: Avengers Endgame - BD Iron Man Mark LXXXV 1:6 Scale Figure - Hot Toys",
                    ProductPrice = 130,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://www.twilight-zone.nl/WebRoot/Pins/Shops/Twilightzone/5DE8/E6F3/4363/B0B2/B9B8/0A0C/05BA/7B1A/SSHOT904923_0_ml.jpg",
                    ManufacturerID = "AD"
                },
                new Product
                {
                    ProductID = 26,
                    ProductName = "Captain America",
                    ProductDescription = "MARVEL STUDIOS: THE FIRST TEN YEARS CAPTAIN AMERICA (CONCEPT ART VERSION) MMS488",
                    ProductPrice = 180,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://kingtoys.com.vn/upload/product/pd15307736741xf-1729.jpg",
                    ManufacturerID = "AD"
                },
                new Product
                {
                    ProductID = 27,
                    ProductName = "Multiverse Black",
                    ProductDescription = "Taskmaster 1/6 Scale Collectible Figures MM5602 Marvel Multiverse Black Widow 30cm MVHT04",
                    ProductPrice = 200,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://sapo.dktcdn.net/100/441/564/variants/mvht04_03-1667294101993.jpg",
                    ManufacturerID = "AD"
                },
                new Product
                {
                    ProductID = 28,
                    ProductName = "Doctor Strange",
                    ProductDescription = "Hot Toys Marvel Doctor Strange Benedict Cumberbatch 1/6 Scale 12 Figure",
                    ProductPrice = 250,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://cdn-amz.woka.io/images/I/61SK7+aajHL.jpg",
                    ManufacturerID = "AD"
                },
                new Product
                {
                    ProductID = 29,
                    ProductName = "Thanos",
                    ProductDescription = "Hot Toys Marvel Comics Avengers Endgame Thanos (Battle Damaged Version) 1/6 Scale Collectible Figure",
                    ProductPrice = 210,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://cdn-amz.woka.io/images/I/81K1KNvcqxL.jpg",
                    ManufacturerID = "AD"
                },
                new Product
                {
                    ProductID = 30,
                    ProductName = "Infinity War Thor",
                    ProductDescription = "Hot Toys Marvel Avengers Infinity War Thor 1/6 Scale Action Figure",
                    ProductPrice = 190,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://cdn-amz.woka.io/images/I/61C53oKNqUL.jpg",
                    ManufacturerID = "AD"
                },
                new Product
                {
                    ProductID = 31,
                    ProductName = "Son Goku",
                    ProductDescription = "Hot Toys Japanese Anime DBS Super saiyan blue DBZ Toys Action Figures Son goku",
                    ProductPrice = 110,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://sc04.alicdn.com/kf/Hfce2cdb7773043558de08b37f74184f8V.jpg",
                    ManufacturerID = "DRB"
                },
                new Product
                {
                    ProductID = 32,
                    ProductName = "Vegeta",
                    ProductDescription = "Dragon Ball Z Gogeta Anime Figures Stardust Breaker Angel Gogeta Action Figurine Dragonball Model Vegeta Hot Toys Collector Doll",
                    ProductPrice = 150,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://ae01.alicdn.com/kf/H2644f4d3fea144fd958c60ff28cf04ac6/Dragon-Ball-Z-Gogeta-Anime-Figures-Stardust-Breaker-Angel-Gogeta-Action-Figurine-Dragonball-Model-Vegeta-Hot.jpg",
                    ManufacturerID = "DRB"
                },
                new Product
                {
                    ProductID = 33,
                    ProductName = "Son Goku",
                    ProductDescription = "S.H.Figuarts Dragon Ball Super - Goku (Ultra Instinct) Figure",
                    ProductPrice = 180,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://cdn.shopify.com/s/files/1/0453/8535/1331/products/uig4_1200x1200.jpg?v=1598138284",
                    ManufacturerID = "DRB"
                },
                new Product
                {
                    ProductID = 34,
                    ProductName = "Super Saiyan 3 Son Goku",
                    ProductDescription = "Dragon Ball Z Estatua PVC FiguartsZERO (Extra Battle) Super Saiyan 3 Son Goku 21 cm",
                    ProductPrice = 190,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://cdn.shopify.com/s/files/1/0259/0195/7180/products/x_btn61515-2_2400x.jpg?v=1615572259",
                    ManufacturerID = "DRB"
                },
                new Product
                {
                    ProductID = 35,
                    ProductName = "Super Saiyan Trunks",
                    ProductDescription = "Dragon Ball Z Estatua PVC FiguartsZERO (Extra Battle) Super Saiyan Trunks The second Super Saiyan 28 cm",
                    ProductPrice = 130,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://cdn.shopify.com/s/files/1/0259/0195/7180/products/x_btn63904-2_2400x.jpg?v=1651234057",
                    ManufacturerID = "DRB"
                },
                new Product
                {
                    ProductID = 36,
                    ProductName = "Frieza First Form & Frieza Pod",
                    ProductDescription = "Dragon Ball Z Figura S.H. Figuarts Frieza First Form & Frieza Pod Set 11 cm",
                    ProductPrice = 180,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://cdn.shopify.com/s/files/1/0259/0195/7180/products/x_btn60827-7_a_2400x.jpg?v=1604684550",
                    ManufacturerID = "DRB"
                },
                new Product
                {
                    ProductID = 37,
                    ProductName = "Super Saiyan Son Goku",
                    ProductDescription = "Dragon Ball Z Estatua PVC FiguartsZERO (Extra Battle) Super Saiyan Son Goku - Are You Talking About Krillin?!!!!!- 27 cm",
                    ProductPrice = 200,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://cdn.shopify.com/s/files/1/0259/0195/7180/products/x_btn63239-5_a_2400x.jpg?v=1640101944",
                    ManufacturerID = "DRB"
                },
                new Product
                {
                    ProductID = 38,
                    ProductName = "Son Goku Kaio Ken",
                    ProductDescription = "Tsume Art 1/6 Dragon Ball Z Statue HQS Goku Kaio-Ken",
                    ProductPrice = 250,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://cdn.shopify.com/s/files/1/0259/0195/7180/products/123957632_5291618757530606_5914271509021700866_n-min_2400x.png?v=1619888382",
                    ManufacturerID = "DRB"
                },
                new Product
                {
                    ProductID = 39,
                    ProductName = "Porunga & Dende",
                    ProductDescription = "Dragon Ball Z Set de Figuras Porunga & Dende -Come Forth, Genuine Shenron!!- 28 cm",
                    ProductPrice = 210,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://cdn.shopify.com/s/files/1/0259/0195/7180/products/x_btn63757-4_a_2400x.jpg?v=1657196421",
                    ManufacturerID = "DRB"
                },
                new Product
                {
                    ProductID = 40,
                    ProductName = "Vegeta Galik Gun",
                    ProductDescription = "Tsume Art 1/6 Dragon Ball Z Statue HQS Vegeta Galik Gun",
                    ProductPrice = 190,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://cdn.shopify.com/s/files/1/0259/0195/7180/products/124851167_5295231337169348_6792909557059317513_n-min_2400x.png?v=1619887693",
                    ManufacturerID = "DRB"
                },
                new Product
                {
                    ProductID = 41,
                    ProductName = "Bear Brick Evangelion Unit 1",
                    ProductDescription = "BEAR BRICK Evangelion Unit 1 Chrome Ver",
                    ProductPrice = 110,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://product.hstatic.net/200000200733/product/bearbrick_evangelion_unit_1_chrome_ver._db835ea3f41b40dcbebf5ce88712b196_master.png",
                    ManufacturerID = "BR"
                },
                new Product
                {
                    ProductID = 42,
                    ProductName = "Bear Brick 2016 Xmas Stained",
                    ProductDescription = "BEAR BRICK 2016 Xmas Stained Glass Multi",
                    ProductPrice = 150,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://product.hstatic.net/200000200733/product/bearbrick_2016_xmas_stained_glass_multi_76087358d28b4deda509ffcd959f0c06_master.png",
                    ManufacturerID = "BR"
                },
                new Product
                {
                    ProductID = 43,
                    ProductName = "Bear Brick 20th Anniversity",
                    ProductDescription = "BEAR BRICK 20th Anniv. 1st Model White Chrome",
                    ProductPrice = 180,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://product.hstatic.net/200000200733/product/bearbrick_20th_anniv._1st_model_white_chrome_05ed4c88607e4a73aeb74e989a3dc9c5_master.png",
                    ManufacturerID = "BR"
                },
                new Product
                {
                    ProductID = 44,
                    ProductName = "Bear Brick 2G Reverse Gold",
                    ProductDescription = "BEAR BRICK 2G Reverse Gold Light Up",
                    ProductPrice = 190,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://product.hstatic.net/200000200733/product/bearbrick_2g_reverse_gold_light_up_5774f6749ae9433a9192264ccd0fb6b2_master.png",
                    ManufacturerID = "BR"
                },
                new Product
                {
                    ProductID = 45,
                    ProductName = "Bear Brick",
                    ProductDescription = "BEAR BRICK 8:16!",
                    ProductPrice = 130,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://product.hstatic.net/200000200733/product/bearbrick_816__11edfb0a654b4b05beb9b49359ecaf94_master.png",
                    ManufacturerID = "BR"
                },
                new Product
                {
                    ProductID = 46,
                    ProductName = "Bear Brick Ader",
                    ProductDescription = "BEAR BRICK ADER",
                    ProductPrice = 180,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://product.hstatic.net/200000200733/product/bearbrick_ader_3e20df6d7d1c47e5b24b30d2388b2b28_master.png",
                    ManufacturerID = "BR"
                },
                new Product
                {
                    ProductID = 47,
                    ProductName = "Bear Brick Alien Black",
                    ProductDescription = "BEAR BRICK Alien Black",
                    ProductPrice = 200,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://product.hstatic.net/200000200733/product/bearbrick_alien_black_3532ba16603f47b8ae026fb1e5262f95_master.png",
                    ManufacturerID = "BR"
                },
                new Product
                {
                    ProductID = 48,
                    ProductName = "Bear Brick - Batman Adventures",
                    ProductDescription = "Bear Brick 100% & 400% Robin (The New Batman Adventures)",
                    ProductPrice = 250,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://www.toygarden.com/media/catalog/product/cache/4c90c585ed8bd3c26bf289bb66dd7ed4/1/_/1_520.jpg",
                    ManufacturerID = "BR"
                },
                new Product
                {
                    ProductID = 49,
                    ProductName = "Bear Brick Sticky Fingers",
                    ProductDescription = "Bear Brick 100% & 400% The Rolling Stones 'Sticky Fingers' Design Ver",
                    ProductPrice = 210,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://www.toygarden.com/media/catalog/product/cache/4c90c585ed8bd3c26bf289bb66dd7ed4/1/_/1_508.jpg",
                    ManufacturerID = "BR"
                },
                new Product
                {
                    ProductID = 50,
                    ProductName = "Bear Brick The Birds",
                    ProductDescription = "Bear Brick 100% & 400% The Birds",
                    ProductPrice = 190,
                    Quantity = 100,
                    CreateOn = new DateTime(2022, 02, 14),
                    ProductImage = "https://www.toygarden.com/media/catalog/product/cache/4c90c585ed8bd3c26bf289bb66dd7ed4/1/_/1_407.jpg",
                    ManufacturerID = "BR"
                }
                );
            modelBuilder.Entity<Product>().HasOne(p => p.Manufacturers).WithMany(p => p.Products).HasForeignKey(p => p.ManufacturerID);
        }
    }
}

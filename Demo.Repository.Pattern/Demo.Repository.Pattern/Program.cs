using AutoMapper;
using Demo.Repository.Pattern.Data;
using Demo.Repository.Pattern.Domain;
using Demo.Repository.Pattern.Profiles;
using Demo.Repository.Pattern.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Demo.Repository.Pattern
{
    internal static class Program
    {
        private static readonly IConfiguration Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        private static readonly IMapper Mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ProductProfile>();
            // cfg.AddMaps(typeof(Program));
        }).CreateMapper();

        static Program()
        {
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public static async Task Main()
        {
            string? connectionString = Configuration.GetConnectionString("DB_CONNECTION");

            DbContextOptions<ProductDbContext> options = new DbContextOptionsBuilder<ProductDbContext>()
                // .UseSqlServer(connectionString)
                .LogTo(m => Print(m, ConsoleColor.Yellow), Microsoft.Extensions.Logging.LogLevel.Information)
                .UseNpgsql(connectionString, builder => builder.EnableRetryOnFailure())
                //.EnableSensitiveDataLogging()
                .Options;

            await using ProductDbContext db = new(options);
            // await db.Database.EnsureCreatedAsync();

            /*var p = new Product { 
                Id = 1, Name = "Product One", CreatedOn= DateTime.Now, CreatedBy = "mohammed"
            };
            p.CreatedBy = "mohammed"; */

            // Option 1:
            /*var products = await db.Products.ToListAsync();
            var productDtos = new List<ProductDto>();
            products.ForEach(p => productDtos.Add(Mapper.Map<ProductDto>(p)));
            productDtos.ForEach(p => Print(p.Name)); */

            // Option 2:
            /*var productDtos = await db.Products.ProjectTo<ProductDto>(Mapper.ConfigurationProvider).ToListAsync();
            productDtos.ForEach(p => Print(p.Name)); */

            // Option 3:
            using GenericRepository<Product, ProductDbContext> repo = new(db, Mapper);
            //var productDtos = await repo.FindAsync<ProductDto>(p => p.Name.Contains("One"), p => p.m);
            //productDtos.ForEach(p => Print(p.Name));

            NewProductSpec newProducts = new();
            ExpensiveProductSpec expProducts = new();

            // BaseSpecification<Product> combinedSpec = newProducts.And(expProducts);

            //var products = await repo.FindAsync(newProducts);
            List<Product> products = await repo.FindAsync(newProducts);
            Console.WriteLine(products.Count);
            products.ForEach(p => Print(p.Name));
        }

        private static void Print(string value, ConsoleColor consoleColor = ConsoleColor.Cyan)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}]: {value}");
            Console.ResetColor();
        }
    }
}
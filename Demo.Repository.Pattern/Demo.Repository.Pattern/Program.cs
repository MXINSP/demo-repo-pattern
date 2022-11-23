using AutoMapper;
using AutoMapper.QueryableExtensions;
using Demo.Repository.Pattern.Data;
using Demo.Repository.Pattern.Domain;
using Demo.Repository.Pattern.DTO;
using Demo.Repository.Pattern.Profiles;
using Demo.Repository.Pattern.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Demo.Repository.Pattern
{
    internal class Program
    {
        private static readonly IConfiguration Configuration;
        private static readonly IMapper Mapper;

        static Program()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductProfile>();
                // cfg.AddMaps(typeof(Program));

            }).CreateMapper();

            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        static async Task Main(string[] args)
        {
            var connectionString = Configuration.GetConnectionString("DB_CONNECTION");

            var options = new DbContextOptionsBuilder<ProductDbContext>()
                // .UseSqlServer(connectionString)
                .LogTo(m => Print(m, ConsoleColor.Yellow), Microsoft.Extensions.Logging.LogLevel.Information)
                .UseNpgsql(connectionString, builder => builder.EnableRetryOnFailure())
                //.EnableSensitiveDataLogging()
                .Options;

            await using var db = new ProductDbContext(options);
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
            using var repo = new GenericRepository<Product, ProductDbContext>(db, Mapper);
            var productDtos = await repo.FindAsync<ProductDto>(p => p.Name.Contains("One"));
            //productDtos.ForEach(p => Print(p.Name));

            var newProducts = new NewProductSpec();
            var expProducts = new ExpensiveProductSpec();

            //var products = await repo.FindAsync(newProducts);
            var products = await repo.FindAsync(newProducts.And(expProducts));
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
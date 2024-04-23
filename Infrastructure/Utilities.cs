using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class Utilities
    {
        public static async Task UpdateDatabase(this IServiceCollection services)
        {
            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            using IServiceScope scope = serviceProvider.CreateScope();
            StoreContext context = scope.ServiceProvider.GetRequiredService<StoreContext>();
            ILoggerFactory loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            ILogger logger = loggerFactory.CreateLogger(typeof(Utilities));

            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured during the migration");
            }
        }

        public static async Task SeedData(this IServiceCollection services)
        {
            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            using IServiceScope scope = serviceProvider.CreateScope();
            StoreContext context = scope.ServiceProvider.GetRequiredService<StoreContext>();
            ILoggerFactory loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            ILogger logger = loggerFactory.CreateLogger(typeof(Utilities));

            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured during the migration");
            }

            try
            {
                if (!context.Products.Any())
                {
                    SeedProductBrands(context);
                    await context.SaveChangesAsync();
                    SeedProductTypes(context);
                    await context.SaveChangesAsync();
                    SeedProducts(context);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured during Seeding Data");
            }
            
        }

        private static void SeedProducts(StoreContext context)
        {
            var productsJson = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize< List<Product>>(productsJson);
            context.Products.AddRange(products);
        }

        private static void SeedProductBrands(StoreContext context)
        {
            var brandsJson = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsJson);
            context.ProductBrands.AddRange(brands);
        }

        private static void SeedProductTypes(StoreContext context)
        {
            var typesJson = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
            var types = JsonSerializer.Deserialize< List<ProductType>>(typesJson);
            context.ProductTypes.AddRange(types);
        }
    }
}
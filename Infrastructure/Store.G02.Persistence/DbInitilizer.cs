using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Identity;
using Store.G02.Domain.Entities.Orders;
using Store.G02.Domain.Entities.Products;
using Store.G02.Persistence.Data.Contexts;
using Store.G02.Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G02.Persistence
{
    public class DbInitilizer(
        StoreDbContext _context,
        IdentityStoreDbContext _identityContext,
        UserManager<AppUser> _userManager,
        RoleManager<IdentityRole> _roleManager
        ) : IDbInitializer
    {
        public async Task IdentityInitializeAsync()
        {
            if (_identityContext.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {
                await _identityContext.Database.MigrateAsync();
            }
            if (!_identityContext.Roles.Any())
            {
               await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
               await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            }
            if (!_identityContext.Users.Any())
            {
                var superAdmin = new AppUser
                {
                    UserName = "SuperAdmin",
                    DisplayName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    PhoneNumber = "01123453"
                };
                var admin = new AppUser
                {
                    UserName = "Admin",
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "01123453"
                };
               await _userManager.CreateAsync(superAdmin, "P@ssw0rd");
               await _userManager.CreateAsync(admin, "P@ssw0rd");
               await _userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
               await _userManager.AddToRoleAsync(admin, "Admin");
            }

        }

        public async Task InitializeAsync()
        {

            if ( _context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {
                await _context.Database.MigrateAsync();
            }
            if (!_context.ProductBrands.Any())
            {
                var Brandsdata = await File.ReadAllTextAsync(@"..\Infrastructure\Store.G02.Persistence\Data\DataSeeding\brands.json");
      
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(Brandsdata);
                if (Brands is not null && Brands.Count>0)
                {
                   await _context.ProductBrands.AddRangeAsync(Brands);
                }                
            }
            if (!_context.DeliveryMethods.Any())
            {
                var deliveryData = await File.ReadAllTextAsync(@"..\Infrastructure\Store.G02.Persistence\Data\DataSeeding\delivery.json");
      
                var deliverMethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                if (deliverMethod is not null && deliverMethod.Count>0)
                {
                   await _context.DeliveryMethods.AddRangeAsync(deliverMethod);
                }                
            }
            if (!_context.ProductTypes.Any())
            {
              var Typesdata = await File.ReadAllTextAsync(@"..\Infrastructure\Store.G02.Persistence\Data\DataSeeding\types.json");

              var Types = JsonSerializer.Deserialize<List<ProductType>>(Typesdata);
                if (Types is not null && Types.Count>0)
                {
                   await _context.AddRangeAsync(Types);
                }
            }
            if (!_context.Products.Any())
            {
              var Productsdata= await File.ReadAllTextAsync(@"..\Infrastructure\Store.G02.Persistence\Data\DataSeeding\products.json");
               var Products= JsonSerializer.Deserialize<List<Product>>(Productsdata);
                if(Products is not null  && Products.Count > 0)
                {
                    await _context.Products.AddRangeAsync(Products);
                }
            }
           await _context.SaveChangesAsync();
        }
    
    }
}

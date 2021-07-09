using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheCuriousReaders.DataAccess.Entities;
using TheCuriousReaders.Models.Enums;

namespace TheCuriousReaders.DataAccess
{
    public class CuriousReadersInitializer
    {
        public static async Task Initialize(CuriousReadersContext context, IServiceProvider serviceProvider)
        {
            await new CuriousReadersInitializer().Seed(context, serviceProvider);
        }

        public async Task Seed(CuriousReadersContext context, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetService<UserManager<UserEntity>>();

            await SeedRole(roleManager, Role.Librarian.ToString());
            await SeedRole(roleManager, Role.Customer.ToString());
            await SeedAdmin(userManager);
        }

        public async Task SeedRole(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);


            if (role == null)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        public async Task SeedAdmin(UserManager<UserEntity> userManager)
        {
            if (await userManager.FindByEmailAsync("admin@endava.com") == null)
            {
                var user = new UserEntity
                {
                    UserName = "admin@endava.com",
                    FirstName = "The",
                    LastName = "Admin",
                    Email = "admin@endava.com",
                    PhoneNumber = "0889445103",
                    Address = new AddressEntity
                    {
                        Country = "Bulgaria",
                        City = "Sofia",
                        Street = "Ivan Vazov",
                        StreetNumber = "69",
                    }
                };

                var result = await userManager.CreateAsync(user, "!Password123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Role.Librarian.ToString());
                }
            }

        }
    }
}

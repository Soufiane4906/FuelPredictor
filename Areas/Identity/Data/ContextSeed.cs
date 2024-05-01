using Microsoft.AspNetCore.Identity;
using FuelPredictor.Models.Users;
using FuelPredictor.Models.V2;
using Microsoft.EntityFrameworkCore;

namespace FuelPredictor.Data
{
    public static class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Gerant.ToString()));

        }


        public static async Task SeedDataAsync(FuelPredictorContext dbContext)
        {
            if (dbContext.Station.Any() || dbContext.Companies.Any())
            {
                return; // Data already seeded
            }

            // Seed companies
            var companies = new List<Company>
        {
            new Company { Nom = "Africa", Pays = "Morocco", Adresse = "123 Avenue XYZ", Email = "africa@example.com", Telephone = "123-456-7890" },
            new Company { Nom = "Shell", Pays = "Morocco", Adresse = "456 Rue ABC", Email = "shell@example.com", Telephone = "987-654-3210" },
                // Add more companies as needed
        };

            await dbContext.Companies.AddRangeAsync(companies);
            await dbContext.SaveChangesAsync();

            // Seed stations
            var africaCompany = companies.FirstOrDefault(c => c.Nom == "Africa");
            var shellCompany = companies.FirstOrDefault(c => c.Nom == "Shell");

            var stations = new List<Station>
        {
            new Station { Nom = "Station 1", Adresse = "123 Rue ABC", Latitude = 31.6352, Longitude = -7.9928, Company = africaCompany },
            new Station { Nom = "Station 2", Adresse = "456 Avenue XYZ", Latitude = 33.5731, Longitude = -7.5898, Company = shellCompany },
                // Add more stations as needed
        };

            await dbContext.Station.AddRangeAsync(stations);
            await dbContext.SaveChangesAsync();
        }

    
   



        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser
            {
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                FirstName = "Soufiane",
                LastName = "Aniba",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
                
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "@Test123");
/*                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Consult.ToString());*/
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Gerant.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.SuperAdmin.ToString());
                }

            }
        }
    }
}

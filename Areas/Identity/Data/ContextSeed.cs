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

            var gerant = dbContext.ApplicationUser.FirstOrDefault(u => u.Id.Equals("09658bad-fc4e-413d-8a4f-01514178daeb"));

            var stations = new List<Station>
        {
            new Station { Nom = "Station 4", Adresse = "123 Rue ABC", Latitude = 31.6352, Longitude = -7.9928, Company = africaCompany , Gerant = gerant , IDGerant="09658bad-fc4e-413d-8a4f-01514178daeb"},
            new Station { Nom = "Station 2", Adresse = "456 Avenue XYZ", Latitude = 33.5731, Longitude = -7.5898, Company = shellCompany ,  Gerant = gerant , IDGerant="09658bad-fc4e-413d-8a4f-01514178daeb" },
                // Add more stations as needed
        };
            var station = new Station { Nom = "Station 5", Adresse = "123 Rue ABC", Latitude = 31.6352, Longitude = -7.9928, Company = africaCompany, Gerant = gerant , IDGerant = "09658bad-fc4e-413d-8a4f-01514178daeb" };


            // Supposons que dbContext soit votre contexte de base de données Entity Framework

            var prixJournaliers = new List<PrixJournalier>();

            DateTime dateDebut = new DateTime(DateTime.Now.Year, 5, 1); // Date de début, 1er mai
            DateTime dateFin = new DateTime(DateTime.Now.Year, 5, 15);  // Date de fin, 15 mai

            for (DateTime date = dateDebut; date <= dateFin; date = date.AddDays(1))
            {
                PrixJournalier nouveauPrix = new PrixJournalier
                {
                    Carburant = dbContext.Carburant.FirstOrDefault(u => u.Id == 2),
                    Station = station, // Assurez-vous que "station" est déjà défini
                    date = date
                };

                prixJournaliers.Add(nouveauPrix);
            }


            await dbContext.Station.AddRangeAsync(stations);
            await dbContext.PrixJournalier.AddRangeAsync(prixJournaliers);
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

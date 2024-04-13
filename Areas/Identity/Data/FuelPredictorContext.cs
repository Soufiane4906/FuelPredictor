using FuelPredictor.Models.Users;
using FuelPredictor.Models.V2;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FuelPredictor.Data;

public class FuelPredictorContext : IdentityDbContext<IdentityUser>
{
    public FuelPredictorContext(DbContextOptions<FuelPredictorContext> options)
        : base(options)
    {
    }
    public DbSet<ApplicationUser> ApplicationUser { get; set; }
    public DbSet<Station> Station { get; set; }
    public DbSet<PrixJournalier> PrixJournalier { get; set; }
    public DbSet<Carburant> Carburant { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {



        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Identity");
        modelBuilder.Entity<IdentityUser>(entity =>
        {
            entity.ToTable(name: "User");
        });
        modelBuilder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable(name: "Role");
        });
        modelBuilder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoles");
        });
        modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("UserClaims");
        });
        modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("UserLogins");
        });
        modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("RoleClaims");
        });
        modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserTokens");
        });




    }



    }


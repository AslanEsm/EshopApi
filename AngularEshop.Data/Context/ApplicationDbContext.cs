using System.IO;
using AngularEshop.Common.Utilities;
using AngularEshop.Data.Validators;
using AngularEshop.Entities.Access;
using AngularEshop.Entities.Account;
using AngularEshop.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace AngularEshop.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    IConfigurationRoot configuration = new ConfigurationBuilder()
        //        .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../AngularEshop.WebApi"))
        //        .AddJsonFile("appsettings.json")
        //        .AddJsonFile($"appsettings.Development.json", optional: true)
        //        .Build();

        //    var connectionString = configuration["ConnectionStrings:Development"];
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer(connectionString);
        //}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entitiesConfig = typeof(SliderValidator).Assembly;
            var entitiesAssembly = typeof(IEntity).Assembly;

            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            modelBuilder.RegisterEntityTypeConfiguration(entitiesConfig);
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.AddSequentialGuidForIdConvention();
            modelBuilder.AddPluralizingTableNameConvention();

            base.OnModelCreating(modelBuilder);

        }
    }
}

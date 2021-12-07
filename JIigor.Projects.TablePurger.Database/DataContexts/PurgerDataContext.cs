using System;
using System.Reflection;
using JIigor.Projects.TablePurger.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JIigor.Projects.TablePurger.Database.DataContexts
{
    public sealed class PurgerDataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public PurgerDataContext(DbContextOptions<PurgerDataContext> options, IConfiguration configuration) 
            : base (options)
        {
            _configuration = configuration;
            PurgeableRecords = Set<PurgeableRecord>();
        }

        public DbSet<PurgeableRecord> PurgeableRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));

            var databaseConfiguration = new DatabaseConfiguration();
            _configuration.Bind("SqlServerConfiguration", databaseConfiguration);

            _ = modelBuilder.HasDefaultSchema(databaseConfiguration.Schema)
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}

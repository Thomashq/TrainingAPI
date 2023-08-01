using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using Utility.Models;

namespace Utility
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Mail>? Mail { get; set; }
        public DbSet<Person>? Person { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            BaseModel.Configure(modelBuilder);
        }

        public void RollbackChanges()
        {
            ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged)
                .ToList()
                .ForEach(e =>
                {
                    e.State = EntityState.Detached;
                    e.Reload();
                });
        }
    }

    public static class DataContextFactory
    {
        public static DataContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("conn"));

            var context = new DataContext(optionsBuilder.Options, configuration);
            return context;
        }
    }
}

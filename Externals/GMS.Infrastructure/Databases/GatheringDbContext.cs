using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Infrastructure.Databases
{
    public class GatheringDbContext(DbContextOptions<GatheringDbContext> options) : DbContext(options)
    {
        public DbSet<Gathering> Gatherings {  get; set; }
        public DbSet<Attendee> Attendees {  get; set; }
        public DbSet<Invitation> Invitations {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(GatheringDbContext).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly, type =>
                type.Namespace != null &&
                type.Namespace.StartsWith("GMS.Infrastructure.Configurations.GatheringDb"));
            base.OnModelCreating(modelBuilder);
        }
    }
}

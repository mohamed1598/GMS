using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GMS.Infrastructure.Databases
{
    public class MemberDbContext(DbContextOptions<MemberDbContext> options) : DbContext(options)
    {
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(MemberDbContext).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly, type =>
                type.Namespace != null &&
                type.Namespace.StartsWith("GMS.Infrastructure.Configurations.MemberDb"));
            base.OnModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}

using GMS.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMS.Domain.ValueObjects;

namespace GMS.Infrastructure.Configurations.GatheringDb
{
    public class GatheringConfiguration : IEntityTypeConfiguration<Gathering>
    {
        public void Configure(EntityTypeBuilder<Gathering> builder)
        {

            builder.Property(g => g.Id)
                .HasConversion(
                    gatheringId => gatheringId.Value,
                    value => new GatheringId(value)
                );

            builder.Property(g => g.CreatorId)
                .HasConversion(
                    memberId => memberId.Value,
                    value => new MemberId(value)
                );

            builder.Property(g => g.Name)
                .HasConversion(
                    name => name.Value,
                    value => new Name(value)
                );

            builder
                .HasMany(g => g.Attendees)
                .WithOne()
                .HasForeignKey("GatheringId");

            builder
                .HasMany(g => g.Invitations)
                .WithOne()
                .HasForeignKey("GatheringId");

        }

    }
}

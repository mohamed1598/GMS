using GMS.Domain.Entities;
using GMS.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Infrastructure.Configurations.GatheringDb
{
    public class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
    {
        public void Configure(EntityTypeBuilder<Attendee> builder)
        {

            builder.Property(a => a.Id)
                .HasConversion(
                    attendeeId => attendeeId.Value,
                    value => new AttendeeId(value)
                );

            builder.Property(a => a.MemberId)
                .HasConversion(
                    memberId => memberId.Value,
                    value => new MemberId(value)
                );

            builder.Property(a => a.GatheringId)
                .HasConversion(
                    gatheringId => gatheringId.Value,
                    value => new GatheringId(value)
            );

            builder.HasIndex(a => new { a.GatheringId, a.MemberId })
            .HasDatabaseName("IX_GatheringId_MemberId")
            .IsUnique(false)
            .IsClustered(false);
        }
    }
}

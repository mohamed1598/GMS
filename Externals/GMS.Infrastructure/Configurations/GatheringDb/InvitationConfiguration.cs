using GMS.Domain.Entities;
using GMS.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Infrastructure.Configurations.GatheringDb
{
    public class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.Property(i => i.Id)
                .HasConversion(
                    invitationId => invitationId.Value,
                    value => new InvitationId(value)
                );

            builder.Property(i => i.MemberId)
                .HasConversion(
                    memberId => memberId.Value,
                    value => new MemberId(value)
                );

            builder.Property(i => i.GatheringId)
                .HasConversion(
                    gatheringId => gatheringId.Value,
                    value => new GatheringId(value)
            );
        }
    }
}

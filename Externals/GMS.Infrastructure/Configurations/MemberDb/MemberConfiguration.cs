using GMS.Domain.Entities;
using GMS.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMS.Infrastructure.Configurations.MemberDb
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id)
                .HasConversion(
                    memberId => memberId.Value,
                    value => new MemberId(value)
                );

            builder.Property(m => m.FirstName)
                .HasConversion(
                    firstName => firstName.Value,
                    value => FirstName.Create(value).Value
                );

            builder.Property(m => m.LastName)
                .HasConversion(
                    lastName => lastName.Value,
                    value => LastName.Create(value).Value
                );

            builder.Property(m => m.Email)
                .HasConversion(
                    email => email.Value,
                    value => Email.Create(value).Value
                );
        }
    }
}

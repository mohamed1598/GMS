using GMS.Domain.Primitives;
using GMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.Entities
{
    public sealed class Member:AggregateRoot<MemberId>
    {
        public Member(MemberId id,FirstName firstName, LastName lastName, Email email):base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
        protected Member():base(MemberId.Create(Guid.NewGuid()).Value)
        {
            
        }

        public FirstName FirstName { get; set; } = null!;
        public LastName LastName { get; set; } = null!;
        public Email Email { get; set; } = null!;
    }
}

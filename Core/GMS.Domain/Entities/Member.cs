using GMS.Domain.Primitives;
using GMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.Entities
{
    public sealed class Member:Entity
    {
        public Member(Guid id,FirstName firstName, string lastName, string email):base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
        protected Member():base(Guid.NewGuid())
        {
            
        }

        public FirstName FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}

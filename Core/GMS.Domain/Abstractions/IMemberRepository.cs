using GMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.Abstractions
{
    public interface IMemberRepository
    {
        void Add(Member member);
        Task<Member> GetByIdAsync(Guid memberId, CancellationToken cancellationToken);
    }
}

using GMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.Abstractions
{
    public interface IInvitationRepository
    {
        void Add(Invitation invitation);
        Task<Invitation> GetByIdAsync(Guid invitationId, CancellationToken cancellationToken);
    }
}

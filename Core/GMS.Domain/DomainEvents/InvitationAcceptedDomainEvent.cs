using GMS.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.DomainEvents
{
    public record InvitationAcceptedDomainEvent(
        Guid InvitationId,
        Guid GatheringId):IDomainEvent;
}

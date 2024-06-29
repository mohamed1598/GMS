using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Application.Gathering.Commands.SendInvitation
{
    public sealed record SendInvitationCommand(Guid MemberId, Guid GatheringId) : IRequest<Unit>;
}

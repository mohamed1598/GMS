using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Application.Gathering.Commands.AcceptInvitation
{
    public sealed record AcceptInvitationCommand(Guid InvitationId) : IRequest<Unit>;
}

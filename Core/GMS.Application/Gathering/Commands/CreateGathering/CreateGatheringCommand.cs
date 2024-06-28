using GMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Application.Gathering.Commands.CreateGathering
{
    public sealed record CreateGatheringCommand
    (
        Guid MemberId,
        GatheringType Type,
        DateTime SchedualedAtUtc,
        string Name,
        string? Location,
        int? MaximumNumberOfAttendees,
        int? InvitationValidBeforeInHours):IRequest<Unit>;
}

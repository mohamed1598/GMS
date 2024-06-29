using GMS.Domain.Abstractions;
using GMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Application.Gathering.Commands.AcceptInvitation
{
    public class AcceptInvitationCommandHandler(
        IUnitOfWork unitOfWork,
        IGatheringRepository gatheringRepository,
        IAttendeeRepository attendeeRepository)
        : IRequestHandler<AcceptInvitationCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IGatheringRepository _gatheringRepository = gatheringRepository;
        private readonly IAttendeeRepository _attendeeRepository = attendeeRepository;
        public async Task<Unit> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
        {
            var gathering = await _gatheringRepository.GetByIdWithCreatorAsync(request.GatheringId, cancellationToken);

            if ( gathering is null)
                return Unit.Value;

            var invitation = gathering.Invitations.FirstOrDefault(i => i.Id.Value == request.InvitationId);
            if (invitation is null || invitation.Status != InvitationStatus.Pending)
                return Unit.Value;

            var attendee = gathering.AcceptInvitation(invitation);
            if (attendee is not null)
                _attendeeRepository.Add(attendee);

            //send mail

            await _unitOfWork.SaveChangesAsync(cancellationToken);


            return Unit.Value;
        }
    }
}

using GMS.Application.Gathering.Commands.SendInvitation;
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
        IMemberRepository memberRepository, 
        IGatheringRepository gatheringRepository, 
        IInvitationRepository invitationRepository,
        IAttendeeRepository attendeeRepository) 
        : IRequestHandler<AcceptInvitationCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMemberRepository _memberRepository = memberRepository;
        private readonly IGatheringRepository _gatheringRepository = gatheringRepository;
        private readonly IInvitationRepository _invitationRepository = invitationRepository;
        private readonly IAttendeeRepository _attendeeRepository = attendeeRepository;
        public async Task<Unit> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
        {
            var invitation = await _invitationRepository.GetByIdAsync(request.InvitationId,cancellationToken);
            if (invitation is null || invitation.Status != InvitationStatus.Pending)
                return Unit.Value;

            var member  = await _memberRepository.GetByIdAsync(invitation.MemberId,cancellationToken);

            var gathering = await _gatheringRepository.GetByIdWithCreatorAsync(member.Id,cancellationToken);

            if(member is null || gathering is null)
                return Unit.Value;

            var attendee = gathering.AcceptInvitation(invitation);
            if (attendee is not null)
                _attendeeRepository.Add(attendee);

            //if(invitation.Status == InvitationStatus.Accepted)
            // implement send mail

            await _unitOfWork.SaveChangesAsync(cancellationToken);


            return Unit.Value;
        }
    }
}

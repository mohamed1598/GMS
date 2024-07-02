using GMS.Domain.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Application.Gathering.Commands.SendInvitation
{
    public sealed class SendInvitationCommandHandler(IUnitOfWork unitOfWork, IMemberRepository memberRepository, IGatheringRepository gatheringRepository, IInvitationRepository invitationRepository) : IRequestHandler<SendInvitationCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMemberRepository _memberRepository = memberRepository;
        private readonly IGatheringRepository _gatheringRepository = gatheringRepository;
        private readonly IInvitationRepository _invitationRepository = invitationRepository;

        public async Task<Unit> Handle(SendInvitationCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

            var gathering = await _gatheringRepository.GetByIdWithCreatorAsync(request.GatheringId, cancellationToken);

            if (member is null || gathering is null) return Unit.Value;

            var invitationResult = gathering.SendInvitation(member.Id);
            if (invitationResult.IsFailure)
                //log here
                return Unit.Value;

            _invitationRepository.Add(invitationResult.Value);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            //send email not implemented

            return Unit.Value;
        }
    }
}

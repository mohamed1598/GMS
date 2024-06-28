using GMS.Domain.Abstractions;
using GatheringEntity = GMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Application.Gathering.Commands.CreateGathering
{
    public sealed class CreateGatheringCommandHandler(
        IUnitOfWork unitOfWork,
        IMemberRepository memberRepository,
        IGatheringRepository gatheringRepository) : IRequestHandler<CreateGatheringCommand,Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMemberRepository _memberRepository = memberRepository;
        private readonly IGatheringRepository _gatheringRepository = gatheringRepository;
        public async Task<Unit> Handle(CreateGatheringCommand request, CancellationToken cancellationToken)
        {
            var member = await _memberRepository.GetByIdAsync(request.MemberId,cancellationToken);

            if (member is null)
                return Unit.Value;

            var gathering = GatheringEntity.Gathering.Create(
                Guid.NewGuid(),
                member,
                request.Type,
                request.SchedualedAtUtc,
                request.Name,
                request.Location,
                request.MaximumNumberOfAttendees,
                request.InvitationValidBeforeInHours);

            _gatheringRepository.Add(gathering);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

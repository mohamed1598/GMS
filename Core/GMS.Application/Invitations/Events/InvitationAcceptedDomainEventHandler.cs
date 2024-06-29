using GMS.Domain.Abstractions;
using GMS.Domain.DomainEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Application.Invitations.Events
{
    public class InvitationAcceptedDomainEventHandler(IEmailService emailService, IGatheringRepository gatheringRepository)
                : INotificationHandler<InvitationAcceptedDomainEvent>
    {
        private readonly IEmailService _emailService = emailService;
        private readonly IGatheringRepository _gatheringRepository = gatheringRepository;

        public async Task Handle(InvitationAcceptedDomainEvent notification, CancellationToken cancellationToken)
        {
            var gathering = await _gatheringRepository.GetByIdWithCreatorAsync(notification.GatheringId,cancellationToken);

            if (gathering is null) return;

            await _emailService.SendInvitationAcceptedEmailAsync(gathering,cancellationToken);
        }
    }
}

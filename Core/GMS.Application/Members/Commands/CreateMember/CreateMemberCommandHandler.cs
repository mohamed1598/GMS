using GMS.Domain.Abstractions;
using GMS.Domain.Entities;
using GMS.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Application.Members.Commands.CreateMember
{
    public sealed class CreateMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateMemberCommand, Unit>
    {
        private readonly IMemberRepository _memberRepository = memberRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Unit> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var firstNameResult = FirstName.Create(request.FirstName);
            if (firstNameResult.IsFailure)
                //log error
                return Unit.Value;

            var lastNameResult = LastName.Create(request.LastName);
            if (lastNameResult.IsFailure)
                //log error
                return Unit.Value;

            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailure)
                //log error
                return Unit.Value;

            var member = new Member(
                MemberId.Create(Guid.NewGuid()).Value,
                firstNameResult.Value,
                lastNameResult.Value,
                emailResult.Value);

            _memberRepository.Add(member);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}

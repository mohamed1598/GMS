using GMS.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Application.Members.Commands.CreateMember;

public record CreateMemberCommand(
    string Email,
    string FirstName,
    string LastName):IRequest<Unit>;

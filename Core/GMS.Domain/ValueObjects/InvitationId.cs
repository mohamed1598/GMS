using GMS.Domain.Errors;
using GMS.Domain.Primitives;
using GMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GMS.Domain.ValueObjects;

public class InvitationId : ValueObject
{
    public InvitationId(Guid value) => Value = value;
    public Guid Value { get; }

    public static Result<InvitationId> Create(Guid id)
    {
        if (id == default)
            return Result.Failure<InvitationId>(ValueObjectErrors.InvitationId.NotValid);

        return new InvitationId(id);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

}

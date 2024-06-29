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

public class MemberId : ValueObject
{
    public MemberId(Guid value) => Value = value;
    public Guid Value { get; }

    public static Result<MemberId> Create(Guid id)
    {
        if (id == default)
            return Result.Failure<MemberId>(ValueObjectErrors.MemberId.NotValid);

        return new MemberId(id);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

}

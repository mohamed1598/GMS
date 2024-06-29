using GMS.Domain.Errors;
using GMS.Domain.Primitives;
using GMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.ValueObjects;

public class LastName : ValueObject
{
    public const int MaxLength = 50;

    public LastName(string value) => Value = value;
    public string Value { get; }

    public static Result<LastName> Create(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure<LastName>(ValueObjectErrors.LastName.Empty);

        if (lastName.Length > MaxLength)
            return Result.Failure<LastName>(ValueObjectErrors.LastName.TooLong);

        return new LastName(lastName);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

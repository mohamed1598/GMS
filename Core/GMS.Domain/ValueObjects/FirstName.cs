using GMS.Domain.Errors;
using GMS.Domain.Primitives;
using GMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.ValueObjects;

public class FirstName : ValueObject
{
    public const int MaxLength = 50;

    public FirstName(string value) => Value = value;
    public string Value { get; }

    public static Result<FirstName> Create(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure<FirstName>(ValueObjectErrors.FirstName.Empty);

        if (firstName.Length > MaxLength)
            return Result.Failure<FirstName>(ValueObjectErrors.FirstName.TooLong);

        return new FirstName(firstName);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

using GMS.Domain.Errors;
using GMS.Domain.Primitives;
using GMS.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.ValueObjects;

public class Name : ValueObject
{
    public const int MaxLength = 50;

    public Name(string value) => Value = value;
    public string Value { get; }

    public static Result<Name> Create(string Name)
    {
        if (string.IsNullOrWhiteSpace(Name))
            return Result.Failure<Name>(ValueObjectErrors.Name.Empty);

        if (Name.Length > MaxLength)
            return Result.Failure<Name>(ValueObjectErrors.Name.TooLong);

        return new Name(Name);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}

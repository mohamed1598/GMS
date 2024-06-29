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

public class Email : ValueObject
{
    public Email(string value) => Value = value;
    public string Value { get; }

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<Email>(ValueObjectErrors.Email.Empty);

        if (!IsValidEmail(email))
            return Result.Failure<Email>(ValueObjectErrors.Email.NotValid);

        return new Email(email);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
    static bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

        Regex regex = new Regex(pattern);
        return regex.IsMatch(email);
    }

}

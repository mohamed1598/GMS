using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.Primitives;

public abstract class ValueObject:IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetAtomicValues();

    public bool Equals(ValueObject? other) 
        => other is not null && ValuesAreEqual(other);

    public override bool Equals(object? obj)
        => obj is ValueObject other && ValuesAreEqual(other);

    private bool ValuesAreEqual(ValueObject other)
    {
        return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
    }

}

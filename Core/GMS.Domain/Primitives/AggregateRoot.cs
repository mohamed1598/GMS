using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.Primitives;

public abstract class AggregateRoot<TId> : Entity<TId> where TId : ValueObject
{
    private readonly List<IDomainEvent> _domainEvents = new();
    protected AggregateRoot(TId id) : base(id)
    {
    }

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}

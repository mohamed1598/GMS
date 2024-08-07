﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.Primitives;
public abstract class Entity<TId> :IEquatable<Entity<TId>> where TId: ValueObject
{
    protected Entity(TId id)
    {
        Id = id;
    }

    public TId Id { get; private init; }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
        => left is not null && right is not null && left.Equals(right);

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
        => !(left == right);

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if(obj.GetType() != GetType()) return false;

        if(obj is not Entity<TId> entity) return false;

        return entity.Id == Id;
    }

    public bool Equals(Entity<TId>? other)
    {
        if(other is null) return false;

        if(other.GetType() != GetType()) return false;

        return other.Id == Id;  
    }

    public override int GetHashCode() => Id.GetHashCode() * 41;
}

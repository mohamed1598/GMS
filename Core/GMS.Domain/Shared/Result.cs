﻿using GMS.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Domain.Shared;

public class Result<TValue> : Result
{
    private readonly TValue? _value;
    protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
    => _value = value;

    public TValue Value =>
        IsSuccess ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public static implicit operator Result<TValue>(TValue value) => Create<TValue>(value);

}
public class Result
{
    private Entity<ValueObject> _value;
    protected internal Result(bool isSuccess, Error error) {
        if (isSuccess && error != Error.None) throw new InvalidOperationException();
        if (!isSuccess && error == Error.None) throw new InvalidOperationException();
        IsSuccess = isSuccess;
        Error = error;
    }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; }

    public static Result Success() => new(true, Error.None);

    public static Result<TValue> Success<TValue>(TValue value)
        => new(value,true,Error.None);

    internal static Result<TValue> Failure<TValue>(Error error) where TValue:class
        => new(null,false, error);

    protected static Result<TValue> Create<TValue>(TValue value)
        => Success(value);
}

namespace Shared.Common.Results;

public class Result
{
    //Properties
    public Error Error { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    //Construction
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }
    public static Result<TValue> Create<TValue>(TValue value, Error error)where TValue : class
    {
        return value is null ? Failure<TValue>(error) : Success(value);
    }

    //Success methods
    public static Result Success() => new(true, Error.None);
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    //Failure methods
    public static Result Failure(Error error) => new(false, error);
    public static Result Failure(Result previousResult) => previousResult;
    public static Result<TValue> Failure<TValue>(Error error) => new(default!, false, error);
}

//Type specific Result
public class Result<TValue> : Result
{
    //Properties
    private readonly TValue _value;
    public TValue Value => IsSuccess ? 
        _value : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    //Construction
    protected internal Result(TValue value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    //Operators
    public static implicit operator Result<TValue>(TValue value) => Success(value);
    
}
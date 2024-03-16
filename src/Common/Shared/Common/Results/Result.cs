namespace Shared.Common.Results;

public class Result(bool success, Error error)
{
    //Properties
    public Error? Error { get; set; } = error;
    public bool Successs { get; } = success;
    public bool Failure { get => Successs; }

    //Success methods
    public static Result OK()
    {
        return new Result(true, Error.NoError);
    }
    public static Result OK<T>(T value)
    {
        return new Result<T>(value, true, Error.NoError);
    }

    //Failure methods
    public static Result Fail(Error error)
    {
        return new Result(false, error);
    }

    public static Result Fail<T>(T value, Error error)
    {
        return new Result<T>(value, false, error);
    }
}

//Type specific Result
public class Result<T>(T value, bool success, Error error) : Result(success, error)
{
    public T Value { get; } = value;
}
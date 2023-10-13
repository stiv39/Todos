namespace Todos.Domain.Shared;

public class Result<TValue>
{
    public bool IsSuccess { get; }
    public TValue Value { get; }
    public Error Error { get; }

    private Result(bool isSuccess, TValue value, Error error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<TValue> Success(TValue value)
    {
        return new Result<TValue>(true, value, Error.None);
    }

    public static Result<TValue> Failure(Error error)
    {
        return new Result<TValue>(false, default, error);
    }
}

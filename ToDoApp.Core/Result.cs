namespace ToDoApp.Core;

public class Result<T>
{
    public bool IsSuccess { get; set; }
    public string Error { get; set; }
    private readonly T? _value;

    private Result(T? value, bool isSuccess, string error)
    {
        _value = value;
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result<T> Success(T value)
        => new Result<T>(value, true, string.Empty);

    public static Result<T> Failure(string error)
        => new Result<T>(default, false, error);

    public T Value => IsSuccess ? _value! : throw new InvalidOperationException("No value present");
}
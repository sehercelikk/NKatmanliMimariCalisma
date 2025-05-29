namespace BookStore.Core.Utilities.Result.Abstract;

public interface IResult
{
    public ResultStatus ResultStatus { get; }
    public string Mesaj { get; }
    public Exception Exception { get; }
}

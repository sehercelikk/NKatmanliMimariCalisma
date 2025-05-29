using BookStore.Core.Utilities.Result.Abstract;

namespace BookStore.Core.Utilities.Result.Concrete;

public class Result : IResult
{
    
    public Result(ResultStatus resultStatus, string mesaj, Exception exception)
    {
        ResultStatus = resultStatus;
        Mesaj = mesaj;
        Exception = exception;
    }



    public ResultStatus ResultStatus { get; }
    public string Mesaj { get; set; }
    public Exception Exception { get; set; }
}



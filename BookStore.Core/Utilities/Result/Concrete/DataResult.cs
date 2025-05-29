using BookStore.Core.Utilities.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Utilities.Result.Concrete;

public class DataResult<T> : IDataResult<T>
{
    public ResultStatus ResultStatus { get; }
    public string Mesaj { get; }
    public T Data { get; }
    public Exception Exception { get; }



    public DataResult(ResultStatus resultStatus, string mesaj, T? data, Exception? exception)
    {
        ResultStatus = resultStatus;
        Mesaj = mesaj;
        Data = data;
        Exception = exception;
    }

}
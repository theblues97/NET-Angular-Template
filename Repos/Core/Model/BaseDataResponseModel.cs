using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public enum Status
    {
        Warning,
        Success,
        Error,
    }
    public class BaseResponseModel
    {
        public Status Status { get; set; }
        public string? Message { get; set; }
    }

    public class BaseDataResponseModel<T> : BaseResponseModel where T : class
    {
        public T? Data { get; set; }
    }

    public class SuccessResponse : BaseResponseModel
    {
        public SuccessResponse()
        {
            Status = Status.Success;
        }
    }

    public class SuccessDataResponse<T> : BaseDataResponseModel<T> where T : class
    {
        public SuccessDataResponse(T? data)
        {
            Status = Status.Success;
            Data = data;
        }
    }

    public class ErrorResponse : BaseResponseModel
    {
        public ErrorResponse(string errMsg)
        {
            Status = Status.Error;
            Message = errMsg;
        }
    }
}

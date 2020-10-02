using System;
using System.Collections.Generic;
using System.Text;

namespace Movies.Abstractions.Errors
{
    public class CommonException : Exception
    {
        public ErrorCode ErrorCode { get; private set; }
        public CommonException(ErrorCode errorCode, string message = ""): base(message)
        {
            ErrorCode = errorCode;
        }
    }
}

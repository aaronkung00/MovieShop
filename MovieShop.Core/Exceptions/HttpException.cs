using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MovieShop.Core.Exceptions
{
    public class HttpException : Exception
    {


        public HttpException(HttpStatusCode code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }
        public object Errors { get; set; }

        public HttpStatusCode Code { get; }
    }
}

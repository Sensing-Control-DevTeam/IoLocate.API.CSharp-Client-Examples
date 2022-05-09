using System;
using System.Collections.Generic;
using System.Linq;

namespace IoLocate.Api.Client.ConsoleApp.Models.Responses
{
    public class Response<T> : Response
    {
        public T Data { get; set; }
    }

    public class Response
    {
        public Response()
        {
            Errors = new List<Exception>();
        }

        public bool IsSuccess => !Errors.Any();
        public List<Exception> Errors { get; set; }
    }
}

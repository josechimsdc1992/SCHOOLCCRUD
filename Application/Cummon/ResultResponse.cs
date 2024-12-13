using Application.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cummon
{
    public class ResultResponse<T> : IResultResponse<T>
    {
        public bool HasError { get; set; }
        public string Mensaje { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T Result { get; set; }

        public void SetError(string mensaje)
        {
            StatusCode = HttpStatusCode.InternalServerError;
            HasError = false;
            Mensaje = mensaje;
            Result = default(T);
        }

        public void SetSucesss(T result, string message = "OK")
        {
            StatusCode = HttpStatusCode.OK;
            HasError = false;
            Mensaje = message;
            Result = result;
        }
    }
}

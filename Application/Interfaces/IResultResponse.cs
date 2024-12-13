using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IResultResponse<T>
    {
        bool HasError { get; set; }
        string Mensaje { get; set; }
        HttpStatusCode StatusCode { get; set; }
        T Result { get; set; }

        void SetError(string mensaje);
        void SetSucesss(T result, string message ="OK");
    }
}

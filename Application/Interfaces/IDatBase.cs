using Application.Cummon;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDatBase<T>
    {
        Task<ResultResponse<T>> DSave(T newItem);

        Task<ResultResponse<List<T>>> DSave(List<T> rangeItems);

        Task<ResultResponse<List<T>>> DGet();

        Task<ResultResponse<T>> DGet(int iKey);

        Task<ResultResponse<bool>> DUpdate(T entity);

        Task<ResultResponse<bool>> DDelete(int iKey);
    }
}

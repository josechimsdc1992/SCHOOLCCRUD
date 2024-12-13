using Application.Cummon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface IBusBase<TRead,TCreate,TUpdate>
    {
        Task<ResultResponse<TRead>> BUpdate(TUpdate updateModel);

        Task<ResultResponse<TRead>> BCreate(TCreate createModel);

        Task<ResultResponse<bool>> BDelete(int iKey);

        Task<ResultResponse<List<TRead>>> BGetAll();

        Task<ResultResponse<TRead>> BGet(int iKey);
    }
}

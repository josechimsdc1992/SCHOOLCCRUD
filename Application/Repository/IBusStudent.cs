using Application.Entities.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface IBusStudent : IBusBase<EntStudent, CUStudent, CUStudent>
    {

    }
}

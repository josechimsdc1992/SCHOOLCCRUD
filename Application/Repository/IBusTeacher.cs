using Application.Entities.Student;
using Application.Entities.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface IBusTeacher : IBusBase<EntTeacher, CUTeacher, CUTeacher>
    {

    }
}

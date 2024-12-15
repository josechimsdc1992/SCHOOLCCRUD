﻿using Application.Cummon;
using Application.Interfaces;

using Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfacess
{
    public interface IDatGrade:IDatBase<Grade>
    {
        public Task<ResultResponse<bool>> isUsedTeacher(int idTeacher); 
    }
}

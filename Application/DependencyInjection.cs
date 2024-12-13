﻿using Application.Bussiness;
using Application.Repository;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IBusGrade, BusGrade>();
        services.AddScoped<IBusStudent, BusStudent>();
        services.AddScoped<IBusTeacher, BusTeacher>();

        return services;
    }
}
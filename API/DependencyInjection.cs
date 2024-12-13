using Infrastructure.Repository;

using Interfaces;

using Interfacess;

using Microsoft.AspNetCore.Mvc;

using SchoolService.Infrastructure.Repository;

using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {

        services.AddScoped<IDatStudent, DatStudent>();
        services.AddScoped<IDatStudentGrade, DatStudentGrade>();
        services.AddScoped<IDatTeacher, DatTeacher>();
        services.AddScoped<IDatGrade, DatGrade>();

        services.AddHttpContextAccessor();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();

        return services;
    }
}


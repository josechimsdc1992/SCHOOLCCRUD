using Application.Bussiness;
using Application.Entities.Grade;
using Application.Entities.Student;
using Application.Entities.Teacher;
using Application.Repository;

using AutoMapper;

using Infrastructure.Repository;

using Interfaces;

using Interfacess;

using Microsoft.AspNetCore.Mvc;

using SchoolService.Infrastructure.Repository;

using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        var mapperConfig = new MapperConfiguration(m =>
        {
            m.AddProfile(new CUGrade.Mapping());
            m.AddProfile(new EntGrade.Mapping());
            m.AddProfile(new CUGradeStudent.Mapping());
            m.AddProfile(new EntGradeStudent.Mapping());


            m.AddProfile(new CUTeacher.Mapping());
            m.AddProfile(new EntTeacher.Mapping());

            m.AddProfile(new CUStudent.Mapping());
            m.AddProfile(new EntStudent.Mapping());

        });
        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddScoped<IDatStudent, DatStudent>();
        services.AddScoped<IDatStudentGrade, DatStudentGrade>();
        services.AddScoped<IDatTeacher, DatTeacher>();
        services.AddScoped<IDatGrade, DatGrade>();

        services.AddScoped<IBusGrade, BusGrade>();
        services.AddScoped<IBusStudent, BusStudent>();
        services.AddScoped<IBusTeacher, BusTeacher>();



        services.AddHttpContextAccessor();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddEndpointsApiExplorer();

        return services;
    }
}


﻿using Api.Domain.Interfaces.Services.User;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection service)
        {
            service.AddTransient<IUserService, UserService>();
        }
    }
}
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public static class ConfigureRepository
    {
        public static void ConfigureDependenciesService(IServiceCollection service)
        {
            service.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            service.AddScoped<IUserRepository, UserImplementation>();

            service.AddDbContext<DataContext>(
                    opt => opt.UseSqlServer(
                        "Server=localhost\\SQLEXPRESS;Database=dbAPI; Trusted_connection = true")
                    );
        }
    }
}

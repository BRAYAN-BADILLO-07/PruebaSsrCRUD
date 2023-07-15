using Backend.Domain.Interfaces;
using Backend.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Repository
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ISellerRepository, SellerRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("ConnectionDefault")));

            return services;
        }
    }
}

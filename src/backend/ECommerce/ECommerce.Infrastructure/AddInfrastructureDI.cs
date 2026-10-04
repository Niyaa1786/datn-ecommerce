using ECommerce.Application.Common.Interfaces;
using ECommerce.Infrastructure.Persistence.Data;
using ECommerce.Infrastructure.Persistence.Repositories;
using ECommerce.Infrastructure.Security;
using ECommerce.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure
{
    public static class AddInfrastructureDI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
            services.AddScoped<IFileUploadService, CloudinaryService>();

            return services;
        }
    }
}

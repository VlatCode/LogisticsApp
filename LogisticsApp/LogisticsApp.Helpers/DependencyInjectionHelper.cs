using LogisticsApp.DataAccess;
using LogisticsApp.DataAccess.Implementations;
using LogisticsApp.Domain.Models;
using LogisticsApp.Services.Implementations;
using LogisticsApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services)
        {
            services.AddDbContext<LogisticsAppDbContext>(x =>
            x.UseSqlServer("Server=.\\SQLExpress;Database=LogisticsAppDb;TrustServerCertificate=True;Trusted_Connection=True"));
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Courier>, CourierRepository>();
            services.AddTransient<IRepository<Validation>, ValidationRepository>();
            services.AddTransient<IRepository<Calculation>, CalculationRepository>();
            services.AddTransient<IRepository<Package>, PackageRepository>();
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<ICourierService, CourierService>();
            services.AddTransient<IValidationService, ValidationService>();
            services.AddTransient<ICalculationService, CalculationService>();
        }
    }
}

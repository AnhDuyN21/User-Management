using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructures.Repositories.Accounts;
using Infrastructures.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructures.Mappers;

namespace Infrastructures
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, string databaseConnection)
        {
            //UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Account
            services.AddScoped<IAccountRepo, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();

            //Authentication
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            //Role
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddSingleton<ICurrentTime, CurrentTime>();


            services.AddDbContext<UserManagementDbContext>(options =>
            {
                options.UseSqlServer(databaseConnection);
            });
            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);

            return services;
        }
    }
}

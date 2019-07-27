using DLayer;
using DLayer.Entities;
using DLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLayer.Infastructure
{
    public static class UoWServiceModule
    { 
        public static void AddUoWService(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}

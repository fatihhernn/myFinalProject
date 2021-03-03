using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    //extension yazabilmek için metodun statik olması gerekiyor

    //bütün injectionları bir araya toplayabileceğimiz bir yapıya dönüştü
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection servicesCollections,ICoreModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(servicesCollections);
            }
            return ServiceTool.Create(servicesCollections);
        }
    }
}

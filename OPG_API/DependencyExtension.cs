using Core.Base;
using Core.Game_dir;
using Core.Map_Handling.Managers;
using Primitives;
using System.Numerics;

namespace OPG_API
{
    public static class DependencyExtensions
    {
        /// <summary>
        /// Adds all the default <see cref="ServiceDescriptor"/>s necessary for running a <see cref="IPlant"/>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDefaultDependencies(this IServiceCollection services)
        {
            services.AddSingleton<Game>();
            services.AddSingleton<ActionManager>();
            services.AddSingleton<OggettoManager>();
            services.AddSingleton<InventarioManager>();


            services.AddUnitOfWork();

            return services;
        }

    

    }
}



using Core.Base;
using Core.Game;
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
            services.AddSingleton<DataAccess>();
            services.AddSingleton<Game>();

            //services.AddSingleton<IManageablePlant, PlantManager>();
            
            //services.AddSingleton<IItemFactory, ItemFactory>();
            
            //services.AddSingleton<IPathFinder, SingleAgentPathFinder>();

            //services.AddSingleton<IFactory<ILoadingUnitMissionBuilder>, LoadingUnitMissionBuilderFactory>();

            //services.AddSingleton<IPlantState, PlantState>();

            //services.AddSingleton<MissionManager>();
            
            services.AddUnitOfWork();

            return services;
        }

    

    }
}



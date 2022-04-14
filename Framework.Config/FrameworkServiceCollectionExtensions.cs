using Framework.Application;
using Framework.Core;
using Framework.Core.Queries;
using Framework.Persistence.EF;
using Microsoft.Extensions.DependencyInjection;
using Framework.Snowflake;
using Framework.Core.Expressions;
using Framework.NCalcExperssions;

namespace Framework.Config
{
    public static class FrameworkServiceCollectionExtensions
    {
        public static IServiceCollection AddFrameworkServices(this IServiceCollection services)
        {
            services.AddScoped<IEventBus, EventAggregator>();
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IIdGenerator, SnowflakeIdGenerator>();
            services.AddScoped<IOperationOptionsService, OperationOptionsService>();
            services.AddTransient<IOutboxMessageDetector, OutboxMessageDetector>();
            services.AddTransient<IExperssionEvaluator, NCalcExperssionEvaluator>();
            return services;
        }
    }
}

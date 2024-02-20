namespace Shared.Jobs.Extensions
{
    using Shared.Jobs.Configs;
    using Shared.Jobs.Contracts;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    /// <summary>
    /// Defines the <see cref="ScheduledServiceExtensions" />.
    /// </summary>
    public static class ScheduledServiceExtensions
    {
        /// <summary>
        /// The AddCronJob.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <param name="options">The options<see cref="Action{IScheduleConfig{T}}"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddCronJob<T>(this IServiceCollection services, Action<IScheduleConfig<T>> options) where T : CronJobService
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), @"Please provide Schedule Configurations.");
            }
            var config = new ScheduleConfig<T>();
            options.Invoke(config);
            if (string.IsNullOrWhiteSpace(config.CronExpression))
            {
                throw new ArgumentNullException(nameof(ScheduleConfig<T>.CronExpression), @"Empty Cron Expression is not allowed.");
            }

            services.AddSingleton<IScheduleConfig<T>>(config);
            services.AddHostedService<T>();
            return services;
        }
    }
}
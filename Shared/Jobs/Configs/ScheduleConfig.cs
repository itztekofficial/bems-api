namespace Shared.Jobs.Configs
{
    using Shared.Jobs.Contracts;
    using System;

    /// <summary>
    /// Defines the <see cref="ScheduleConfig{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class ScheduleConfig<T> : IScheduleConfig<T>
    {
        /// <summary>
        /// Gets or sets the CronExpression.
        /// </summary>
        public string CronExpression { get; set; }

        /// <summary>
        /// Gets or sets the TimeZoneInfo.
        /// </summary>
        public TimeZoneInfo TimeZoneInfo { get; set; }
    }
}
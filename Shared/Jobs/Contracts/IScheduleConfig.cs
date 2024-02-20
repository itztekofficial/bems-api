namespace Shared.Jobs.Contracts
{
    using System;

    /// <summary>
    /// Defines the <see cref="IScheduleConfig{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public interface IScheduleConfig<T>
    {
        /// <summary>
        /// Gets or sets the CronExpression.
        /// </summary>
        string CronExpression { get; set; }

        /// <summary>
        /// Gets or sets the TimeZoneInfo.
        /// </summary>
        TimeZoneInfo TimeZoneInfo { get; set; }
    }
}
namespace Shared.Jobs
{
    using Cronos;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CronJobService" />.
    /// </summary>
    public abstract class CronJobService : IHostedService, IDisposable
    {
        /// <summary>
        /// Defines the _timer.
        /// </summary>
        private System.Timers.Timer _timer;

        /// <summary>
        /// Defines the _expression.
        /// </summary>
        private readonly CronExpression _expression;

        /// <summary>
        /// Defines the _timeZoneInfo.
        /// </summary>
        private readonly TimeZoneInfo _timeZoneInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CronJobService"/> class.
        /// </summary>
        /// <param name="cronExpression">The cronExpression<see cref="string"/>.</param>
        /// <param name="timeZoneInfo">The timeZoneInfo<see cref="TimeZoneInfo"/>.</param>
        protected CronJobService(string cronExpression, TimeZoneInfo timeZoneInfo)
        {
            _expression = CronExpression.Parse(cronExpression);
            _timeZoneInfo = timeZoneInfo;
        }

        /// <summary>
        /// The StartAsync.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            await ScheduleJob(cancellationToken);
        }

        /// <summary>
        /// The ScheduleJob.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            try
            {
                var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
                Log.Information("Next Run:" + next.ToString());
                if (next.HasValue)
                {
                    var delay = next.Value - DateTimeOffset.Now;
                    if (delay.TotalMilliseconds <= 0)   // prevent non-positive values from being passed into Timer
                    {
                        await ScheduleJob(cancellationToken);
                    }
                    double v = Math.Abs(delay.TotalMilliseconds);
                    v = v <= 0 ? 5000 : v;
                    _timer = new System.Timers.Timer(v);
                    _timer.Elapsed += async (sender, args) =>
                    {
                        _timer.Dispose();  // reset and dispose timer
                        _timer = null;

                        if (!cancellationToken.IsCancellationRequested)
                        {
                            await DoWork(cancellationToken);
                        }

                        if (!cancellationToken.IsCancellationRequested)
                        {
                            await ScheduleJob(cancellationToken);    // reschedule next
                        }
                    };
                    _timer.Start();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "ScheduleJob");
            }
            await Task.CompletedTask;
        }

        /// <summary>
        /// The DoWork.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public virtual async Task DoWork(CancellationToken cancellationToken)
        {
            await Task.Delay(5000, cancellationToken);  // do the work
        }

        /// <summary>
        /// The StopAsync.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            await Task.CompletedTask;
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        public virtual void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
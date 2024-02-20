using Shared.Repository.Contracts;
using Microsoft.Extensions.Logging;
using Shared.Jobs.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Jobs
{
    public class DownloadDataJob : CronJobService
    {
        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<DownloadDataJob> _logger;

        /// <summary>
        /// Defines the _downloadDataRepository.
        /// </summary>
        private readonly IDownloadDataRepository _downloadDataRepository;

        /// <summary>
        /// Initializes a new instance of the DownloadDataJob
        /// </summary>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        /// <param name="uploadDataRepository"></param>
        public DownloadDataJob(IScheduleConfig<UploadDataJob> config, ILogger<DownloadDataJob> logger, IDownloadDataRepository downloadDataRepository) : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _downloadDataRepository = downloadDataRepository;
        }

        /// <summary>
        /// The StartAsync.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DownloadDataJob starts.");
            return base.StartAsync(cancellationToken);
        }

        /// <summary>
        /// The DoWork.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public override Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} DownloadDataJob.call Update/insert required data");
            _downloadDataRepository.DownloadDataJob();
            return Task.CompletedTask;
        }

        /// <summary>
        /// The StopAsync.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("DownloadDataJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
using Shared.Repository.Contracts;
using Microsoft.Extensions.Logging;
using Shared.Jobs.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Jobs
{
    public class UploadDataJob : CronJobService
    {
        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<UploadDataJob> _logger;

        /// <summary>
        /// Defines the _uploadDataRepository.
        /// </summary>
        private readonly IUploadDataRepository _uploadDataRepository;

        /// <summary>
        /// Initializes a new instance of the UploadDataJob
        /// </summary>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        /// <param name="uploadDataRepository"></param>
        public UploadDataJob(IScheduleConfig<UploadDataJob> config, ILogger<UploadDataJob> logger, IUploadDataRepository uploadDataRepository) : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _uploadDataRepository = uploadDataRepository;
        }

        /// <summary>
        /// The StartAsync.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("UploadDataJob starts.");
            return base.StartAsync(cancellationToken);
        }

        /// <summary>
        /// The DoWork.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public override Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} UploadDataJob.call Update/insert required data");
            _uploadDataRepository.UploadDataJob();
            return Task.CompletedTask;
        }

        /// <summary>
        /// The StopAsync.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("UploadDataJob is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
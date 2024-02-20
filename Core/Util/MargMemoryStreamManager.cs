using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System.IO;

namespace Core.Util
{
    public interface IMemoryStreamManager
    {
        public MemoryStream GetStream();

        public MemoryStream GetStream(byte[] buffer);
    }

    public class MemoryStreamManager : IMemoryStreamManager
    {
        private readonly ILogger<MemoryStreamManager> _logger;

        public MemoryStreamManager(ILogger<MemoryStreamManager> logger)
        {
            _logger = logger;
        }

        public MemoryStream GetStream()
        {
            int blockSize = 1024;
            int largeBufferMultiple = 1024 * 1024;
            int maximumBufferSize = 16 * largeBufferMultiple;
            int maximumFreeLargePoolBytes = maximumBufferSize * 4;
            int maximumFreeSmallPoolBytes = 250 * blockSize;
            RecyclableMemoryStreamManager recyclableMemoryStreamManager = new RecyclableMemoryStreamManager(blockSize, largeBufferMultiple, maximumBufferSize);
            recyclableMemoryStreamManager.AggressiveBufferReturn = true;
            recyclableMemoryStreamManager.GenerateCallStacks = true;
            recyclableMemoryStreamManager.MaximumFreeLargePoolBytes = maximumFreeLargePoolBytes;
            recyclableMemoryStreamManager.MaximumFreeSmallPoolBytes = maximumFreeSmallPoolBytes;
            return recyclableMemoryStreamManager.GetStream();
        }

        public MemoryStream GetStream(byte[] buffer)
        {
            int blockSize = 1024;
            int largeBufferMultiple = 1024 * 1024;
            int maximumBufferSize = 16 * largeBufferMultiple;
            int maximumFreeLargePoolBytes = maximumBufferSize * 4;
            int maximumFreeSmallPoolBytes = 250 * blockSize;
            RecyclableMemoryStreamManager recyclableMemoryStreamManager = new RecyclableMemoryStreamManager(blockSize, largeBufferMultiple, maximumBufferSize);
            recyclableMemoryStreamManager.AggressiveBufferReturn = true;
            recyclableMemoryStreamManager.GenerateCallStacks = true;
            recyclableMemoryStreamManager.MaximumFreeLargePoolBytes = maximumFreeLargePoolBytes;
            recyclableMemoryStreamManager.MaximumFreeSmallPoolBytes = maximumFreeSmallPoolBytes;
            return recyclableMemoryStreamManager.GetStream(buffer);
        }
    }
}
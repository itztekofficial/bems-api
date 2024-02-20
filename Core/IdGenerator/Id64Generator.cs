using System;
using System.Collections.Generic;
using System.Threading;

namespace Core.Util
{
    /// <summary>
    /// The id64 generator.
    /// </summary>
    public class Id64Generator : IIdGenerator<long>
    {
        #region Private Constant

        /// <summary>
        /// 1 January 1970. Used to calculate timestamp (in milliseconds)
        /// </summary>
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static readonly DateTime Jan1st2010 = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private const long Epoch = 1351606710465L;

        /// <summary>
        /// Number of bits allocated for a worker id in the generated identifier. 5 bits indicates values from 0 to 31
        /// </summary>
        private const int WorkerIdBits = 5;

        /// <summary>
        /// Datacenter identifier this worker belongs to. 5 bits indicates values from 0 to 31
        /// </summary>
        private const int DatacenterIdBits = 5;

        /// <summary>
        /// Generator identifier. 10 bits indicates values from 0 to 1023
        /// </summary>
        private const int GeneratorIdBits = 10;

        /// <summary>
        /// Maximum generator identifier
        /// </summary>
        private const long MaxGeneratorId = -1 ^ (-1L << GeneratorIdBits);

        /// <summary>
        /// Maximum worker identifier
        /// </summary>
        private const long MaxWorkerId = -1L ^ (-1L << WorkerIdBits);

        /// <summary>
        /// Maximum datacenter identifier
        /// </summary>
        private const long MaxDatacenterId = -1L ^ (-1L << DatacenterIdBits);

        /// <summary>
        /// Number of bits allocated for sequence in the generated identifier
        /// </summary>
        private const int SequenceBits = 12;

        private const int WorkerIdShift = SequenceBits;

        private const int DatacenterIdShift = SequenceBits + WorkerIdBits;

        private const int TimestampLeftShift = SequenceBits + WorkerIdBits + DatacenterIdBits;

        private const long SequenceMask = -1L ^ (-1L << SequenceBits);

        #endregion Private Constant

        #region Private Fields

        /// <summary>
        /// Object used as a monitor for threads synchronization.
        /// </summary>
        private readonly object monitor = new object();

        /// <summary>
        /// The timestamp used to generate last id by the worker
        /// </summary>
        private long lastTimestamp = -1L;

        private long sequence = 0;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Indicates how many times the given generator had to wait
        /// for next millisecond <see cref="TillNextMillis"/> since startup.
        /// </summary>
        public int NextMillisecondWait { get; set; }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Id64Generator"/> class.
        /// </summary>
        /// <param name="generatorId">The generator id.</param>
        /// <param name="sequence">The sequence.</param>
        public Id64Generator(int generatorId = 0, int sequence = 0)
            : this(
                (int)(generatorId & MaxWorkerId),
                (int)(generatorId >> WorkerIdBits & MaxDatacenterId),
                sequence)
        {
            // sanity check for generatorId
            if (generatorId > MaxGeneratorId || generatorId < 0)
            {
                throw new InvalidOperationException(
                    string.Format("Generator Id can't be greater than {0} or less than 0", MaxGeneratorId));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Id64Generator"/> class.
        /// </summary>
        /// <param name="workerId">The worker id.</param>
        /// <param name="datacenterId">The datacenter id.</param>
        /// <param name="sequence">The sequence.</param>
        public Id64Generator(int workerId, int datacenterId, int sequence = 0)
        {
            // sanity check for workerId
            if (workerId > MaxWorkerId || workerId < 0)
            {
                throw new InvalidOperationException(
                    string.Format("Worker Id can't be greater than {0} or less than 0", MaxWorkerId));
            }

            // sanity check for datacenterId
            if (datacenterId > MaxDatacenterId || datacenterId < 0)
            {
                throw new InvalidOperationException(
                    string.Format("Datacenter Id can't be greater than {0} or less than 0", MaxDatacenterId));
            }

            WorkerId = workerId;
            DatacenterId = datacenterId;
            this.sequence = sequence;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// The identifier of the worker
        /// </summary>
        public long WorkerId { get; private set; }

        /// <summary>
        /// Identifier of datacenter the worker belongs to
        /// </summary>
        public long DatacenterId { get; private set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Generates the id.
        /// </summary>
        /// <returns>A long.</returns>
        public long GenerateId()
        {
            lock (monitor)
            {
                return NextId();
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>An IEnumerator.</returns>
        public IEnumerator<long> GetEnumerator()
        {
            while (true)
            {
                yield return GenerateId();
            }
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>A System.Collections.IEnumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion Public Methods

        #region Private Properties

        /// <summary>
        /// Gets the current time.
        /// </summary>
        public static long CurrentTime
        {
            get { return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds; }
        }

        /// <summary>
        /// Gets the temp i d.
        /// </summary>
        /// <returns>A long.</returns>
        public static long GetTempID()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        private static readonly object objLock = new object();
        private static long _prvgetID;

        /// <summary>
        /// Gets the get i d.
        /// </summary>
        public static long GetID
        {
            get
            {
                int count = 1;
                long _getID;
                lock (objLock)
                {
                    do
                    {
                        Thread.Sleep(1);
                        _getID = (long)(DateTime.UtcNow - Jan1st2010).TotalMilliseconds;
                        if (_prvgetID == _getID)
                        {
                            Thread.Sleep(1);
                        }
                        count++;
                        if (count >= 20)
                        {
                            break;
                        }
                    } while (_prvgetID == _getID);
                    _prvgetID = _getID;
                    return _getID;
                }
            }
        }

        #endregion Private Properties

        #region Private Methods

        /// <summary>
        /// Tills the next millis.
        /// </summary>
        /// <param name="lastTimestamp">The last timestamp.</param>
        /// <returns>A long.</returns>
        private long TillNextMillis(long lastTimestamp)
        {
            NextMillisecondWait++;

            var timestamp = CurrentTime;

            SpinWait.SpinUntil(() => (timestamp = CurrentTime) > lastTimestamp);

            return timestamp;
        }

        /// <summary>
        /// Nexts the id.
        /// </summary>
        /// <returns>A long.</returns>
        private long NextId()
        {
            var timestamp = CurrentTime;

            if (timestamp < lastTimestamp)
            {
                throw new InvalidOperationException(string.Format("Clock moved backwards. Refusing to generate id for {0} milliseconds", (lastTimestamp - timestamp)));
            }

            if (lastTimestamp == timestamp)
            {
                sequence = (sequence + 1) & SequenceMask;
                if (sequence == 0)
                {
                    timestamp = TillNextMillis(lastTimestamp);
                }
            }
            else
            {
                sequence = 0;
            }

            lastTimestamp = timestamp;
            return ((timestamp - Epoch) << TimestampLeftShift) |
                (DatacenterId << DatacenterIdShift) |
                (WorkerId << WorkerIdShift) |
                sequence;
        }

        #endregion Private Methods
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Util
{
    /// <summary>
    /// The id guid generator.
    /// </summary>
    public class IdGuidGenerator : IIdGenerator<Guid>
    {
        #region Private Constant

        private const int IdentifierMaxBytes = 6;

        private const int SequenceBits = 16;

        private const long SequenceBitMask = -1 ^ (-1 << SequenceBits);

        #endregion Private Constant

        #region Private Fields

        /// <summary>
        /// Object used as a monitor for threads synchronization.
        /// </summary>
        private readonly object monitor = new object();

        private readonly ulong epoch;

        private readonly byte[] identifier;

        private ulong lastTimestamp;

        private int sequence;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IdGuidGenerator"/> class.
        /// </summary>
        public IdGuidGenerator()
            : this(0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdGuidGenerator"/> class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        public IdGuidGenerator(long identifier)
            : this(BitConverter.GetBytes(identifier).Take(6).ToArray())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdGuidGenerator"/> class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="epoch">The epoch.</param>
        public IdGuidGenerator(long identifier, DateTime epoch)
            : this(BitConverter.GetBytes(identifier).Take(6).ToArray(), (ulong)epoch.Ticks / 10)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdGuidGenerator"/> class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        public IdGuidGenerator(byte[] identifier)
            : this(identifier, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdGuidGenerator"/> class.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="epoch">The epoch.</param>
        public IdGuidGenerator(byte[] identifier, DateTime epoch)
            : this(identifier, (ulong)epoch.Ticks / 10)
        {
        }

        #endregion Public Constructors

        #region Private Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="IdGuidGenerator"/> class from being created.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="epoch">The epoch.</param>
        private IdGuidGenerator(byte[] identifier, ulong epoch)
        {
            if (identifier.Length > 6)
                throw new ApplicationException("Identifier too long");

            this.identifier = identifier;
            this.epoch = epoch == 0 ? (ulong)new DateTime(1970, 1, 1).Ticks / 10 : epoch;
        }

        #endregion Private Constructors

        #region Public Methods

        /// <summary>
        /// Generates the id.
        /// </summary>
        /// <returns>A Guid.</returns>
        public Guid GenerateId()
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
        public IEnumerator<Guid> GetEnumerator()
        {
            while (true)
            {
                yield return GenerateId();
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>A System.Collections.IEnumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Handles the time.
        /// </summary>
        private void HandleTime()
        {
            var timestamp = CurrentTime;

            if (lastTimestamp < timestamp)
            {
                lastTimestamp = timestamp;
                sequence = 0;
            }
            else if (lastTimestamp > timestamp)
            {
                throw new ApplicationException("Clock is running backwards");
            }
            else
            {
                sequence++;
            }
        }

        /// <summary>
        /// Nexts the id.
        /// </summary>
        /// <returns>A Guid.</returns>
        private Guid NextId()
        {
            HandleTime();

            byte[] id = new byte[8];
            byte[] sequenceBytes = BitConverter.GetBytes(sequence & SequenceBitMask).Take(2).ToArray();

            Array.Copy(identifier, 0, id, 2, identifier.Length); // identifier - 48 bits
            Array.Copy(sequenceBytes, 0, id, 0, 2); // sequence - 16 bits

            if (BitConverter.IsLittleEndian)
                Array.Reverse(id);

            return new Guid((int)(lastTimestamp >> 32 & 0xFFFFFFFF),
                (short)(lastTimestamp >> 16 & 0xFFFF),
                (short)(lastTimestamp & 0xFFFF),
                id);
        }

        /// <summary>
        /// Gets the current time.
        /// </summary>
        private ulong CurrentTime
        {
            get { return ((ulong)DateTime.UtcNow.Ticks / 10) - epoch; }
        }

        #endregion Private Methods
    }
}
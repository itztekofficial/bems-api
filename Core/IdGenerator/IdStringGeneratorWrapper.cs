using System;
using System.Collections.Generic;

namespace Core.Util
{
    /// <summary>
    /// The id string generator wrapper.
    /// </summary>
    public class IdStringGeneratorWrapper : IIdGenerator<string>
    {
        #region Predefined Converters

        /// <summary>
        /// Default converter calling <code>ToString()</code> on generated id
        /// </summary>
        public static readonly Func<long, string> Default =
            (id) => id.ToString();

        /// <summary>
        /// Converter calling <code>ToString()</code> with formating "X" on generated id
        /// </summary>
        public static readonly Func<long, string> UpperHex =
            (id) => id.ToString("X");

        /// <summary>
        /// Converter calling <code>ToString()</code> with formating "x" on generated id
        /// </summary>
        public static readonly Func<long, string> LowerHex =
            (id) => id.ToString("x");

        public static readonly Func<long, string> Base32 =
            (id) => Encoder.Encode32(id);

        public static readonly Func<long, string> Base32LeadingZero =
            (id) => Encoder.Encode32(id, true);

        #endregion Predefined Converters

        #region Private Fields

        private readonly IIdGenerator<long> baseGenerator;

        private readonly Func<long, string> converter;

        private readonly string prefix;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IdStringGeneratorWrapper"/> class.
        /// </summary>
        /// <param name="baseGenerator">The base generator.</param>
        /// <param name="prefix">The prefix.</param>
        public IdStringGeneratorWrapper(IIdGenerator<long> baseGenerator, string prefix = null)
            : this(baseGenerator, Default, prefix)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdStringGeneratorWrapper"/> class.
        /// </summary>
        /// <param name="baseGenerator">The base generator.</param>
        /// <param name="converter">The converter.</param>
        /// <param name="prefix">The prefix.</param>
        public IdStringGeneratorWrapper(
            IIdGenerator<long> baseGenerator, Func<long, string> converter, string prefix = null)
        {
            if (baseGenerator == null)
                throw new ArgumentException("base generator must be provided");

            if (converter == null)
                throw new ArgumentException("converter must be provided");

            this.baseGenerator = baseGenerator;
            this.converter = converter;
            this.prefix = string.IsNullOrEmpty(prefix) ? string.Empty : prefix;
        }

        #endregion Public Constructors

        /// <summary>
        /// Generates the id.
        /// </summary>
        /// <returns>A string.</returns>
        public string GenerateId()
        {
            return prefix + converter(baseGenerator.GenerateId());
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>An IEnumerator.</returns>
        public IEnumerator<string> GetEnumerator()
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
    }
}
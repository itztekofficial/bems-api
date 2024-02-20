using System.Linq;

namespace Core.Util
{
    /// <summary>
    /// The encoder.
    /// </summary>
    internal sealed class Encoder
    {
        private static readonly char[] EncodingChars = "0123456789ABCDEFGHIJKLMNOPQRSTUV".ToArray();

        /// <summary>
        /// Encode32S the.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="withLeadingZero">If true, with leading zero.</param>
        /// <returns>A string.</returns>
        internal static string Encode32(long number, bool withLeadingZero = false)
        {
            char[] output = new char[13];
            int index = 12;

            do
            {
                output[index--] = EncodingChars[number & 0x1F];
                number >>= 5;
            }
            while (index >= 0 && number != 0);

            var id = new string(output, index + 1, 12 - index);
            return withLeadingZero ? id.PadLeft(13, '0') : id;
        }
    }
}
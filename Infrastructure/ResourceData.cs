using System.Reflection;

namespace Repositories //Template
{
    /// <summary>
    /// The resource data.
    /// </summary>
    public static class ResourceData
    {
        //private static readonly object objLock = new object();
        /// <summary>
        /// Reads the fully.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>An array of byte.</returns>
        public static byte[]? ReadFully(Stream input)
        {
            if (input != null)
            {
                byte[] buffer = new byte[16 * 1024];
                using MemoryStream ms = new();
                int read; while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                { ms.Write(buffer, 0, read); }
                return ms.ToArray();
            }
            else
                return null;
        }

        private static readonly Dictionary<string, byte[]> dataTemplate = new Dictionary<string, byte[]>();

        /// <summary>
        /// Gets the template.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>An array of byte.</returns>
        public static byte[] GetTemplate(string fileName)
        {
            byte[]? rtbyte = null;
            if (dataTemplate.ContainsKey(fileName))
            {
                rtbyte = dataTemplate[fileName];
            }
            else
            {
                Assembly asmb = Assembly.GetExecutingAssembly();
                Stream strm = asmb.GetManifestResourceStream(asmb.GetName().Name + ".Resources." + ".Requests." + fileName);
                MemoryStream mm = new();
                strm.CopyTo(mm);
                rtbyte = mm.ToArray();
                strm.Close();
                strm.Dispose();
                dataTemplate.Add(fileName, rtbyte);
            }
            return rtbyte;
        }
    }
}
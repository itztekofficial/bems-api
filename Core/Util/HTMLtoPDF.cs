using iText.Html2pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Util
{
    public interface IHTMLtoPDF
    {
        public void SetConvertOptions(IConvertOptions convertOptions);

        public byte[] GeneratePDF(string data);

        public byte[] GeneratePDF(byte[] data);

        public byte[] GeneratePDFItext(string data);
    }

    public class HTMLtoPDF : IHTMLtoPDF
    {
        private IConvertOptions _convertOptions;
        private readonly IPdfGenerator _pdfGenerator1;
        private readonly IMemoryStreamManager _memoryStreamManager1;

        public HTMLtoPDF(IPdfGenerator pdfGenerator1, IMemoryStreamManager memoryStreamManager1)
        {
            _convertOptions = new ConvertOptions();
            _pdfGenerator1 = pdfGenerator1;
            _memoryStreamManager1 = memoryStreamManager1;
        }

        public void SetConvertOptions(IConvertOptions convertOptions)
        {
            _convertOptions = convertOptions;
        }

        public byte[] GeneratePDF(string data)
        {
            var task = Task.Run(() => { return _pdfGenerator1.Generate(data, _convertOptions.GetConvertOptions()); });
            task.Wait();
            var rst = task.Result;
            return rst;
        }

        public byte[] GeneratePDF(byte[] data)
        {
            var task = Task.Run(() => { return _pdfGenerator1.Generate(data, _convertOptions.GetConvertOptions()); });
            task.Wait();
            var rst = task.Result;
            return rst;
        }

        public byte[] GeneratePDFItext(string data)
        {
            byte[] bData = null;
            using (MemoryStream stream = new MemoryStream())
            {
                ConverterProperties converterProperties = new ConverterProperties();
                HtmlConverter.ConvertToPdf(data, stream, converterProperties);
                bData = stream.ToArray();
            }
            return bData;
        }
    }

    public struct ExecutionResult
    {
        public bool Success;

        public int ExitCode;

        public byte[] Stdout;

        public byte[] Stderr;
    }

    public interface IWkHtmlToPdfBinary
    {
        Task<ExecutionResult> Execute(string stdin, string switches, TimeSpan timeout);

        Task<ExecutionResult> Execute(byte[] stdin, string switches, TimeSpan timeout);
    }

    public class PdfConvertException : Exception
    {
        public PdfConvertException(string message)
            : base(message)
        {
        }
    }

    public interface IPdfGenerator
    {
        public Task<byte[]> Generate(string data, string switches);

        public Task<byte[]> Generate(byte[] data, string switches);

        public Task<byte[]> Generate(string data, string switches, TimeSpan timeout);

        public Task<byte[]> Generate(byte[] data, string switches, TimeSpan timeout);
    }

    public class PdfGenerator : IPdfGenerator
    {
        private readonly IWkHtmlToPdfBinary _pdfBinary;

        public PdfGenerator(IWkHtmlToPdfBinary pdfBinary)
        {
            _pdfBinary = pdfBinary;
        }

        public Task<byte[]> Generate(string data, string switches)
        {
            return Generate(data, switches, TimeSpan.FromSeconds(30.0));
        }

        public Task<byte[]> Generate(byte[] data, string switches)
        {
            return Generate(data, switches, TimeSpan.FromSeconds(30.0));
        }

        public async Task<byte[]> Generate(string data, string switches, TimeSpan timeout)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new PdfConvertException("input string empty");
            }
            ExecutionResult executionResult = await _pdfBinary.Execute(data, switches, timeout);
            if (!executionResult.Success)
            {
                throw new PdfConvertException("call to executable failed");
            }
            if (executionResult.Stderr.Length != 0 && executionResult.ExitCode != 0)
            {
                throw new PdfConvertException($"{Encoding.UTF8.GetString(executionResult.Stderr)} ; exit code {executionResult.ExitCode}");
            }
            if (executionResult.Stdout.Length == 0)
            {
                throw new PdfConvertException("output empty");
            }
            return executionResult.Stdout;
        }

        public async Task<byte[]> Generate(byte[] data, string switches, TimeSpan timeout)
        {
            if (data == null)
            {
                throw new PdfConvertException("input string empty");
            }
            ExecutionResult executionResult = await _pdfBinary.Execute(data, switches, timeout);
            if (!executionResult.Success)
            {
                throw new PdfConvertException("call to executable failed");
            }
            if (executionResult.Stderr.Length != 0 && executionResult.ExitCode != 0)
            {
                throw new PdfConvertException($"{Encoding.UTF8.GetString(executionResult.Stderr)} ; exit code {executionResult.ExitCode}");
            }
            if (executionResult.Stdout.Length == 0)
            {
                throw new PdfConvertException("output empty");
            }
            return executionResult.Stdout;
        }
    }

    public class WkHtmlToPdfBinary : IWkHtmlToPdfBinary
    {
        private const int MAX_PATH = 255;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName(
            [MarshalAs(UnmanagedType.LPTStr)]
         string path,
            [MarshalAs(UnmanagedType.LPTStr)]
         StringBuilder shortPath,
            int shortPathLength
            );

        private readonly IMemoryStreamManager _memoryStreamManager;

        public WkHtmlToPdfBinary(IMemoryStreamManager memoryStreamManager)
        {
            _memoryStreamManager = memoryStreamManager;
        }

        private static string GetShortPath(string path)
        {
            var shortPath = new StringBuilder(MAX_PATH);
            GetShortPathName(path, shortPath, MAX_PATH);
            return shortPath.ToString();
        }

        public async Task<ExecutionResult> Execute(string stdin, string switches, TimeSpan timeout)
        {
            ExecutionResult result = default(ExecutionResult);
            var IsElectron = ConfigFile.appSettings.IsElectron;
            try
            {
                string path = string.Empty;
                string path1 = string.Empty;
                if (IsElectron == true)
                {
                    var processModule = Process.GetCurrentProcess().MainModule;
                    if (processModule != null)
                    {
                        var pathToExe = processModule.FileName;
                        path1 = Path.GetDirectoryName(pathToExe);
                    }
                }
                else
                {
                    path1 = AppDomain.CurrentDomain.BaseDirectory;
                }
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    path = Path.Combine(path1, "wkhtml", "Windows", "wkhtmltopdf.exe");
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    path = Path.Combine(path1, "wkhtml", "Linux", "wkhtmltopdf");
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    path = Path.Combine(path1, "wkhtml", "Mac", "wkhtmltopdf");
                }
                else
                {
                    path = Path.Combine(path1, "wkhtml", "Windows", "wkhtmltopdf.exe");
                }
                using (Process process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = path,
                        Arguments = "-q -n " + switches + " - -",
                        CreateNoWindow = true,
                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false
                    };

                    if (!process.Start())
                    {
                        result = default(ExecutionResult);
                        result.Success = false;
                        return result;
                    }
                    process.StandardInput.Write(stdin);
                    process.StandardInput.Close();
                    using MemoryStream stdout = _memoryStreamManager.GetStream();
                    using MemoryStream stderr = _memoryStreamManager.GetStream();
                    await process.StandardOutput.BaseStream.CopyToAsync(stdout);
                    await process.StandardError.BaseStream.CopyToAsync(stderr);
                    bool success = process.WaitForExit((int)timeout.TotalMilliseconds);
                    result = default;
                    result.ExitCode = process.ExitCode;
                    result.Stderr = stderr.GetBuffer();
                    result.Stdout = stdout.GetBuffer();
                    result.Success = success;
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog("HTMLToPDF", "Execute Wkhtml", ex.Message);
                return result;
            }
        }

        public async Task<ExecutionResult> Execute(byte[] stdin, string switches, TimeSpan timeout)
        {
            ExecutionResult result = default;
            var IsElectron = ConfigFile.appSettings.IsElectron;
            try
            {
                string path = string.Empty;
                string path1 = string.Empty;
                if (IsElectron == true)
                {
                    var processModule = Process.GetCurrentProcess().MainModule;
                    if (processModule != null)
                    {
                        var pathToExe = processModule.FileName;
                        path1 = Path.GetDirectoryName(pathToExe);
                    }
                }
                else
                {
                    path1 = AppDomain.CurrentDomain.BaseDirectory;
                }
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    path = Path.Combine(path1, "wkhtml", "Windows", "wkhtmltopdf.exe");
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    path = Path.Combine(path1, "wkhtml", "Linux", "wkhtmltopdf");
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    path = Path.Combine(path1, "wkhtml", "Mac", "wkhtmltopdf");
                }
                else
                {
                    path = Path.Combine(path1, "wkhtml", "Windows", "wkhtmltopdf.exe");
                }
                using (Process process = new())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = path,
                        Arguments = "-q -n " + switches + " - -",
                        CreateNoWindow = true,
                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false
                    };

                    if (!process.Start())
                    {
                        result = default;
                        result.Success = false;
                        return result;
                    }
                    process.StandardInput.Write(Encoding.UTF8.GetString(stdin));
                    process.StandardInput.Close();
                    using MemoryStream stdout = _memoryStreamManager.GetStream();
                    using MemoryStream stderr = _memoryStreamManager.GetStream();
                    await process.StandardOutput.BaseStream.CopyToAsync(stdout);
                    await process.StandardError.BaseStream.CopyToAsync(stderr);
                    bool success = process.WaitForExit((int)timeout.TotalMilliseconds);
                    result = default;
                    result.ExitCode = process.ExitCode;
                    result.Stderr = stderr.GetBuffer();
                    result.Stdout = stdout.GetBuffer();
                    result.Success = success;
                    return result;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog("HTMLToPDF", "Execute Wkhtml", ex.Message);
                return result;
            }
        }

        private string SpecialCharsEncode(string text)
        {
            char[] array = text.ToCharArray();
            StringBuilder stringBuilder = new(text.Length + (int)((double)text.Length * 0.1));
            char[] array2 = array;
            foreach (char value in array2)
            {
                int num = Convert.ToInt32(value);
                if (num > 127)
                {
                    stringBuilder.AppendFormat("&#{0};", num);
                }
                else
                {
                    stringBuilder.Append(value);
                }
            }
            return stringBuilder.ToString();
        }
    }

    //***********************   Configuration   **********************
    public interface IConvertOptions
    {
        string GetConvertOptions();
    }

    public enum ContentDisposition
    {
        Attachment,
        Inline
    }

    public class OptionFlag : Attribute
    {
        public string Name { get; private set; }

        public OptionFlag(string name)
        {
            Name = name;
        }
    }

    public class Margins
    {
        [OptionFlag("--margin-bottom")]
        public string Bottom;

        [OptionFlag("--margin-left")]
        public string Left;

        [OptionFlag("--margin-right")]
        public string Right;

        [OptionFlag("--margin-top")]
        public string Top;

        public Margins()
        {
        }

        public Margins(string top, string right, string bottom, string left)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }

        public Margins(string top, string bottom, string left)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
        }

        public Margins(string top, string left)
        {
            Top = top;
            Left = left;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            FieldInfo[] fields = GetType().GetFields();
            foreach (FieldInfo fieldInfo in fields)
            {
                if (fieldInfo.GetCustomAttributes(typeof(OptionFlag), inherit: true).FirstOrDefault() is OptionFlag optionFlag)
                {
                    object value = fieldInfo.GetValue(this);
                    if (value != null)
                    {
                        stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " {0} {1}", optionFlag.Name, value);
                    }
                }
            }
            return stringBuilder.ToString().Trim();
        }
    }

    public enum Orientation
    {
        Landscape,
        Portrait
    }

    public enum Size
    {
        A0,
        A1,
        A2,
        A3,
        A4,
        A5,
        A6,
        A7,
        A8,
        A9,
        B0,
        B1,
        B2,
        B3,
        B4,
        B5,
        B6,
        B7,
        B8,
        B9,
        B10,
        C5E,
        Comm10E,
        Dle,
        Executive,
        Folio,
        Ledger,
        Legal,
        Letter,
        Tabloid
    }

    public class ConvertOptions : IConvertOptions
    {
        [OptionFlag("--page-size")]
        public Size? PageSize { get; set; }

        [OptionFlag("--page-width")]
        public string PageWidth { get; set; }

        [OptionFlag("--page-height")]
        public string PageHeight { get; set; }

        [OptionFlag("-O")]
        public Orientation? PageOrientation { get; set; }

        public Margins PageMargins { get; set; }

        [OptionFlag("--enable-forms")]
        public bool EnableForms { get; set; }

        [OptionFlag("-l")]
        public bool IsLowQuality { get; set; }

        [OptionFlag("--copies")]
        public int? Copies { get; set; }

        [OptionFlag("-g")]
        public bool IsGrayScale { get; set; }

        [OptionFlag("--header-html")]
        public string HeaderHtml { get; set; }

        [OptionFlag("--header-spacing")]
        public int? HeaderSpacing { get; set; }

        [OptionFlag("--footer-html")]
        public string FooterHtml { get; set; }

        [OptionFlag("--footer-spacing")]
        public int? FooterSpacing { get; set; }

        [OptionFlag("--replace")]
        public Dictionary<string, string> Replacements { get; set; }

        [OptionFlag("--zoom")]
        public string Zoom { get; set; }

        [OptionFlag("--minimum-font-size")]
        public string MinimumFontSize { get; set; }

        [OptionFlag("--print-media-type")]
        public string PrintMediaType { get; set; }

        [OptionFlag("--dpi")]
        public string DPI { get; set; }

        [OptionFlag("--disable-smart-shrinking")]
        public string DisableSmartShrinking { get; set; }

        public ConvertOptions()
        {
            PageMargins = new Margins();
        }

        protected string GetContentType()
        {
            return "application/pdf";
        }

        public string GetConvertOptions()
        {
            StringBuilder stringBuilder = new();
            if (PageMargins != null)
            {
                stringBuilder.Append(PageMargins.ToString());
            }
            stringBuilder.Append(" ");
            stringBuilder.Append(GetConvertBaseOptions());
            return stringBuilder.ToString().Trim();
        }

        protected string GetConvertBaseOptions()
        {
            StringBuilder stringBuilder = new();
            PropertyInfo[] properties = GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (!(propertyInfo.GetCustomAttributes(typeof(OptionFlag), inherit: true).FirstOrDefault() is OptionFlag optionFlag))
                {
                    continue;
                }
                object value = propertyInfo.GetValue(this, null);
                if (value == null)
                {
                    continue;
                }
                if (propertyInfo.PropertyType == typeof(Dictionary<string, string>))
                {
                    foreach (KeyValuePair<string, string> item in (Dictionary<string, string>)value)
                    {
                        stringBuilder.AppendFormat(" {0} \"{1}\" \"{2}\"", optionFlag.Name, item.Key, item.Value);
                    }
                }
                else if (propertyInfo.PropertyType == typeof(bool))
                {
                    if ((bool)value)
                    {
                        stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " {0}", optionFlag.Name);
                    }
                }
                else
                {
                    stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " {0} {1}", optionFlag.Name, value);
                }
            }
            return stringBuilder.ToString().Trim();
        }
    }
}
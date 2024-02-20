using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;

namespace Core.Util
{
    public interface IConvertExcelToPDF
    {
        byte[] ConvertXlsToPdf(byte[] fileData);

        byte[] ConvertXlsxToPdf(byte[] fileData);
    }

    public class ConvertExcelToPDF : IConvertExcelToPDF
    {
        private readonly IMemoryStreamManager _memoryStreamManager;
        private readonly IHTMLtoPDF _hTMLtoPDF;

        public ConvertExcelToPDF(IMemoryStreamManager memoryStreamManager, IHTMLtoPDF hTMLtoPDF)
        {
            _memoryStreamManager = memoryStreamManager;
            _hTMLtoPDF = hTMLtoPDF;
        }

        public byte[] ConvertXlsToPdf(byte[] fileData)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(_memoryStreamManager.GetStream(fileData));
            ExcelToHtmlConverter excelToHtmlConverter = new ExcelToHtmlConverter();
            // Set output parameters
            excelToHtmlConverter.OutputColumnHeaders = false;
            //excelToHtmlConverter.OutputHiddenColumns = true;
            //excelToHtmlConverter.OutputHiddenRows = true;
            //excelToHtmlConverter.OutputLeadingSpacesAsNonBreaking = false;
            excelToHtmlConverter.OutputRowNumbers = true;
            //excelToHtmlConverter.UseDivsToSpan = true;
            // Process the Excel file
            excelToHtmlConverter.ProcessWorkbook(workbook);
            // Output the HTML file
            MemoryStream file = _memoryStreamManager.GetStream();
            excelToHtmlConverter.Document.Save(file);
            return _hTMLtoPDF.GeneratePDF(file.ToArray());
        }

        public byte[] ConvertXlsxToPdf(byte[] fileData)
        {
            XSSFWorkbook xssfwb = new XSSFWorkbook(_memoryStreamManager.GetStream(fileData));
            ExcelToHtmlConverter excelToHtmlConverter = new ExcelToHtmlConverter();
            //set output parameter
            excelToHtmlConverter.OutputColumnHeaders = false;
            //excelToHtmlConverter.OutputHiddenColumns = true;
            //excelToHtmlConverter.OutputHiddenRows = true;
            //excelToHtmlConverter.OutputLeadingSpacesAsNonBreaking = false;
            excelToHtmlConverter.OutputRowNumbers = false;
            //excelToHtmlConverter.UseDivsToSpan = true;
            //process the excel file
            excelToHtmlConverter.ProcessWorkbook(xssfwb);
            //output the html file
            MemoryStream file = _memoryStreamManager.GetStream();
            excelToHtmlConverter.Document.Save(file);
            return _hTMLtoPDF.GeneratePDF(file.ToArray());
        }
    }
}
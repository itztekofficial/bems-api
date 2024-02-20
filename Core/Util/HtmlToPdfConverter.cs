using System;

namespace Core.Util
{
    public interface IHtmlToPdfConverter
    {
        public byte[] ConvertToPdf(string htmlcode, string pagesize = "A4", string orientation = "Portrait",
            float leftmargin = 0, float bottommargin = 0, float rightmargin = 0, float topmargin = 0, bool fullpage = false, bool isBarcodePrint = false, bool isBarTender = false, string measurement = "", string pageHeight = "", string pageWidth = "");
    }

    public class HtmlToPdfConverter : IHtmlToPdfConverter
    {
        private readonly IHTMLtoPDF _hTMLtoPDF1;

        public HtmlToPdfConverter(IHTMLtoPDF hTMLtoPDF1)
        {
            _hTMLtoPDF1 = hTMLtoPDF1;
        }

        public byte[] ConvertToPdf(string htmlcode, string pagesize = "A4", string orientation = "Portrait",
            float leftmargin = 0, float bottommargin = 0, float rightmargin = 0, float topmargin = 0, bool fullpage = false, bool isBarcodePrint = false, bool isBarTender = false, string measurement = "", string pageHeight = "", string pageWidth = "")
        {
            // read parameters from the webpage
            string htmlString = htmlcode;
            //string baseUrl = collection["TxtBaseUrl"];
            //string baseUrl = "";

            string pdf_page_size = pagesize;
            if (pagesize.Contains("-"))
            {
                var arr = pagesize.Split('-');
                pdf_page_size = arr[0];
            }

            byte[] pdf = null;
            ConvertOptions convertOptions = new ConvertOptions
            {
                PrintMediaType = "",
                DPI = "300",
                DisableSmartShrinking = "",
                PageOrientation = (orientation == "Portrait") ? Orientation.Portrait : Orientation.Landscape
            };

            if (isBarcodePrint)
            {
                try
                {
                    convertOptions.PageHeight = "297";
                    convertOptions.PageWidth = "210";
                    if (isBarTender)
                    {
                        convertOptions.PageOrientation = Orientation.Landscape;
                        convertOptions.PageMargins = new Margins("0", "0", "0");
                    }
                    else
                    {
                        convertOptions.PageMargins = new Margins(topmargin.ToString() + "" + measurement, "0", "0", "0");
                    }

                    _hTMLtoPDF1.SetConvertOptions(convertOptions);
                    pdf = _hTMLtoPDF1.GeneratePDF(htmlString);
                }
                catch (Exception ex)
                {
                    Log.WriteLog("HtmlToPdfConverter", "GeneratePDF", "", ex.Message);
                }
            }
            else
            {
                ConvertOptions PrintconvertOptions = new ConvertOptions
                {
                    PageOrientation = (orientation == "Portrait") ? Orientation.Portrait : Orientation.Landscape
                };
                PrintconvertOptions.PageMargins = new Margins("0", "0", "0", "0");
                PrintconvertOptions.PageSize = Size.A4;
                _hTMLtoPDF1.SetConvertOptions(PrintconvertOptions);
                pdf = _hTMLtoPDF1.GeneratePDF(htmlString);
            }
            return pdf;
        }
    }
}
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Core.Util
{
    /// <summary>
    /// The util function.
    /// </summary>
    public static class UtilFunction
    {
        /// <summary>
        /// Tos the data table.
        /// </summary>
        /// <param name="iList">The i list.</param>
        /// <returns>A DataTable.</returns>
        public static DataTable ToDataTable<T>(List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);

                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        /// <summary>
        /// Base64S the encode.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>A string.</returns>
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Base64S the decode.
        /// </summary>
        /// <param name="base64EncodedData">The base64 encoded data.</param>
        /// <returns>A string.</returns>
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// Onlies the letters.
        /// </summary>
        /// <param name="Text">The text.</param>
        /// <returns>A bool.</returns>
        public static bool OnlyLetters(string Text)
        {
            return Regex.IsMatch(Text, "^[a-zA-Z]+$");
        }

        /// <summary>
        /// Gets the g u i d.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetGUID()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Contains the any.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="containsKeywords">The contains keywords.</param>
        /// <returns>A bool.</returns>
        public static bool ContainsAny(string input, IEnumerable<string> containsKeywords)
        {
            return containsKeywords.Any(keyword => input.IndexOf(keyword, StringComparison.Ordinal) >= 0);
        }

        /// <summary>
        /// Cleans the invalid xml chars.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A string.</returns>
        public static string CleanInvalidXmlChars(string text)
        {
            string re = @"[^\x09\x0A\x0D\x20-\xD7FF\xE000-\xFFFD\x10000-x10FFFF]";
            return Regex.Replace(text, re, "");
        }

        /// <summary>
        /// Bytes the to hex string.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>A string.</returns>
        public static string ByteToHexString(byte[] data)
        {
            return "0x" + BitConverter.ToString(data).Replace("-", string.Empty);
        }

        /// <summary>
        /// Hexes the string to byte array.
        /// </summary>
        /// <param name="hex">The hex.</param>
        /// <returns>An array of byte.</returns>
        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        /// <summary>
        /// Removes the special characters.
        /// </summary>
        /// <param name="str">The str.</param>
        /// <returns>A string.</returns>
        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c == '.' || c == ','
                    || c == '_' || c == '/' || c == '-' || c == ' '))
                {
                    sb.Append(c);
                }
            }

            string _str = sb.ToString();

            _str = _str.Trim();

            return _str;
        }

        /// <summary>
        /// Removes the single quote.
        /// </summary>
        /// <param name="str">The str.</param>
        /// <returns>A string.</returns>
        public static string RemoveSingleQuote(this string str)
        {
            if (str.Contains("'") == true)
                str = str.Replace("'", "");

            str = str.Trim();

            return str;
        }

        /// <summary>
        /// Singles the to double quotes.
        /// </summary>
        /// <param name="inStr">The in str.</param>
        /// <returns>A string.</returns>
        public static string SingleToDoubleQuotes(string inStr)
        {
            if (inStr.Contains("'") == true)
                return inStr.Replace("'", "''");
            else
                return inStr;
        }

        /// <summary>
        /// Cleans the string.
        /// </summary>
        /// <param name="str">The str.</param>
        /// <returns>A string.</returns>
        public static string CleanString(string str)
        {
            return str.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
        }

        /// <summary>
        /// Gets the alpha numeric string.
        /// </summary>
        /// <param name="str">The str.</param>
        /// <returns>A string.</returns>
        public static string GetAlphaNumericString(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9 -]", "");
        }

        /// <summary>
        /// Are the alpha numeric.
        /// </summary>
        /// <param name="Text">The text.</param>
        /// <returns>A bool.</returns>
        public static bool IsAlphaNumeric(string Text)
        {
            return Regex.IsMatch(Text, "^[0-9a-zA-Z]+$");
        }

        /// <summary>
        /// Are the numeric.
        /// </summary>
        /// <param name="Text">The text.</param>
        /// <returns>A bool.</returns>
        public static bool IsNumeric(string Text)
        {
            return Regex.IsMatch(Text, @"^\d$");
        }

        /// <summary>
        /// Are the data numeric.
        /// </summary>
        /// <param name="Text">The text.</param>
        /// <returns>A bool.</returns>
        public static bool IsDataNumeric(string Text)
        {
            double dat;
            return double.TryParse(Text, out dat);
        }

        /// <summary>
        /// Are the date.
        /// </summary>
        /// <param name="Text">The text.</param>
        /// <returns>A bool.</returns>
        public static bool IsDate(string Text)
        {
            try
            {
                DateTime dt = DateTime.Parse(Text);
                return true;
            }
            catch
            { return false; }
        }

        /// <summary>
        /// Dates the str for pur imp fr exl.
        /// </summary>
        /// <param name="Text">The text.</param>
        /// <param name="DateFormat">The date format.</param>
        /// <param name="DateType">The date type.</param>
        /// <param name="finFr">The fin fr.</param>
        /// <param name="finTo">The fin to.</param>
        /// <returns>A string.</returns>
        public static string DateStrForPurImpFrExl(string Text, string DateFormat, string DateType, DateTime finFr, DateTime finTo)
        {
            string s = Text, t = FindSeperatorInDate(Text);
            string _dd = string.Empty, _mo = string.Empty, _yr = string.Empty;
            string date = "";// DateTime.Now.Date.ToString();
            string f1 = string.Empty, f2 = string.Empty, f3 = string.Empty;

            //== Important !
            //   -----------
            //== Function will work only if date string will be any standard format == //

            if (s.IndexOf(t) > 0)
            {
                if (s.IndexOf(t, s.IndexOf(t) + 1) > -1)
                {
                    f1 = s.Substring(0, s.IndexOf(t));
                    f2 = s.Substring(s.IndexOf(t) + 1, s.IndexOf(t, s.IndexOf(t) + 1) - s.IndexOf(t) - 1);
                    f3 = s.Substring(s.IndexOf(t, s.IndexOf(t) + 1) + 1);
                    if (f1.Length > 0 && f2.Length > 0 && f3.Length > 0)
                    {
                        date = SetDateAsPerCoProfilerFormat(f1, f2, f3, DateFormat, finFr, finTo);
                    }
                }
                else
                {
                    f1 = s.Substring(0, s.IndexOf(t));
                    f2 = s.Substring(s.IndexOf(t) + 1);
                    f3 = "";
                    if (f1.Length > 0 && f2.Length > 0)
                    {
                        if (DateType == "Expiry")
                        {
                            if (int.Parse(f1) < 13)
                            { _mo = f1; _yr = f2; }
                            else
                            { _mo = f2; _yr = f1; }
                            _dd = SetDay(_mo, _yr);
                            date = _dd + t + _mo + t + _yr;
                        }
                        else
                        {
                            date = SetDateAsPerCoProfilerFormat(f1, f2, f3, DateFormat, finFr, finTo);
                        }
                    }
                }
            }
            else
            {
                if (Text.Length == 8)
                {
                    f1 = Text.Substring(0, 2);
                    f2 = Text.Substring(2, 2);
                    f3 = Text.Substring(4);

                    date = SetDateAsPerCoProfilerFormat(f1, f2, f3, "dd-MM-yyyy", finFr, finTo);
                }
                else if (Text.Length == 6)
                {
                    f1 = Text.Substring(0, 2);
                    f2 = Text.Substring(2, 2);
                    f3 = Text.Substring(4);

                    date = SetDateAsPerCoProfilerFormat(f1, f2, f3, "dd-MM-yyyy", finFr, finTo);
                }
                else if (Text.Length == 4)
                {
                    f1 = "01";
                    f2 = Text.Substring(0, 2);
                    f3 = Text.Substring(2);

                    date = SetDateAsPerCoProfilerFormat(f1, f2, f3, "dd-MM-yyyy", finFr, finTo);
                }
                else if (Text.Length == 5)
                {
                    date = DateTime.FromOADate(Convert.ToDouble(Text)).ToString();
                }
            }

            return date;
        }

        public static string DateStrForPurImpFrExlMMDDYY(string Text, string DateFormat, string DateType, DateTime finFr, DateTime finTo)
        {
            string s = Text, t = FindSeperatorInDate(Text);
            string _dd = string.Empty, _mo = string.Empty, _yr = string.Empty;
            string date = "";// DateTime.Now.Date.ToString();
            string f1 = string.Empty, f2 = string.Empty, f3 = string.Empty;

            //== Important !
            //   -----------
            //== Function will work only if date string will be any standard format == //

            if (s.IndexOf(t) > 0)
            {
                if (s.IndexOf(t, s.IndexOf(t) + 1) > -1)
                {
                    f1 = s.Substring(0, s.IndexOf(t));
                    f2 = s.Substring(s.IndexOf(t) + 1, s.IndexOf(t, s.IndexOf(t) + 1) - s.IndexOf(t) - 1);
                    f3 = s.Substring(s.IndexOf(t, s.IndexOf(t) + 1) + 1);
                    if (f1.Length > 0 && f2.Length > 0 && f3.Length > 0)
                    {
                        date = SetDateAsPerCoProfilerFormatMMDDYY(f1, f2, f3, DateFormat, finFr, finTo);
                    }
                }
                else
                {
                    f1 = s.Substring(0, s.IndexOf(t));
                    f2 = s.Substring(s.IndexOf(t) + 1);
                    f3 = "";
                    if (f1.Length > 0 && f2.Length > 0)
                    {
                        if (DateType == "Expiry")
                        {
                            if (int.Parse(f1) < 13)
                            { _mo = f1; _yr = f2; }
                            else
                            { _mo = f2; _yr = f1; }
                            _dd = SetDay(_mo, _yr);
                            date = _mo + t + _dd + t + _yr;
                        }
                        else
                        {
                            date = SetDateAsPerCoProfilerFormatMMDDYY(f1, f2, f3, DateFormat, finFr, finTo);
                        }
                    }
                }
            }
            else
            {
                if (Text.Length == 8)
                {
                    f1 = Text.Substring(0, 2);
                    f2 = Text.Substring(2, 2);
                    f3 = Text.Substring(4);

                    date = SetDateAsPerCoProfilerFormat(f1, f2, f3, "MM-dd-yyyy", finFr, finTo);
                }
                else if (Text.Length == 6)
                {
                    f1 = Text.Substring(0, 2);
                    f2 = Text.Substring(2, 2);
                    f3 = Text.Substring(4);

                    date = SetDateAsPerCoProfilerFormat(f1, f2, f3, "MM-dd-yyyy", finFr, finTo);
                }
                else if (Text.Length == 4)
                {
                    f1 = "01";
                    f2 = Text.Substring(0, 2);
                    f3 = Text.Substring(2);

                    date = SetDateAsPerCoProfilerFormat(f1, f2, f3, "MM-dd-yyyy", finFr, finTo);
                }
                else if (Text.Length == 5)
                {
                    date = DateTime.FromOADate(Convert.ToDouble(Text)).ToString();
                }
            }

            return date;
        }

        /// <summary>
        /// Finds the seperator in date.
        /// </summary>
        /// <param name="dateFormatOrString">The date format or string.</param>
        /// <returns>A string.</returns>
        private static string FindSeperatorInDate(string dateFormatOrString)
        {
            string seperator = string.Empty;
            if (dateFormatOrString.IndexOf(".") > -1)
            { seperator = "."; }
            if (dateFormatOrString.IndexOf("-") > -1)
            { seperator = "-"; }
            if (dateFormatOrString.IndexOf("/") > -1)
            { seperator = "/"; }

            return seperator;
        }

        /// <summary>
        /// Sets the date as per co profiler format.
        /// </summary>
        /// <param name="f1">The f1.</param>
        /// <param name="f2">The f2.</param>
        /// <param name="f3">The f3.</param>
        /// <param name="dateformat">The dateformat.</param>
        /// <param name="finFr">The fin fr.</param>
        /// <param name="finTo">The fin to.</param>
        /// <returns>A string.</returns>
        private static string SetDateAsPerCoProfilerFormat(string f1, string f2, string f3, string dateformat, DateTime finFr, DateTime finTo)
        {
            string dd = "", mm = "", yyyy = "";
            string fF1, fF2, fF3, fS = FindSeperatorInDate(dateformat);
            string date;

            fF1 = dateformat.Substring(0, dateformat.IndexOf(fS));
            fF2 = dateformat.Substring(dateformat.IndexOf(fS) + 1, dateformat.IndexOf(fS, dateformat.IndexOf(fS) + 1) / dateformat.IndexOf(fS) / 1);
            fF3 = dateformat.Substring(dateformat.IndexOf(fS, dateformat.IndexOf(fS) + 1) + 1);

            //format this but in excel comming diff ... have to think n do
            if (fF1.ToUpper() == "DD" && fF2.ToUpper() == "MM" && fF3.ToUpper() == "YYYY")
            { dd = f1; mm = f2; yyyy = f3; }
            else if (fF1.ToUpper() == "MM" && fF2.ToUpper() == "DD" && fF3.ToUpper() == "YYYY")
            { dd = f2; mm = f1; yyyy = f3; }
            else if (fF1.ToUpper() == "YYYY" && fF2.ToUpper() == "MM" && fF3.ToUpper() == "DD")
            { dd = f3; mm = f2; yyyy = f1; }

            if (yyyy.Length == 0)
            {
                if (mm == "01" || mm == "02" || mm == "03" || mm == "1" || mm == "2" || mm == "3")
                { yyyy = finTo.Year.ToString(); }
                else
                { yyyy = finFr.Year.ToString(); }
            }
            else if (yyyy.Length == 2 || yyyy.Length == 1)
            {
                yyyy = DateTime.Now.Date.Year.ToString().Substring(0, 2) + string.Format("{0:00}", int.Parse(yyyy));
            }

            if (dd.Length == 0) { dd = SetDay(mm, yyyy); }

            date = dd + fS + mm + fS + yyyy;

            return date;
        }

        private static string SetDateAsPerCoProfilerFormatMMDDYY(string f1, string f2, string f3, string dateformat, DateTime finFr, DateTime finTo)
        {
            string dd = "", mm = "", yyyy = "";
            string fF1, fF2, fF3, fS = FindSeperatorInDate(dateformat);
            string date;

            fF1 = dateformat.Substring(0, dateformat.IndexOf(fS));
            fF2 = dateformat.Substring(dateformat.IndexOf(fS) + 1, dateformat.IndexOf(fS, dateformat.IndexOf(fS) + 1) / dateformat.IndexOf(fS) / 1);
            fF3 = dateformat.Substring(dateformat.IndexOf(fS, dateformat.IndexOf(fS) + 1) + 1);

            //format this but in excel comming diff ... have to think n do
            if (fF1.ToUpper() == "DD" && fF2.ToUpper() == "MM" && fF3.ToUpper() == "YYYY")
            { dd = f1; mm = f2; yyyy = f3; }
            else if (fF1.ToUpper() == "MM" && fF2.ToUpper() == "DD" && fF3.ToUpper() == "YYYY")
            { dd = f2; mm = f1; yyyy = f3; }
            else if (fF1.ToUpper() == "YYYY" && fF2.ToUpper() == "MM" && fF3.ToUpper() == "DD")
            { dd = f3; mm = f2; yyyy = f1; }

            if (yyyy.Length == 0)
            {
                if (mm == "01" || mm == "02" || mm == "03" || mm == "1" || mm == "2" || mm == "3")
                { yyyy = finTo.Year.ToString(); }
                else
                { yyyy = finFr.Year.ToString(); }
            }
            else if (yyyy.Length == 2 || yyyy.Length == 1)
            {
                yyyy = DateTime.Now.Date.Year.ToString().Substring(0, 2) + string.Format("{0:00}", int.Parse(yyyy));
            }

            if (dd.Length == 0) { dd = SetDay(mm, yyyy); }

            date = mm + fS + dd + fS + yyyy;

            return date;
        }

        /// <summary>
        /// Sets the day.
        /// </summary>
        /// <param name="mm">The mm.</param>
        /// <param name="yyyy">The yyyy.</param>
        /// <returns>A string.</returns>
        private static string SetDay(string mm, string yyyy)
        {
            return DateTime.DaysInMonth(Convert.ToInt32(yyyy), Convert.ToInt32(mm)).ToString();
        }

        /// <summary>
        /// Tos the date.
        /// </summary>
        /// <param name="Text">The text.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <returns>A string.</returns>
        public static string ToDate(string Text, string dateFormat = "")
        {
            string s = Text;
            string error = String.Empty;
            DateTime? date = null;

            if (Text.Length == 10)
            {
                if (IsDate(Text))
                { date = new Nullable<DateTime>(DateTime.Parse(Text)); }
                else
                {
                }
            }
            else if (Text.Length == 5 || Text.Length == 4 || Text.Length == 7 || Text.Length == 8)
            {
                string _dd = "";
                string _mo = "";
                string _yr = "";

                DateTime dt = DateTime.Now.Date;

                if (s.IndexOf(".") > 0 || s.IndexOf("/") > 0 || s.IndexOf("-") > 0)
                {
                    if (s.IndexOf(".") > 0)
                    {
                        if (s.Length == 5 || s.Length == 4)
                        {
                            _mo = s.Substring(0, s.IndexOf("."));
                            _yr = s.Substring(s.IndexOf(".") + 1, 2);

                            if (_mo == "02" || _mo == "2")
                            {
                                _dd = "28";
                            }
                            else
                            {
                                _dd = DateTime.Now.Date.Day.ToString();
                            }
                        }
                        else if (s.Length == 8 || s.Length == 8)
                        {
                            _dd = s.Substring(0, s.IndexOf("."));
                            _mo = s.Substring(s.IndexOf(".") + 1, s.IndexOf(".", s.IndexOf(".") + 1) - s.IndexOf(".") - 1);
                            _yr = s.Substring(s.IndexOf(".", s.IndexOf(".") + 1) + 1);
                        }
                    }
                    else if (s.IndexOf("/") > 0)
                    {
                        if (s.Length == 5 || s.Length == 4)
                        {
                            _mo = s.Substring(0, s.IndexOf("/"));
                            _yr = s.Substring(s.IndexOf("/") + 1, 2);

                            if (_mo == "02" || _mo == "2")
                            {
                                _dd = "28";
                            }
                            else
                            {
                                _dd = DateTime.Now.Date.Day.ToString();
                            }
                        }
                        else if (s.Length == 8 || s.Length == 8)
                        {
                            _dd = s.Substring(0, s.IndexOf("/"));
                            _mo = s.Substring(s.IndexOf("/") + 1, s.IndexOf("/", s.IndexOf("/") + 1) - s.IndexOf("/") - 1);
                            _yr = s.Substring(s.IndexOf("/", s.IndexOf("/") + 1) + 1);
                        }
                    }
                    else if (s.IndexOf("-") > 0)
                    {
                        string f1 = "", f2 = "", f3 = "";

                        f1 = s.Substring(0, s.IndexOf("-"));
                        if (s.IndexOf("-", s.IndexOf("-") + 1) > -1)
                        {
                            f2 = s.Substring(s.IndexOf("-") + 1, s.IndexOf("-", s.IndexOf("-") + 1) - s.IndexOf("-") - 1);
                            f3 = s.Substring(s.IndexOf("-", s.IndexOf("-") + 1) + 1);
                        }
                        else
                        {
                            f2 = s.Substring(0, s.IndexOf("-"));
                            f3 = s.Substring(s.IndexOf("-") + 1);
                        }

                        if (f1.Length > 0)
                        {
                            if (Convert.ToInt32(f1) > 12)
                            { _dd = f1; _mo = f2; }
                            else
                            { _mo = f1; _dd = f2; }
                        }
                        else
                        {
                            _mo = f2;
                            if (_mo == "01" || _mo == "03" || _mo == "05" || _mo == "07" || _mo == "08" || _mo == "10" || _mo == "12")
                            { _dd = "31"; }
                            else if (_mo == "04" || _mo == "06" || _mo == "09" || _mo == "11")
                            { _dd = "30"; }
                            else
                            {
                                if (((int.Parse(f3) % 4 == 0) && (int.Parse(f3) % 100 != 0)) || (int.Parse(f3) % 400 == 0))
                                { _dd = "29"; }
                                else
                                { _dd = "28"; }
                            }
                        }
                        _yr = f3;
                    }

                    _yr = DateTime.Now.Date.Year.ToString().Substring(0, 2) + _yr;

                    if (Convert.ToInt32(_mo) > 12)
                    { string mm = _mo; _mo = _yr; _yr = mm; }
                    else if (Convert.ToInt32(_mo) < 1)
                    { error = "Invalid Date !"; }

                    string _dt = _yr + "-" + _mo + "-" + _dd;// DateTime.Now.Date.Day;

                    date = new Nullable<DateTime>(DateTime.Parse(_dt));
                }
                else
                { date = DateTime.FromOADate(Convert.ToInt32(Text)); }
            }

            if (dateFormat.Length > 0)
            {
                string _f1 = "", _f2 = "", _f3 = "";
                string _day = "", _month = "", _year = "";
                string _date = "";

                _f1 = dateFormat.Substring(0, dateFormat.IndexOf("/"));
                _f2 = dateFormat.Substring(dateFormat.IndexOf("/") + 1, dateFormat.IndexOf("/", dateFormat.IndexOf("/") + 1) / dateFormat.IndexOf("/") / 1);
                _f3 = dateFormat.Substring(dateFormat.IndexOf("/", dateFormat.IndexOf("/") + 1) + 1);

                _day = Convert.ToDateTime(date).Day.ToString();
                _month = Convert.ToDateTime(date).Month.ToString();
                _year = Convert.ToDateTime(date).Year.ToString();

                _date = "";
                if (_f1.ToUpper() == "DD")
                { _date += _day; }
                else if (_f1.ToUpper() == "MM")
                { _date += _month; }
                else if (_f1.ToUpper() == "YYYY")
                { _date += _year; }

                _date += "/";
                if (_f2.ToUpper() == "DD")
                { _date += _day; }
                else if (_f2.ToUpper() == "MM")
                { _date += _month; }
                else if (_f2.ToUpper() == "YYYY")
                { _date += _year; }

                _date += "/";
                if (_f3.ToUpper() == "DD")
                { _date += _day; }
                else if (_f3 == "MM")
                { _date += _month; }
                else if (_f3.ToUpper() == "YYYY")
                { _date += _year; }

                return _date.ToString();
            }
            else
            {
                return date.ToString();
            }
            //return date.ToString();
        }

        /// <summary>
        /// Isfourdigits the.
        /// </summary>
        /// <param name="Number">The number.</param>
        /// <returns>A bool.</returns>
        public static bool Isfourdigit(string Number)
        {
            return Regex.IsMatch(Number, @"^\d{1,4}");
        }

        /// <summary>
        /// Generates the voucher no.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GenerateVoucherNo()
        {
            Random random = new Random();
            return random.Next(0, 9999).ToString("D6");
        }

        /// <summary>
        /// Gets the from to date.
        /// </summary>
        /// <param name="finFromDate">The fin from date.</param>
        /// <param name="finToDate">The fin to date.</param>
        /// <param name="month">The month.</param>
        /// <param name="toDate">The to date.</param>
        /// <param name="isFound">If true, is found.</param>
        /// <returns>A DateTime.</returns>
        public static DateTime GetFromToDate(DateTime finFromDate, DateTime finToDate, string month, out DateTime toDate, out bool isFound)
        {
            DateTime fromDate = DateTime.Now;
            toDate = DateTime.Now;
            isFound = false;
            DateTime dtimeTemp = new DateTime(finFromDate.Year, finFromDate.Month, 1);
            while (dtimeTemp <= new DateTime(finToDate.Year, finToDate.Month, 1))
            {
                if (dtimeTemp.ToString("MMMM").Contains(month) == true)
                {
                    fromDate = dtimeTemp;
                    toDate = new DateTime(fromDate.Year, fromDate.Month, DateTime.DaysInMonth(fromDate.Year, fromDate.Month));
                    isFound = true;
                    break;
                }

                dtimeTemp = dtimeTemp.AddMonths(1);
            }
            return fromDate;
        }

        private static List<long> tmpLinkID1 = new List<long>();
        private static long tmpid1 = 0;
        private static readonly object objLock1 = new object();

        /// <summary>
        /// Gets the link i d.
        /// </summary>
        /// <returns>A long.</returns>
        public static long GetLinkID()
        {
            lock (objLock1)
            {
                if (tmpLinkID1.Count > 0)
                {
                    tmpid1 = tmpid1 + 1;
                    return tmpid1;
                }
                else
                {
                    tmpid1 = Id64Generator.GetID;
                    tmpLinkID1.Add(tmpid1);
                    return tmpid1;
                }
            }
            ////sreturn Id64Generator.GetID;
        }

        /// <summary>
        /// Gets the link i d.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns>A list of long.</returns>
        public static List<long> GetLinkID(int length)
        {
            List<long> lstLinkid = new List<long>();
            for (int i = 1; i <= length; i++)
            {
                lstLinkid.Add(Id64Generator.GetID);
            }
            return lstLinkid;
        }

        private static List<long> tmpLinkID2 = new List<long>();
        private static long tmpid2 = 0;
        private static readonly object objLock2 = new object();

        /// <summary>
        /// Gets the temp i d.
        /// </summary>
        /// <returns>A long.</returns>
        public static long GetTempID()
        {
            lock (objLock2)
            {
                if (tmpLinkID2.Count > 0)
                {
                    tmpid2 = tmpid2 + 1;
                    return tmpid2;
                }
                else
                {
                    tmpid2 = Id64Generator.GetTempID();
                    tmpLinkID2.Add(tmpid2);
                    return tmpid2;
                }
            }
        }

        /// <summary>
        /// Gets the total second.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetTotalSecond()
        {
            DateTime value = new DateTime(1980, 1, 1, 0, 0, 0, 0);
            return DateTime.Now.Subtract(value).TotalSeconds.ToString("#0");
        }

        /// <summary>
        /// Are the valid website.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>A bool.</returns>
        public static bool IsValidWebsite(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;

            return Regex.IsMatch(s.ToLower(), @"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([^,][\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$+?[^,]*$");
        }

        /// <summary>
        /// validurls the.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>A bool.</returns>
        public static bool validurl(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;

            return Regex.IsMatch(s.ToLower(), @"http?:\/\/(www.\.)?[-a-zA-Z0-9]@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)");
        }

        /// <summary>
        /// Are the valid pan.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>A bool.</returns>
        public static bool IsValidPan(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;

            return Regex.IsMatch(s, @"^[A-Za-z]{5}\d{4}[A-Za-z]{1}$");
        }

        /// <summary>
        /// Are the valid email.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>A bool.</returns>
        public static bool IsValidEmail(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;

            return Regex.IsMatch(s, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,10}$");
        }

        //public static BitmapImage LoadImageinList(byte[] imagedata)
        //{
        //    if (imagedata == null || imagedata.Length == 0) return null;
        //    try
        //    {
        //        var image = new BitmapImage();
        //        using (var mem = new MemoryStream(imagedata))
        //        {
        //            mem.Position = 0;
        //            image.BeginInit();
        //            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
        //            image.CacheOption = BitmapCacheOption.OnLoad;
        //            image.UriSource = null;
        //            image.StreamSource = mem;
        //            image.EndInit();
        //        }
        //        image.Freeze();
        //        return image;
        //    }
        //    catch (Exception)
        //    { }
        //    return null;
        //}

        /// <summary>
        /// Are the valid phone.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>A bool.</returns>
        public static bool IsValidPhone(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;

            return Regex.IsMatch(s, @"\d{5}([- ]*)\d{6}");
        }

        /// <summary>
        /// Are the valid mobile.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>A bool.</returns>
        public static bool IsValidMobile(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;
            if (s.Contains("-"))
            {
                return Regex.IsMatch(s, @"^[\d-]{11,13}$");
            }
            else
            {
                return Regex.IsMatch(s, @"^[\d-]{10,12}$");
            }
        }

        /// <summary>
        /// Are the valid mobile11 digit.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>A bool.</returns>
        public static bool IsValidMobile11Digit(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;

            return Regex.IsMatch(s, "^[0-9]{11}$");
        }

        /// <summary>
        /// Are the valid mobile12 digit.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>A bool.</returns>
        public static bool IsValidMobile12Digit(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;

            return Regex.IsMatch(s, "^[0-9]{12}$");
        }

        /// <summary>
        /// Are the validtelmob.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>A bool.</returns>
        public static bool IsValidtelmob(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;

            return Regex.IsMatch(s, @"^(?:\s+|)((0|(?:(\+|)91))(?:\s|-)*(?:(?:\d(?:\s|-)*\d{9})|(?:\d{2}(?:\s|-)*\d{8})|(?:\d{3}(?:\s|-)*\d{7}))|\d{10})(?:\s+|)$");
        }

        /// <summary>
        /// Are the valid passwowd.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>A bool.</returns>
        public static bool IsValidPasswowd(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            return Regex.IsMatch(password, "[0-9]+") && Regex.IsMatch(password, "[A-Z]+") && Regex.IsMatch(password, ".{4,}") && Regex.IsMatch(password, "[a-z]+")
                && Regex.IsMatch(password, @"[A-Za-z\\d$@$!%*#?~^()-_+{}|<>]");
        }

        /// <summary>
        /// Passwords the text notallowed.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>A bool.</returns>
        public static bool PasswordTextNotallowed(string s)
        {
            return s == "OemPeriod" || s == "OemComma" || s == "Oem3" || s == "OemOpenBrackets" || s == "Oem6" || s == "Oem7";
        }

        /// <summary>
        /// Specials the character notallowed.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>A bool.</returns>
        public static bool SpecialCharacterNotallowed(string s)
        {
            return s == "OemPeriod" || s == "OemComma" || s == "Oem3" || s == "Oem6" || s == "Oem7" || s == "Oem1"
                || s == "Oem2" || s == "Oem3" || s == "OemOpenBrackets" || s == "Oem5" || s == "Oem6" || s == "Oem7"
                || s == "Oem102" || s == "OemPlus" || s == "OemMinus" || s == "OemPeriod" || s == "OemQuotes"
                || s == "OemQuestion" || s == "Divide" || s == "Multiply" || s == "Subtract" || s == "Add";
        }

        /// <summary>
        /// Are the network available.
        /// </summary>
        /// <returns>A bool.</returns>
        public static bool IsNetworkAvailable()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        /// <summary>
        /// getfinancialyears the.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <returns>A string.</returns>
        public static string getfinancialyear(Int32 year, Int32 month)
        {
            if (month <= 3)
            {
                year = year - 1;
            }
            string financialyear = "01/04/" + year + "";
            return financialyear;
        }

        /// <summary>
        /// Gets the formatted date.
        /// </summary>
        /// <param name="_date">The _date.</param>
        /// <returns>A string.</returns>
        public static string GetFormattedDate(DateTime _date)
        {
            return _date.ToString("dd-MMM-yy");
        }

        public static string GetFormattedDateNew(DateTime _date)
        {
            return _date.ToString("dd-MM-yyyy");
        }

        /// <summary>
        /// Gets the formatted date.
        /// </summary>
        /// <param name="_date">The _date.</param>
        /// <returns>A string.</returns>
        public static string GetGSTR1FormattedDate(DateTime _date)
        {
            return _date.ToString("dd-MMM-yyyy");
        }

        /*Used to get accountgroupcode from child count of accountgroupmaster-------Added by yudhvir*/

        /// <summary>
        /// Ints the to char.
        /// </summary>
        /// <param name="parentacccode">The parentacccode.</param>
        /// <param name="childcount">The childcount.</param>
        /// <returns>A string.</returns>
        public static string IntToChar(string parentacccode, int childcount)
        {
            const String alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            char char1 = alphabets[childcount];
            return parentacccode + char1;
        }

        /// <summary>
        /// Calenders the converter.
        /// </summary>
        /// <param name="dateconv">The dateconv.</param>
        /// <param name="calender">The calender.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>A string.</returns>
        public static string CalenderConverter(DateTime dateconv, calendertype calender, culturetype culture)
        {
            DateTimeFormatInfo DTFormat;
            string cul = "en-US";
            if (calender == calendertype.Hizri && culture == culturetype.enUS)
            {
                cul = "ar-sa";
            }
            DTFormat = new CultureInfo(cul, false).DateTimeFormat;
            switch (calender)
            {
                case calendertype.Hizri:
                    DTFormat.Calendar = new HijriCalendar();
                    break;

                case calendertype.Gregorian:
                    DTFormat.Calendar = new GregorianCalendar();
                    break;
            }
            DTFormat.ShortDatePattern = "dd/MM/yyyy";
            return dateconv.Date.ToString("f", DTFormat);
        }

        /// <summary>
        /// Copies the data.
        /// </summary>
        /// <param name="objSource">The obj source.</param>
        /// <param name="objDestination">The obj destination.</param>
        /// <returns>A T.</returns>
        public static T CopyData<T>(T objSource, T objDestination)
        {
            Type typeSource = objSource.GetType();
            Type typeDestination = objDestination.GetType();

            foreach (System.Reflection.PropertyInfo pi in typeSource.GetProperties())
            {
                typeDestination.GetProperty(pi.Name).SetValue(objDestination, pi.GetValue(objSource));
            }

            return objDestination;
        }

        /// <summary>
        /// Serializes the.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>A string.</returns>
        public static string Serialize<T>(T source)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(source);
        }

        /// <summary>
        /// Deserializes the.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>A T.</returns>
        public static T Deserialize<T>(string source)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(source);
        }

        /// <summary>
        /// Serializes the to byte.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>An array of byte.</returns>
        public static byte[] SerializeToByte<T>(T obj)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                BinaryFormatter binSerializer = new BinaryFormatter();
                binSerializer.Serialize(memStream, obj);
                return memStream.ToArray();
            }
        }

        /// <summary>
        /// Isvalids the phoneno.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>A bool.</returns>
        public static bool IsvalidPhoneno(string s)
        {
            if (string.IsNullOrEmpty(s))
                return true;
            if (s.Contains("-"))
            {
                return Regex.IsMatch(s, @"^[\d-]{10,13}$");
            }
            else
            {
                return Regex.IsMatch(s, @"^[\d-]{10,12}$");
            }
        }

        /// <summary>
        /// Dates the to number.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>A string.</returns>
        public static string DateToNumber(DateTime? date)
        {
            try
            {
                if (date != null)
                    return Convert.ToDateTime(date).ToString("yyyyMMdd");
                else
                    return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Numbers the to date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>A DateTime? .</returns>
        public static DateTime? NumberToDate(string date)
        {
            DateTime? cDate = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(date) && date.Trim().Length >= 8)
                {
                    int year = Convert.ToInt32(date.Substring(0, 4));
                    int month = Convert.ToInt32(date.Substring(4, 2));
                    int day = Convert.ToInt32(date.Substring(6, 2));
                    cDate = new DateTime(year, month, day);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return cDate;
        }

        /// <summary>
        /// Gets the transaction module name.
        /// </summary>
        /// <param name="dataType">The data type.</param>
        /// <returns>A string.</returns>
        public static string GetTransactionModuleName(int dataType)
        {
            string ss = string.Empty;
            switch (dataType)
            {
                case 1:
                    ss = "Sale Challan";
                    break;

                case 2:
                    ss = "Sale";
                    break;

                case 3:
                    ss = "Sale Order";
                    break;

                case 4:
                    ss = "Brk/Exp Receive";
                    break;

                case 5:
                    ss = "Stock Issue";
                    break;

                case 6:
                    ss = "Credit Note";
                    break;

                case 7:
                    ss = "Brk / Exp Receive Challan";
                    break;

                case 8:
                    ss = "Sale Return Challan";
                    break;

                case 9:
                    ss = "Counter Sale";
                    break;

                case 10:
                    ss = "Not Sold but Billed";
                    break;

                case 11:
                    ss = "Sold but not Billed";
                    break;

                case 12:
                    ss = "Store Transfer";
                    break;

                case 13:
                    ss = "Branch Transfer";
                    break;

                case 14:
                    break;

                case 15:
                    break;

                case 16:
                    ss = "Purchase Challan";
                    break;

                case 17:
                    ss = "Purchase";
                    break;

                case 18:
                    ss = "Purchase Order";
                    break;

                case 19:
                    ss = "Stock Receive";
                    break;

                case 20:
                    ss = "Debit Note";
                    break;

                case 21:
                    ss = "Purchase Return Challan";
                    break;

                case 22:
                    ss = "Brk/Exp Issue";
                    break;

                case 23:
                    ss = "Brk/Exp Challan";
                    break;

                case 24:
                    break;

                case 25:
                    break;

                case 50:
                    break;

                case 51:
                case 52:
                    ss = "Payment";
                    break;

                case 53:
                case 54:
                    ss = "Receipt";
                    break;

                case 55:
                    ss = "Contra";
                    break;

                case 56:
                    ss = "Debit Note (A/C)";
                    break;

                case 57:
                    ss = "Credit Note (A/C)";
                    break;

                case 58:
                    ss = "Journal";
                    break;

                case 59:
                    ss = "Mode Of Payment";
                    break;

                case 60:
                    ss = "PD Cheque Cash Receive";
                    break;

                case 61:
                    ss = "Bill Adjustment";
                    break;

                case 62:
                    ss = "PDC Payment";
                    break;

                case 63:
                    ss = "PDC Receipt";
                    break;

                case 111:
                    ss = "Physical Stock";
                    break;

                default:
                    break;
            }

            return ss;
        }

        /// <summary>
        /// S the m s encode.
        /// </summary>
        /// <param name="str">The str.</param>
        /// <returns>A string.</returns>
        public static string SMSEncode(string str)
        {
            string result = string.Empty;
            result = HttpUtility.UrlEncode(str, System.Text.Encoding.GetEncoding("ISO-8859-1"));
            return result;
        }

        /// <summary>
        /// S the m s decode.
        /// </summary>
        /// <param name="str">The str.</param>
        /// <returns>A string.</returns>
        public static string SMSDecode(string str)
        {
            string result = string.Empty;
            result = HttpUtility.UrlDecode(str, System.Text.Encoding.GetEncoding("ISO-8859-1"));
            return result;
        }

        /// <summary>
        /// Concatenates the pdfs.
        /// </summary>
        /// <param name="documents">The documents.</param>
        /// <returns>An array of byte.</returns>
        public static byte[] ConcatenatePdfs(IEnumerable<byte[]> documents)
        {
            using (var memoryStream = new MemoryStream())
            {
                var pdfWriter = new PdfWriter(memoryStream);
                var pdfDocument = new PdfDocument(pdfWriter);
                PdfMerger merge = new PdfMerger(pdfDocument);
                PdfDocument pdoc;
                foreach (var doc in documents)
                {
                    pdoc = new PdfDocument(new PdfReader(new MemoryStream(doc)));
                    merge.Merge(pdoc, 1, pdoc.GetNumberOfPages());
                    pdoc.Close();
                }
                merge.Close();
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Excels the col alpha to num.
        /// </summary>
        /// <param name="ExcelColName">The excel col name.</param>
        /// <returns>An int.</returns>
        public static int ExcelColAlphaToNum(string ExcelColName)
        {
            int ret = 0;
            switch (ExcelColName.ToUpper())
            {
                case "A":
                    ret = 1; break;
                case "B":
                    ret = 2; break;
                case "C":
                    ret = 3; break;
                case "D":
                    ret = 4; break;
                case "E":
                    ret = 5; break;
                case "F":
                    ret = 6; break;
                case "G":
                    ret = 7; break;
                case "H":
                    ret = 8; break;
                case "I":
                    ret = 9; break;
                case "J":
                    ret = 10; break;
                case "K":
                    ret = 11; break;
                case "L":
                    ret = 12; break;
                case "M":
                    ret = 13; break;
                case "N":
                    ret = 14; break;
                case "O":
                    ret = 15; break;
                case "P":
                    ret = 16; break;
                case "Q":
                    ret = 17; break;
                case "R":
                    ret = 18; break;
                case "S":
                    ret = 19; break;
                case "T":
                    ret = 20; break;
                case "U":
                    ret = 21; break;
                case "V":
                    ret = 22; break;
                case "W":
                    ret = 23; break;
                case "X":
                    ret = 24; break;
                case "Y":
                    ret = 25; break;
                case "Z":
                    ret = 26; break;
            }

            return ret;
        }

        /// <summary>
        /// Read Full data from stream
        /// </summary>
        /// <param name="input">Stream</param>
        /// <returns>byte[]</returns>
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static MemoryStream ByteToMemoryStream(byte[] byteArray)
        {
            return new MemoryStream(byteArray)
            {
                Position = 0
            };
        }

        public static DateTime? ConvertTimeFromUtc(DateTime date, long countrylinkid = 0)
        {
            try
            {
                if (date != null && date.ToString() != "")
                    if (countrylinkid == 102989488)
                        return TimeZoneInfo.ConvertTime(date, TimeZoneInfo.FindSystemTimeZoneById("Arabic Standard Time"));
                    else
                        return TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                else
                    return date;
            }
            catch
            {
                return date;
            }
        }

        private static readonly char[] _chars = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        
        public static string ColumnName(int index)
        {
            index -= 1;

            int quotient = index / 26;
            if (quotient > 0)
                return ColumnName(quotient) + _chars[index % 26].ToString();
            else
                return _chars[index % 26].ToString();
        }

        public static string AddSpacesToText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            StringBuilder newText = new(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }

    /// <summary>
    /// The calendertype.
    /// </summary>
    public enum calendertype
    {
        Hizri,
        Gregorian,
        BikramSamwat
    }

    /// <summary>
    /// The culturetype.
    /// </summary>
    public enum culturetype
    {
        enUS
    }
}
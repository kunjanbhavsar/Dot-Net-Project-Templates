using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;

namespace SimformWebApplication.Core
{
    public static class CommonHelper
    {
        #region DateTime

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inputFormat"></param>
        /// <param name="outputFormat"></param>
        /// <returns></returns>
        public static string ConvertDateTimeFormat(string input, string inputFormat, string outputFormat)
        {
            System.Globalization.DateTimeFormatInfo MyDateTimeFormatInfo = new System.Globalization.DateTimeFormatInfo();
            MyDateTimeFormatInfo.ShortDatePattern = "MM/d/yyyy";

            DateTime dateTime = DateTime.ParseExact(input, inputFormat, MyDateTimeFormatInfo);
            return dateTime.ToString(outputFormat, MyDateTimeFormatInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inputFormat"></param>
        /// <returns></returns>
        public static DateTime ConvertDateTimeFormat(string input, string inputFormat)
        {
            System.Globalization.DateTimeFormatInfo MyDateTimeFormatInfo = new System.Globalization.DateTimeFormatInfo();
            MyDateTimeFormatInfo.ShortDatePattern = "MM/d/yyyy";

            DateTime dateTime = DateTime.ParseExact(input, inputFormat, MyDateTimeFormatInfo);
            return dateTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static int NoOfDaysBetweenTwoDate(DateTime StartDate, DateTime EndDate)
        {
            return (int)(EndDate - StartDate).TotalDays;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static int GetTotalMonth(DateTime date1, DateTime date2)
        {
            return ((date1.Year - date2.Year) * 12) + date1.Month - date2.Month;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GivenDate"></param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfCurrentWeek(DateTime GivenDate)
        {
            int delta = Convert.ToInt32(GivenDate.DayOfWeek);
            delta = delta == 0 ? delta + 7 : delta;
            DateTime monday = GivenDate.AddDays(1 - delta);
            return monday;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GivenDate"></param>
        /// <returns></returns>
        public static DateTime GetLastDateOfCurrentWeek(DateTime GivenDate)
        {
            int delta = Convert.ToInt32(GivenDate.DayOfWeek);
            delta = delta == 0 ? delta + 7 : delta;
            DateTime sunday = GivenDate.AddDays(7 - delta);
            return sunday;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GivenDate"></param>
        /// <returns></returns>
        public static DateTime GetFirstDateOfMonth(DateTime GivenDate)
        {
            int delta = Convert.ToInt32(GivenDate.DayOfWeek);
            delta = delta == 0 ? delta + 30 : delta;
            DateTime monday = GivenDate.AddDays(1 - delta);
            return monday;
        }

        #endregion

        #region Generate Rendom Number/String/Filename

        /// <summary>
        /// Returns an random interger number within a specified rage
        /// </summary>
        /// <param name="min">Minimum number</param>
        /// <param name="max">Maximum number</param>
        /// <returns>Result</returns>
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        /// Generate random digit code
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns>Result string</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
                str = String.Concat(str, random.Next(10).ToString());
            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomCode(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, length)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeLength"></param>
        /// <returns></returns>
        public static string GeneratePromoCode(int codeLength = 6)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[codeLength];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }

        /// <summary>
        /// Author : Dharmraj Mangukiya
        /// Usage : Get Unique FileName of File
        /// </summary>
        public static string GetUniqueFileName(string prefix)
        {
            try
            {
                return prefix + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Latitude/Longitude

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //:::                                                                         :::
        //:::  This routine calculates the distance between two points (given the     :::
        //:::  latitude/longitude of those points). It is being used to calculate     :::
        //:::  the distance between two locations using GeoDataSource(TM) products    :::
        //:::                                                                         :::
        //:::  Definitions:                                                           :::
        //:::    South latitudes are negative, east longitudes are positive           :::
        //:::                                                                         :::
        //:::  Passed to function:                                                    :::
        //:::    lat1, lon1 = Latitude and Longitude of point 1 (in decimal degrees)  :::
        //:::    lat2, lon2 = Latitude and Longitude of point 2 (in decimal degrees)  :::
        //:::    unit = the unit you desire for results                               :::
        //:::           where: 'M' is statute miles                                   :::
        //:::                  'K' is kilometers (default)                            :::
        //:::                  'N' is nautical miles                                  :::
        //:::                                                                         :::
        //:::  Worldwide cities and other features databases with latitude longitude  :::
        //:::  are available at http://www.geodatasource.com                          :::
        //:::                                                                         :::
        //:::  For enquiries, please contact sales@geodatasource.com                  :::
        //:::                                                                         :::
        //:::  Official Web site: http://www.geodatasource.com                        :::
        //:::                                                                         :::
        //:::           GeoDataSource.com (C) All Rights Reserved 2014                :::
        //:::                                                                         :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        public static double distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        public static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        public static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        #endregion

        #region IO Operations

        public static byte[] GetBytesFromFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                return System.IO.File.ReadAllBytes(filePath);
            }
            else
            {
                return new byte[0];
            }
        }

        public static string ConvertFileToBase64(string filePath)
        {
            try
            {
                var webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(filePath);
                return Convert.ToBase64String(imageBytes);
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static void CreateFileFromStream(Stream stream, string destPath)
        {
            using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }

        #endregion

        #region Common

        public static string Escape(string s)
        {
            string QUOTE = "\"";
            string ESCAPED_QUOTE = "\"\"";
            char[] CHARACTERS_THAT_MUST_BE_QUOTED = { ',', '"', '\n' };
            if (s.Contains(QUOTE))
                s = s.Replace(QUOTE, ESCAPED_QUOTE);

            if (s.IndexOfAny(CHARACTERS_THAT_MUST_BE_QUOTED) > -1)
                s = QUOTE + s + QUOTE;

            return s;
        }

        #endregion
    }
}
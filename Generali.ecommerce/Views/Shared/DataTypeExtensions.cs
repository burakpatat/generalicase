
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Xml;

public static class DataTypeExtensions
{


    public static string FirstCharToLowerCase(this string str)
    {
        if (string.IsNullOrEmpty(str) || char.IsLower(str[0]))
            return str;

        return char.ToLowerInvariant(str[0]) + str.Substring(1);
    }
   
    public static string FirstCharToUpperCase(this string str)
    {
        if (string.IsNullOrEmpty(str) || char.IsLower(str[0]))
            return str;

        return char.ToUpperInvariant(str[0]) + str.ToLowerInvariant().Substring(1);
    }
    public static string intDictionaryToJson(this Dictionary<int, List<int>> dict)
    {
        var entries = dict.Select(d =>
            string.Format("\"{0}\": [{1}]", d.Key, string.Join(",", d.Value)));
        return "{" + string.Join(",", entries) + "}";
    }

    public static string stringDictionaryToJson(this Dictionary<string, string> dict)
    {
        var entries = dict.Select(d =>
            string.Format("\"{0}\": [{1}]", d.Key, string.Join(",", d.Value)));
        return "{" + string.Join(",", entries) + "}";
    }
    public static string objectDictionaryToJson(this Dictionary<string, object> dict)
    {
        var entries = dict.Select(d =>
            string.Format("\"{0}\": {1}", d.Key, string.Join(",", d.Value)));
        return "{" + string.Join(",", entries) + "}";
    }
    public static string ToTitleCase(this string str)
    {
        var sb = new StringBuilder(str.Length);
        var flag = true;

        for (int i = 0; i < str.Length; i++)
        {
            var c = str[i];
            if (char.IsLetterOrDigit(c))
            {
                sb.Append(flag ? char.ToUpperInvariant(c) : c);
                flag = false;
            }
            else
            {
                flag = true;
            }
        }

        return sb.ToString();
    }
    public static Decimal ToDecimal(this object value)
    {
        try
        {
            if (value != null)
                return Convert.ToDecimal(value);
            else
                return 0;

        }
        catch
        {
            return 0;
        }
    }




    public static string ToSqlDate(this DateTime value)
    {
        return value.ToString("yyyy-MM-dd HH:mm:ss.fff");
    }

    public static string ShowMatch(this string text, string expr)
    {
        MatchCollection mc = Regex.Matches(text, expr);
        string result = "";
        foreach (Match m in mc)
        {
            result += m.ToString() + "\n";
        }
        return result;
    }
    public static bool ToBool(this object value)
    {
        if (value == null)
        {
            return false;
        }
        if (value.GetType() == typeof(int))
        {
            try
            {
                if (value.ToInt32() == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return new Boolean();
            }
        }
        else
        {
            try
            {
                return Boolean.Parse(value.ToString());
            }
            catch (Exception)
            {
                return new Boolean();
            }
        }

    }

    public static string ToChecked(this bool value)
    {
        if (value == null)
        {
            return " value='false' ";
        }
        if (value.GetType() == typeof(int))
        {
            try
            {
                if (value.ToInt32() == 1)
                {
                    return "checked  value='true'";
                }
                else
                {
                    return " value='false' ";
                }

            }
            catch (Exception)
            {
                return "";
            }
        }
        else
        {
            try
            {
                if (value)
                {
                    return "checked value='true'";
                }
                else
                {
                    return " value='false' ";
                }

            }
            catch (Exception)
            {
                return "";
            }
        }

    }

    public static byte ToByte(this string value)
    {
        try
        {
            return Byte.Parse(value);
        }
        catch (Exception)
        {
            return new Byte();
        }
    }


    public static Char ToChar(this string value)
    {
        try
        {
            return Convert.ToChar(value);
        }
        catch (Exception)
        {
            return new Char();
        }
    }

    public static Decimal ToDecimal(this string value)
    {
        try
        {
            return Decimal.Parse(value);
        }
        catch (Exception)
        {
            return new Decimal();
        }
    }

    public static Double ToDouble(this string value)
    {
        try
        {
            return Double.Parse(value);
        }
        catch (Exception)
        {
            return new Double();
        }
    }

    public static float ToFloat(this string value)
    {
        try
        {
            return float.Parse(value);
        }
        catch (Exception)
        {
            return new float();
        }
    }

    public static Int16 ToInt16(this object value)
    {
        try
        {
            if (value != null)
                return Int16.Parse(value.ToString());
            else
                return new Int16();

        }
        catch (Exception)
        {
            return new Int16();
        }
    }

    public static Int32 ToInt32(this object value)
    {
        try
        {
            if (value != null)
                return Int32.Parse(value.ToString());
            else
                return new Int32();
        }
        catch (Exception)
        {
            return new Int32();
        }
    }

    public static DateTime ToDateTime(this object value)
    {

        if (value.GetType() == typeof(string))
        {
            return Convert.ToDateTime(Convert.ToDateTime(value).ToString("dd/MM/yyyy", Thread.CurrentThread.CurrentUICulture));
        }
        else
        {
            return Convert.ToDateTime(Convert.ToDateTime(value).ToString("dd/MM/yyyy", Thread.CurrentThread.CurrentUICulture));
        }


    }
    public static string ToMaskedDateTime(this object value)
    {
        try
        {
            if (value.GetType() == typeof(string))
            {

                return Convert.ToDateTime(value).ToString("dd/MM/yyyy", Thread.CurrentThread.CurrentUICulture);
            }
            else if (value.GetType() == typeof(DateTime))
            {
                return Convert.ToDateTime(value).ToString("dd/MM/yyyy", Thread.CurrentThread.CurrentUICulture);
            }
            else
            {
                return new DateTime().ToShortDateString();
            }
        }
        catch (Exception)
        {
            return new DateTime().ToShortDateString();
        }
    }
    public static string ToImage(this object value)
    {
        if (value != null)
        {


            if (value.ToString().Contains(".mp4") || value.ToString().Contains(".flv") || value.ToString().Contains(".webm") || value.ToString().Contains(".avi") || value.ToString().Contains(".mov") || value.ToString().Contains(".3gp"))
            {
                try
                {
                    return "<video src='" + value + "' controls='controls' height='100' />";
                }
                catch (Exception)
                {
                    return "<video src='" + value + "' controls='controls' height='100' />";
                }
            }
            else
            {
                try
                {
                    return "<img src='" + value + "' height='100'/>";
                }
                catch (Exception)
                {
                    return "<img src='" + value + "' height='100'/>";
                }
            }
        }
        else
        {
            return "";
        }
    }
    public static string ToImage(this object value, int width)
    {
        if (width == 0)
        {
            try
            {
                return "<img src='" + value + "' />";
            }
            catch (Exception)
            {
                return "<img src='" + value + "' />";
            }
        }
        else
        {
            try
            {
                return "<img src='" + value + "' height='" + width + "'/>";
            }
            catch (Exception)
            {
                return "<img src='" + value + "' height='" + width + "'/>";
            }
        }

    }
    public static string ToImageNoWidth(this object value)
    {

        try
        {
            return "<img src='" + value + "' />";
        }
        catch (Exception)
        {
            return "<img src='" + value + "' />";
        }



    }
    public static string ToShortDateTime(this object value, string format = "yyyy-MM-dd")
    {
        try

        {
            if (value.GetType() == typeof(string))
            {
                if (value != null && value.ToString().Contains('.'))
                {
                    var day = value.ToString().Split('.')[0];
                    var month = value.ToString().Split('.')[1];
                    var year = value.ToString().Split('.')[2];
                    format = year + "-" + month + "-" + day;
                    return format;
                }
                else
                {
                    return new DateTime().ToShortDateString();
                }
            }
            else if (value.GetType() == typeof(DateTime))
            {
                return Convert.ToDateTime(value).ToString(format, Thread.CurrentThread.CurrentUICulture);
            }
            else
            {
                return new DateTime().ToShortDateString();
            }



        }
        catch (Exception)
        {
            return new DateTime().ToShortDateString();
        }
    }
    public static Int64 ToInt64(this object value)
    {
        try
        {
            if (value == null)
            {
                return Int64.MinValue;
            }
            return Int64.Parse(value.ToString());
        }
        catch (Exception)
        {
            return new Int64();
        }
    }

    public static string ToMoneyString(this decimal value)
    {
        try
        {
            return value.ToString("#.##");
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }


    public static string ToMoney(this float value)
    {
        try
        {
            return value.ToString("#.##");
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
    public static string ToMoneyString(this float value)
    {
        try
        {
            var price = value;
            string moneyStr = String.Format("{0,0:C2}", price).Replace("¤", "");
            return moneyStr;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    public static string ToMoneyString(this string value)
    {
        try
        {
            var price = value.ToFloat();
            string moneyStr = String.Format("{0,0:C2}", price).Replace("¤", "");
            return moneyStr;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
    public static string ToMoneyString(this double value)
    {
        try
        {
            var price = value;
            string moneyStr = String.Format("{0,0:C2}", price).Replace("¤", "");
            return moneyStr;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    public static string ToPublishedStatu(this object value)
    {
        var yayindaDegil = "<span class='label label-danger'>Yayında Değil</span>";
        var yayinda = "<span class='label label-success'>Yayında</span>";
        try
        {
            if (value.GetType() == typeof(int))
            {
                if (value.ToString().ToInt32() == 1)
                {
                    return yayinda;
                }
                else
                {
                    return yayindaDegil;
                }
            }
            if (value.GetType() == typeof(bool))
            {
                if (Convert.ToBoolean(value) == true)
                {
                    return yayinda;
                }
                else
                {
                    return yayindaDegil;
                }
            }
            return yayindaDegil;
        }
        catch (Exception)
        {
            return yayindaDegil;
        }
    }


    public static string ToYesNo(this object value)
    {
        const string hayir = "<span class='btn btn-outline-danger'>No</span>";
        const string evet = "<span class='btn btn-outline-success'>Yes</span>";

        try
        {
            if (value.GetType() == typeof(int))
            {
                if (value.ToString().ToInt32() == 1)
                {
                    return evet;
                }
                if (value.ToString().ToInt32() == 0)
                {
                    return hayir;
                }

            }
            else if (value.GetType() == typeof(bool))
            {
                if (Convert.ToBoolean(value) == true)
                {
                    return evet;
                }
                else
                {
                    return hayir;
                }

            }

            return hayir;
        }
        catch (Exception)
        {
            return hayir;
        }
    }




    public static string ToDbNvarcharType(this object value, int? maxLength)
    {
        try
        {
            if (value.ToString().Contains("varchar"))
            {
                if (maxLength == -1)
                {
                    return value + " (max)";
                }
                else
                {
                    return value + " (" + maxLength / 2 + ")";
                }
            }
            else
            {
                return value.ToString();
            }

        }
        catch (Exception)
        {
            return value + " ";
        }
    }


    public static string ToDbNvarcharType(this object value, int? maxLength, bool? isNullable)
    {
        try
        {
            if (value.ToString().ToLowerInvariant().Contains("varchar"))
            {
                if (maxLength == -1)
                {
                    return value + " (max)";
                }
                else
                {
                    return value + " (" + maxLength / 2 + ")";
                }
            }
            else
            {
                return value.ToString();
            }

        }
        catch (Exception)
        {
            return value + " (max)";
        }
    }
    public static string ToFormat(this string value, params string[] parameters)
    {
        return string.Format(value, parameters);
    }
}
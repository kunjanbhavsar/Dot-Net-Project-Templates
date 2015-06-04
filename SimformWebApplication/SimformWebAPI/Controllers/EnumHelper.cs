using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SimformWebApplication.Core
{
    /*
    TestEnum testEnum = (TestEnum)Enum.Parse(typeof(TestEnum), "Super");
    We had to define the enum type THREE (count them) THREE times, in a single row. Blech! With this helper, this statement becomes much cleaner:
    var testEnum = Enum<TestEnum>.Parse("Super");
    // Fails parsing, enum value = null
    var enumTest = Enum<TestEnum>.TryParse("WrongValue");
    // Get an IEnumerable of the Enums
    var list = Enum<TestEnum>.ToEnumerable();
    // So this...
    string name = Enum<TestEnum>.GetName(TestEnum.Super);
    // Instead of this...
    string name = Enum.GetName(typeof(TestEnum), TestEnum.Super); */
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Enum<T>
        where T : struct
    {
        public static T Parse(string name)
        {
            return Parse(name, false);
        }

        public static T Parse(string name, bool ignoreCase)
        {
            return (T)Enum.Parse(typeof(T), name, ignoreCase);
        }

        public static T? TryParse(string name)
        {
            return TryParse(name, false);
        }

        public static T? TryParse(string name, bool ignoreCase)
        {
            T value;
            if (!string.IsNullOrEmpty(name) && TryParse(name, out value, ignoreCase))
                return value;
            return null;
        }

        public static bool TryParse(string name, out T value)
        {
            return TryParse(name, out value, false);
        }

        public static bool TryParse(string name, out T value, bool ignoreCase)
        {
            try
            {
                value = Parse(name, ignoreCase);
                return true;
            }
            catch (ArgumentException)
            {
                value = default(T);
                return false;
            }
        }

        public static IEnumerable<T> ToEnumerable()
        {
            foreach (var value in Enum.GetValues(typeof(T)))
                yield return (T)value;
        }

        public static IEnumerable<object> GetValues()
        {
            foreach (var value in Enum.GetValues(typeof(T)))
                yield return value;
        }

        public static IDictionary<object, string> GetDictionary()
        {
            Dictionary<object, string> dictionary = new Dictionary<object, string>();
            foreach (var value in GetValues())
                dictionary.Add(
                    Convert.ChangeType(value, Enum.GetUnderlyingType(typeof(T))),
                    GetName(value));

            return dictionary;
        }

        #region Strongly-Typed Enum Extenders

        public static string Format(object value, string format)
        {
            return Enum.Format(typeof(T), value, format);
        }

        public static string GetName(object value)
        {
            return Enum.GetName(typeof(T), value);
        }

        public static IEnumerable<string> GetNames()
        {
            return Enum.GetNames(typeof(T));
        }

        public static Type GetUnderlyingType()
        {
            return Enum.GetUnderlyingType(typeof(T));
        }

        public static bool IsDefined(object value)
        {
            return Enum.IsDefined(typeof(T), value);
        }

        public static T ToObject(object value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static T ToObject(byte value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static T ToObject(sbyte value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static T ToObject(int value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static T ToObject(uint value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static T ToObject(long value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static T ToObject(ulong value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static T ToObject(short value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }
        public static T ToObject(ushort value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }
        public static IList<string> GetNames(Enum value)
        {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }
        public static IList<string> GetDisplayValues(Enum value)
        {
            return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }
        public static string GetDisplayValue(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }
        #endregion
    }
}
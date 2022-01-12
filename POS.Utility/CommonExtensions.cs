using System;
using System.Collections.Generic;
using System.Linq;

namespace POS.Utility
{
    public static class CommonExtensions
    {
        public static string SetValueForNullOrEmpty(this string str1,string str2)
        {
            return string.IsNullOrEmpty(str1) ? str2 : str1;
        }

        public static bool IsEqualTo(this string str1,string str2)
        {
            return str1.Equals(str2, StringComparison.OrdinalIgnoreCase);
        }

        public static object DefaultDBNullValue<T>(this T obj) where T: IEquatable<string>
        {

            return typeof(T).Name.ToLower() switch
            {
                "string"=>!string.IsNullOrEmpty((string)(object)obj)?(string)(object)obj:(object)DBNull.Value,
                _ => (object)obj
            };
            
        }

        public static IEnumerable<T> Split<T>(this string str,string seperator=";") where T : IEquatable<string>
        {
            return typeof(T).Name.ToLower() switch
            {
                "string"=> !string.IsNullOrEmpty(str) ? (IEnumerable<T>)str.Split(seperator).Where(x=>!string.IsNullOrEmpty(x)).ToList<string>():Enumerable.Empty<T>(),
                _ => !string.IsNullOrEmpty(str) ? (IEnumerable<T>)str.Split(seperator).ToList<string>() : Enumerable.Empty<T>()
            };
        }

     

        
    }
}

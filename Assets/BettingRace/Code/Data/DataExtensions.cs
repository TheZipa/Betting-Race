using System.Globalization;
using UnityEngine;

namespace BettingRace.Code.Data
{
    public static class DataExtensions
    {
        private static CultureInfo _culture = new CultureInfo("ru-RU");
        
        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) =>
            JsonUtility.ToJson(obj);

        public static string ToCultureString(this int number) =>
            number.ToString("#,#", _culture);
    }
}
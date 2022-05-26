using System.Globalization;
using UnityEngine;

namespace BettingRace.Code.Data
{
    public static class DataExtensions
    {
        private static readonly CultureInfo Culture = new CultureInfo("ru-RU");

        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) =>
            JsonUtility.ToJson(obj);

        public static string ToCultureString(this int number) =>
            number.ToString("#,#", Culture);

        public static void SetPositionX(this GameObject gameObject, float x)
        {
            Vector3 tribunesPosition = gameObject.transform.position;
            tribunesPosition.Set(x, tribunesPosition.y, tribunesPosition.z);
            gameObject.transform.position = tribunesPosition;
        }
    }
}
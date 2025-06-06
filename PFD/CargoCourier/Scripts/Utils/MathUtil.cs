using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;

namespace Utilities
{
    public static class MathUtil
    {
        private static float secPerYear;

        public static void SetSecPerYear(float value)
        {
            //Debug.LogError("SET");
            secPerYear = value;
        }

        public static float YearsToSec(float years)
        {
            //Debug.LogError("");
            return years * secPerYear;
        }
        public static float SecToYears(float value)
        {
            //Debug.LogError("");
            return value / secPerYear;
        }

        public static string FloatToString(float value, int decim)
        {
            string _string = "F" + decim;
            return value.ToString(_string);
        }

        public static float SecToMos(float value)
        {
            float _years = value / secPerYear;
            float _mos = _years * 12;
            return _mos;
        }

        public static string UlongToString(ulong cash, string prefix = "")
        {
            string[] suffixes = { "", "k", "m", "b" };
            int suffixIndex;
            if (cash == 0)
                suffixIndex = 0;    // log10 of 0 is not valid
            else
                suffixIndex = (int)(Mathf.Log10(cash) / 3); // get number of digits and divide by 3
            var dividor = Mathf.Pow(10, suffixIndex * 3);  // actual number we print
            var text = "";
            int digits = cash.ToString().Length;

            //Debug.LogError(digits);

            if (digits < 4)
            {
                text = prefix + (cash / dividor).ToString() + suffixes[suffixIndex];
            }
            else if (digits >= 4 && digits < 7)
            {
                text = prefix + (cash / dividor).ToString("F2") + suffixes[suffixIndex];
            }
            else
            {
                text = prefix + (cash / dividor).ToString("F3") + suffixes[suffixIndex];
            }
            return text;
        }

        public static ulong IntToUlong(int value)
        {
            return Convert.ToUInt64(value);
        }

        public static float HoursToSeconds(float value)
        {
            return value * 3600;
        }

        public static string SecondsToHours(float value)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(value);
            string timerFormatted;
            if (timeSpan.Days == 0)
            {
                timerFormatted = string.Format("{0:D1}h {1:D1}m {2:D1}s", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            }
            else timerFormatted = string.Format("{0:D1}d {1:D1}h {2:D1}m {3:D1}s", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            return timerFormatted;
        }

        public static string SecondsToMinutes(float value)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(value);
            string timerFormatted;
            string _minutes = "min";
            string _seconds = "s";

            if (timeSpan.Minutes == 0)
            {
                timerFormatted = string.Format("{0:D1}" + _seconds, timeSpan.Seconds);
            }
            else if( timeSpan.Seconds == 0)
            {
                timerFormatted = string.Format("{0:D1}" + _minutes, timeSpan.Minutes);
            }
            else timerFormatted = string.Format("{0:D1}" + _minutes + "{1:D1}" + _seconds, timeSpan.Minutes, timeSpan.Seconds);
            return timerFormatted;
        }










        private static readonly System.Random _random;

        static MathUtil()
        {
            _random = new System.Random();
        }

        public static float GetAngle(Vector3 start, Vector3 end)
        {
            return Mathf.Atan2(end.z - start.z, end.x - start.x) * Mathf.Rad2Deg;
        }

        public static long Lerp(double a, double b, float t)
        {
            return (long)(a + (b - a) * Mathf.Clamp01(t));
        }

        public static int Sign(double value)
        {
            return (value >= 0) ? 1 : -1;
        }

        public static int RandomSystem(int min, int max)
        {
            return _random.Next(min, max + 1);
        }

        public static float RandomSystem(float min, float max)
        {
            return (float)_random.NextDouble() * (max + .0001f - min) + min;
        }

        public static int Random(int min, int max)
        {
            return UnityEngine.Random.Range(min, max + 1);
        }

        public static float Random(float min, float max)
        {
            return UnityEngine.Random.Range(min, max + .0001f);
        }

        public static string IntToHex(uint crc)
        {
            return string.Format("{0:X}", crc);
        }

        public static uint HexToInt(string crc)
        {
            return uint.Parse(crc, System.Globalization.NumberStyles.AllowHexSpecifier);
        }

        public static bool RandomBool
        {
            get
            {
                return UnityEngine.Random.value > 0.5f;
            }
        }

        public static int RandomSign
        {
            get
            {
                return RandomBool ? 1 : -1;
            }
        }

        public static Vector2 RotateAround(Vector2 center, Vector2 point, float angleInRadians)
        {
            angleInRadians *= Mathf.Deg2Rad;
            float cosTheta = Mathf.Cos(angleInRadians);
            float sinTheta = Mathf.Sin(angleInRadians);
            return new Vector2
            {
                x = (cosTheta * (point.x - center.x) - sinTheta * (point.y - center.y)),
                y = (sinTheta * (point.x - center.x) + cosTheta * (point.y - center.y))
            };
        }
    }
}
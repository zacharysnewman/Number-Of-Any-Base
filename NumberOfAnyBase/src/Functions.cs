using System;
using System.Linq;
using System.Collections.Generic;

namespace NumberOfAnyBase
{
    public class Functions
    {
        /// <summary>
        /// Accepts any string in the format <base>:<number> (i.e. 10:789 or 10:-789)
        /// </summary>
        /// <param name="numberStr"></param>
        /// <param name="baseValue"></param>
        /// <param name="isNegative"></param>
        /// <param name="valueStr"></param>
        /// <returns></returns>
        public static bool IsValidBaseAndNumber(string numberStr, out int baseValue, out bool isNegative, out string valueStr)
        {
            var baseAndNumber = numberStr.Split(Number.DELIMITER);
            baseValue = -1;
            isNegative = false;
            valueStr = null;

            if (baseAndNumber.Length == 2)
            {
                var baseStr = baseAndNumber[0];
                valueStr = baseAndNumber[1];
                isNegative = valueStr.Contains("-");
                valueStr = valueStr.Replace("-", "");
                if (IsValidBaseValueString(baseStr))
                {
                    baseValue = int.Parse(baseStr);
                    return IsValidValueString(valueStr, GetBaseValues(baseValue));
                }
            }
            return false;
        }

        public static string GetBaseValues(int baseNumber) => Number.ALL_NUMBER_VALUES.Substring(0, baseNumber);
        public static bool IsValidBaseValueString(string baseStr) => baseStr.All((c) => "0123456789".Contains(c));
        public static bool IsValidValueString(string numberStr, string baseValues) => numberStr.All(c => baseValues.Contains(c));

        public static List<int> ClearTrailingZeros(List<int> numberDigits)
        {
            var newDigits = new List<int>(numberDigits);

            while (newDigits.Count > 0 && newDigits[newDigits.Count - 1] == 0)
            {
                newDigits.RemoveAt(newDigits.Count - 1);
            }
            return newDigits;
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;

namespace NumberOfAnyBase
{
    public class Functions
    {
        public static bool IsValidBaseAndNumber(string numberStr, out int baseValue, out string valueStr)
        {
            var baseAndNumber = numberStr.Split(Number.DELIMITER);
            baseValue = -1;
            valueStr = null;

            if (baseAndNumber.Length == 2)
            {
                var baseStr = baseAndNumber[0];
                valueStr = baseAndNumber[1];
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

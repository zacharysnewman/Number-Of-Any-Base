using System;
using System.Collections.Generic;
using System.Linq;
using static NumberOfAnyBase.Functions;

namespace NumberOfAnyBase
{
    // Converting between different number bases and doing math with different bases (up to Base-62 before running out of characters) - Zack Newman
    public partial struct Number : IEquatable<Number>
    {
        public const string ALL_NUMBER_VALUES = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        public const char DELIMITER = '|';

        public static int DefaultBaseForStringParsing { get; set; } = 10;
        public static bool TransformIntToDefaultBase { get; set; } = false;

        public static Number MinValue { get => int.MinValue; }
        public static Number MaxValue { get => int.MaxValue; }

        public int BaseValue { get; private set; }
        public bool IsNegative { get; private set; }
        public bool IsPositive { get => !IsNegative; }
        public List<int> Digits { get; private set; }

        public Number(int baseValue, bool isNegative, List<int> digits)
        {
            this.BaseValue = baseValue;
            this.IsNegative = isNegative;
            this.Digits = new List<int>(digits);
        }

        public Number(string number)
        {
            this = Number.Parse(number);
        }

        public Number(int number)
        {
            if (Number.TransformIntToDefaultBase)
            {
                this = new Number(number.ToString());
            }
            else
            {
                this = new Number($"10{Number.DELIMITER}{number}");
            }
        }

        public override string ToString() => ToString(includeBase: false);
        public string ToString(bool includeBase)
        {
            var prefix = includeBase ? $"{this.BaseValue}{Number.DELIMITER}" : "";
            return $"{prefix}{(this.IsNegative ? "-" : "")}{string.Join("", this.Digits.ToArray().Reverse().Select((i) => Number.ALL_NUMBER_VALUES[i]))}";
        }

        public static Number Parse(string numberStr)
        {
            var baseAndNumber = numberStr.Split(Number.DELIMITER);
            int baseValue = -1;
            bool isNegative = false;
            string valueStr = null;

            if (baseAndNumber.Length == 2)
            {
                var baseStr = baseAndNumber[0];
                valueStr = baseAndNumber[1];
                isNegative = valueStr.Contains("-");
                valueStr = valueStr.Replace("-", "");
                if (IsValidBaseValueString(baseStr))
                {
                    baseValue = int.Parse(baseStr);
                    if (!IsValidValueString(valueStr, GetBaseValues(baseValue)))
                    {
                        throw new Exception($"Invalid Number. Not a valid value: {valueStr}, base: {baseValue}");
                    }
                }
                else
                {
                    throw new Exception($"Invalid Number. Not a valid base: {baseStr}");
                }
            }
            else
            {
                baseValue = Number.DefaultBaseForStringParsing;
                valueStr = baseAndNumber[0];
                isNegative = valueStr.Contains("-");
                valueStr = valueStr.Replace("-", "");

                if (!IsValidValueString(valueStr, GetBaseValues(baseValue)))
                {
                    throw new Exception($"Invalid Number. Not a valid base-{baseValue} value: '{valueStr}'. Did you mean to include an explicit base and value separated by the delimiter? '{Number.DELIMITER}'");
                }
            }

            return new Number(baseValue, isNegative, valueStr.Select(ToDigits).Reverse().ToList());
        }

        private static Func<char, int> ToDigits = (s) => Number.ALL_NUMBER_VALUES.IndexOf(s.ToString());
    }
}

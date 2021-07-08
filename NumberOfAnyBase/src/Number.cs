using System;
using System.Collections.Generic;
using System.Linq;
using static NumberOfAnyBase.Functions;

namespace NumberOfAnyBase
{
    // Converting between different number bases and doing math with different bases (up to Base-62 before running out of characters) - Zack Newman
    public partial struct Number : IEquatable<Number>
    {
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
            if (Number.OptionUseDecimalBaseForIntLiterals)
            {
                this = new Number($"10{Number.OptionDelimiter}{number}");
            }
            else
            {
                this = new Number(number.ToString());
            }
        }

        public override string ToString() => ToString(includeBase: false);
        public string ToString(bool includeBase)
        {
            var prefix = includeBase ? $"{this.BaseValue}{Number.OptionDelimiter}" : "";
            return $"{prefix}{(this.IsNegative ? "-" : "")}{string.Join("", this.Digits.ToArray().Reverse().Select((i) => Number.OptionAllNumberValues[i]))}";
        }

        public const string Default_OptionAllNumberValues = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        public const char Default_OptionDelimiter = '|';
        public const int Default_OptionDefaultBaseForStringParsing = 10;
        public const bool Default_OptionUseDecimalBaseForIntLiterals = true;

        public static string OptionAllNumberValues { get; set; } = Number.Default_OptionAllNumberValues;
        public static char OptionDelimiter { get; set; } = Number.Default_OptionDelimiter;
        public static int OptionDefaultBaseForStringParsing { get; set; } = Number.Default_OptionDefaultBaseForStringParsing;
        public static bool OptionUseDecimalBaseForIntLiterals { get; set; } = Number.Default_OptionUseDecimalBaseForIntLiterals;

        public static void ResetOptionsToDefaults()
        {
            Number.OptionAllNumberValues = Number.Default_OptionAllNumberValues;
            Number.OptionDelimiter = Number.Default_OptionDelimiter;
            Number.OptionDefaultBaseForStringParsing = Number.Default_OptionDefaultBaseForStringParsing;
            Number.OptionUseDecimalBaseForIntLiterals = Number.Default_OptionUseDecimalBaseForIntLiterals;
        }

        public static Number MinValue { get => int.MinValue; }
        public static Number MaxValue { get => int.MaxValue; }

        public static Number Parse(string numberStr)
        {
            var baseAndNumber = numberStr.Split(Number.OptionDelimiter);
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
                baseValue = Number.OptionDefaultBaseForStringParsing;
                valueStr = baseAndNumber[0];
                isNegative = valueStr.Contains("-");
                valueStr = valueStr.Replace("-", "");

                if (!IsValidValueString(valueStr, GetBaseValues(baseValue)))
                {
                    throw new Exception($"Invalid Number. Not a valid base-{baseValue} value: '{valueStr}'. Did you mean to include an explicit base and value separated by the delimiter? '{Number.OptionDelimiter}'");
                }
            }

            return new Number(baseValue, isNegative, valueStr.Select(ToDigits).Reverse().ToList());
        }

        private static Func<char, int> ToDigits = (s) => Number.OptionAllNumberValues.IndexOf(s.ToString());
    }
}

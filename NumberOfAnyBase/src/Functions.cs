using System;
using System.Linq;
using System.Collections.Generic;

namespace NumberOfAnyBase
{
    public class Functions
    {
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

        public static List<int> AddListsOfDigits(List<int> aDigits, List<int> bDigits, int baseValue)
        {
            bool doCarry = false;
            var sumDigits = new List<int>();
            for (int i = 0; i < aDigits.Count || i < bDigits.Count; i++)
            {
                var result = AddDigits(i, aDigits, bDigits) + (doCarry ? 1 : 0);

                doCarry = result >= baseValue;
                sumDigits.Add(result % baseValue);
            }

            if (doCarry)
            {
                sumDigits.Add(1);
            }

            return sumDigits;
        }

        //private enum ListCounts
        //{
        //    BothMoreDigits,
        //    ANoMoreDigits,
        //    BNoMoreDigits
        //}
        //private static ListCounts GetListCounts(int i, int aDigitsCount, int bDigitsCount)
        //{
        //    return i < aDigitsCount && i < bDigitsCount ? ListCounts.BothMoreDigits :
        //        i >= aDigitsCount ? ListCounts.ANoMoreDigits :
        //        i >= bDigitsCount ? ListCounts.BNoMoreDigits :
        //        throw new Exception("AddDigits Exception: Passed invalid index, neither have digits.");
        //}
        public static int AddDigits(int i, List<int> aDigits, List<int> bDigits)
        {
            var result = 0;

            result +=
                i < aDigits.Count && i < bDigits.Count ? aDigits[i] + bDigits[i] :
                i >= aDigits.Count ? bDigits[i] :
                i >= bDigits.Count ? aDigits[i] :
                throw new Exception("AddDigits Exception: Passed invalid index, neither have digits.");

            return result;
        }

        public enum DoCarryBorrow
        {
            Borrow = -1,
            Neither = 0,
            Carry = 1
        }

        public static List<int> SubtractListsOfDigits(List<int> aDigits, List<int> bDigits, int baseValue)
        {
            //  Subtract
            //  If result is less than 0
            //      Borrow from next digit
            // 100
            // 10
            // 2
            // 1
            // 0
            // 1
            // 2
            // 10
            // 100

            bool doBorrow = false;
            var sumDigits = new List<int>();
            for (int i = 0; i < aDigits.Count || i < bDigits.Count; i++)
            {
                var result = SubtractDigits(i, aDigits, bDigits) + (doBorrow ? -1 : 0);

                doBorrow = result >= baseValue;
                sumDigits.Add(result % baseValue);
            }

            if (doBorrow)
            {
                sumDigits.Add(1);
            }

            return sumDigits;
        }

        public static int SubtractDigits(int i, List<int> aDigits, List<int> bDigits)
        {
            var result = 0;

            result +=
                i < aDigits.Count && i < bDigits.Count ? aDigits[i] - bDigits[i] :
                i >= aDigits.Count ? bDigits[i] :
                i >= bDigits.Count ? aDigits[i] :
                throw new Exception("SubtractDigits Exception: Passed invalid index, neither have digits.");

            return result;
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T tempB = b;
            b = a;
            a = tempB;
        }

        // Conversions //
        public static string DecimalToAny(int i, int toBase)
        {
            IEnumerable<int> acc = new int[0];
            while (i != 0)
            {
                i = FullDivide(i, toBase, out int remainder);
                acc = acc.Prepend(remainder);
            }
            var charCol = acc.Select((x) => CharFromIndex(x));
            return string.Join("", charCol);
        }

        public static int AnyToDecimal(string str, int b_ase)
        {
            int len = str.Length;
            int power = 1; // power base 
            int num = 0; // result 
            int i;

            for (i = len - 1; i >= 0; i--)
            {
                if (IndexFromChar(str[i]) >= b_ase)
                {
                    Console.WriteLine("Invalid Number");
                    return -1;
                }

                num += IndexFromChar(str[i]) * power;
                power = power * b_ase;
            }

            return num;
        }

        public static string AnyToAny(string val, int fromBase, int toBase) => DecimalToAny(AnyToDecimal(val, fromBase), toBase);

        private static int FullDivide(int a, int b, out int remainder)
        {
            remainder = a % b;
            return a / b;
        }

        private static int IndexFromChar(char c) => Number.ALL_NUMBER_VALUES.IndexOf(c);
        private static char CharFromIndex(int index) => Number.ALL_NUMBER_VALUES[index];
    }
}

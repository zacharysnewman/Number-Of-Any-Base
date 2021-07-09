using System;
using System.Linq;
using System.Collections.Generic;

namespace NumberOfAnyBase
{
    public class Functions
    {
        public static Number Abs(Number number) => new Number(number.BaseValue, isNegative: false, number.Digits);

        public static string GetBaseValues(int baseNumber) => Number.OptionAllNumberValues.Substring(0, baseNumber);
        public static bool IsValidBaseValueString(string baseStr) => baseStr.All((c) => "0123456789".Contains(c));
        public static bool IsValidValueString(string numberStr, string baseValues) => numberStr.All(c => baseValues.Contains(c));

        public static Number IntToNumber(int i, int toBase)
        {
            var isNegative = i.ToString().Contains("-");
            var sign = isNegative ? "-" : "";
            i = Math.Abs(i);
            IEnumerable<int> acc = new int[0];
            while (i != 0)
            {
                i = FullDivide(i, toBase, out int remainder);
                acc = acc.Prepend(remainder);
            }
            var charCol = acc.Select((x) => AllNumbersCharFromIndex(x));
            charCol = charCol.Count() > 0 ? charCol : new List<char>() { '0' };
            return new Number($"{toBase}{Number.OptionDelimiter}{sign}{string.Join("", charCol)}");
        }

        public static int NumberToInt(Number number)
        {
            string numberStr = (string)Abs(number);
            int len = numberStr.Length;
            int power = 1; // power base 
            int num = 0; // result 
            int i;

            for (i = len - 1; i >= 0; i--)
            {
                num += IndexFromAllNumbersChar(numberStr[i]) * power;
                power = power * number.BaseValue;
            }

            return number.IsNegative ? num * -1 : num;
        }

        public static Number NumberToNumber(Number number, int toBase) => IntToNumber(NumberToInt(number), toBase);

        private static int FullDivide(int a, int b, out int remainder)
        {
            remainder = a % b;
            return a / b;
        }

        private static int IndexFromAllNumbersChar(char c) => Number.OptionAllNumberValues.IndexOf(c);
        private static char AllNumbersCharFromIndex(int index) => Number.OptionAllNumberValues[index];


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

                doBorrow = result >= baseValue; // < 0?
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
    }
}

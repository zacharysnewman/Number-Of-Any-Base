using System;
using System.Collections.Generic;
using System.Linq;
using static NumberOfAnyBase.Functions;

namespace NumberOfAnyBase
{
    // Converting between different number bases and doing math with different bases (up to Base-62 before running out of characters) - Zack Newman
    public struct Number : IEquatable<Number>
    {
        public const string ALL_NUMBER_VALUES = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const char DELIMITER = '|';

        public int baseValue { get; private set; }
        public List<int> digits { get; private set; }

        public Number(int baseValue)
        {
            this.baseValue = baseValue;
            this.digits = new List<int>() { 0 };
        }

        private Number(int baseValue, List<int> digits)
        {
            this.baseValue = baseValue;
            this.digits = new List<int>(digits);
        }

        public Number(string number)
        {
            //this.baseNumber = baseNumber;
            if (IsValidBaseAndNumber(number, out int baseValue, out string valueStr))
            {
                this.baseValue = baseValue;
                // Convert chars to indexes
                this.digits = valueStr.Select((s) => Number.ALL_NUMBER_VALUES.IndexOf(s.ToString())).Reverse().ToList();
            }
            else
            {
                throw new Exception($"Invalid string format. Not a valid Number: {number}");
            }
        }

        public Number Add(Number other)
        {
            List<int> sumDigits = new List<int>();
            bool doCarry = false;
            int result = -1;

            for (int i = 0; i < this.digits.Count || i < other.digits.Count; i++)
            {
                var carry = doCarry ? 1 : 0;
                if (i < this.digits.Count && i < other.digits.Count)
                {
                    result = this.digits[i] + other.digits[i] + carry;
                }
                else if (i >= this.digits.Count)
                {
                    result = other.digits[i] + carry;
                }
                else if (i >= other.digits.Count)
                {
                    result = this.digits[i] + carry;
                }

                doCarry = result >= this.baseValue;
                sumDigits.Add(result % this.baseValue);
            }

            if (doCarry == true)
            {
                sumDigits.Add(1);
            }

            var sum = new Number(this.baseValue, ClearTrailingZeros(sumDigits));
            return sum;
        }

        /*public Number Subtract(Number other)
        {

        }*/

        /*public Number Multiply(Number other)
        {
            for(int i = 0; )
        }*/

        /*public Number Divide(Number other)
        {

        }*/

        /*public Number Mod(Number other)
        {

        }*/

        public override string ToString() => $"{this.baseValue}{Number.DELIMITER}{string.Join("", this.digits.ToArray().Reverse().Select((i) => Number.ALL_NUMBER_VALUES[i]))}";

        public static bool operator ==(Number num1, Number num2) => num1.ToString() == num2.ToString();
        public static bool operator !=(Number num1, Number num2) => !(num1 == num2);
        public bool Equals(Number num) => this == num;
        public override bool Equals(object obj) => Equals(obj as Nullable<Number>);
        public override int GetHashCode() => this.ToString().GetHashCode();

        //private int MinDigit { get => 0; }
        //private int MaxDigit { get => this.Base - 1; }

        //public string Values
        //{
        //    get => GetValues(this.baseNumber);
        //}

        //private static string GetValues(int Base) => ALL_NUMBER_VALUES.Substring(0, Base);
    }
}

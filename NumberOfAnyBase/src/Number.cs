using System;
using System.Collections.Generic;
using System.Linq;
using static NumberOfAnyBase.Functions;

namespace NumberOfAnyBase
{
    // Converting between different number bases and doing math with different bases (up to Base-62 before running out of characters) - Zack Newman
    public partial struct Number : IEquatable<Number>
    {
        public const string ALL_NUMBER_VALUES = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const char DELIMITER = '|';

        public int baseValue { get; private set; }
        public bool isPositive { get; private set; }
        public List<int> digits { get; private set; }

        public Number(int baseValue)
        {
            this.baseValue = baseValue;
            this.isPositive = true;
            this.digits = new List<int>() { 0 };
        }

        private Number(int baseValue, bool isPositive, List<int> digits)
        {
            this.baseValue = baseValue;
            this.isPositive = isPositive;
            this.digits = new List<int>(digits);
        }

        public Number(string number)
        {
            if (IsValidBaseAndNumber(number, out int baseValue, out bool isPositive, out string valueStr))
            {
                this.baseValue = baseValue;
                this.isPositive = isPositive;
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

        /*
            export const add = (a: number) => (b: number) => a + b;
            export const neg = (a: number) => (a === 0 ? 0 : -a);

            const uncurryAdd = (a: number, b: number) => add(a)(b);
            const sumAll = (...values: number[]) => values.reduce(uncurryAdd, 0);
            export const sub = (a: number) => (b: number) => add(a)(neg(b));

            const total = (adder: (a: any, b: any) => unknown) => ({
                adder,
                sentinel: 0,
            });

            // Get absolute value of a JUST for loop
            export const mul = (multiplicand: number) => (multiplier: number) => {
                // if a or b is 0 return 0
                // if a or b is 1 return the opposite
                const addBTo = add(multiplier);
                let product = 0;
                let i = Math.abs(multiplicand); // CHEATING
                while (i > 0) {
                product = addBTo(product);
                i = dec(i);
                }
                return product;
            };

                    // To 2 decimal point precision
                    export const div = (dividend: number) => (divisor: number) => {
                return divCore(dividend)(divisor);

                // console.log(quotient, remainder);

                // to 2 decimals plus 1 to get the rounded final digit
            };

                export const mod = (dividend: number) => (divisor: number) =>
                divCore(dividend)(divisor).remainder;

            const inc = add(1);
                const dec = add(-1);

                // 10 / 3
                // 7 : 1
                // 4 : 2
                // 1 : 3

                // Is remainder ALWAYS positive?

                const divCore = (dividend: number) => (divisor: number) => {
                let workingDividend = Math.abs(dividend);
                const workingDivisor = Math.abs(divisor);

                const subDivisorFrom = sub(workingDivisor);
                let quotient = 0;
                while (workingDividend >= workingDivisor) {
                workingDividend = subDivisorFrom(workingDividend);
                quotient = inc(quotient);
            }

            const remainder = dividend < 0 ? neg(workingDividend) : workingDividend;
            quotient = XOR(dividend < 0)(divisor < 0) ? neg(quotient) : quotient;

            return { quotient, remainder };

                // const numNegatives = add(dividend < 0 ? 1 : 0)(divisor < 0 ? 1 : 0);
                // return mul(numNegatives == 1 ? -1 : 1)(quotient); // (do I need NEG: XOR)
            };
        */
    }
}

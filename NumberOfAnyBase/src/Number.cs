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
            if (IsValidBaseAndNumber(number, out int baseValue, out bool isNegative, out string valueStr))
            {
                this.BaseValue = baseValue;
                this.IsNegative = isNegative;
                this.Digits = valueStr.Select((s) => Number.ALL_NUMBER_VALUES.IndexOf(s.ToString())).Reverse().ToList();
            }
            else
            {
                throw new Exception($"Invalid string format. Not a valid Number: {number}");
            }
        }

        public Number Add(Number other)
        {
            var a = this;
            var b = other;

            if (a.BaseValue != b.BaseValue)
            {
                throw new Exception($"Bases don't match. This base: {a.BaseValue}, Other base: {b.BaseValue}");
            }

            bool resultIsNegative = false;
            Number sum;

            if (a.IsPositive && b.IsPositive)
            {
                resultIsNegative = false;
                // sum = Add(a, b);
            }
            else if (a.IsNegative && a.IsNegative)
            {
                resultIsNegative = true;
                // sum = Add(a.Abs(), b.Abs());
            }
            else // if only one is negative
            {
                if (b.Abs() > a.Abs())
                {
                    Swap(ref a, ref b);
                }
                resultIsNegative = a.IsNegative;
                // sum = Subtract(a.Abs(), b.Abs());
            }
            // return resultIsNegative ? sum.Neg() : sum;

            var sumDigits = AddListsOfDigits(a.Digits, b.Digits, a.BaseValue);

            sum = new Number(a.BaseValue, true, ClearTrailingZeros(sumDigits)); // explicit true, WRONG
            return sum;
        }

        public override string ToString() => ToString(includeBase: true);
        public string ToString(bool includeBase)
        {
            var prefix = includeBase ? $"{this.BaseValue}{Number.DELIMITER}" : "";
            return $"{prefix}{(this.IsNegative ? "-" : "")}{string.Join("", this.Digits.ToArray().Reverse().Select((i) => Number.ALL_NUMBER_VALUES[i]))}";
        }

        public Number Abs() => this.IsNegative ? new Number(this.BaseValue, isNegative: false, this.Digits) : this;

        public static Number operator +(Number a, Number b) => new Operation(Operator.Add, a, b).SimplifyAndSolve();
        public static Number operator -(Number a, Number b) => new Operation(Operator.Subtract, a, b).SimplifyAndSolve();
        public static Number operator *(Number a, Number b) => new Operation(Operator.Multiply, a, b).SimplifyAndSolve();
        public static Number operator /(Number a, Number b) => new Operation(Operator.Divide, a, b).SimplifyAndSolve();
        public static Number operator %(Number a, Number b) => new Operation(Operator.Mod, a, b).SimplifyAndSolve();

        public static implicit operator Number(string s) => new Number(s);
        public static explicit operator string(Number n) => n.ToString();
        public static implicit operator Number(int i) => new Number($"10{Number.DELIMITER}{i}");


        // Compare greater than, less than, equal to, not equal to using symbols (>,<,>=,<=,==,!=)
        public static bool operator ==(Number num1, Number num2) => num1.ToString() == num2.ToString();
        public static bool operator !=(Number num1, Number num2) => !(num1 == num2);
        public bool Equals(Number num) => this == num;
        public override bool Equals(object obj) => Equals(obj as Nullable<Number>);
        public override int GetHashCode() => this.ToString().GetHashCode();

        private static bool GreaterThan(Number num1, Number num2, bool arePositive)
        {
            var subtractionResult = num1.Digits.Zip(num2.Digits, (one, two) => one - two).ToList();
            for (int i = 0; i < subtractionResult.Count; i++)
            {
                if (subtractionResult[i] > 0)
                {
                    return arePositive;
                }
                else if (subtractionResult[i] < 0)
                {
                    return !arePositive;
                }
            }
            return false;
        }

        private static bool LessThan(Number num1, Number num2, bool arePositive)
        {
            var subtractionResult = num1.Digits.Zip(num2.Digits, (one, two) => one - two).ToList();
            for (int i = 0; i < subtractionResult.Count; i++)
            {
                if (subtractionResult[i] > 0)
                {
                    return !arePositive;
                }
                else if (subtractionResult[i] < 0)
                {
                    return arePositive;
                }
            }
            return false;
        }

        public static bool operator >(Number num1, Number num2)
        {
            if (!num1.IsNegative && num2.IsNegative)
            {
                return true;
            }
            else if (num1.IsNegative && !num2.IsNegative)
            {
                return false;
            }
            else if (num1.IsNegative && num2.IsNegative) // both -
            {
                if (num1.Digits.Count < num2.Digits.Count)
                {
                    return true;
                }
                else if (num1.Digits.Count > num2.Digits.Count)
                {
                    return false;
                }
                else // if (num1.digits.Count == num2.digits.Count)
                {
                    return GreaterThan(num1, num2, arePositive: false);
                }
            }
            else // if (!num1.isNegative && !num2.isNegative) // both +
            {
                if (num1.Digits.Count < num2.Digits.Count)
                {
                    return false;
                }
                else if (num1.Digits.Count > num2.Digits.Count)
                {
                    return true;
                }
                else // if (num1.digits.Count == num2.digits.Count)
                {
                    return GreaterThan(num1, num2, arePositive: true);
                }
            }
        }
        public static bool operator <(Number num1, Number num2)
        {
            if (!num1.IsNegative && num2.IsNegative)
            {
                return false;
            }
            else if (num1.IsNegative && !num2.IsNegative)
            {
                return true;
            }
            else if (num1.IsNegative && num2.IsNegative) // both -
            {
                if (num1.Digits.Count < num2.Digits.Count)
                {
                    return false;
                }
                else if (num1.Digits.Count > num2.Digits.Count)
                {
                    return true;
                }
                else // if (num1.digits.Count == num2.digits.Count)
                {
                    return LessThan(num1, num2, arePositive: false);
                }
            }
            else // if (!num1.isNegative && !num2.isNegative) // both +
            {
                if (num1.Digits.Count < num2.Digits.Count)
                {
                    return true;
                }
                else if (num1.Digits.Count > num2.Digits.Count)
                {
                    return false;
                }
                else // if (num1.digits.Count == num2.digits.Count)
                {
                    return LessThan(num1, num2, arePositive: true);
                }
            }
        }
        public static bool operator >=(Number num1, Number num2) => num1 > num2 || num1 == num2;
        public static bool operator <=(Number num1, Number num2) => num1 < num2 || num1 == num2;

        //private Number(int baseValue)
        //{
        //    this.baseValue = baseValue;
        //    this.isNegative = true;
        //    this.digits = new List<int>() { 0 };
        //}

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

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
            this = Number.Parse(number);
        }

        public Number(int number)
        {
            this = new Number($"10{Number.DELIMITER}{number}");
        }

        public override string ToString() => ToString(includeBase: false);
        public string ToString(bool includeBase)
        {
            var prefix = includeBase ? $"{this.BaseValue}{Number.DELIMITER}" : "";
            return $"{prefix}{(this.IsNegative ? "-" : "")}{string.Join("", this.Digits.ToArray().Reverse().Select((i) => Number.ALL_NUMBER_VALUES[i]))}";
        }

        public static Number Parse(string numberStr)
        {
            if (!numberStr.Contains(Number.DELIMITER))
            {
                throw new Exception($"Number string does not contain delimiter: '{Number.DELIMITER}', Input: \"{numberStr}\"");
            }
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
                throw new Exception($"Invalid string format. Not a valid Number string: {baseAndNumber}");
            }

            return new Number(baseValue, isNegative, valueStr.Select(ToDigits).Reverse().ToList());
        }

        private static Func<char, int> ToDigits = (s) => Number.ALL_NUMBER_VALUES.IndexOf(s.ToString());

        /* Example TypeScript implementation of math functions
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

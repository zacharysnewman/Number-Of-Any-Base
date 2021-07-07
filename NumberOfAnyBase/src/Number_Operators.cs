using System;
using static NumberOfAnyBase.Functions;

namespace NumberOfAnyBase
{
    public partial struct Number
    {
        public static Number operator +(Number a, Number b) => BinaryOperation(Add, a, b);
        public static Number operator -(Number a, Number b) => BinaryOperation(Subtract, a, b);
        public static Number operator -(Number n) => new Number(n.BaseValue, !n.IsNegative, n.Digits);
        public static Number operator *(Number a, Number b) => BinaryOperation(Multiply, a, b);
        public static Number operator /(Number a, Number b) => BinaryOperation(Divide, a, b);
        public static Number operator %(Number a, Number b) => BinaryOperation(Mod, a, b);

        private static Func<Func<int, int, int>, Number, Number, Number> BinaryOperation = (operation, a, b) => DecimalToAnyBase(operation(AnyBaseToDecimal(a), AnyBaseToDecimal(b)), a.BaseValue);

        private static Func<int, int, int> Add = (int a, int b) => a + b;
        private static Func<int, int, int> Subtract = (int a, int b) => a - b;
        private static Func<int, int, int> Multiply = (int a, int b) => a * b;
        private static Func<int, int, int> Divide = (int a, int b) => a / b;
        private static Func<int, int, int> Mod = (int a, int b) => a % b;

        // AbsAdd and AbsSubtract treat all Numbers as Absolute values
        // AbsSubtract expects the first value to be larger than the second value
        //public static Number Add(Number a, Number b)
        //{
        //    throw new NotImplementedException(); // UNTIL OLD IS FIXED

        //    if (a.BaseValue != b.BaseValue)
        //    {
        //        throw new Exception($"Bases don't match. This base: {a.BaseValue}, Other base: {b.BaseValue}");
        //    }

        //    bool resultIsNegative = false;
        //    Number sum;

        //    if (a.IsPositive && b.IsPositive)
        //    {
        //        resultIsNegative = false;
        //        // sum = AbsAdd(a, b);
        //    }
        //    else if (a.IsNegative && a.IsNegative)
        //    {
        //        resultIsNegative = true;
        //        // sum = AbsAdd(Abs(a), Abs(b));
        //    }
        //    else // if only one is negative
        //    {
        //        if (Abs(b) > Abs(a))
        //        {
        //            Swap(ref a, ref b);
        //        }
        //        resultIsNegative = a.IsNegative;
        //        // sum = AbsSubtract(Abs(a), Abs(b));
        //    }
        //    // return resultIsNegative ? Neg(sum) : sum;

        //    var sumDigits = AddListsOfDigits(a.Digits, b.Digits, a.BaseValue); // OLD

        //    sum = new Number(a.BaseValue, true, ClearTrailingZeros(sumDigits)); // OLD
        //    return sum; // OLD
        //}
    }
}

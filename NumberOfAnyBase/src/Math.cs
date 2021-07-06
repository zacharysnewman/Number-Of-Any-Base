using System;
namespace NumberOfAnyBase.Math
{
    public static class Math
    {
        public static Number Neg(Number number) => new Number(number.BaseValue, !number.IsNegative, number.Digits);
        public static Number Abs(Number number) => new Number(number.BaseValue, isNegative: false, number.Digits);
    }
}

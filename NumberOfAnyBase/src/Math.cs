using System;
namespace NumberOfAnyBase.Math
{
    public static class Math
    {
        public static Number Neg(Number number) => new Number(number.baseValue, !number.isNegative, number.digits);
        public static Number Abs(Number number) => new Number(number.baseValue, isNegative: false, number.digits);
    }
}

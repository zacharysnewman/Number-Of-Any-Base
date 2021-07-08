using System;
using System.Linq;
using System.Collections.Generic;

namespace NumberOfAnyBase
{
    public partial struct Number
    {
        public static bool operator ==(Number num1, Number num2) => num1.ToString(includeBase: true) == num2.ToString(includeBase: true);
        public static bool operator !=(Number num1, Number num2) => !(num1 == num2);
        public bool Equals(Number num) => this == num;
        public override bool Equals(object obj) => Equals(obj as Nullable<Number>);
        public override int GetHashCode() => this.ToString(includeBase: true).GetHashCode();
        public static bool operator >=(Number num1, Number num2) => num1 > num2 || num1 == num2;
        public static bool operator <=(Number num1, Number num2) => num1 < num2 || num1 == num2;
        public static bool operator >(Number num1, Number num2) =>
            NegativeAndDigitsGreaterThan(num1.IsNegative, num2.IsNegative, num1.Digits, num2.Digits);
        public static bool operator <(Number num1, Number num2) =>
            NegativeAndDigitsLessThan(num1.IsNegative, num2.IsNegative, num1.Digits, num2.Digits);

        private static bool DigitsGreaterThan(List<int> num1Digits, List<int> num2Digits, bool bothPositive)
        {
            var subtractionResult = num1Digits.Zip(num2Digits, (one, two) => one - two).ToList();
            for (int i = 0; i < subtractionResult.Count; i++)
            {
                if (subtractionResult[i] > 0)
                {
                    return bothPositive;
                }
                else if (subtractionResult[i] < 0)
                {
                    return !bothPositive;
                }
            }
            return false;
        }

        private static bool DigitsLessThan(List<int> num1Digits, List<int> num2Digits, bool bothPositive)
        {
            var subtractionResult = num1Digits.Zip(num2Digits, (one, two) => one - two).ToList();
            for (int i = 0; i < subtractionResult.Count; i++)
            {
                if (subtractionResult[i] > 0)
                {
                    return !bothPositive;
                }
                else if (subtractionResult[i] < 0)
                {
                    return bothPositive;
                }
            }
            return false;
        }

        private static bool NegativeAndDigitsGreaterThan(bool num1IsNegative, bool num2IsNegative, List<int> num1Digits, List<int> num2Digits)
        {
            if (!num1IsNegative && num2IsNegative)
            {
                return true;
            }
            else if (num1IsNegative && !num2IsNegative)
            {
                return false;
            }
            else if (num1IsNegative && num2IsNegative) // both -
            {
                if (num1Digits.Count < num2Digits.Count)
                {
                    return true;
                }
                else if (num1Digits.Count > num2Digits.Count)
                {
                    return false;
                }
                else // if (num1.digits.Count == num2.digits.Count)
                {
                    return DigitsGreaterThan(num1Digits, num2Digits, bothPositive: false);
                }
            }
            else // if (!num1.isNegative && !num2.isNegative) // both +
            {
                if (num1Digits.Count < num2Digits.Count)
                {
                    return false;
                }
                else if (num1Digits.Count > num2Digits.Count)
                {
                    return true;
                }
                else // if (num1.digits.Count == num2.digits.Count)
                {
                    return DigitsGreaterThan(num1Digits, num2Digits, bothPositive: true);
                }
            }
        }

        private static bool NegativeAndDigitsLessThan(bool num1IsNegative, bool num2IsNegative, List<int> num1Digits, List<int> num2Digits)
        {
            if (!num1IsNegative && num2IsNegative)
            {
                return false;
            }
            else if (num1IsNegative && !num2IsNegative)
            {
                return true;
            }
            else if (num1IsNegative && num2IsNegative) // both -
            {
                if (num1Digits.Count < num2Digits.Count)
                {
                    return false;
                }
                else if (num1Digits.Count > num2Digits.Count)
                {
                    return true;
                }
                else // if (num1.digits.Count == num2.digits.Count)
                {
                    return DigitsLessThan(num1Digits, num2Digits, bothPositive: false);
                }
            }
            else // if (!num1.isNegative && !num2.isNegative) // both +
            {
                if (num1Digits.Count < num2Digits.Count)
                {
                    return true;
                }
                else if (num1Digits.Count > num2Digits.Count)
                {
                    return false;
                }
                else // if (num1.digits.Count == num2.digits.Count)
                {
                    return DigitsLessThan(num1Digits, num2Digits, bothPositive: true);
                }
            }
        }
    }
}

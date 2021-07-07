using System;
using System.Linq;

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

        private static bool GreaterThan(Number num1, Number num2, bool bothPositive)
        {
            var subtractionResult = num1.Digits.Zip(num2.Digits, (one, two) => one - two).ToList();
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

        private static bool LessThan(Number num1, Number num2, bool bothPositive)
        {
            var subtractionResult = num1.Digits.Zip(num2.Digits, (one, two) => one - two).ToList();
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
                    return GreaterThan(num1, num2, bothPositive: false);
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
                    return GreaterThan(num1, num2, bothPositive: true);
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
                    return LessThan(num1, num2, bothPositive: false);
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
                    return LessThan(num1, num2, bothPositive: true);
                }
            }
        }
    }
}

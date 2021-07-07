// Scope
// (√) Accept any base in string format (i.e. 10|999, 10|-999)
// (√) Neg, Add, Subtract, Divide, Multiply, Mod using actual symbols (+,-,/,*,%)
// (√) Compare greater than, less than, equal to, not equal to using symbols (>,<,>=,<=,==,!=)
// (√) Implicitly convert from int to Number
// (√) Convert from any base to any base

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using static NumberOfAnyBase.Functions;

namespace NumberOfAnyBase
{
    class Program
    {
        static Action<dynamic> Log = Console.WriteLine;

        public static void Main(string[] args)
        {
            Number a = "3|22";
            Number b = "3|11";
            Console.WriteLine("-- Example --");
            Console.WriteLine($"Base: {a.BaseValue}");
            Console.WriteLine($"{a} + {b} = {a + b}");
            Console.WriteLine($"{a} - {b} = {a - b}");
            Console.WriteLine($"{a} / {b} = {a / b}");
            Console.WriteLine($"{a} * {b} = {a * b}");
            Console.WriteLine($"{a} % {b} = {a % b}");

            // -- Tests -- //
            AdditionCommutativeTest();
            AdditionAssociativeTest();
            GreaterThanTest();
            GreaterThanOrEqualToTest();
            LessThanTest();
            LessThanOrEqualToTest();
            EqualToTest();
            NotEqualToTest();
        }

        static void AdditionCommutativeTest()
        {
            // Commutative: a+b=b+a
            Log("\n-- Commutative --");
            Number a = "10|21";
            Number b = "10|1001";
            Log($"a: {a}, b: {b}");
            Log($"{a} + {b} = {a + b}");
            Log($"{b} + {a} = {b + a}");
            Log($"a + b = b + a: {a + b == b + a}");
        }

        static void AdditionAssociativeTest()
        {
            // Associative: a+(b+c)=(a+b)+c
            Log("\n-- Associative --");
            Number a = "40|KL";
            Number b = "40|HH";
            Number c = "40|1dIA";
            Log($"a: {a}, b: {b}, c: {c}");

            Log($"{a} + ({b} + {c}) = {a + (b + c)}");
            Log($"({a} + {b}) + {c} = {c + (a + b)}");
            Log($"a + (b + c) = (a + b) + c: {a + (b + c) == c + (a + b)}");
        }

        static void GreaterThanTest()
        {
            Log("\n---- Greater Than Test ----");
            var nums = new[] { new Number("10|-2"), new Number("10|-1"), new Number("10|0"), new Number("10|1"), new Number("10|2") };
            List<string> result = new List<string>();
            int i = 0;
            for (int x = 0; x < nums.Length; x++)
            {
                for (int y = 0; y < nums.Length; y++, i++)
                {
                    result.Add($"{nums[x].ToString(false)} > {nums[y].ToString(false)}: {nums[x] > nums[y] == int.Parse(nums[x].ToString(false)) > int.Parse(nums[y].ToString(false))}");
                }
            }
            Log($"{string.Join("\n", result)}");
        }
        static void GreaterThanOrEqualToTest()
        {
            Log("\n---- Greater Than Or Equal To Test ----");
            var nums = new[] { new Number("10|-2"), new Number("10|-1"), new Number("10|0"), new Number("10|1"), new Number("10|2") };
            List<string> result = new List<string>();
            int i = 0;
            for (int x = 0; x < nums.Length; x++)
            {
                for (int y = 0; y < nums.Length; y++, i++)
                {
                    result.Add($"{nums[x]} >= {nums[y]}: {nums[x] >= nums[y] == int.Parse(nums[x].ToString(false)) >= int.Parse(nums[y].ToString(false))}");
                }
            }
            Log($"{string.Join("\n", result)}");
        }

        static void LessThanTest()
        {
            Log("\n---- Less Than Test ----");
            var nums = new[] { new Number("10|-2"), new Number("10|-1"), new Number("10|0"), new Number("10|1"), new Number("10|2") };
            List<string> result = new List<string>();
            int i = 0;
            for (int x = 0; x < nums.Length; x++)
            {
                for (int y = 0; y < nums.Length; y++, i++)
                {
                    result.Add($"{nums[x]} < {nums[y]}: {nums[x] < nums[y] == int.Parse(nums[x].ToString(false)) < int.Parse(nums[y].ToString(false))}");
                }
            }
            Log($"{string.Join("\n", result)}");
        }

        static void LessThanOrEqualToTest()
        {
            Log("\n---- Less Than Or Equal To Test ----");
            var nums = new[] { new Number("10|-2"), new Number("10|-1"), new Number("10|0"), new Number("10|1"), new Number("10|2") };
            List<string> result = new List<string>();
            int i = 0;
            for (int x = 0; x < nums.Length; x++)
            {
                for (int y = 0; y < nums.Length; y++, i++)
                {
                    result.Add($"{nums[x]} <= {nums[y]}: {nums[x] <= nums[y] == int.Parse(nums[x].ToString(false)) <= int.Parse(nums[y].ToString(false))}");
                }
            }
            Log($"{string.Join("\n", result)}");
        }

        static void EqualToTest()
        {
            Log("\n---- Equal To Test ----");
            var nums = new[] { new Number("10|-2"), new Number("10|-1"), new Number("10|0"), new Number("10|1"), new Number("10|2") };
            List<string> result = new List<string>();
            int i = 0;
            for (int x = 0; x < nums.Length; x++)
            {
                for (int y = 0; y < nums.Length; y++, i++)
                {
                    result.Add($"{nums[x]} == {nums[y]}: {(nums[x] == nums[y]) == (int.Parse(nums[x].ToString(false)) == int.Parse(nums[y].ToString(false)))}");
                }
            }
            Log($"{string.Join("\n", result)}");
        }

        static void NotEqualToTest()
        {
            Log("\n---- Not Equal To Test ----");
            var nums = new[] { new Number("10|-2"), new Number("10|-1"), new Number("10|0"), new Number("10|1"), new Number("10|2") };
            List<string> result = new List<string>();
            int i = 0;
            for (int x = 0; x < nums.Length; x++)
            {
                for (int y = 0; y < nums.Length; y++, i++)
                {
                    result.Add($"{nums[x]} != {nums[y]}: {(nums[x] != nums[y]) == (int.Parse(nums[x].ToString(false)) != int.Parse(nums[y].ToString(false)))}");
                }
            }
            Log($"{string.Join("\n", result)}");
        }
    }
}

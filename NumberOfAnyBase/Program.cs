// Scope
// ( ) Accept any base in string format (i.e. 10:999, 10:-999)
// ( ) Add, Subtract, Divide, Multiply, Mod using actual symbols (+,-,/,*,%)
// (√) Compare greater than, less than, equal to, not equal to using symbols (>,<,>=,<=,==,!=)
// ( ) Convert from any base to any base

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace NumberOfAnyBase
{
    class MainClass
    {
        static Action<dynamic> Log = Console.WriteLine;

        public static void Main(string[] args)
        {
            // Commutative: a+b=b+a
            Log("-- Commutative --");
            var a = new Number("10|21");
            var b = new Number("10|1001");
            Log($"a: {a}, b: {b}");
            Log($"{a} + {b} = {a.Add(b)}");
            Log($"{b} + {a} = {b.Add(a)}");
            Log($"a + b = b + a: {a.Add(b) == b.Add(a)}");

            // Associative: a+(b+c)=(a+b)+c
            Log("\n-- Associative --");
            a = new Number("40|kl");
            b = new Number("40|hh");
            var c = new Number("40|1DiA");
            Log($"a: {a}, b: {b}, c: {c}");

            Log($"{a} + ({b} + {c}) = {a.Add(b.Add(c))}");
            Log($"({a} + {b}) + {c} = {c.Add(a.Add(b))}");
            Log($"a + (b + c) = (a + b) + c: {a.Add(b.Add(c)) == c.Add(a.Add(b))}");

            GreaterThanTest();
            GreaterThanOrEqualToTest();
            LessThanTest();
            LessThanOrEqualToTest();
            EqualToTest();
            NotEqualToTest();
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
                    result.Add($"{nums[x].ToString(false)} > {nums[y].ToString(false)}: {nums[x] > nums[y]} {int.Parse(nums[x].ToString(false)) > int.Parse(nums[y].ToString(false))}");
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
                    result.Add($"{nums[x].ToString(false)} >= {nums[y].ToString(false)}: {nums[x] >= nums[y]} {int.Parse(nums[x].ToString(false)) >= int.Parse(nums[y].ToString(false))}");
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
                    result.Add($"{nums[x].ToString(false)} < {nums[y].ToString(false)}: {nums[x] < nums[y]} {int.Parse(nums[x].ToString(false)) < int.Parse(nums[y].ToString(false))}");
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
                    result.Add($"{nums[x].ToString(false)} <= {nums[y].ToString(false)}: {nums[x] <= nums[y]} {int.Parse(nums[x].ToString(false)) <= int.Parse(nums[y].ToString(false))}");
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
                    result.Add($"{nums[x].ToString(false)} == {nums[y].ToString(false)}: {nums[x] == nums[y]} {int.Parse(nums[x].ToString(false)) == int.Parse(nums[y].ToString(false))}");
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
                    result.Add($"{nums[x].ToString(false)} != {nums[y].ToString(false)}: {nums[x] != nums[y]} {int.Parse(nums[x].ToString(false)) != int.Parse(nums[y].ToString(false))}");
                }
            }
            Log($"{string.Join("\n", result)}");
        }
    }
}

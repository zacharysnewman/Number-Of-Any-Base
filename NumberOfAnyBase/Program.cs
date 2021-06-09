using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NumberOfAnyBase
{
    class MainClass
    {
        static Action<dynamic> Log = Console.WriteLine;

        public static void Main(string[] args)
        {
            // Commutative: a+b=b+a
            Log("-- Commutative --");
            var a = new Number("3|21");
            var b = new Number("3|1001");
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

            Log("\n-- Misc --");
            var num1 = new Number("4|31");
            var num2 = new Number("4|3");
            Log($"(Base {num1.baseValue}) {num1} + {num2} = {num1.Add(num2)}");
            // "9:123456780"
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberOfAnyBase
{
    public partial struct Number
    {
        public static implicit operator Number(string s) => new Number(s);
        public static explicit operator string(Number n) => n.ToString(includeBase: false);
        public static implicit operator Number(int i) => new Number(i);
    }
}

using System;
using static NumberOfAnyBase.Math.Operators;
using static NumberOfAnyBase.Math.Math;

namespace NumberOfAnyBase
{
    public struct Operation
    {
        public Operator Op { get; set; }
        public Number A { get; set; }
        public Number B { get; set; }

        public Operation(Operation operation)
        {
            this.Op = operation.Op;
            this.A = operation.A;
            this.B = operation.B;
        }

        public Operation(Operator op, Number A, Number B)
        {
            this.Op = op;
            this.A = A;
            this.B = B;
        }

        private Operation Simplify()
        {
            switch (Op)
            {
                case Operator.Add:
                    // -A + B = B - A
                    if (this.A.isNegative && !this.B.isNegative)
                    {
                        return new Operation(Operator.Subtract, this.B, Abs(this.A));
                    }
                    // A + -B = A - B
                    else if (!this.A.isNegative && this.B.isNegative)
                    {
                        return new Operation(Operator.Subtract, this.A, Abs(this.B));
                    }
                    // (√) -A + -B, A + B
                    else
                    {
                        return new Operation(this);
                    }
                    break;
                case Operator.Subtract:
                    // -A - -B = B - A
                    if (this.A.isNegative && this.B.isNegative)
                    {
                        return new Operation(Operator.Subtract, Abs(this.B), Abs(this.A));
                    }
                    // A - -B = A + B
                    else if (!this.A.isNegative && this.B.isNegative)
                    {
                        return new Operation(Operator.Add, this.A, Abs(this.B));
                    }
                    // -A - B = -A + -B
                    else if (this.A.isNegative && !this.B.isNegative)
                    {
                        return new Operation(Operator.Add, this.A, Neg(this.B));
                    }
                    // (√) A - B
                    else
                    {
                        return new Operation(this);
                    }
                    break;
                case Operator.Multiply:
                    throw new NotImplementedException();
                    break;
                case Operator.Divide:
                    throw new NotImplementedException();
                    break;
                case Operator.Mod:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new Exception("Invalid operator.");
                    break;
            }
        }

        public Number Solve()
        {
            Simplify();
            return new Func<Number, Number, Number>[] { Add, Subtract, Multiply, Divide, Mod }[(int)this.Op](this.A, this.B);
        }

        public override string ToString() => $"{{ {this.A} {"+-*/%"[(int)this.Op]} {this.B} }}";
    }
}

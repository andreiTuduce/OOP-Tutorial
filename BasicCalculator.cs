namespace OOP_Tutorial
{
    public class BasicCalculator
    {

        public virtual double Sum(double a, double b)
        {
            return a + b;
        }

        public virtual double Substract(double a, double b)
        {
            return a - b;
        }

        public virtual double Multiply(double a, double b)
        {
            return a * b;
        }

        public virtual double Divide(double a, double b)
        {
            if (a == 0)
                return a / b;

            if (b == 0)
                return b / a;

            return 0;
        }

        public string ToString(double a, double b)
        {
            return $"a is {a} and b is {b}";
        }
    }
}

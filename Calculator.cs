using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Tutorial
{
    public class Calculator : BasicCalculator 
    {
        public double RoundNumber(double a) => Math.Round(a);

        public bool IsEven(double a) => a % 2 == 0;

        public double Pow(double a, double b) => Math.Pow(a, b);
    }
}

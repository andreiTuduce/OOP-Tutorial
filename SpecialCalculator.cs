using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Tutorial
{
    public class SpecialCalculator : BasicCalculator
    {

        private double specialModifier = new Random().Next(1, 100);

        public override double Sum(double a, double b) => base.Sum(a, b + specialModifier);

        public override double Multiply(double a, double b) => base.Multiply(a, b) * specialModifier;

        public double Multiply(double a, double b, double c) => base.Multiply(a, c) - b;

        public void ChangeModifier() => specialModifier = new Random().Next(1, 100);

        public double ModifierValue() => specialModifier;

    }
}

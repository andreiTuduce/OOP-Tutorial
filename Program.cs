using System;
using System.Collections.Generic;
using System.Reflection;

namespace OOP_Tutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, MethodInfo> availableOperations = null;
            BasicCalculator calculator = PickCalculator(ref availableOperations);

            double a, b, c;

            (a, b, c) = SetNumberForOperations();

            ShowTypedActions();

            bool exitWhileLoop = false;
            while (!exitWhileLoop)
            {
                string typedString = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(typedString))
                    continue;

                if (ChosenAction(typedString, new string[] { "C", "(C)" }))
                {
                    calculator = PickCalculator(ref availableOperations);
                    ShowOperations(availableOperations);
                    continue;
                }

                if (ChosenAction(typedString, new string[] { "HELP", "(HELP)" }))
                {
                    ShowOperations(availableOperations);
                    continue;
                }

                if (ChosenAction(typedString, new string[] { "QUIT", "(QUIT)" }))
                {
                    exitWhileLoop = true;
                    continue;
                }

                if (ChosenAction(typedString, new string[] { "CHANGE", "(CHANGE)" }))
                {
                    (a, b, c) = SetNumberForOperations();
                    continue;
                }

                MakeOperation(calculator, a, b, c, availableOperations, int.Parse(typedString));

                Console.WriteLine("Try another operation!");
            }

            Console.WriteLine("To close the application press any key!");
            Console.ReadKey();
        }

        private static void MakeOperation(BasicCalculator calculator, double a, double b, double c, Dictionary<int, MethodInfo> availableOperations, int operation)
        {
            object[] args = null;

            switch (availableOperations[operation].GetParameters().Length)
            {
                case 0:
                    args = new object[0];
                    break;
                case 1:
                    args = new object[] { a };
                    break;
                case 2:
                    args = new object[] { a, b };
                    break;
                case 3:
                    args = new object[] { a, b, c };
                    break;
            }

            Console.WriteLine(availableOperations[operation].Invoke(calculator, args));
        }

        private static (double, double, double) SetNumberForOperations()
        {
            Console.Write("Give a: ");
            double a = double.Parse(Console.ReadLine());
            Console.Write("Give b: ");
            double b = double.Parse(Console.ReadLine());
            Console.Write("Give c: ");
            double c = double.Parse(Console.ReadLine());

            return (a, b, c);
        }

        private static bool ChosenAction(string typedString, string[] options)
        {
            return typedString.ToUpper() == options[0] || typedString == options[1];
        }

        private static void ShowOperations(Dictionary<int, MethodInfo> availableOperations)
        {
            foreach (KeyValuePair<int, MethodInfo> keyValuePair in availableOperations)
            {
                Console.WriteLine($"Press {keyValuePair.Key} for {keyValuePair.Value}");
            }
        }

        private static BasicCalculator PickCalculator(ref Dictionary<int, MethodInfo> availableOperations)
        {
            Console.WriteLine("Choose the calculator you want to use: \n Press 1 for Calculator \n Press 2 for Special Calculator");
            int option = int.Parse(Console.ReadLine());
            BasicCalculator calculator = null;

            bool optionChosen = true;

            while (optionChosen)
            {
                if (option == 1)
                {
                    calculator = new Calculator();
                    optionChosen = false;
                }
                else if (option == 2)
                {
                    calculator = new SpecialCalculator();
                    optionChosen = false;
                }
                else
                {
                    Console.WriteLine("Invalid option! Choose again");
                }
            }

            availableOperations = GetOperations(calculator);

            return calculator;
        }

        private static Dictionary<int, MethodInfo> GetOperations(BasicCalculator calculator)
        {
            int i = 1;

            Dictionary<int, MethodInfo> keyValuePairs = new Dictionary<int, MethodInfo>();

            foreach (MethodInfo property in calculator.GetType().GetMethods())
            {
                keyValuePairs.Add(i, property);
                i++;
            }

            return keyValuePairs;
        }

        private static void ShowTypedActions()
        {
            Console.WriteLine("To check the options type (Help)");
            Console.WriteLine("To choose again type (C)");
            Console.WriteLine("To exit type (Quit)");
            Console.WriteLine("To change numbers type (Change)");
        }
    }
}

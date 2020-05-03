using System;
using System.Collections.Generic;
using System.Reflection;

namespace OOP_Tutorial
{
    class Program
    {
        static void Main(string[] args)
        {

            Calculator calculator = new Calculator();
            SpecialCalculator specialCalculator = new SpecialCalculator();

            (double, double, double) number = SetNumberForOperations();

            double a = number.Item1;
            double b = number.Item2;
            double c = number.Item3;

            Dictionary<int, MethodInfo> availableOperations = PickCalculator(calculator, specialCalculator);
            ShowOperations(availableOperations);
            ShowTypedActions();

            bool exitWhileLoop = false;
            while (!exitWhileLoop)
            {
                string typedString = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(typedString))
                    continue;

                if (ChosenAction(typedString, new string[] { "C", "(C)" }))
                {
                    availableOperations = PickCalculator(calculator, specialCalculator);
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

                int operation = int.Parse(typedString);

                Type type = availableOperations[operation].ReflectedType;

                bool isSpecialCalculator = type == typeof(SpecialCalculator);
                
                if(isSpecialCalculator)
                    MakeOperation(specialCalculator, a, b, c, availableOperations, operation);
                else
                    MakeOperation(calculator, a, b, c, availableOperations, operation);


                Console.WriteLine("Try another operation!");
            }

            Console.WriteLine("To close the application press any key!");
            Console.ReadKey();
        }


        private static void MakeOperation(BasicCalculator calculator, double a, double b, double c, Dictionary<int, MethodInfo> availableOperations, int operation)
        {
            switch (availableOperations[operation].GetParameters().Length)
            {
                case 0:
                    Console.WriteLine(availableOperations[operation].Invoke(calculator, new object[] { }));
                    break;
                case 1:
                    Console.WriteLine(availableOperations[operation].Invoke(calculator, new object[] { a }));
                    break;
                case 2:
                    Console.WriteLine(availableOperations[operation].Invoke(calculator, new object[] { a, b }));
                    break;
                case 3:
                    Console.WriteLine(availableOperations[operation].Invoke(calculator, new object[] { a, b, c }));
                    break;
                default:
                    break;
            }
        }


        private static void ShowTypedActions()
        {
            Console.WriteLine("To check the options type (Help)");
            Console.WriteLine("To choose again type (C)");
            Console.WriteLine("To exit type (Quit)");
            Console.WriteLine("To change numbers type (Change)");
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

        private static Dictionary<int, MethodInfo> PickCalculator(Calculator calculator, SpecialCalculator specialCalculator)
        {
            Console.WriteLine("Choose the calculator you want to use: \n Press 1 for Calculator \n Press 2 for Special Calculator");
            int option = int.Parse(Console.ReadLine());

            bool optionChosen = true;

            while (optionChosen)
            {
                if (option == 1)
                {
                    return GetOperations(calculator);
                }
                else if (option == 2)
                {
                    return GetOperations(specialCalculator);
                }
                else
                {
                    Console.WriteLine("Invalid option! Choose again");
                }
            }

            return null;
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
    }
}

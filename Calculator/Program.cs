using CalculatorLibrary;
using System.Diagnostics;
using System.Runtime.InteropServices;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calculator = new Calculator();
        bool useOldCalcs = false;
        List<double> oldCalculations = new List<double>();

        while (!endApp)
        {
            // Declare variables and set to empty.
            string numInput1 = "";
            string numInput2 = "";
            double result = 0;
            string op = "";

            // Ask the user to choose an operator.
            do
            {
                Console.WriteLine("Choose an operator from the following list:");
                if (oldCalculations.Count > 0) { Console.WriteLine("\tdel - Delete Existing List of Results"); }
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tsr - Square Root");
                Console.WriteLine("\tp - Take the Power of");
                Console.Write("Your option? ");

                op = Console.ReadLine();

                if (op == "del")
                {
                    oldCalculations.Clear();
                    Console.WriteLine("The List of Existing Results has been cleared.");
                }
            } while (op == "del");


            double cleanNum1 = 0;

            if (oldCalculations.Count > 0)
            {
                Console.Write("Would you like to use the old results (y/n): ");

                cleanNum1 = Console.ReadLine() switch
                {
                    "y" => SelectOldCalc(oldCalculations),
                    _ => 0
                };
            }

            if (cleanNum1 == 0)
            {

                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }
            }

            // Ask the user to type the second number.
            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter an integer value: ");
                numInput2 = Console.ReadLine();
            }

            try
            {
                result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                if (double.IsNaN(result))
                {
                    Console.WriteLine("This operation will result in a mathematical error.\n");
                }
                else
                {
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                    oldCalculations.Add(result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
            }

            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing.
        }

        // Add call to close the JSON writer before return
        calculator.Finish();

        return;
    }

    private static double SelectOldCalc(List<double> oldCalcs)
    {
        string input = "";
        int result = 0;

        for (int i = 0; i < oldCalcs.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {oldCalcs[i]}");
        }

        while (result <= 0 || result > oldCalcs.Count)
        {
            Console.Write("Select from the following results: ");
            int.TryParse(Console.ReadLine(), out result);
        }

        Console.WriteLine($"First number: {oldCalcs[result - 1]}");

        return oldCalcs[result - 1];
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1___Calculator
{
    static class Calculator
    {
        static string operations = "*/-+";
        static public string DoOperation(string num1, string op, string num2)
        {
            if (!CheckInputNum(num1, out float fnum1)
                || !CheckInputNum(num2, out float fnum2)
                || !CheckInputOper(op)
                || (fnum1.Equals(0) & op.Equals(operations[1]))
                )
                return string.Empty;
            else
            {
                switch (op)
                {
                    case "+": return Convert.ToString(fnum1 + fnum2);
                    case "-": return Convert.ToString(fnum1 - fnum2);
                    case "/": return Convert.ToString(fnum1 / fnum2);
                    case "*": return Convert.ToString(fnum1 * fnum2);

                    default:
                        return string.Empty;
                }
            }
        }
        static public bool CheckInputNum(string num, out float fnum)
        {
            if (Single.TryParse(num, out fnum))
                return true;
            return false;
        }
        static public bool CheckInputOper(string op)
        {
            if (operations.Contains(op))
                return true;
            return false;
        }
        static public void CheckCommand(ref string command)
        {
            command = command.Replace('.', ',');
            if (command == "#")
                System.Environment.Exit(0);
            if (command == "@")
                command = string.Empty;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("'#' - exit.\n'@' - clear.\n");

                string result;

                Console.Write("Enter the number: ");
                string num1 = Console.ReadLine();
                Calculator.CheckCommand(ref num1);
                if (num1 == string.Empty)
                    continue;

                while (true)
                {

                    Console.Write("Enter the operation: ");
                    string op = Console.ReadLine();
                    Calculator.CheckCommand(ref op);
                    if (op == string.Empty)
                        break;

                    Console.Write("Enter the number: ");
                    string num2 = Console.ReadLine();
                    Calculator.CheckCommand(ref num2);
                    if (num2 == string.Empty)
                        break;

                    result = Calculator.DoOperation(num1, op, num2);
                    if (result == string.Empty)
                        if (Calculator.CheckInputNum(num1, out float fnum1))
                            Console.WriteLine("Error. Old result = " + num1);
                        else
                            break;
                    else
                    {
                        Console.WriteLine("Result = " + result);
                        num1 = result;
                    }
                }
            }
        }
    }
}

using Csharp13Net.Features;

namespace Csharp13Net
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Topics covered in here are as mentioned below entr anyone option {Environment.NewLine}" +
                $"1.  New Lock Object {Environment.NewLine}" +
                $"2.  Task.WhenEach {Environment.NewLine}" +
                $"3.  Params Collections {Environment.NewLine}" +
                $"4.  Semi-Auto Properties {Environment.NewLine}" +
                $"5.  Hybrid Cache {Environment.NewLine}" +
               // $"6.  Built-in OpenAPI Document Generation {Environment.NewLine}" +
                $"7.  SearchValues Improvements {Environment.NewLine}" +
                $"8.  New LINQ Methods {Environment.NewLine}" +
                $"9.  Built-in UUID v7 Generation {Environment.NewLine}" +
                $"10. Implicit index access {Environment.NewLine}" +
                $"11. Partial properties {Environment.NewLine}" +
                $"12. Allows ref struct {Environment.NewLine}" +
                $"");

            Console.Write("Enter option number: ");
            if (int.TryParse(Console.ReadLine(), out int option))
            {
                ExecuteOption(option);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 12.");
            }
        }

        static void ExecuteOption(int option)
        {
            IDoWork doWork = null;
            switch (option)
            {
                case 1:
                    Console.WriteLine("Executing: New Lock Object");
                    doWork = new NewLockExample();                    
                    break;

                case 2:
                    Console.WriteLine("Executing: Task.WhenEach");
                    doWork = new TaskWhenEach();                    
                    break;
                case 3:
                    Console.WriteLine("Executing: Params Collections");
                    doWork = new ParamsCollections();
                    break;
                case 4:
                    Console.WriteLine("Executing: Semi-Auto Properties");
                    doWork = new SemiAutoProperties();
                    break;
                case 5:
                    Console.WriteLine("Executing: Hybrid Cache");
                    doWork = new HybridCacheExample();
                    break;
                case 6:
                    Console.WriteLine("Executing: Built-in OpenAPI Document Generation");
                    break;
                case 7:
                    Console.WriteLine("Executing: SearchValues Improvements");
                    doWork = new SearchValuesImprovements();
                    break;
                case 8:
                    Console.WriteLine("Executing: New LINQ Methods");
                    doWork = new NewLINQMethods();
                    break;
                case 9:
                    Console.WriteLine("Executing: Built-in UUID v7 Generation");
                    doWork = new UUIDV7();
                    break;
                case 10:
                    Console.WriteLine("Executing: Implicit index access");
                    doWork = new ImplicitIndex();
                    break;
                case 11:
                    Console.WriteLine("Executing: Partial properties");
                    doWork = new PartialProperty();
                    break;
                case 12:
                    Console.WriteLine("Executing: Allows ref struct");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please enter a number between 1 and 12.");
                    break;
            }

            doWork.DoWork();
        }
    }
}

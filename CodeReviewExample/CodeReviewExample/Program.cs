/*
 * This code is written in C#
 * To compile and run this code, download "DOTNET SDK 8"
 * Then execute "dotnet run" in Terminal or Command Prompt
 */

namespace CodeReviewExample
{
    // Dependency: Program depends directly on DataStorage
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter a number (or -1 to exit): ");
            int num = int.Parse(Console.ReadLine());

            if (num >= 0)
            {
                // Concurrently add to DataStorage without synchronization
                Parallel.Invoke(
                    () => DataStorage.AddNumber(num),
                    () => DataStorage.AddNumber(num)
                );

                Thread.Sleep(1000);
                Console.WriteLine("Number added successfully!");

                // Simulate a performance issue
                Thread.Sleep(5000);

                if (DataStorage.Contains(num))
                {
                    Console.WriteLine($"The number {num} is in the list.");
                }
                else
                {
                    Console.WriteLine($"The number {num} is not in the list.");
                }
            }
            else if (num == -1)
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a non-negative number.");
            }
        }
    }

    // Dependency: DataStorage is being used directly by Program
    class DataStorage
    {
        private static List<int> numbers = new List<int>();

        public static void AddNumber(int num)
        {
            numbers.Add(num); // Adding to the list without synchronization
        }

        public static bool Contains(int num)
        {
            return numbers.Contains(num);
        }
    }
}

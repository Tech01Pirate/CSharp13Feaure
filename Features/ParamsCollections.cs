namespace Csharp13Net.Features
{
    internal class ParamsCollections : IDoWork
    {
        static void WriteNumbersCount(params ReadOnlySpan<int> numbers) =>
            Console.WriteLine(numbers.Length);

        static void WriteNumbersCount(params IEnumerable<int> numbers) =>
            Console.WriteLine(numbers.Count());

        static void WriteNumbersCount(params HashSet<int> numbers) =>
            Console.WriteLine(numbers.Count);

        public void DoWork()
        {
            WriteNumbersCount(new[] { 1, 2, 3, 4, 5 }); // Calls the IEnumerable<int> overload
            WriteNumbersCount(new List<int> { 10, 20, 30 }); // Calls the IEnumerable<int> overload
            WriteNumbersCount(new HashSet<int> { 100, 200, 300 }); // Calls the HashSet<int> overload
        }
    }
}

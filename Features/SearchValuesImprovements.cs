using System.Buffers;

namespace Csharp13Net.Features
{
    internal class SearchValuesImprovements : IDoWork
    {
        public void DoWork()
        {
            var text = "Exploring new capabilities of SearchValues!".AsSpan();

            // Before
            var vowelSearch = SearchValues.Create(['n', 'e', 'w']);
            Console.WriteLine(text.ContainsAny(vowelSearch));

            // .NET 9
            var keywordSearch = SearchValues.Create(["new", "of"], StringComparison.OrdinalIgnoreCase);
            Console.WriteLine(text.ContainsAny(keywordSearch));
        }
    }
}

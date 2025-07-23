namespace Csharp13Net.Features
{
    internal class SemiAutoProperties : IDoWork
    {
        public int Number
        {
            get => field;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be greater than 0");
                field = value;
            }
        }
        public void DoWork()
        {
            Number = 10;
            Console.WriteLine($"Assignig and accessing Semi Auto Property > Number {Number}");
        }
    }
}

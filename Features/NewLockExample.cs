namespace Csharp13Net.Features
{
    public class NewLockExample : IDoWork
    {
        // Example of using the new lock object
        private readonly Lock lockObject = new();

        public void DoWork()
        {
            lock (lockObject)
            {
                // Critical section code here
                Console.WriteLine("Lock acquired using new lock object.");
            }
        }
    }
}

namespace Csharp13Net.Features
{
    internal class TaskWhenEach : IDoWork
    {
        public void DoWork()
        {
            Task.Run(async () =>
            {
                var tasks = Enumerable.Range(1, 5)
                .Select(async i =>
                {
                    await Task.Delay(new Random().Next(1000, 5000));
                    return $"Task {i} done";
                })
                .ToList();                

                // .NET 9
                await foreach (var completedTask in Task.WhenEach(tasks))
                    Console.WriteLine(await completedTask);

            });
        }
    }
}

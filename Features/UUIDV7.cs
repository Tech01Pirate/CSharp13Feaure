using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp13Net.Features
{
    internal class UUIDV7 : IDoWork
    {
        public void DoWork()
        {
            var guid = Guid.NewGuid(); // v4 UUID
            guid = Guid.CreateVersion7(); // v7 UUID
            guid = Guid.CreateVersion7(TimeProvider.System.GetUtcNow()); // v7 UUID with timestamp
            Console.WriteLine(guid);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp13Net.Features
{
    internal class ImplicitIndex : IDoWork
    {
        public void DoWork()
        {
            var countdown = new TimerRemaining()
            {
                buffer =
                {
                    [^1] = 0,
                    [^2] = 1,
                    [^3] = 2,
                    [^4] = 3,
                    [^5] = 4,
                    [^6] = 5,
                    [^7] = 6,
                    [^8] = 7,
                    [^9] = 8,
                    [^10] = 9
                }
            };
        }
    }

    public class TimerRemaining
    {
        public int[] buffer { get; set; } = new int[10];
    }
}

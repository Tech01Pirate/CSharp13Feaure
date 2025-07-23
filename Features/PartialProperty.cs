using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Csharp13Net.Features
{
    internal partial class PartialPropertyClass
    {
        [GeneratedRegex("abc|def")]
        public static partial Regex AbcOrDefProperty { get; }
    }

    internal class PartialProperty : IDoWork
    {
        public void DoWork()
        {
            var text = "This is a sample text with abc and def in it.";
            // Fix CS0176: Access the static property using the class name instead of an instance.
            // Fix CS0103: Use the correct method name 'IsMatch' from the Regex class.
            if (PartialPropertyClass.AbcOrDefProperty.IsMatch(text))
            {
                // Take action with matching text
            }
        }
    }

}

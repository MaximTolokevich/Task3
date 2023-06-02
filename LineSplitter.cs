using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
     public static class LineSplitter
    {
        public static IEnumerable<string> SplitLine(string str , char[] spliters)
        {
            return str.Split(spliters,StringSplitOptions.RemoveEmptyEntries);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public static class DataEditor
    {
        public static IEnumerable<int> FindWordPositions(IEnumerable<string> line, string word)
        {
            return line.Select((x, y) => (x, y)).Where(z => z.x.Equals(word, StringComparison.OrdinalIgnoreCase)).Select(q => q.y);
        }

        public static (int, string) ReplaceWord(IEnumerable<string> line, string word ,string wordToInsert)
        {
            var indexesToReplace = FindWordPositions(line, word).ToArray();
            var replacedWords = 0;
            var lineToArray = line.ToArray();
            for (int i = 0; i < indexesToReplace.Length; i++)
            {
                lineToArray[indexesToReplace[i]] = wordToInsert;
                replacedWords++;
            }

            return (replacedWords, string.Join(" ", lineToArray));
        }

        public static (int,string) DeleteWord(IEnumerable<string> line, string word)
        {
            var lineAsArray = line.ToArray();
            var changedLine = line.Where(x => !x.Equals(word, StringComparison.OrdinalIgnoreCase));
            return (lineAsArray.Length - changedLine.Count(), string.Join(" ", changedLine));
        }

        public static int CountRepitions(IEnumerable<string> line, string word)
        {
            return line.Count(x => x.Equals(word, StringComparison.OrdinalIgnoreCase));
        }
    }
}

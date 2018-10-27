using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercises
{
    public class Program
    {
        static void Main(string[] args)
        {
            var stopWords = File.ReadAllText("stop_words.txt");
            var secondMem = new List<SecondMem>();
            var inputLines = File.ReadAllLines("input.txt");
            foreach (var line in inputLines)
            {
                var data = new OldTimes
                {
                    StopWords = stopWords.Split(',').ToList()
                };
                data.NewLine = line;
                if (data.NewLine == "")
                    break;
                if (data.NewLine[data.NewLine.Length - 1] != '\n')
                {
                    data.NewLine += "\n";
                }
                foreach (var c in data.NewLine)
                {
                    if (data.StartCharIndex == null)
                    {
                        if (Char.IsLetterOrDigit(c))
                        {
                            data.StartCharIndex = data.IndexOfChars;
                        }
                    }
                    else
                    {
                        if (!Char.IsLetterOrDigit(c))
                        {
                            data.WordIsFound = false;
                            data.IndexOfChars++;
                            int length = data.IndexOfChars - data.StartCharIndex.GetValueOrDefault();
                            data.Word = line.Substring(data.StartCharIndex.GetValueOrDefault(), length);

                            if (data.Word.Length > 2 && !data.StopWords.Contains(data.Word))
                            {
                                if (secondMem.Any(x => x.Word == data.Word))
                                {
                                    secondMem.FirstOrDefault(x => x.Word == data.Word).Frequancy++;
                                }
                                else
                                {
                                    secondMem.Add(new SecondMem
                                    {
                                        Frequancy = 1,
                                        Word = data.Word
                                    });
                                }

                            }
                            data.StartCharIndex = null;
                        }
                        data.IndexOfChars += 1;
                    }
                }

            }

            // Process line
            var Top25 = secondMem.OrderByDescending(x => x.Frequancy).ToList();
            foreach(var entry in Top25)
            {

                Console.WriteLine(entry.Word+" - "+entry.Frequancy);
            }
            Console.ReadKey();
        }
    }

    public class OldTimes
    {
        public List<string> StopWords { get; set; }
        public string NewLine { get; set; }
        public int? StartCharIndex { get; set; } = null;
        public int IndexOfChars { get; set; } = 0;
        public bool WordIsFound { get; set; } = false;
        public string Word { get; set; } = "";
        public string WorldTemp { get; set; } = "";
        public int Frequesncy { get; set; } = 0;
    }

    public class SecondMem
    {
        public string Word { get; set; }
        public int Frequancy { get; set; }
    }
}

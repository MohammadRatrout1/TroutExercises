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
            var stopWordsStr = File.ReadAllText("stop_words.txt");
            var stopWords = stopWordsStr.Split(',').ToList();
            var inputLines = File.ReadAllLines("input.txt");
            var i = 0;
            int? start_char = null;
            var found = false;
            var wordsFrequence = new List<WordsFrequency>();
            foreach (var line in inputLines)
            {
                start_char = null;
                i = 0;
                foreach (var c in line)
                {
                    if (start_char == null)
                    {
                        if (char.IsLetterOrDigit(c))
                        {
                            start_char = i;
                        }
                    }
                    else
                    {
                        if (!char.IsLetterOrDigit(c))
                        {
                            found = false;
                            var length = i - start_char.GetValueOrDefault();
                            var word = line.Substring(start_char.GetValueOrDefault(), length).ToLower();
                            if (!stopWords.Contains(word))
                            {
                                if (wordsFrequence.Any(x => x.Word == word))
                                {
                                    wordsFrequence.FirstOrDefault(x => x.Word == word).Freq++;
                                }
                                else
                                {
                                    wordsFrequence.Add(new WordsFrequency {
                                        Word = word,
                                        Freq = 1
                                    });
                                }
                            }
                            start_char = null;
                        }
                    }
                    i++;
                }
            }
            foreach (var entry in wordsFrequence)
            {

                Console.WriteLine(entry.Word + " - " + entry.Freq);
            }
            Console.ReadKey();
        }
    }

    public class WordsFrequency
    {
        public string Word { get; set; }
        public int Freq { get; set; }

    }
}

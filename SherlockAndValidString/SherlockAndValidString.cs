using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit.Abstractions;

namespace algorithm
{
    class SherlockAndValidString
    {
        public SherlockAndValidString(ITestOutputHelper output)
        {
            this.output = output;
        }

        ITestOutputHelper output;

        public string IsValid(string s) {
            var stringInfo = s.Length > 10
                ? s.Substring(0, 10) + "..."
                : s;
            output.WriteLine($"SherlockAndValidString.IsValid: {stringInfo}");

            var letterOccurrences = CountOccurrence(s.ToCharArray());

            DebugInfo("Letter & Count", letterOccurrences, output);

            var numberOccurrences = CountOccurrence(letterOccurrences.Select(x => x.Value));
            var totalNumbers = numberOccurrences.Count();

            DebugInfo("Letter-Count & Occurrence", numberOccurrences, output);

            if (numberOccurrences.Count == 1)
            {
                return "YES";
            }
            else if (numberOccurrences.Count == 2)
            {
                // letter occured once
                if (numberOccurrences.Any(x => x.Value == 1 && x.Key == 1))
                {
                    return "YES";
                }
                else
                {
                    // letter occurred more than once: check if the different occurence with others just 1
                    var keys = numberOccurrences.Select(x => x.Key).ToArray();
                    var values = numberOccurrences.Select(x => x.Value);
                    if (values.Any(v => v == 1) && (Math.Abs(keys[0] - keys[1]) <= 1))
                    {
                        return "YES";
                    }
                }
            }

            return "NO";
        }

        static void DebugInfo<T>(string message, Dictionary<T, int> dict, ITestOutputHelper output)
        {
            var debugInfo = string.Join("\r\n", dict.Select(x => $"\t\t{x.Key}: {x.Value}"));
            output.WriteLine($"\t{message}: \r\n{debugInfo}");
        }

        static Dictionary<T, int> CountOccurrence<T>(IEnumerable<T> data)
        {
            var dictionary = new Dictionary<T, int>();
            foreach (var letter in data)
            {
                if (dictionary.ContainsKey(letter))
                {
                    dictionary[letter]++;
                }
                else
                {
                    dictionary.Add(letter, 1);
                }
            }

            return dictionary;
        }
    }
}

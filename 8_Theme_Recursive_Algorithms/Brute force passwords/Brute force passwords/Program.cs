using System;
using System.Collections.Generic;
using System.Linq;

namespace Brute_force_passwords
{
    public class CaseAlternatorTask
    {
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var result = new List<string>();
            AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
            return result;
        }

        static void AlternateCharCases(char[] word, int startIndex, List<string> result)
        {
            var subWord = word.ToArray();
            if (startIndex == word.Length)
            {
                if (!result.Contains(new string(word))) result.Add(new string(word));
                return;
            }
            if (char.IsLetter(word[startIndex]))
                word[startIndex] = char.ToLower(word[startIndex]);
            AlternateCharCases(word, startIndex + 1, result);
            if (char.IsLetter(word[startIndex]))
                word[startIndex] = char.ToUpper(word[startIndex]);
            AlternateCharCases(word, startIndex + 1, result);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            foreach(var result in CaseAlternatorTask.AlternateCharCases("ab42"))
            Console.WriteLine(result);
        }
    }
}

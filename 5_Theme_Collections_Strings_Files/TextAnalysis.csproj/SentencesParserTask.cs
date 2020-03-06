using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {           
            var sentancePattern = @"(['\-\—\p{L}]+[\s,…\”\“#\$\+=\d\t‘\^]*)+[\.\!\?\)\(;]*";
            var sentences = Regex.Matches(text, sentancePattern, RegexOptions.Multiline);
            var stringArray = sentences.Cast<Match>().Select(m => m.Value).ToArray();
            var wordPattern = @"[\p{L}|']+";
			var sentencesList = new List<List<string>>();
            foreach (var sentance in stringArray)
            {
                var words = Regex.Matches(sentance.ToLower(), wordPattern, RegexOptions.Multiline);
                sentencesList.Add(new List<string>(words.Cast<Match>().Select(m => m.Value).ToArray()));
            }
            return sentencesList;
        }
    }
}
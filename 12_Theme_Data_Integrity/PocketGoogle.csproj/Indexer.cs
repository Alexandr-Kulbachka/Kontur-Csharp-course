using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PocketGoogle
{
    public class Word
    {
        public string Value;
        public int Position;
        public Word(string value, int position)
        {
            Value = value;
            Position = position;
        }
    }

    public class IndexDataStructure : Dictionary<string, Dictionary<int, List<int>>>
    {
        public void AddData(int id, Word word)
        {
            if (ContainsKey(word.Value))
                if (this[word.Value].ContainsKey(id))
                    this[word.Value][id].Add(word.Position);
                else
                    this[word.Value].Add(id, new List<int>() { word.Position });
            else
                Add(word.Value, new Dictionary<int, List<int>> { { id, new List<int> { word.Position } } });
        }

        public List<int> ReturnIds(string word)
        {
            var allIds = new List<int>();
            if (ContainsKey(word))
                foreach (var id in this[word].Keys)
                    allIds.Add(id);
            return allIds;
        }

        public void DeleteText(int textId)
        {
            foreach (var word in Keys)
                if (this[word].ContainsKey(textId)) this[word].Remove(textId);
        }

        public List<int> FindWordPositionsInText(int textId, string word)
        {
            var allWordPositionsInText = new List<int>();
            if (ContainsKey(word) && this[word].ContainsKey(textId))
                allWordPositionsInText = this[word][textId].ToList();
            return allWordPositionsInText;
        }
    }

    public class TextParser
    {
        public List<Word> Parse(string documentText)
        {
            string wordPattern = @"[\p{L}|']+";
            var wordsFromText = new List<Word>();
            foreach (Match match in Regex.Matches(documentText, wordPattern, RegexOptions.Multiline))
                wordsFromText.Add(new Word(match.Value, match.Index));
            return wordsFromText;
        }
    }

    public class Indexer : IIndexer
    {
        private IndexDataStructure statistics;
        public Indexer()
        {
            statistics = new IndexDataStructure();
        }

        public void Add(int id, string documentText)
        {
            var textParser = new TextParser();
            var allWordsFromText = textParser.Parse(documentText);
            foreach (var word in allWordsFromText)
                statistics.AddData(id, word);
        }

        public List<int> GetIds(string word)
        {
            return statistics.ReturnIds(word);
        }

        public List<int> GetPositions(int id, string word)
        {
            return statistics.FindWordPositionsInText(id, word);
        }

        public void Remove(int id)
        {
            statistics.DeleteText(id);
        }
    }
}
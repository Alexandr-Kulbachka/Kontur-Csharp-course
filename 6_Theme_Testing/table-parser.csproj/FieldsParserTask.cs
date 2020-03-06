using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Linq;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        [TestCase("text", new[] { "text" })]
        [TestCase("hello world", new[] { "hello", "world" })]
        [TestCase("hello  world", new[] { "hello", "world" })]
        [TestCase(" hello world!", new[] { "hello", "world!" })]
        [TestCase("hello world", new[] { "hello", "world" })]
        [TestCase("\"'\"", new[] { "'" })]
        [TestCase("\'\"\'", new[] { "\"" })]
        [TestCase("\"y", new[] { "y" })]
        [TestCase(@"""\\""", new[] { "\\" })]
        [TestCase(@"""abc ", new[] { "abc " })]
        [TestCase("\"", new[] { "" })]
        [TestCase("he\"llo\"world", new[] { "he", "llo", "world" })]
        [TestCase("\"\\\"\\'", new[] { "\"'" })]
        [TestCase("\'\\\'\"", new[] { "'\"" })]
        [TestCase("", new string[0])]
        // Вставляйте сюда свои тесты
        public static void RunTests(string input, string[] expectedOutput)
        {
            // Тело метода изменять не нужно
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        public static HashSet<char> Quotes = new HashSet<char>() { '"', '\'' };
        // При решении этой задаче постарайтесь избежать создания методов, длиннее 10 строк.
        // Подумайте как можно использовать ReadQuotedField и Token в этой задаче.
        public static List<Token> ParseLine(string line)
        {
            var resultList = new List<Token>();
            var index = 0;
            while (index < line.Length)
            {
                if (line[index] == ' ') index++;
                else
                {
                    resultList.Add(Quotes.Contains(line[index]) ?
                                   ReadQuotedField(line, index) : ReadField(line, index));
                    index = resultList.Last().GetIndexNextToToken();
                }
            }
            return resultList; // сокращенный синтаксис для инициализации коллекции.
        }

        private static Token ReadField(string line, int startIndex)
        {
            var counterOfSymbols = 0;
            for (int i = startIndex; i < line.Length; i++)
                if (line[i] == ' ' || Quotes.Contains(line[i]))
                    break;
                else counterOfSymbols++;
            return new Token(line.Substring(startIndex, counterOfSymbols), startIndex, counterOfSymbols);
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}
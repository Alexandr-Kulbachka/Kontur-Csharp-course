using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase("' ggt'", 0, " ggt", 6)]
        [TestCase("'br", 0, "br", 3)]
        [TestCase(@"'abc \\", 0, "abc \\", 7)]
        [TestCase(@"\'22'", 0, "", 2)]
        [TestCase(@"'abc,,'''\'", 0, "abc,,", 7)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(actualToken, new Token(expectedValue, startIndex, expectedLength));
        }
        // Добавьте свои тесты
    }

    class QuotedFieldTask
    {
        public static int CalculateTheLength(char startSymbol, char endSymbol, int lengthOfToken)
        {
            return startSymbol == endSymbol ? lengthOfToken + 2 : lengthOfToken + 1;
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            var resultLine = new StringBuilder();
            var openingQuote = line[startIndex];
            var lengthOfToken = 0;
            var index = startIndex;
            if (openingQuote == '"' || openingQuote == '\'')
                while (index < line.Length - 1 && line[++index] != openingQuote)
                {
                    if (line[index] == '\\')
                    {
                        index++;
                        lengthOfToken++;
                    }
                    resultLine.Append(line[index]);
                    lengthOfToken++;
                }
            lengthOfToken = CalculateTheLength(openingQuote, line[index], lengthOfToken);
            return new Token(resultLine.ToString(), startIndex, lengthOfToken);
        }
    }
}
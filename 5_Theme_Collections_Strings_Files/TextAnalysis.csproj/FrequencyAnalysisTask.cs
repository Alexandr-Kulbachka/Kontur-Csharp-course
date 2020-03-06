using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {

        public static Dictionary<string, Dictionary<string, int>> GetAllBgramsFromText(List<List<string>> text) 
        {
            var allBgrams = new Dictionary<string, Dictionary<string, int>>();

            return allBgrams;
        }

        public static Dictionary<string, Dictionary<string, int>> GetAllTrigramsFromText(List<List<string>> text)
        {
            var allTrigrams = new Dictionary<string, Dictionary<string, int>>();

            return allTrigrams;
        }

        public static Dictionary<string, Dictionary<string, int>> GetAllN_GramsFromText(List<List<string>> text) 
        {
            var allN_Grams = GetAllBgramsFromText(text)
                .Union(GetAllTrigramsFromText(text))
                .ToDictionary(s => s.Key, s => s.Value); 
            return allN_Grams;
        }

        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {

            var result = new Dictionary<string, string>();
            //...
            return result;
        }
   }
}
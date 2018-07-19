using System;
using System.Collections.Generic;
using System.Linq;

// * Дан фрагмент программы:
// Dictionary<string, int> dict = new Dictionary<string, int>()
//  {
//    {"four",4 },
//    {"two",2 },
//    { "one",1 },
//    {"three",3 },
//  };
// var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
//     foreach (var pair in d)
//    {
//      Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
//    }
//        а) Свернуть обращение к OrderBy с использованием лямбда-выражения $.
//        б) * Развернуть обращение к OrderBy с использованием делегата Predicate<T>.


namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = new Dictionary<string, int>() { { "four", 4 }, { "two", 2 }, { "one", 1 }, { "three", 3 } };
            
            foreach (var pair in GetSortedDictionaryByLambda(dict))
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
        }

        private static Dictionary<string, int> GetSortedDictionaryByLambda(Dictionary<string, int> dic)
        {
            return dic.OrderBy(d => d.Value).Select(d => new { key = d.Key, value = d.Value }).ToDictionary(d => d.key, d => d.value);
        }
    }
}
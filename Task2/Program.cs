using System;
using System.Collections.Generic;
using System.Linq;

// Дана коллекция List<T>​. Требуется подсчитать, сколько раз каждый элемент встречается в
// данной коллекции:
// a. для целых чисел;
// b. * для обобщенной коллекции;
// c. ** используя Linq

// Александр Коновальцев

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            var listInt = new List<int> { 1, 2, 3, 4, 5, 2, 3, 4, 6, 7, 8, 9, 8, 9, 10 };
            var listString = new List<string> { "hello", "hi", "hello", "hi", "hello" };
            GetCountOfEveryElementInGenericCollection(listInt);
            GetCountOfEveryElementInGenericCollection(listString);

        }

        private static void GetCountOfEveryElementInGenericCollection<T>(List<T> list)
        {
            var result = list.GroupBy(i => i);
            foreach (var i in result) Console.WriteLine($@"element {i.Key} is repeated {i.Count()} times");
        }
    }
}
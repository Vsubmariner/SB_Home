using System;
using System.Linq;

namespace Homework5_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //создание массива слов
            string[] words = { };
            //создание и заполнение массива символов-разделителей
            char[] delimeters = new char[] { ' ', '.', ',', '-', '!', ':' };

            //цикл ожидания ввода пользователя выполняется до тех пор, пока не будет введена не нулевая строка, не состоящая только из разделителей
            while (words.Length == 0)
            {
                Console.WriteLine("Введите предложение из нескольких слов");
                string str = Console.ReadLine().Trim(); //убираем непечатаемые символы из строки
                words = str.Split(delimeters, StringSplitOptions.RemoveEmptyEntries); //разбиваем строку на массив слов
            }

            //Выводим самое короткое слово
            var word = GetShortestWord(words);
            Console.WriteLine("Самое короткое слово в этом предложении: " + word);

            //Выводим 3 самых длинных слова через запятую
            string[] longestWords = GetLongestWords(words, 3);
            Console.WriteLine($"Самые длинные три слова в этом предложении: {String.Join(", ", longestWords)}");
            Console.ReadLine();
        }

        /// <summary>
        /// Принимает массив слов и количество самых длинных слов, которое нужно вывести. Выводит заданное количество самых длинных слов.
        /// </summary>
        /// <param name="inputArray"></param>
        /// <param name="numberOfWordsToDisplay"></param>
        /// <returns></returns>
        private static string[] GetLongestWords(string[] inputArray, int numberOfWordsToDisplay)
        {
            //изменяем количество слов, которые нужно вывести в случае, если входящая строка
            //короче, чем стандартное значение
            if (numberOfWordsToDisplay > inputArray.Length)
            {
                numberOfWordsToDisplay = inputArray.Length;
            }

            //создание переменных для массива длинных слов и для хранения текущего длинного слова
            string[] longestWords = new string[numberOfWordsToDisplay];
            string longestWord;

            //вложенный цикл для перебора слов по убыванию длины
            for (int i = 0; i < numberOfWordsToDisplay; i++)
            {
                longestWord = "";
                foreach (var word in inputArray)
                {
                    if (word.Length > longestWord.Length && !longestWords.Contains(word))
                    {
                        longestWord = word;
                    }
                }

                longestWords[i] = longestWord;
            }

            return longestWords;
        }

        /// <summary>
        /// Принимает массив слов и возвращает самое короткое из слов в массиве
        /// </summary>
        /// <param name="inputArray"></param>
        /// <returns></returns>
        static private string GetShortestWord(string[] inputArray)
        {
            string shortestWord = inputArray[0];
            foreach (var w in inputArray)
            {
                if (shortestWord.Length > w.Length)
                {
                    shortestWord = w;
                }
            }

            return shortestWord;
        }
    }
}


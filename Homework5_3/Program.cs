using System;

namespace Homework5_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку, в которой будут удалены все повторяющиеся символы");
            var inputString = Console.ReadLine();

            //Вызов метода удаления повторяющихся символов
            var outputString = RemoveDuplicateCharacters(inputString);
            Console.WriteLine("Результат удаления повторяющихся символов: " + outputString);
        }

        /// <summary>
        /// Принимает строку и возвращает новую строку, в которой удалены идущие подряд дублирующиеся символы.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string RemoveDuplicateCharacters(string input)
        {
            //Преобразование входящей строки в массив символов
            char[] inputArray = input.ToCharArray();
            //Создание массива символов для формирования исходящей строки
            char[] outputArray = new char[0];

            //Цикл для поиска повторяющихся символов и заполнения исходящего массива символов
            for (int i = 0; i < inputArray.Length; i++)
            {
                //Если за текущим символом в массиве не следует такой же символ,
                //увеличить на 1 массив исходящих символов и поместить в него текущий символ
                if (i == inputArray.Length - 1 || Char.ToLower(inputArray[i]) != Char.ToLower(inputArray[i + 1]))
                {
                    Array.Resize(ref outputArray, outputArray.Length + 1);
                    outputArray[outputArray.Length - 1] = inputArray[i];
                }
            }

            return new string(outputArray);

        }
    }
}

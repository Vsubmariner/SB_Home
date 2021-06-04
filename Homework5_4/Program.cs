using System;

namespace Homework5_4
{
    class Program
    {
        static void Main(string[] args)
        {
            bool correctEntry = false;
            string numberStr;
            int[] nums = new int[0];

            do
            {
                Console.WriteLine("Введите последовательность целых чисел, разделяя их пробелами или запятыми");
                numberStr = Console.ReadLine();
                if (!String.IsNullOrEmpty(numberStr))
                {
                    string[] splitted = numberStr.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    Console.WriteLine("Принята последовательность: ");

                    for (int i = 0; i < splitted.Length; i++)
                    {
                        bool success = int.TryParse(splitted[i], out int num);
                        if (success)
                        {
                            Array.Resize(ref nums, nums.Length + 1);
                            nums[nums.Length - 1] = num;
                            Console.Write(nums[nums.Length - 1] + " ");
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if (nums.Length == 1)
                    {
                        Console.WriteLine("\nПоследовательность должна содержать минимум 2 числа");
                        continue;
                    }

                    if (nums.Length > 0)
                    {
                        correctEntry = true;
                    }
                    else
                    {
                        Console.WriteLine("Принятая последовательность не содержит чисел");
                    }
                }
                else
                {
                    Console.WriteLine("Введена пустая строка");
                    Array.Resize(ref nums, 0);
                }

            } while (!correctEntry);

            Console.WriteLine();
            Console.WriteLine(CheckProgression(nums));

        }

        /// <summary>
        /// Принимает массив чисел типа int, определяет, является ли данная последовательность чисел в массиве прогрессией и если да,
        /// то какой: арифметической или геометрической
        /// </summary>
        /// <param name="inputArray"></param>
        private static string CheckProgression(int[] inputArray)
        {
            var multiplier = Math.Abs(inputArray[1] - inputArray[0]);
            var divider = inputArray[1] / inputArray[0];

            int progressionType = 0;

            for (int i = 0; i < inputArray.Length - 1; i++)
            {
                if (Math.Abs(inputArray[i + 1] - inputArray[i]) == multiplier)
                {
                    progressionType = 1;
                }
                else
                {
                    progressionType = 0;
                    break;
                }
            }

            if (progressionType != 1)
            {
                for (int i = 0; i < inputArray.Length - 1; i++)
                {
                    if (inputArray[i + 1] / inputArray[i] == divider)
                    {
                        progressionType = 2;
                    }
                    else
                    {
                        progressionType = 0;
                        break;
                    }
                }
            }

            switch (progressionType)
            {
                case 1:
                    return "Данная последовательность является арифметической прогрессией";
                case 2:
                    return "Данная последовательность является геометрической прогрессией";
                default:
                    return "Данная последовательность не является прогрессией";
            }

        }
    }
}

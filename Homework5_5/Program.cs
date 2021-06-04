using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework5_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вычисляем функцию Аккермана и убиваем стек :)");
            Console.WriteLine("Введите число m. Это должно быть целое неотрицательное число.");
            int inputM = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите число n. Это должно быть целое неотрицательное число.");
            int inputN = Convert.ToInt32(Console.ReadLine());

            int result = CalculateAkkermann(inputM, inputN);

            Console.WriteLine("Получился результат: " + result);
        }

        /// <summary>
        /// Принимает два входящих параметра функции Аккермана, возвращает результат вычислений рекурсивной функции.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static int CalculateAkkermann(int m, int n)
        {
            if (m == 0) return n += 1;

            if (m > 0 && n == 0) return CalculateAkkermann(m - 1, 1);

            if (m > 0 && n > 0) return CalculateAkkermann(m - 1, CalculateAkkermann(m, n - 1));

            return CalculateAkkermann(m, n);
        }
    }
}

using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Homework6
{
    public class Program
    {
        public static void Main()
        {
            int nNumber = 1;
            string readPath = @".\input.txt";
            string writePath = @".\result.txt";
            string archivePath = @".\result.zip";
            int lowNLimit = 1;
            int maxNLimit = 1_000_000_000;

            CheckInputFileExists(readPath);

            if (N_NumberIsReadable(ref nNumber, readPath, lowNLimit, maxNLimit))
            {
                Console.WriteLine($"Число N равняется {nNumber}");
                double numberOfGroups = GetGroupsCount(nNumber);

                //Выводим количество групп или выводим количество групп и пишем их в файл, по выбору пользователя
                Console.WriteLine("\nХотите ли вы заполнить файл result.txt группами чисел? Y/N");
                Console.WriteLine("Если вы ответите нет, на экран будет выведено только количество групп");
                ConsoleKeyInfo confirmFileCreation = Console.ReadKey(true);
                if (confirmFileCreation.Key != ConsoleKey.Y)
                {
                    Console.WriteLine($"\nКоличество групп чисел: {numberOfGroups}");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"\nКоличество групп чисел: {numberOfGroups}");
                    InitOutputFileCreation(nNumber, writePath, numberOfGroups);

                    //Архивируем результирующий файл, если пользователь хочет это сделать, пишем размеры результирующего файла и архива
                    Console.WriteLine("\nХотите ли вы поместить файл result.txt в архив? Y/N");
                    ConsoleKeyInfo confirmArchiveCreation = Console.ReadKey(true);
                    if (confirmArchiveCreation.Key == ConsoleKey.Y)
                    {
                        ArchiveResultAndWriteFileSizes(writePath, archivePath);
                    }
                }
            }
        }

        /// <summary>
        /// Архивирует результирующий файл по указанному пути, создает архив по указанному пути и выводит размер обеих файлов
        /// </summary>
        /// <param name="writePath"></param>
        /// <param name="archivePath"></param>
        private static void ArchiveResultAndWriteFileSizes(string writePath, string archivePath)
        {
            Console.WriteLine("Выполняется архивирование файла result.txt, пожалуйста подождите...");
            if (File.Exists(archivePath))
            {
                Console.WriteLine("Обнаружен существующий файл archive.zip. Он будет удалён.");
                File.Delete(archivePath);
            }

            ArchiveResultFile(writePath, archivePath);

            Console.WriteLine("\nАрхивирование завершено");
            long fileLengthResult = new FileInfo(writePath).Length;
            long fileLengthArchive = new FileInfo(archivePath).Length;

            Console.WriteLine($"Размер файла result.txt: {fileLengthResult} байт");
            Console.WriteLine($"Размер файла archive.zip: {fileLengthArchive} байт");

            Console.ReadLine();
        }

        /// <summary>
        /// Удаляет существующий файл результатов, выводит информацию для пользователя на консоль, инициирует запись нового файла результатов и выводит время выполнения на консоль
        /// </summary>
        /// <param name="nNumber"></param>
        /// <param name="writePath"></param>
        /// <param name="numberOfGroups"></param>
        private static void InitOutputFileCreation(int nNumber, string writePath, double numberOfGroups)
        {
            Console.WriteLine("Выполняется запись групп чисел в файл result.txt, пожалуйста подождите...");

            if (File.Exists(writePath))
            {
                Console.WriteLine("Обнаружен существующий файл result.txt. Он будет удалён.");
                File.Delete(writePath);
            }

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            FillOutputFile(nNumber, writePath);
            stopwatch.Stop();

            Console.WriteLine("\nЗапись в файл result.txt завершена");
            Console.WriteLine("Время выполнения " + stopwatch.Elapsed);
        }

        /// <summary>
        /// Определяет количество групп неделящихся друг на друга чисел от 1 до N
        /// </summary>
        /// <param name="nNumber"></param>
        /// <returns></returns>
        private static double GetGroupsCount(int nNumber)
        {
            double numberOfGroups = Math.Floor(Math.Log(Convert.ToDouble(nNumber), 2)) + 1;
            return numberOfGroups;
        }

        /// <summary>
        /// Валидирует содержимое входящего файла на предмет наличия числа N и вхождение числа N в заданные границы. Возвращает true если проверка пройдена.
        /// </summary>
        /// <param name="nNumber"></param>
        /// <param name="readPath"></param>
        /// <param name="lowNLimit"></param>
        /// <param name="maxNLimit"></param>
        /// <returns></returns>
        private static bool N_NumberIsReadable(ref int nNumber, string readPath, int lowNLimit, int maxNLimit)
        {
            bool nNumberReadable = false;
            using (StreamReader sr = new StreamReader(readPath, System.Text.Encoding.Default))
            {
                Console.WriteLine("Файл input.txt считывается");
                if (Int32.TryParse(sr.ReadLine(), out int result))
                {
                    if (result >= lowNLimit && result <= maxNLimit)
                    {
                        nNumber = result;
                        nNumberReadable = true;
                    }
                    else
                    {
                        Console.WriteLine("Значение числа N лежит за пределами диапазона от одного до миллиарда");
                        Console.WriteLine("Замените значение в файле на корректное");
                        Console.ReadLine();
                        sr.Dispose();
                    }
                }
                else
                {
                    Console.WriteLine("Число N не может быть считано");
                    Console.WriteLine("Замените значение в файле на корректное");
                    Console.ReadLine();
                    sr.Dispose();
                }
            }

            return nNumberReadable;
        }

        /// <summary>
        /// Проверка того, что входящий файл существует. Цикл выполняется, пока файл не найден, или пока пользвоатель не закрыл приложение.
        /// </summary>
        /// <param name="readPath"></param>
        private static void CheckInputFileExists(string readPath)
        {
            while (!File.Exists(readPath))
            {
                Console.WriteLine("Файл input.txt отсутствует в папке программы. Пожалуйста, создайте файл input.txt с целым числом от 1 до миллиарда включительно на первой строке в той же папке, где находится .exe файл программы");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Получает путь к результирующему файлу по указанному пути в первом параметре и создает файл архива по указанному пути во втором параметре
        /// </summary>
        /// <param name="writePath"></param>
        /// <param name="archivePath"></param>
        private static void ArchiveResultFile(string writePath, string archivePath)
        {
            using (FileStream fileStream = new FileStream(archivePath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
            {
                char[] sep = new char[] { '\\' };
                string archiveFileName = writePath.Split(sep)[1];
                archive.CreateEntryFromFile(writePath, archiveFileName);
            }
        }

        /// <summary>
        /// Записывает группы чисел не делящихся друг на друга в файл по строку на каждую группу.
        /// Принимает максимальное число N до которого осуществляется запись и путь, по которому будет записан файл.
        /// </summary>
        /// <param name="nNumber"></param>
        /// <param name="writePath"></param>
        private static void FillOutputFile(int nNumber, string writePath)
        {
            StringBuilder sb = new StringBuilder();
            int powOfTwo = 1;
            int maxNumbersInOneString = 100_000_000;

            using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
            {
                for (int i = 1; i <= nNumber; i++)
                {
                    if (sb.Length >= maxNumbersInOneString)
                    {
                        sw.Write(sb.ToString());
                        sb.Clear();
                    }

                    if (i >= powOfTwo * 2)
                    {
                        sw.WriteLine(sb.ToString());

                        sb.Clear();
                        powOfTwo *= 2;
                    }

                    sb.Append($"{i} ");

                    if (i == nNumber)
                    {
                        sw.Write(sb.ToString());
                    }
                }
            }
        }
    }
}


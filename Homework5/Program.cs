using System;

namespace Homework5_1
{
    class Program
    {
        const int Indent = 4; //Стандартное значение отступов в выводимых в консоль матрицах, в символах строки
        const string MatrixBoundarySymbol = "|"; //Символ границ матриц

        //Перечисление для выбора типа операции над матрицами - сложения или вычитания
        enum MathOperation
        {
            Summation,
            Deduction
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Пожалуйста, выберите тип операции с матрицами:");
            Console.WriteLine("Введите 1, чтобы умножить матрицу на число");
            Console.WriteLine("Введите 2, чтобы сложить две матрицы");
            Console.WriteLine("Введите 3, чтобы вычесть матрицу из матрицы");
            Console.WriteLine("Введите 4, чтобы умножить две матрицы");
            Console.WriteLine("Любой другой ввод завершит выполнение программы\n");

            string selection = Console.ReadLine();
            switch (selection)
            {
                case "1":
                    PerformMatrixByNumberMultiplication();
                    break;

                case "2":
                    PerformMatrixSumOrDeduct(MathOperation.Summation);
                    break;

                case "3":
                    PerformMatrixSumOrDeduct(MathOperation.Deduction);
                    break;

                case "4":
                    PerformMultiplyMatrixes();
                    break;
            }
        }

        /// <summary>
        /// Выполняет операцию умножения матрицы на число с вводом исходных данных и выводом форматированного результата.
        /// </summary>
        private static void PerformMatrixByNumberMultiplication()
        {
            Console.WriteLine("Введите количество строк исходной матрицы");
            int row = ReadMatrixDimension();
            Console.WriteLine("Введите количество столбцов исходной матрицы");
            int col = ReadMatrixDimension();
            Console.WriteLine("Введите число, на которое нужно умножить матрицу");
            int multiplier = ReadMultiplier();
            Console.ResetColor();
            Console.Clear();

            //Создание массивов матриц
            int[,] matrixOne = new int[row, col];
            int[,] resultMatrix;

            Random rnd = new Random();

            //Заполнение входящей матрицы случайными числами
            FillMatrix(rnd, matrixOne);

            //Вызов метода умножения матрицы на число
            resultMatrix = MultiplyMatrixByNumber(matrixOne, multiplier);

            //Задание строк, выводимых перед первой матрицей и между исходной и результирующей матрицами
            string startLine = multiplier + " * ";
            string equalityLine = " =  ";

            //Определяем ширину матриц в символах
            int matrixWidth = GetMatrixWidth(col);

            //Определяем общую ширину вывода
            int outputWidth = startLine.Length + matrixWidth + equalityLine.Length + matrixWidth;

            //Проверка необходимости увеличения окна консоли и возможности вывода если максимальный размер консоли превышен, вывод результата, если проверка пройдена
            if (TryResizeConsole(outputWidth, row))
            {
                //Выводим результат в виде двух матриц если вывод возможен
                Console.ForegroundColor = ConsoleColor.Green;
                WriteMatrix(matrixOne, startLine, startLine.Length, row);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                WriteMatrix(resultMatrix, equalityLine, startLine.Length + matrixWidth + equalityLine.Length, row);
                Console.ResetColor();
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Выполняет операцию сложения или вычитания двух матриц с вводом данных и выводом форматированного результата. Принимает значение перечисления mathOperation. 
        /// Если mathOperation имеет значение Summation, будет выполнена операция сложения, если Deduction, будет выполнено вычитания двух матриц.
        /// </summary>
        /// <param name="mathOperation"></param>
        private static void PerformMatrixSumOrDeduct(MathOperation mathOperation)
        {
            Console.WriteLine("Введите количество строк матриц");
            int row = ReadMatrixDimension();
            Console.WriteLine("Введите количество столбцов матриц");
            int col = ReadMatrixDimension();
            Console.ResetColor();
            Console.Clear();

            //Инициализация двух входящих матрицы и создание массива результирующей матрицы
            int[,] matrixOne = new int[row, col];
            int[,] matrixTwo = new int[row, col];
            int[,] resultMatrix = new int[row, col];

            Random rnd = new Random();

            //Заполнение входящих матриц случайными значениями
            FillMatrix(rnd, matrixOne);
            FillMatrix(rnd, matrixTwo);

            //Вызов методов сложения матриц или вычитания матриц
            switch (mathOperation)
            {
                case MathOperation.Summation:
                    resultMatrix = SumOrDeductTwoMatrixes(matrixOne, matrixTwo, MathOperation.Summation);
                    break;
                case MathOperation.Deduction:
                    resultMatrix = SumOrDeductTwoMatrixes(matrixOne, matrixTwo, MathOperation.Deduction);
                    break;
            }

            //Задание строк, выводимых перед каждой из трех матриц
            string startLine = "  ";
            string operationLine = "+";
            string equalityLine = " =  ";

            //Выбираем какой символ выводить между двумя входящими матрицами
            switch (mathOperation)
            {
                case MathOperation.Summation:
                    operationLine = " +  ";
                    break;
                case MathOperation.Deduction:
                    operationLine = " -  ";
                    break;
            }

            //Определяем ширину матрицы
            int matrixWidth = MatrixBoundarySymbol.Length + Indent + col * Indent + MatrixBoundarySymbol.Length;

            //Определяем общую ширину вывода
            int outputWidth = startLine.Length + matrixWidth + operationLine.Length + matrixWidth + equalityLine.Length + matrixWidth;

            //Проверка необходимости увеличения окна консоли и возможности вывода если максимальный размер консоли превышен, вывод результата, если проверка пройдена
            if (TryResizeConsole(outputWidth, row))
            {
                //Выводим результат в виде трех матриц, если вывод возможен
                Console.ForegroundColor = ConsoleColor.Green;
                WriteMatrix(matrixOne, startLine, startLine.Length, row);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                WriteMatrix(matrixTwo, operationLine, startLine.Length + matrixWidth + operationLine.Length, row);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Magenta;
                WriteMatrix(resultMatrix, equalityLine, startLine.Length + matrixWidth + operationLine.Length + matrixWidth + equalityLine.Length, row);
                Console.ResetColor();
            }

            Console.ReadLine();

        }

        /// <summary>
        /// Выполняет операцию умножения двух матриц с вводом исходных данных и выводом форматированного результата
        /// </summary>
        private static void PerformMultiplyMatrixes()
        {
            Console.WriteLine("Введите количество строк первой матрицы");
            int rowOne = ReadMatrixDimension();
            Console.WriteLine("Введите количество столбцов первой матрицы");
            int colOne = ReadMatrixDimension();
            int rowTwo = colOne;
            Console.WriteLine("Введите количество столбцов второй матрицы. Количество строк определяется автоматически и будет равно количеству столбцов первой матрицы.");
            int colTwo = ReadMatrixDimension();

            Console.ResetColor();
            Console.Clear();

            //Инициализация двух входящих матрицы и создание массива результирующей матрицы
            int[,] matrixOne = new int[rowOne, colOne];
            int[,] matrixTwo = new int[rowTwo, colTwo];
            int[,] resultMatrix;

            Random rnd = new Random();

            //Заполнение входящих матриц случайными значениями
            FillMatrix(rnd, matrixOne);
            FillMatrix(rnd, matrixTwo);

            resultMatrix = MultiplyTwoMatrix(matrixOne, matrixTwo);

            //Задание строк, выводимых перед каждой из трех матриц
            string startLine = "  ";
            string operationLine = " *  ";
            string equalityLine = " =  ";

            //Определяем ширину первой матрицы
            int matrixWidthOne = MatrixBoundarySymbol.Length + Indent + colOne * Indent + MatrixBoundarySymbol.Length;

            //Определяем ширину второй матрицы
            int matrixWidthTwo = MatrixBoundarySymbol.Length + Indent + colTwo * Indent + MatrixBoundarySymbol.Length;

            //Определяем ширину результирующей матрицы
            int matrixWidthResult = MatrixBoundarySymbol.Length + Indent + colTwo * Indent + MatrixBoundarySymbol.Length;

            //Определяем общую ширину вывода
            int outputWidth = startLine.Length + matrixWidthOne + operationLine.Length + matrixWidthTwo + equalityLine.Length + matrixWidthResult;

            //Определяем наибольшее количество строк в матрицах
            int maxRow;
            if (rowOne >= rowTwo)
            {
                maxRow = rowOne;
            }
            else
            {
                maxRow = rowTwo;
            }

            //Проверка необходимости увеличения окна консоли и возможности вывода если максимальный размер консоли превышен, вывод результата, если проверка пройдена
            if (TryResizeConsole(outputWidth, maxRow))
            {
                //Выводим результат в виде трех матриц
                Console.ForegroundColor = ConsoleColor.Green;
                WriteMatrix(matrixOne, startLine, startLine.Length, maxRow);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                WriteMatrix(matrixTwo, operationLine, startLine.Length + matrixWidthOne + operationLine.Length, maxRow);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Magenta;
                WriteMatrix(resultMatrix, equalityLine, startLine.Length + matrixWidthOne + operationLine.Length + matrixWidthTwo + equalityLine.Length, maxRow);
                Console.ResetColor();

                //Ставим курсор на строку ниже самой высокой матрицы из вывода
                Console.SetCursorPosition(0, maxRow + 1);
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Выполняет умножение двух входящих матриц. Возвращает результирующую матрицу.
        /// </summary>
        /// <param name="matrixOne"></param>
        /// <param name="matrixTwo"></param>
        /// <returns></returns>
        private static int[,] MultiplyTwoMatrix(int[,] matrixOne, int[,] matrixTwo)
        {
            int[,] resultMatrix = new int[matrixOne.GetLength(0), matrixTwo.GetLength(1)];
            for (int i = 0; i < matrixOne.GetLength(0); i++)
            {

                for (int j = 0; j < matrixTwo.GetLength(1); j++)
                {
                    int sum = 0;
                    for (int k = 0; k < matrixOne.GetLength(1); k++)
                    {
                        int mult = matrixOne[i, k] * matrixTwo[k, j];
                        sum = sum + mult;
                    }

                    resultMatrix[i, j] = sum;
                }
            }

            return resultMatrix;
        }

        /// <summary>
        /// Принимает две матрицы и параметр mathOperation, в зависимости от значения которого выполняет либо сложение, либо вычитание матриц, возвращает результирующую матрицу.
        /// </summary>
        /// <param name="matrixOne"></param>
        /// <param name="matrixTwo"></param>
        /// <param name="mathOperation"></param>
        /// <returns></returns>
        private static int[,] SumOrDeductTwoMatrixes(int[,] matrixOne, int[,] matrixTwo, MathOperation mathOperation)
        {
            int[,] resultMatrix = new int[matrixOne.GetLength(0), matrixOne.GetLength(1)];

            for (int i = 0; i < matrixOne.GetLength(0); i++)
            {
                switch (mathOperation)
                {
                    case MathOperation.Summation:
                        for (int j = 0; j < matrixOne.GetLength(1); j++)
                        {
                            resultMatrix[i, j] = matrixOne[i, j] + matrixTwo[i, j];
                        }
                        break;
                    case MathOperation.Deduction:
                        for (int j = 0; j < matrixOne.GetLength(1); j++)
                        {
                            resultMatrix[i, j] = matrixOne[i, j] - matrixTwo[i, j];
                        }
                        break;
                }
            }

            return resultMatrix;
        }

        /// <summary>
        /// Определяет ширину матрицы в символах по количеству столбцов
        /// </summary>
        /// <param name="col"></param>
        /// <returns>Ширина матрицы в символах</returns>
        private static int GetMatrixWidth(int col)
        {
            return MatrixBoundarySymbol.Length + Indent + col * Indent + MatrixBoundarySymbol.Length;
        }

        /// <summary>
        /// Умножает входящую матрицу на заданное целое число. Возвращает результирующую матрицу.
        /// </summary>
        /// <param name="matrixOne"></param>
        /// <param name="multiplier"></param>
        /// <returns></returns>
        private static int[,] MultiplyMatrixByNumber(int[,] matrixOne, int multiplier)
        {
            int[,] resultMatrix = new int[matrixOne.GetLength(0), matrixOne.GetLength(1)];

            for (int i = 0; i < matrixOne.GetLength(0); i++)
            {
                for (int j = 0; j < matrixOne.GetLength(1); j++)
                {
                    resultMatrix[i, j] = multiplier * matrixOne[i, j];
                }
            }

            return resultMatrix;
        }

        /// <summary>
        /// Возвращает введенное значение размерности матрицы в int из консоли, если пользователь ввел корректное значение: ввод должен являться целым числом больше нуля.
        /// </summary>
        /// <returns>Значение размерности матрицы</returns>
        private static int ReadMatrixDimension()
        {
            int dimension = 0;
            bool correctUserEntry = false;
            while (!correctUserEntry)
            {
                if (Int32.TryParse(Console.ReadLine(), out dimension) && dimension > 0)
                {
                    correctUserEntry = true;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Введите целое число больше нуля.");
                }
            }

            return dimension;
        }

        /// <summary>
        /// Возвращает в int ввёденное значение числа на которое нужно умножить матрицу, если пользователь ввёл верное значение: целое число больше или меньше нуля.
        /// </summary>
        /// <returns>Число для цмножение на матрицу</returns>
        private static int ReadMultiplier()
        {
            int multiplier = 0;
            bool correctUserEntry = false;
            while (!correctUserEntry)
            {
                if (Int32.TryParse(Console.ReadLine(), out multiplier))
                {
                    correctUserEntry = true;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Введите целое число больше или меньше нуля.");
                }
            }

            return multiplier;
        }

        /// <summary>
        /// Заполняет входящую матрицу случайными числами от 0 до 10. Принимает матрицу в виде двумерного массива и переменную рандома.
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="matrix"></param>
        private static void FillMatrix(Random rnd, int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = rnd.Next(0, 11);
                }
            }
        }

        /// <summary>
        /// Выводит на экран матрицу. Принимает двойной массив матрицы, строку, которая будет выводится перед матрицей,
        /// значение сдвига курсора вправо от левого края консоли на позицию, с которой начнется вывод матрицы, начиная с precedingString,
        /// а также наибольшее количество строк из всех матриц в выводе.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="precedingString"></param>
        /// <param name="cursorOffsetToWriteMatrix"></param>
        /// <param name="maxRow"
        private static void WriteMatrix(int[,] matrix, string precedingString, int cursorOffsetToWriteMatrix, int maxRow)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                //Если текущая строка не является средней строкой, ставить крусор в позицию для вывода строк матрицы,
                //иначе выводить и строки матрицы и предшествующую строку
                if (i + maxRow / 2 - matrix.GetLength(0) / 2 != maxRow / 2)
                {
                    Console.SetCursorPosition(cursorOffsetToWriteMatrix, i + maxRow / 2 - matrix.GetLength(0) / 2);
                }
                else
                {
                    Console.SetCursorPosition(cursorOffsetToWriteMatrix - precedingString.Length, i + maxRow / 2 - matrix.GetLength(0) / 2);
                    Console.Write(precedingString);
                }

                //Непосредственно вывод матрицы
                Console.Write(MatrixBoundarySymbol);
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j],Indent}");
                }
                Console.Write($"{MatrixBoundarySymbol,Indent}");
                Console.Write("\n");
            }
        }

        /// <summary>
        /// Возвращает true если удалось успешно изменить размеры консоли, или если консоль не нужно увеличивать.
        /// Если консоль не может быть увеличина по любой из осей, возвращает false и выводит соответсвующее сообщение.
        /// Принимает количество строк, по которому будет произведено увеличение высоты и общую ширину вывода для 
        /// определения значения. на которое будет произведено увеличение ширины консоли.
        /// </summary>
        /// <param name="outputWidth"></param>
        /// <param name="largestRowNumber"></param>
        /// <returns></returns>
        private static bool TryResizeConsole(int outputWidth, int largestRowNumber)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (outputWidth > Console.WindowWidth)
            {
                //Увеличиваем консоль по ширине, если вывод не влезает в стандартный размер по ширине
                int outputWidthShift = outputWidth - Console.WindowWidth;

                //Если результирующий вывод не влезает в максимально возможный размер консоли по ширине, сообщаем об этом пользователю
                //и прерываем выполнение программы
                if (Console.WindowWidth + outputWidthShift > Console.LargestWindowWidth)
                {
                    Console.WriteLine("Невозможно вывести результат расчета на консоли в текущей размерности ширины экрана.");
                    return false;
                }

                //Увеличиваем ширину консоли так, чтобы поместился вывод
                else
                {
                    Console.SetWindowSize(Console.WindowWidth + outputWidthShift, 25);
                }

            }

            //Увеличиваем консоль по высоте, если вывод не влезает в стандартный размер по высоте
            if (largestRowNumber > Console.WindowHeight)
            {
                //Если высота консоли меньше максимальной, увеличиваем так, чтобы вывод полностью влез по высоте бех скролла
                if (Console.WindowHeight + (largestRowNumber - Console.WindowHeight) > Console.LargestWindowHeight)
                {
                    Console.SetWindowSize(Console.WindowWidth, Console.LargestWindowHeight);
                    return true;
                }

                //Если высота вывода больше, чем максимально возможная высота консоли, делаем высоту консоли максимальной
                else
                {
                    Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight + (largestRowNumber - Console.WindowHeight));
                    return true;
                }
            }

            Console.ResetColor();
            return true;
        }
    }
}

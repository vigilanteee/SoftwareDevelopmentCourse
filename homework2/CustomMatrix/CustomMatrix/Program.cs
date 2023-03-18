namespace CustomMatrix;

public class Program
{
    private static Matrix _matrix1 = new Matrix(0, 0);
    private static Matrix _matrix2 = new Matrix(0, 0);
    private static Matrix _matrix3 = new Matrix(0, 0);
    
    private static void CreateMatrix(string letter, out Matrix matrix)
    {
        Console.Clear();
        while (true)
        {
            Console.WriteLine($"Введите матрицу {letter} в формате \"1 2 3, 4 5 6, 7 8 9\":");
            string? sMatrix = Console.ReadLine();
            Console.Clear();
            if (!String.IsNullOrEmpty(sMatrix))
            {
                try
                {
                    Matrix.TryParse(sMatrix, out matrix);
                    return;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введен неверный формат");
                }
            }
        }
    }
    
    private static void MatrixMultiplication()
    {
        Console.Clear();
        try
        {
            _matrix3 = _matrix1 * _matrix2;
            Console.WriteLine("A * B:");
            Console.WriteLine(_matrix3.ToString());
        }
        catch (FormatException)
        {
            Console.WriteLine("Матрицы A и B не одинакового размера");
        }
    }

    private static void ShowMatrix(Matrix matrix, string letter)
    {
        Console.Clear();
        Console.WriteLine($"{letter}:");
        Console.Write(matrix.ToString());
        Console.WriteLine();
    }
    
    private static void ShowMainMenu()
    {
        Console.WriteLine("1 - Создать матрицу A");
        Console.WriteLine("2 - Вывести матрицу А");
        Console.WriteLine("3 - Создать матрицу B");
        Console.WriteLine("4 - Вывести матрицу B");
        Console.WriteLine("5 - Произвести умножение матриц A и B");
        Console.WriteLine("0 - Выйти из программы");
    }
    
    // ДОП. ЗАДАНИЕ: Консольный ввод
    static void Main(string[] args)
    {
        Console.WriteLine("Вариант: {0}", ('C' + 'A') % 7);
        while (true)
        {
            ShowMainMenu();
            switch (char.ToLower(Console.ReadKey(true).KeyChar))
            {
                case '1':
                    CreateMatrix("A", out _matrix1);
                    break;
                case '2':
                    ShowMatrix(_matrix1, "A");
                    break;
                case '3':
                    CreateMatrix("B", out _matrix2);
                    break;
                case '4':
                    ShowMatrix(_matrix2, "B");
                    break;
                case '5':
                    MatrixMultiplication();
                    break;
                case '0':
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    break;
            }
        }
    }
}

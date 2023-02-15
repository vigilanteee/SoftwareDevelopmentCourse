using System.Numerics;
using System.Reflection;

namespace IntroductionToTypeReflection
{
    partial class Program
    {
        // Метод для изменения цветовой схемы программы
        public static void ChangeConsoleView()
        {
            while (true)
            {
                ShowConsoleViewColors();
                Console.Write("Введите команду: ");
                switch (char.ToLower(Console.ReadKey().KeyChar))
                {
                    case '1':
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                    case '2':
                        Console.ResetColor();
                        break;
                    case '3':
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case '0':
                        return;
                    default:
                        break;
                }
            }
        }

        /*
         * Метод для вывода общей информации по текущей сборке, а также всем подключенным
         * ДОПОЛНИТЕЛЬНОЕ ЗАДАНИЕ: вывести длину свойства, количество полей и их имена
         */
        public static void ShowAllTypeInfo()
        {
            Assembly[] refAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> types = new List<Type>();
            foreach (Assembly assembly in refAssemblies)
                types.AddRange(assembly.GetTypes());

            int refTypes = 0;
            int primTypes = 0;
            string longestPropertyName = string.Empty;
            string typeWithMostFields = string.Empty;
            int fieldsCount = int.MinValue;

            foreach (Type type in types)
            {
                if (type.IsClass)
                    refTypes++;
                else if (type.IsValueType)
                    primTypes++;

                if (type.GetFields().Length > fieldsCount)
                {
                    typeWithMostFields = type.Name;
                    fieldsCount = type.GetFields().Length;
                }

                foreach (var property in type.GetProperties())
                {
                    if (property.Name.Length > longestPropertyName.Length)
                        longestPropertyName = property.Name;
                }
            }

            Type? t = types.Find(item => item.Name == typeWithMostFields);
            string[] fieldNames = new string[t.GetFields().Length];

            if (t != null && t.GetFields().Length > 0)
            {
                for (int i = 0; i < fieldNames.Length; i++)
                    fieldNames[i] = t.GetFields()[i].Name;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Общая информация по типам");
                Console.WriteLine("Подключенные сборки: {0}", refAssemblies.Length);
                Console.WriteLine("Всего типов по всем подключенным сборкам: {0}", types.Count);
                Console.WriteLine("Ссылочные типы (только классы): {0}", refTypes);
                Console.WriteLine("Значимые типы: {0}", primTypes);
                Console.WriteLine("\nИнформация в соответствии с вариантом №2");
                Console.WriteLine("\nСамое длинное название свойства - {0}, его длина: {1}",
                    longestPropertyName, longestPropertyName.Length);
                Console.WriteLine("Тип с наибольшим числом полей - {0}, количество полей: {1}\n",
                    typeWithMostFields, fieldsCount);
                for (int i = 1; i <= fieldNames.Length; i++)
                {
                    if (i > 15)
                        break;
                    Console.WriteLine("{0}: {1}", i, fieldNames[i - 1]);
                }
                Console.WriteLine("\n0 - Выход в главное меню");

                if (char.ToLower(Console.ReadKey().KeyChar) == '0')
                    return;
            }
        }

        // Метод для выбора типа с последующим выводом информации о нем
        public static void SelectType()
        {
            Type t;
            while (true)
            {
                ShowAvailableTypes();
                switch (char.ToLower(Console.ReadKey().KeyChar))
                {
                    case '1':
                        t = typeof(uint);
                        SelectTypeInfo(t);
                        break;
                    case '2':
                        t = typeof(int);
                        SelectTypeInfo(t);
                        break;
                    case '3':
                        t = typeof(long);
                        SelectTypeInfo(t);
                        break;
                    case '4':
                        t = typeof(float);
                        SelectTypeInfo(t);
                        break;
                    case '5':
                        t = typeof(double);
                        SelectTypeInfo(t);
                        break;
                    case '6':
                        t = typeof(char);
                        SelectTypeInfo(t);
                        break;
                    case '7':
                        t = typeof(string);
                        SelectTypeInfo(t);
                        break;
                    case '8':
                        t = typeof(Vector);
                        SelectTypeInfo(t);
                        break;
                    case '9':
                        t = typeof(Matrix4x4);
                        SelectTypeInfo(t);
                        break;
                    case '0':
                        return;
                    default:
                        break;
                }
            }
        }


        // Входная точка работы программы
        static void Main(string[] args)
        {
            while (true)
            {
                ShowMainMenu();
                Console.Write("Введите команду: ");

                switch (char.ToLower(Console.ReadKey().KeyChar))
                {
                    case '1':
                        ShowAllTypeInfo();
                        break;
                    case '2':
                        SelectType();
                        break;
                    case '3':
                        ChangeConsoleView();
                        break;
                    case '0':
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

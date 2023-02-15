namespace IntroductionToTypeReflection
{
    partial class Program
    {
        // Метод для подсчета всех доступных полей и свойств типа
        public static void SelectTypeInfo(Type t)
        {
            string sFieldNames = "-";
            if (t.GetFields().Length > 0)
            {
                string[] fieldNames = new string[t.GetFields().Length];
                for (int i = 0; i < fieldNames.Length; i++)
                {
                    fieldNames[i] = t.GetFields()[i].Name;
                }
                sFieldNames = String.Join(", ", fieldNames);
            }

            string sPropertiesNames = "-";
            if (t.GetProperties().Length > 0)
            {
                string[] propertyNames = new string[t.GetProperties().Length];
                for (int i = 0; i < propertyNames.Length; i++)
                {
                    propertyNames[i] = t.GetProperties()[i].Name;
                }
                sPropertiesNames = String.Join(", ", propertyNames);
            }

            while (true)
            {
                ShowSelectedTypeInfo(t, sFieldNames, sPropertiesNames);

                switch (char.ToLower(Console.ReadKey().KeyChar))
                {
                    case 'm':
                        ShowExtraTypeInfo(t);
                        break;
                    case '0':
                        return;
                    default:
                        break;
                }
            }
        }

        /*
         * Метод для отображения дополнительной информации о методе
         * Данный метод вызывается из метода SelectTypeInfo
         * ДОПОЛНИТЕЛЬНОЕ ЗАДАНИЕ: Вывести среднее число полей и параметров
         */
        public static void ShowExtraTypeInfo(Type t)
        {
            var overloads = new Dictionary<string, int>();
            var parameters = new Dictionary<string, int[]>();
            int[] temp = new int[2];
            double sumOverloads = 0;
            double sumParameters = 0;
            int total = 0;

            foreach (var method in t.GetMethods())
            {
                foreach (var parameter in method.GetParameters())
                {
                    if (parameter.IsOptional)
                        temp[1]++;
                    else
                    {
                        temp[0]++;
                        temp[1]++;
                    }
                }

                if (overloads.ContainsKey(method.Name)
                    && parameters.ContainsKey(method.Name))
                {
                    overloads[method.Name]++;

                    if (parameters[method.Name][0] > temp[0])
                        parameters[method.Name][0] = temp[0];
                    if (parameters[method.Name][1] < temp[1])
                        parameters[method.Name][1] = temp[1];
                }
                else
                {
                    overloads.Add(method.Name, 1);
                    parameters.Add(method.Name, temp);
                }
                temp = new int[2];
            }

            foreach (var entry in overloads)
            {
                sumOverloads += entry.Value;
                total++;
            }
            double avgOverloads = sumOverloads / total;

            foreach (var entry in parameters)
                sumParameters += entry.Value[0];
            double avgParameters = sumParameters / total;

            while (true)
            {
                Console.Clear();
                int line = 1;
                Console.WriteLine("Методы типа " + t.FullName);
                Console.Write("Название");
                Console.SetCursorPosition(30, line);
                Console.Write("Число перегрузок");
                Console.SetCursorPosition(60, line);
                Console.Write("Число параметров\n");
                line++;
                foreach (var item in overloads)
                {
                    Console.Write(item.Key);
                    Console.SetCursorPosition(30, line);
                    Console.Write(item.Value);
                    Console.SetCursorPosition(60, line);
                    if (parameters[item.Key][0] == parameters[item.Key][1])
                        Console.WriteLine(parameters[item.Key][0]);
                    else
                        Console.WriteLine(parameters[item.Key][0] + ".." + parameters[item.Key][1]);
                    line++;
                }
                Console.SetCursorPosition(0, line);
                Console.WriteLine("Среднее:");
                Console.SetCursorPosition(30, line);
                Console.Write(Math.Round(avgOverloads, 2));
                Console.SetCursorPosition(60, line);
                Console.Write(Math.Round(avgParameters, 2));
                Console.SetCursorPosition(0, ++line);
                Console.WriteLine("Нажмите '0' чтобы вернуться");

                if (char.ToLower(Console.ReadKey().KeyChar) == '0')
                    return;
            }
        }

        // Метод для отображения всех доступных типов
        public static void ShowAvailableTypes()
        {
            Console.Clear();
            Console.WriteLine("Информация по типам");
            Console.WriteLine("Выберите тип:");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("\t1 - uint");
            Console.WriteLine("\t2 - int");
            Console.WriteLine("\t3 - long");
            Console.WriteLine("\t4 - float");
            Console.WriteLine("\t5 - double");
            Console.WriteLine("\t6 - char");
            Console.WriteLine("\t7 - string");
            Console.WriteLine("\t8 - Vector");
            Console.WriteLine("\t9 - Matrix");
            Console.WriteLine("\t0 - Выход в главное меню");
        }

        // Метод для отображения доступных цветовых схем в программе
        public static void ShowConsoleViewColors()
        {
            Console.Clear();
            Console.WriteLine("Выберите цветовую схему:");
            Console.WriteLine("1 - Светлая");
            Console.WriteLine("2 - Темная");
            Console.WriteLine("3 - Темная (контрастная)");
            Console.WriteLine("0 - Выход в главное меню");
        }

        // Метод для отображения главного меню программы
        public static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Вариант: 2");
            Console.WriteLine("Информация по типам:");
            Console.WriteLine("1 - Общая информация по типам");
            Console.WriteLine("2 - Выбрать тип из списка");
            Console.WriteLine("3 - Параметры консоли");
            Console.WriteLine("0 - Выход из программы");
        }

        /*
         * Метод для отображения основной информации о типе
         * Данный метод вызывается из метода SelectTypeInfo
         */
        public static void ShowSelectedTypeInfo(Type t, string fields, string properties)
        {
            Console.Clear();
            Console.WriteLine("Информация по типу: {0}", t.FullName);
            Console.WriteLine("\tЗначимый тип: {0}", t.IsPrimitive ? "+" : "-");
            Console.WriteLine("\tПространство имен: {0}", t.Namespace);
            Console.WriteLine("\tСборка: {0}", t.Assembly.GetName().Name);
            Console.WriteLine("\tОбщее число элементов: {0}", t.GetMembers().Length);
            Console.WriteLine("\tЧисло методов: {0}", t.GetMethods().Length);
            Console.WriteLine("\tЧисло свойств: {0}", t.GetProperties().Length);
            Console.WriteLine("\tЧисло полей: {0}", t.GetFields().Length);
            Console.WriteLine("\tСписок полей: {0}", fields);
            Console.WriteLine("\tСписок свойств: {0}", properties);
            Console.WriteLine("\nНажмите 'M' для вывода дополнительной информации по методам:");
            Console.WriteLine("Нажмите '0' для выбора типа");
        }
    }
}

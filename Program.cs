using System;
using System.IO;

namespace lab_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Считать весь текст из файла \n" +
                    "2. Вывести построчно текст в консоль\n" +
                    "3. Закодировать его шифром Цезаря\n" +
                    "4. Закодировать его шифром пар\n" +
                    "5. Раскодировать его шифром Цезаря\n" +
                    "6. Раскодировать его шифром пар \n" +
                    "7. Сохранить закодированный текст, в файл откуда он был считан, заменив исходный текст\n" +
                    "8. Завершение приложения");
            string path = "";
            string encr = "";
            string encrwrite = "";
            do
            {
                int y = CheckNumber();// Проверка вводимого числа на корректность
                switch (y)
                {
                    case 1:
                        Console.WriteLine("Введите путь к файлу");
                        path = Console.ReadLine();
                        if ((ReadFile(path) == "")) //проверка на ввод корректного пути
                        {
                        }
                        else
                        {
                            Console.WriteLine("Данные считаны");
                        }
                        break;
                    case 2:
                        if (path == "")
                        {
                            Console.WriteLine("Файл не прочитан");
                        }
                        else
                        {
                            if (encr == "")
                            {
                                string str = ReadFile(path);
                                encr = str;
                                Console.WriteLine(encr);
                            }
                            else
                            {
                                Console.WriteLine(encr);//вывод измененного текста
                            }
                        }
                        break;
                    case 3:
                        if (path == "")
                        {
                            Console.WriteLine("Файл не прочитан");
                        }
                        else
                        {
                            string rome = RomeCode(ReadFile(path)); //шифрование Цезарем
                            encr = rome;
                            encrwrite = rome;
                            Console.WriteLine("текст закодирован шифром Цезаря");
                        }

                        break;
                    case 4:
                        if (path == "")
                        {
                            Console.WriteLine("Файл не прочитан");
                        }
                        else
                        {
                            string pair = PairCode(ReadFile(path));// Шифрование парами
                            encr = pair;
                            encrwrite = pair;
                            Console.WriteLine();
                            Console.WriteLine("Текст закодирован шифром пар");
                        }
                        break;
                    case 5:
                        if (path == "")
                        {
                            Console.WriteLine("Файл не прочитан");
                        }
                        else
                        {
                            Console.WriteLine("Введите ключ");
                            string key = Console.ReadLine();
                            string romde = RomeDeCode(encr, key);//Дешифрование Цезарем
                            encr = romde;
                            Console.WriteLine("Текст декодирован шифром Цезаря");
                        }
                        break;
                    case 6:
                        if (path == "")
                        {
                            Console.WriteLine("Файл не прочитан");
                        }
                        else
                        {
                            Console.WriteLine("Введите ключ");
                            string keyP = Console.ReadLine();
                            string parde = PairDeCode(encr, keyP);// Дешифрование парами
                            encr = parde;
                            Console.WriteLine("Текст декодирован шифром пар");
                        }
                        break;
                    case 7:
                        if (path == "")
                        {
                            Console.WriteLine("Файл не прочитан");
                        }
                        else
                        {
                            WriteFile(encrwrite, path);
                        }


                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                }
            } while (true);

        }
        static Random rnd = new Random();
        /// <summary>
        /// чтение файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns>текст из файла</returns>
        static string ReadFile(string path)
        {
            string text = "";
            if (File.Exists(path))
            {
                try
                {
                    text = File.ReadAllText(path);// Чтение файла и запись в переменную
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Такого файла не существует");
            }
            return text;
        }
        /// <summary>
        /// запись в файл
        /// </summary>
        /// <param name="text"></param>
        /// <param name="path"></param>
        static void WriteFile(string text, string path)
        {
            try
            {
                File.WriteAllText(path, text);// Запись текста в файл
                Console.WriteLine("Данные записаны");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Шифрование парами
        /// </summary>
        /// <param name="text"></param>
        /// <returns>зашифрованный текст</returns>
        static string PairCode(string text)
        {
            int N = 52;
            string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] arr = alphabet.ToCharArray();
            for (int i = 0; i < N; i++)
            {
                int j = rnd.Next(i + 1);
                //перестановка букв
                char tmp = arr[j];
                arr[j] = arr[i];
                arr[i] = tmp;
            }
            Console.Write("Key = ");
            for (int k = 0; k < arr.Length; k++)
            {
                Console.Write("{0}", arr[k]);
            }
            string paircode = "";
            foreach (char symb in text)//проверка каждого символа в строке
            {
                if (((symb < 65) || (symb > 122)) || ((symb < 90) & (symb > 97)))//проверка на принадлежность буквы юникоду
                {
                    if (!((symb == 13) || (symb == 10)))
                    {
                        //Console.WriteLine("wrong");
                        //Environment.Exit(0);
                    }
                }
            }
            char[] arr1 = text.ToCharArray();
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] == 10)
                {
                    paircode += "\n";
                }
                for (int j = 0; j < arr.Length; j++)
                {
                    if (text[i] == arr[j])
                    {
                        if (j < N / 2)
                        {
                            arr1[i] = arr[N / 2 + j];
                            paircode += arr1[i];
                        }
                        else
                        {
                            arr1[i] = arr[j - N / 2];
                            paircode += arr1[i];
                        }
                    }
                }
            }
            return paircode;
        }
        /// <summary>
        /// Дешифрование парами
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns>дешифрованный текст</returns>
        static string PairDeCode(string text, string key)
        {

            char[] arr = key.ToCharArray();

            string pairdecode = "";
            foreach (char symb in text)//проверка каждого символа в строке
            {
                if (((symb < 65) || (symb > 122)) || ((symb < 90) & (symb > 97)))//проверка на принадлежность буквы юникоду
                {
                    if (!((symb == 13) || (symb == 10)))
                    {
                        Console.WriteLine("wrong");
                        Environment.Exit(0);
                    }
                }
            }
            char[] arr1 = text.ToCharArray();
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] == 10)
                {
                    pairdecode += "\n";
                }
                for (int j = 0; j < arr.Length; j++)
                {
                    if (text[i] == arr[j])
                    {
                        if (j < arr.Length / 2)
                        {
                            arr1[i] = arr[arr.Length / 2 + j];
                            pairdecode += arr1[i];
                        }
                        else
                        {
                            arr1[i] = arr[j - arr.Length / 2];
                            pairdecode += arr1[i];
                        }
                    }
                }
            }
            return pairdecode;
        }
        /// <summary>
        /// Шиврование Цезарем
        /// </summary>
        /// <param name="text"></param>
        /// <returns>зашифрованная строка цезарем</returns>
        static string RomeCode(string text)
        {
            int shift = rnd.Next(1, 11);
            Console.WriteLine("key={0}", shift);
            string ciphertext = "";
            foreach (char symb in text)//проверка каждого символа в строке
            {
                if (((symb < 65) || (symb > 122)) || ((symb < 90) & (symb > 97)))//проверка на принадлежность буквы юникоду
                {
                    if (!((symb == 13) || (symb == 10)))
                    {
                        //Console.WriteLine("wrong");
                        //Environment.Exit(0);
                    }
                }
            }
            //int shift = rnd.Next(0, 11);
            foreach (char symb in text)//проверка каждого символа в строке
            {
                if (symb == 13)
                {
                    ciphertext += "\r";
                }
                else if (symb == 10)
                {
                    ciphertext += "\n";
                }
                else
                {
                    if ((symb > 96) & (symb < 123))
                    {
                        int value;
                        value = (shift % 26) + symb; // символ со сдвигом
                        if (value > 122)//выход из диапазона
                        {
                            value = 97 + (value - 122 - 1);
                        }
                        else if (value < 97)// выход из диапазона
                        {
                            value = 122 - (97 - value - 1);
                        }
                        char b = Convert.ToChar(value);
                        ciphertext += b;
                    }
                    else if ((symb > 64) & (symb < 91))
                    {
                        int value;
                        value = (shift % 26) + symb;
                        if (value > 90)
                        {
                            value = 65 + (value - 90 - 1);
                        }
                        else if (value < 65)
                        {
                            value = 90 - (65 - value - 1);
                        }
                        char b = Convert.ToChar(value);
                        ciphertext += b;
                    }
                }
            }
            return ciphertext;
        }
        /// <summary>
        /// Дешифрование Цезарем
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key"></param>
        /// <returns>дешифрованная строка Цезарем</returns>
        static string RomeDeCode(string text, string key)
        {
            int ikey = CheckKey(key);

            string ciphertext = "";
            foreach (char symb in text)//проверка каждого символа в строке
            {
                if (((symb < 65) || (symb > 122)) || ((symb < 90) & (symb > 97)))//проверка на принадлежность буквы юникоду
                {
                    if (!((symb == 13) || (symb == 10)))
                    {
                        Console.WriteLine("wrong");
                        Environment.Exit(0);
                    }
                }
            }
            foreach (char symb in text)//проверка каждого символа в строке
            {
                if (symb == 13)
                {
                    ciphertext += "\r";
                }
                else if (symb == 10)
                {
                    ciphertext += "\n";
                }
                else
                {
                    if ((symb > 96) & (symb < 123))
                    {
                        int value;
                        value = symb - (ikey % 26); // символ со сдвигом

                        if (value > 122)//выход из диапазона
                        {
                            value = 97 + (value - 122 - 1);
                        }
                        else if (value < 97)// выход из диапазона
                        {
                            value = 122 - (97 - value - 1);
                        }
                        char b = Convert.ToChar(value);
                        ciphertext += b;
                    }
                    else if ((symb > 64) & (symb < 91))
                    {
                        int value;
                        value = symb - (ikey % 26);
                        if (value > 90)
                        {
                            value = 65 + (value - 90 - 1);
                        }
                        else if (value < 65)
                        {
                            value = 90 - (65 - value - 1);
                        }
                        char b = Convert.ToChar(value);
                        ciphertext += b;
                    }
                }
            }
            return ciphertext;
        }
        /// <summary>
        /// проверка на  ввод корректного числа
        /// </summary>
        /// <returns></returns>
        static int CheckNumber()
        {
            int N;
            while (!int.TryParse(Console.ReadLine(), out N) || N < 1 || N > 8)
            {
                Console.WriteLine("Что-то не то, попробуйте ещё раз");
                continue;
            }
            return N;
        }
        /// <summary>
        /// проверка на ввод корректного числа
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        static int CheckKey(string k)
        {
            int N;
            while (!int.TryParse(k, out N) || N < 1 || N > 10)
            {
                Console.WriteLine("Что-то не то, попробуйте ещё раз");
                k = Console.ReadLine();
                continue;
            }
            return N;
        }
    }
}

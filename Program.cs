using System.Text.RegularExpressions;

namespace _1Task
{
    internal class Program
    {
        static void Main()
        {

            Console.WriteLine("Выберите режим поиска:\n 1 - С учетом регистра \n 2 - Без учета регистра");

            string input = Console.ReadLine();

            if (input != "1" && input != "2") { 
                Console.WriteLine("Введено некорректное значение.");
                return;
            }

            int choose = Convert.ToInt32(input);

            string filename = "D:\\Разработка\\CID\\1Task\\files\\words.txt";

            // Заданные слова для поиска
            List<string> wordsToFind = ReadWordsFromFile(filename);

            // Путь к директории, которую нужно проверить
            //string directoryPath = "путь_к_директории";
            string directoryPath = "D:\\Разработка\\CID\\1Task\\files";

            // Проверяем, существует ли указанная директория
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("Указанная директория не существует.");
                return;
            }

            // Получаем пути всех текстовых файлов в указанной директории
            string[] files = Directory.GetFiles(directoryPath, "*.txt");

            // Перебираем найденные файлы
            foreach (string file in files)
            {
                if (file == "D:\\Разработка\\CID\\1Task\\files\\words.txt") // Пропускаем наш файл со словами
                {
                    continue;
                }

                // Считываем содержимое файла
                string fileContent = File.ReadAllText(file);


                if (Convert.ToInt32(choose) == 2)
                {
                    // Проходим по каждому заданному слову
                    foreach (string word in wordsToFind)
                    {
                        // Ищем слово в содержимом файла (регистронезависимый поиск)
                        if (fileContent.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            Console.WriteLine($"Слово '{word}' найдено в файле: {file}");
                        }
                    }
                }
                else if (Convert.ToInt32(choose) == 1)
                {
                    // Проходим по каждому заданному слову
                    foreach (string word in wordsToFind)
                    {
                        // Ищем слово в содержимом файла (регистрозависимый поиск)
                        if (fileContent.IndexOf(word) >= 0)
                        {
                            Console.WriteLine($"Слово '{word}' найдено в файле: {file}");
                        }
                    }
                }
            }
            Console.WriteLine("Поиск завершен.");
        }

        static List<string> ReadWordsFromFile(string filePath)
        {
            List<string> words = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] lineWords = Regex.Split(line, @"\W+");  // разделим текст на слова
                        foreach (string word in lineWords)
                        {
                            words.Add(word);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("При чтении файла возникла ошибка: " + ex.Message);
            }

            // Почистим пустое значение
            words.Remove(words.Where(x => x.ToString() == "").First());

            return words;
        }
    }
}
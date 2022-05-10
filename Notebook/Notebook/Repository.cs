using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillbox_7
{
    struct Repository
    {

        int index;
        private Worker[] workers; // Массив работников.
        public string[] titles;// Массив заголовков
        private string path;// Путь к файлу
        public Repository(string Path)
        {
            this.path = Path; // Сохранение пути к файлу с данными
            this.index = 0; // текущая позиция для добавления сотрудника в workers 
            this.workers = new Worker[1]; // инициализаия массива сотрудников.    | изначально предпологаем, что данных нет
            this.titles = new string[0];
            this.Check();
            this.Load(); // Загрузка данных
        }
        private void Check()
        {
            if (File.Exists(this.path) == false)
            {
                Console.WriteLine("Справочник создан введите данные сотрудников");
                using (StreamWriter sw = new StreamWriter(this.path, true))
                {
                    sw.WriteLine("ID ДАТА ФИО ВОЗРАСТ РОСТ ДАТА.РОЖДЕНИЯ МЕСТО.РОЖДЕНИЯ");
                };
            }
        }
        /// <summary>
        /// Добавление нового работника в справочник
        /// </summary>
        /// <param Путь к файлу="Path"></param>
        public void AddWorker()
        {
            char again = 'д';
            Worker worker = new Worker();
            do
            {                               
                
                Console.WriteLine("Введите ID пользователя");
                worker.ID = Convert.ToInt32(Console.ReadLine());
                worker.Date = DateTime.Now;
                Console.WriteLine("Введите ФИО пользователя");
                worker.Name = Console.ReadLine();
                Console.WriteLine("Введите возраст");
                worker.Age = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите рост");
                worker.Growth = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Введите дату рождения");
                worker.DateOfBirth = Console.ReadLine();
                Console.WriteLine("Введите место рождения");
                worker.NameOfBirth = Console.ReadLine();
                string temp = string.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}",
                    worker.ID,
                    worker.Date,
                    worker.Name,
                    worker.Age,
                    worker.Growth,
                    worker.DateOfBirth,
                    worker.NameOfBirth);
                File.AppendAllText(this.path, $"{temp}\n");
                Console.WriteLine("Продолжить ввод работников д/н");
                again = Console.ReadKey(true).KeyChar;
            } while (char.ToLower(again) == 'д');
            Add(worker);//Обновить список сотрудников
        }
        /// <summary>
        /// Увеличение хранилища
        /// </summary>
        /// <param name="flag"></param>
        private void Resize(bool flag)
        {
            Array.Resize(ref this.workers, this.workers.Length * 2);
        }
        /// <summary>
        /// Добавление работников в хранилище
        /// </summary>
        /// <param name="list"></param>
        private void Add(Worker list)
        {
            this.Resize(this.index >= workers.Length);
            this.workers[this.index] = list;
            this.index++;
        }
        /// <summary>
        /// Чтение работников из файла и сохранение в память
        /// </summary>
        private void Load()
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                titles = sr.ReadLine().Split(' ');
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('#');
                    Add(new Worker(Convert.ToInt32(args[0]), Convert.ToDateTime(args[1]), args[2], 
                        Convert.ToInt32(args[3]), Convert.ToInt32(args[4]), args[5], args[6]));
                }
            }
        }
        /// <summary>
        /// Заполнение консоли работниками из файла
        /// </summary>
        public void PrintConsole()
        {
            Console.WriteLine($"{this.titles[0], -2}{this.titles[1], 12}{this.titles[2], 28}" +
                $"{this.titles[3], 22}{this.titles[4], 9}{this.titles[5], 20}{this.titles[6], 22} ");
            for (int i = 0; i < index; i++)
            {
                Console.WriteLine($"{Convert.ToString(this.workers[i].ID), -2}" +
                    $"{this.workers[i].Date, 12}"+ 
                    $"{this.workers[i].Name, 30}"+
                    $"{this.workers[i].Age, 11}"+
                    $"{this.workers[i].Growth, 11}"+
                    $"{this.workers[i].DateOfBirth, 20}"+
                    $"{this.workers[i].NameOfBirth, 22}");
            }
        }
        /// <summary>
        /// Метод вывода работников
        /// </summary>
        /// <param name="i"></param>
        private void Print(int i)
        {
            Console.WriteLine($"{Convert.ToString(this.workers[i].ID),-2}" +
                        $"{this.workers[i].Date,12}" +
                        $"{this.workers[i].Name,30}" +
                        $"{this.workers[i].Age,11}" +
                        $"{this.workers[i].Growth,11}" +
                        $"{this.workers[i].DateOfBirth,20}" +
                        $"{this.workers[i].NameOfBirth,22}");
        }
        /// <summary>
        /// Метод вывода заголовка в консоль
        /// </summary>
        private void PrintTitle()
        {
            Console.WriteLine($"{this.titles[0],-2}{this.titles[1],12}{this.titles[2],28}" +
                    $"{this.titles[3],22}{this.titles[4],9}{this.titles[5],20}{this.titles[6],22} ");
        }
        /// <summary>
        /// Поиск работника по ID
        /// </summary>
        public void SearchId()
        {
            Console.WriteLine("Введите id работника");
            int id = Convert.ToInt32(Console.ReadLine()); // Запрос на ввод id работника
            PrintTitle();
            for (int i = 0; i < index; i++) // Через цикл перебираем всех работников 
            {
                if (workers[i].ID == id) // Если id работника из списка совпадает с введенным id выводим этого пользователя на экран. 
                {
                    Print(i);
                }
            }
        }
        /// <summary>
        /// Сортировка по возрастанию даты добавления методом пузырька
        /// </summary>
        public void SortAscending()
        {
            PrintTitle();
            for (int i = 0; i < index; i++)
            {
                for (int j = i + 1; j < index; j++)
                {
                    if (workers[i].Date > workers[j].Date)
                    {
                        Worker temp = workers[i];
                        workers[i] = workers[j];
                        workers[j] = temp;
                    }
                }
            }
            for (int i = 0; i < index; i++)
            {
                Print(i);
            }
        }
        /// <summary>
        /// Сортировка по убыванию даты добавления методом Пузырька
        /// </summary>
        public void Descendingsort()
        {
            PrintTitle();
            for (int i = 0; i < index; i++)
            {
                for (int j = i + 1; j < index; j++)
                {
                    if (workers[i].Date < workers[j].Date)
                    {
                        Worker temp = workers[i];
                        workers[i] = workers[j];
                        workers[j] = temp;
                    }
                }
            }
            for (int i = 0; i < index; i++)
            {
                Print(i);
            }
        }
        /// <summary>
        /// Медод изменения параметров сотрудника
        /// </summary>
        public void ChangeWorker()
        {
            Console.WriteLine("Введите ID работника, которого хотите изменить");//Просим ввести id работника
            int id = Convert.ToInt32(Console.ReadLine()); // Запрос на ввод id работника
            for (int i = 0; i < index; i++) // Через цикл перебираем всех работников 
            {
                if (workers[i].ID == id) // Если id работника из списка совпадает с введенным id заменяем его данные в файле. 
                {
                    workers[i].ID = id;
                    workers[i].Date = DateTime.Now;
                    Console.WriteLine("Введите ФИО пользователя");
                    workers[i].Name = Console.ReadLine();
                    Console.WriteLine("Введите возраст");
                    workers[i].Age = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите рост");
                    workers[i].Growth = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Введите дату рождения");
                    workers[i].DateOfBirth = Console.ReadLine();
                    Console.WriteLine("Введите место рождения");
                    workers[i].NameOfBirth = Console.ReadLine();
                    string temp = string.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}",
                    workers[i].ID,
                    workers[i].Date,
                    workers[i].Name,
                    workers[i].Age,
                    workers[i].Growth,
                    workers[i].DateOfBirth,
                    workers[i].NameOfBirth);
                    File.Delete(this.path); //Удаляем файл                    
                }
            }
            Save();
            
        }
        /// <summary>
        /// Метод сохранения данных в справочник
        /// </summary>
        private void Save()
        {
            string title = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                                    titles[0],
                                    titles[1],
                                    titles[2],
                                    titles[3],
                                    titles[4],
                                    titles[5],
                                    titles[6]);
            File.AppendAllText(this.path, $"{title}\n");
            for (int j = 0; j < index; j++)
            {
                string list = string.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}",
                    workers[j].ID,
                    workers[j].Date,
                    workers[j].Name,
                    workers[j].Age,
                    workers[j].Growth,
                    workers[j].DateOfBirth,
                    workers[j].NameOfBirth);
                File.AppendAllText(this.path, $"{list}\n");
            }
        }
        /// <summary>
        /// Удаление работника
        /// </summary>
        public void DeleteWorker()
        {
            Console.WriteLine("Введите ID работника, которого хотите изменить");//Просим ввести id работника
            int id = Convert.ToInt32(Console.ReadLine());
            File.Delete(this.path);
            string title = string.Format("{0} {1} {2} {3} {4} {5} {6}",
                                    titles[0],
                                    titles[1],
                                    titles[2],
                                    titles[3],
                                    titles[4],
                                    titles[5],
                                    titles[6]);
            File.AppendAllText(this.path, $"{title}\n");

            for (int i = 0; i < index; i++)
            {
                if (id != workers[i].ID)
                {
                    string list = string.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}",
                            workers[i].ID,
                            workers[i].Date,
                            workers[i].Name,
                            workers[i].Age,
                            workers[i].Growth,
                            workers[i].DateOfBirth,
                            workers[i].NameOfBirth);
                    File.AppendAllText(this.path, $"{list}\n");
                }

            }
        }
        /// <summary>
        /// Метод загрузки пользователей в определенном диапазане дат
        /// </summary>
        public void LoadForDateRange()
        {
            Console.WriteLine("Введите первую дату: ");
            DateTime date = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Введите вторую дату: ");
            DateTime date1 = Convert.ToDateTime(Console.ReadLine());
            PrintTitle();
            for (int i = 0; i < index; i++)
            {
                    if ((workers[i].Date > date && workers[i].Date <date1)^(workers[i].Date < date && workers[i].Date > date1))//Условие вывода данных
                    {
                    Print(i);//Вывод пользователей
                    }
            }
        }
    }
}


using System;

namespace Skillbox_7;

static class Program
{

    static void Main()
    {
        string path = ".Directory.txt";
        Repository repository = new Repository(path);
        repository.PrintConsole();
        while (true)
        {
            Console.WriteLine($"\nОтсортировать работников по возрастанию даты добавления: 1" +
            $"\nОтсортировать работников по убыванию даты добавления даты добавления:  2" +
            $"\nДобавить нового работника в справочник: 3" +
            $"\nПоиск работника по его ID: 4" +
            $"\nИзменить данные работника: 5" +
            $"\nВывести данные в определенном диапазоне дат: 6" +
            $"\nУдалить выбранного работника: 7" +
            $"\nЧтобы закрыть программу введите: 10");
            int Choice = Convert.ToInt32(Console.ReadLine());
            if (Choice == 1) repository.SortAscending();
            if (Choice == 2) repository.Descendingsort();
            if (Choice == 3) repository.AddWorker();
            if (Choice == 4) repository.SearchId();
            if (Choice == 5) repository.ChangeWorker();               
            if (Choice == 6) repository.LoadForDateRange();
            if (Choice == 7) repository.DeleteWorker();
            if (Choice == 10) break;
        }
        
    }
}
using System;
using System.Collections.Generic;

internal class Program
{
    static Dictionary<string, string> userCredentials = new Dictionary<string, string>();
    static List<User> users = new List<User>();

    static void Main(string[] args)
    {
        bool exit = false;
        bool isAuthenticated = false;

        while (!exit)
        {
            Console.WriteLine("\nМеню:");
            if (!isAuthenticated)
            {
                Console.WriteLine("1. Авторизоваться");
                Console.WriteLine("2. Зарегистрироваться");
                Console.WriteLine("3. Выйти из программы");
            }
            else
            {
                Console.WriteLine("1. Добавить пользователя");
                Console.WriteLine("2. Удалить пользователя");
                Console.WriteLine("3. Найти пользователя по имени");
                Console.WriteLine("4. Вывести всех пользователей");
                Console.WriteLine("5. Выйти из учетной записи");
                Console.WriteLine("6. Выйти из программы");
            }

            Console.Write("Выберите опцию: ");
            string choice = Console.ReadLine();

            // Обработка выбора...
        }
    }

    static bool Authorize()
    {
        Console.WriteLine("Введите имя пользователя:");
        string username = Console.ReadLine();
        Console.WriteLine("Введите пароль:");
        string password = Console.ReadLine();

        if (userCredentials.TryGetValue(username, out string storedPassword) && storedPassword == password)
        {
            Console.WriteLine("Успешная авторизация!");
            return true;
        }
        else
        {
            Console.WriteLine("Неверное имя пользователя или пароль.");
            return false;
        }
    }

    static void Register()
    {
        Console.WriteLine("Введите имя пользователя для регистрации:");
        string username = Console.ReadLine();

        if (string.IsNullOrEmpty(username))
        {
            Console.WriteLine("Имя пользователя не может быть пустым.");
            return;
        }

        if (userCredentials.ContainsKey(username))
        {
            Console.WriteLine("Пользователь с таким именем уже существует.");
            return;
        }

        Console.WriteLine("Введите пароль:");
        string password = Console.ReadLine();

        if (password.Length < 8)
        {
            Console.WriteLine("Пароль слишком короткий. Минимум 8 символов.");
            return;
        }

        userCredentials.Add(username, password);
        Console.WriteLine("Пользователь успешно зарегистрирован.");
    }

    static void FindUser()
    {
        Console.WriteLine("Введите имя пользователя для поиска:");
        string name = Console.ReadLine();
        User userFound = users.Find(u => u.Name == name);

        if (userFound != null)
        {
            Console.WriteLine($"Найден пользователь: {userFound.Name}, возраст {userFound.Age}");
        }
        else
        {
            Console.WriteLine("Пользователь не найден.");
        }
    }

    static void DisplayUsers()
    {
        if (users.Count == 0)
        {
            Console.WriteLine("Список пользователей пуст.");
            return;
        }

        for (int i = 0; i < users.Count; i++)
        {
            Console.WriteLine($"Имя: {users[i].Name}, Возраст: {users[i].Age}");
        }
    }
}

class User
{
    public string Name;
    public int Age;

    public User(string name, int age)
    {
        Name = name;
        Age = age;
    }
}
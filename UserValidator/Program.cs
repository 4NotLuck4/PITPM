using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        int totalTests = 0;
        int successfulTests = 0;
        int failedTests = 0;

        string filePath = "C:\\MOCK_DATA.csv"; // полный путь

        try
        {
            // Читаем все строки из файла
            var allLines = File.ReadAllLines(filePath);

            // Пропускаем заголовок (первую строку) и обрабатываем остальные
            foreach (var line in allLines.Skip(1))
            {
                totalTests++;

                // Разделяем строку на значения.
                var values = line.Split(',');

                // Извлекаем и преобразуем значения. Защита от отсутствующих данных.
                string username = values.Length > 0 ? values[0] : null;
                // Проверяем на пустую строку или строку "null"
                if (username == "" || username == "null") username = null;

                string password = values.Length > 1 ? values[1] : null;
                if (password == "" || password == "null") password = null;

                // Для числовых полей используем TryParse
                int age = -1; // Значение по умолчанию, которое заведомо невалидно
                if (values.Length > 2 && !string.IsNullOrEmpty(values[2]))
                {
                    _ = int.TryParse(values[2], out age);
                }

                string email = values.Length > 3 ? values[3] : null;
                if (email == "" || email == "null") email = null;

                // Вызываем тестируемый метод
                bool isValid = ValidateUser(username, password, age, email);

                // Выводим результат для каждой строки(необязательно)
                //Console.WriteLine($"Line {totalTests}: {isValid} - {username}, {password}, {age}, {email}");

                // Подсчитываем результаты
                if (isValid)
                {
                    successfulTests++;
                }
                else
                {
                    failedTests++;
                }
            }

            // Выводим итоговый отчет
            Console.WriteLine("\n--- Результаты тестирования ---");
            Console.WriteLine($"Всего тестов: {totalTests}");
            Console.WriteLine($"Успешных проверок: {successfulTests}");
            Console.WriteLine($"Неуспешных проверок: {failedTests}");
            if (totalTests > 0)
            {
                double successRate = (double)successfulTests / totalTests * 100;
                Console.WriteLine($"Процент успешных проверок: {successRate:F2}%");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Ошибка: Файл '{filePath}' не найден.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка при чтении файла: {ex.Message}");
        }

    }

    // ТЕСТИРУЕМЫЙ МЕТОД (должен точно соответствовать условию)
    public static bool ValidateUser(string username, string password, int age, string email)
    {
        if (string.IsNullOrWhiteSpace(username) || username.Length < 3 || username.Length > 20)
            return false;
        if (string.IsNullOrEmpty(password) || password.Length < 6 || !password.Any(char.IsDigit))
            return false;
        if (age < 13 || age > 120)
            return false;
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.EndsWith(".edu"))
            return false;
        return true;
    }
}
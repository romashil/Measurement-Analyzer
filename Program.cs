using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== АНАЛИЗ РЕЗУЛЬТАТОВ ИЗМЕРЕНИЙ ===\n\n");
        int numb;
        while (true)
        {
            Console.WriteLine("Введите количество замеров N (5 до 20): ");
            try
            {
                numb = Convert.ToInt16(Console.ReadLine());
                if (numb < 5 || numb > 20) { Console.WriteLine("Неправильное количество"); }
                else { break; }
            }
            catch (FormatException)
            {
                Console.WriteLine("Введите количество!");
            }
        }
        Console.WriteLine("Введите результаты измерений: ");
        float sum = 0;
        List<float> arr = new List<float>();
        for (int i = 0; i < numb; i++)
        {
            try
            {
                float x = Convert.ToSingle(Console.ReadLine().Replace(',', '.'), CultureInfo.InvariantCulture);
                arr.Add(x);
                sum += x;
            }
            catch (FormatException)
            {
                Console.WriteLine("Введите число!");
                i--;
            }
        }
        float srznach = sum / numb;
        float sqSum = 0;
        foreach (float x in arr)
        {
            sqSum += (x - srznach) * (x - srznach);
        }
        float sko = (float)Math.Sqrt(sqSum / numb);
        float otnPogr = sko / srznach * 100;

        Console.WriteLine($"=== РЕЗУЛЬТАТЫ АНАЛИЗА ===\nКоличество измерений: {numb}\nСреднее значение: {srznach:F3}\nМинимальное число: {arr.Min():F3}\nМаксимальное число: {arr.Max():F3}\nРазмах: {(arr.Max() - arr.Min()):F3}\nСреднеквадратическое отклонение: {sko:F3}\nОтносительная погрешность: {otnPogr:F1}%");
        Console.WriteLine($"\n=== ФИЛЬТРАЦИЯ ВЫБРОСОВ ===\nДопустимый диапазон: {srznach:F3} ± {2 * sko:F3}\nОтфильтрованные данные:");
        List<float> filtr = new List<float>();
        foreach (float x in arr)
        {
            if (Math.Abs(x - srznach) <= 2 * sko)
            {
                filtr.Add(x);
                Console.Write($"{x:F3} ");
            }
        }
        Console.WriteLine($"\nОсталось измерений после фильтрации: {filtr.Count}");
    }
}
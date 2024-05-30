using System;
using System.Threading;

class Program
{
    static Semaphore semaphore = new Semaphore(1, 1);

    static void Main(string[] args)
    {
        for (int i = 1; i <= 5; i++)
        {
            new Thread(CriticalSection).Start(i);
        }
    }

    static void CriticalSection(object id)
    {
        Console.WriteLine($"Поток {id} пытается зайти в критическую секцию");
        semaphore.WaitOne();
        Console.WriteLine($"Поток {id} успешно вошел в критическую секцию");
        Thread.Sleep(1000);
        Console.WriteLine($"Поток {id} выходит из критической секции");
        semaphore.Release();
    }
}

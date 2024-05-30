using System;
using System.Collections.Generic;
using System.Threading;

class Elevator
{
    private Queue<int> targetFloors = new Queue<int>();
    private Semaphore semaphore = new Semaphore(1, 1);
    private int currentFloor = 1;

    public void AddFloor(int floor)
    {
        semaphore.WaitOne();
        targetFloors.Enqueue(floor);
        semaphore.Release();
    }

    public void Start()
    {
        while (true)
        {
            semaphore.WaitOne();
            if (targetFloors.Count > 0)
            {
                int targetFloor = targetFloors.Dequeue();
                semaphore.Release();

                Console.WriteLine($"Moving from floor {currentFloor} to floor {targetFloor}...");
                Thread.Sleep(Math.Abs(currentFloor - targetFloor) * 1000);
                currentFloor = targetFloor;
                Console.WriteLine($"Arrived at floor {currentFloor}.");
            }
            else
            {
                semaphore.Release();
                Thread.Sleep(1000);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var elevator = new Elevator();
        new Thread(elevator.Start).Start();

        while (true)
        {
            Console.Write("Enter target floor: ");
            int floor = int.Parse(Console.ReadLine());
            elevator.AddFloor(floor);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading;

class Elevator
{
    private Queue<int> targetFloors = new Queue<int>();
    private object lockObject = new object();
    private int currentFloor = 1;

    public void AddFloor(int floor)
    {
        lock (lockObject)
        {
            targetFloors.Enqueue(floor);
        }
    }

    public void Start()
    {
        while (true)
        {
            int targetFloor = 0;
            lock (lockObject)
            {
                if (targetFloors.Count > 0)
                {
                    targetFloor = targetFloors.Dequeue();
                }
            }

            if (targetFloor > 0)
            {
                Console.WriteLine($"Moving from floor {currentFloor} to floor {targetFloor}...");
                Thread.Sleep(Math.Abs(currentFloor - targetFloor) * 1000);
                currentFloor = targetFloor;
                Console.WriteLine($"Arrived at floor {currentFloor}.");
            }
            else
            {
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
using System;
using System.Collections.Generic;
using System.Linq;

namespace _20_lab_10_variant
{
    class Program
    {
        class Worker
        {
            private string name;
            private DateTime dateOfStartWorking;
            private double salary;
            private double prize;
            private int countOfWorkDays;
            private double accrued;
            private double deducted;

            public string Name
            {
                get => name;
                set
                {
                    if (!Char.IsUpper(value[0]))
                        throw new Exception("Invalid value of name");
                    name = value;
                }
            }
            public DateTime DateOfStartWorking { get => dateOfStartWorking; set => dateOfStartWorking = value; }
            public double Salary
            {
                get => salary;
                set
                {
                    if (value < 0 || value.ToString().Substring(value.ToString().IndexOf(',') + 1).Length != 2)
                        throw new Exception("Invalid value of salary");
                    salary = value;
                }
            }
            public double Prize
            {
                get => prize;
                set
                {
                    if (value < 0 || value.ToString().Substring(value.ToString().IndexOf(',') + 1).Length != 2)
                        throw new Exception("Invalid value of prize");
                    prize = value;
                }
            }
            public double Accrued
            {
                get => accrued;
                set
                {
                    if (value < 0 || value.ToString().Substring(value.ToString().IndexOf(',') + 1).Length != 2)
                        throw new Exception("Invalid value of accrued");
                    accrued = value;
                }
            }
            public double Deducted
            {
                get => deducted;
                set
                {
                    if (value < 0 || value.ToString().Substring(value.ToString().IndexOf(',') + 1).Length != 2)
                        throw new Exception("Invalid value of deducted");
                    deducted = value;
                }
            }
            public int CountOfWorkDays
            {
                get => countOfWorkDays;
                set
                {
                    if (value < 0)
                        throw new Exception("Invalid value of count of work days");
                    countOfWorkDays = value;
                }
            }

            public Worker(string name, DateTime date, double salary, double prize, double accrued, double deducted, int countOfWorkDays)
            {
                Name = name;
                DateOfStartWorking = date;
                Salary = salary;
                Prize = prize;
                Accrued = accrued;
                Deducted = deducted;
                CountOfWorkDays = countOfWorkDays;
            }

            public override string ToString()
            {
                return $"Name: {Name}, Date of start working: {DateOfStartWorking}, Salary: {Salary}, Prize: {Prize}, Accrued: {Accrued}, Deducted: {Deducted}, Count of work days: {CountOfWorkDays}";
            }
        }

        static void Main(string[] args)
        {
            List<Worker> workers = new List<Worker>();
            workers.Add(new Worker("Dngn", new DateTime(2001, 12, 03), 234.43, 27.55, 83.45, 3.34, 17));
            workers.Add(new Worker("Dngn", new DateTime(2002, 12, 03), 238.43, 25.55, 80.45, 2.34, 18));
            workers.Add(new Worker("Qergf", new DateTime(2001, 12, 03), 234.43, 27.55, 80.45, 2.34, 17));
            workers.Add(new Worker("Qergf", new DateTime(2018, 12, 03), 234.43, 25.55, 83.45, 3.34, 18));
            workers.Add(new Worker("Qergf", new DateTime(2002, 12, 03), 238.43, 29.55, 88.45, 1.34, 19));
            workers.Add(new Worker("Dngn", new DateTime(2002, 12, 03), 230.43, 25.55, 88.45, 5.34, 19));
            workers.Add(new Worker("Hfgh", new DateTime(2018, 12, 03), 236.43, 29.55, 83.45, 5.34, 20));
            workers.Add(new Worker("Hfgh", new DateTime(2001, 12, 03), 236.43, 25.55, 81.45, 4.34, 20));
            workers.Add(new Worker("Hfgh", new DateTime(2002, 12, 03), 234.43, 22.55, 81.45, 4.34, 5));
            workers.Add(new Worker("Dngn", new DateTime(2018, 12, 03), 230.43, 22.55, 83.45, 1.34, 5));

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    foreach(Worker worker in workers.OrderBy(el => el.Name).ThenBy(el => el.DateOfStartWorking))
                        Console.WriteLine(worker);
                    break;
                case 2:
                    foreach(Worker worker in workers.Where(el => (2020 - el.DateOfStartWorking.Year) > 3))
                        Console.WriteLine(worker);
                    break;
                case 3: 
                    foreach(Worker worker in workers.OrderBy(el => el.Salary))
                        Console.WriteLine(worker);
                    break;
                case 4:
                    Console.WriteLine(workers.Sum(el => el.Accrued - el.Deducted));
                    break;
                case 5:
                    foreach(IGrouping<string,Worker> item in workers.GroupBy(el => el.Name))
                    {
                        Console.WriteLine(item.Key);
                        foreach(Worker worker in item)
                            Console.WriteLine(worker);
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    foreach (IGrouping<DateTime, Worker> item in workers.GroupBy(el => el.DateOfStartWorking))
                    {
                        Console.WriteLine(item.Key);
                        foreach (Worker worker in item)
                            Console.WriteLine(worker);
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    foreach (IGrouping<int, Worker> item in workers.GroupBy(el => el.CountOfWorkDays))
                    {
                        Console.WriteLine(item.Key);
                        foreach (Worker worker in item)
                            Console.WriteLine(worker);
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    foreach (IGrouping<double, Worker> item in workers.GroupBy(el => el.Salary))
                    {
                        Console.WriteLine(item.Key);
                        foreach (Worker worker in item)
                            Console.WriteLine(worker);
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    foreach (IGrouping<double, Worker> item in workers.GroupBy(el => el.Prize))
                    {
                        Console.WriteLine(item.Key);
                        foreach (Worker worker in item)
                            Console.WriteLine(worker);
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    foreach (IGrouping<double, Worker> item in workers.GroupBy(el => el.Accrued))
                    {
                        Console.WriteLine(item.Key);
                        foreach (Worker worker in item)
                            Console.WriteLine(worker);
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    foreach (IGrouping<double, Worker> item in workers.GroupBy(el => el.Deducted))
                    {
                        Console.WriteLine(item.Key);
                        foreach (Worker worker in item)
                            Console.WriteLine(worker);
                    }
                    break;
            }
        }
    }
}

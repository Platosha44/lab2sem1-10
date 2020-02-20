using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

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

        public static XElement AddWorker()
        {
            Console.WriteLine("Input values \n " +
                "name \n" +
                "date \n" +
                "salary \n" +
                "prize \n" +
                "accrued \n" +
                "deducted \n" +
                "countOfWorkDays \n");
            string name = Console.ReadLine();
            DateTime date = Convert.ToDateTime(Console.ReadLine());
            double prize = Convert.ToDouble(Console.ReadLine());
            double salary = Convert.ToDouble(Console.ReadLine());
            double accrued = Convert.ToDouble(Console.ReadLine());
            double deducted = Convert.ToDouble(Console.ReadLine());
            int countOfWorkDays = Convert.ToInt32(Console.ReadLine());
            Worker wr = new Worker(name, date, salary, prize, accrued, deducted, countOfWorkDays);
            return new XElement("Worker", new XAttribute("Name", wr.Name),
                    new XElement("Date", wr.DateOfStartWorking),
                    new XElement("Salary", wr.Salary),
                    new XElement("Prize", wr.Prize),
                    new XElement("Accrued", wr.Accrued),
                    new XElement("Deducted", wr.Deducted),
                    new XElement("CountOfWorkDays", wr.CountOfWorkDays));
        }
        public static int Choice()
        {
            Console.WriteLine("Choose: \n" +
                "1 - save doc \n" +
                "2 - add object \n" +
                "3 - individual tasks \n" +
                "4 - out \n");
            int choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }
        static XDocument CreateDocGroup<T>(IEnumerable<IGrouping<T, Worker>> group)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Grouping"));
            foreach (var item in group)
            {
                doc.Root.Add(new XElement("Group", new XAttribute("GroupAttribute", item.Key),
                                                    item.ToList().Select(wr => new XElement("Worker",
                                                    new XAttribute("Name", wr.Name),
                                                    new XElement("Date", wr.DateOfStartWorking),
                                                    new XElement("Salary", wr.Salary),
                                                    new XElement("Prize", wr.Prize),
                                                    new XElement("Accrued", wr.Accrued),
                                                    new XElement("Deducted", wr.Deducted),
                                                    new XElement("CountOfWorkDays", wr.CountOfWorkDays)))));
            }
            return doc;
        }
        static XDocument CreateDocument(List<Worker> list)
        {
            XDocument task1 = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("task1", list.Select(wr => new XElement("Worker",
                        new XAttribute("Name", wr.Name),
                        new XElement("Date", wr.DateOfStartWorking),
                        new XElement("Salary", wr.Salary),
                        new XElement("Prize", wr.Prize),
                        new XElement("Accrued", wr.Accrued),
                        new XElement("Deducted", wr.Deducted),
                        new XElement("CountOfWorkDays", wr.CountOfWorkDays)))));
            return task1;
        }
        public static void SaveDoc(XDocument doc, string fileName)
        {
            doc.Save(Path.Combine(Environment.CurrentDirectory, fileName));
        }
        static void IndividualTasks(List<Worker> workers)
        {
            Console.WriteLine("Input value from 1-5");
            int number = Convert.ToInt32(Console.ReadLine());
            switch (number)
            {
                case 1:
                    var answer0 = workers.OrderBy(el => el.Name).ThenBy(el => el.DateOfStartWorking);
                    foreach (Worker worker in answer0) Console.WriteLine(worker);
                    XDocument task1 = CreateDocument(answer0.ToList());
                    SaveDoc(task1, "task1.xml");
                    break;
                case 2:
                    var answer1 = workers.Where(el => (2020 - el.DateOfStartWorking.Year) > 3);
                    foreach (Worker worker in answer1) Console.WriteLine(worker);
                    XDocument task2 = CreateDocument(answer1.ToList());
                    SaveDoc(task2, "task2.xml");
                    break;
                case 3:
                    var answer2 = workers.OrderBy(el => el.Salary);
                    foreach (Worker worker in answer2) Console.WriteLine(worker);
                    XDocument task3 = CreateDocument(answer2.ToList());
                    SaveDoc(task3, "task3.xml");
                    break;
                case 4:
                    Console.WriteLine(workers.Sum(el => el.Accrued - el.Deducted));
                    XDocument task4 = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Group"));
                    double answer3 = workers.Sum(el => el.Accrued - el.Deducted);
                    task4.Root.Add(new XElement("Sum", answer3));
                    SaveDoc(task4, "task4.xml");
                    break;
                case 5:
                    var groupName = workers.GroupBy(el => el.Name);
                    foreach (IGrouping<string, Worker> item in groupName)
                    {
                        Console.WriteLine(item.Key);
                        foreach (Worker worker in item)
                            Console.WriteLine(worker);
                    }
                    XDocument group = CreateDocGroup(groupName);
                    SaveDoc(group, "group.xml");

                    var groupDateOfStartWorking = workers.GroupBy(el => el.DateOfStartWorking);
                    foreach (IGrouping<DateTime, Worker> item in groupDateOfStartWorking)
                    {
                        Console.WriteLine(item.Key);
                        foreach (Worker worker in item)
                            Console.WriteLine(worker);
                    }
                    XDocument group1 = CreateDocGroup(groupDateOfStartWorking);
                    SaveDoc(group1, "group1.xml");

                    var groupOfCountWorkDays = workers.GroupBy(el => el.CountOfWorkDays);
                    foreach (IGrouping<int, Worker> item in groupOfCountWorkDays)
                    {
                        Console.WriteLine(item.Key);
                        foreach (Worker worker in item)
                            Console.WriteLine(worker);
                    }
                    XDocument group2 = CreateDocGroup(groupOfCountWorkDays);
                    SaveDoc(group2, "group2.xml");

                    var groupSalary = workers.GroupBy(el => el.Salary);
                    foreach (IGrouping<double, Worker> item in groupSalary)
                    {
                        Console.WriteLine(item.Key);
                        foreach (Worker worker in item)
                            Console.WriteLine(worker);
                    }
                    XDocument group3 = CreateDocGroup(groupSalary);
                    SaveDoc(group3, "group3.xml");

                    var groupPrize = workers.GroupBy(el => el.Prize);
                    foreach (IGrouping<double, Worker> item in groupPrize)
                    {
                        Console.WriteLine(item.Key);
                        foreach (Worker worker in item)
                            Console.WriteLine(worker);
                    }
                    XDocument group4 = CreateDocGroup(groupPrize);
                    SaveDoc(group4, "group4.xml");

                    var groupAccrued = workers.GroupBy(el => el.Accrued);
                    foreach (IGrouping<double, Worker> item in groupAccrued)
                    {
                        Console.WriteLine(item.Key);
                        foreach (Worker worker in item)
                            Console.WriteLine(worker);
                    }
                    XDocument group5 = CreateDocGroup(groupAccrued);
                    SaveDoc(group5, "group5.xml");

                    var groupDeducted = workers.GroupBy(el => el.Deducted);
                    foreach (IGrouping<double, Worker> item in groupDeducted)
                    {
                        Console.WriteLine(item.Key);
                        foreach (Worker worker in item)
                            Console.WriteLine(worker);
                    }
                    XDocument group6 = CreateDocGroup(groupDeducted);
                    SaveDoc(group6, "group6.xml");
                    break;
                default:
                    throw new Exception("Wrong number");
            }
        }
        static void Start(List<Worker> workers)
        {
            int choice = Choice();
            var xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Workers", workers.Select(wr => new XElement("Worker",
                                                            new XAttribute("Name", wr.Name),
                                                            new XElement("Date", wr.DateOfStartWorking),
                                                            new XElement("Salary", wr.Salary),
                                                            new XElement("Prize", wr.Prize),
                                                            new XElement("Accrued", wr.Accrued),
                                                            new XElement("Deducted", wr.Deducted),
                                                            new XElement("CountOfWorkDays", wr.CountOfWorkDays)))));
            switch (choice)
            {
                case 1:
                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml"));
                    break;
                case 2:
                    xDoc.Root.Add(AddWorker());
                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml"));
                    break;
                case 3:
                    IndividualTasks(workers);
                    break;
                case 4:
                    break;
                default:
                    throw new Exception("wrong value");
            }
            Console.ReadKey();
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

            Start(workers);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace _20_lab_5_variant
{
    class Program
    {
        class BankAccount : IEnumerable<BankAccount>
        {
            private long code;
            private string name;
            private double amount;
            private int procent;
            private DateTime date;

            public long Code
            {
                get => code;
                set
                {
                    if (value.ToString().Length != 12 || value < 0)
                    {
                        throw new Exception("Invalid value of code");
                    }
                    code = value;
                }
            }
            public DateTime Date { get => date; set => date = value; }
            public string Name
            {
                get => name;
                set
                {
                    if (!char.IsUpper(value[0]))
                    {
                        throw new Exception("Invalid value of name");
                    }
                    name = value;
                }
            }
            public double Amount
            {
                get => amount;
                set
                {
                    if(value.ToString().Split(",")[1].Length != 2)
                    {
                        throw new Exception("Invalid value of amount");
                    }
                    amount = value;
                }
            }
            public int Procent
            {
                get => procent;
                set
                {
                    if(value < 0 || value > 100)
                    {
                        throw new Exception("Invalid value of procent");
                    }
                    procent = value;
                }
            }

            public BankAccount(long code, string name, double amount, int procent, DateTime date)
            {
                Code = code;
                Name = name;
                Amount = amount;
                Procent = procent;
                Date = date;
            }

            public override string ToString()
            {
                return $"Code: {Code}, Name: {Name},Amount: {Amount}$,Procent: {Procent}%,Date: {Date}";
            }

            public IEnumerator<BankAccount> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
        public static XElement AddBankAccount()
        {
            Console.WriteLine("Input values \n " +
                "code \n" +
                "name \n" +
                "amount \n" +
                "procent \n" +
                "date \n");
            long code = Convert.ToInt64(Console.ReadLine());
            string name = Console.ReadLine();
            int procent = Convert.ToInt32(Console.ReadLine());
            DateTime date = Convert.ToDateTime(Console.ReadLine());
            double amount = Convert.ToDouble(Console.ReadLine());
            BankAccount ba = new BankAccount(code, name, amount, procent, date);
            return new XElement("BankAccount", new XAttribute("Code", ba.Code),
                    new XElement("Name", ba.Name),
                    new XElement("Amount", ba.Amount),
                    new XElement("Procent", ba.Procent),
                    new XElement("Date", ba.Date));
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
        static XDocument CreateDocGroup<T>(IEnumerable<IGrouping<T, BankAccount>> group)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Grouping"));
            foreach (var item in group)
            {
                doc.Root.Add(new XElement("Group", new XAttribute("GroupAttribute", item.Key),
                                                    item.ToList().Select(ba => new XElement("BankAccount", new XAttribute("Code", ba.Code),
                                    new XElement("Name", ba.Name),
                                    new XElement("Amount", ba.Amount),
                                    new XElement("Procent", ba.Procent),
                                    new XElement("Date", ba.Date)))));
            }
            return doc;
        }
        static XDocument CreateDocument(List<BankAccount> list)
        {
            XDocument task1 = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("task1", list.Select(ba => new XElement("BankAccount",
                                    new XAttribute("Code", ba.Code),
                                    new XElement("Name", ba.Name),
                                    new XElement("Amount", ba.Amount),
                                    new XElement("Procent", ba.Procent),
                                    new XElement("Date", ba.Date)))));
            return task1;
        }
        public static void SaveDoc(XDocument doc, string fileName)
        {
            doc.Save(Path.Combine(Environment.CurrentDirectory, fileName));
        }
        static void IndividualTasks(List<BankAccount> list)
        {
            Console.WriteLine("Input value from 1-5");
            int number = Convert.ToInt32(Console.ReadLine());
            switch (number)
            {
                case 1:
                    var answer0 = list.OrderBy(el => el.Date).ThenBy(el => el.Name);
                    foreach (BankAccount bankAccount in answer0)
                    {
                        Console.WriteLine(bankAccount);
                    }
                    XDocument task1 = CreateDocument(answer0.ToList());
                    SaveDoc(task1, "task1.xml");
                    break;
                case 2:
                    var answer1 = list.Where(el => el.Name == "Inside");
                    foreach (BankAccount bankAccount in answer1)
                    {
                        Console.WriteLine(bankAccount.Code);
                    }
                    XDocument task2 = CreateDocument(answer1.ToList());
                    SaveDoc(task2, "task2.xml");
                    break;
                case 3:
                    var answer2 = list.Where(el => el.Date.Year == 2013);
                    foreach (BankAccount bankAccount in answer2)
                    {
                        Console.WriteLine(bankAccount.Name);
                    }
                    XDocument task3 = CreateDocument(answer2.ToList());
                    SaveDoc(task3, "task3.xml");
                    break;
                case 4:
                    var answer3 = list.OrderBy(el => el.Amount).ToList();
                    Console.WriteLine(answer3[answer3.Count - 1].Amount + "$");
                    XDocument task4 = CreateDocument(answer3);
                    SaveDoc(task4, "task4.xml");
                    break;
                case 5:
                    var groupCode = list.GroupBy(el => el.Code);
                    foreach (IGrouping<long, BankAccount> bankAccounts in groupCode)
                    {
                        Console.WriteLine(bankAccounts.Key);
                        foreach (var b in bankAccounts)
                        {
                            Console.WriteLine(b);
                        }
                    }
                    XDocument group = CreateDocGroup(groupCode);
                    SaveDoc(group, "group.xml");

                    var groupName = list.GroupBy(el => el.Name);
                    foreach (IGrouping<string, BankAccount> bankAccounts in groupName)
                    {
                        Console.WriteLine(bankAccounts.Key);
                        foreach (var b in bankAccounts)
                        {
                            Console.WriteLine(b);
                        }
                    }
                    XDocument group1 = CreateDocGroup(groupName);
                    SaveDoc(group1, "group1.xml");

                    var groupAmount = list.GroupBy(el => el.Amount);
                    foreach (IGrouping<double, BankAccount> bankAccounts in groupAmount)
                    {
                        Console.WriteLine(bankAccounts.Key);
                        foreach (var b in bankAccounts)
                        {
                            Console.WriteLine(b);
                        }
                    }
                    XDocument group2 = CreateDocGroup(groupAmount);
                    SaveDoc(group2, "group2.xml");

                    var groupProcent = list.GroupBy(el => el.Procent);
                    foreach (IGrouping<int, BankAccount> bankAccounts in groupProcent)
                    {
                        Console.WriteLine(bankAccounts.Key);
                        foreach (var b in bankAccounts)
                        {
                            Console.WriteLine(b);
                        }
                    }
                    XDocument group3 = CreateDocGroup(groupProcent);
                    SaveDoc(group3, "group3.xml");

                    var groupDate = list.GroupBy(el => el.Date);
                    foreach (IGrouping<DateTime, BankAccount> bankAccounts in groupDate)
                    {
                        Console.WriteLine(bankAccounts.Key);
                        foreach (var b in bankAccounts)
                        {
                            Console.WriteLine(b);
                        }
                    }
                    XDocument group4 = CreateDocGroup(groupDate);
                    SaveDoc(group4, "group4.xml");
                    break;
                default:
                    throw new Exception("Wrong number");
            }
        }
        static void Start(List<BankAccount> bankAccounts)
        {
            int choice = Choice();
            var xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("BankAccounts", bankAccounts.Select(ba => new XElement("BankAccount",
                                                                        new XAttribute("Code", ba.Code),
                                                                        new XElement("Name", ba.Name),
                                                                        new XElement("Amount", ba.Amount),
                                                                        new XElement("Procent", ba.Procent),
                                                                        new XElement("Date", ba.Date)))));
            switch (choice)
            {
                case 1:
                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml"));
                    break;
                case 2:
                    xDoc.Root.Add(AddBankAccount());
                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml"));
                    break;
                case 3:
                    IndividualTasks(bankAccounts);
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
            List<BankAccount> list = new List<BankAccount>();

            list.Add(new BankAccount(100000000000, "Ohhhhhh", 232324.12, 4, new DateTime(2013,12,30)));
            list.Add(new BankAccount(111111111111, "Nahyi", 232324.12, 3, new DateTime(2011, 11, 20)));
            list.Add(new BankAccount(222222222222, "Ymeret", 235667.12, 8, new DateTime(2010, 7, 15)));
            list.Add(new BankAccount(333333333333, "Hochy", 23256.12, 4, new DateTime(2017, 8, 11)));
            list.Add(new BankAccount(444444444444, "Programmirovanie", 237684.12, 4, new DateTime(2011, 5, 9)));
            list.Add(new BankAccount(555555555555, "Ebuchee", 237865.12, 8, new DateTime(2011, 7, 16)));
            list.Add(new BankAccount(666666666666, "AAAAAA", 234578.12, 4, new DateTime(2011, 11, 20)));
            list.Add(new BankAccount(777777777777, "Suka", 2326568.12, 3, new DateTime(2017, 8, 11)));
            list.Add(new BankAccount(888888888888, "Inside", 2328778.12, 3, new DateTime(2010, 7, 15)));
            list.Add(new BankAccount(999999999999, "Dead", 2389994.12, 8, new DateTime(2010, 7, 15)));

            Start(list);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    var answer0 = list.OrderBy(el => el.Date).ThenBy(el => el.Name);
                    foreach(BankAccount bankAccount in answer0)
                    {
                        Console.WriteLine(bankAccount);
                    }
                    break;
                case 2:
                    var answer1 = list.Where(el => el.Name == "Inside");
                    foreach(BankAccount bankAccount in answer1)
                    {
                        Console.WriteLine(bankAccount.Code);
                    }
                    break;
                case 3:
                    var answer2 = list.Where(el => el.Date.Year == 2013);
                    foreach(BankAccount bankAccount in answer2)
                    {
                        Console.WriteLine(bankAccount.Name);
                    }
                    break;
                case 4:
                    var answer3 = list.OrderBy(el => el.Amount).Last();
                    Console.WriteLine(answer3.Amount+"$");
                    break;
                case 5:
                    var answer4 = list.GroupBy(el => el.Code);
                    foreach(IGrouping<long,BankAccount> bankAccounts in answer4)
                    {
                        Console.WriteLine(bankAccounts.Key);
                        foreach(var b in bankAccounts)
                        {
                            Console.WriteLine(b);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    var answer5 = list.GroupBy(el => el.Name);
                    foreach (IGrouping<string, BankAccount> bankAccounts in answer5)
                    {
                        Console.WriteLine(bankAccounts.Key);
                        foreach (var b in bankAccounts)
                        {
                            Console.WriteLine(b);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    var answer6 = list.GroupBy(el => el.Amount);
                    foreach (IGrouping<double, BankAccount> bankAccounts in answer6)
                    {
                        Console.WriteLine(bankAccounts.Key);
                        foreach (var b in bankAccounts)
                        {
                            Console.WriteLine(b);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    var answer7 = list.GroupBy(el => el.Procent);
                    foreach (IGrouping<int, BankAccount> bankAccounts in answer7)
                    {
                        Console.WriteLine(bankAccounts.Key);
                        foreach (var b in bankAccounts)
                        {
                            Console.WriteLine(b);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    var answer8 = list.GroupBy(el => el.Date);
                    foreach (IGrouping<DateTime, BankAccount> bankAccounts in answer8)
                    {
                        Console.WriteLine(bankAccounts.Key);
                        foreach (var b in bankAccounts)
                        {
                            Console.WriteLine(b);
                        }
                    }
                    break;

            }
        }
    }
}

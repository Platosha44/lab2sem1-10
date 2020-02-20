using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace _20_lab_9_variant
{
    class Program
    {
        public class PaymentPhoneCall
        {
            private string surname;
            private string phone;
            private DateTime dateOfCall;
            private double tarif;
            private int sale;
            private TimeSpan start;
            private TimeSpan finish;

            public string Surname
            {
                get => surname;
                set
                {
                    if (!Char.IsUpper(value[0]))
                    {
                        throw new Exception("Invalid value of surname");
                    }
                    surname = value;
                }
            }
            public string Phone
            {
                get => phone;
                set
                {
                    Regex check = new Regex(@"^\+375(44|29|25)\d{7}$");
                    if (!check.IsMatch(value))
                    {
                        throw new Exception("Invalid value of phone");
                    }
                    phone = value;
                }
            }
            public DateTime DateOfCall { get => dateOfCall; set => dateOfCall = value; }
            public double Tarif
            {
                get => tarif;
                set
                {
                    if(value < 0 || value.ToString().Substring(value.ToString().IndexOf(',') + 1).Length != 2)
                    {
                        throw new Exception("Invalid value of tarif");
                    }
                    tarif = value;
                }
            }
            public int Sale
            {
                get => sale;
                set
                {
                    if(value < 0 || value > 100)
                    {
                        throw new Exception("Invalid value of sale");
                    }
                    sale = value;
                }
            }
            public TimeSpan Start { get => start; set => start = value; }
            public TimeSpan Finish { get => finish; set => finish = value; }

            public PaymentPhoneCall(string surname, string phone, DateTime dateOfCall, double tarif, int sale, TimeSpan start, TimeSpan finish)
            {
                Surname = surname;
                Phone = phone;
                DateOfCall = dateOfCall;
                Tarif = tarif;
                Sale = sale;
                Start = start;
                Finish = finish;
            }

            public override string ToString()
            {
                return $"Surname: {Surname}, Phone: {Phone}, Date of call: {DateOfCall}, Tarif: {Tarif}, Start: {Start}, Finish: {Finish}";
            }
        }

        public static XElement AddPaymentPhoneCall()
        {
            Console.WriteLine("Input values \n " +
                "surname \n" +
                "phone \n" +
                "dateOfCall \n" +
                "tarif \n" +
                "sale \n" +
                "start \n" +
                "finish \n");
            string surname = Console.ReadLine();
            string phone = Console.ReadLine();
            DateTime dateOfCall = Convert.ToDateTime(Console.ReadLine());
            double tarif = Convert.ToDouble(Console.ReadLine());
            int sale = Convert.ToInt32(Console.ReadLine());
            TimeSpan start = new TimeSpan(5,3,6);
            TimeSpan finish = new TimeSpan(7,1,9);

            PaymentPhoneCall pp = new PaymentPhoneCall(surname, phone, dateOfCall, tarif, sale, start, finish);
            return new XElement("PaymentPhoneCall", new XAttribute("Surname", pp.Surname),
                    new XElement("Phone", pp.Phone),
                    new XElement("DateOfCall", pp.DateOfCall),
                    new XElement("Tarif", pp.Tarif),
                    new XElement("Sale", pp.Sale),
                    new XElement("Start", pp.Start),
                    new XElement("Finish", pp.Finish));
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
        public static XDocument CreateDocGroup<T>(IEnumerable<IGrouping<T, PaymentPhoneCall>> group)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Grouping"));
            foreach (var item in group)
            {
                doc.Root.Add(new XElement("Group", new XAttribute("GroupAttribute", item.Key),
                                                    item.ToList().Select(pp => new XElement("PaymentPhoneCall",
                                                    new XAttribute("Surname", pp.Surname),
                                                    new XElement("Phone", pp.Phone),
                                                    new XElement("DateOfCall", pp.DateOfCall),
                                                    new XElement("Tarif", pp.Tarif),
                                                    new XElement("Sale", pp.Sale),
                                                    new XElement("Start", pp.Start),
                                                    new XElement("Finish", pp.Finish)))));
            }
            return doc;
        }
        public static XDocument CreateDocument(List<PaymentPhoneCall> list)
        {
            XDocument task1 = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("task1", list.Select( pp => new XElement("PaymentPhoneCall",
                        new XAttribute("Surname", pp.Surname),
                        new XElement("Phone", pp.Phone),
                        new XElement("DateOfCall", pp.DateOfCall),
                        new XElement("Tarif", pp.Tarif),
                        new XElement("Sale", pp.Sale),
                        new XElement("Start", pp.Start),
                        new XElement("Finish", pp.Finish)))));
            return task1;
        }
        public static void SaveDoc(XDocument doc, string fileName)
        {
            doc.Save(Path.Combine(Environment.CurrentDirectory, fileName));
        }
        public static void IndividualTasks(List<PaymentPhoneCall> calls)
        {
            Console.WriteLine("Input value from 1-5");
            int number = Convert.ToInt32(Console.ReadLine());
            switch (number)
            {
                case 1:
                    var answer0 = calls.OrderBy(el => el.Surname).ThenBy(el => el.DateOfCall).ToList();
                    foreach (PaymentPhoneCall call in answer0)
                    {
                        Console.WriteLine(call);
                    }
                    XDocument task1 = CreateDocument(answer0.ToList());
                    SaveDoc(task1, "task1.xml");
                    break;
                case 2:
                    var answer1 = calls.GroupBy(el => el.Phone).ToList();
                    foreach (IGrouping<string, PaymentPhoneCall> call in answer1)
                    {
                        if (call.ToList().Count > 1)
                        {
                            Console.WriteLine(call.Key);
                            foreach (PaymentPhoneCall item in call)
                            {
                                Console.WriteLine(item);
                            }
                        }
                    }
                    XDocument task2 = CreateDocument(answer1[0].ToList());
                    SaveDoc(task2, "task2.xml");
                    break;
                case 3:
                    var answer2 = calls.OrderByDescending(el => el.Finish - el.Start).ToList();
                    foreach (PaymentPhoneCall call in answer2)
                    {
                        Console.WriteLine(call);
                    }
                    XDocument task3 = CreateDocument(answer2);
                    SaveDoc(task3, "task3.xml");
                    break;
                case 4:
                    var answer3 = calls.OrderBy(el => (el.Tarif * el.Finish.Minutes - el.Tarif * el.Sale / 100)).ToList();
                    Console.WriteLine(answer3[0].Tarif);
                    XDocument task4 = CreateDocument(answer3);
                    SaveDoc(task4, "task4.xml");
                    break;
                case 5:
                    var groupSurname = calls.GroupBy(el => el.Surname);
                    foreach (IGrouping<string, PaymentPhoneCall> call in groupSurname)
                    {
                        Console.WriteLine(call.Key);
                        foreach (PaymentPhoneCall item in call)
                            Console.WriteLine(item);
                    }
                    XDocument group = CreateDocGroup(groupSurname);
                    SaveDoc(group, "group.xml");

                    var groupPhone = calls.GroupBy(el => el.Phone);
                    foreach (IGrouping<string, PaymentPhoneCall> call in groupPhone)
                    {
                        Console.WriteLine(call.Key);
                        foreach (PaymentPhoneCall item in call)
                            Console.WriteLine(item);
                    }
                    XDocument group1 = CreateDocGroup(groupPhone);
                    SaveDoc(group1, "group1.xml");

                    var groupTarif = calls.GroupBy(el => el.Tarif);
                    foreach (IGrouping<double, PaymentPhoneCall> call in groupTarif)
                    {
                        Console.WriteLine(call.Key);
                        foreach (PaymentPhoneCall item in call)
                            Console.WriteLine(item);
                    }
                    XDocument group2 = CreateDocGroup(groupTarif);
                    SaveDoc(group2, "group2.xml");

                    var groupStart = calls.GroupBy(el => el.Start);
                    foreach (IGrouping<TimeSpan, PaymentPhoneCall> call in groupStart)
                    {
                        Console.WriteLine(call.Key);
                        foreach (PaymentPhoneCall item in call)
                            Console.WriteLine(item);
                    }
                    XDocument group3 = CreateDocGroup(groupStart);
                    SaveDoc(group3, "group3.xml");

                    var groupFinish = calls.GroupBy(el => el.Finish);
                    foreach (IGrouping<TimeSpan, PaymentPhoneCall> call in groupFinish)
                    {
                        Console.WriteLine(call.Key);
                        foreach (PaymentPhoneCall item in call)
                            Console.WriteLine(item);
                    }
                    XDocument group4 = CreateDocGroup(groupFinish);
                    SaveDoc(group4, "group4.xml");

                    var groupDateOfCall = calls.GroupBy(el => el.DateOfCall);
                    foreach (IGrouping<DateTime, PaymentPhoneCall> call in groupDateOfCall)
                    {
                        Console.WriteLine(call.Key);
                        foreach (PaymentPhoneCall item in call)
                            Console.WriteLine(item);
                    }
                    XDocument group5 = CreateDocGroup(groupDateOfCall);
                    SaveDoc(group5, "group5.xml");
                    break;
                default:
                    throw new Exception("Wrong number");
            }
        }
        public static void Start(List<PaymentPhoneCall> calls)
        {
            int choice = Choice();
            var xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("PaymentPhoneCalls", calls.Select(pp => new XElement("PaymentPhoneCall",
                                                    new XAttribute("Surname", pp.Surname),
                                                    new XElement("Phone", pp.Phone),
                                                    new XElement("DateOfCall", pp.DateOfCall),
                                                    new XElement("Tarif", pp.Tarif),
                                                    new XElement("Sale", pp.Sale),
                                                    new XElement("Start", pp.Start),
                                                    new XElement("Finish", pp.Finish)))));
            switch (choice)
            {
                case 1:
                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml"));
                    break;
                case 2:
                    xDoc.Root.Add(AddPaymentPhoneCall());
                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml"));
                    break;
                case 3:
                    IndividualTasks(calls);
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
            List<PaymentPhoneCall> calls = new List<PaymentPhoneCall>();
            calls.Add(new PaymentPhoneCall("Sefgh", "+375443452354", new DateTime(2018, 11, 25), 2.54, 20, new TimeSpan(0,2,4),new TimeSpan(3,2,1)));
            calls.Add(new PaymentPhoneCall("Sgfgfg", "+375293445354", new DateTime(2019, 11, 25), 2.14, 20, new TimeSpan(1, 2, 4), new TimeSpan(3, 2, 1)));
            calls.Add(new PaymentPhoneCall("Sefg", "+375293445354", new DateTime(2018, 11, 25), 2.24, 20, new TimeSpan(1, 2, 4), new TimeSpan(4, 2, 1)));
            calls.Add(new PaymentPhoneCall("Sefgh", "+375443452354", new DateTime(2019, 11, 25), 2.54, 20, new TimeSpan(0, 2, 4), new TimeSpan(4, 2, 1)));
            calls.Add(new PaymentPhoneCall("Sefg", "+375443452354", new DateTime(2018, 11, 25), 2.54, 20, new TimeSpan(1, 2, 4), new TimeSpan(4, 2, 1)));
            calls.Add(new PaymentPhoneCall("Sefgh", "+375293445354", new DateTime(2018, 11, 25), 2.24, 20, new TimeSpan(1, 2, 4), new TimeSpan(4, 2, 1)));
            calls.Add(new PaymentPhoneCall("Sefg", "+375443452354", new DateTime(2019, 11, 25), 2.54, 20, new TimeSpan(1, 2, 4), new TimeSpan(4, 2, 1)));
            calls.Add(new PaymentPhoneCall("Sefgh", "+375293445354", new DateTime(2018, 11, 25), 2.14, 20, new TimeSpan(1, 2, 4), new TimeSpan(3, 2, 1)));
            calls.Add(new PaymentPhoneCall("Sgfgfg", "+375443452354", new DateTime(2019, 11, 25), 2.14, 20, new TimeSpan(0, 2, 4), new TimeSpan(3, 2, 1)));
            calls.Add(new PaymentPhoneCall("Sgfgfg", "+375443452354", new DateTime(2018, 11, 25), 2.24, 20, new TimeSpan(0, 2, 4), new TimeSpan(4, 2, 1)));

            Start(calls);
        }
    }
}

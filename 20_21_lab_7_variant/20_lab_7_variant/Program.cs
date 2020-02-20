using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace _20_lab_7_variant
{
    class Program
    {
        class BookInLibrary
        {
            private int id;
            private string surname;
            private DateTime date;
            private int periodOfReturn;
            private string author;
            private string label;
            private int year;
            private double cost;

            public BookInLibrary(int id,string surname, string author, string label, double cost, int year,DateTime date, int periodOfReturn)
            {
                Id = id;
                Surname = surname;
                Date = date;
                PeriodOfReturn = periodOfReturn;
                Author = author;
                Label = label;
                Year = year;
                Cost = cost;
            }
            public int Id
            {
                get => id;
                set
                {
                    if (value < 0 || value.ToString().Length != 8)
                    {
                        throw new Exception("Invalid value of id");
                    }
                    id = value;
                }
            }
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
            public string Author
            {
                get => author;
                set
                {
                    if (!Char.IsUpper(value[0]))
                    {
                        throw new Exception("Invalid value of author");
                    }
                    author = value;
                }
            }
            public string Label
            {
                get => label;
                set
                {
                    if (!Char.IsUpper(value[0]))
                    {
                        throw new Exception("Invalid value of label");
                    }
                    label = value;
                }
            }
            public DateTime Date { get => date; set => date = value; }
            public int PeriodOfReturn
            {
                get => periodOfReturn;
                set
                {
                    if(value < 0)
                    {
                        throw new Exception("Invalid value of period");
                    }
                    periodOfReturn = value;
                }
            }
            public int Year
            {
                get => year;
                set
                {
                    if(value < 0 || value > 2019)
                    {
                        throw new Exception("Invalid value of year");
                    }
                    year = value;
                }
            }
            public double Cost
            {
                get => cost;
                set
                {
                    if (value < 0 || value.ToString().Substring(value.ToString().IndexOf(',') + 1).Length != 2)
                    {
                        throw new Exception("Invalid value of cost");
                    }
                    cost = value;
                }
            }

            public override string ToString()
            {
                return $"Id: {Id}, Surname: {Surname}, Author: {Author}, Label: {Label}, Cost: {Cost}$, Year: {Year},Period Of Return: {PeriodOfReturn}";
            }
        }

        static XElement AddBook()
        {
            Console.WriteLine("Input values \n " +
                "id \n" +
                "surname \n" +
                "author \n" +
                "label \n" +
                "cost \n" +
                "year \n" +
                "date \n" +
                "periodOfReturn \n");
            int id = Convert.ToInt32(Console.ReadLine());
            string surname = Console.ReadLine();
            string author = Console.ReadLine();
            string label = Console.ReadLine();
            DateTime date = Convert.ToDateTime(Console.ReadLine());
            int periodOfReturn = Convert.ToInt32(Console.ReadLine());
            int year = Convert.ToInt32(Console.ReadLine());
            double cost = Convert.ToDouble(Console.ReadLine());
            
            BookInLibrary bl = new BookInLibrary(id, surname, author, label, cost, year, date,periodOfReturn);
            return  new XElement("BookInLibrary", new XAttribute("Id", bl.Id),
                    new XElement("Surname", bl.Surname),
                    new XElement("Author", bl.Author),
                    new XElement("Label", bl.Label),
                    new XElement("Cost", bl.Cost),
                    new XElement("Year", bl.Year),
                    new XElement("Date", bl.Date),
                    new XElement("PeriodOfReturn",bl.PeriodOfReturn));
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
        static XDocument CreateDocGroup<T>(IEnumerable<IGrouping<T, BookInLibrary>> group)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Grouping"));
            foreach (var item in group)
            {
                doc.Root.Add(new XElement("Group", new XAttribute("GroupAttribute", item.Key),
                                                    item.ToList().Select(bl => new XElement("BookOfLibrary",
                                                    new XAttribute("Id", bl.Id),
                                                    new XElement("Surname", bl.Surname),
                                                    new XElement("Author", bl.Author),
                                                    new XElement("Label", bl.Label),
                                                    new XElement("Cost", bl.Cost),
                                                    new XElement("Year", bl.Year),
                                                    new XElement("Date", bl.Date),
                                                    new XElement("PeriodOfReturn", bl.PeriodOfReturn)))));
            }
            return doc;
        }
        static XDocument CreateDocument(List<BookInLibrary> list)
        {
            XDocument task1 = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("task1", list.Select(bl => new XElement("BookOfLibrary",
                                    new XAttribute("Id", bl.Id),
                                    new XElement("Surname", bl.Surname),
                                    new XElement("Author", bl.Author),
                                    new XElement("Label", bl.Label),
                                    new XElement("Cost", bl.Cost),
                                    new XElement("Year", bl.Year),
                                    new XElement("Date", bl.Date),
                                    new XElement("PeriodOfReturn", bl.PeriodOfReturn)))));
            return task1;
        }
        public static void SaveDoc(XDocument doc, string fileName)
        {
            doc.Save(Path.Combine(Environment.CurrentDirectory, fileName));
        }
        static void IndividualTasks(List<BookInLibrary> books)
        {
            Console.WriteLine("Input value from 1-5");
            int number = Convert.ToInt32(Console.ReadLine());
            switch (number)
            {
                case 1:
                    List<BookInLibrary> answer0 = books.OrderBy(el => el.Id).ToList();
                    foreach (BookInLibrary book in answer0) Console.WriteLine(book);
                    XDocument task1 = CreateDocument(answer0.ToList());
                    SaveDoc(task1, "task1.xml");
                    break;
                case 2:
                    List<BookInLibrary> answer1 = books.Where(el => el.PeriodOfReturn == 0).ToList();
                    foreach (BookInLibrary book in answer1) Console.WriteLine(book);
                    XDocument task2 = CreateDocument(answer1.ToList());
                    SaveDoc(task2, "task2.xml");
                    break;
                case 3:
                    var answer2 = books.GroupBy(el => el.Author).ToList();
                        int temp = answer2.Max(el => el.ToList().Count);
                        Console.WriteLine(answer2.Where(el => el.ToList().Count == temp).First().Key);
                    XDocument task3 = CreateDocument(answer2.Where(el => el.ToList().Count == temp).First().ToList());
                    SaveDoc(task3, "task3.xml");
                    break;
                case 4:
                    List<BookInLibrary> answer3 = books.OrderBy(el => el.Cost).ToList();
                    Console.WriteLine($"Label: {answer3[0].Label}, Cost: {answer3[0].Cost}");
                    XDocument task4 = CreateDocument(answer3);
                    SaveDoc(task4, "task4.xml");
                    break;
                case 5:
                    List<IGrouping<string, BookInLibrary>> answer5 = books.GroupBy(el => el.Author).ToList();
                    ForStringGrouping(answer5);
                    XDocument group = CreateDocGroup(answer5);
                    SaveDoc(group, "group.xml");

                    answer5 = books.GroupBy(el => el.Label).ToList();
                    ForStringGrouping(answer5);
                    XDocument group1 = CreateDocGroup(answer5);
                    SaveDoc(group1, "group1.xml");

                    answer5 = books.GroupBy(el => el.Surname).ToList();
                    ForStringGrouping(answer5);
                    XDocument group2 = CreateDocGroup(answer5);
                    SaveDoc(group2, "group2.xml");

                    List<IGrouping<double, BookInLibrary>> answer6 = books.GroupBy(el => el.Cost).ToList();
                    foreach (IGrouping<double, BookInLibrary> item in answer6)
                    {
                        Console.WriteLine(item.Key);
                        foreach (BookInLibrary book in item)
                            Console.WriteLine(book);
                    }
                    XDocument group3 = CreateDocGroup(answer6);
                    SaveDoc(group3, "group3.xml");

                    List<IGrouping<int, BookInLibrary>> answer7 = books.GroupBy(el => el.Id).ToList();
                    ForIntGrouping(answer7);
                    XDocument group4 = CreateDocGroup(answer7);
                    SaveDoc(group4, "group4.xml");

                    answer7 = books.GroupBy(el => el.Year).ToList();
                    ForIntGrouping(answer7);
                    XDocument group5 = CreateDocGroup(answer7);
                    SaveDoc(group5, "group5.xml");

                    answer7 = books.GroupBy(el => el.PeriodOfReturn).ToList();
                    ForIntGrouping(answer7);
                    XDocument group6 = CreateDocGroup(answer7);
                    SaveDoc(group6, "group6.xml");

                    List<IGrouping<DateTime, BookInLibrary>> answer8 = books.GroupBy(el => el.Date).ToList();
                    foreach (IGrouping<DateTime, BookInLibrary> item in answer8)
                    {
                        Console.WriteLine(item.Key);
                        foreach (BookInLibrary book in item)
                            Console.WriteLine(book);
                    }
                    XDocument group7 = CreateDocGroup(answer7);
                    SaveDoc(group7, "group7.xml");
                    break;
                default:
                    throw new Exception("Wrong number");
            }
        }
        static void Start(List<BookInLibrary> bookInLibraries)
        {
            int choice = Choice();
            var xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("BooksOfLibrary", bookInLibraries.Select(bl => new XElement("BookOfLibrary",
                                    new XAttribute("Id", bl.Id),
                                    new XElement("Surname", bl.Surname),
                                    new XElement("Author", bl.Author),
                                    new XElement("Label", bl.Label),
                                    new XElement("Cost", bl.Cost),
                                    new XElement("Year", bl.Year),
                                    new XElement("Date", bl.Date),
                                    new XElement("PeriodOfReturn", bl.PeriodOfReturn)))));
            switch (choice)
            {
                case 1:
                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml"));
                    break;
                case 2:
                    xDoc.Root.Add(AddBook());
                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml"));
                    break;
                case 3:
                    IndividualTasks(bookInLibraries);
                    break;
                case 4:
                    break;
                default:
                    throw new Exception("wrong value");
            }
            Console.ReadKey();
        }
        static void ForStringGrouping(List<IGrouping<string,BookInLibrary>> list)
        {
            foreach (IGrouping<string, BookInLibrary> item in list)
            {
                Console.WriteLine(item.Key);
                foreach (BookInLibrary book in item)
                    Console.WriteLine(book);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        static void ForIntGrouping(List<IGrouping<int, BookInLibrary>> list)
        {
            foreach (IGrouping<int, BookInLibrary> item in list)
            {
                Console.WriteLine(item.Key);
                foreach (BookInLibrary book in item)
                    Console.WriteLine(book);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            List<BookInLibrary> books = new List<BookInLibrary>();
            
            books.Add(new BookInLibrary(10000000, "Qert", "Feert", "Htrttt", 23.73, 2010, new DateTime(2001, 12, 4), 20));
            books.Add(new BookInLibrary(11000000, "Fdsd", "Feert", "Qtrttt", 21.43, 2010, new DateTime(2001, 11, 4), 23));
            books.Add(new BookInLibrary(12000000, "Qtyjk", "DFGH", "Atrttt", 23.43, 2010, new DateTime(2001, 12, 4), 0));
            books.Add(new BookInLibrary(13000000, "Qertghjk", "Feert", "Ktrttt", 23.43, 2015, new DateTime(2001, 12, 4), 23));
            books.Add(new BookInLibrary(14000000, "Ertt", "Feert", "Mtrttt", 21.73, 2015, new DateTime(2001, 11, 4), 20));
            books.Add(new BookInLibrary(15000000, "Kghgt", "DFGH", "Frttt", 23.73, 2010, new DateTime(2001, 12, 4), 23));
            books.Add(new BookInLibrary(16000000, "Ghjjt", "Toooy", "Atrttt", 23.43, 2015, new DateTime(2001, 12, 4), 0));
            books.Add(new BookInLibrary(17000000, "Liiyj", "Rtyk", "Hrttt", 21.73, 2010, new DateTime(2001, 11, 4), 23));
            books.Add(new BookInLibrary(18000000, "Hkyktk", "Hkkdk", "Rtrttt", 23.73, 2015, new DateTime(2001, 12, 4), 20));
            books.Add(new BookInLibrary(11100000, "Mkghkf", "Asdf", "Strttt", 21.43, 2015, new DateTime(2001, 11, 4), 23));

            Start(books);
        }
    }
}

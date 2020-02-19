using System;
using System.Collections.Generic;
using System.Linq;

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
            

            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    {
                        List<BookInLibrary> answer0 = books.OrderBy(el => el.Id).ToList();
                        foreach(BookInLibrary book in answer0)
                            Console.WriteLine(book);
                        break;
                    }
                case 2:
                    {
                        List<BookInLibrary> answer1 = books.Where(el => el.PeriodOfReturn == 0).ToList();
                        foreach(BookInLibrary book in answer1)
                            Console.WriteLine(book);
                        break;
                    }
                case 3:
                    {
                        var answer2 = books.GroupBy(el => el.Author).ToList();
                        int temp = answer2.Max(el => el.ToList().Count);
                        Console.WriteLine(answer2.Where(el => el.ToList().Count == temp).First().Key);
                        break;
                    }
                case 4:
                    {
                        BookInLibrary answer4 = books.OrderBy(el => el.Cost).First();
                        Console.WriteLine($"Label: {answer4.Label}, Cost: {answer4.Cost}");
                        break;
                    }
                case 5:
                    {
                        List<IGrouping<string,BookInLibrary>> answer5 = books.GroupBy(el => el.Author).ToList();
                        ForStringGrouping(answer5);
                        answer5 = books.GroupBy(el => el.Label).ToList();
                        ForStringGrouping(answer5);
                        answer5 = books.GroupBy(el => el.Surname).ToList();
                        ForStringGrouping(answer5);
                        List<IGrouping<double, BookInLibrary>> answer6 = books.GroupBy(el => el.Cost).ToList();
                        foreach(IGrouping<double,BookInLibrary> item in answer6)
                        {
                            Console.WriteLine(item.Key);
                            foreach(BookInLibrary book in item)
                                Console.WriteLine(book);
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                        List<IGrouping<int, BookInLibrary>> answer7 = books.GroupBy(el => el.Id).ToList();
                        ForIntGrouping(answer7);
                        answer7 = books.GroupBy(el => el.Year).ToList();
                        ForIntGrouping(answer7);
                        answer7 = books.GroupBy(el => el.PeriodOfReturn).ToList();
                        ForIntGrouping(answer7);
                        List<IGrouping<DateTime, BookInLibrary>> answer8 = books.GroupBy(el => el.Date).ToList();
                        foreach (IGrouping<DateTime, BookInLibrary> item in answer8)
                        {
                            Console.WriteLine(item.Key);
                            foreach (BookInLibrary book in item)
                                Console.WriteLine(book);
                        }
                        break;
                    }
            }

        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace _20_lab_1_variant
{
    class Program 
    {
        class Movie : IEnumerable<Movie>
        {
            private string label;
            private int year;
            private List<string> genres;
            private string producer;
            private int tallage;

            public string Label 
            { 
                get => label;
                set
                {
                    if (char.IsUpper(value[0]))
                    {
                        label = value;
                    }
                    else
                    {
                        throw new Exception("Label must be with Uppercase");
                    }
                }
            }
            public int Year 
            { 
                get => year;
                set
                {
                    if(value < 1920 || value > 2019)
                    {
                        throw new Exception("Invalid value of year");
                    }
                    year = value;
                }
            }
            public List<string> Genres { get => genres; set => genres = value; }
            public string Producer { get => producer; set => producer = value; }
            public int Tallage { get => tallage; set => tallage = value < 0 ? 0 : value; }

            public override string ToString()
            {
                return $"label: {Label}, year: {Year}, producer: {Producer}";
            }

            public IEnumerator<Movie> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
            public Movie() { }
            public Movie(string label, int year, List<string> genres, string producer, int tallage)
            {
                Label = label;
                Year = year;
                Genres = genres;
                Producer = producer;
                Tallage = tallage;
            }
        }

        static void FillXmlList(List<Movie> movies, XmlDocument xDoc, XmlElement xRoot)
        {
            foreach (Movie movie in movies)
            {
                FillXmlEl(movie, xDoc, xRoot);
            }
        }

        static void FillXmlEl(Movie movie, XmlDocument xDoc, XmlElement xRoot)
        {
            XmlElement movieElem = xDoc.CreateElement("movie");
            XmlAttribute labelAttr = xDoc.CreateAttribute("label");
            XmlElement yearElem = xDoc.CreateElement("year");
            XmlElement genresElem = xDoc.CreateElement("genres");
            XmlElement genre1Elem = xDoc.CreateElement("genre");
            XmlElement genre2Elem = xDoc.CreateElement("genre");
            XmlElement genre3Elem = xDoc.CreateElement("genre");
            XmlElement producerElem = xDoc.CreateElement("producer");
            XmlElement tallageElem = xDoc.CreateElement("tallage");

            XmlText labelText = xDoc.CreateTextNode(movie.Label);
            XmlText yearText = xDoc.CreateTextNode(movie.Year.ToString());
            XmlText genre1Text = xDoc.CreateTextNode(movie.Genres[0]);
            XmlText genre2Text = xDoc.CreateTextNode(movie.Genres[1]);
            XmlText genre3Text = xDoc.CreateTextNode(movie.Genres[2]);
            XmlText producerText = xDoc.CreateTextNode(movie.Producer);
            XmlText tallageText = xDoc.CreateTextNode(movie.Tallage.ToString());

            labelAttr.AppendChild(labelText);
            yearElem.AppendChild(yearText);
            producerElem.AppendChild(producerText);
            movieElem.Attributes.Append(labelAttr);
            tallageElem.AppendChild(tallageText);
            genre1Elem.AppendChild(genre1Text);
            genre2Elem.AppendChild(genre2Text);
            genre3Elem.AppendChild(genre3Text);

            genresElem.AppendChild(genre1Elem);
            genresElem.AppendChild(genre2Elem);
            genresElem.AppendChild(genre3Elem);
            movieElem.AppendChild(yearElem);
            movieElem.AppendChild(producerElem);
            movieElem.AppendChild(genresElem);
            movieElem.AppendChild(tallageElem);
            xRoot.AppendChild(movieElem);
        }

        static void Main(string[] args)
        {
            List<Movie> movies = new List<Movie>();

            List<string> genres = new List<string>();
            genres.Add("horror");
            genres.Add("camedy");
            genres.Add("drama");

            movies.Add(new Movie("Karate", 2013, genres, "Cameron", 12333));
            movies.Add(new Movie("Pupusi", 2019, genres, "Camsaw", 16733));
            movies.Add(new Movie("Rersaw", 2009, genres, "Admeron", 17833));
            movies.Add(new Movie("Kagter", 2015, genres, "Cameron", 121234));
            movies.Add(new Movie("Dfghtr", 2010, genres, "Cljjgfd", 121345));
            movies.Add(new Movie("Kwerty", 2002, genres, "iyukfg", 123245));
            movies.Add(new Movie("Ertyvc", 2004, genres, "yjmeron", 154543));
            movies.Add(new Movie("Cvbjjh", 2000, genres, "hjkeron", 134563));
            movies.Add(new Movie("Jyujbn", 1945, genres, "Ryuron", 12345));
            movies.Add(new Movie("Ertyub", 1921, genres, "Came", 1656764));

            XmlDocument xDoc = new XmlDocument();

            xDoc.Load("data.xml");

            XmlElement xRoot = xDoc.DocumentElement;
            int l = 1;
            while (l < 5)
            {

                xRoot.RemoveAll();

                FillXmlList(movies,xDoc,xRoot);
           
                xDoc.Save("data.xml");
                Console.WriteLine("Все четко");


                int c = Convert.ToInt32(Console.ReadLine());

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (c)
                {
                    case 1:
                        {
                            // обход всех узлов в корневом элементе
                            foreach (XmlNode xnode in xRoot)
                            {
                                // получаем атрибут name
                                if (xnode.Attributes.Count > 0)
                                {
                                    XmlNode attr = xnode.Attributes.GetNamedItem("label");
                                    if (attr != null)
                                        Console.WriteLine(attr.Value);
                                }
                                // обходим все дочерние узлы элемента user
                                foreach (XmlNode childnode in xnode.ChildNodes)
                                {
                                    // если узел - company
                                    if (childnode.Name == "year")
                                    {
                                        Console.WriteLine($"Год: {childnode.InnerText}");
                                    }
                                    // если узел age
                                    if (childnode.Name == "producer")
                                    {
                                        Console.WriteLine($"Продюсер: {childnode.InnerText}");
                                    }
                                    if (childnode.Name == "genres")
                                    {
                                        Console.WriteLine("Жанры:");
                                        foreach (XmlNode str in childnode.ChildNodes)
                                        {
                                            Console.WriteLine($"{str.InnerText}");
                                        }
                                    }
                                    if (childnode.Name == "tallage")
                                    {
                                        Console.WriteLine($"Бюджет: {childnode.InnerText}$");
                                    }
                                }
                                Console.WriteLine();
                            }
                            Console.WriteLine(xRoot);
                            break;
                        }
                    case 2:
                        {
                                Movie movie = new Movie();
                                Console.WriteLine("введите название фильма:");
                                movie.Label = Console.ReadLine();
                                Console.WriteLine("введите год фильма");
                                movie.Year = Convert.ToInt32(Console.ReadLine());
                                movie.Genres = genres;
                                Console.WriteLine("введите имя продюсера:");
                                movie.Producer = Console.ReadLine();
                                movie.Tallage = Convert.ToInt32(Console.ReadLine());
                                movies.Add(movie);
                            
                            break;
                        }
                    case 3:
                        {

                            switch (choice)
                            {
                                case 1:
                                    List<Movie> newList0 = movies.OrderBy(el => el.Label).ThenBy(el => el.Producer).ToList();
                                    XmlDocument xD1 = new XmlDocument();
                                    xD1.Load("case1.xml");
                                    XmlElement xR1 = xD1.DocumentElement;
                                    xR1.RemoveAll();
                                    FillXmlList(newList0, xD1, xR1);
                                    xD1.Save("case1.xml");
                                    break;
                                case 2:
                                    List<Movie> newList1 = movies.Where(el => el.Producer == "Cameron").ToList();
                                    XmlDocument xD2 = new XmlDocument();
                                    xD2.Load("case2.xml");
                                    XmlElement xR2 = xD2.DocumentElement;
                                    xR2.RemoveAll();
                                    FillXmlList(newList1, xD2, xR2);
                                    xD2.Save("case2.xml");
                                    break;
                                case 3:
                                    Movie newList2 = movies.Where(el => el.Label.Split(" ").Length == 1).OrderBy(el => el.Year).First();
                                    XmlDocument xD3 = new XmlDocument();
                                    xD3.Load("case3.xml");
                                    XmlElement xR3 = xD3.DocumentElement;
                                    xR3.RemoveAll();
                                    FillXmlEl(newList2, xD3, xR3);
                                    xD3.Save("case3.xml");
                                    break;
                                case 4:
                                    double newList3 = movies.Where(el => el.Year > 2010).Average(el => el.Tallage);
                                    XmlDocument xD4 = new XmlDocument();
                                    xD4.Load("case4.xml");
                                    XmlElement xR4 = xD4.DocumentElement;
                                    xR4.RemoveAll();
                                    XmlElement answer = xD4.CreateElement("answer");
                                    XmlAttribute valueAttr = xD4.CreateAttribute("value");
                                    XmlText valueText = xD4.CreateTextNode(newList3.ToString());
                                    valueAttr.AppendChild(valueText);
                                    answer.Attributes.Append(valueAttr);
                                    xR4.AppendChild(answer);
                                    xD4.Save("case4.xml");
                                    break;
                                case 5:
                                    var newList4 = movies.GroupBy(el => el.Label);
                                    foreach (IGrouping<string, Movie> item in newList4)
                                    {
                                        foreach (var t in item)
                                            Console.WriteLine(t.Label);
                                        Console.WriteLine();
                                    }
                                    var newList5 = movies.GroupBy(el => el.Year);
                                    foreach (IGrouping<string, Movie> item in newList4)
                                    {
                                        foreach (var t in item)
                                            Console.WriteLine(t.Year);
                                        Console.WriteLine();
                                    }
                                    var newList6 = movies.GroupBy(el => el.Producer);
                                    foreach (IGrouping<string, Movie> item in newList4)
                                    {
                                        foreach (var t in item)
                                            Console.WriteLine(t.Producer);
                                        Console.WriteLine();
                                    }
                                    var newList7 = movies.GroupBy(el => el.Tallage);
                                    foreach (IGrouping<string, Movie> item in newList4)
                                    {
                                        foreach (var t in item)
                                            Console.WriteLine(t.Tallage);
                                        Console.WriteLine();
                                    }
                                    break;
                                default:
                                    Console.WriteLine("Invalid value");
                                    break;
                            }
                            break;
                        }
                    case 4:
                        {
                            l = 5;
                            break;
                        }
                }
            }
        }
    }
}

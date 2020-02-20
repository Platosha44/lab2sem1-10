using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace _20_lab_3_variant
{
    class Program
    {
        public class Actor
        {
            private string name;
            private string sex;
            private DateTime birthday;
            private int countOfOskars;
            private int countOfFilms;
            private double markOnKinoPoisk;
            private List<string> genres;

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
            public string Sex
            {
                get => sex;
                set
                {
                    if(value != "M" && value != "Ж")
                    {
                        throw new Exception("Invalid value of sex");
                    }
                    sex = value;
                }
            }
            public DateTime Birthday { get => birthday; set => birthday = value; }
            public int CountOfFilms
            {
                get => countOfFilms;
                set
                {
                    if(value < 0)
                    {
                        throw new Exception("Invalid value of count of films");
                    }
                    countOfFilms = value;
                }
            }
            public int CountOfOskars
            {
                get => countOfOskars;
                set
                {
                    if (value < 0)
                    {
                        throw new Exception("Invalid value of count of oskars");
                    }
                    countOfOskars = value;
                }
            }
            public double MarkOnKinoPoisk
            {
                get => markOnKinoPoisk;
                set
                {
                    if(value < 0 && value > 10)
                    {
                        throw new Exception("Invalid value of mark on kinoPoisk");
                    }
                    markOnKinoPoisk = value;
                }
            }
            public List<string> Genres
            {
                get => genres;
                set
                {
                    foreach(string item in value)
                    {
                        if (!char.IsUpper(item[0]))
                        {
                            throw new Exception("Invalid value of name");
                        }
                    }
                    genres = value;
                }
            }

            public Actor(string name, string sex, DateTime bday, int oskars, int films, double mark, List<string> genres)
            {
                Name = name;
                Sex = sex;
                Birthday = bday;
                CountOfFilms = films;
                CountOfOskars = oskars;
                MarkOnKinoPoisk = mark;
                Genres = genres;
            }

            public string GenresToString()
            {
                string temp = "";
                foreach (string item in Genres)
                {
                    temp += item;
                    temp += " ";
                }
                return temp;
            }

            public override string ToString()
            {
                return $"Name: {Name}, Sex: {Sex}, Birthday: {Birthday}, films: {CountOfFilms}, oskars: {CountOfOskars}, mark: {MarkOnKinoPoisk}, genres: {GenresToString()}";
            }
        }
        public static XElement AddActor()
        {
            Console.WriteLine("Input values \n " +
                "name \n" +
                "sex \n" +
                "birthday \n" +
                "countOfOskars \n" +
                "countOfFilms \n" +
                "markOnKinopoisk \n" +
                "genres \n");
            string name = Console.ReadLine();
            string sex = Console.ReadLine();
            DateTime birthday = Convert.ToDateTime(Console.ReadLine());
            int countOfOskars = Convert.ToInt32(Console.ReadLine());
            int countOfFilms = Convert.ToInt32(Console.ReadLine());
            double markOnKinopoisk = Convert.ToDouble(Console.ReadLine());
            List<string> genres = new List<string>(3);
            for (int i = 0; i != genres.Count; ++i)
                genres[i] = Console.ReadLine();
            Actor ac = new Actor(name, sex, birthday, countOfOskars, countOfFilms, markOnKinopoisk, genres);
            return new XElement("Actor", new XAttribute("Name", ac.Name),
                    new XElement("Birthday", ac.Birthday),
                    new XElement("CountOfOskars", ac.CountOfOskars),
                    new XElement("CountOfFilms", ac.CountOfFilms),
                    new XElement("MarkOnKinopoisk", ac.MarkOnKinoPoisk),
                    new XElement("Genres", ac.GenresToString()));
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
        public static XDocument CreateDocGroup<T>(IEnumerable<IGrouping<T, Actor>> group)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Grouping"));
            foreach (var item in group)
            {
                doc.Root.Add(new XElement("Group", new XAttribute("GroupAttribute", item.Key),
                                                    item.ToList().Select(ac => new XElement("Actor", new XAttribute("Name", ac.Name),
                    new XElement("Birthday", ac.Birthday),
                    new XElement("CountOfOskars", ac.CountOfOskars),
                    new XElement("CountOfFilms", ac.CountOfFilms),
                    new XElement("MarkOnKinopoisk", ac.MarkOnKinoPoisk),
                    new XElement("Genres", ac.GenresToString())))));
            }
            return doc;
        }
        public static XDocument CreateDocument(List<Actor> list)
        {
            XDocument task1 = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("task1", list.Select(ac => new XElement("Actor", new XAttribute("Name", ac.Name),
                    new XElement("Birthday", ac.Birthday),
                    new XElement("CountOfOskars", ac.CountOfOskars),
                    new XElement("CountOfFilms", ac.CountOfFilms),
                    new XElement("MarkOnKinopoisk", ac.MarkOnKinoPoisk),
                    new XElement("Genres", ac.GenresToString())))));
            return task1;
        }
        public static void SaveDoc(XDocument doc, string fileName)
        {
            doc.Save(Path.Combine(Environment.CurrentDirectory, fileName));
        }
        public static void IndividualTasks(List<Actor> actors)
        {
            Console.WriteLine("Input value from 1-5");
            int number = Convert.ToInt32(Console.ReadLine());
            switch (number)
            {
                case 1:
                    var answer1 = actors.OrderBy(el => el.Name).ThenBy(el => el.Genres).ToList();
                    foreach (Actor actor in answer1)
                    {
                        Console.WriteLine(actor);
                    }
                    XDocument task1 = CreateDocument(answer1);
                    SaveDoc(task1, "task1.xml");
                    break;
                case 2:
                    var answer2 = actors.Where(el => el.Birthday == new DateTime(2001, 11, 21)).ToList();
                    foreach (Actor actor in answer2)
                    {
                        Console.WriteLine(actor.Name);
                    }
                    XDocument task2 = CreateDocument(answer2);
                    SaveDoc(task2, "task2.xml");
                    break;
                case 3:
                    var answer3 = actors.OrderBy(el => el.CountOfFilms & el.CountOfOskars).ToList();
                    Console.WriteLine("the most popular actor: " + answer3[answer3.Count - 1]);
                    XDocument task3 = CreateDocument(answer3);
                    SaveDoc(task3, "task3.xml");
                    break;
                case 4:
                    var answer4 = actors.Where(el => el.Sex == "M").OrderBy(el => el.MarkOnKinoPoisk).ToList();
                    Console.WriteLine(answer4[answer4.Count - 1].MarkOnKinoPoisk);
                    XDocument task4 = CreateDocument(answer4);
                    SaveDoc(task4, "task4.xml");
                    break;
                case 5:
                    var groupName = actors.GroupBy(el => el.Name);
                    foreach (IGrouping<string, Actor> actor in groupName)
                    {
                        Console.WriteLine(actor.Key);
                        foreach (var a in actor)
                        {
                            Console.WriteLine(a);
                        }
                        Console.WriteLine();
                    }
                    XDocument group = CreateDocGroup(groupName);
                    SaveDoc(group, "group.xml");

                    var groupBirthday = actors.GroupBy(el => el.Birthday);
                    foreach (IGrouping<DateTime, Actor> actor in groupBirthday)
                    {
                        Console.WriteLine(actor.Key);
                        foreach (var a in actor)
                        {
                            Console.WriteLine(a);
                        }
                        Console.WriteLine();
                    }
                    XDocument group1 = CreateDocGroup(groupBirthday);
                    SaveDoc(group1, "group1.xml");

                    var groupCountOfFilms = actors.GroupBy(el => el.CountOfFilms);
                    foreach (IGrouping<int, Actor> actor in groupCountOfFilms)
                    {
                        Console.WriteLine(actor.Key);
                        foreach (var a in actor)
                        {
                            Console.WriteLine(a);
                        }
                        Console.WriteLine();
                    }
                    XDocument group2 = CreateDocGroup(groupCountOfFilms);
                    SaveDoc(group2, "group2.xml");

                    var groupCountOfOskars = actors.GroupBy(el => el.CountOfOskars);
                    foreach (IGrouping<int, Actor> actor in groupCountOfOskars)
                    {
                        Console.WriteLine(actor.Key);
                        foreach (var a in actor)
                        {
                            Console.WriteLine(a);
                        }
                        Console.WriteLine();
                    }
                    XDocument group3 = CreateDocGroup(groupCountOfOskars);
                    SaveDoc(group3, "group3.xml");

                    var groupSex = actors.GroupBy(el => el.Sex);
                    foreach (IGrouping<string, Actor> actor in groupSex)
                    {
                        Console.WriteLine(actor.Key);
                        foreach (var a in actor)
                        {
                            Console.WriteLine(a);
                        }
                        Console.WriteLine();
                    }
                    XDocument group4 = CreateDocGroup(groupSex);
                    SaveDoc(group4, "group4.xml");

                    var groupMarkOnKinoPoisk = actors.GroupBy(el => el.MarkOnKinoPoisk);
                    foreach (IGrouping<double, Actor> actor in groupMarkOnKinoPoisk)
                    {
                        Console.WriteLine(actor.Key);
                        foreach (var a in actor)
                        {
                            Console.WriteLine(a);
                        }
                        Console.WriteLine();
                    }
                    XDocument group5 = CreateDocGroup(groupMarkOnKinoPoisk);
                    SaveDoc(group5, "group5.xml");

                    var groupGenres = actors.GroupBy(el => el.Genres);
                    foreach (IGrouping<List<string>, Actor> actor in groupGenres)
                    {
                        foreach (string item in actor.Key)
                        {
                            Console.Write(item + " ");
                        }
                        Console.WriteLine();
                        foreach (var a in actor)
                        {
                            Console.WriteLine(a);
                        }
                        Console.WriteLine();
                    }
                    XDocument group6 = CreateDocGroup(groupGenres);
                    SaveDoc(group6, "group6.xml");
                    break;
                default:
                    throw new Exception("Wrong number");
            }
        }
        public static void Start(List<Actor> students)
        {
            int choice = Choice();
            var xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Actors", students.Select(ac => new XElement("Actor", new XAttribute("Name", ac.Name),
                    new XElement("Birthday", ac.Birthday),
                    new XElement("CountOfOskars", ac.CountOfOskars),
                    new XElement("CountOfFilms", ac.CountOfFilms),
                    new XElement("MarkOnKinopoisk", ac.MarkOnKinoPoisk),
                    new XElement("Genres", ac.GenresToString())))));
            switch (choice)
            {
                case 1:
                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml"));
                    break;
                case 2:
                    xDoc.Root.Add(AddActor());
                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml"));
                    break;
                case 3:
                    IndividualTasks(students);
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
            List<string> genres = new List<string>();
            genres.Add("Horror");
            genres.Add("Triller");
            genres.Add("Dramma");

            List<Actor> actors = new List<Actor>();
            actors.Add(new Actor("Den", "M", new DateTime(2001, 11, 21), 1, 24, 8.5, genres));
            actors.Add(new Actor("Ren", "M", new DateTime(2001, 11, 11), 1, 34, 8.5, genres));
            actors.Add(new Actor("Wen", "Ж", new DateTime(2002, 10, 21), 2, 14, 6.5, genres));
            actors.Add(new Actor("Qen", "M", new DateTime(2001, 11, 21), 1, 24, 6.5, genres));
            actors.Add(new Actor("Een", "Ж", new DateTime(2003, 10, 21), 0, 34, 8.5, genres));
            actors.Add(new Actor("Sen", "M", new DateTime(2001, 11, 11), 1, 24, 8.5, genres));
            actors.Add(new Actor("Hen", "Ж", new DateTime(2001, 10, 11), 0, 29, 8.5, genres));
            actors.Add(new Actor("Ken", "M", new DateTime(2003, 11, 21), 1, 34, 7.5, genres));
            actors.Add(new Actor("Jen", "Ж", new DateTime(2003, 10, 11), 8, 29, 7.5, genres));
            actors.Add(new Actor("Pen", "M", new DateTime(2002, 11, 21), 1, 24, 8.5, genres));

            Start(actors);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace _20_lab_3_variant
{
    class Program
    {
        class Actor
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

            int choice = Convert.ToInt32(Console.ReadLine());
            string name = "Den";

            switch (choice)
            {
                case 1:
                    var answer0 = actors.Where(el => el.Name == name && el.Genres.Any(el => el == "Horror"));
                    foreach(Actor actor in answer0)
                    {
                        Console.WriteLine(actor);
                    }
                    break;
                case 2:
                    var answer1 = actors.Where(el => el.Birthday == new DateTime(2001, 11, 21));
                    foreach(Actor actor in answer1)
                    {
                        Console.WriteLine(actor.Name);
                    }
                    break;
                case 3:
                    var answer2 = actors.OrderBy(el => el.CountOfFilms & el.CountOfOskars).Last();
                    Console.WriteLine("the most popular actor: " + answer2);
                    break;
                case 4:
                    var answer3 = actors.Where(el => el.Sex == "M").OrderBy(el => el.MarkOnKinoPoisk).Last();
                    Console.WriteLine(answer3.MarkOnKinoPoisk);
                    break;
                case 5:
                    var answer4 = actors.GroupBy(el => el.Name);
                    foreach(IGrouping<string,Actor> actor in answer4)
                    {
                        Console.WriteLine(actor.Key);
                        foreach(var a in actor)
                        {
                            Console.WriteLine(a);
                        }
                        Console.WriteLine();
                    }
                    
                    var answer5 = actors.GroupBy(el => el.Birthday);
                    foreach (IGrouping<DateTime, Actor> actor in answer5)
                    {
                        Console.WriteLine(actor.Key);
                        foreach (var a in actor)
                        {
                            Console.WriteLine(a);
                        }
                        Console.WriteLine();
                    }
                    
                    var answer6 = actors.GroupBy(el => el.CountOfFilms);
                    foreach (IGrouping<int, Actor> actor in answer6)
                    {
                        Console.WriteLine(actor.Key);
                        foreach (var a in actor)
                        {
                            Console.WriteLine(a);
                        }
                        Console.WriteLine();
                    }
                    
                    var answer7 = actors.GroupBy(el => el.CountOfOskars);
                    foreach (IGrouping<int, Actor> actor in answer7)
                    {
                        Console.WriteLine(actor.Key);
                        foreach (var a in actor)
                        {
                            Console.WriteLine(a);
                        }
                        Console.WriteLine();
                    }
                    
                    var answer8 = actors.GroupBy(el => el.Sex);
                    foreach (IGrouping<string, Actor> actor in answer8)
                    {
                        Console.WriteLine(actor.Key);
                        foreach (var a in actor)
                        {
                            Console.WriteLine(a);
                        }
                        Console.WriteLine();
                    }

                    var answer9 = actors.GroupBy(el => el.MarkOnKinoPoisk);
                    foreach (IGrouping<double, Actor> actor in answer9)
                    {
                        Console.WriteLine(actor.Key);
                        foreach (var a in actor)
                        {
                            Console.WriteLine(a);
                        }
                        Console.WriteLine();
                    }

                    var answer10 = actors.GroupBy(el => el.Genres);
                    foreach (IGrouping<List<string>, Actor> actor in answer10)
                    {
                        foreach(string item in actor.Key)
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
                    break;
            }
        }
    }
}

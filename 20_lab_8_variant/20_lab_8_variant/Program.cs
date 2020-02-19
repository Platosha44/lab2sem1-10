using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace _20_lab_8_variant
{
    class Program
    {
        static void ForStringGrouping(List<IGrouping<string,File>> list)
        {
            foreach(IGrouping<string,File> item in list)
            {
                Console.WriteLine(item.Key);
                foreach(File file in item)
                    Console.WriteLine(file);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
        public enum Attributes
        {
            OnlyRead,Hide,System
        }
        class File
        {
            private string catalog;
            private string name;
            private int size;
            private string extend;
            private DateTime date;
            private Attributes attribute;

            public string Catalog
            {
                get => catalog;
                set
                {
                    if (!Char.IsUpper(value[0]))
                        throw new Exception("Invalid value of catalog");
                    catalog = value;
                }
            }
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
            public int Size
            {
                get => size;
                set
                {
                    if (value < 0)
                        throw new Exception("Invalid value of size");
                    size = value;
                }
            }
            public string Extend
            {
                get => extend;
                set
                {
                    Regex check = new Regex(@"^[a-b]{3}$");
                    if (check.IsMatch(value))
                        throw new Exception("Invalid value of extend");
                    extend = value;
                }
            }
            public DateTime Date { get => date; set => date = value; }
            public Attributes Attribute { get => attribute; set => attribute = value; }

            public File(string catalog, string name, string extand, DateTime date, Attributes attribute, int size)
            {
                Catalog = catalog;
                Name = name;
                Extend = extand;
                Date = date;
                Attribute = attribute;
                Size = size;
            }

            public override string ToString()
            {
                return $"Catalog: {Catalog}, Name: {Name}, Extand: {Extend}, Date of creation: {Date}, Attribute: {Attribute}, Size: {Size}";
            }
        }
        static void Main(string[] args)
        {
            List<File> files = new List<File>();
            files.Add(new File("Qert", "Trtkkd", "txt", new DateTime(2017, 11, 25), Attributes.Hide, 455));
            files.Add(new File("Wrtyh", "Sertyj", "bin", new DateTime(2016, 11, 25), Attributes.OnlyRead, 655));
            files.Add(new File("Qert", "Trtkkd", "xml", new DateTime(2017, 11, 25), Attributes.System, 705));
            files.Add(new File("Wrtyh", "Sertyj", "txt", new DateTime(2016, 11, 25), Attributes.Hide, 705));
            files.Add(new File("Qert", "Sertyj", "txt", new DateTime(2017, 11, 25), Attributes.OnlyRead, 655));
            files.Add(new File("Wrtyh", "Trtkkd", "xml", new DateTime(2016, 11, 25), Attributes.System, 455));
            files.Add(new File("Qert", "Sertyj", "bin", new DateTime(2017, 11, 25), Attributes.Hide, 455));
            files.Add(new File("Wrtyh", "Trtkkd", "txt", new DateTime(2018, 11, 25), Attributes.OnlyRead, 655));
            files.Add(new File("Qert", "Sertyj", "bin", new DateTime(2017, 11, 25), Attributes.System, 705));
            files.Add(new File("Wrtyh", "Sertyj", "xml", new DateTime(2018, 11, 25), Attributes.Hide, 455));


            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                        foreach (File file in files.OrderBy(el => el.Name).ThenBy(el => el.Attribute).ToList())
                            Console.WriteLine(file);
                        break;
                case 2:
                        foreach (IGrouping<string, File> item in files.GroupBy(el => el.Name).ToList().Where(el => el.ToList().Count > 1).ToList())
                            Console.WriteLine(item.Key);
                        break;
                case 3:
                        foreach (IGrouping<string, File> item in files.GroupBy(el => el.Extend).ToList().Where(el => el.ToList().Count == files
                                                                      .GroupBy(el => el.Extend).ToList().Min(el => el.ToList().Count)).ToList())
                            Console.WriteLine(item.Key);
                        break;
                case 4:
                        Console.WriteLine(files.OrderByDescending(el => el.Size).First());
                        break;
                case 5:
                        ForStringGrouping(files.GroupBy(el => el.Catalog).ToList());
                        ForStringGrouping(files.GroupBy(el => el.Name).ToList());
                        ForStringGrouping(files.GroupBy(el => el.Extend).ToList());
                        foreach (IGrouping<int,File> item in files.GroupBy(el => el.Size).ToList())
                        {
                            Console.WriteLine(item.Key);
                            foreach(File file in item)
                                Console.WriteLine(file);
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                    foreach (IGrouping<DateTime, File> item in files.GroupBy(el => el.Date).ToList())
                    {
                        Console.WriteLine(item.Key);
                        foreach (File file in item)
                            Console.WriteLine(file);
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    foreach (IGrouping<Attributes, File> item in files.GroupBy(el => el.Attribute).ToList())
                    {
                        Console.WriteLine(item.Key);
                        foreach (File file in item)
                            Console.WriteLine(file);
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    break;
            }
        }
    }
}

using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;

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
        public static XElement AddFile()
        {
            Console.WriteLine("Input values \n " +
                "catalog \n" +
                "name \n" +
                "extand \n" +
                "date \n" +
                "attribute \n" +
                "size \n");
            string catalog = Console.ReadLine();
            string name = Console.ReadLine();
            string extand = Console.ReadLine();
            DateTime date = Convert.ToDateTime(Console.ReadLine());
            Attributes attribute = Attributes.OnlyRead;
            int size = Convert.ToInt32(Console.ReadLine());
            File fl = new File(catalog, name, extand, date, attribute, size);
            return  new XElement("File", new XAttribute("Catalog", fl.Catalog),
                    new XElement("Name", fl.Name),
                    new XElement("Extand", fl.Extend),
                    new XElement("Date", fl.Date),
                    new XElement("Attribute", fl.Attribute),
                    new XElement("Size", fl.Size));
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
        static XDocument CreateDocGroup<T>(IEnumerable<IGrouping<T, File>> group)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Grouping"));
            foreach (var item in group)
            {
                doc.Root.Add(new XElement("Group", new XAttribute("GroupAttribute", item.Key),
                                                    item.ToList().Select(fl => new XElement("File", new XAttribute("Catalog", fl.Catalog),
                                                                               new XElement("Name", fl.Name),
                                                                               new XElement("Extand", fl.Extend),
                                                                               new XElement("Date", fl.Date),
                                                                               new XElement("Attribute", fl.Attribute),
                                                                               new XElement("Size", fl.Size)))));
            }
            return doc;
        }
        static XDocument CreateDocument(List<File> list)
        {
            XDocument task1 = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("task1", list.Select(fl => new XElement("File", new XAttribute("Catalog", fl.Catalog),
                                                                               new XElement("Name", fl.Name),
                                                                               new XElement("Extand", fl.Extend),
                                                                               new XElement("Date", fl.Date),
                                                                               new XElement("Attribute", fl.Attribute),
                                                                               new XElement("Size", fl.Size)))));
            return task1;
        }
        public static void SaveDoc(XDocument doc, string fileName)
        {
            doc.Save(Path.Combine(Environment.CurrentDirectory, fileName));
        }
        static void IndividualTasks(List<File> files)
        {
            Console.WriteLine("Input value from 1-5");
            int number = Convert.ToInt32(Console.ReadLine());
            switch (number)
            {
                case 1:
                    var answer1 = files.OrderBy(el => el.Name).ThenBy(el => el.Attribute).ToList();
                    foreach (File file in answer1)
                        Console.WriteLine(file);
                    XDocument task1 = CreateDocument(answer1.ToList());
                    SaveDoc(task1, "task1.xml");
                    break;
                case 2:
                    var answer2 = files.GroupBy(el => el.Name).ToList().Where(el => el.ToList().Count > 1).ToList();
                    foreach (IGrouping<string, File> item in answer2)
                        Console.WriteLine(item.Key);
                    XDocument task2 = CreateDocument(answer2[0].ToList());
                    SaveDoc(task2, "task2.xml");
                    break;
                case 3:
                    var answer3 = files.GroupBy(el => el.Extend).ToList().Where(el => el.ToList().Count == files
                                        .GroupBy(el => el.Extend).ToList().Min(el => el.ToList().Count)).ToList();
                    foreach (IGrouping<string, File> item in answer3) Console.WriteLine(item.Key);
                    XDocument task3 = CreateDocument(answer3[0].ToList());
                    SaveDoc(task3, "task3.xml");
                    break;
                case 4:
                    var answer4 = files.OrderByDescending(el => el.Size).ToList();
                    Console.WriteLine(answer4[0]);
                    XDocument task4 = CreateDocument(answer4);
                    SaveDoc(task4, "task4.xml");
                    break;
                case 5:
                    var groupCatalog = files.GroupBy(el => el.Catalog).ToList();
                    ForStringGrouping(groupCatalog);
                    XDocument group = CreateDocGroup(groupCatalog);
                    SaveDoc(group, "group.xml");

                    var groupName = files.GroupBy(el => el.Name).ToList();
                    ForStringGrouping(groupName);
                    XDocument group1 = CreateDocGroup(groupName);
                    SaveDoc(group1, "group1.xml");

                    var groupExtand = files.GroupBy(el => el.Extend).ToList();
                    ForStringGrouping(groupExtand);
                    XDocument group2 = CreateDocGroup(groupExtand);
                    SaveDoc(group2, "group2.xml");

                    var groupSize = files.GroupBy(el => el.Size).ToList();
                    foreach (IGrouping<int, File> item in groupSize)
                    {
                        Console.WriteLine(item.Key);
                        foreach (File file in item)
                            Console.WriteLine(file);
                    }
                    XDocument group3 = CreateDocGroup(groupSize);
                    SaveDoc(group3, "group3.xml");

                    var groupDate = files.GroupBy(el => el.Date).ToList();
                    foreach (IGrouping<DateTime, File> item in groupDate)
                    {
                        Console.WriteLine(item.Key);
                        foreach (File file in item)
                            Console.WriteLine(file);
                    }
                    XDocument group4 = CreateDocGroup(groupDate);
                    SaveDoc(group4, "group4.xml");

                    var groupAttribute = files.GroupBy(el => el.Attribute).ToList();
                    foreach (IGrouping<Attributes, File> item in groupAttribute)
                    {
                        Console.WriteLine(item.Key);
                        foreach (File file in item)
                            Console.WriteLine(file);
                    }
                    XDocument group5 = CreateDocGroup(groupAttribute);
                    SaveDoc(group5, "group5.xml");
                    break;
                default:
                    throw new Exception("Wrong number");
            }
        }
        static void Start(List<File> files)
        {
            int choice = Choice();
            var xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Files", files.Select(fl => new XElement("File", new XAttribute("Catalog", fl.Catalog),
                                                                               new XElement("Name", fl.Name),
                                                                               new XElement("Extand", fl.Extend),
                                                                               new XElement("Date", fl.Date),
                                                                               new XElement("Attribute", fl.Attribute),
                                                                               new XElement("Size", fl.Size)))));
            switch (choice)
            {
                case 1:
                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml"));
                    break;
                case 2:
                    xDoc.Root.Add(AddFile());
                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml"));
                    break;
                case 3:
                    IndividualTasks(files);
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

            Start(files);
        }
    }
}

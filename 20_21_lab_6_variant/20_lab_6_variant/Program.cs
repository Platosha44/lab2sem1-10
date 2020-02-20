using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace _20_lab_6_variant
{
    class Program
    {
        public enum ViewOfMainTest
        {
            Exam, Test
        }
        class Subject
        {
            private string label;
            private string surname;
            private int countOfStudents;
            private int hoursOfLectures;
            private int hoursOfPractica;
            private bool curs;
            private ViewOfMainTest cursProject;

            public bool Curs { get => curs; set => curs = value; }
            public string Label
            {
                get => label;
                set
                {
                    if (!char.IsUpper(value[0]))
                    {
                        throw new Exception("Invalid value of label");
                    }
                    label = value;
                }
            }
            public string Surname
            {
                get => surname;
                set
                {
                    if (!char.IsUpper(value[0]))
                    {
                        throw new Exception("Invalid value of surname");
                    }
                    surname = value;
                }
            }
            public int CountOfStudents
            {
                get => countOfStudents;
                set
                {
                    if(value < 0)
                    {
                        throw new Exception("Invalid value of count of students");
                    }
                    countOfStudents = value;
                }
            }
            public int HoursOfLectures
            {
                get => hoursOfLectures;
                set
                {
                    if(value < 0)
                    {
                        throw new Exception("Invalid value of hours of lectures");
                    }
                    hoursOfLectures = value;
                }
            }
            public int HoursOfPractica
            {
                get => hoursOfPractica;
                set
                {
                    if(value < 0)
                    {
                        throw new Exception("Invalid value of hours of practica");
                    }
                    hoursOfPractica = value;
                }
            }
            public ViewOfMainTest CursProject { get => cursProject; set => cursProject = value; }

            public Subject(string label, string surname, int students, int lectures, int practica, ViewOfMainTest test, bool curs)
            {
                Label = label;
                Surname = surname;
                CountOfStudents = students;
                HoursOfLectures = lectures;
                HoursOfPractica = practica;
                CursProject = test;
                Curs = curs;
            }
            public override string ToString()
            {
                return $"Label: {Label}, Surname: {Surname}, Students: {CountOfStudents}, Lectures: {HoursOfLectures}, Practica: {HoursOfPractica}, View of main test: {CursProject}, Nalichie cursovoi: {Curs}";
            }
        }

        public static XElement AddSubject()
        {
            Console.WriteLine("Input values \n " +
                "Label \n" +
                "Surname \n" +
                "Lectures \n" +
                "Practica \n" +
                "Test \n" +
                "Curs \n");
            string label = Console.ReadLine();
            string surname = Console.ReadLine();
            int students = Convert.ToInt32(Console.ReadLine());
            int lectures = Convert.ToInt32(Console.ReadLine());
            int practica = Convert.ToInt32(Console.ReadLine());
            ViewOfMainTest test = ViewOfMainTest.Exam;
            bool curs = Convert.ToBoolean(Console.ReadLine());

            Subject sb = new Subject(label, surname, students, lectures, practica, test, curs);
            return new XElement("Subject", new XAttribute("Label", sb.Label),
                    new XElement("Surname", sb.Surname),
                    new XElement("Lectures", sb.HoursOfLectures),
                    new XElement("Practica", sb.HoursOfPractica),
                    new XElement("Test", sb.CursProject),
                    new XElement("Curs", sb.Curs));
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
        static XDocument CreateDocGroup<T>(IEnumerable<IGrouping<T, Subject>> group)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Grouping"));
            foreach (var item in group)
            {
                doc.Root.Add(new XElement("Group", new XAttribute("GroupAttribute", item.Key),
                                                    item.ToList().Select(sb => new XElement("Subject",
                                                    new XAttribute("Label", sb.Label),
                                                    new XElement("Surname", sb.Surname),
                                                    new XElement("Lectures", sb.HoursOfLectures),
                                                    new XElement("Practica", sb.HoursOfPractica),
                                                    new XElement("Test", sb.CursProject),
                                                    new XElement("Curs", sb.Curs)))));
            }
            return doc;
        }

        static XDocument CreateDocument(List<Subject> list)
        {
            XDocument task1 = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("task1", list.Select(sb => new XElement("Subject",
                                                                new XAttribute("Label", sb.Label),
                                                                new XElement("Surname", sb.Surname),
                                                                new XElement("Lectures", sb.HoursOfLectures),
                                                                new XElement("Practica", sb.HoursOfPractica),
                                                                new XElement("Test", sb.CursProject),
                                                                new XElement("Curs", sb.Curs)))));
            return task1;
        }
        public static void SaveDoc(XDocument doc, string fileName)
        {
            doc.Save(Path.Combine(Environment.CurrentDirectory, fileName));
        }
        static void IndividualTasks(List<Subject> subjects)
        {
            Console.WriteLine("Input value from 1-5");
            int number = Convert.ToInt32(Console.ReadLine());
            switch (number)
            {
                case 1:
                    var answer0 = subjects.OrderBy(el => el.Surname).ThenBy(el => el.CountOfStudents);
                    foreach (Subject subject in answer0)
                    {
                        Console.WriteLine(subject);
                    }
                    XDocument task1 = CreateDocument(answer0.ToList());
                    SaveDoc(task1, "task1.xml");
                    break;
                case 2:
                    var answer1 = subjects.Where(el => el.Curs == true);
                    foreach (Subject subject in answer1)
                    {
                        Console.WriteLine(subject.Label);
                    }
                    XDocument task2 = CreateDocument(answer1.ToList());
                    SaveDoc(task2, "task2.xml");
                    break;
                case 3:
                    var answer2 = subjects.OrderBy(el => el.HoursOfPractica).ToList();
                    Console.WriteLine(answer2[answer2.Count - 1]);
                    XDocument task3 = CreateDocument(answer2);
                    SaveDoc(task3, "task3.xml");
                    break;
                case 4:
                    foreach (Subject subject in subjects)
                    {
                        Console.WriteLine(subject.Label + " - " + subject.HoursOfPractica);
                    }
                    XDocument task4 = CreateDocument(subjects);
                    SaveDoc(task4, "task4.xml");
                    break;
                case 5:
                    var groupLabel = subjects.GroupBy(el => el.Label);
                    foreach (IGrouping<string, Subject> subject in groupLabel)
                    {
                        Console.WriteLine(subject.Key);
                        foreach (var s in subject)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    XDocument group = CreateDocGroup(groupLabel);
                    SaveDoc(group, "group.xml");

                    var groupSurname = subjects.GroupBy(el => el.Surname);
                    foreach (IGrouping<string, Subject> subject in groupSurname)
                    {
                        Console.WriteLine(subject.Key);
                        foreach (var s in subject)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    XDocument group1 = CreateDocGroup(groupSurname);
                    SaveDoc(group1, "group1.xml");

                    var groupCountOfStudents = subjects.GroupBy(el => el.CountOfStudents);
                    foreach (IGrouping<int, Subject> subject in groupCountOfStudents)
                    {
                        Console.WriteLine(subject.Key);
                        foreach (var s in subject)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    XDocument group2 = CreateDocGroup(groupCountOfStudents);
                    SaveDoc(group2, "group2.xml");

                    var groupHoursOfLectures = subjects.GroupBy(el => el.HoursOfLectures);
                    foreach (IGrouping<int, Subject> subject in groupHoursOfLectures)
                    {
                        Console.WriteLine(subject.Key);
                        foreach (var s in subject)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    XDocument group3 = CreateDocGroup(groupHoursOfLectures);
                    SaveDoc(group3, "group3.xml");

                    var groupHoursOfPractica = subjects.GroupBy(el => el.HoursOfPractica);
                    foreach (IGrouping<int, Subject> subject in groupHoursOfPractica)
                    {
                        Console.WriteLine(subject.Key);
                        foreach (var s in subject)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    XDocument group4 = CreateDocGroup(groupHoursOfPractica);
                    SaveDoc(group4, "group4.xml");

                    var groupCursProject = subjects.GroupBy(el => el.CursProject);
                    foreach (IGrouping<ViewOfMainTest, Subject> subject in groupCursProject)
                    {
                        Console.WriteLine(subject.Key);
                        foreach (var s in subject)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    XDocument group5 = CreateDocGroup(groupCursProject);
                    SaveDoc(group5, "group5.xml");

                    var groupCurs = subjects.GroupBy(el => el.Curs);
                    foreach (IGrouping<bool, Subject> subject in groupCurs)
                    {
                        Console.WriteLine(subject.Key);
                        foreach (var s in subject)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    XDocument group6 = CreateDocGroup(groupCurs);
                    SaveDoc(group6, "group6.xml");
                    break;
                default:
                    throw new Exception("Wrong number");
            }
        }
        static void Start(List<Subject> subjects)
        {
            int choice = Choice();
            var xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Subjects", subjects.Select(sb => new XElement("Subject",
                                    new XAttribute("Label", sb.Label),
                                    new XElement("Surname", sb.Surname),
                                    new XElement("Lectures", sb.HoursOfLectures),
                                    new XElement("Practica", sb.HoursOfPractica),
                                    new XElement("Test", sb.CursProject),
                                    new XElement("Curs", sb.Curs)))));
            switch (choice)
            {
                case 1:
                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml"));
                    break;
                case 2:
                    xDoc.Root.Add(AddSubject());
                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml"));
                    break;
                case 3:
                    IndividualTasks(subjects);
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
            List<Subject> subjects = new List<Subject>();
            subjects.Add(new Subject("Chemistry", "Eris", 24, 76, 23, ViewOfMainTest.Test,true));
            subjects.Add(new Subject("Biology", "Eris", 24, 54, 16, ViewOfMainTest.Exam,false));
            subjects.Add(new Subject("Math", "Pups", 29, 76, 23, ViewOfMainTest.Test,false));
            subjects.Add(new Subject("Chemistry", "Pups", 23, 76, 12, ViewOfMainTest.Exam,true));
            subjects.Add(new Subject("Chemistry", "Queen", 36, 54, 23, ViewOfMainTest.Test,false));
            subjects.Add(new Subject("Language", "Queen", 35, 35, 16, ViewOfMainTest.Exam,true));
            subjects.Add(new Subject("Math", "Pups", 38, 35, 23, ViewOfMainTest.Test,false));
            subjects.Add(new Subject("Language", "Weers", 24, 54, 16, ViewOfMainTest.Exam,true));
            subjects.Add(new Subject("Biology", "Pups", 67, 35, 23, ViewOfMainTest.Test,false));
            subjects.Add(new Subject("Math", "Weers", 24, 54, 12, ViewOfMainTest.Exam,true));

            Start(subjects);
        }
    }
}

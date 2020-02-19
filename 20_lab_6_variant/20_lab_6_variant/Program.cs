using System;
using System.Collections.Generic;
using System.Linq;

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

            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    var answer0 = subjects.OrderBy(el => el.Surname).ThenBy(el => el.CountOfStudents);
                    foreach(Subject subject in answer0)
                    {
                        Console.WriteLine(subject);
                    }
                    break;
                case 2:
                    var answer1 = subjects.Where(el => el.Curs == true);
                    foreach(Subject subject in answer1)
                    {
                        Console.WriteLine(subject.Label);
                    }
                    break;
                case 3:
                    var answer2 = subjects.OrderBy(el => el.HoursOfPractica).Last();
                    Console.WriteLine(answer2);
                    break;
                case 4:
                    foreach(Subject subject in subjects)
                    {
                        Console.WriteLine(subject.Label + " - " + subject.HoursOfPractica);
                    }
                    break;
                case 5:
                    var answer3 = subjects.GroupBy(el => el.Label);
                    foreach(IGrouping<string,Subject> subject in answer3)
                    {
                        Console.WriteLine(subject.Key);
                        foreach(var s in subject)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    var answer4 = subjects.GroupBy(el => el.Surname);
                    foreach (IGrouping<string, Subject> subject in answer4)
                    {
                        Console.WriteLine(subject.Key);
                        foreach (var s in subject)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    var answer5 = subjects.GroupBy(el => el.CountOfStudents);
                    foreach (IGrouping<int, Subject> subject in answer5)
                    {
                        Console.WriteLine(subject.Key);
                        foreach (var s in subject)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    var answer6 = subjects.GroupBy(el => el.HoursOfLectures);
                    foreach (IGrouping<int, Subject> subject in answer6)
                    {
                        Console.WriteLine(subject.Key);
                        foreach (var s in subject)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    var answer7 = subjects.GroupBy(el => el.HoursOfPractica);
                    foreach (IGrouping<int, Subject> subject in answer7)
                    {
                        Console.WriteLine(subject.Key);
                        foreach (var s in subject)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    var answer8 = subjects.GroupBy(el => el.CursProject);
                    foreach (IGrouping<ViewOfMainTest, Subject> subject in answer8)
                    {
                        Console.WriteLine(subject.Key);
                        foreach (var s in subject)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    var answer9 = subjects.GroupBy(el => el.Curs);
                    foreach (IGrouping<bool, Subject> subject in answer9)
                    {
                        Console.WriteLine(subject.Key);
                        foreach (var s in subject)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    break;
            }
        }
    }
}

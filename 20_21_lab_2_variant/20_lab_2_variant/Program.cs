using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace _20_lab_2_variant
{
    class Program
    {

        public static XElement AddChennal()
        {
            Console.WriteLine("Input values \n " +
                "labelOfChannel \n" +
                "genreOfChannel \n" +
                "countOfVideo \n" +
                "videos \n");
            string labelOfChannel = Console.ReadLine();
            string genreOfChannel = Console.ReadLine();
            int countOfVideo = Convert.ToInt32(Console.ReadLine());
            List<Video> videos = new List<Video>(3);
            foreach (Video el in videos)
            {
                string labelOfVideo = Console.ReadLine();
                int countOfViews = Convert.ToInt32(Console.ReadLine());
                int countOfLikes = Convert.ToInt32(Console.ReadLine());
                int countOfDislike = Convert.ToInt32(Console.ReadLine());
                int countOfComments = Convert.ToInt32(Console.ReadLine());
            }
            Canal cn = new Canal(labelOfChannel, genreOfChannel, countOfVideo, videos);
            return new XElement("Canal", new XAttribute("labelOfChannel", cn.LabelOfCanal),
                    new XElement("genreOfCanal", cn.GenreOfCanal),
                    new XElement("countOfVideo", cn.CountOfVideo),
                    new XElement("videos", cn.VideosToString()));
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
        static XDocument CreateDocGroup<T>(IEnumerable<IGrouping<T, Canal>> group)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Grouping"));
            foreach (var item in group)
            {
                doc.Root.Add(new XElement("Group", new XAttribute("GroupAttribute", item.Key),
                                                    item.ToList().Select(cn => new XElement("Canal",
                                  new XAttribute("labelOfChannel", cn.LabelOfCanal),
                                  new XElement("genreOfCanal", cn.GenreOfCanal),
                                  new XElement("countOfVideo", cn.CountOfVideo),
                                  new XElement("videos", cn.VideosToString())))));
            }
            return doc;
        }
        static XDocument CreateDocument(List<Canal> list)
        {
            XDocument task1 = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("task1", list.Select(cn => new XElement("Canal",
                                  new XAttribute("labelOfChannel", cn.LabelOfCanal),
                                  new XElement("genreOfCanal", cn.GenreOfCanal),
                                  new XElement("countOfVideo", cn.CountOfVideo),
                                  new XElement("videos", cn.VideosToString())))));
            return task1;
        }
        public static void SaveDoc(XDocument doc, string fileName)
        {
            doc.Save(Path.Combine(Environment.CurrentDirectory, fileName));
        }
        static void IndividualTasks(List<Canal> canals)
        {
            Console.WriteLine("Input value from 1-5");
            int number = Convert.ToInt32(Console.ReadLine());
            switch (number)
            {
                case 1:
                    List<Canal> task1 = canals.OrderBy(el => el.LabelOfCanal).ThenBy(el => el.Videos).ToList();
                    XDocument group1 = CreateDocument(task1);
                    foreach (Canal item in task1)
                    {
                        Console.WriteLine(item);
                    }
                    SaveDoc(group1, "task1.xml");
                    break;
                case 2:
                    var task2 = canals.OrderBy(el => el.Videos.OrderBy(el => el.CountOfViews).First()).First().ToList();
                    Console.WriteLine(task2[0].Videos[0].LabelOfVideo);
                    XDocument group2 = CreateDocument(task2);
                    SaveDoc(group2, "task2.xml");
                    break;
                case 3:
                    var task3 = canals.Where(el => el.Videos.Any(el => el.CountOfViews > 1000000)).ToList();
                    XDocument group3 = CreateDocument(task3);
                    foreach (Canal item in task3)
                    {
                        Console.WriteLine(item.LabelOfCanal);
                    }
                    SaveDoc(group3, "task3.xml");
                    break;
                case 4:
                    var answer1 = canals.Average(el => el.Videos.Average(el => el.CountOfViews));
                    Console.WriteLine("Average of views: " + answer1);
                    var answer2 = canals.Average(el => el.Videos.Average(el => el.CountLikes));
                    Console.WriteLine("Average of likes: " + answer2);
                    var answer3 = canals.Average(el => el.Videos.Average(el => el.CountDislikes));
                    Console.WriteLine("Average of dislikes: " + answer3);
                    var answer4 = canals.Average(el => el.Videos.Average(el => el.CountComments));
                    Console.WriteLine("Average of comments: " + answer4);
                    List<double> averages = new List<double>();
                    averages.Add(answer1);
                    averages.Add(answer2);
                    averages.Add(answer3);
                    averages.Add(answer4);
                    XDocument group4 = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Group"));
                    
                    group4.Root.Add(new XElement("Averages", averages));
                    SaveDoc(group4, "task4.xml");
                    break;
                case 5:

                    var genreOfCanal = canals.GroupBy(el => el.GenreOfCanal);
                    foreach (IGrouping<string, Canal> item in genreOfCanal)
                    {
                        Console.WriteLine(item.Key);
                        foreach (var t in item)
                        {
                            Console.WriteLine(t);
                        }
                    }
                    XDocument t1 = CreateDocGroup(genreOfCanal);
                    SaveDoc(t1, "group.xml");

                    var labelOfCannal = canals.GroupBy(el => el.LabelOfCanal);
                    foreach (IGrouping<string, Canal> item in labelOfCannal)
                    {
                        Console.WriteLine(item.Key);
                        foreach (var t in item)
                        {
                            Console.WriteLine(t);
                        }
                    }
                    XDocument t2 = CreateDocGroup(labelOfCannal);
                    SaveDoc(t2, "group1.xml");

                    var countOfVideo = canals.GroupBy(el => el.CountOfVideo);
                    foreach (IGrouping<int, Canal> item in countOfVideo)
                    {
                        Console.WriteLine(item.Key);
                        foreach (var t in item)
                        {
                            Console.WriteLine(t);
                        }
                    }
                    XDocument t3 = CreateDocGroup(countOfVideo);
                    SaveDoc(t3, "group2.xml");
                    break;
                default:
                    throw new Exception("Wrong number");
            }
        }
        static void Start(List<Canal> canals)
        {
            int choice = Choice();
            var xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Canals", canals.Select(cn => new XElement("Canal",
                                  new XAttribute("labelOfChannel", cn.LabelOfCanal),
                                  new XElement("genreOfCanal", cn.GenreOfCanal),
                                  new XElement("countOfVideo", cn.CountOfVideo),
                                  new XElement("videos", cn.VideosToString())))));
            switch (choice)
            {
                case 1:
                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml"));
                    break;
                case 2:
                    xDoc.Root.Add(AddChennal());
                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml"));
                    break;
                case 3:
                    IndividualTasks(canals);
                    break;
                case 4:
                    break;
                default:
                    throw new Exception("wrong value");
            }
            Console.ReadKey();
        }
        class Video
        {
            private string labelOfVideo;
            private int countOfViews;
            private int countLikes;
            private int countDislikes;
            private int countComments;

            public string LabelOfVideo 
            {
                get => labelOfVideo;
                set
                {
                    if(!char.IsUpper(value[0]))
                    {
                        throw new Exception("Invalid value of label");
                    }
                    labelOfVideo = value;
                }
            }
            public int CountOfViews
            {
                get => countOfViews;
                set
                {
                    if(value < 0)
                    {
                        throw new Exception("Negative value of count of views");
                    }
                    countOfViews = value;
                }
            }
            public int CountLikes
            {
                get => countLikes;
                set
                {
                    if (value < 0)
                    {
                        throw new Exception("Negative value of count of likes");
                    }
                    countLikes = value;
                }
            }
            public int CountDislikes
            {
                get => countDislikes;
                set
                {
                    if (value < 0)
                    {
                        throw new Exception("Negative value of count of dislikes");
                    }
                    countDislikes = value;
                }
            }
            public int CountComments
            {
                get => countComments;
                set
                {
                    if (value < 0)
                    {
                        throw new Exception("Negative value of count of comments");
                    }
                    countComments = value;
                }
            }

            public Video(string label, int views, int likes, int dislikes, int comments)
            {
                LabelOfVideo = label;
                CountOfViews = views;
                CountLikes = likes;
                CountDislikes = dislikes;
                CountComments = comments;
            }
            public override string ToString()
            {
                return $"Label: {CountLikes}, views: {CountOfViews}, likes: {CountLikes}, dislikes: {CountDislikes}, comments: {CountComments}";
            }
        }
        class Canal : IEnumerable<Canal>
        {
            private string labelOfCanal;
            private string genreOfCanal;
            private int countOfVideo;
            private List<Video> videos;
            
            public string LabelOfCanal
            {
                get => labelOfCanal;
                set
                {
                    if (!char.IsUpper(value[0]))
                    {
                        throw new Exception("Invalid value of label");
                    }
                    labelOfCanal = value;
                }
            }
            public string GenreOfCanal
            {
                get => genreOfCanal;
                set
                {
                    if (!char.IsUpper(value[0]))
                    {
                        throw new Exception("Invalid value of genre");
                    }
                    genreOfCanal = value;
                }
            }
            public int CountOfVideo
            {
                get => countOfVideo;
                set
                {
                    if (value < 0)
                    {
                        throw new Exception("Negative value of count of videos");
                    }
                    countOfVideo = value;
                }
            }
            public List<Video> Videos { get => videos; set => videos = value; }

            public string VideosToString()
            {
                string temp = "";
                foreach(Video item in Videos)
                {
                    temp += item;
                    temp += "\n";
                }
                return temp;
            }
            public override string ToString()
            {
                return $"label: {LabelOfCanal}, genre: {GenreOfCanal}, count of videos: {CountOfVideo}\n videos:\n{VideosToString()}";
            }

            public IEnumerator<Canal> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }

            public Canal(string label, string genre, int count, List<Video> videos)
            {
                LabelOfCanal = label;
                GenreOfCanal = genre;
                CountOfVideo = count;
                Videos = videos;
            }

        }
        static void Main(string[] args)
        {
            List<Video> videos = new List<Video>();

            videos.Add(new Video("Panimay", 10000000, 200000, 300, 4000));
            videos.Add(new Video("Retry",30000,4423242,122133,43333));
            videos.Add(new Video("Hight slot", 500000, 340000, 1000, 200));

            List<Canal> canals = new List<Canal>();

            canals.Add(new Canal("Lol", "Humor", 3, videos));
            canals.Add(new Canal("Yuyy", "Humor", 3, videos));
            canals.Add(new Canal("Qwer", "Humor", 3, videos));
            canals.Add(new Canal("Deeen", "Chert", 3, videos));
            canals.Add(new Canal("Akkk", "Aaaaa", 3, videos));
            canals.Add(new Canal("Dss", "Pizdec", 3, videos));
            canals.Add(new Canal("J", "Mirror", 3, videos));
            canals.Add(new Canal("Joy", "Horror", 3, videos));
            canals.Add(new Canal("Saske", "Ert", 3, videos));
            canals.Add(new Canal("Vernis", "Vderevnu", 3, videos));

            Start(canals);
        }
    }
}

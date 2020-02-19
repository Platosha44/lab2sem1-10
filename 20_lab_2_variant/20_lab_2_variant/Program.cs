using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _20_lab_2_variant
{
    class Program
    {
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

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    var answer0 = canals.Where(el => el.LabelOfCanal == "Deeen" && el.Videos[0].LabelOfVideo == "Panimay");
                    foreach(Canal item in answer0)
                    {
                        Console.WriteLine(item);
                    }
                break;
                case 2:
                    var answer1 = canals.OrderBy(el => el.Videos.OrderBy(el => el.CountOfViews).First()).First();
                    Console.WriteLine(answer1.Videos[0].LabelOfVideo);
                break;
                case 3:
                    var answer2 = canals.Where(el => el.Videos.Any(el => el.CountOfViews > 1000000));
                    foreach(Canal item in answer2)
                    {
                        Console.WriteLine(item.LabelOfCanal);
                    }
                break;
                case 4:
                    var answer3 = canals.Average(el => el.Videos.Average(el => el.CountOfViews));
                    Console.WriteLine("Average of views: " + answer3);
                    var answer4 = canals.Average(el => el.Videos.Average(el => el.CountLikes));
                    Console.WriteLine("Average of likes: " + answer4);
                    var answer5 = canals.Average(el => el.Videos.Average(el => el.CountDislikes));
                    Console.WriteLine("Average of dislikes: " + answer5);
                    var answer6 = canals.Average(el => el.Videos.Average(el => el.CountComments));
                    Console.WriteLine("Average of comments: " + answer6);
                    break;
                case 5:
                    var answer7 = canals.GroupBy(el => el.GenreOfCanal);
                    foreach (IGrouping<string, Canal> item in answer7)
                    {
                        Console.WriteLine(item.Key);
                        foreach(var t in item)
                        {
                            Console.WriteLine(t);
                        }
                    }
                    Console.WriteLine();
                    var answer8 = canals.GroupBy(el => el.LabelOfCanal);
                    foreach (IGrouping<string, Canal> item in answer8)
                    {
                        Console.WriteLine(item.Key);
                        foreach (var t in item)
                        {
                            Console.WriteLine(t);
                        }
                    }
                    Console.WriteLine();
                    var answer9 = canals.GroupBy(el => el.CountOfVideo);
                    foreach (IGrouping<int, Canal> item in answer9)
                    {
                        Console.WriteLine(item.Key);
                        foreach (var t in item)
                        {
                            Console.WriteLine(t);
                        }
                    }
                    break;
            }
        }
    }
}

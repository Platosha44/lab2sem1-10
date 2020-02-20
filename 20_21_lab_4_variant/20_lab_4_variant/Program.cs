using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace _20_lab_4_variant
{
    class Program
    {
        class Product
        {
            private string label;
            private string creater;
            private double weight;
            private double cost;
            private int count;
            private int sale;

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
            public string Creater
            {
                get => creater;
                set
                {
                    if (!char.IsUpper(value[0]))
                    {
                        throw new Exception("Invalid value of creater");
                    }
                    creater = value;
                }
            }
            public double Weight
            {
                get => weight;
                set
                {
                    if(value < 0)
                    {
                        throw new Exception("Invalid value of weight");
                    }
                    weight = value;
                }
            }
            public double Cost
            {
                get => cost;
                set
                {
                    if(value < 0)
                    {
                        throw new Exception("Invalid value of cost");
                    }
                    cost = value;
                }
            }
            public int Count
            {
                get => count;
                set
                {
                    if(value < 0)
                    {
                        throw new Exception("Invalid value of count");
                    }
                    count = value;
                }
            }
            public int Sale
            {
                get => sale;
                set
                {
                    if (value < 0 || value > 100)
                    {
                        throw new Exception("Invalid value of sale");
                    }
                    sale = value;
                }
            }

            public Product(string label, string creater, double weight, double cost, int count, int sale)
            {
                Label = label;
                Creater = creater;
                Weight = weight;
                Cost = cost;
                Count = count;
                Sale = sale;
            }

            public override string ToString()
            {
                return $"Label: {Label}, Creater: {Creater}, Weight: {Weight}кг,Cost: {Cost}руб, Count: {Count}шт,Sale: {Sale}%";
            }
        }

        public static XElement AddProduct()
        {
            Console.WriteLine("Input values \n " +
                "label \n" +
                "creater \n" +
                "weight \n" +
                "cost \n" +
                "count \n" +
                "sale \n");
            string label = Console.ReadLine();
            string creater = Console.ReadLine();
            double weight = Convert.ToDouble(Console.ReadLine());
            double cost = Convert.ToDouble(Console.ReadLine());
            int count = Convert.ToInt32(Console.ReadLine());
            int sale = Convert.ToInt32(Console.ReadLine());
            Product pr = new Product(label, creater, weight, cost, count, sale);
            return new XElement("Product", new XAttribute("Label", pr.Label),
                    new XElement("Creater", pr.Creater),
                    new XElement("Weight", pr.Weight),
                    new XElement("Cost", pr.Cost),
                    new XElement("Count", pr.Count),
                    new XElement("Sale", pr.Sale));
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
        static XDocument CreateDocGroup<T>(IEnumerable<IGrouping<T, Product>> group)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement("Grouping"));
            foreach (var item in group)
            {
                doc.Root.Add(new XElement("Group", new XAttribute("GroupAttribute", item.Key),
                                                    item.ToList().Select(pr => new XElement("Product", new XAttribute("Label", pr.Label),
                    new XElement("Creater", pr.Creater),
                    new XElement("Weight", pr.Weight),
                    new XElement("Cost", pr.Cost),
                    new XElement("Count", pr.Count),
                    new XElement("Sale", pr.Sale)))));
            }
            return doc;
        }
        static XDocument CreateDocument(List<Product> list)
        {
            XDocument task1 = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                        new XElement("task1", list.Select(pr => new XElement("Product", new XAttribute("Label", pr.Label),
                    new XElement("Creater", pr.Creater),
                    new XElement("Weight", pr.Weight),
                    new XElement("Cost", pr.Cost),
                    new XElement("Count", pr.Count),
                    new XElement("Sale", pr.Sale)))));
            return task1;
        }
        public static void SaveDoc(XDocument doc, string fileName)
        {
            doc.Save(Path.Combine(Environment.CurrentDirectory, fileName));
        }
        static void IndividualTasks(List<Product> products)
        {
            Console.WriteLine("Input value from 1-5");
            int number = Convert.ToInt32(Console.ReadLine());
            switch (number)
            {
                case 1:
                    var answer1 = products.OrderBy(el => el.Label).ThenBy(el => el.Weight).ToList();
                    foreach (Product product in answer1)
                    {
                        Console.WriteLine(product);
                    }
                    XDocument task1 = CreateDocument(answer1);
                    SaveDoc(task1, "task1.xml");
                    break;
                case 2:
                    var answer2 = products.OrderBy(el => el.Cost).ToList();
                    Console.WriteLine(answer2[answer2.Count - 1]);
                    XDocument task2 = CreateDocument(answer2);
                    SaveDoc(task2, "task2.xml");
                    break;
                case 3:
                    var answer3 = products.Where(el => el.Weight > 1);
                    foreach (Product product in answer3)
                    {
                        Console.WriteLine(product.Label);
                    }
                    XDocument task3 = CreateDocument(answer3.ToList());
                    SaveDoc(task3, "task3.xml");
                    break;
                case 4:
                    var answer4 = products.OrderBy(el => el.Cost).ToList();
                    Console.WriteLine(answer4[0]);
                    XDocument task4 = CreateDocument(answer4);
                    SaveDoc(task4, "task4.xml");
                    break;
                case 5:
                    var groupLabel = products.GroupBy(el => el.Label);
                    foreach (IGrouping<string, Product> product in groupLabel)
                    {
                        Console.WriteLine(product.Key);
                        foreach (var a in product)
                        {
                            Console.WriteLine(a);
                        }
                    }
                    XDocument group = CreateDocGroup(groupLabel);
                    SaveDoc(group, "group.xml");

                    var groupCreater = products.GroupBy(el => el.Creater);
                    foreach (IGrouping<string, Product> product in groupCreater)
                    {
                        Console.WriteLine(product.Key);
                        foreach (var a in product)
                        {
                            Console.WriteLine(a);
                        }
                    }
                    XDocument group1 = CreateDocGroup(groupCreater);
                    SaveDoc(group1, "group1.xml");

                    var groupCost = products.GroupBy(el => el.Cost);
                    foreach (IGrouping<double, Product> product in groupCost)
                    {
                        Console.WriteLine(product.Key);
                        foreach (var a in product)
                        {
                            Console.WriteLine(a);
                        }
                    }
                    XDocument group2 = CreateDocGroup(groupCost);
                    SaveDoc(group2, "group2.xml");

                    var groupWeight = products.GroupBy(el => el.Weight);
                    foreach (IGrouping<double, Product> product in groupWeight)
                    {
                        Console.WriteLine(product.Key);
                        foreach (var a in product)
                        {
                            Console.WriteLine(a);
                        }
                    }
                    XDocument group3 = CreateDocGroup(groupWeight);
                    SaveDoc(group3, "group3.xml");

                    var groupSale = products.GroupBy(el => el.Sale);
                    foreach (IGrouping<int, Product> product in groupSale)
                    {
                        Console.WriteLine(product.Key);
                        foreach (var a in product)
                        {
                            Console.WriteLine(a);
                        }
                    }
                    XDocument group4 = CreateDocGroup(groupSale);
                    SaveDoc(group4, "group4.xml");

                    var groupCount = products.GroupBy(el => el.Count);
                    foreach (IGrouping<int, Product> product in groupCount)
                    {
                        Console.WriteLine(product.Key);
                        foreach (var a in product)
                        {
                            Console.WriteLine(a);
                        }
                    }
                    XDocument group5 = CreateDocGroup(groupCount);
                    SaveDoc(group5, "group5.xml");
                    break;
                default:
                    throw new Exception("Wrong number");
            }
        }
        static void Start(List<Product> students)
        {
            int choice = Choice();
            var xDoc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Products", students.Select(pr => new XElement("Product", new XAttribute("Label", pr.Label),
                    new XElement("Creater", pr.Creater),
                    new XElement("Weight", pr.Weight),
                    new XElement("Cost", pr.Cost),
                    new XElement("Count", pr.Count),
                    new XElement("Sale", pr.Sale)))));
            switch (choice)
            {
                case 1:
                    xDoc.Save(Path.Combine(Environment.CurrentDirectory, "xmlDoc.xml"));
                    break;
                case 2:
                    xDoc.Root.Add(AddProduct());
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
            List<Product> products = new List<Product>();
            products.Add(new Product("Tomato", "Ukrain", 23.3, 1, 2, 25));
            products.Add(new Product("Apple", "Russia", 14.3, 1, 6, 20));
            products.Add(new Product("Pineapple", "Ukrain", 27.3, 2, 2, 35));
            products.Add(new Product("Lime", "America", 17.3, 2, 6, 20));
            products.Add(new Product("Limon", "Ukrain", 25.2, 5, 8, 25));
            products.Add(new Product("Apple", "Africa", 23.8, 3, 8, 30));
            products.Add(new Product("Tomato", "Africa", 21.9, 2, 6, 30));
            products.Add(new Product("Pineapple", "Gorgia", 27.2, 3, 6, 20));
            products.Add(new Product("Paneapple", "Russia", 15.4, 4, 3, 35));
            products.Add(new Product("Tomato", "America", 0.8, 3, 3, 20));

            Start(products);
        }
    }
}

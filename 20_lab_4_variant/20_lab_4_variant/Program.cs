using System;
using System.Collections.Generic;
using System.Linq;

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

            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    var answer0 = products.Where(el => el.Label == "Apple" && el.Weight == 23.8);
                    foreach(Product product in answer0)
                    {
                        Console.WriteLine(product);
                    }
                    break;
                case 2:
                    var answer1 = products.OrderBy(el => el.Cost).Last();
                        Console.WriteLine(answer1);
                    break;
                case 3:
                    var answer2 = products.Where(el => el.Weight > 1);
                    foreach (Product product in answer2)
                    {
                        Console.WriteLine(product.Label);
                    }
                    break;
                case 4:
                    var answer3 = products.OrderBy(el => el.Cost).First();
                        Console.WriteLine(answer3);
                    break;
                case 5:
                    var answer4 = products.GroupBy(el => el.Label);
                    foreach (IGrouping<string,Product> product in answer4)
                    {
                        Console.WriteLine(product.Key);
                        foreach(var a in product)
                        {
                            Console.WriteLine(a);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    var answer5 = products.GroupBy(el => el.Creater);
                    foreach (IGrouping<string, Product> product in answer5)
                    {
                        Console.WriteLine(product.Key);
                        foreach (var a in product)
                        {
                            Console.WriteLine(a);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    var answer6 = products.GroupBy(el => el.Cost);
                    foreach (IGrouping<double, Product> product in answer6)
                    {
                        Console.WriteLine(product.Key);
                        foreach (var a in product)
                        {
                            Console.WriteLine(a);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    var answer7 = products.GroupBy(el => el.Weight);
                    foreach (IGrouping<double, Product> product in answer7)
                    {
                        Console.WriteLine(product.Key);
                        foreach (var a in product)
                        {
                            Console.WriteLine(a);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    var answer8 = products.GroupBy(el => el.Sale);
                    foreach (IGrouping<int, Product> product in answer8)
                    {
                        Console.WriteLine(product.Key);
                        foreach (var a in product)
                        {
                            Console.WriteLine(a);
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    var answer9 = products.GroupBy(el => el.Count);
                    foreach (IGrouping<int, Product> product in answer9)
                    {
                        Console.WriteLine(product.Key);
                        foreach (var a in product)
                        {
                            Console.WriteLine(a);
                        }
                    }
                    break;
            }

        }
    }
}

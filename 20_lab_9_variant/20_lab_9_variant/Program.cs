using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace _20_lab_9_variant
{
    class Program
    {
        public class PaymentPhoneCall
        {
            private string surname;
            private string phone;
            private DateTime dateOfCall;
            private double tarif;
            private int sale;
            private TimeSpan start;
            private TimeSpan finish;

            public string Surname
            {
                get => surname;
                set
                {
                    if (!Char.IsUpper(value[0]))
                    {
                        throw new Exception("Invalid value of surname");
                    }
                    surname = value;
                }
            }
            public string Phone
            {
                get => phone;
                set
                {
                    Regex check = new Regex(@"^\+375(44|29|25)\d{7}$");
                    if (!check.IsMatch(value))
                    {
                        throw new Exception("Invalid value of phone");
                    }
                    phone = value;
                }
            }
            public DateTime DateOfCall { get => dateOfCall; set => dateOfCall = value; }
            public double Tarif
            {
                get => tarif;
                set
                {
                    if(value < 0 || value.ToString().Substring(value.ToString().IndexOf(',') + 1).Length != 2)
                    {
                        throw new Exception("Invalid value of tarif");
                    }
                    tarif = value;
                }
            }
            public int Sale
            {
                get => sale;
                set
                {
                    if(value < 0 || value > 100)
                    {
                        throw new Exception("Invalid value of sale");
                    }
                    sale = value;
                }
            }
            public TimeSpan Start { get => start; set => start = value; }
            public TimeSpan Finish { get => finish; set => finish = value; }

            public PaymentPhoneCall(string surname, string phone, DateTime dateOfCall, double tarif, int sale, TimeSpan start, TimeSpan finish)
            {
                Surname = surname;
                Phone = phone;
                DateOfCall = dateOfCall;
                Tarif = tarif;
                Sale = sale;
                Start = start;
                Finish = finish;
            }

            public override string ToString()
            {
                return $"Surname: {Surname}, Phone: {Phone}, Date of call: {DateOfCall}, Tarif: {Tarif}, Start: {Start}, Finish: {Finish}";
            }
        }
   
        static void Main(string[] args)
        {
            List<PaymentPhoneCall> calls = new List<PaymentPhoneCall>();
            calls.Add(new PaymentPhoneCall("Sefgh", "+375443452354", new DateTime(2018, 11, 25), 2.54, 20, new TimeSpan(0,2,4),new TimeSpan(3,2,1)));
            calls.Add(new PaymentPhoneCall("Sgfgfg", "+375293445354", new DateTime(2019, 11, 25), 2.14, 20, new TimeSpan(1, 2, 4), new TimeSpan(3, 2, 1)));
            calls.Add(new PaymentPhoneCall("Sefg", "+375293445354", new DateTime(2018, 11, 25), 2.24, 20, new TimeSpan(1, 2, 4), new TimeSpan(4, 2, 1)));
            calls.Add(new PaymentPhoneCall("Sefgh", "+375443452354", new DateTime(2019, 11, 25), 2.54, 20, new TimeSpan(0, 2, 4), new TimeSpan(4, 2, 1)));
            calls.Add(new PaymentPhoneCall("Sefg", "+375443452354", new DateTime(2018, 11, 25), 2.54, 20, new TimeSpan(1, 2, 4), new TimeSpan(4, 2, 1)));
            calls.Add(new PaymentPhoneCall("Sefgh", "+375293445354", new DateTime(2018, 11, 25), 2.24, 20, new TimeSpan(1, 2, 4), new TimeSpan(4, 2, 1)));
            calls.Add(new PaymentPhoneCall("Sefg", "+375443452354", new DateTime(2019, 11, 25), 2.54, 20, new TimeSpan(1, 2, 4), new TimeSpan(4, 2, 1)));
            calls.Add(new PaymentPhoneCall("Sefgh", "+375293445354", new DateTime(2018, 11, 25), 2.14, 20, new TimeSpan(1, 2, 4), new TimeSpan(3, 2, 1)));
            calls.Add(new PaymentPhoneCall("Sgfgfg", "+375443452354", new DateTime(2019, 11, 25), 2.14, 20, new TimeSpan(0, 2, 4), new TimeSpan(3, 2, 1)));
            calls.Add(new PaymentPhoneCall("Sgfgfg", "+375443452354", new DateTime(2018, 11, 25), 2.24, 20, new TimeSpan(0, 2, 4), new TimeSpan(4, 2, 1)));

            Console.WriteLine("введите 1 2 3 4 или 5");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    {
                        foreach(PaymentPhoneCall call in calls.OrderBy(el => el.Surname).ThenBy(el => el.DateOfCall).ToList())
                        {
                            Console.WriteLine(call);
                        }
                        break;
                    }
                case 2:
                    {
                        foreach (IGrouping<string, PaymentPhoneCall> call in calls.GroupBy(el => el.Phone).ToList())
                        {
                            if(call.ToList().Count > 1)
                            {
                                Console.WriteLine(call.Key);
                                foreach(PaymentPhoneCall item in call)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                        }
                        break;
                    }
                case 3:
                    { 
                        foreach(PaymentPhoneCall call in calls.OrderByDescending(el => el.Finish - el.Start).ToList())
                        {
                            Console.WriteLine(call);
                        }
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine(calls.OrderBy(el => (el.Tarif * el.Finish.Minutes - el.Tarif * el.Sale/100)).First().Tarif);
                        break;
                    }
                case 5:
                    {
                        foreach (IGrouping<string, PaymentPhoneCall> call in calls.GroupBy(el => el.Surname))
                        {
                            Console.WriteLine(call.Key);
                            foreach (PaymentPhoneCall item in call)
                                Console.WriteLine(item);
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                        foreach (IGrouping<string, PaymentPhoneCall> call in calls.GroupBy(el => el.Phone))
                        {
                            Console.WriteLine(call.Key);
                            foreach (PaymentPhoneCall item in call)
                                Console.WriteLine(item);
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                        foreach (IGrouping<double, PaymentPhoneCall> call in calls.GroupBy(el => el.Tarif))
                        {
                            Console.WriteLine(call.Key);
                            foreach (PaymentPhoneCall item in call)
                                Console.WriteLine(item);
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                        foreach (IGrouping<TimeSpan, PaymentPhoneCall> call in calls.GroupBy(el => el.Start))
                        {
                            Console.WriteLine(call.Key);
                            foreach (PaymentPhoneCall item in call)
                                Console.WriteLine(item);
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                        foreach (IGrouping<TimeSpan, PaymentPhoneCall> call in calls.GroupBy(el => el.Finish))
                        {
                            Console.WriteLine(call.Key);
                            foreach (PaymentPhoneCall item in call)
                                Console.WriteLine(item);
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                        foreach (IGrouping<DateTime, PaymentPhoneCall> call in calls.GroupBy(el => el.DateOfCall))
                        {
                            Console.WriteLine(call.Key);
                            foreach (PaymentPhoneCall item in call)
                                Console.WriteLine(item);
                        }
                        Console.WriteLine();
                        Console.WriteLine();
                        break;
                    }
            }
        }
    }
}

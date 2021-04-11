using System;

namespace home
{

    namespace andrej_OOP
    {
        class Person
        {
            string name;
            int age;


            public string Name
            {
                get
                {
                    return name;
                }

                set
                {
                    if (String.IsNullOrEmpty(value)) throw new Exception("Name must not be empty.");
                    name = value;
                }
            }

            public int Age
            {
                get
                {
                    return age;
                }

                set
                {
                    if (age < 0 || age > 150) throw new Exception("Age must be more than zero and less tnen 150.");
                    age = value;
                }
            }

            public int AgeIn4
            {
                get
                {
                    return age + 4;
                }
            }

            public string Info
            {
                get
                {
                    return "Name: " + Name + ", age in 4 years: " + AgeIn4;
                }
            }
        }
        class Program
        {
            static void Main(string[] args)
            {
                Person[] m = new Person[3];
                for (int i = 0; i < m.Length; i++)
                {
                    m[i] = new Person();
                    while (true)
                    {
                        Console.Write("Enter name " + i + ": ");
                        try
                        {
                            m[i].Name = Console.ReadLine();
                        }
                        catch (ArgumentNullException e)
                        {
                            Console.WriteLine(e.Message);
                            continue;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            continue;
                        }
                        break;
                    }

                    while (true)
                    {
                        Console.Write("Enter age " + i + ": ");
                        try
                        {
                            m[i].Age = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                            continue;
                        }
                        catch (OverflowException e)
                        {
                            Console.WriteLine(e.Message);
                            continue;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            continue;
                        }
                        break;
                    }
                }

                for (int i = 0; i < m.Length; i++)
                {
                    Console.WriteLine(m[i].Info);
                }
                Console.ReadKey();
            }
        }

    }
}
            
        
    
using System.Linq.Expressions;

namespace ConsoleApp18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 2, 3};

            try
            {
                //int a = 5;
                //int b = 0;
                //int result = a / b;
                throw new IndexOutOfRangeException();

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(arr[i]);
                }
            
            }
            catch(IndexOutOfRangeException ex)
            {
                Console.WriteLine("배열의 범위를 벗어났습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("부모 예외클래스에서 잡혔습니다.");
            }
            finally
            {
                Console.WriteLine("무조건 실행 됩니다.");
            }
        }
    }
}
------------------------------------------------------------
namespace FileTest01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "c:\\Temp\\abc.txt";
            string content = "안녕!";

            //쓰기
            File.WriteAllText(path, content);
            Console.WriteLine("파일 쓰기 성공");

            //읽기
            string path2 = "c:\\Temp\\ccc.txt";
            try
            {
                string words = File.ReadAllText(path2);
                Console.WriteLine(words);
            }
            catch (Exception ex)
            {
                Console.WriteLine("파일의 이름이 잘못되었습니다.");
            }
        }
    }
}
------------------------------------------------------------
namespace FileTest02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"c:\Temp\hello.txt";
            string content = "안녕하세요 반갑습니다.";

            using (StreamWriter writer = new StreamWriter(path))//using 있으면 Close() 안해줘도됨
            {
                writer.WriteLine(content);

                //writer.Close(); // 이게 있어야 진짜서 글이 써짐
            }
            Console.WriteLine("현재 프로그램을 종료합니다.");
        }
    }
}
-------------------------------------------------------------
namespace LingQuiz02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Char ch = 'A';
            Char[] alpaabets = new char[26];

            for(int i = 0; i <26; i++)
            {
                alpaabets[i] = ch++;
            }

            //Ling
            var result = from c in alpaabets
                         orderby c descending
                         select c;
            foreach( char e in result)
            {
                Console.WriteLine(e+ " ");
            }
            Console.WriteLine();
        }
    }
}
------------------------------------------------------------
namespace LinqExam03
{
    //p624
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }

        public Person(string name, int age, string address)
        {
            Name = name;
            Age = age;
            Address = address;
        }
        public override string ToString()
        {
            return string.Format($"{Name}{Age}{Address}");
        }
    }
    class MainLanguage
    {
        public string Name { get; set; }
        public string Language { get; set; }

        public MainLanguage(string name, string language)
        {
            Name = name;
            Language = language;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>
            {
                new Person("Tom", 63, "Korea"),
                new Person("Winnie", 40, "Tibet"),
                new Person("Anders", 47, "Sudan"),
                new Person("Hans", 25, "Tibet"),
                new Person("Eureka", 32, "Sudan"),
                new Person("Hawk", 15, "Korea")
            };

            List<MainLanguage> languages = new List<MainLanguage>
            {
                new MainLanguage("Anders", "Delphi"),
                new MainLanguage("Anders", "C#"),
                new MainLanguage("Tom", "Borland C++"),
                new MainLanguage("Hans", "Visual C++"),
                new MainLanguage("Winnie", "R")
            };

            //Ling
            var all = from person in people
                      select person;
            //foreach (var item in all)
            //{
            //    Console.WriteLine($"{item.Name}, {item.Age}, {item.Address}");
            //}
            //Console.WriteLine(string.Join(Environment.NewLine, all));

            var nameList1 = from person in people
                           select person.Name;
            foreach ( var item in nameList1 )
            {
                Console.WriteLine(item);
            }

            
        }
    }
}
------------------------------------------------------------------
namespace LingStandardTest01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 1, 3, 5, 6, 7, 9, 11, 13, 15, 20 };
            int even = numbers.FirstOrDefault(n => n % 2 == 0);

            if (even == 0)
                Console.WriteLine("짝수가 없다.");
            else
                Console.WriteLine(even);
        }
    }
}
--------------------------------------------------------------------
namespace LamdaExam04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Action act1 = () => Console.WriteLine("Action()");
            act1();

            int result = 0;
            Action<int> act2 = (x) => result = x * x;
            act2(3);
            Console.WriteLine(result);

            Action<double, double> act3 = (x, y) =>
            {
                double pi = x / y;
                Console.WriteLine(pi);
            };
            act3(22.0, 7.0);
        }
    }
}


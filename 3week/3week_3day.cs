namespace ArrayTest01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string a = Console.ReadLine();
            string[] b = a.Split(' ');

            foreach (string name in b)
            {
                Console.Write(name + " ");
            }
        }
    }
}
----------------------------------------------------
using System.Security.Cryptography.X509Certificates;

namespace ArrayTest01
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "아무개";
        public string Major { get; set; } = "공통학부";
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Student[] student = new Student[3];
            Student s1 = new Student();
            Student s2 = new Student();
            Student s3 = new Student();

            student[0] = s1;
            student[1] = s2;
            student[2] = s3;

            s1.Id = 1;
            s1.Name = "이명박";
            s1.Major = "경제학과";

            s2.Id = 2;
            s2.Name = "윤석열";
            s2.Major = "검찰학과";

            s3.Id = 3;
            s3.Name = "문재인";
            s3.Major = "코로나학과";
            
            foreach (Student s in student)
            {
                Console.WriteLine(s.Id);
                Console.WriteLine(s.Name);
                Console.WriteLine(s.Major);
            }

            for (int i = 0; i < student.Length; i++)
            {
                Console.WriteLine(student[i].Id);
                Console.WriteLine(student[i].Name);
                Console.WriteLine(student[i].Major);
            }
        }
    }
}
-----------------------------------------------------------------
using System.Security.Cryptography.X509Certificates;

namespace ArrayTest01
{

    internal class Program
    {
        static void TestMethod(double[] arr)
        {

        }
        static int TotalSum(params int[] myArray)
        {
            int sum = 0;

            for (int i = 0; i < myArray.Length; i++)
            {
                sum += myArray[i];
            }
            return sum;
        }
        static void Main(string[] args)
        {

            double[] arr2 = { 1, 2, 3 };

            TestMethod(arr2);
            Console.WriteLine(TotalSum(1, 2, 3, 4, 5, 6, 7, 8, 9));
        }
    }
}
-----------------------------------------------------------------
namespace Indexer_Test
{
    class IdxDemo
    {
        private int[] num = new int[5];

        public int this[int x]
        {
            get { return num[x]; }
            set { num[x] = value; }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            IdxDemo test = new IdxDemo();
            for (int i = 0; i < 5; i++)
            {
                test[i] = i;
                Console.WriteLine(test[i]);
            }
        }
    }
}
-----------------------------------------------------------------
namespace IndexerTest2
{
    class StudentScore
    {
        private double[,] echoScore = new double[3, 3]
        {
            { 0, 0, 0 },
            { 1, 1, 1 },
            { 2, 2, 2 }
        };
        public double this[int x, int y]
        {
            set { echoScore[x, y] = value; }
            get { return echoScore[x, y]; } 
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            int a;
            int b;
            double sum = 0.0;
            double avg = 0.0;

            StudentScore ss = new StudentScore();
            ss[0, 0] = 1; ss[0, 1] = 1; ss[0, 2] = 1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sum += ss[i, j];
                    Console.WriteLine(sum);
                }
            }
        }
    }
}
-----------------------------------------------------------
namespace Code100
{
    enum TrafficLight { Green, Red, Yellow };

    internal class Program
    {
        static void Main(string[] args)
        {
            TrafficLight light = new TrafficLight();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(light+i);
            }
            for (int i = 0;i < 3;i++)
            {
                Console.WriteLine((TrafficLight)i);
            }
        }
    }
}
--------------------------------------------------------
namespace Code99
{
    enum Days { Sun=2, Mon, Tue, Wed=8, Thu, Fri, Sat }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine((int)Days.Sun);
            Console.WriteLine((int)Days.Mon);
            Console.WriteLine((int)Days.Tue);
            Console.WriteLine((int)Days.Wed);
            Console.WriteLine((int)Days.Thu);
            Console.WriteLine((int)Days.Fri);
            Console.WriteLine(Days.Sat);
        }
    }
}
--------------------------------------------------------
namespace Code101
{
    enum TrafficLights { Green=1, Red=10, Yellow=100 }
    internal class Program
    {
        static void Main(string[] args)
        {
            int x = int.Parse(Console.ReadLine());
            TrafficLights trafficLights = new TrafficLights();

            switch(trafficLights + x)
            {
                case TrafficLights.Green:
                    break;
                case TrafficLights.Red:
                    break;
                case TrafficLights.Yellow:
                    break;
                default:
                    break;
            }
            //switch ((TrafficLights)x)
            //{
            //    case TrafficLights.Green:
            //        break;
            //    case TrafficLights.Red:
            //        break;
            //    case TrafficLights.Yellow:
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}

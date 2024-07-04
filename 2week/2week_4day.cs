using System.Security.Cryptography.X509Certificates;

namespace OOPApp06
{
    abstract class Car
    {
        public string kind;
        public int speed;
        public Car()
        {
            kind = "소형차";
            speed = 60;
        }
        public Car(string kind)
        {
            this.kind = kind;
        }
        public Car(string kind, int speed)
        {
            this.kind = kind;
            this.speed = speed;
        }
        public virtual void ShowInformation()
        {
            Console.WriteLine($"차 종류는: {kind}, 속도는: {speed}입니다.");
        } 
    }
    class Bus : Car
    {
        public Bus()
        {
            kind = "버스";
            speed = 70;
        }
        public Bus(string kind)
        {
            this.kind = kind;
            speed = 70;
        }
        public Bus(string kind, int speed)
        {
            this.kind = kind;
            this.speed = speed;
        }
        /*public override void ShowInformation()
        {
            Console.WriteLine($"차 종류는: {kind}, 속도는: {speed}입니다.");
        }*/
    }
    class Taxi : Car
    {
        public Taxi()
        {
            kind = "택시";
            speed = 80;
        }
        public Taxi(string kind)
        {
            this.kind = kind;
            speed = 80;
        }
        public Taxi(string kind, int speed)
        {
            this.kind = kind;
            this.speed = speed;
        }
        /*public override void ShowInformation()
        {
            Console.WriteLine($"차 종류는: {kind}, 속도는: {speed}입니다.");
        }*/
    }
    class Truck : Car
    {
        public Truck()
        {
            kind = "트럭";
            speed = 90;
        }
        public Truck(string kind)
        {
            this.kind = kind;
            speed = 90;
        }
        public Truck(string kind, int speed)
        {
            this.kind = kind;
            this.speed = speed;
        }
        /*public override void ShowInformation()
        {
            Console.WriteLine($"차 종류는: {kind}, 속도는: {speed}입니다.");
        }*/
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Car bus = new Bus();
            Taxi taxi = new Taxi();
            Truck truck = new Truck();
            bus.ShowInformation();
            taxi.ShowInformation();
            truck.ShowInformation();
            Car bus1 = new Bus("좋은버스");
            Taxi taxi1 = new Taxi("좋은 택시");
            Truck truck1 = new Truck("좋은 트럭");
            bus1.ShowInformation();
            taxi1.ShowInformation();
            truck1.ShowInformation();
            Car bus2 = new Bus("좋은버스", 75);
            Taxi taxi2 = new Taxi("좋은 택시", 85);
            Truck truck2 = new Truck("좋은 트럭", 95);
            bus2.ShowInformation();
            taxi2.ShowInformation();
            truck2.ShowInformation();
        }
    }
}
-------------------------------------------------------------------------------
namespace OOP07
{
    //다중상속
    //유니콘 ---> 날개가 있는 말!!
    class Horse
    {
        public void Run()
        {
            Console.WriteLine("말 달리다.");
        }
    }
    class Angel { }
    interface IWing //Abstract Method
    {
        public void Fly();  //Abstract Method

    }
    interface IWing2 //Abstract Method
    {
        public void Fly();  //Abstract Method

    }
    class Unicon : Horse, IWing
    {
        public void Fly()//interface의 메소드 구현
        {
            Console.WriteLine("유니콘 날다!");
        }
        public void performMagic()//유니콘의 멤버 메소드
        {
            Console.WriteLine("마법 뿅뿅");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Unicon unicon = new Unicon();
            unicon.Run();
            unicon.Fly();
            unicon.performMagic();
        }
    }
}
---------------------------------------------------------------------
namespace Quiz07
{
    /*완전수란 자신을 제외한 약수의 합이 자신이 되는 수를 완전수라고 합니다. 6은 완전수입니다.
    6의 약수는 1 2 3 6 이 중 자신을 제외한 약수의 합 1 + 2 + 3 = 6 즉, 6은 완전수입니다.
    1000 이하의 완전수를 입력 받아 yes, no로 표현해 주세요.*/
    internal class Program
    {
        static void Main(string[] args)
        {
            int i = 1;
            int j = 0;
            int[] sum = new int[1000];
            int total = 0;
            while (true)
            {
                int num = Int32.Parse(Console.ReadLine());
                if (num <= 1000 && i < 1000)
                {
                    while (num > i)
                    {
                        if (num % i == 0)
                        {
                            sum[j] = i;
                            total += sum[j];
                            //Console.WriteLine(sum[j]);
                            j++;
                        }
                        i++;
                    }
                    if (total == num || num ==1)
                    {
                        Console.WriteLine("yes");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("no");
                        break;
                    }
                }
                else { Console.WriteLine("1000이하 숫자 다시입력");}
            }
        }
    }
}
-------------------------------------------------------------------
namespace ConsoleApp5
{   /*1 보다 큰 정수 N 가 1 과 N 자신 이외의 양의 약수를 가지지 않을 때의 N 을 소수라고 부른다.
    이를테면, 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31 등은 모두 소수이다.
    4, 6, 16 등과 같이 소수가 아니면서 2 이상인 자연수를 합성수라고 정의하며,
    1 은 소수도 아니고 합성수도 아닌 수이다.*/
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 2; i <= 100; i++)
            {
                int count = 0;
                for (int j = 2; j < i; j++)
                {
                    if(i % j == 0)
                    {
                        count++;
                        break;
                    }
                }
                if (count == 0)
                {
                    Console.WriteLine($"{i}");
                }
            }
        }
    }
}
------------------------------------------------------------------

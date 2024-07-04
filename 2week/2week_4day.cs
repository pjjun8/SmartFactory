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

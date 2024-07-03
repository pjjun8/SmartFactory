namespace MethodApp01
{
    class MyClass
    {
        public  void Print()
        {
            Console.WriteLine("MyClass Hello~!");
        }
        public  void Print(string s)
        {
            Console.WriteLine(s);
        }
        public void Print(string s, double a)
        {
            Console.WriteLine($"{a}, {s}");
        }
        public void Print(double a, string s)
        {
            Console.WriteLine($"{a}, {s}");
        }
    }
    internal class Program
    {
        static void Print()
        {
            Console.WriteLine("Hello~!");
        }
        public static void Print(string s)
        {
            Console.WriteLine(s);
        }
        static void Main(string[] args)
        {
            Print();
            Print("안녕하세요");
            MyClass mc = new MyClass();
            mc.Print();
            mc.Print("반갑습니다.");
            mc.Print("흥해라흥.", 123);
            mc.Print(3.14, "어서오세요.");
        }
    }    
}
---------------------------------------------------------------------------------
  namespace MethodApp01
{
    class Bank
    {
        private int money;

        public Bank()
        {
            this.money = 10000;
        }
        public void Deposit()
        {
            Console.WriteLine($"{money} 금액을 예금하다");
        }
        public void Deposit(int money)
        {
            this.money += money;
            Console.WriteLine($"{money} 금액을 예금하다");
        }
        public void WithDraw()
        {
            Console.WriteLine($"{money}인출하다");
        }
        public void Transfer()
        {
            Console.WriteLine($"{money}이체하다");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank kb = new Bank();

            kb.Deposit();
        }
    }    
}
---------------------------------------------------------------------------
  namespace MethodApp01
{
    class Sensor
    {
        public void ReadData()
        {
            Console.WriteLine($"데이터를 읽다.");
        }
        public void ReadData(byte[] data)
        {
            Console.WriteLine($"{data[0]}, {data[1]}, {data[2]}데이터를 읽다.");
        }
        public void Calibrate()
        {
            Console.WriteLine($"설정값을 조정하다.");
        }
        public void SendAlert()
        {
            Console.WriteLine("경고 보내기.");
        }
        public void SendAlert(string message)
        {
            Console.WriteLine($"{message} 경고 보내기.");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Sensor sensor = new Sensor();
            sensor.ReadData();
            byte[] arr = { 0x001, 0x002, 0x003 };
            sensor.ReadData(arr);
            sensor.Calibrate();
            sensor.SendAlert();
            sensor.SendAlert("온도 초과");
        }
    }    
}
-----------------------------------------------------------------------------
using System.Reflection.Metadata;

namespace OOPApp03
{
    class Shape
    {
        public string name;

        public Shape()
        {
            this.name = "도형";
            Console.WriteLine("부모 클래스 생성자!!");
        }
        public virtual void Draw()
        {
            Console.WriteLine("도형을 그리다.");
        }
    }
    class Ractangle : Shape
    {
        public Ractangle()
        {
            this.name = "사각형";
            Console.WriteLine("자식 클래스 생성자!!");
        }
        public override void Draw()
        {
            Console.WriteLine("사각형을 그리다.");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Ractangle ract = new Ractangle();
            Console.WriteLine(ract.name);
            ract.Draw();
        }
    }
}
-------------------------------------------------------------------------------
namespace OOPApp04
{
    abstract class Shape    //abstract 는 정의는 하되 세상에 나올 수 없다.
    {
        public virtual void Draw()  //virtual 은 상속에서 오버 라이딩 당할때 사용
        {
            Console.WriteLine("도형을 그리다");
        }
    }
    class Triangle : Shape
    {
        public override void Draw()     //override 는 부모에게서 오버라이딩(함수 재정의) 할때 사용
        {
            Console.WriteLine("삼각형을 그리다");
        }
    }
    class Rectangle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("사각형을 그리다");
        }
    }
    class Circle : Shape
    {
        public override void Draw()
        {
            Console.WriteLine("원을 그리다");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Shape shape = new Shape();
            Triangle triangle = new Triangle();
            Rectangle rectangle = new Rectangle();
            Circle circle = new Circle();
            shape.Draw();
            triangle.Draw();
            rectangle.Draw();
            circle.Draw();
        }
    }
}
------------------------------------------------------------------------------

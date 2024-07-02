namespace DoWhile
{
    internal class Program
    {
        static void Call()
        {
            Console.WriteLine("[메뉴선택]");
            Console.WriteLine("1. 데이터 베이스 입력");
            Console.WriteLine("2. 데이터 베이스 검색");
            Console.WriteLine("3. 데이터 베이스 수정");
            Console.WriteLine("4. 데이터 베이스 삭제");
            Console.WriteLine("5. 프로그램 종료");
            Console.Write("선택 : ");
        }
        static void Menu()
        {
            int i = 0;
            do
            {
                Call();
                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        Console.WriteLine("데이터베이스 입력을 선택하셨습니다.");
                        break;
                    case 2:
                        Console.WriteLine("데이터베이스 검색을 선택하셨습니다.");
                        break;
                    case 3:
                        Console.WriteLine("데이터베이스 수정을 선택하셨습니다.");
                        break;
                    case 4:
                        Console.WriteLine("데이터베이스 삭제을 선택하셨습니다.");
                        break;
                    case 5:
                        Console.WriteLine("프로그램을 종료합니다.");
                        i++;
                        break;
                    default:
                        Console.WriteLine("오류! 다시입력하세요");
                        break;
                }
            } while (i < 1);
        }
        static void Main(string[] args)
        {
            Menu();
        }
    }
}
---------------------------------------------------------------------------------
namespace Menu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int stop = 0;
            do
            {
                Console.WriteLine("\n\n메뉴 입력");
                Console.WriteLine("1. 1 ~ 100까지 홀수만 출력합니다. ");
                Console.WriteLine("2. 알파벳 A ~ Z / a ~ z 까지 출력합니다.");
                Console.WriteLine("3. 12와 18의 최대공약수(GCD)를 구해봅니다.");
                Console.WriteLine("4. 프로그램을 종료합니다.");

                switch (int.Parse(Console.ReadLine()))
                {
                    case 1:
                        for (int i = 1; i < 100; i = i + 2)
                        {
                            Console.Write($"{i}, ");
                        }
                        break;
                    case 2:
                        for (int i = 'A'; i <= 'Z'; i++)
                        {
                            Console.Write($"{(char)i}, ");
                        }
                        for (int i = 'a'; i <= 'z'; i++)
                        {
                            Console.Write($"{(char)i}, ");
                        }
                        break;
                    case 3:
                        int a = 18, b = 12 , c;
                        int a1 =a, b1 =b;
                        while (b != 0)
                        {
                            c = a % b;
                            a = b;
                            b = c;
                        }
                        Console.Write($"{a1}와 {b1}의 최대공약수는 {a}");
                        break;
                    case 4:
                        Console.WriteLine("프로그램 종료");
                        stop++;
                        break;
                    default:
                        Console.WriteLine("오류! 다시");
                        break;
                }
            } while (stop < 1);
        }
    }
}
--------------------------------------------------------------------------------
using System.Security.Cryptography.X509Certificates;

namespace OOPApp02
{
    class Car
    {
        public int speed;
        public string brand;
        
        public Car()
        {
            this.speed = 0;
            this.brand = "현대";
        }
        public Car(string brand)
        {
            this.speed = 100;
            this.brand = brand;
        }
        public string Run(int speed)
        {
            this.speed = speed;
            return this.speed + "km 속도로 달립니다.";
        }
        public string Run()
        {
            this.speed = speed;
            return this.speed + "km 속도로 달립니다.";
        }
        public string Showbrand()
        {
            return "제 브랜드 명은 "+this.brand+ "입니다.";
        }
    }
        
    internal class Program
    {
        static void Main(string[] args)
        {
            Car tony = new Car();
            Console.WriteLine(tony.Showbrand());
            Console.WriteLine(tony.Run());
            Console.WriteLine(tony.Run(80));

            Car jack = new Car("제네시스");
            Console.WriteLine(jack.Showbrand());
            Console.WriteLine(jack.Run());
            Console.WriteLine(jack.Run(500));
        }
    }
}
-----------------------------------------------------------------------
namespace OOP_Sample
{
    class Cat
    {
        private string kind;
        private int age;
        public Cat()
        {
            this.age = 10;
            this.kind = "고등어";
        }
        public Cat(int age, string kind)
        {
            this.age = age;
            this.kind = kind;
        }
        public void Get()
        {
            Console.WriteLine($"고양이 나이:{age}, 종: {kind}");
        }
    }
    class Dog
    {
        private string kind;
        private int age;
        public Dog()
        {
            this.age = 5;
            this.kind = "시고르자브종";
        }
        public Dog(int age, string kind)
        {
            this.age = age;
            this.kind = kind;
        }
        public void Get()
        {
            Console.WriteLine($"개 나이:{age}, 종: {kind}");
        }
    }
    class Person
    {
        private string name;
        private int age;
        public Person()
        {
            this.age = 70;
            this.name = "jjs";
        }
        public Person(int age, string name)
        {
            this.age = age;
            this.name = name;
        }
        public void Get()
        {
            Console.WriteLine($"사람 나이:{age}, 이름: {name}");
        }
    }
    class Student
    {
        private string name;
        private int age;
        public Student()
        {
            this.age = 25;
            this.name = "psw";
        }
        public Student(int age, string name)
        {
            this.age = age;
            this.name = name;
        }
        public void Get()
        {
            Console.WriteLine($"학생 나이:{age}, 이름: {name}");
        }
    }
    class Shape
    {
        private string name;
        private int number;
        public Shape()
        {
            this.number = 4;
            this.name = "사각형";
        }
        public Shape(int age, string name)
        {
            this.number = age;
            this.name = name;
        }
        public void Get()
        {
            Console.WriteLine($"모양 개수:{number}, 이름: {name}");
        }
    }
    class Car
    {
        private string brand;
        private int speed;
        public Car()
        {
            this.speed = 100;
            this.brand = "현대";
        }
        public Car(int speed, string brand)
        {
            this.speed = speed;
            this.brand = brand;
        }
        public void Get()
        {
            Console.WriteLine($"차 속도:{speed}, 브랜드: {brand}");
        }
    }
    class Tiger
    {
        private string sex;
        private int age;
        public Tiger()
        {
            this.age = 10;
            this.sex = "수컷";
        }
        public Tiger(int age, string sex)
        {
            this.age = age;
            this.sex = sex;
        }
        public void Get()
        {
            Console.WriteLine($"호랑이 나이:{age}, 성별: {sex}");
        }
    }
    class Lion
    {
        private string sex;
        private int age;
        public Lion()
        {
            this.age = 30;
            this.sex = "암컷";
        }
        public Lion(int age, string sex)
        {
            this.age = age;
            this.sex = sex;
        }
        public void Get()
        {
            Console.WriteLine($"사자 나이:{age}, 성별: {sex}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Cat cat = new Cat();
            cat.Get();
            Cat cat1 = new Cat(20, "샴 고양이");
            cat1.Get();
            Dog dog = new Dog();
            dog.Get();
            Dog dog1 = new Dog(30, "불독");
            dog1.Get();
            Person person = new Person();
            person.Get();
            Person person1 = new Person(35, "장창훈");
            person1.Get();
            Student student = new Student();
            student.Get();
            Student student1 = new Student(11, "안중근");
            student1.Get();
            Shape shape = new Shape();
            shape.Get();
            Shape shape1 = new Shape(3, "삼각형");
            shape1.Get();
            Car car = new Car();
            car.Get();
            Car car1 = new Car(80, "기아");
            car1.Get();
            Tiger tiger = new Tiger();
            tiger.Get();
            Tiger tiger1 = new Tiger(50, "암컷");
            tiger1.Get();
            Lion lion = new Lion();
            lion.Get();
            Lion lion1 = new Lion(40, "수컷");
            lion1.Get();
        }
    }
}
-----------------------------------------------------------------------

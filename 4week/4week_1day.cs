namespace ConsoleApp13
{
    class Person
    {
        public string name;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] arr = new string[3] {"홍길도", "이순신","강감찬"};
            List<char> list = new List<char>() { 'A', 'B', 'C'};
            list.Add('X'); list.Add('Y'); list.Add('Z');

            Person[] persons = new Person[2];
            Person p1 = new Person();
            p1.name = "홍길동";
            persons[0] = p1;
            Person p2 = new Person();
            p2.name = "이순신";
            persons[1] = p2;

            List<Person> list2 = new List<Person>();
            Person p3 = new Person();
            p3.name = "홍길동";
            list2.Add(p3);
            p3 = new Person();
            p3.name = "이순신";
            list2.Add(p3);

            foreach (Person a in list2)
            {
                Console.WriteLine(a.name);
            }
        }
    }
}
---------------------------------------------------------
namespace LIstObject01
{
    class Student
    {
        public string Name { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. 리스트 studentList를 만드세요
            List<Student> studentList = new List<Student>();
            //2. 학생 3명을 만들어서 리스트에 넣으세요
            Student student = new Student();
            //3. 학생은 "이순신", "강감찬", "을지문덕"
            student.Name = "이순신";
            studentList.Add(student);
            student = new Student();
            student.Name = "강감찬";
            studentList.Add(student);
            student = new Student();
            student.Name = "을지문덕";
            studentList.Add(student);
            //4. 출력은 이름만 출력하고 순환문은 foreach를 사용해 주세요
            foreach (Student i in studentList)
            {
                Console.WriteLine(i.Name);
            }
        }
    }
}
---------------------------------------------------------
namespace ConsoleApp14
{
    class Car
    {
        public string Brand { get; set; }
        public int Speed { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. 브랜드와 스피드가 다른 자동차 3개를 만들세요
            Car car1 = new Car();
            Car car2 = new Car();
            Car car3 = new Car();
            // 2. car 객체를 담는 carList를 만듭니다.
            List<Car> carList = new List<Car>();
            car1.Brand = "기아";
            car1.Speed = 100;
            car2.Brand = "현대";
            car2.Speed = 200;
            car3.Brand = "벤츠";
            car3.Speed = 300;
            carList.Add(car1); carList.Add(car2); carList.Add(car3);
            // 3. carList를 이용해서 자동차의 브랜드와 속도를 출력하세요.
            foreach (Car car in carList)
            {
                Console.WriteLine(car.Brand);
                Console.WriteLine(car.Speed);
            }
        }
    }
}
------------------------------------------------------
using System.Threading;

namespace WinFormsApp7
{
    public partial class Form1 : Form
    {
        //멤버변수
        private int number;
        DateTime nowTime;
        //생성자
        public Form1()
        {
            InitializeComponent();
        }

        private void GetNumber()
        {
            number++;
        }
        public void OutNumber()
        {
            textBox1.AppendText(number + "\r\n");   //\r는 캐리즈 리턴으로 엔터를 치면 다음 줄에 커서를 앞으로 땡겨줌
        }
        public void GetTime()
        {
            nowTime = DateTime.Now; //현재 시간을 준다.
        }
        public void OutTime()
        {
            textBox2.AppendText(nowTime + "\r\n");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 5; i++)
            {
                GetNumber();
                OutNumber();
                GetTime();
                OutTime();
                Thread.Sleep(1000);
            }
        }
    }
}
------------------------------------------------
namespace WinFormsApp8
{
    public partial class Form1 : Form
    {
        private int n;
        private int x;
        private int y;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            n = int.Parse(textBox1.Text);
            x = 0; y = 0;

            for (int i = 1; i <= n; i++)
            {
                if (i % 2 == 0)
                {
                    x += i;
                    textBox3.Text = x.ToString();
                }
                else
                {
                    y += i;
                    textBox2.Text = y.ToString();
                }
            }


        }
    }
}
-------------------------------------------------------
namespace WinFormsApp8
{
    public partial class Form1 : Form
    {
        private int n;
        private int x;
        private int y;
        private int z;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            n = int.Parse(textBox1.Text);
            x = 0; y = 0; z = 0;

            for (int i = 1; i <= n; i++)
            {
                if (i % 3 == 0)
                {
                    x += i;
                    textBox2.AppendText(i + " + ");
                }
                else if (i % 3 == 1)
                {
                    y += i;
                    textBox3.AppendText(i + " + ");
                }
                else if (i % 3 == 2)
                {
                    z += i;
                    textBox4.AppendText(i + " + ");
                }
            }
            textBox2.Text = textBox2.Text.Substring(0, textBox2.Text.Length - 2);
            textBox3.Text = textBox3.Text.Substring(0, textBox3.Text.Length - 2);
            textBox4.Text = textBox4.Text.Substring(0, textBox4.Text.Length - 2);
            textBox2.AppendText("= " + x);
            textBox3.AppendText("= " + y);
            textBox4.AppendText("= " + z);
        }
    }
}
-----------------------------------------------------------------------

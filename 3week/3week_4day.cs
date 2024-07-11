namespace OOPTest05
{
    class Person
    {
        //1.멤버변수
       // private string name;
       // private int age;
        //2.생성자
        public Person() { }
        public Person(string name)
        {
            Name = name;   //this.name = name; 
        }
        public Person(string name , int age)
        {
            Name = name;//   this.name = name;
            Age = age;//   this.age = age;
        }
        //3.멤버메소드
        public void Eat()
        {
            Console.WriteLine("밥을 먹습니다.");
        }
        public void Eat(string food)
        {
            Console.WriteLine($"{food}를(을) 먹습니다.");
        }
        //Getter, Setter 접근자
        //public string GetName(){return name;}
        //public void SetName(string name){this.name = name;}
        //public int GetAge(){return age;}
        //public void SetAge(int age){this.age = age;}

        //property
        public string Name { get; set; }
        public int Age { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Person tom = new Person();
            tom.Eat();
            tom.Eat("오랜지");
            //Console.WriteLine(tom.GetName());
            Console.WriteLine(tom.Name);

            Person sam = new Person("Sam");
            //Console.WriteLine(sam.GetName());
            //Console.WriteLine(sam.GetAge());
            Console.Write(sam.Name);
            Console.WriteLine(sam.Age);
            Person tony = new Person("Tony", 25);
            //Console.WriteLine(tony.GetName());
            //Console.WriteLine(tony.GetAge());
            Console.Write(tony.Name);
            Console.WriteLine(tony.Age);
        }
    }
}
-----------------------------------------------------
namespace ConsoleApp8
{
    class Car
    {
        //int speed;
       // string brand;
        public Car() { }
        public Car(int speed) { Speed = speed; }
        public Car(int speed, string brand) { Speed = speed; Brand = brand; }

        //public int GetSpeed() { return Speed; }
        //public void SetSpeed(int speed) { Speed = speed; }
        //public string GetBrand() { return Brand; }
        //public void SetBrand(string brand) { Brand = brand; }
        public int Speed { get; set; }
        public string Brand { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Car car = new Car();
            Car car2 = new Car(80);
            Car car3 = new Car(100, "기아");
            //Console.WriteLine(car.GetSpeed());
            //Console.WriteLine(car2.GetSpeed());
            //Console.WriteLine(car3.GetSpeed());
            //Console.WriteLine(car.GetBrand());
            //Console.WriteLine(car2.GetBrand());
            //Console.WriteLine(car3.GetBrand());
            //car.SetSpeed(300);
            //car.SetBrand("도요타");
            //car2.SetSpeed(500);
            //car2.SetBrand("르노");
            //car3.SetSpeed(1000);
            //car3.SetBrand("현대");
            //Console.WriteLine(car.GetSpeed());
            //Console.WriteLine(car2.GetSpeed());
            //Console.WriteLine(car3.GetSpeed());
            //Console.WriteLine(car.GetBrand());
            //Console.WriteLine(car2.GetBrand());
            //Console.WriteLine(car3.GetBrand());
            car.Speed = 10;
            car.Brand = "BMW";
            car2.Speed = 20;
            car2.Brand = "밴츠";
            car3.Speed = 30;
            car3.Brand = "쉐보레";
            Console.WriteLine(car.Speed);
            Console.WriteLine(car.Brand);
            Console.WriteLine(car2.Speed);
            Console.WriteLine(car2.Brand);
            Console.WriteLine(car3.Speed);
            Console.WriteLine(car3.Brand);
        }
    }
}
----------------------------------------------------------
namespace ConsoleApp11
{
    struct School
    {
        public string schName;
        public string stName;
        public int stGrade;
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            School sc;
            sc.schName = "창원기공";
            Console.WriteLine(sc.schName);
        }
    }
}
--------------------------------------------------------
namespace DelegateApp02
{
    internal class Program
    {
        public delegate int Compute(int a, int b);
        static void Main(string[] args)
        {
            int x = 10;
            int y = 5;
            Compute plus = Plus;
            Compute minus = Minus;

            Console.WriteLine(plus(x, y));
            Console.WriteLine(minus(x, y));
        }
        public static int Plus(int a, int b)
        {
            return a + b;
        }
        public static int Minus(int a, int b)
        {
            return a - b;
        }
    }
}
---------------------------------------------------------
namespace DelegateApp02
{
    public class MathFuntion()
    {
        public int Plus(int a, int b)
        {
            return a + b;
        }
        public int Minus(int a, int b)
        {
            return a - b;
        }
        public int Multiple(int a, int b)
        {
            return a * b;
        }
        public double Divide(int a, int b)
        {
            return a / (double)b;
        }
    }
    internal class Program
    {
       // public delegate int Compute(int a, int b);

        static void Main(string[] args)
        {
            int x = 7;
            int y = 3;
            MathFuntion math = new MathFuntion();

            //Compute compute = math.Plus;
            Func<int, int, int> intCompute = math.Plus;
            //Console.WriteLine(compute(x, y));
            Console.WriteLine(intCompute(x, y));

            //compute = math.Minus;
            intCompute = math.Minus;
            //Console.WriteLine(compute(x, y));
            Console.WriteLine(intCompute(x, y));

            //compute = math.Multiple;
            Func<int, int, int> intCompute2 = math.Multiple;
            //Console.WriteLine(compute(x, y));
            Console.WriteLine(intCompute2(x, y));

            //compute = math.Divide;
            Func<int, int, double> intCompute3 = math.Divide;
            //Console.WriteLine(compute(x, y));
            Console.WriteLine(intCompute3(x, y));
        }
    }
}
-------------------------------------------------------------
using System.Text;

namespace WinFormsApp6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("당신의 연령은");
            if (radioButton1.Checked == true)
                sb.Append(radioButton1.Text);
            if (radioButton2.Checked == true)
                sb.Append(radioButton2.Text);
            if (radioButton3.Checked == true)
                sb.Append(radioButton3.Text);
            if (radioButton4.Checked == true)
                sb.Append(radioButton4.Text);
            sb.AppendLine();
            sb.AppendLine("좋아하는 색은");
            if (checkBox1.Checked == true)
                sb.Append(checkBox1.Text);
            if (checkBox2.Checked == true)
                sb.Append(checkBox2.Text);
            if (checkBox3.Checked == true)
                sb.Append(checkBox3.Text);
            if (checkBox4.Checked == true)
                sb.Append(checkBox4.Text);
            sb.AppendLine("입니다.");

            textBox1.Text = sb.ToString();
        }
    }
}
-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("장갑");
            listBox1.Items.Add("양말");
            listBox1.Items.Add("타월");
            listBox1.Items.Add("바지");
            listBox1.Items.Add("반팔티");
            listBox1.Items.Add("팬티");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
            textBox1.Text = "";
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add(listBox1.Text);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            listBox2.Items.Remove(listBox2.Text);
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Add("경상북도");
            listBox1.Items.Add("경상남도");
            listBox1.Items.Add("경상북도");
            listBox1.Items.Add("강원도");
            listBox1.Items.Add("서울특별시");
            listBox1.Items.Add("인천광역시");
            listBox1.Items.Add("부산광역시");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
            textBox1.Text = "";
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox2.Items.Add(listBox1.Text);
            listBox1.Items.Remove(listBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(listBox2.Text);
            listBox2.Items.Remove(listBox2.Text);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = int.Parse(textBox1.Text);
            int sum = 0;
            
            for (int i = 0; i <= n; i++)
            {
                sum += i;
            }
            textBox2.Text = sum.ToString();
        }
    }
}
-------------------------------------------------------------------

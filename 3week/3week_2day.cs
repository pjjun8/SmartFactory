static void Main(string[] args)
{
    static int CallByValueDemo(int x)
    {
        return x;
    }
    Console.WriteLine("정수를 입력하세요.");
    int inputNumber = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("입력하신 정수의 값은{0}입니다.", CallByValueDemo(inputNumber));
}
--------------------------------------------------
namespace MethodTest
{
    internal class Program
    {
        //static void GetNumber(out int x,out int y)
        //{
        //    x = 0;
        //    y = 0;
        //}
        static void Getvalue(out int x)
        {
            x = 1;
        }
        static void Main(string[] args)
        {
            //int a = int.Parse(Console.ReadLine());
            //int b = int.Parse(Console.ReadLine());

            //GetNumber(out a,out b);
            //Console.WriteLine(a);
            //Console.WriteLine(b);
            int a = 100;
            Getvalue(out a);
            Console.WriteLine(a);
        }
    }
}
--------------------------------------------------
namespace Recursive01
{
    internal class Program
    {
        static int Factorial(int n)
        {
            if (n == 1)
                return n;
            else
                return n * Factorial(n - 1);
        }
        static void Main(string[] args)
        {
            int a = 5;
            Console.WriteLine(Factorial(a));
        }
    }
}
-------------------------------------------------
namespace FactorialDynamic
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            //Dynamic Programming Factorial!!
            int n = 5;
            long[] arr = new long[n + 1];
            arr[0] = 1; // 이 코드가 핵심!!

            for(int i = 1; i <= n; i++)
            {
                arr[i] = i * arr[i - 1];
            }
            Console.WriteLine(arr[n]);
        }
    }
}
---------------------------------------------------
 namespace FactorialDynamic
{
    internal class Program
    {
        static long Recursive(long num)
        {
            //Console.WriteLine(num);
            if (num == 1)
                return num;
            else
                return num * Recursive(num - 1);
        }

        static void Main(string[] args)
        {
            long num = long.Parse(Console.ReadLine());
            int mul = 1;
            Console.WriteLine(Recursive(num));
            for (int i = 1; i < num + 1; i++)
            {
                mul *= i;
            }
            Console.WriteLine(mul);
        }
    }
}
----------------------------------------------------
namespace LocalVariableTest
{
    internal class Program
    {
        static string name = "이종선";
        static void ShowName()
        {
            Console.WriteLine(name);
        }
        static void Main(string[] args)
        {
            ShowName();
            Console.WriteLine(name);
        }
    }
}
-----------------------------------------------
using System.ComponentModel;
using System.Data.SqlTypes;

namespace Code68
{
    class Bank
    {
        private double backBook = 0;

        public void Deposit(double money)
        {
            backBook += money;
        }
        public double Withdraw(double money)
        {
            if(backBook > 0)
            {
                backBook -= money;
                if(backBook > 0)
                    return money;
                else
                {
                    backBook += money;
                    return 0;
                }
            }
            else return 0;
        }
        public void GetBackbook()
        {
            Console.WriteLine(backBook);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            bank.Deposit(1000);
            bank.GetBackbook();
            bank.Withdraw(500);
            bank.GetBackbook();
        }
    }
}
------------------------------------------------------
    namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text + "님 소프트웨어 공학과에 오신것을 환영합니다.";
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}


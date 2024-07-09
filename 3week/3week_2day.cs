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

namespace Quiz01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int value1 = Int32.Parse(Console.ReadLine());
            int value2 = Int32.Parse(Console.ReadLine());
            int value3;
            Console.WriteLine($"{value1}, {value2}");   //100 200
            value3 = value1;
            value1 = value2;
            value2 = value3;
            Console.WriteLine($"{value1}, {value2}");   //200 100
        }
    }
}
-----------------------------------------------------------------
namespace CToF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int cen = Int32.Parse(Console.ReadLine());
            double F = ((double)cen * 9 / 5) + 32;		// 정수--> 실수로 바꿔 주기

            Console.WriteLine(F);
        }
    }
}
------------------------------------------------------------------
using System;

namespace CToF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //반지름을 입력 받으면 원넓이를 구해주는 프로그램을 작성하세요.
            //단, 소수점 둘째자리까지만 출력하세요.
            //단, 원주율은 Math 클래스를 사용하세요
            double radius;
            Console.WriteLine($"반지름을 입력하세요 : ");
            radius = Int32.Parse(Console.ReadLine());

            double area = Math.PI * radius * radius;

            Console.WriteLine($"{area:F2}");
        }
    }
}

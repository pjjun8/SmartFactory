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
            Console.Write($"반지름을 입력하세요 : ");
            radius = Int32.Parse(Console.ReadLine());    // int32.Parse 중요

            double area = Math.PI * radius * radius;

            Console.WriteLine($"{area:F2}");    //:F2 는 소숫점 2자리까지 출력하게 해준다.
        }
    }
}
-----------------------------------------------------------------
namespace IfApp01
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int value = Int32.Parse(Console.ReadLine());  // 변수선언, 초기화

            if (value == 100)
            {
                Console.WriteLine("if로직 실행");
            }
            else if(value == 200)
            {
                Console.WriteLine("else if 로직 실행");
            }
            else
            {
                Console.WriteLine("else 로직 실행");
            }

                        Console.Write("수를 입력하세요 : ");
            double num = double.Parse(Console.ReadLine());

            if (num % 2 == 1)
            {
                Console.WriteLine("홀수입니다.");
            }
            else
            { 
                Console.WriteLine("짝수입니다.");
            }
        }
    }
}
---------------------------------------------------------------
int a = 5;
int b = 3;
int c = 4;

if((a + b + c > 10) && (a == b))
{
    Console.WriteLine("다 맞다.");
}
else if ((a + b + c > 10) || (a == b))
{
    Console.WriteLine("하나 맞음");
}
else{
    Console.WriteLine("님 틀림");
}
--------------------------------------------------------------
namespace FirstFor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1~100 까지 합을 구해보세요
            int sum = 0;
            for(int i = 0; i <= 100; i++)
            {
                sum += i;
            }
            Console.WriteLine(sum);
        }
    }
}

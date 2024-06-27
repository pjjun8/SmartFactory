namespace WhileApp
{
    internal class Program
    {
        // 1~100 까지 3의 배수와 7의 배수만 출력해 주세요
        // 2500 = 홀수, 2550 = 짝수
      
        static void Main(string[] args)
        {
            int sum1 = 0;
            int sum2 = 0;
          
            for (int j = 0; j <= 100; j++)
            {
                if (j % 3 == 0)
                {
                    Console.Write($"{j}, ");
                }
                else if (j % 7 == 0)
                {
                    Console.Write($"{j}, ");
                }
            }

            int i = 0;
            sum1 = 0;
            sum2 = 0;
            Console.WriteLine("");
          
            while (i <= 100)
            {
                if (i % 3 == 0)
                {
                    Console.Write($"{i}, ");
                }
                else if (i % 7 == 0)
                {
                    Console.Write($"{i}, ");
                }
                i++;
            }
        }
    }
}
---------------------------------------------------------------------------------
    //for문 100~1까지 거꾸로 출력해 보세요 --> 짝수만
    //while문 100~1까지 거꾸로 출력 --> 홀수만
    for (int i = 100; i >= 0; i--)
    {
        if (i % 2 == 0)
        {
            Console.Write($"{i}, ");
        }
    }
    Console.WriteLine();

    int j = 100; 

    while (j >= 0)
    {
        if (j % 2 == 1)
        {
            Console.Write($"{j}, ");
        }
        j--;
    }
--------------------------------------------------------------------------------
 Console.Write("성적을 입력하세요: ");
 int score = int.Parse(Console.ReadLine());

 if(100 == score || 90 <= score)
 {
     Console.WriteLine("A학점");
 }
else if (90 > score && 80 <= score)
 {
     Console.WriteLine("B학점");
 }
else if (80 > score && 70 <= score)
 {
     Console.WriteLine("C학점");
 }
else if (70 > score && 60 <= score)
 {
     Console.WriteLine("D학점");
 }
 else
 {
     Console.WriteLine("F학점");
 }
------------------------------------------------------------------------------
while (true)
{
    Console.Write("첫 번째 숫자를 입력하세요: ");
    double num1 = double.Parse(Console.ReadLine());

    Console.Write("연산자를 입력하세요: ");
    string op = Console.ReadLine();

    Console.Write("두 번째 숫자를 입력하세요: ");
    double num2 = double.Parse(Console.ReadLine());

    switch (op)
    {

        case "+":
            Console.WriteLine($"결과는 {num1 + num2}입니다.");
            break;
        case "-":
            Console.WriteLine($"{num1 - num2}");
            break;
        case "*":
            Console.WriteLine($"{num1 * num2}");
            break;
        case "/":
            Console.WriteLine($"{num1 / num2}");
            break;
        default:
            Console.WriteLine("오류");
            break;
    }
    Console.Write("계산을 계속하시겠습니까? (y/n): ");
    string ox = Console.ReadLine().ToLower();
    if (ox == "n")
    {
        Console.Write("계산 종료");
        break;
    }
    else if (ox == "y")
    {
        Console.WriteLine("계산 다시");
    }
    else
    {
        Console.WriteLine("비정상적 입력");
        break;
    }
}
-------------------------------------------------------------------------
namespace GuGuDan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //9단부터 2단까지 출력!
            // 9*9 = 81부터 2*1 = 2까지
            for (int i = 9; i > 1; i--)
            {
                for(int j = 9; j > 0; j--)
                {
                    Console.WriteLine($"{i} * {j} = {i * j}");
                }
            } 
        }
    }
}
--------------------------------------------------------------------------

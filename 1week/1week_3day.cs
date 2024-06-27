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

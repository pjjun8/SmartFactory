 char ch = 'A';
 Console.WriteLine(ch);

 char[] arr = new char[3];
 arr[0] = 'a';
 arr[1] = 'b';
 arr[2] = 'c';

 Console.WriteLine($"{arr[0]}{arr[1]}{arr[2]}");

 for (int i = 0; i < 3; i++)
 { 
     Console.Write(arr[i]);
 }
---------------------------------------------------------------------------
           // 크기가 5인 정수형 배열 arrint를 선언하고
           // 값은 10 20 30 40 50 을 입력해 보자.

            int[] arrint = new int[5];

            for (int i = 0; i < arrint.Length; i++)
            {
                arrint[i] = (i + 1) * 10;    
                Console.WriteLine(arrint[i]);
            }
----------------------------------------------------------------------------
  // 크기가 3인 문자 배열 arr를 만드세요.
  // 값은 'Z', 'Y', 'X'
  // 화면에 요소값을 index 순서대로 출력해 보세요.

  char[] arr = new char[26];
  char ch = 'Z';

  for (int i = 0; i < arr.Length; i++)
  {
      arr[i] = ch--;
      Console.WriteLine(arr[i]);
  }
----------------------------------------------------------------------------

           // 정수형 배열 score를 만들고 순서대로 국어, 영어, 수학 성적을 입력받아 총점과 평균을 출력하세요.

            int[] score = new int[3];
            double total = 0;
            double avg = 0;
            
            for (int i = 0; i < score.Length; i++)
            {
                score[i] = Int32.Parse(Console.ReadLine());
                total += score[i];
                avg = total / score.Length;
            }

            Console.WriteLine($"총점:{total}, 평균: {avg:F2}");
----------------------------------------------------------------------------
 int[] numbers = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

for (int i = 0; i < numbers.Length; i++)
{ 
    if(numbers[i] % 2 == 0)
    {
        Console.WriteLine(numbers[i]);
    }
}

for (int i = 0; i < numbers.Length; i++)
{
    Console.WriteLine(numbers[i]);
}
-------------------------------------------------------------------------------
             string str = Console.ReadLine();
            string outText = "";
            for (int i = str.Length -1; i >= 0; i--)
            { 
                outText += str[i];
            }
            Console.WriteLine(outText);
--------------------------------------------------------------------------------
             // 1. 크기 100인 정수형 배열을 만들고 1~100 까지 초기화 하세요.
            // 2. 3의 배수는 배열의 요소값을 이용해서 콘솔 화면에 출력
            // 3. 7의 배수는 index 값을 이용해서 출력해 보세요.
            int[] arr = new int[100];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i + 1;               
                
            }

            for (int i = 0;i < arr.Length; i++)
            {
                if (arr[i] % 3 == 0)
                    Console.Write($"{arr[i]} ");
            }
------------------------------------------------------------------------------------
             int[,] arr = new int[3, 3];
            int cnt = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    arr[i, j] = cnt++;
                    Console.Write($"{arr[i,j]} ");
                }
                Console.WriteLine(" ");
            }
------------------------------------------------------------------------------------
class product
{
    static void NamePrint()
    {
        Console.WriteLine("라면 입니다.");
    }
    static string NamePrint2()
    {
        return "라면 입니다2.";
    }
    public void GetNamePrint()
    {
        NamePrint();
    }
    public void GetNamePrint2()
    {
        Console.WriteLine(NamePrint2());
    }
}

static void Main()
{
    product pd = new product();
    pd.GetNamePrint();
    pd.GetNamePrint2();
    
}
--------------------------------------------------------------------------------
static int Plus(int a, int b)
{
   return a + b;
}
static void Main()
{
    int v1 = 100;
    int v2 = 200;
    int result = v1 + v2;
    Console.WriteLine(Plus(v1, v2));
    Console.WriteLine(result);
}
--------------------------------------------------------------------------------
 class Calculator
{
    public Calculator() // 생성자
    {

    }
    public Calculator(string name)
    {

    }
    public int Multiple(int a, int b)
    {
        return a * b;
    }
    public double Divide(int a, double b)
    {
        return (double)a / b;
    }
}
static void Main(string[] args)
{
    Calculator cal = new Calculator("계산기");

    Console.WriteLine(cal.Multiple(5, 6));
    Console.WriteLine(cal.Divide(100, 5));
}
-------------------------------------------------------------------------------
         static void PrintInfo(string name, int age)
        {
            Console.WriteLine($"{name}은{age}살 입니다.");
        }
        static int ThreePlus(int a, int b, int c)
        { 
            return a + b + c;
        }
        double Plus(double a, double b)
        {
            return a + b;
        }
        static void Main(string[] args)
        {
            PrintInfo("홍길동", 20); // static 으로 선언된 함수는 객체를 생성 하지 않아도 사용가능
            PrintInfo("이순신", 30);
            Console.WriteLine(ThreePlus(1, 2, 3));

            Program p = new Program();
            Console.WriteLine(p.Plus(3.14, 9.88));
        }
-------------------------------------------------------------------------------
 using System.Dynamic;
using static System.Formats.Asn1.AsnWriter;

namespace Quiz06
{
    internal class Program
    {
        //국어, 영어 ,수학 성적을 입력받아 총점을 구해 출력하는 프로그램 메소드로 만들어봥
        static int[] Input()
        {
            int[] score = new int[3];
            Console.WriteLine("국어, 영어, 수학 성적을 입력하세요: ");

            for (int i = 0; i < score.Length; i++)
            {
                score[i] = Int32.Parse(Console.ReadLine());
            }
            return score;
        }
        static int Output(int []a)
        {
            int []score = a;
            int total = 0;
            for (int i = 0;i < score.Length;i++)
            {
                total += score[i];
            }
            return total ;
        }
        static void Main(string[] args)
        {
            int[] score = Input();
            int total = Output(score);
            Console.WriteLine(total);
        }
    }
}
-----------------------------------------------------------------------------------
using System.Dynamic;
using static System.Formats.Asn1.AsnWriter;

namespace Quiz06
{
    internal class Program
    {
        //국어, 영어 ,수학 성적을 입력받아 총점을 구해 출력하는 프로그램 메소드로 만들어봥
        static int[] Input()
        {
            int[] score = new int[3];
            Console.WriteLine("국어, 영어, 수학 성적을 입력하세요: ");

            for (int i = 0; i < score.Length; i++)
            {
                score[i] = Int32.Parse(Console.ReadLine());
            }
            return score;
        }
        static int Output(int[] a)
        {
            int[] score = a;
            int total = 0;
            for (int i = 0; i < score.Length; i++)
            {
                total += score[i];
            }
            return total;
        }
        static double Output1(int[] a)
        {
            int[] score = a;
            double avg = 0;
            for (int i = 0; i < score.Length; i++)
            {
                avg += score[i] / (score.Length * 1.0);
                Console.WriteLine(avg);
            }
            return avg;
        }
        static void Main(string[] args)
        {
            int[] score = Input();
            int total = Output(score);
            double avg = Output1(score);
            Console.WriteLine(total);
            Console.WriteLine($"{avg:F2}");
        }
    }
}

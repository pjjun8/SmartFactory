namespace Exam03
{
    class Sum
    {
        static int NumberAdd(int n)
        {
            int sum = 0;
            for (int i = 0; i <= n; i++)
            {
                sum += i;
                Console.WriteLine(sum);
            }
            return sum; 
        }
        public int Get(int n)
        { 
            return NumberAdd(n);
        }
    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Sum sum = new Sum();
            Console.WriteLine(sum.Get(100));
        }
    }
}
-----------------------------------------------------------------------------
namespace Exam04
{
    //max를 구하는 메소드 GetMax(int[] arr) 를 만들어 주세요.
    internal class Program
    {//배열 요소값 중 가장 큰 값을 max로 대입 후 출력하라!!!
        static int GetMax(int[] arr)
        {
            int max = int.MinValue;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > max)
                {
                    max = arr[i];
                }
            }
            return max;
        }
        //배열 요소값 중 가장 작은 값을 min로 대입 후 출력하라!!!
        static int GetMin(int[] arr)
        {
            int min = int.MaxValue;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                }
            }
            return min;
        }
        static void Main(string[] args)
        {
            int[] arr = { -7, 5, 60, -33, 43 };

            Console.WriteLine($"최대 값은: {GetMax(arr)}");
            Console.WriteLine($"최소 값은: {GetMin(arr)}");
        }
    }
}
--------------------------------------------------------------------------
namespace ScoreApp02
{
    internal class Program
    {
        //성적 입력 함수를 만들어 주세요. 3과목
        static int[] InputThreeScore()
        {
            int[] score = new int[3];
            for (int i = 0; i < score.Length; i++)
            {
                score[i] = int.Parse(Console.ReadLine());   
            }
            return score;
        }
        // 국어, 영어, 수학 성적을 합하는 코딩 
        static int TotalScore(int[] score)
        {
            int totalscore = 0;
            foreach (int i in score)
            {
                totalscore += i;
            }
            return totalscore;
        }
        // 총점에서 평균값을 만드는 코딩
        static double GetAvg(int totalScore, int[] score)
        {
            return totalScore / (double)score.Length;
        }
        static void Main(string[] args)
        {
            //1. 세 성적 입력받기
            int[] score = InputThreeScore();
            //2. 총점 구하기
            Console.WriteLine($"total : {TotalScore(score)}");
            //3. 평균 구하기
            Console.WriteLine($"avg : {GetAvg(TotalScore(score), score):F2}");
        }
    }
}

-----------------------------------------------------------------------
namespace OOP01
{
    class Book
    {
        string Title;
        decimal ISBN13;
        string Contants;
        string Author;
        int PageCount;
    }
    class Student
    {
        public int ID;
        public string Name;
        public string Run()
        {
            return "학번 : "+ this.ID +" " + this.Name +" 달리다";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Book guliver = new Book();
            Student hong = new Student();
            hong.ID = 1;
            hong.Name = "홍길동";
            Console.WriteLine(hong.ID);
            Console.WriteLine(hong.Name);
            Console.WriteLine(hong.Run());
        }
    }
}
------------------------------------------------------------------------
class Mathmatics
{
    /*멤버 변수
     * 생성자
     * 멤버 메소드*/
    public int f(int x)
    {
        return x * x;
    }
}
internal class Program
{
    static void Main(string[] args)
    {
        Mathmatics m = new Mathmatics();
        Console.WriteLine(m.f(5));
    }
}
-------------------------------------------------------------------------
namespace ConsoleApp4
{
    class Person
    {
        public string name;
        
        public Person()
        {
            name = "이름없음";
            Console.WriteLine("생성자 호출");
        }
        public Person(string name)
        {
            this.name = name;
            Console.WriteLine($"인자가 1개 있는 생성자 호출 : {this.name}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person("홍길동");
        }
    }
}
-----------------------------------------------------------------------
namespace SwapByValue
{
    internal class Program
    {
        static void Swap(ref int a,ref int b)
        {
            int temp = b;
            b = a;
            a = temp;
            Console.WriteLine($"{a}, {b}");
        }
  
        static void Main(string[] args)
        {
            int x = 3, y = 4;
            Console.WriteLine($"{x}, {y}");
            Swap(ref x,ref y);
            Console.WriteLine($"{x}, {y}");
           
        }
    }
}

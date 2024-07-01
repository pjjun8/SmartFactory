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

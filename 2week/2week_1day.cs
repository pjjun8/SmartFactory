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

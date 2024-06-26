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

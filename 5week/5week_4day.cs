using System.Linq.Expressions;

namespace ConsoleApp18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 2, 3};

            try
            {
                //int a = 5;
                //int b = 0;
                //int result = a / b;
                throw new IndexOutOfRangeException();

                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(arr[i]);
                }
            
            }
            catch(IndexOutOfRangeException ex)
            {
                Console.WriteLine("배열의 범위를 벗어났습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("부모 예외클래스에서 잡혔습니다.");
            }
            finally
            {
                Console.WriteLine("무조건 실행 됩니다.");
            }
        }
    }
}
------------------------------------------------------------

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
namespace FileTest01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "c:\\Temp\\abc.txt";
            string content = "안녕!";

            //쓰기
            File.WriteAllText(path, content);
            Console.WriteLine("파일 쓰기 성공");

            //읽기
            string path2 = "c:\\Temp\\ccc.txt";
            try
            {
                string words = File.ReadAllText(path2);
                Console.WriteLine(words);
            }
            catch (Exception ex)
            {
                Console.WriteLine("파일의 이름이 잘못되었습니다.");
            }
        }
    }
}
------------------------------------------------------------
namespace FileTest02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"c:\Temp\hello.txt";
            string content = "안녕하세요 반갑습니다.";

            using (StreamWriter writer = new StreamWriter(path))//using 있으면 Close() 안해줘도됨
            {
                writer.WriteLine(content);

                //writer.Close(); // 이게 있어야 진짜서 글이 써짐
            }
            Console.WriteLine("현재 프로그램을 종료합니다.");
        }
    }
}

namespace FileTest03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //File.WriteAllText("C:\\Temp\\abc.txt", "1234");
            string path = @"C:\Temp\abc.txt";
            string content = "Hello World~!";
            File.WriteAllText(path, content);
           // byte[] bytes = new byte[3] { 1, 2, 3 };
           // File.WriteAllBytes(path, bytes);

            //string a = File.ReadAllText(path);
            //Console.WriteLine(a);

            FileInfo fileInfo = new FileInfo(path);

            using (StreamWriter sw = fileInfo.CreateText())
            {
                sw.WriteLine("안녕하세요, 반갑습니다");
                sw.WriteLine("오늘 저녁 뭐드실 건가요?");
                sw.Close();
            }
        }
    }
}
-----------------------------------------------------------------
namespace FileExam02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Temp\ccc.txt";

            FileInfo fi = new FileInfo(path);
            //쓰기
            using (StreamWriter sw = fi.CreateText())
            {
                sw.WriteLine("hello mother fucker");
            }

            //읽기
            using (StreamReader sr = fi.OpenText())
            {
                var s = "";
                while( (s = sr.ReadLine()) != null )
                {
                    Console.WriteLine(s);
                }
            }
        }
    }
}
-----------------------------------------------------------------
using System.Text;

namespace FileExam05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Temp\test.log";
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine("aaaaaaaaaa");
                    sw.WriteLine("bbbbbbbbbb");
                    sw.WriteLine(230000000);
                }
            }
        }
    }
}
-------------------------------------------------------------------
namespace FileExam05_BasicIo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            long someValue = 0x123456789ABCDEF0;
            //Console.WriteLine($"{someValue:X16}");

            string path = @"C:\Temp\a.dat";
            Stream outstream = new FileStream(path, FileMode.Create);
            byte[] wBytes = BitConverter.GetBytes( someValue );
            //회면에 byte 출력??
            //foreach( byte b in wBytes )
            //{
            //    Console.Write($"{b:X16}");
            //}
            outstream.Write(wBytes, 0, wBytes.Length );
            outstream.Close();

            Stream inStream = new FileStream(path, FileMode.Open);
            byte[] rbytes = new byte[8];

            int i = 0;
            while (inStream.Position < inStream.Length)
            {
                rbytes[i++] = (byte)inStream.ReadByte();
            }
            Console.WriteLine();
            long readValus = BitConverter.ToInt64(rbytes, 0);
            Console.WriteLine($"{readValus} ");
        }
    }
}

-----------------------------------------------------------------
namespace MyCopy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //args[0] abc.txt
            //args[1] cba.txt
            string add = $@"C:\Temp\{args[0]}";
            string mal = "Hello World~!";
            string copy = $@"C:\Temp\{args[1]}";
            FileStream fs = new FileStream(add, FileMode.Create);

            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(mal);
            }
            try
            {
                File.Copy(add, copy, true);
                Console.WriteLine("복사 성공");
            }
            catch
            {
                Console.WriteLine("do not");
            }
        }
    }
}
------------------------------------------------------------------
namespace Thread01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(threadFunc1);
            Thread t2 = new Thread(threadFunc2);

            t1.Start();
            t2.Start();
        }
        static void threadFunc1()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
            }
        }
        static void threadFunc2()
        {
            char c1 = 'A', c2 = 'a';
            for (int i = 1; i < 26; i++)
                Console.WriteLine((char)c1++);
            for (int j = 1; j < 26; j++)
                Console.WriteLine((char)c2++);

        }
    }
}
-------------------------------------------------------------------
  //쓰레드 설명 잘해논 예제
  namespace Thread02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(threadFunc);
            t.IsBackground = false; //백그라운드로 만들면 main 죽으면 같이 죽음 
            t.Start(); // 백그라운드 안주면 main 종료돼도 계속 실행됨
            t.Join(); // main 함수가 기다려줌
            Thread.CurrentThread.Name = "사장님 스레드";
            string mtName = Thread.CurrentThread.Name;
            Console.WriteLine($"{mtName} 프로그램 종료");

        }
        static void threadFunc()
        {
            Console.WriteLine("3초 후에 프로그램 종료");
            Thread.Sleep(3000);

            Thread.CurrentThread.Name = "개발부장 스레드";
            string NtName = Thread.CurrentThread.Name;
            Console.WriteLine($"{NtName} 프로그램 종료");
        }
    }
}
--------------------------------------------------------------------------
namespace ConsoleApp22
{
    internal class Program
    {

        static void DataWrite()
        {
            string add = @"c:\temp\data.txt";
            string txt = "파일처리 / 스레드 프로그램은 재미있다~!";
            File.WriteAllText(add, txt);
            //Console.WriteLine("DataWrite");
        }
        static void DataRead()
        {
            Thread.Sleep(1000);
            string add = @"C:\Temp\data.txt";
            string zzz = File.ReadAllText(add);
            Console.WriteLine(zzz);

        }
        static void Main(string[] args)
        {
            Thread writeThread = new Thread(DataWrite);
            Thread readThread = new Thread(DataRead);
            writeThread.IsBackground = true;
            readThread.IsBackground = true;
            writeThread.Start();   
            readThread.Start();
            writeThread.Join();
            readThread.Join();
        }
    }
}

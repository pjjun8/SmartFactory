namespace BitConverterTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] boolBytes = BitConverter.GetBytes(true);
            byte[] shortBytes = BitConverter.GetBytes((short)32000); //직렬화 byte 배열로 변환
            byte[] intBytes = BitConverter.GetBytes(1652300);

            bool boolResult = BitConverter.ToBoolean(boolBytes, 0); //역직렬화
            short shortResult = BitConverter.ToInt16(shortBytes, 0);
            int intResult = BitConverter.ToInt32(intBytes, 0);
            Console.WriteLine(boolResult + " " + " " + shortResult+ " " + " " +intResult);
        }
    }
}
====================================================================
using System.Text;

namespace BitConverterTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //byte[] boolBytes = BitConverter.GetBytes(true);
            //byte[] shortBytes = BitConverter.GetBytes((short)32000); //직렬화 byte 배열로 변환
            //byte[] intBytes = BitConverter.GetBytes(1652300);

            //bool boolResult = BitConverter.ToBoolean(boolBytes, 0); //역직렬화
            //short shortResult = BitConverter.ToInt16(shortBytes, 0);
            //int intResult = BitConverter.ToInt32(intBytes, 0);
            //Console.WriteLine(boolResult + " " + " " + shortResult+ " " + " " +intResult);
            StreamReader streamReader = new StreamReader(@"C:\Temp\abc.txt", Encoding.UTF8);
            string txt = streamReader.ReadToEnd();
            //Console.WriteLine(s);
            MemoryStream memoryStream = new MemoryStream();
            byte[] strBytes = Encoding.UTF8.GetBytes(txt);  //문자열 직렬화 - byte 배열로 만들었다!!!
            memoryStream.Write(strBytes, 0, strBytes.Length);
            memoryStream.Position = 0;
            
            StreamReader sr = new StreamReader(memoryStream, Encoding.UTF8, true);
            txt = sr.ReadToEnd();
            Console.WriteLine(txt);


        }
    }
}
====================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadExpress
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(new ThreadStart(Run));
            t1.Start();

            Thread t2 = new Thread(Run);
            t2.Start();

            Thread t3 = new Thread(delegate ()
            {
                Run();
            });
            t3.Start();

            Thread t4 = new Thread( () => Run() );
            t4.Start();

            //익명
            new Thread(() => Run()).Start();
        }
        
        static void Run()
        {
            Console.WriteLine("Run 메소드가 동작합니다.");
        }
    }
}
=======================================================================
namespace WorkThread01
{
    internal class Program
    {
        //스레드 동기화
        private static readonly object lockObject = new object();

        static void Main(string[] args)
        {
            //Main Thread --> 사장님!!!
            int threadCount = 5; //일꾼 5명 불러봅니다.

            Thread[] threads = new Thread[threadCount];

            for (int i = 0; i < threads.Length; i++)
            {
                int threadindex = i;
                threads[i] = new Thread(() => DoWork(threadindex));
                threads[i].Start();
            }
            foreach(Thread thread in threads)
                thread.Join();

            Console.WriteLine("모든 작업이 완료되었습니다.");
        }
        static void DoWork(int index)
        {
            lock (lockObject)
            {
                Console.WriteLine($"스레드 {index} 시작 : 작업을 수행 중...");
                Thread.Sleep(1000);
                Console.WriteLine($"스레드 {index} 종료 : 작업이 끝났습니다.");
            }
        }
    }
}
==============================================================================
namespace BinaryReaderWriter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Temp\pic1.png";
            byte[] picture;
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                BinaryReader br = new BinaryReader(fs);
                picture = br.ReadBytes((int)fs.Length);   //직렬화되어있는 값으로 읽어옮
                br.Close();
            }//사진 파일 읽어오기 --------> 메모리로 가져왔음 (byte[] picture)

            //pic2.png로 Write 해 봅시다.
            string path2 = @"C:\Temp\pic2.png";
            using(FileStream fs = new FileStream(path2, FileMode.Create))
            {
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(picture);
                bw.Flush(); //버퍼 내부 강제로 비우는 거
                bw.Close();
            }
        }
    }
}
===============================================================================
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MultiTreadServerV1
{
    internal class Program
    {
        static int cnt = 0;
        static void Main(string[] args)
        {
            Thread serverThread = new Thread(serverFunc);
            serverThread.IsBackground = true;
            serverThread.Start();

            serverThread.Join();
            Console.WriteLine("서버 프로그램을 종료합니다!!");
        }
        private static void serverFunc(object obj)
        {
            using(Socket srvSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                IPEndPoint server = new IPEndPoint(IPAddress.Any, 13000);
                srvSocket.Bind(server);
                srvSocket.Listen(50);
                Console.WriteLine("서버 시작...");
                while(true)
                {
                    Socket clieSocket = srvSocket.Accept();
                    cnt++;
                    Thread workThread = new Thread(new ParameterizedThreadStart(workFunc));
                    workThread.Start(clieSocket);
                }
            }
        }
        private static void workFunc(object obj)
        {
            byte[] recvBytes = new byte[2048];
            Socket cliSocket = (Socket)obj;
            int nRecv = cliSocket.Receive(recvBytes);

            string txt = Encoding.UTF8.GetString(recvBytes, 0, nRecv);
            Console.WriteLine($"클라이언트 번호 ({cnt}) : {txt}");
            byte[] sendByte = Encoding.UTF8.GetBytes(txt);
            cliSocket.Send(sendByte);
            cliSocket.Close();
            
        }
    }
}
=================================================================================================
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace TestClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread clientThread = new Thread(clientFunc);
            clientThread.Start();
            clientThread.IsBackground = true;
            clientThread.Join();

            Console.WriteLine("클라이언트가 종료 되었습니다.");
        }
        static void clientFunc(object obj)
        {
            try
            {
                //1.소켓만들기
                Socket socket = new Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream,
                                        ProtocolType.Tcp);
                //2.연결
                //EndPoint serverEP = new IPEndPoint(IPAddress.Loopback, 10000);
                EndPoint serverEP = new IPEndPoint(IPAddress.Parse("192.168.81.71"), 13000);
                socket.Connect(serverEP);
                //3.Read, Write
                //write
                //byte[] buffer = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
                byte[] buffer = Encoding.UTF8.GetBytes("마이크 테스트");
                socket.Send(buffer);

                //read
                byte[] recvBytes = new byte[1024];
                int nRecv = socket.Receive(recvBytes);

                string txt = Encoding.UTF8.GetString(recvBytes, 0, nRecv);
                Console.WriteLine(txt);
                //4.종료
                socket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
=================================================================================================

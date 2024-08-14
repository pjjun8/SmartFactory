using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimplePPTServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TCPListener 클래스를 이용해서 작업 -- 서버 만들기
            //1. Server 만들고 Binding
            //1-1 IP 만들기
            IPAddress localAddr = IPAddress.Parse("192.168.0.20");
            //1-2 Port 만들기
            int port = 13000;

            TcpListener server = new TcpListener(localAddr, port);
            server.Start(); //서버시작
            Console.WriteLine("연결을 기다리는 중...");

            //2.Listener, Accepting
            using (TcpClient client = server.AcceptTcpClient())
            {
                Console.WriteLine("연결성공!");
                //3.Write (서버 ---> 클라이언트 메시지 전달)
                using(NetworkStream stream = client.GetStream())
                {
                    string message = "Hello world!";
                    byte[] data = Encoding.UTF8.GetBytes(message);
                
                    stream.Write(data, 0, data.Length); //네트워크로 클라이언트에게 메시지 전송
                    Console.WriteLine($"전달한 메시지 : {message}");
                }
            }
            server.Stop();
        }
    }
}
=====================================================================================
using System.Net.Sockets;
using System.Text;

namespace SimpleTCPClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string server = "192.168.0.20";
            int port = 13000;

            //1. 서버로 접속할 클라이언트 소켓 만들기
            // 성공시 접속됨 COnnect
            using (TcpClient client = new TcpClient(server, port))
            {
                //2. 메시지 받기
                NetworkStream stream = client.GetStream();
                byte[] data = new byte[256];
                int bytes = stream.Read(data, 0, data.Length);
                string responseData = Encoding.UTF8.GetString(data, 0, bytes);
                Console.WriteLine($"받은 메시지 : {responseData}");
            }
        }
    }
}

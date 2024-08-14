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
===================================================================================
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketTCPServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. SetverSocket 만들기
            IPAddress loacalAddr = IPAddress.Parse("127.0.0.1");
            int port = 13000;
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //2. Bind
            serverSocket.Bind(new IPEndPoint(loacalAddr, port));
            //3. Listen
            serverSocket.Listen(1);
            Console.WriteLine("연결을 기다리는 중...");
            //4. Accept
            Socket clientSocket = serverSocket.Accept();
            Console.WriteLine("연결 성공!!!");
            //5. Read/Write
            string message = "안녕하세요. 클라이언트님!";
            byte[] bytes = new byte[1024];
            byte[] data = Encoding.UTF8.GetBytes(message);
            clientSocket.Send(data);
            Console.WriteLine($"전송한 메시지 : {message}");
            //6. Close
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
    }
}

=====================================================================================
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketTCPClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Client소캣 만들기, 서버접속을 위한 소켓만들기
            IPAddress serverAddr = IPAddress.Parse("127.0.0.1"); //친구서버 주소
            int port = 13000;
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //2. Connect
            clientSocket.Connect(new IPEndPoint(serverAddr, port));
            Console.WriteLine("서버에 연결되었습니다.");
            //3. Read,Write
            byte[] bytes = new byte[1024];
            int byteReceived = clientSocket.Receive(bytes);

            //받은메시지 출력
            string message = Encoding.UTF8.GetString(bytes);
            Console.WriteLine("서버로 부터 받은 내용 : " + message);
            //4. Close
            clientSocket.Close();
        }
    }
}


using System.Net.Sockets;
using System.Net;
using System.Text;

namespace TCPListenerTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 서버 소켓 생성
            TcpListener server = null;
            try
            {
                int port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // 서버 소켓 초기화
                server = new TcpListener(localAddr, port);

                // 서버 시작
                server.Start();
                Console.WriteLine("서버가 시작되었습니다. 클라이언트를 기다리는 중...");

                // 클라이언트의 연결을 기다림
                using (TcpClient client = server.AcceptTcpClient())
                {
                    Console.WriteLine("클라이언트가 연결되었습니다.");

                    // 네트워크 스트림을 통해 데이터를 주고받음
                    using (NetworkStream stream = client.GetStream())
                    {
                        // 클라이언트로부터 데이터를 읽음
                        byte[] buffer = new byte[256];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine("클라이언트로부터 받은 메시지: " + receivedMessage);

                        // 클라이언트에게 메시지 전송
                        string responseMessage = "메시지를 받았습니다.";
                        byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
                        stream.Write(responseData, 0, responseData.Length);
                        Console.WriteLine("클라이언트에게 응답을 전송했습니다.");
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("소켓 예외: " + e.ToString());
            }
            finally
            {
                // 서버 소켓을 종료
                if (server != null)
                {
                    server.Stop();
                }
            }

            Console.WriteLine("서버를 종료합니다.");
        }
    }
}
==============================================================================
using System.Net.Sockets;
using System.Text;

namespace TCPListenerTestClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string server = "127.0.0.1";
            int port = 13000;

            TcpClient client = new TcpClient(server, port);

            NetworkStream stream = client.GetStream();

            string sendMessage = "좋은 아침입니다.";
            byte[] sendeData = Encoding.UTF8.GetBytes(sendMessage);
            stream.Write(sendeData, 0, sendeData.Length);
            Console.WriteLine($"서버에게 {sendMessage}을 전송했습니다.");

            byte[] data = new byte[256];
            int bytes = stream.Read(data, 0, data.Length);
            string responseData = Encoding.UTF8.GetString(data, 0, bytes);
            Console.WriteLine($"Received: {responseData}");


            
        }
    }
}
============================================================================
    //[조별과제][퀴즈] 서버/클라이언트 만들기
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EchoServer
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
            using (Socket srvSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                IPEndPoint server = new IPEndPoint(IPAddress.Any, 13000);
                srvSocket.Bind(server);
                srvSocket.Listen(50);
                Console.WriteLine("서버 시작...");
                while (true)
                {
                    Socket clieSocket = srvSocket.Accept();
                    Thread workThread = new Thread(new ParameterizedThreadStart(workFunc));
                    workThread.Start(clieSocket);
                }
            }
        }
        private static void workFunc(object obj)
        {
            while (true)
            {
                Socket cliSocket = (Socket)obj;
                string quiz;
                
                cnt++;
                switch (cnt)
                {
                    case 1:
                        quiz = "문제 1: C#의 창시자는?\r\n\r\n1. Anders Hejlsberg\r\n\r\n2. James Gosling\r\n\r\n3. Bjarne Stroustrup\r\n\r\n정답을 입력하세요 (1, 2, 3):";
                        byte[] sendByte1 = Encoding.UTF8.GetBytes(quiz);
                        cliSocket.Send(sendByte1);
                        break;
                    case 2:
                        quiz = "문제 2: HTTP의 기본 포트 번호는?\r\n\r\n1. 21\r\n\r\n2. 80\r\n\r\n3. 443\r\n\r\n정답을 입력하세요 (1, 2, 3):";
                        byte[] sendByte2 = Encoding.UTF8.GetBytes(quiz);
                        cliSocket.Send(sendByte2);
                        break;
                    case 3:
                        quiz = "문제 3: OOP에서 상속을 제공하는 키워드는?\r\n\r\n1. class\r\n\r\n2. interface\r\n\r\n3. extends\r\n\r\n정답을 입력하세요 (1, 2, 3):";
                        byte[] sendByte3 = Encoding.UTF8.GetBytes(quiz);
                        cliSocket.Send(sendByte3);
                        break;
                }

                byte[] recvBytes = new byte[2048];
                int nRecv = cliSocket.Receive(recvBytes);
                string txt = Encoding.UTF8.GetString(recvBytes, 0, nRecv);
                string ox;
                switch (cnt)
                {
                    case 1:
                        if(txt == "1")
                        {
                            ox = "정답입니다! 다음 문제를 보려면 엔터눌러주세요!";
                            Console.WriteLine($"{cnt}번답 {ox}");
                            byte[] sendByte4 = Encoding.UTF8.GetBytes(ox);
                            cliSocket.Send(sendByte4);
                        }
                        else
                        {
                            ox = "오답입니다. 다음 기회에 도전하세요.";
                            Console.WriteLine($"{cnt}번답 {ox}");
                            byte[] sendByte4 = Encoding.UTF8.GetBytes(ox);
                            cliSocket.Send(sendByte4);
                            cnt = 4;
                        }
                        break;
                    case 2:
                        if (txt == "2")
                        {
                            ox = "정답입니다! 다음 문제를 보려면 엔터눌러주세요!";
                            Console.WriteLine($"{cnt}번답 {ox}");
                            byte[] sendByte5 = Encoding.UTF8.GetBytes(ox);
                            cliSocket.Send(sendByte5);
                        }
                        else
                        {
                            ox = "오답입니다. 다음 기회에 도전하세요.";
                            Console.WriteLine($"{cnt}번답 {ox}");
                            byte[] sendByte4 = Encoding.UTF8.GetBytes(ox);
                            cliSocket.Send(sendByte4);
                            cnt = 4;
                        }
                        break;
                    case 3:
                        if (txt == "3")
                        {
                            ox = "정답입니다! 다음 문제를 보려면 엔터눌러주세요!";
                            string lastOx = "역시 유다은! 안동의 자랑! 안동의 미래!!!";
                            Console.WriteLine($"{cnt}번답 {ox}");
                            byte[] sendByte6 = Encoding.UTF8.GetBytes(lastOx);
                            cliSocket.Send(sendByte6);
                            
                        }
                        else
                        {
                            ox = "오답입니다. 다음 기회에 도전하세요.";
                            Console.WriteLine($"{cnt}번답 {ox}");
                            byte[] sendByte4 = Encoding.UTF8.GetBytes(ox);
                            cliSocket.Send(sendByte4);
                            cnt = 4;
                        }
                        break;
                }
                //Console.WriteLine($"클라이언트 번호 ({cnt}) : {txt}");
                //cliSocket.Close();
            }
        }
    }
}
===========================================================================
    //[조별과제][퀴즈] 서버/클라이언트 만들기
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EchoClient
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
                EndPoint serverEP = new IPEndPoint(IPAddress.Parse("192.168.0.20"), 13000);
                socket.Connect(serverEP);
                //3.Read, Write
                //write
                //byte[] buffer = Encoding.UTF8.GetBytes(DateTime.Now.ToString());
                while (true)
                {
                    //read
                    byte[] recvBytes = new byte[1024];
                    int nRecv = socket.Receive(recvBytes);

                    string txt = Encoding.UTF8.GetString(recvBytes, 0, nRecv);
                    Console.WriteLine($"서버로 부터 받은 문자 : {txt}");

                    string msg = Console.ReadLine();
                    byte[] buffer = Encoding.UTF8.GetBytes(msg, 0, msg.Length);
                    socket.Send(buffer);

                    
                    //4.종료
                    //socket.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("오류 났어영");
            }
        }
    }
}

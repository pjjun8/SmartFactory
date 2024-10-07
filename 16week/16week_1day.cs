https://holy-brick-a81.notion.site/1-1181b466f44180dca796f761ad1514a6?pvs=4


// Server.cs 

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // 서버가 수신할 IP 주소와 포트 정의
            IPAddress ipAddress = IPAddress.Any; // 모든 네트워크 인터페이스에서 수신
            int port = 10003;

            TcpListener listener = new TcpListener(ipAddress, port);

            try
            {
                listener.Start(); // 클라이언트 연결 대기 시작
                Console.WriteLine("서버 시작됨. 클라이언트 대기 중...");

                // 들어오는 연결 수락
                using (TcpClient client = listener.AcceptTcpClient())
                {
                    Console.WriteLine("클라이언트 연결됨.");

                    // 읽기/쓰기 위한 네트워크 스트림 가져오기
                    using (NetworkStream stream = client.GetStream())
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine("받은 메시지: " + receivedMessage);

                        // 클라이언트에게 응답 전송
                        string responseMessage = "서버에서 인사드립니다!";
                        byte[] responseBytes = Encoding.UTF8.GetBytes(responseMessage);
                        stream.Write(responseBytes, 0, responseBytes.Length);
                        Console.WriteLine("클라이언트에게 응답 전송 완료.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("오류 발생: " + ex.Message);
            }
            finally
            {
                listener.Stop(); // 연결 대기 중지
                Console.WriteLine("서버 중지됨.");
            }
        }
    }
}
================================================================
// Client.cs
using System;
using System.Net.Sockets;
using System.Text;

namespace SimpleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverIp = "127.0.0.1"; // 서버 IP 주소 설정
            int port = 10003;

            try
            {
                // 서버에 연결 시도
                using (TcpClient client = new TcpClient(serverIp, port))
                {
                    Console.WriteLine("서버에 연결됨.");

                    // 읽기/쓰기 위한 네트워크 스트림 가져오기
                    using (NetworkStream stream = client.GetStream())
                    {
                        // 서버에 메시지 전송
                        string message = "클라이언트에서 인사드립니다!";
                        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                        stream.Write(messageBytes, 0, messageBytes.Length);
                        Console.WriteLine("서버에 메시지 전송 완료.");

                        // 서버로부터 응답 수신
                        byte[] buffer = new byte[1024];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string responseMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine("서버로부터 받은 메시지: " + responseMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("오류 발생: " + ex.Message);
            }
        }
    }
}

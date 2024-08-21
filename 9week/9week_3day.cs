//일반 서버
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPListenerECHOServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                IPAddress serverIP = IPAddress.Parse("127.0.0.1");
                int port = 13000;
                server = new TcpListener(serverIP, port);
                server.Start();

                Console.WriteLine("Echo Server 시작...");

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("클라이언트가 연결되었습니다.");

                    Thread clientThread = new Thread(new ParameterizedThreadStart(ClientAction));
                    clientThread.IsBackground = true;
                    clientThread.Start(client);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (server != null)
                {
                    server.Stop();
                }
            }//end of finally
        }//end of Main
        static void ClientAction(object obj)
        {
            TcpClient client = (TcpClient)obj; //다운캐스팅해준다 수준을 맞추기위해
            using (NetworkStream stream = client.GetStream())
            {
                //받기
                byte[] buffer = new byte[2048];
                int bytesRead = stream.Read(buffer, 0, buffer.Length); //바이트만 옵셋과 마지막을 설정해준다?
                string receiveMsg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"클라이언트에게 받은 메시지 : {receiveMsg}");

                //보내기
                byte[] echoMsg = Encoding.UTF8.GetBytes(receiveMsg);
                stream.Write(echoMsg, 0, echoMsg.Length);
                Console.WriteLine($"클라이언트에게 보낸 메시지 : {receiveMsg}");
            }
        }
    }//end of Main class
}//end of end
=======================================================================
//일반 클라이언트
using System.Net.Sockets;
using System.Text;

namespace TCPECHOClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string serverIP = "127.0.0.1";
            int port = 13000;
            TcpClient Client = new TcpClient(serverIP, port);

            using (NetworkStream stream = Client.GetStream())
            {

                Console.WriteLine("메시지 작성");
                byte[] Msg = Encoding.UTF8.GetBytes(Console.ReadLine());
                stream.Write(Msg, 0, Msg.Length);

                //메아리 받기
                byte[] echoMsgBytes = new byte[2048];
                int bytes = stream.Read(echoMsgBytes, 0, echoMsgBytes.Length);
                string echoeMsg = Encoding.UTF8.GetString(echoMsgBytes);
                Console.WriteLine($"Echo 메시지 : {echoeMsg}");
            }

        }
    }
}
=======================================================================
//윈폼용 서버
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WinFormEchoServer01
{
    public partial class Form1 : Form
    {
        private TcpListener server = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread serverThread = new Thread(StartServer);
            serverThread.IsBackground = true;
            serverThread.Start();
        }
        private void StartServer()
        {
            try
            {
                IPAddress serverIP = IPAddress.Parse("127.0.0.1");
                int port = 13000;
                server = new TcpListener(serverIP, port);
                server.Start();

                //Console.WriteLine("Echo Server 시작...");
                AppendText("Echo Server 시작...");
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    //Console.WriteLine("클라이언트가 연결되었습니다.");
                    AppendText("클라이언트가 연결되었습니다.");
                    Thread clientThread = new Thread(new ParameterizedThreadStart(ClientAction));
                    clientThread.IsBackground = true;
                    clientThread.Start(client);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (server != null)
                {
                    server.Stop();
                }
            }//end of finally
        }

        private void AppendText(string text) //textBox 사용 권한이 없어서 Invoke로 권한을 줘서 Append를 사용함
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendText), new object[] { text });
            }
            else
            {
                textBox1.AppendText(text + Environment.NewLine);
            }
        }
        private void ClientAction(object obj)
        {
            TcpClient client = (TcpClient)obj; //다운캐스팅해준다 수준을 맞추기위해
            using (NetworkStream stream = client.GetStream())
            {
                //받기
                byte[] buffer = new byte[2048];
                int bytesRead = stream.Read(buffer, 0, buffer.Length); //바이트만 옵셋과 마지막을 설정해준다?
                string receiveMsg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                //Console.WriteLine($"클라이언트에게 받은 메시지 : {receiveMsg}");
                AppendText($"클라이언트에게 받은 메시지 : {receiveMsg}");

                //보내기
                byte[] echoMsg = Encoding.UTF8.GetBytes(receiveMsg);
                stream.Write(echoMsg, 0, echoMsg.Length);
                //Console.WriteLine($"클라이언트에게 보낸 메시지 : {receiveMsg}");
                AppendText($"클라이언트에게 보낸 메시지 : {receiveMsg}");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
===========================================================================
//윈폼용 클라이언트
using System.Net.Sockets;
using System.Text;

namespace WinFormTCPECHOClient01
{
    public partial class Form1 : Form
    {
        private TcpClient Client = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string serverIP = "127.0.0.1";
            int port = 13000;
            Client = new TcpClient(serverIP, port);

            using (NetworkStream stream = Client.GetStream())
            {

                //Console.WriteLine("메시지 작성");
                byte[] Msg = Encoding.UTF8.GetBytes(textBox1.Text);
                stream.Write(Msg, 0, Msg.Length);

                //메아리 받기
                byte[] echoMsgBytes = new byte[2048];
                int bytes = stream.Read(echoMsgBytes, 0, echoMsgBytes.Length);
                string echoeMsg = Encoding.UTF8.GetString(echoMsgBytes);
                //textBox2.Text = echoeMsg;
                textBox2.AppendText($"{echoeMsg}\n\r");
                //Console.WriteLine($"Echo 메시지 : {echoeMsg}");
            }
        }
        
    }
}

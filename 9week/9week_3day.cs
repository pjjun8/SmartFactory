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
=============================================================================================
//브로드 캐스트 서버
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace WinFormEchoServer01
{
    public partial class Form1 : Form
    {
        private TcpListener server = null;
        private List<TcpClient> clientList = new List<TcpClient>();
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

                    clientList.Add(client);

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
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)//바이트만 옵셋과 마지막을 설정해준다?
                {
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    AppendText("클라이언트로부터 받은 메시지: " + receivedMessage);

                    BroadcastMessage(receivedMessage, client);
                } 
                string receiveMsg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                //Console.WriteLine($"클라이언트에게 받은 메시지 : {receiveMsg}");
                //AppendText($"클라이언트에게 받은 메시지 : {receiveMsg}");

                //보내기
                //byte[] echoMsg = Encoding.UTF8.GetBytes(receiveMsg);
                //stream.Write(echoMsg, 0, echoMsg.Length);
                //Console.WriteLine($"클라이언트에게 보낸 메시지 : {receiveMsg}");
                //AppendText($"클라이언트에게 보낸 메시지 : {receiveMsg}");
            }
        }
        private void BroadcastMessage(string receivedMessage, TcpClient senderClient)
        {
            byte[] broadcastMsg = Encoding.UTF8.GetBytes(receivedMessage);

            lock (clientList)
            {
                foreach (TcpClient client in clientList)
                {
                    if (client != senderClient)
                    {
                        try
                        {
                            NetworkStream stream = client.GetStream();
                            stream.Write(broadcastMsg, 0, broadcastMsg.Length);
                        }
                        catch (Exception ex)
                        {
                            AppendText("메시지 전송 중 예외 발생: " + ex.Message);
                        }
                    }
                }
            }
            AppendText("메시지를 모든 클라이언트에게 전송했습니다.");
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
===================================================================
//브로드캐스트 클라이언트
using System.Net.Sockets;
using System.Text;

namespace WinFormTCPECHOClient01
{
    public partial class Form1 : Form
    {
        private TcpClient client;
        NetworkStream stream = null;
        private Thread receiveThread;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("서버에 연결되어 있지 않습니다.");
                return;
            }

            string message = textBox1.Text;
            byte[] Msg = Encoding.UTF8.GetBytes(message);
            stream.Write(Msg, 0, Msg.Length); // 메시지 전송

            AppendText("나: " + message); //화면에 내가 보낸 글자 추가

            textBox1.Text = "";
        }
        private void ReceiveMsg()
        {
            try
            {
                byte[] buffer = new byte[256];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string receiveMsg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    AppendText("받은 메시지: " + receiveMsg);
                }
            }
            catch (Exception ex)
            {
                AppendText("서버로부터 메시지 수신 중 오류 발생: " + ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }
        private void Disconnect()
        {
            /// isRunning = false; // 스레드 종료 신호

            if (stream != null)
                stream.Close();
            if (client != null)
                client.Close();

            AppendText("서버와의 연결이 종료되었습니다.");
        }
        // UI 스레드에서 안전하게 텍스트박스에 메시지를 추가하는 메서드
        private void AppendText(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendText), new object[] { text });
            }
            else
            {
                textBox2.AppendText(text + Environment.NewLine);
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectToServer();
        }
        private void ConnectToServer()
        {
            string server = "127.0.0.1";
            int port = 13000;

            try
            {
                client = new TcpClient(server, port);
                stream = client.GetStream();

                receiveThread = new Thread(ReceiveMsg);
                receiveThread.IsBackground = true;
                receiveThread.Start();

                AppendText("서버에 연결되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("서버에 연결할 수 없습니다: " + ex.Message);
            }
        }
    }
}

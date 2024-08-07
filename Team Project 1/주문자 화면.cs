using Oracle.ManagedDataAccess.Client;

namespace WinFormsApp14
{

    public partial class Form1 : Form
    {

        public string ProductName { get; set; } //주문한 제품 이름
        public int ProductEa {  get; set; }     // 주문한 제품 수량
        public Form1()
        {
            InitializeComponent();
        }
        static void Dbms()
        {
            string strConn = "Data Source=(DESCRIPTION=" +
                "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
                "(HOST=localhost)(PORT=1521)))" +
                "(CONNECT_DATA=(SERVER=DEDICATED)" +
                "(SERVICE_NAME=xe)));" +
                "User Id=scott;Password=tiger;";
            //1.연결 객체 만들기 - Client
            OracleConnection conn = new OracleConnection(strConn);

            //2.데이터베이스 접속을 위한 연결
            conn.Open();

            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;


            //4. 리소스 반환 및 종료
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (kVA15.Checked == true)
            {
                ProductName = "15kVA";
                ProductEa = int.Parse(orderCountText.Text);
            }
            else if(kVA20.Checked == true)
            {
                ProductName = "20kVA";
                ProductEa = int.Parse(orderCountText.Text);
            }
            else if (kVA50.Checked == true)
            {
                ProductName = "50kVA";
                ProductEa = int.Parse(orderCountText.Text);
            }
            else if (kVA75.Checked == true)
            {
                ProductName = "75kVA";
                ProductEa = int.Parse(orderCountText.Text);
            }
            else if (kVA100.Checked == true)
            {
                ProductName = "100kVA";
                ProductEa = int.Parse(orderCountText.Text);
            }

            MessageBox.Show("주문이 완료 되었습니다!");
            Form2 frm2 = new Form2(this);
            frm2.ShowDialog(); //모달 
            //Show() 모달리스
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Dbms();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

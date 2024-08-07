//오늘 부터 미니 프로젝트를 한다.
//윈폼1 주문자 화면
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
------------------------------------------------------------------
//윈폼2 공정화면
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp14
{
    public partial class Form2 : Form
    {
        private string name;    //주문한 제품 이름
        private int ea;         // 주문한 제품 수량
        Form1 frm1;     // HAS-A ((포함))
        public Form2() //디폴트 생성자
        {
            InitializeComponent();
            Application.Run(new Form1());
        }
        //Form1에 접근하려면 Form1 속성 중 Modifiers를 private --> public으로 수정해 줘야 한다.

        public Form2(object form)       //생성자를 하나 더 만듦
        {
            InitializeComponent();

            frm1 = (Form1)form;
            name = frm1.ProductName;
            ea = frm1.ProductEa;
            MessageBox.Show($"{name}, {ea}");
        }
        public Form2(string str, Form1 form1)
        {
            this.frm1 = form1;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Add("Name", "제품명");
            this.dataGridView1.Columns.Add("Ea", "제품 수량");
            this.dataGridView1.Rows.Add($"{name}", $"{ea}");
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

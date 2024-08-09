//윈폼 1
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
            //this.Hide(); // 약간의 오류 있음 Hide 하면 종료해도 백그라운드로 계속 돌아감 
            //frm2.ShowDialog(); //모달 
            frm2.Show(); //모달리스

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
-------------------------------------------------------------------------
//윈폼 2
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
        private int progressValue = 0;
        private int count = 1;
        Form1 frm1;     // HAS-A ((포함))
        Random random = new Random();
        public void GongZang1() //임의적 오류 발생 코드
        {
            int rand = random.Next(88, 113);

            if (!(rand > 90 && rand < 110))
            {
                timer1.Stop();
                MessageBox.Show($"권선이 {rand}번 감겨 오류입니다!");
            }
            else
            {
                MessageBox.Show($"성공!");
            }

        }
        public Form2() //디폴트 생성자
        {
            InitializeComponent();
        }
        //Form1에 접근하려면 Form1 속성 중 Modifiers를 private --> public으로 수정해 줘야 한다.

        public Form2(object form)       //생성자를 하나 더 만듦
        {
            InitializeComponent();
            frm1 = (Form1)form;
            name = frm1.ProductName;
            ea = frm1.ProductEa;

            //MessageBox.Show($"{name}, {ea}"); //확인용 메세지 박스
        }
        public Form2(string str, Form1 form1)
        {
            this.frm1 = form1;

        }

        private void Form2_Load(object sender, EventArgs e)
        {

            this.dataGridView1.Columns.Add("Name", "제품명");
            this.dataGridView1.Columns.Add("Ea", "제품 수량");

            for (int i = 1; i <= 8; i++)
            {
                // "progressBar" + i 라는 이름의 컨트롤을 찾습니다.
                Control[] controls = this.Controls.Find("progressBar" + i, true);
                if (controls.Length > 0 && controls[0] is ProgressBar)
                {
                    ProgressBar progressBar = (ProgressBar)controls[0];
                    progressBar.Minimum = 0;
                    progressBar.Maximum = 100;
                    progressBar.Value = 0;
                }
            }
            timer1.Interval = 10; // 100 milliseconds
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add($"{name}", $"{ea}"); // 오류 사항! 버튼 누르면 계속 같은 같이 나옴 수정 필요??
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Control[] controls = this.Controls.Find("progressBar" + count, true);
            if (controls.Length > 0 && controls[0] is ProgressBar)
            {
                ProgressBar progressBar = (ProgressBar)controls[0];
                progressValue += 1;

                if (progressValue <= 100)
                {
                    progressBar.Value = progressValue;

                }
                else
                {
                    count++;
                    progressValue = 0;
                    GongZang1();

                }
                if (count > 8)
                {
                    timer1.Stop();
                    MessageBox.Show("진행완료!");
                }
            }
        }

        private void 주문화면ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Hide(); // 약간의 오류 있음 Hide 하면 종료해도 백그라운드로 계속 돌아감 
            Form1 frm1 = new Form1();
            frm1.Show(); //모달
            //frm1.Show(); //모달리스
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            Form3 frm3 = new Form3();
            Form4 frm4 = new Form4();
            //frm1.Show();
            frm1.Dispose();
            frm3.Dispose();
            frm4.Dispose();
            this.Dispose();
        }

        private void 제품판매통계ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 frm5 = new Form5();
            frm5.Show(); //모달
        }

        private void 재고관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();
            frm6.Show(); //모달
        }
    }
}
---------------------------------------------------------
//윈폼 3
using Oracle.ManagedDataAccess.Client;
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
    public partial class Form3 : Form
    {

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strConn = "Data Source=(DESCRIPTION=" +
                    "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
                    "(HOST=localhost)(PORT=1521)))" +
                    "(CONNECT_DATA=(SERVER=DEDICATED)" +
                    "(SERVICE_NAME=xe)));" +
                    "User Id=scott;Password=tiger;";



            string ID = txtID.Text;
            string PWD = txtPWD.Text;


            // Oracle 데이터베이스 연결 문자열 설정


            // SQL 쿼리 정의

            using (OracleConnection conn = new OracleConnection(strConn))
            {

                conn.Open();
                try
                {
                    string query = "SELECT COUNT(*) FROM CUSTOMER WHERE LOGIN_ID = :ID AND LOGIN_PWD = :PWD";
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Connection = conn;
                        // 매개변수 설정
                        cmd.Parameters.Add(new OracleParameter("ID", ID));
                        cmd.Parameters.Add(new OracleParameter("PWD", PWD));

                        // 쿼리 실행 및 결과 확인
                        int userCount = Convert.ToInt32(cmd.ExecuteScalar());



                        if (userCount > 0)
                        {
                            MessageBox.Show("로그인 성공");

                            Form1 frm = new Form1();
                            frm.ShowDialog();

                        }
                        else
                        {
                            MessageBox.Show("아이디 또는 비밀번호가 일치하지 않습니다.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("데이터베이스 연결 중 오류가 발생했습니다: " + ex.Message);
                }


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4(this, "안녕하세요");
            frm4.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
--------------------------------------------------------
//윈폼4
using Oracle.ManagedDataAccess.Client;
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
    public partial class Form4 : Form
    {
        private Form3 frm1;
        private string str;
        public Form4()
        {
            InitializeComponent();
        }
        public Form4(object sender, EventArgs e)
        {
            InitializeComponent();
        }
        public Form4(object frm, string _str)
        {
            InitializeComponent();
            frm1 = (Form3)frm;
            str = _str;
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("아이디를 입력하세요.");
                return;
            }

            if (string.IsNullOrEmpty(txtPWD.Text))
            {
                MessageBox.Show("비밀번호를 입력하세요.");
                return;
            }

            if (string.IsNullOrEmpty(txtPWD_RE.Text))
            {
                MessageBox.Show("비밀번호 확인을 입력하세요.");
                return;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("이름을 입력하세요.");
                return;
            }

            if (string.IsNullOrEmpty(txtHP.Text))
            {
                MessageBox.Show("핸드폰 번호를 입력하세요.");
                return;
            }

            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("주소를 입력하세요.");
                return;
            }

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
            //3.1 Query 명령객체 만들기
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn; //연결객체와 연동

            //테이블 삭제 - 필요시 사용
            //cmd.CommandText = "Drop Table Customer";
            //cmd.ExecuteNonQuery();

            //테이블 생성 -처음만 사용
            cmd.CommandText = "CREATE TABLE Customer( " +
                               "LOGIN_ID       VARCHAR2(100) NOT NULL PRIMARY KEY, " +
                               "LOGIN_PWD    VARCHAR2(100) NOT NULL, " +
                               "CNAME    VARCHAR2(100) NOT NULL, " +
                               "HP      VARCHAR2(100) NOT NULL, " +
                               "ADDRESS      VARCHAR2(50) NOT NULL)";
            cmd.ExecuteNonQuery();

            if (txtPWD.Text == txtPWD_RE.Text)
            {
                cmd.CommandText = "INSERT INTO Customer (LOGIN_ID, LOGIN_PWD, CNAME, HP, ADDRESS)" +
                                  $"VALUES ('{txtID.Text}', '{txtPWD.Text}', '{txtName.Text}', '{txtHP.Text}', '{txtAddress.Text}')";


                cmd.ExecuteNonQuery();

                MessageBox.Show("회원 가입이 완료되었습니다.");
                MessageBox.Show("로그인 하면으로 돌아갑니다.");

                Dispose();

            }
            else
            {
                MessageBox.Show("비밀번호를 다시 확인해주세요");
            }
            conn.Close();
        }
    }
}
-------------------------------------------------------------------
//윈폼 5
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WinFormsApp14
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
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
            //3.1 Query 명령객체 만들기
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn; //연결객체와 연동

            //cmd.CommandText = "DROP TABLE PRODUCT_SALE";
            //cmd.ExecuteNonQuery();

            //처음 테이블 생성시 사용
            //cmd.CommandText = "CREATE TABLE PRODUCT_SALE(" +
            //                  "PCode VARCHAR2(30) PRIMARY KEY, " +
            //                  "PName VARCHAR2(30) NOT NULL, " +
            //                  "PPrice NUMBER(30), " +
            //                  "SalesQuantity NUMBER(30) DEFAULT 0, " +
            //                  "SalesPrice NUMBER(30))";
            //cmd.ExecuteNonQuery();

            //테이블 데이터 삽입
            //cmd.CommandText = "INSERT INTO PRODUCT_SALE (PCODE, PNAME, PPRICE, SALESQUANTITY, SALESPRICE) " +
            //                      $"VALUES ('1', '15kVA', {150000}, {0}, {0})";
            //cmd.ExecuteNonQuery();
            //cmd.CommandText = "INSERT INTO PRODUCT_SALE (PCode, PName, PPrice, SalesQuantity, SalesPrice) " +
            //                      $"VALUES ('2', '20kVA',{200000}, {0}, {0})";
            //cmd.ExecuteNonQuery();
            //cmd.CommandText = "INSERT INTO PRODUCT_SALE (PCode, PName, PPrice, SalesQuantity, SalesPrice) " +
            //                      $"VALUES ('3', '50kVA', {500000}, {0}, {0})";
            //cmd.ExecuteNonQuery();
            //cmd.CommandText = "INSERT INTO PRODUCT_SALE (PCode, PName, PPrice, SalesQuantity, SalesPrice) " +
            //                      $"VALUES ('4', '75kVA',{750000}, {0}, {0})";
            //cmd.ExecuteNonQuery();
            //cmd.CommandText = "INSERT INTO PRODUCT_SALE (PCode, PName, PPrice, SalesQuantity, SalesPrice) " +
            //                      $"VALUES ('5', '100kVA',{1000000}, {0}, {0})";
            //cmd.ExecuteNonQuery();

            cmd.CommandText = "COMMIT";
            cmd.ExecuteNonQuery();
            //4. 리소스 반환 및 종료
            conn.Close();
        }
    }
}
--------------------------------------------------------------------
//윈폼 6
using Oracle.ManagedDataAccess.Client;
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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
            //3.1 Query 명령객체 만들기
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn; //연결객체와 연동



            //처음 테이블 생성시 사용
            //cmd.CommandText = "CREATE TABLE Material(" +
            //                  "PNO NUMBER(30) PRIMARY KEY, " +
            //                  "PNAME VARCHAR2(30) NOT NULL, " +
            //                  "PEA VARCHAR2(30))";

            

            cmd.ExecuteNonQuery();



            //4. 리소스 반환 및 종료
            conn.Close();
        }
    }
}

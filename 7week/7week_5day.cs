//주문화면 폼
using Oracle.ManagedDataAccess.Client;

namespace WinFormsApp14
{

    public partial class Form1 : Form   // 주문 화면
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
---------------------------------------------------------------------------------------
//공정화면 폼
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
    public partial class Form2 : Form   //공정 화면
    {
        public string name;    //주문한 제품 이름
        public int ea;         // 주문한 제품 수량
        private int progressValue = 0;
        private int count = 1;
        public bool flag = false;
        Form1 frm1;     // HAS-A ((포함))
        Random random = new Random();
        
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
        public void GongZang1() //임의적 오류 발생 코드
        {
            int rand = random.Next(49, 152);

            if (!(rand >= 50 && rand <= 150))
            {
                timer1.Stop();
                MessageBox.Show($"해당 공정에서 오류입니다!");
            }
            else
            {
                //MessageBox.Show($"성공!");
            }

        }
        public void DbmsCheck() //재고 수량 확인 로직
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

            cmd.CommandText = "SELECT * FROM Material ";
            cmd.ExecuteNonQuery();

            OracleDataReader reader = cmd.ExecuteReader();
            int Dbea; string Dbname;
            
            while (reader.Read())
            {
                Dbea = int.Parse(reader["PEA"].ToString());
                Dbname = reader["PNAME"] as string;

                if (Dbea <= 0)
                {
                    flag = true;
                }
            }
            conn.Close();
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

        //재고 수량 확인 로직
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Rows.Add($"{name}", $"{ea}"); // 오류 사항! 버튼 누르면 계속 같은 같이 나옴 수정 필요??
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DbmsCheck();
            Form6 form6 = new Form6(this);
            
            if (flag == false)
            {
                timer1.Start();
                form6.Dbms();
                form6.upFlag = false;
                form6.addFlag = true;
                form6.checkFlag = true;
            }
            else
                MessageBox.Show("재료가 부족합니다.");
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
            Form5 frm5 = new Form5();
            Form6 frm6 = new Form6();
            //frm1.Show();
            frm1.Dispose();
            frm3.Dispose();
            frm4.Dispose();
            frm5.Dispose();
            frm6.Dispose();
            this.Dispose();
        }

        private void 제품판매통계ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 frm5 = new Form5(this);
            frm5.Show(); //모달
        }

        private void 재고관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6(this);
            frm6.Show(); //모달
        }
    }
}
-------------------------------------------------------------
//로그인 화면 폼
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
    public partial class Form3 : Form   //로그인 화면
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
-----------------------------------------------------------------
//회원가입 화면 폼
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
    public partial class Form4 : Form   //회원 가입
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
-----------------------------------------------------------------------------
//통계화면 폼
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
    public partial class Form5 : Form   // 통계 화면
    {
        private string f2name;
        private int f2ea;
        Form2 form2;
        public Form5()
        {
            InitializeComponent();
        }
        public Form5(Object form)
        {
            InitializeComponent();
            form2 = (Form2)form;
            f2name = form2.name;
            f2ea = form2.ea;

        }
        public void Dbmssale()
        {
            string strConn = "Data Source=(DESCRIPTION=" +
                    "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
                    "(HOST=localhost)(PORT=1521)))" +
                    "(CONNECT_DATA=(SERVER=DEDICATED)" +
                    "(SERVICE_NAME=xe)));" +
                    "User Id=scott;Password=tiger;";

            //dataGridView에 db테이블 가져오기
            OracleConnection conn = new OracleConnection(strConn);

            conn.Open();
            string query = "SELECT PCODE, PNAME, PPRICE, SALESQUANTITY, SALESPRICE FROM product_sale";
            OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            OracleCommand cmd = new OracleCommand();

            cmd.CommandText = $"UPDATE PRODUCT_SALE SET SALESQUANTITY = '{f2ea}' WHERE PNAME = '{f2name}'";
            cmd.ExecuteNonQuery();

            string query1 = "SELECT PNAME, SALESQUANTITY FROM product_sale";
            string query2 = "SELECT PNAME, SALESPRICE FROM product_sale";
            OracleCommand cmd1 = new OracleCommand(query1, conn);
            OracleCommand cmd2 = new OracleCommand(query2, conn);
            OracleDataReader dataReader1 = cmd1.ExecuteReader();
            OracleDataReader dataReader2 = cmd2.ExecuteReader();

            chart1.Series["SALESQUANTITY"].Points.DataBind(dataReader1, "PNAME", "SALESQUANTITY", "Tooltip=SALESQUANTITY");
            chart1.Series["SALESPRICE"].Points.DataBind(dataReader2, "PNAME", "SALESPRICE", "Tooltip=SALESPRICE");
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

            //cmd.CommandText = "COMMIT";
           // cmd.ExecuteNonQuery();
            //4. 리소스 반환 및 종료
            conn.Close();
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            Dbmssale();
        }
    }
}
-----------------------------------------------------------------------
//재고 관리화면 폼
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
    public partial class Form6 : Form   //재고 화면
    {
        public string ProductName { get; set; } //재료 이름
        public int ProductEa { get; set; }     // 재료 수량
        Form2 form2;
        private string f2name;
        private int f2ea;
        public bool upFlag = false;
        public bool addFlag = false;
        public bool checkFlag = false;
        public Form6()
        {
            InitializeComponent();
        }
        public Form6(Object form)
        {
            InitializeComponent();
            form2 = (Form2)form;
            f2name = form2.name;
            f2ea = form2.ea;
            this.dataGridView1.Columns.Add("No", "재료 번호");
            this.dataGridView1.Columns.Add("Name", "재료 이름");
            this.dataGridView1.Columns.Add("Ea", "재료 수량");
        }

        public void Dbms()
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

            //cmd.CommandText = "INSERT INTO Material(PNO, PNAME, PEA) " +
            //                   $"VALUES ({1}, '절연유', '100')";
            //cmd.ExecuteNonQuery();
            //cmd.CommandText = "INSERT INTO Material(PNO, PNAME, PEA) " +
            //                  $"VALUES ({2}, '알루미늄 코일', '100')";
            //cmd.ExecuteNonQuery();
            //cmd.CommandText = "INSERT INTO Material(PNO, PNAME, PEA) " +
            //                  $"VALUES ({3}, '구리 코일', '100')";
            //cmd.ExecuteNonQuery();
            //cmd.CommandText = "INSERT INTO Material(PNO, PNAME, PEA) " +
            //                  $"VALUES ({4}, '코어', '100')";
            //cmd.ExecuteNonQuery();
            //cmd.CommandText = "INSERT INTO Material(PNO, PNAME, PEA) " +
            //                  $"VALUES ({5}, '케이스', '100')";
            //cmd.ExecuteNonQuery();


            // 테이블 수정을 위한 요소
            cmd.CommandText = "SELECT * FROM Material ";
            cmd.ExecuteNonQuery();
            OracleDataReader reader = cmd.ExecuteReader();
            int Dbno = 0, Dbea = 0; string Dbname = "";

            //재료 업데이트(사용) 로직
            if (upFlag == false)
            {
                while (reader.Read())
                {
                    Dbea = int.Parse(reader["PEA"].ToString());
                    Dbname = reader["PNAME"] as string;
                    //MessageBox.Show($"{Dbname},{f2name}");
                    if (f2name == "15kVA")
                    {
                        if (Dbname == "케이스")
                        {
                            Dbea -= f2ea;
                            cmd.CommandText = $"UPDATE Material SET PEA = '{Dbea}' WHERE PNAME = '{Dbname}'";
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            Dbea -= f2ea;
                            cmd.CommandText = $"UPDATE Material SET PEA = '{Dbea}' WHERE PNAME = '{Dbname}'";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else if (f2name == "20kVA")
                    {
                        if (Dbname == "케이스")
                        {
                            Dbea -= f2ea;
                            cmd.CommandText = $"UPDATE Material SET PEA = '{Dbea}' WHERE PNAME = '{Dbname}'";
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            Dbea -= (f2ea * 2);
                            cmd.CommandText = $"UPDATE Material SET PEA = '{Dbea}' WHERE PNAME = '{Dbname}'";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else if (f2name == "50kVA")
                    {
                        if (Dbname == "케이스")
                        {
                            Dbea -= f2ea;
                            cmd.CommandText = $"UPDATE Material SET PEA = '{Dbea}' WHERE PNAME = '{Dbname}'";
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            Dbea -= (f2ea * 3);
                            cmd.CommandText = $"UPDATE Material SET PEA = '{Dbea}' WHERE PNAME = '{Dbname}'";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else if (f2name == "75kVA")
                    {
                        if (Dbname == "케이스")
                        {
                            Dbea -= f2ea;
                            cmd.CommandText = $"UPDATE Material SET PEA = '{Dbea}' WHERE PNAME = '{Dbname}'";
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            Dbea -= (f2ea * 4);
                            cmd.CommandText = $"UPDATE Material SET PEA = '{Dbea}' WHERE PNAME = '{Dbname}'";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else if (f2name == "100kVA")
                    {
                        if (Dbname == "케이스")
                        {
                            Dbea -= f2ea;
                            cmd.CommandText = $"UPDATE Material SET PEA = '{Dbea}' WHERE PNAME = '{Dbname}'";
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            Dbea -= (f2ea * 5);
                            cmd.CommandText = $"UPDATE Material SET PEA = '{Dbea}' WHERE PNAME = '{Dbname}'";
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else { MessageBox.Show("앞의 if문 올 실행 안됨"); }
                }//end of while //재료 업데이트(사용) 로직
            }


            //재료 업데이트(추가) 로직
            
            if (addFlag == false)
            {
                //Dbno = 0; Dbea = 0; Dbname = "";
                cmd.CommandText = "SELECT * FROM Material ";
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Dbea = int.Parse(reader["PEA"].ToString());
                    Dbname = reader["PNAME"] as string;

                    ProductEa = int.Parse(numericUpDown1.Text);
                    int ea = 0;
                    if (oil.Checked == true && Dbname == "절연유")
                    {
                        ea = Dbea + ProductEa;
                        //ProductName = "절연유";
                        cmd.CommandText = $"UPDATE Material SET PEA = '{ea}' WHERE PNAME = '{Dbname}'";
                        ea = 0;
                        cmd.ExecuteNonQuery();
                        oil.Checked = false;
                    }
                    if (alcoil.Checked == true && Dbname == "알루미늄 코일")
                    {
                        ea = Dbea + ProductEa;
                        //ProductName = "알루미늄 코일";
                        ProductEa = int.Parse(numericUpDown1.Text);
                        cmd.CommandText = $"UPDATE Material SET PEA = '{ea}' WHERE PNAME = '{Dbname}'";
                        ea = 0;
                        cmd.ExecuteNonQuery();
                        alcoil.Checked = false;
                    }
                    if (gucoil.Checked == true && Dbname == "구리 코일")
                    {
                        ea = Dbea + ProductEa;
                        //ProductName = "구리 코일";
                        ProductEa = int.Parse(numericUpDown1.Text);
                        cmd.CommandText = $"UPDATE Material SET PEA = '{ea}' WHERE PNAME = '{Dbname}'";
                        ea = 0;
                        cmd.ExecuteNonQuery();
                        gucoil.Checked = false;
                    }
                    if (core.Checked == true && Dbname == "코어")
                    {
                        ea = Dbea + ProductEa;
                        //ProductName = "코어";
                        ProductEa = int.Parse(numericUpDown1.Text);
                        cmd.CommandText = $"UPDATE Material SET PEA = '{ea}' WHERE PNAME = '{Dbname}'";
                        ea = 0;
                        cmd.ExecuteNonQuery();
                        core.Checked = false;
                    }
                    if (case1.Checked == true && Dbname == "케이스")
                    {
                        ea = Dbea + ProductEa;
                        //ProductName = "케이스";
                        ProductEa = int.Parse(numericUpDown1.Text);
                        cmd.CommandText = $"UPDATE Material SET PEA = '{ea}' WHERE PNAME = '{Dbname}'";
                        ea = 0;
                        cmd.ExecuteNonQuery();
                        case1.Checked = false;
                    }
                }
                numericUpDown1.Text = "";
            }//재료 업데이트(추가) 로직

            //재료 조회 로직
            if (checkFlag == false)
            {
                cmd.CommandText = "SELECT * FROM Material ";
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    Dbno = int.Parse(reader["PNO"].ToString());
                    Dbea = int.Parse(reader["PEA"].ToString());
                    Dbname = reader["PNAME"] as string;
                    this.dataGridView1.Rows.Add($"{Dbno}", $"{Dbname}", $"{Dbea}");

                }
            }

                //4. 리소스 반환 및 종료
                conn.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            upFlag = true;
            checkFlag = true;
            addFlag = false;
            
            Dbms();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            upFlag = true;
            addFlag = true;
            checkFlag = false;
            dataGridView1.Rows.Clear();

            Dbms();
        }
    }
}


using Oracle.ManagedDataAccess.Client;

namespace WinFormsApp14
{

    public partial class Form1 : Form   // 주문 화면
    {
        public string ProductName { get; set; } //주문한 제품 이름
        public int ProductEa { get; set; }     // 주문한 제품 수량
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
            else if (kVA20.Checked == true)
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

            //색상 변경

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Dbms();




        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void kVA15_CheckedChanged(object sender, EventArgs e)
        {
            if (kVA15.Checked)
            {
                kVA15.ForeColor = Color.Yellow;
            }
            else
            {
                kVA15.ForeColor = SystemColors.Control; // 체크 해제 시 기본 색상으로 되돌림
            }
        }

        private void kVA20_CheckedChanged(object sender, EventArgs e)
        {
            if (kVA20.Checked)
            {
                kVA20.ForeColor = Color.Yellow;
            }
            else
            {
                kVA20.ForeColor = SystemColors.Control; // 체크 해제 시 기본 색상으로 되돌림
            }



        }

        private void kVA50_CheckedChanged(object sender, EventArgs e)
        {
            if (kVA50.Checked)
            {
                kVA50.ForeColor = Color.Yellow;
            }
            else
            {
                kVA50.ForeColor = SystemColors.Control; // 체크 해제 시 기본 색상으로 되돌림
            }
        }

        private void kVA75_CheckedChanged(object sender, EventArgs e)
        {
            if (kVA75.Checked)
            {
                kVA75.ForeColor = Color.Yellow;
            }
            else
            {
                kVA75.ForeColor = SystemColors.Control; // 체크 해제 시 기본 색상으로 되돌림
            }
        }

        private void kVA100_CheckedChanged(object sender, EventArgs e)
        {
            if (kVA100.Checked)
            {
                kVA100.ForeColor = Color.Yellow;
            }
            else
            {
                kVA100.ForeColor = SystemColors.Control; // 체크 해제 시 기본 색상으로 되돌림
            }
        }

        private void orderButton_MouseMove(object sender, MouseEventArgs e)
        {
            orderButton.ForeColor = Color.Yellow;
        }

        private void orderButton_MouseLeave(object sender, EventArgs e)
        {
            orderButton.ForeColor = Color.White;
        }
    }
}
--------------------------------------------------------------------------------
using LanguageExt;
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
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.DataFormats;

namespace WinFormsApp14
{
    public partial class Form2 : Form   //공정 화면
    {

        public int timnum = 0;
        public string name;    //주문한 제품 이름
        public int ea;         // 주문한 제품 수량
        private int progressValue = 0;
        private int count = 1;
        public bool flag = false;
        public bool stopFlag = false;
        public int flagF5 = 0;
        Form1 frm1;     // HAS-A ((포함))
        //Form5 form5;
        Form5 frm5;
        Random random = new Random();

        public Form2() //디폴트 생성자
        {
            InitializeComponent();

        }

        private void MenuItem_MouseEnter(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;


            if (menuItem != null)
            {
                menuItem.ForeColor = Color.Yellow; // 마우스가 올라갔을 때 색상 변경
            }
        }

        public class CustomProfessionalColors : ProfessionalColorTable  //menuStrip 선택시 배경 색 투명
        {
            public override Color MenuItemSelected
            {
                get { return Color.Transparent; } // 선택된 항목의 배경색을 투명하게 설정
            }

            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.Transparent; } // 선택된 항목의 그라디언트 시작 색상을 투명하게 설정
            }

            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.Transparent; } // 선택된 항목의 그라디언트 끝 색상을 투명하게 설정
            }

            public override Color MenuItemBorder
            {
                get { return Color.Transparent; } // 선택된 항목의 경계선을 투명하게 설정
            }
        }

        private void MenuItem_MouseLeave(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                menuItem.ForeColor = Color.White; // 마우스가 떠나면 원래 색상으로 변경
            }
        }

        //Form1에 접근하려면 Form1 속성 중 Modifiers를 private --> public으로 수정해 줘야 한다.

        public Form2(object form)       //생성자를 하나 더 만듦
        {
            InitializeComponent();
            frm1 = (Form1)form;
            name = frm1.ProductName;
            ea = frm1.ProductEa;
            //form5 = new Form5();
            //MessageBox.Show($"{name}, {ea}"); //확인용 메세지 박스
            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new CustomProfessionalColors());

            // 각 MenuItem에 이벤트 핸들러 추가
            foreach (ToolStripMenuItem menuItem in menuStrip1.Items)
            {
                menuItem.MouseEnter += MenuItem_MouseEnter;
                menuItem.MouseLeave += MenuItem_MouseLeave;
            }
            foreach (ToolStripMenuItem menuItem in menuStrip1.Items)
            {
                menuItem.MouseEnter += MenuItem_MouseEnter;
                menuItem.MouseLeave += MenuItem_MouseLeave;
            }
        }
        public Form2(string str, Form1 form1)
        {
            this.frm1 = form1;

        }
        public void GongZang1() //임의적 오류 발생 코드
        {
            int rand = random.Next(45, 153);

            if (!(rand >= 50 && rand <= 150))
            {
                pictureBox9.Visible = false;
                pictureBox10.Visible = false;
                pictureBox11.Visible = false;
                pictureBox12.Visible = false;
                pictureBox13.Visible = false;
                pictureBox14.Visible = false;
                pictureBox15.Visible = false;
                pictureBox16.Visible = false;
                stopFlag = true;
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

            Form6 form6 = new Form6(this);
            form6.upFlag = false;
            form6.addFlag = true;
            form6.checkFlag = true;
            form6.Dbms();

            while (reader.Read())
            {
                Dbea = int.Parse(reader["PEA"].ToString());
                Dbname = reader["PNAME"] as string;

                if (Dbname != "케이스")
                {
                    if ((Dbea - ea) < 0 && name == "15kVA")
                    {
                        flag = true;
                    }
                    else if ((Dbea - (ea * 2)) < 0 && name == "20kVA")
                    {
                        flag = true;
                    }
                    else if ((Dbea - (ea * 3)) < 0 && name == "50kVA")
                    {
                        flag = true;
                    }
                    else if ((Dbea - (ea * 4)) < 0 && name == "75kVA")
                    {
                        flag = true;
                    }
                    else if ((Dbea - (ea * 5)) < 0 && name == "100kVA")
                    {
                        flag = true;
                    }
                }
                else if (Dbname == "케이스")
                {
                    if ((Dbea - ea) < 0)
                    {
                        flag = true;
                    }
                }
            }
            if (flag == true)
            {

                form6.upFlag = true;
                form6.addFlag = false;
                form6.checkFlag = true;

                form6.oil.Checked = true;
                form6.alcoil.Checked = true;
                form6.gucoil.Checked = true;
                form6.core.Checked = true;
                form6.case1.Checked = true;
                form6.smartFlag = true;
                if (name == "15kVA")
                    form6.numericUpDown1.Text = $"{ea * 1}";
                else if (name == "20kVA")
                    form6.numericUpDown1.Text = $"{ea * 2}";
                else if (name == "50kVA")
                    form6.numericUpDown1.Text = $"{ea * 3}";
                else if (name == "75kVA")
                    form6.numericUpDown1.Text = $"{ea * 4}";
                else if (name == "100kVA")
                    form6.numericUpDown1.Text = $"{ea * 5}";
                form6.Dbms();
            }

            conn.Close();
        }
        private void Form2_Load(object sender, EventArgs e)
        {

            this.dataGridView1.Columns.Add("Name", "제품명");
            this.dataGridView1.Columns.Add("Ea", "제품 수량");
            //시작 시 gif 안보이게 하기
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;
            pictureBox13.Visible = false;
            pictureBox14.Visible = false;
            pictureBox15.Visible = false;
            pictureBox16.Visible = false;

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
            timer1.Interval = 40; // 100 milliseconds
            //gridview HeaderColor
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.RowHeadersDefaultCellStyle.ForeColor = Color.White;

            menuStrip1.ForeColor = Color.White;
        }

        //재고 수량 확인 로직


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Rows.Add($"{name}", $"{ea}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (stopFlag == false)
            {
                flagF5 = 1;
                DbmsCheck();
            }
            if (flag == false)
            {

                //form5 = new Form5(this);
                frm5 = new Form5(this);
                timer1.Start();
            }
            else
            {
                flag = false;
                MessageBox.Show("재료가 부족합니다.");
            }

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Control[] controls = this.Controls.Find("progressBar" + count, true);
            ProgressBar progressBar;
            if (controls.Length > 0 && controls[0] is ProgressBar)
            {
                progressBar = (ProgressBar)controls[0];
                progressValue += 1;

                if (progressValue <= 100)
                {
                    progressBar.Value = progressValue;
                    if (count == 1)
                    {
                        pictureBox9.Visible = true;
                        label3.ForeColor = Color.Yellow;
                    }
                    else
                    {
                        pictureBox9.Visible = false;
                        label3.ForeColor = SystemColors.Control;
                    }
                    if (count == 2)
                    {
                        pictureBox10.Visible = true;
                        label4.ForeColor = Color.Yellow;
                    }
                    else
                    {
                        pictureBox10.Visible = false;
                        label4.ForeColor = SystemColors.Control;
                    }
                    if (count == 3)
                    {
                        pictureBox11.Visible = true;
                        label5.ForeColor = Color.Yellow;
                    }
                    else
                    {
                        pictureBox11.Visible = false;
                        label5.ForeColor = SystemColors.Control;
                    }
                    if (count == 4)
                    {
                        pictureBox12.Visible = true;
                        label6.ForeColor = Color.Yellow;
                    }
                    else
                    {
                        pictureBox12.Visible = false;
                        label6.ForeColor = SystemColors.Control;
                    }
                    if (count == 5)
                    {
                        pictureBox13.Visible = true;
                        label7.ForeColor = Color.Yellow;
                    }
                    else
                    {
                        pictureBox13.Visible = false;
                        label7.ForeColor = SystemColors.Control;
                    }
                    if (count == 6)
                    {
                        pictureBox14.Visible = true;
                        label8.ForeColor = Color.Yellow;
                    }
                    else
                    {
                        pictureBox14.Visible = false;
                        label8.ForeColor = SystemColors.Control;
                    }
                    if (count == 7)
                    {
                        pictureBox15.Visible = true;
                        label9.ForeColor = Color.Yellow;
                    }
                    else
                    {
                        pictureBox15.Visible = false;
                        label9.ForeColor = SystemColors.Control;
                    }
                    if (count == 8)
                    {
                        pictureBox16.Visible = true;
                        label10.ForeColor = Color.Yellow;
                    }
                    else
                    {
                        pictureBox16.Visible = false;
                        label10.ForeColor = SystemColors.Control;
                    }
                }
                else
                {
                    count++;
                    progressValue = 0;
                    GongZang1();

                }
                if (count > 8)
                {
                    pictureBox9.Visible = false;
                    pictureBox10.Visible = false;
                    pictureBox11.Visible = false;
                    pictureBox12.Visible = false;
                    pictureBox13.Visible = false;
                    pictureBox14.Visible = false;
                    pictureBox15.Visible = false;
                    pictureBox16.Visible = false;

                    count = 1;
                    progressValue = 0;
                    for (int i = 1; i <= 8; i++)
                    {
                        controls = this.Controls.Find("progressBar" + i, true);
                        progressBar = (ProgressBar)controls[0];
                        progressBar.Value = progressValue;

                    }
                    stopFlag = false;
                    timer1.Stop();
                    MessageBox.Show("진행완료!");
                }
                if (progressBar8.Value == 0)
                {
                    label10.ForeColor = Color.White;
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
            frm5 = new Form5(this);
            frm5.Show();

            //form5.Show(); //모달
        }

        private void 재고관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6(this);
            frm6.Show(); //모달
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                menuItem.ForeColor = Color.Yellow; // 마우스가 올라갔을 때 색상 변경


            }
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.ForeColor = Color.Yellow;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White;
        }

        private void productComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 관리자명단ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form7 form7 = new Form7();
            form7.Show();
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            
        }
    }
}
-----------------------------------------------------------------------------
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp14
{
    public partial class Form3 : Form   //로그인 화면
    {
        private string password = ""; // 실제 비밀번호를 저장할 변수
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            txtPWD.PasswordChar = '\0'; // 기본적으로는 PasswordChar를 사용하지 않음

            int selectionStart = txtPWD.SelectionStart; // 현재 커서 위치를 저장
            int length = txtPWD.Text.Length;

            // 입력된 비밀번호를 저장 (문자열이 추가된 경우)
            if (length > password.Length && selectionStart > 0)
            {
                password += txtPWD.Text.Substring(selectionStart - 1, 1);
            }
            else if (length < password.Length) // 문자열이 삭제된 경우
            {
                password = password.Substring(0, length);
            }

            // 이전 문자들은 *로 변경하고 마지막 문자만 보이도록 설정
            string maskedPassword = password.Length > 1
                ? new string('*', password.Length - 1) + password.LastOrDefault()
                : password;

            // 텍스트박스에 *로 표시
            txtPWD.TextChanged -= txtPWD_TextChanged; // 이벤트 핸들러를 일시적으로 제거
            txtPWD.Text = maskedPassword;
            txtPWD.TextChanged += txtPWD_TextChanged; // 이벤트 핸들러를 다시 추가

            txtPWD.SelectionStart = selectionStart; // 커서를 마지막 위치로 복원

        }
        private void txtPWD_TextChanged(object sender, EventArgs e)
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
            string PWD = password;




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

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.ForeColor = Color.Yellow;
            
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White;
            
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            button2.ForeColor = Color.Yellow;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.White;
        }
    }
}
------------------------------------------------------------------------------
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
            //cmd.CommandText = "CREATE TABLE Customer( " +
            //                   "LOGIN_ID       VARCHAR2(100) NOT NULL PRIMARY KEY, " +
            //                   "LOGIN_PWD    VARCHAR2(100) NOT NULL, " +
            //                   "CNAME    VARCHAR2(100) NOT NULL, " +
            //                   "HP      VARCHAR2(100) NOT NULL, " +
            //                   "ADDRESS      VARCHAR2(50) NOT NULL)";
            //cmd.ExecuteNonQuery();

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

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.ForeColor= Color.Yellow;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White;
        }
    }
}
------------------------------------------------------------------------------------
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;

namespace WinFormsApp14
{
    public partial class Form5 : Form   // 통계 화면
    {
        private string f2Name;
        private int f2Ea;
        private string dbName;  //판매 통계 테이블 제품 이름
        private int dbEa;       //판매 통계 테이블 제품 판매 개수
        Form2 form2;
        public Form5()
        {
            InitializeComponent();
        }
        public Form5(Object form)
        {
            InitializeComponent();
            form2 = (Form2)form;
            f2Name = form2.name;
            f2Ea = form2.ea;
        }
        string strConn = "Data Source=(DESCRIPTION=" +
                    "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
                    "(HOST=localhost)(PORT=1521)))" +
                    "(CONNECT_DATA=(SERVER=DEDICATED)" +
                    "(SERVICE_NAME=xe)));" +
                    "User Id=scott;Password=tiger;";
        public void Dbmssale()
        {
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
            //dataGridView에 db테이블 가져오기

            using (OracleConnection conn = new OracleConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT PCODE, PNAME, PPRICE, SALESQUANTITY, SALESPRICE FROM PRODUCT_SALE";
                    OracleDataAdapter adapter = new OracleDataAdapter(query, conn);

                    DataTable dataTable = new DataTable();

                    OracleCommand cmd = new OracleCommand(query, conn);

                    OracleDataReader dataReader;

                    if (form2.flagF5 == 1)
                    {
                        dataReader = cmd.ExecuteReader();
                        int eaSum = 0, priceSum = 0;
                        while (dataReader.Read())
                        {
                            dbName = dataReader.GetString("PNAME");
                            dbEa = dataReader.GetInt32("SALESQUANTITY");
                            if (f2Name == "15kVA" && dbName == "15kVA")
                            {
                                eaSum = dbEa + f2Ea;
                                priceSum = eaSum * 150000;
                                cmd.CommandText = $"UPDATE PRODUCT_SALE SET SALESQUANTITY = {eaSum},SALESPRICE = {priceSum} WHERE PNAME = '15kVA'";
                                cmd.ExecuteNonQuery();
                                break;
                            }
                            else if (f2Name == "20kVA" && dbName == "20kVA")
                            {
                                eaSum = dbEa + f2Ea;
                                priceSum = eaSum * 200000;
                                cmd.CommandText = $"UPDATE PRODUCT_SALE SET SALESQUANTITY = {eaSum},SALESPRICE = {priceSum}  WHERE PNAME = '20kVA'";
                                cmd.ExecuteNonQuery();
                                break;
                            }
                            else if (f2Name == "50kVA" && dbName == "50kVA")
                            {
                                eaSum = dbEa + f2Ea;
                                priceSum = eaSum * 500000;
                                cmd.CommandText = $"UPDATE PRODUCT_SALE SET SALESQUANTITY = {eaSum},SALESPRICE = {priceSum} WHERE PNAME = '50kVA'";
                                cmd.ExecuteNonQuery();
                                break;
                            }
                            else if (f2Name == "75kVA" && dbName == "75kVA")
                            {
                                eaSum = dbEa + f2Ea;
                                priceSum = eaSum * 750000;
                                cmd.CommandText = $"UPDATE PRODUCT_SALE SET SALESQUANTITY = {eaSum},SALESPRICE = {priceSum} WHERE PNAME = '75kVA'";
                                cmd.ExecuteNonQuery();
                                break;
                            }
                            else if (f2Name == "100kVA" && dbName == "100kVA")
                            {
                                eaSum = dbEa + f2Ea;
                                priceSum = eaSum * 1000000;
                                cmd.CommandText = $"UPDATE PRODUCT_SALE SET SALESQUANTITY = {eaSum},SALESPRICE = {priceSum} WHERE PNAME = '100kVA'";
                                cmd.ExecuteNonQuery();
                                break;
                            }
                        }// end of while
                        form2.flagF5 = 0;
                    }// end of if
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                    //MessageBox.Show("폼 로드 및 using문 실행됨");
                    conn.Close(); //원래 없었음
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"에러: {ex.Message}");
                }
            }

            //버튼 클릭 시 두 개의 차트 조회
            using (OracleConnection conn = new OracleConnection(strConn))
            {
                try
                {
                    conn.Open();
                    string query1 = "SELECT PNAME, SALESQUANTITY FROM PRODUCT_SALE";
                    string query2 = "SELECT PNAME, SALESPRICE FROM PRODUCT_SALE";
                    OracleCommand cmd1 = new OracleCommand(query1, conn);
                    OracleCommand cmd2 = new OracleCommand(query2, conn);

                    OracleDataReader dataReader1 = cmd1.ExecuteReader();
                    OracleDataReader dataReader2 = cmd2.ExecuteReader();

                    chart1.Series["SALESQUANTITY"].Points.DataBind(dataReader1, "PNAME", "SALESQUANTITY", "Tooltip=SALESQUANTITY");
                    chart1.Series["SALESPRICE"].Points.DataBind(dataReader2, "PNAME", "SALESPRICE", "Tooltip=SALESPRICE");
                    //MessageBox.Show("버튼 실행 및 using문 실행됨");

                    string query = "SELECT PCODE, PNAME, PPRICE, SALESQUANTITY, SALESPRICE FROM PRODUCT_SALE";
                    OracleDataAdapter adapter = new OracleDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                    conn.Close(); //원래 없었음
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"에러: {ex.Message}");
                }
            }
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            //차트 속성
            Title title = new Title();
            chart1.Titles.Add(title);
            title.Text = "판매통계";
            title.Font = new Font("맑은고딕", 12, FontStyle.Bold);

            chart1.ForeColor = Color.White;
            chart1.BorderlineColor = Color.AliceBlue;
            chart1.BackColor = Color.Transparent;


            if (chart1.Titles.Count > 0)
            {
                chart1.Titles[0].ForeColor = Color.White;
            }

            // X축과 Y축의 라벨 색상 설정
            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[1].AxisX.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[1].AxisY.LabelStyle.ForeColor = Color.White;

            // X축과 Y축의 제목 색상 설정
            chart1.ChartAreas[0].AxisX.TitleForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.White;
            chart1.ChartAreas[1].AxisX.TitleForeColor = Color.White;
            chart1.ChartAreas[1].AxisY.TitleForeColor = Color.White;

            // X축과 Y축의 선 색상 설정
            chart1.ChartAreas[0].AxisX.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.LineColor = Color.White;
            chart1.ChartAreas[1].AxisX.LineColor = Color.White;
            chart1.ChartAreas[1].AxisY.LineColor = Color.White;

            if (chart1.Legends.Count > 0)
            {
                chart1.Legends[0].BackColor = Color.Black; // 범례의 배경을 검정색으로 설정
                chart1.Legends[0].ForeColor = Color.White; // 범례의 글자색을 흰색으로 설정
            }

            //gridview HeaderColor
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.RowHeadersDefaultCellStyle.ForeColor = Color.White;


        }


        private void button1_Click(object sender, EventArgs e)
        {
            Dbmssale();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.ForeColor = Color.Yellow;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White;
        }
    }
}
-----------------------------------------------------------------------------------------
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
        public bool smartFlag = false;
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
                cmd.CommandText = "COMMIT";
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
                        //ProductEa = int.Parse(numericUpDown1.Text);
                        cmd.CommandText = $"UPDATE Material SET PEA = '{ea}' WHERE PNAME = '{Dbname}'";
                        ea = 0;
                        cmd.ExecuteNonQuery();
                        alcoil.Checked = false;
                    }
                    if (gucoil.Checked == true && Dbname == "구리 코일")
                    {
                        ea = Dbea + ProductEa;
                        //ProductName = "구리 코일";
                        //ProductEa = int.Parse(numericUpDown1.Text);
                        cmd.CommandText = $"UPDATE Material SET PEA = '{ea}' WHERE PNAME = '{Dbname}'";
                        ea = 0;
                        cmd.ExecuteNonQuery();
                        gucoil.Checked = false;
                    }
                    if (core.Checked == true && Dbname == "코어")
                    {
                        ea = Dbea + ProductEa;
                        //ProductName = "코어";
                        //ProductEa = int.Parse(numericUpDown1.Text);
                        cmd.CommandText = $"UPDATE Material SET PEA = '{ea}' WHERE PNAME = '{Dbname}'";
                        ea = 0;
                        cmd.ExecuteNonQuery();
                        core.Checked = false;
                    }
                    if (case1.Checked == true && Dbname == "케이스")
                    {
                        if (smartFlag == true)
                        {
                            if (f2name == "15kVA")
                                ea = Dbea + ProductEa;
                            else if (f2name == "20kVA")
                                ea = Dbea + (ProductEa / 2);
                            else if (f2name == "50kVA")
                                ea = Dbea + (ProductEa / 3);
                            else if (f2name == "75kVA")
                                ea = Dbea + (ProductEa / 4);
                            else if (f2name == "100kVA")
                                ea = Dbea + (ProductEa / 5);
                        }
                        else
                        {
                            ea = Dbea + ProductEa;
                        }
                        //ProductName = "케이스";
                        //ProductEa = int.Parse(numericUpDown1.Text);
                        cmd.CommandText = $"UPDATE Material SET PEA = '{ea}' WHERE PNAME = '{Dbname}'";
                        ea = 0;
                        cmd.ExecuteNonQuery();
                        case1.Checked = false;
                        smartFlag = false;
                    }
                }
                cmd.CommandText = "COMMIT";
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
            cmd.CommandText = "COMMIT";
            conn.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            upFlag = true;
            checkFlag = true;
            addFlag = false;

            Dbms(); //재료 업뎃 이상함
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            //gridview HeaderColor
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.Black;
            dataGridView1.RowHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            upFlag = true;
            addFlag = true;
            checkFlag = false;
            dataGridView1.Rows.Clear();

            Dbms();
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.ForeColor = Color.Yellow;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.White;
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            button2.ForeColor = Color.Yellow;
        }

        private void oil_CheckedChanged(object sender, EventArgs e)
        {
            if (oil.Checked)
            {
                oil.ForeColor = Color.Yellow;
            }
            else
            {
                oil.ForeColor = SystemColors.Control; // 체크 해제 시 기본 색상으로 되돌림
            }
        }

        private void alcoil_CheckedChanged(object sender, EventArgs e)
        {
            if (alcoil.Checked)
            {
                alcoil.ForeColor = Color.Yellow;
            }
            else
            {
                alcoil.ForeColor = SystemColors.Control; // 체크 해제 시 기본 색상으로 되돌림
            }
        }

        private void gucoil_CheckedChanged(object sender, EventArgs e)
        {
            if (gucoil.Checked)
            {
                gucoil.ForeColor = Color.Yellow;
            }
            else
            {
                gucoil.ForeColor = SystemColors.Control; // 체크 해제 시 기본 색상으로 되돌림
            }
        }

        private void core_CheckedChanged(object sender, EventArgs e)
        {
            if (core.Checked)
            {
                core.ForeColor = Color.Yellow;
            }
            else
            {
                core.ForeColor = SystemColors.Control; // 체크 해제 시 기본 색상으로 되돌림
            }
        }

        private void case1_CheckedChanged(object sender, EventArgs e)
        {
            if (case1.Checked)
            {
                case1.ForeColor = Color.Yellow;
            }
            else
            {
                case1.ForeColor = SystemColors.Control; // 체크 해제 시 기본 색상으로 되돌림
            }
        }
    }
}
-------------------------------------------------------------------------------------------
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
    public partial class Form7 : Form   // 관리자 명단 화면
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form7_Load(object sender, EventArgs e)
        {
            dataGridView2.Columns.Add("Role", "역할");
            dataGridView2.Columns.Add("No", "학번");
            dataGridView2.Columns.Add("Name", "이름");

            dataGridView2.Rows.Add("계획서,통계", "20210714", "유다은");
            dataGridView2.Rows.Add("전체 설계, DB연동", "20201071", "박상원");
            dataGridView2.Rows.Add("다이어그램, 애니메이션", "20181021", "이형찬");
            dataGridView2.Rows.Add("로그인, 총괄 디자인", "20191059", "채호성");
            dataGridView2.Rows.Add("자료수집, 총괄 보조", "20181024", "전일준");
        }
    }
}


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

using Oracle.ManagedDataAccess.Client;
using System.Drawing;

namespace WinFormsApp14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {


            string strConn = "Data Source=(DESCRIPTION=" +
                    "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
                    "(HOST=localhost)(PORT=9000)))" +
                    "(CONNECT_DATA=(SERVER=DEDICATED)" +
                    "(SERVICE_NAME=xe)));" +
                    "User Id=SCOTT2;Password=TIGER;";



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

                            Form3 frm = new Form3(this, "Hello");
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
            Form2 frm2 = new Form2(this, "안녕하세요");
            frm2.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(@"C:\Users\Admin\Desktop\zzal\004.png");
        }
    }
}

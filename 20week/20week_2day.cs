using Oracle.ManagedDataAccess.Client;

namespace OracleWinFormsAppTest01
{
    public partial class Form1 : Form
    {
        static string strConn = "Data Source=(DESCRIPTION=" +
                "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
                "(HOST=localhost)(PORT=1521)))" +
                "(CONNECT_DATA=(SERVER=DEDICATED)" +
                "(SERVICE_NAME=xe)));" +
                "User Id=scott;Password=tiger;";

        //1.연결 객체 만들기 - Client
        static OracleConnection conn;
        static OracleCommand cmd;
        static int id = 0;
        public Form1()
        {
            InitializeComponent();
            updateTextBox.MouseClick += UpdateTextBox_MouseClick;
            conn = new OracleConnection(strConn);
            //2.데이터베이스 접속을 위한 연결
            conn.Open();

            //3.서버와 함께 신나게 놀기

            //명령객체 생성
            cmd = conn.CreateCommand();
            cmd.Connection = conn;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateTable(cmd);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void insertButton_Click(object sender, EventArgs e)
        {

            InsertTable(cmd, id);
        }
        static void CreateTable(OracleCommand cmd)
        {
            //삭제문
            //cmd.CommandText = "DROP TABLE NOODLEFACTORY";
            //cmd.ExecuteNonQuery();
            //cmd.CommandText = "DROP TABLE ACENOODLE";
            //cmd.ExecuteNonQuery();

            //테이블 생성문
            //cmd.CommandText = "CREATE TABLE NOODLEFACTORY(" +
            //                  "ID       NUMBER NOT NULL PRIMARY KEY, " +
            //                  "NAME     VARCHAR2(20) NOT NULL, " +
            //                  "PRICE    VARCHAR2(20), " +
            //                  "DOM      VARCHAR2(20), " +
            //                  "CAL      VARCHAR2(20))";
            //cmd.ExecuteNonQuery();

            //cmd.CommandText = "CREATE TABLE ACENOODLE(" +
            //                  "ID       NUMBER NOT NULL PRIMARY KEY, " +
            //                  "NAME     VARCHAR2(20) NOT NULL, " +
            //                  "PRICE    VARCHAR2(20), " +
            //                  "DOM      VARCHAR2(20), " +
            //                  "CAL      VARCHAR2(20))";
            //cmd.ExecuteNonQuery();
        }
        void InsertTable(OracleCommand cmd, int id)
        {
            //라면 제조, 생성문
            id = 1; string name = "", price = "", dom = "", cal = "";

            cmd.CommandText = "SELECT * FROM NOODLEFACTORY ORDER BY ID";
            cmd.ExecuteNonQuery();

            OracleDataReader rdr3 = cmd.ExecuteReader();
            while (rdr3.Read())
            {
                int rid = int.Parse(rdr3["ID"].ToString());
                if (rid == id)
                    id++;
            }
            name = pNameTextBox.Text; price = pPriceTextBox.Text; dom = pDomTextBox.Text; cal = pCalTextBox.Text;
            //Console.Write("\n라면 이름 : "); name = Console.ReadLine();
            //Console.Write("\n라면 가격 : "); price = Console.ReadLine();
            //Console.Write("\n라면 제조일 : "); dom = Console.ReadLine();
            //Console.Write("\n라면 칼로리 : "); cal = Console.ReadLine();
            cmd.CommandText = "INSERT INTO NOODLEFACTORY(ID, NAME, PRICE, DOM, CAL) " +
                              $"VALUES ({id}, '{name}', '{price}', '{dom}', '{cal}')";
            cmd.ExecuteNonQuery();
            id = 1;
            screenMonitor.Text = "제품 생성 완료";
            pNameTextBox.Text = ""; pPriceTextBox.Text = ""; pDomTextBox.Text = ""; pCalTextBox.Text = "";
        }
        void DeleteTableValue(OracleCommand cmd)
        {
            //라면 제품 삭제
            cmd.CommandText = "SELECT * FROM NOODLEFACTORY ORDER BY ID";
            cmd.ExecuteNonQuery();

            int delete = int.Parse(deleteTextBox.Text);
            cmd.CommandText = $"DELETE FROM NOODLEFACTORY WHERE ID = {delete}";
            cmd.ExecuteNonQuery();
            screenMonitor.Text = "제품 삭제 완료";
            deleteTextBox.Text = "";
        }
        void TableSearch(OracleCommand cmd)
        {
            //제품검색
            cmd.CommandText = "SELECT * FROM NOODLEFACTORY ORDER BY ID";
            cmd.ExecuteNonQuery();

            OracleDataReader rdr = cmd.ExecuteReader();

            List<Product> products = new List<Product>();


            while (rdr.Read())
            {
                int rid = int.Parse(rdr["ID"].ToString());
                string rname = rdr["NAME"] as string;
                string rprice = rdr["PRICE"] as string;
                string rdom = rdr["DOM"] as string;
                string rcal = rdr["CAL"] as string;

                products.Add(new Product { Id = rid, Name = rname, Price = rprice, Dom = rdom, Cal = rcal });
            }
            dataGridView1.DataSource = products;
        }
        void UpdateTable(OracleCommand cmd)
        {
            //라면제품 수정
            cmd.CommandText = "SELECT * FROM NOODLEFACTORY ORDER BY ID";
            cmd.ExecuteNonQuery();
            
            //Console.Write("수정할 제품 번호입력 : ");
            int upId = int.Parse(updateTextBox.Text);

            int upnum = int.Parse(updateNumTextBox.Text);

            string update;
            switch (upnum)
            {
                case 1:
                    //Console.Write("새로운 제품명 : ");
                    update = updateInfoTextBox.Text;
                    cmd.CommandText = "UPDATE NOODLEFACTORY " +
                                     $"SET NAME = '{update}'" +
                                     $"WHERE ID = {upId}";
                    cmd.ExecuteNonQuery();
                    screenMonitor.Text = "수정완료";
                    break;
                case 2:
                    //Console.Write("새로운 제품가격 : ");
                    update = updateInfoTextBox.Text;
                    cmd.CommandText = "UPDATE NOODLEFACTORY " +
                                     $"SET PRICE = '{update}'" +
                                     $"WHERE ID = {upId}";
                    cmd.ExecuteNonQuery();
                    screenMonitor.Text = "수정완료";
                    break;
                case 3:
                    //Console.Write("새로운 제품 제조일 : ");
                    update = updateInfoTextBox.Text;
                    cmd.CommandText = "UPDATE NOODLEFACTORY " +
                                     $"SET DOM = '{update}'" +
                                     $"WHERE ID = {upId}";
                    cmd.ExecuteNonQuery();
                    screenMonitor.Text = "수정완료";
                    break;
                case 4:
                    //Console.Write("새로운 제품칼로리 : ");
                    update = updateInfoTextBox.Text;
                    cmd.CommandText = "UPDATE NOODLEFACTORY " +
                                     $"SET CAL = '{update}'" +
                                     $"WHERE ID = {upId}";
                    cmd.ExecuteNonQuery();
                    screenMonitor.Text = "수정완료";
                    break;
                default:
                    screenMonitor.Text = "오류났어용!!";
                    break;
            }//end of update switch
            updateTextBox.Text = ""; updateNumTextBox.Text = ""; updateInfoTextBox.Text = "";
        }
        private void UpdateTextBox_MouseClick(object sender, MouseEventArgs e)
        {
            screenMonitor.Text = "수정할 속성 1: 제품명, 2: 제품가격, 3: 제품 제조일, 4: 제품 칼로리";
        }
        private void end_Click(object sender, EventArgs e)
        {
            //4. 리소스 반환 및 종료
            conn.Close();
            Dispose();
        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
            TableSearch(cmd);
            screenMonitor.Text = "테이블 검색 완료";
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DeleteTableValue(cmd);
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            UpdateTable(cmd);
        }
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Dom {  get; set; }
        public string Cal {  get; set; }
    }
}
============================
using Oracle.ManagedDataAccess.Client;
using OracleWinFormsAppTest01;

namespace login_01

{
    public partial class Login : Form
    {
        private string connectionString = "User Id=scott;Password=tiger;" +
                  "Data Source=(DESCRIPTION=" +
                  "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
                  "(HOST=Localhost)(PORT=1521)))" +
                  "(CONNECT_DATA=(SERVER=DEDICATED)" +
                  "(SERVICE_NAME=xe)));";
        public Login()
        {
            InitializeComponent();
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string UserID = txtUserID.Text;
            string Passwd = txtPassWord.Text;

            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    string loginQuery = "SELECT COUNT(*) FROM ACCOUNT WHERE userid = :userid AND passwd = :passwd";

                    using (OracleCommand command = new OracleCommand(loginQuery, connection))
                    {
                        //테이블 생성문
                        //command.commandtext = "create table account(" +
                        //                  "userid    varchar2(20) not null primary key, " +
                        //                  "username  varchar2(20) not null, " +
                        //                  "passwd    varchar2(20) not null" + ")";
                        //command.executenonquery();
                        

                        command.Parameters.Add(new OracleParameter("userid", UserID));
                        command.Parameters.Add(new OracleParameter("passwd", Passwd));

                        int userCount = Convert.ToInt32(command.ExecuteScalar());

                        if (userCount > 0)
                        {
                            MessageBox.Show("로그인 성공!");
                            // 메인 폼으로 이동 또는 다른 동작 수행
                            Form1 showform2 = new Form1();
                            showform2.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("아이디 또는 비밀번호가 잘못되었습니다.");
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("로그인 중 데이터베이스 오류가 발생했습니다: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("로그인 중 오류가 발생했습니다: " + ex.Message);
            }
        }


        private void btnSignUp_Click(object sender, EventArgs e)
        {
            OracleConnection connection = new OracleConnection(connectionString);
            connection.Open();
            using (OracleCommand cmd = connection.CreateCommand())
            {
                cmd.Connection = connection;
                cmd.CommandText = "create table account(" +
                              "userid    varchar2(20) not null primary key, " +
                              "username  varchar2(20) not null, " +
                              "passwd    varchar2(20) not null" + ")";
                cmd.ExecuteNonQuery();
            }
            SignUp showform2 = new SignUp();
            showform2.ShowDialog();
        }
        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            // chkShowPassword가 체크된 경우 비밀번호를 보이게 하고, 체크 해제되면 숨깁니다.
            txtPassWord.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
    }
}

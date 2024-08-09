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

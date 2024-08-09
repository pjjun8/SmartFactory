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

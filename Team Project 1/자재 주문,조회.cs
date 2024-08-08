using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp6
{
    public partial class Ordermatter : Form
    {
        OracleConnection conn;
        OracleDataReader rdr;
        OracleDataAdapter adapter;
        DataTable dataTable;

        public Ordermatter()
        {
            InitializeComponent();
            oracle();
        }
        public void oracle()
        {
            string strConn = "Data Source=(DESCRIPTION=" +
                "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
                "(HOST=localhost)(PORT=1521)))" +
                "(CONNECT_DATA=(SERVER=DEDICATED)" +
                "(SERVICE_NAME=xe)));" +
                "User Id=scott2;Password=tiger;";
            //1.연결 객체 만들기 - Client
            conn = new OracleConnection(strConn);

            adapter = new OracleDataAdapter("SELECT * FROM MAT_TABLE", conn);

            // Initialize data table
            dataTable = new DataTable();
            try
            {
                // Open the connection
                conn.Open();

                // Fill the data table with data from the database
                adapter.Fill(dataTable);

                // Display data in DataGridView
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection
                conn.Close();
            }
        }
        private void Ordermatter_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("구리");
            comboBox1.Items.Add("알루미늄");
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string command = $"UPDATE MAT_TABLE SET QUANTITY = QUANTITY + {textBox1.Text} WHERE NAME='{comboBox1.Text}'";
            string strConn = "Data Source=(DESCRIPTION=" +
                "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
                "(HOST=localhost)(PORT=1521)))" +
                "(CONNECT_DATA=(SERVER=DEDICATED)" +
                "(SERVICE_NAME=xe)));" +
                "User Id=scott2;Password=tiger;";
            //1.연결 객체 만들기 - Client
            conn = new OracleConnection(strConn);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = command;
            cmd.ExecuteNonQuery();
            MessageBox.Show("정상적으로 행이 삽입되었습니다 !");
            oracle();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}

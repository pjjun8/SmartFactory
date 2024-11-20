using OPlusBestFUI.Property;
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

namespace OPlusBestFUI
{
    public partial class MainForm : Form
    {
        static string strConn = "Data Source=(DESCRIPTION=" +
                "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
                "(HOST=localhost)(PORT=1521)))" +
                "(CONNECT_DATA=(SERVER=DEDICATED)" +
                "(SERVICE_NAME=xe)));" +
                "User Id=scott;Password=tiger;";

        //1.연결 객체 만들기 - Client
        static OracleConnection conn = new OracleConnection(strConn);

        //3.서버와 함께 신나게 놀기

        //명령객체 생성
        OracleCommand cmd = conn.CreateCommand();
        
        public MainForm()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //2.데이터베이스 접속을 위한 연결
            conn.Open();
            cmd.Connection = conn;
            TableSearch(cmd);
        }
        private void mainButton_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void workOrderDateSearchButton_Click(object sender, EventArgs e)
        {
            TableSearch(cmd);
        }
        void TableSearch(OracleCommand cmd)
        {
            //제품검색
            if (processSelectionComboBox.Text == "전체")
            {
                cmd.CommandText = "SELECT * FROM WORKORDER ORDER BY WOID";
            }
            else if (processSelectionComboBox.Text == "믹싱")
            {
                cmd.CommandText = "SELECT * FROM WORKORDER WHERE PROCID = 'PC001' ORDER BY WOID";
            }
            else if (processSelectionComboBox.Text == "코팅")
            {
                cmd.CommandText = "SELECT * FROM WORKORDER WHERE PROCID = 'PC002' ORDER BY WOID";
            }
            else if (processSelectionComboBox.Text == "슬리팅")
            {
                cmd.CommandText = "SELECT * FROM WORKORDER WHERE PROCID = 'PC003' ORDER BY WOID";
            }
            cmd.ExecuteNonQuery();

            OracleDataReader rdr = cmd.ExecuteReader();

            List<workOrder> workOrders = new List<workOrder>();

            while (rdr.Read())
            {
                string woid = rdr["WOID"] as string;
                string wostat = rdr["WOSTAT"] as string;

                // TIMESTAMP 필드를 nullable DateTime으로 처리
                Convert.ToDateTime(rdr["PLANDTTM"]).ToString("yyyy-MM-dd HH:mm:ss");
                Convert.ToDateTime(rdr["WOSTDTTM"]).ToString("yyyy-MM-dd HH:mm:ss");
                Convert.ToDateTime(rdr["WOEDDTTM"]).ToString("yyyy-MM-dd HH:mm:ss");
                Convert.ToDateTime(rdr["INSDTTM"]).ToString("yyyy-MM-dd HH:mm:ss");
                DateTime? plandttm = rdr["PLANDTTM"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["PLANDTTM"]);
                DateTime? wostdttm = rdr["WOSTDTTM"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["WOSTDTTM"]);
                DateTime? woeddttm = rdr["WOEDDTTM"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["WOEDDTTM"]);
                DateTime? insdttm = rdr["INSDTTM"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["INSDTTM"]);

                string prodid = rdr["PRODID"] as string;
                string planqty = rdr["PLANQTY"] as string;
                string prodqty = rdr["PRODQTY"] as string;
                string procid = rdr["PROCID"] as string;
                string userid = rdr["USERID"] as string;

                workOrders.Add(new workOrder
                {
                    WoId = woid,
                    WoStat = wostat,
                    PlanDttm = plandttm,
                    WoStDttm = wostdttm,
                    WoEdDttm = woeddttm,
                    ProdId = prodid,
                    PlanQty = planqty,
                    ProdQty = prodqty,
                    ProcId = procid,
                    InsDttm = insdttm,
                    UserId = userid
                });

            }//end of while (rdr.Read())

            WorkOrderDataGridView1.DataSource = workOrders;

            // 컬럼명 변경
            WorkOrderDataGridView1.Columns[0].HeaderText = "작업지시ID";
            WorkOrderDataGridView1.Columns[1].HeaderText = "공정상태";
            WorkOrderDataGridView1.Columns[2].HeaderText = "계획일자";
            WorkOrderDataGridView1.Columns[3].HeaderText = "작업시작일";
            WorkOrderDataGridView1.Columns[4].HeaderText = "작업완료일";
            WorkOrderDataGridView1.Columns[5].HeaderText = "제품코드";
            WorkOrderDataGridView1.Columns[6].HeaderText = "계획수량";
            WorkOrderDataGridView1.Columns[7].HeaderText = "생산한수량(무시)";
            WorkOrderDataGridView1.Columns[8].HeaderText = "공정";
            WorkOrderDataGridView1.Columns[9].HeaderText = "작업지시일자";
            WorkOrderDataGridView1.Columns[10].HeaderText = "지시자ID";
        }//end of TableSearch()
    }//end of class MainForm
}//end of OPlusBestFUI
-----------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPlusBestFUI.Property
{
    internal class workOrder
    {
        public string WoId { get; set; }
        public string WoStat { get; set; }
        public DateTime? PlanDttm { get; set; } // Nullable DateTime
        public DateTime? WoStDttm { get; set; }
        public DateTime? WoEdDttm { get; set; }
        public string ProdId { get; set; }
        public string PlanQty { get; set; }
        public string ProdQty { get; set; }
        public string ProcId { get; set; }
        public DateTime? InsDttm { get; set; }
        public string UserId { get; set; }
    }
}

using OPlusBestFUI.Property;
using OPlusBestFUI.DB;
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
        Details detailsForm;
        BOM bom;
        //static string strConn = "Data Source=(DESCRIPTION=" +
        //        "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
        //        "(HOST=localhost)(PORT=1521)))" +
        //        "(CONNECT_DATA=(SERVER=DEDICATED)" +
        //        "(SERVICE_NAME=xe)));" +
        //        "User Id=scott;Password=tiger;";

        ////1.연결 객체 만들기 - Client
        //static OracleConnection conn = new OracleConnection(strConn);

        ////3.서버와 함께 신나게 놀기

        ////명령객체 생성
        //OracleCommand cmd = conn.CreateCommand();
        
        public MainForm()
        {
            InitializeComponent();
            detailsForm = new Details();
            bom = new BOM();
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //2.데이터베이스 접속을 위한 연결
            oracleDb.conn.Open();
            oracleDb.cmd.Connection = oracleDb.conn;
            TableSearch(oracleDb.cmd);
            //WorkOrderDataGridView1.CellClick += WorkOrderDataGridView1_CellClick;

        }
        private void mainButton_Click(object sender, EventArgs e)
        {
            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            oracleDb.conn.Close();
            Application.Exit();
        }

        private void workOrderDateSearchButton_Click(object sender, EventArgs e)
        {
            TableSearch(oracleDb.cmd);
        }
        public void TableSearch(OracleCommand cmd)
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
                string prodName = rdr["PRODNAME"] as string;
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
                    ProdName = prodName,
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
            WorkOrderDataGridView1.Columns[6].HeaderText = "제품명";
            WorkOrderDataGridView1.Columns[7].HeaderText = "계획수량";
            WorkOrderDataGridView1.Columns[8].HeaderText = "생산한수량(무시)";
            WorkOrderDataGridView1.Columns[9].HeaderText = "공정";
            WorkOrderDataGridView1.Columns[10].HeaderText = "작업지시일자";
            WorkOrderDataGridView1.Columns[11].HeaderText = "지시자ID";
        }//end of TableSearch()

        private void detailsButton_Click(object sender, EventArgs e)
        {
            detailsForm.ShowDialog();
        }

        private void bomButton_Click(object sender, EventArgs e)
        {
            bom.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void logoPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void rawMaterialSearchButton_Click(object sender, EventArgs e)
        {

        }

        private void productionStatusButton_Click(object sender, EventArgs e)
        {

        }

        private void productionLogButton_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void workStartButton_Click(object sender, EventArgs e)
        {

        }

        private void workStopButton_Click(object sender, EventArgs e)
        {

        }

        private void processSelectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void workCompletedButton_Click(object sender, EventArgs e)
        {

        }

        private void processSelectionLabel_Click(object sender, EventArgs e)
        {

        }

        private void workOrderDateLabel_Click(object sender, EventArgs e)
        {

        }

        private void workOrderDateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void workOrderDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void WorkOrderDataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void WorkOrderDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // 유효한 행인지 확인
            {
                // DataGridView에서 행을 가져옴
                DataGridViewRow selectedRow = WorkOrderDataGridView1.Rows[e.RowIndex];

                // 열 이름이 정확한지 확인 후 데이터 가져오기
                try
                {
                    string woId = selectedRow.Cells["WOID"].Value?.ToString();
                    string woStat = selectedRow.Cells["WOSTAT"].Value?.ToString();
                    string planDttm = selectedRow.Cells["PLANDTTM"].Value?.ToString();
                    string woStDttm = selectedRow.Cells["WOSTDTTM"].Value?.ToString();
                    string woEdDttm = selectedRow.Cells["WOEDDTTM"].Value?.ToString();
                    string prodId = selectedRow.Cells["PRODID"].Value?.ToString();
                    string prodName = selectedRow.Cells["PRODNAME"].Value?.ToString();
                    //아마 데이터 다 안넣어줘서 오류나는듯??
                    // Details 폼에 데이터 전달
                    detailsForm.LoadSelectedRowData(
                        woId, woStat,
                        string.IsNullOrEmpty(planDttm) ? (DateTime?)null : DateTime.Parse(planDttm),
                        string.IsNullOrEmpty(woStDttm) ? (DateTime?)null : DateTime.Parse(woStDttm),
                        string.IsNullOrEmpty(woEdDttm) ? (DateTime?)null : DateTime.Parse(woEdDttm),
                        prodId, prodName
                    );

                    detailsForm.ShowDialog();
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show($"DataGridView 열 이름을 확인하세요.\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }//end of class MainForm
}//end of OPlusBestFUI
========================================================
using OPlusBestFUI.DB;
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
using static System.Net.Mime.MediaTypeNames;

namespace OPlusBestFUI
{
    public partial class BOM : Form
    {
        public BOM()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BOM_Load(object sender, EventArgs e)
        {
            //oracleDb.conn.Open();
        }
        private void searchButton_Click(object sender, EventArgs e)
        {
            int prodQty = int.Parse(QTYTextBox.Text);   //제품 수량
            string prodCodeText = prodCodeComboBox.Text;    //제품 코드
            string prodNameText = prodNameComboBox.Text;    //제품 이름

            // 컨트롤 초기화
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox tb = (TextBox)ctrl;
                    if (tb != null)
                    {
                        tb.Text = string.Empty;
                    }
                }
                else if (ctrl is ComboBox)
                {
                    ComboBox dd = (ComboBox)ctrl;
                    if (dd != null)
                    {
                        dd.Text = string.Empty;
                        dd.SelectedIndex = -1;
                    }
                }
            }
            QTYTextBox.Text = prodQty.ToString();
            prodCodeComboBox.Text = prodCodeText.ToString();
            prodNameComboBox.Text = prodNameText.ToString();

            if (prodCodeComboBox.Text == "P001" || prodNameComboBox.Text == "NCM양극판 80")
            {
                int psm002 = prodQty / 3;   //완제품 반제품 비율 3:1
                int psm001 = psm002;        // 반제품 비율 1:1
                NCM믹싱반제품TextBox.Text = $"{psm001}";
                NCM코팅반제품TextBox.Text = $"{psm002}";
            }
            else if (prodCodeComboBox.Text == "P002" || prodNameComboBox.Text == "LFP양극판 80")
            {
                int psm004 = prodQty / 3;   //완제품 반제품 비율 3:1
                int psm003 = psm004;        // 반제품 비율 1:1
                LFP믹싱반제품TextBox.Text = $"{psm003}";
                LFP코팅반제품TextBox.Text = $"{psm004}";
            }
            else if (prodCodeComboBox.Text == "P003" || prodNameComboBox.Text == "음극판 80")
            {
                int msm002 = prodQty / 3;   //완제품 반제품 비율 3:1
                int msm001 = msm002;        // 반제품 비율 1:1
                음극판믹싱반제품TextBox.Text = $"{msm001}";
                음극판코팅반제품TextBox.Text = $"{msm002}";
            }
            else if (prodCodeComboBox.Text == "PSM001" || prodNameComboBox.Text == "NCM믹싱반제품")
            {
                int m001 = prodQty / 10;   
                int m002 = prodQty / 10;
                int m003 = prodQty / 5 * 3;
                int m004 = prodQty / 10;
                int m005 = prodQty / 10;
                CarbonBlackTextBox.Text = $"{m001}";
                PVDFTextBox.Text = $"{m002}";
                NMPTextBox.Text = $"{m003}";
                CNTTextBox.Text = $"{m004}";
                NCMTextBox.Text = $"{m005}";
            }
            else if (prodCodeComboBox.Text == "PSM002" || prodNameComboBox.Text == "NCM코팅반제품")
            {
                int psm001 = prodQty;
                int m008 = prodQty;
                NCM믹싱반제품TextBox.Text = $"{psm001}";
                알루미늄호일TextBox.Text = $"{m008}";
            }
            else if (prodCodeComboBox.Text == "PSM003" || prodNameComboBox.Text == "LFP믹싱반제품")
            {
                int m001 = prodQty / 10;
                int m002 = prodQty / 10;
                int m003 = prodQty / 5 * 3;
                int m004 = prodQty / 10;
                int m006 = prodQty / 10;
                CarbonBlackTextBox.Text = $"{m001}";
                PVDFTextBox.Text = $"{m002}";
                NMPTextBox.Text = $"{m003}";
                CNTTextBox.Text = $"{m004}";
                LFPTextBox.Text = $"{m006}";
            }
            else if (prodCodeComboBox.Text == "PSM004" || prodNameComboBox.Text == "LFP코팅반제품")
            {
                int psm003 = prodQty;
                int m008 = prodQty;
                LFP믹싱반제품TextBox.Text = $"{psm003}";
                알루미늄호일TextBox.Text = $"{m008}";
            }
            else if (prodCodeComboBox.Text == "MSM001" || prodNameComboBox.Text == "음극판믹싱반제품")
            {
                int m002 = prodQty / 10;
                int m003 = prodQty / 10*7;
                int m004 = prodQty / 10;
                int m007 = prodQty / 10;
                PVDFTextBox.Text = $"{m002}";
                NMPTextBox.Text = $"{m003}";
                CNTTextBox.Text = $"{m004}";
                인조흑연TextBox.Text = $"{m007}";
            }
            else if (prodCodeComboBox.Text == "MSM002" || prodNameComboBox.Text == "음극판코팅반제품")
            {
                int msm001 = prodQty;
                int m009 = prodQty;
                음극판믹싱반제품TextBox.Text = $"{msm001}";
                구리호일TextBox.Text = $"{m009}";
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
=========================================================
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
    public partial class Details : Form
    {
        public Details()
        {
            InitializeComponent();
        }
        // 데이터 로드 메서드
        public void LoadSelectedRowData(string woId, string woStat, DateTime? planDttm, DateTime? woStDttm, DateTime? woEdDttm, string prodId, string prodName)
        {
            // 선택된 데이터를 DataGridView에 추가
            dataGridView1.Rows.Clear(); // 기존 데이터 초기화
            dataGridView1.Rows.Add(
                woId,
                woStat,
                planDttm?.ToString("yyyy-MM-dd HH:mm:ss"),
                woStDttm?.ToString("yyyy-MM-dd HH:mm:ss"),
                woEdDttm?.ToString("yyyy-MM-dd HH:mm:ss"),
                prodId,
                prodName
            );
        }

        private void Details_Load(object sender, EventArgs e)
        {
            // DataGridView 컬럼 초기화
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("WoId", "작업지시ID");
            dataGridView1.Columns.Add("WoStat", "공정상태");
            dataGridView1.Columns.Add("PlanDttm", "계획일자");
            dataGridView1.Columns.Add("WoStDttm", "작업시작일");
            dataGridView1.Columns.Add("WoEdDttm", "작업완료일");
            dataGridView1.Columns.Add("ProdId", "제품코드");
            dataGridView1.Columns.Add("ProdName", "제품명");
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
===================================================================
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPlusBestFUI.DB
{
    public class oracleDb
    {
        static string strConn = "Data Source=(DESCRIPTION=" +
                "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)" +
                "(HOST=localhost)(PORT=1521)))" +
                "(CONNECT_DATA=(SERVER=DEDICATED)" +
                "(SERVICE_NAME=xe)));" +
                "User Id=scott;Password=tiger;";

        //1. 연결 객체 만들기 - Client
        public static OracleConnection conn = new OracleConnection(strConn);

        //3.서버와 함께 신나게 놀기
        // 명령 객체 생성
        public static OracleCommand cmd = conn.CreateCommand();
    }
}
======================================================================
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
        public string ProdName { get; set; }
        public string PlanQty { get; set; }
        public string ProdQty { get; set; }
        public string ProcId { get; set; }
        public DateTime? InsDttm { get; set; }
        public string UserId { get; set; }
    }
}


//
using OPlusBestFUI.DB;
using OPlusBestFUI.ProcessForm;
using OPlusBestFUI.Property;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPlusBestFUI
{
    public partial class Details : Form
    {
        public static string WOID;
        public static string LOTID;
        //NCMForm nCMForm;
        public Details()
        {
            InitializeComponent();
            //WOID = MainForm.WOID;

        }
        // 데이터 로드 메서드
        

        public void Details_Load(object sender, EventArgs e)
        {
            //string[] formList = { "NCMMixingForm"};
            //string[] prodidList = { "P001", "P002", "P003", "PSM001", "PSM002", "PSM003", "PSM004", "MSM001", "MSM002" };
            WOID = MainForm.WOID;
            ProcessSelect();
            LotSelect();
        }
        public void ProcessSelect()
        {
            

            WOID = MainForm.WOID;
            oracleDb.cmd.CommandText = $"SELECT PRODID FROM WORKORDER WHERE WOID = '{WOID}'";
            oracleDb.cmd.ExecuteNonQuery();

            OracleDataReader rdr = oracleDb.cmd.ExecuteReader();
            string prodid = "";
            while (rdr.Read())
            {
                prodid = rdr["PRODID"] as string;
            }

            this.panel2.Controls.Clear();
            NCMForm nCMForm = new NCMForm(MainForm.detailsForm, prodid);
            nCMForm.TopLevel = false;
            this.Controls.Add(nCMForm);
            nCMForm.Parent = this.panel2;
            nCMForm.ControlBox = false;
            nCMForm.Show();

            //switch (prodid)
            //{
            //    case "P001":
            //        this.panel2.Controls.Clear();
            //        NCMForm nCMForm = new NCMForm(MainForm.detailsForm, prodid);
            //        nCMForm.TopLevel = false;
            //        this.Controls.Add(nCMForm);
            //        nCMForm.Parent = this.panel2;
            //        nCMForm.ControlBox = false;
            //        nCMForm.Show();
            //        break;
            //    case "P002":
            //        this.panel2.Controls.Clear();
            //        LFPForm lFPForm = new LFPForm(MainForm.detailsForm);
            //        lFPForm.TopLevel = false;
            //        this.Controls.Add(lFPForm);
            //        lFPForm.Parent = this.panel2;
            //        lFPForm.ControlBox = false;
            //        lFPForm.Show();
            //        break;
            //    case "P003":
            //        this.panel2.Controls.Clear();
            //        MSForm msForm = new MSForm();
            //        msForm.TopLevel = false;
            //        this.Controls.Add(msForm);
            //        msForm.Parent = this.panel2;
            //        msForm.ControlBox = false;
            //        msForm.Show();
            //        break;
            //    case "PSM001":
            //        this.panel2.Controls.Clear();
            //        NCMMixingForm ncmMixingForm = new NCMMixingForm();
            //        ncmMixingForm.TopLevel = false;
            //        this.Controls.Add(ncmMixingForm);
            //        ncmMixingForm.Parent = this.panel2;
            //        ncmMixingForm.ControlBox = false;
            //        ncmMixingForm.Show();
            //        break;
            //    case "PSM002":
            //        this.panel2.Controls.Clear();
            //        NCMCoatingForm ncmCoatingForm = new NCMCoatingForm();
            //        ncmCoatingForm.TopLevel = false;
            //        this.Controls.Add(ncmCoatingForm);
            //        ncmCoatingForm.Parent = this.panel2;
            //        ncmCoatingForm.ControlBox = false;
            //        ncmCoatingForm.Show();
            //        break;
            //    case "PSM003":
            //        this.panel2.Controls.Clear();
            //        LFPMixingForm lFPMixingForm = new LFPMixingForm();
            //        lFPMixingForm.TopLevel = false;
            //        this.Controls.Add(lFPMixingForm);
            //        lFPMixingForm.Parent = this.panel2;
            //        lFPMixingForm.ControlBox = false;
            //        lFPMixingForm.Show();
            //        break;
            //    case "PSM004":
            //        this.panel2.Controls.Clear();
            //        LFPCoatingForm lFPCoatingForm = new LFPCoatingForm();
            //        lFPCoatingForm.TopLevel = false;
            //        this.Controls.Add(lFPCoatingForm);
            //        lFPCoatingForm.Parent = this.panel2;
            //        lFPCoatingForm.ControlBox = false;
            //        lFPCoatingForm.Show();
            //        break;
            //    case "MSM001":
            //        this.panel2.Controls.Clear();
            //        MSMixingForm mSMixingForm = new MSMixingForm();
            //        mSMixingForm.TopLevel = false;
            //        this.Controls.Add(mSMixingForm);
            //        mSMixingForm.Parent = this.panel2;
            //        mSMixingForm.ControlBox = false;
            //        mSMixingForm.Show();
            //        break;
            //    case "MSM002":
            //        this.panel2.Controls.Clear();
            //        MSCoatingForm msCoatingForm = new MSCoatingForm();
            //        msCoatingForm.TopLevel = false;
            //        this.Controls.Add(msCoatingForm);
            //        msCoatingForm.Parent = this.panel2;
            //        msCoatingForm.ControlBox = false;
            //        msCoatingForm.Show();
            //        break;
            //    default:
            //        MessageBox.Show("오류가 났습니다.");
            //        break;
            //}
        }
        public void LotSelect()
        {
            //WOID = MainForm.WOID;
            oracleDb.cmd.CommandText = $"SELECT * FROM LOT WHERE WOID = '{WOID}' ORDER BY DECODE(LOTSTAT, 'S', 1, 'C', 2, 'E', 3, 'D', 4)";
            oracleDb.cmd.ExecuteNonQuery();

            OracleDataReader rdr = oracleDb.cmd.ExecuteReader();

            List<LOT> lot = new List<LOT>();

            while (rdr.Read())
            {
                string lotid = rdr["LOTID"] as string;
                string lotstat = rdr["LOTSTAT"] as string;

                // TIMESTAMP 필드를 nullable DateTime으로 처리
                //Convert.ToDateTime(rdr["LOTCRDTTM"]).ToString("yyyy-MM-dd HH:mm:ss");
                //Convert.ToDateTime(rdr["LOTSTDTTM"]).ToString("yyyy-MM-dd HH:mm:ss");
                //Convert.ToDateTime(rdr["LOTEDDTTM"]).ToString("yyyy-MM-dd HH:mm:ss");
                DateTime? lotcrdttm = rdr["LOTCRDTTM"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["LOTCRDTTM"]);
                DateTime? lotstdttm = rdr["LOTSTDTTM"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["LOTSTDTTM"]);
                DateTime? loteddttm = rdr["LOTEDDTTM"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["LOTEDDTTM"]);

                string woid = rdr["WOID"] as string;
                int lotcrqty = rdr["LOTCRQTY"] == DBNull.Value ? 0 : int.Parse(rdr["LOTCRQTY"].ToString());
                int lotqty = rdr["LOTQTY"] == DBNull.Value ? 0 : int.Parse(rdr["LOTQTY"].ToString());
                string eqptid = rdr["EQPTID"] as string;
                string workerid = rdr["WORKERID"] as string;

                lot.Add(new LOT
                {
                    LotId = lotid,
                    LotStat = lotstat,
                    LotCrDttm = lotcrdttm,
                    LotStDttm = lotstdttm,
                    LotEdDttm = loteddttm,
                    Woid = woid,
                    LotCrQty = lotcrqty,
                    LotQty = lotqty,
                    EqptId = eqptid,
                    WorkerId = workerid
                });
            }
            dataGridView2.DataSource = lot;
            dataGridView2.Columns[0].HeaderText = "LOTID";
            dataGridView2.Columns[1].HeaderText = "LOT상태";
            dataGridView2.Columns[2].HeaderText = "LOT생성일";
            dataGridView2.Columns[3].HeaderText = "LOT작업시작일";
            dataGridView2.Columns[4].HeaderText = "LOT작업완료일";
            dataGridView2.Columns[5].HeaderText = "작업지시ID";
            dataGridView2.Columns[6].HeaderText = "수량";
            dataGridView2.Columns[7].HeaderText = "생성수량";
            dataGridView2.Columns[8].HeaderText = "공정ID";
            dataGridView2.Columns[9].HeaderText = "작업자ID";
            //dataGridView2.Columns.Clear(); // 기존 컬럼 초기화
            //dataGridView2.Columns.Add("LotId", "LOTID");
            //dataGridView2.Columns.Add("LotStat", "LOT상태");
            //dataGridView2.Columns.Add("LotCrDttm", "LOT생성일");
            //dataGridView2.Columns.Add("LotStDttm", "LOT작업시작일");
            //dataGridView2.Columns.Add("LotEdDttm", "LOT작업완료일");
            //dataGridView2.Columns.Add("Woid", "작업지시ID");
            //dataGridView2.Columns.Add("LotCrQty", "수량");
            //dataGridView2.Columns.Add("LotQty", "생성수량");
            //dataGridView2.Columns.Add("EqptId", "공정ID");
            //dataGridView2.Columns.Add("WorkerId", "작업자ID");
        }
        public void LoadSelectedRowData(string woId, string woStat, DateTime? planDttm, DateTime? woStDttm, DateTime? woEdDttm, string prodId)
        {
            // 선택된 데이터를 DataGridView에 추가
            dataGridView1.Rows.Clear(); // 기존 데이터 초기화
            dataGridView1.Rows.Add(
                woId,
                woStat,
                planDttm?.ToString("yyyy-MM-dd HH:mm:ss"),
                woStDttm?.ToString("yyyy-MM-dd HH:mm:ss"),
                woEdDttm?.ToString("yyyy-MM-dd HH:mm:ss"),
                prodId
            );
            
        }
        public void InitializeDataGridViewColumns()
        {
            dataGridView1.Columns.Clear(); // 기존 컬럼 초기화
            dataGridView1.Columns.Add("WoId", "작업지시ID");
            dataGridView1.Columns.Add("WoStat", "공정상태");
            dataGridView1.Columns.Add("PlanDttm", "계획일자");
            dataGridView1.Columns.Add("WoStDttm", "작업시작일");
            dataGridView1.Columns.Add("WoEdDttm", "작업완료일");
            dataGridView1.Columns.Add("ProdId", "제품코드");
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LOTInsertButton_Click(object sender, EventArgs e)
        {
            Flag.FLAG = false;
            LOTInsert lotInsert = new LOTInsert();
            if (lotInsert.ShowDialog() == DialogResult.Yes)
            {
                this.LotSelect();
            }

        }

        private void defectInsertButton_Click(object sender, EventArgs e)
        {
            DEFECTInsert defectInsert = new DEFECTInsert();
            if (defectInsert.ShowDialog() == DialogResult.Yes)
            {
                this.LotSelect();
            }
        }

        private void LOTDeleteButton_Click(object sender, EventArgs e)
        {
            LOTDelete lotDelete = new LOTDelete();
            if (lotDelete.ShowDialog() == DialogResult.Yes)
            {
                this.LotSelect();
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // 유효한 행인지 확인
            {
                // DataGridView에서 행을 가져옴
                DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];

                // 열 이름이 정확한지 확인 후 데이터 가져오기
                try
                {
                    string lotId = selectedRow.Cells["LOTID"].Value?.ToString();

                    LOTID = lotId; //넘겨주기위한 ID
                    //// Details 폼 초기화 및 데이터 설정
                    //detailsForm.InitializeDataGridViewColumns(); // 컬럼 초기화

                    //// Details 폼에 데이터 전달
                    //detailsForm.LoadSelectedRowData(
                    //    woId, woStat,
                    //    string.IsNullOrEmpty(planDttm) ? (DateTime?)null : DateTime.Parse(planDttm),
                    //    string.IsNullOrEmpty(woStDttm) ? (DateTime?)null : DateTime.Parse(woStDttm),
                    //    string.IsNullOrEmpty(woEdDttm) ? (DateTime?)null : DateTime.Parse(woEdDttm),
                    //    prodId
                    //);

                    //detailsForm.ShowDialog();
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show($"DataGridView 열 이름을 확인하세요.\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void defectSearchButton_Click(object sender, EventArgs e)
        {
            DEFECTSearch dEFECTSearch = new DEFECTSearch();
            dEFECTSearch.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.LotSelect();
        }
    }

}
===============================================================
using OPlusBestFUI.DB;
using OPlusBestFUI.Property;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPlusBestFUI.ProcessForm
{
    public partial class NCMForm : Form
    {
        private int count;
        string pathPng;
        string pathGif;
        private static string LOTID;
        private static string PRODID;
        private static string EQPTID;
        private static string Electrode;
        private Details details;
        public NCMForm()
        {
            InitializeComponent();
            //프로그램 실행시 타이머 미작동
            timer1.Enabled = false;
            timer1.Interval = 1000;
            timer2.Enabled = false;
            timer2.Interval = 1000;
            //초기화
            count = 0;
            Flag.FLAG = false;
        }
        // 생성자에서 Details 폼 인스턴스를 받음
        public NCMForm(Details detailsForm, string prodid)
        {
            InitializeComponent();
            //프로그램 실행시 타이머 미작동
            timer1.Enabled = false;
            timer1.Interval = 1000;
            timer2.Enabled = false;
            timer2.Interval = 1000;
            //초기화
            count = 0;
            Flag.FLAG = false;
            PRODID = prodid;////////////////////////////////
            this.details = detailsForm; // 전달받은 Details 폼 저장
        }

        private void NCMForm_Load(object sender, EventArgs e)
        {
            LOTID = Details.LOTID;
            
            string[] prodidList = { "P001", "P002", "PSM001", "PSM002", "PSM003", "PSM004", "P003", "MSM001", "MSM002" };
            for (int i = 0; i < prodidList.Length; i++)
            {
                if( prodidList[i] == PRODID)
                {
                    if(i < 6) //양극
                    {
                        button1.Name = "1호기 시작";
                        button2.Name = "2호기 시작";
                    }
                    else // 음극
                    {
                        button1.Text = "3호기 시작";
                        button2.Text = "4호기 시작";
                    }
                }
            }
            if(PRODID == prodidList[0] || PRODID == prodidList[1] || PRODID == prodidList[6])// 슬리팅 공정 이미지
            {
                pathPng = "C:\\Users\\Admin\\source\\repos\\OPlusBestFUI\\OPlusBestFUI\\Resources\\권선공정-remove.png";
                pathGif = "C:\\Users\\Admin\\source\\repos\\OPlusBestFUI\\OPlusBestFUI\\Resources\\1차권선공정.remove.gif";
                pictureBox1.Image = Image.FromFile(pathPng);
                pictureBox2.Image = Image.FromFile(pathPng);
                label3.Text = "슬리팅설비";
            }
            else if(PRODID == prodidList[2] || PRODID == prodidList[4] || PRODID == prodidList[7])// 믹싱 공정 이미지
            {
                pathPng = "C:\\Users\\Admin\\source\\repos\\OPlusBestFUI\\OPlusBestFUI\\Resources\\믹싱설비확정.png";
                pathGif = "C:\\Users\\Admin\\source\\repos\\OPlusBestFUI\\OPlusBestFUI\\Resources\\믹싱설비확정.gif";
                pictureBox1.Image = Image.FromFile(pathPng);
                pictureBox2.Image = Image.FromFile(pathPng);
                label3.Text = "믹싱설비";
            }
            else if(PRODID == prodidList[3] || PRODID == prodidList[5] || PRODID == prodidList[8])// 코팅 공정 이미지
            {
                pathPng = "C:\\Users\\Admin\\source\\repos\\OPlusBestFUI\\OPlusBestFUI\\Resources\\권선공정-remove.png";
                pathGif = "C:\\Users\\Admin\\source\\repos\\OPlusBestFUI\\OPlusBestFUI\\Resources\\1차권선공정.remove.gif";
                pictureBox1.Image = Image.FromFile(pathPng);
                pictureBox2.Image = Image.FromFile(pathPng);
                label3.Text = "코팅설비";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectEqptId(1);
            LotCreate(EQPTID);
            ANIMATION(1);
            Equipment(EQPTID);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SelectEqptId(2);
            LotCreate(EQPTID);
            ANIMATION(2);
            Equipment(EQPTID);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = count.ToString();
            count++;
            
            if (count == 10)
            {
                LotEND(EQPTID, 1);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label2.Text= count.ToString();
            count++;

            if(count == 10)
            {
                LotEND(EQPTID, 2);
            }
        }
        private void SelectEqptId(int num)
        {
            Dictionary<string, string> eqptidDic = new Dictionary<string, string>();
            eqptidDic.Clear();
            if(num == 1) // 1번 버튼
            {
                eqptidDic.Add("P001", "ST001"); eqptidDic.Add("P002", "ST001"); eqptidDic.Add("P003", "ST003"); eqptidDic.Add("PSM001", "MX001");
                eqptidDic.Add("PSM002", "CT001"); eqptidDic.Add("PSM003", "MX001"); eqptidDic.Add("PSM004", "CT001"); eqptidDic.Add("MSM001", "MX003");
                eqptidDic.Add("MSM002", "CT003");
            }
            else if(num == 2) // 2번 버튼
            {
                eqptidDic.Add("P001", "ST002"); eqptidDic.Add("P002", "ST002"); eqptidDic.Add("P003", "ST004"); eqptidDic.Add("PSM001", "MX002");
                eqptidDic.Add("PSM002", "CT002"); eqptidDic.Add("PSM003", "MX002"); eqptidDic.Add("PSM004", "CT002"); eqptidDic.Add("MSM001", "MX004");
                eqptidDic.Add("MSM002", "CT004");
            }
            

            foreach (var item in eqptidDic)
            {
                if (PRODID == item.Key)
                {
                    EQPTID = item.Value;
                }
            }
        }
        private void Equipment(string EQPTID)
        {
            oracleDb.cmd.CommandText = $"update EQUIPMENT set eqptstats = 'R', workerid = 'wk001' where eqptid = '{EQPTID}'/*변수*/";
            oracleDb.cmd.ExecuteNonQuery();
        }
        private void LotCreate(string EQPTID)
        {
            LOTInsert lotInsert = new LOTInsert(EQPTID);
            Flag.FLAG = true;
            if (lotInsert.ShowDialog() == DialogResult.Yes)
            {
                details.LotSelect();
            }
        }

        private void ANIMATION(int num)
        {
            LOTID = LOTInsert.LOTID;

            oracleDb.cmd.CommandText = $"select LOTSTAT, EQPTID from lot where lotid = '{LOTID}'";
            oracleDb.cmd.ExecuteNonQuery();

            OracleDataReader rdr = oracleDb.cmd.ExecuteReader();
            string lotstat = "";
            while (rdr.Read())
            {
                lotstat = rdr["LOTSTAT"] as string;
            }
            if (lotstat == "C")
            {
                if (num == 1)
                {
                    label1.Text = count.ToString();
                    pictureBox1.Image = Image.FromFile(pathGif);
                    oracleDb.cmd.CommandText = $"UPDATE LOT SET LOTSTAT = (SELECT CASE WHEN LOTSTAT = 'C' THEN 'S'\n " +
                                                                            $"ELSE LOTSTAT\n " +
                                                                            $"END\n " +
                                                                     $"FROM LOT WHERE LOTID = '{LOTID}')\n " +
                                                      $"WHERE LOTID = '{LOTID}'";
                    oracleDb.cmd.ExecuteNonQuery();

                    // Details 폼의 LotSelect() 메서드 호출
                    details.LotSelect();

                    timer1.Start();
                }
                else if (num == 2)
                {
                    label2.Text = count.ToString();
                    pictureBox2.Image = Image.FromFile(pathGif);
                    oracleDb.cmd.CommandText = $"UPDATE LOT SET LOTSTAT = (SELECT CASE WHEN LOTSTAT = 'C' THEN 'S'\n " +
                                                                            $"ELSE LOTSTAT\n " +
                                                                            $"END\n " +
                                                                     $"FROM LOT WHERE LOTID = '{LOTID}')\n " +
                                                      $"WHERE LOTID = '{LOTID}'";
                    oracleDb.cmd.ExecuteNonQuery();

                    // Details 폼의 LotSelect() 메서드 호출
                    details.LotSelect();

                    timer2.Start();
                }

            }
            //else if (lotstat == "S")
            //{
            //    MessageBox.Show("실행중인 LOT입니다.");
            //}
            //else if (lotstat == "E")
            //{
            //    MessageBox.Show("종료된 LOT입니다.");
            //}
            //else if (lotstat == "D")
            //{
            //    MessageBox.Show("삭제된 LOT입니다.");
            //}
        }
        private void LotEND(string EQPTID, int num)
        {
            if (num == 1)
            {
                pictureBox1.Image = Image.FromFile(pathPng);
                count = 0;

                oracleDb.cmd.CommandText = $"UPDATE LOT SET LOTSTAT = (SELECT CASE WHEN LOTSTAT = 'S' THEN 'E'\n " +
                                                                        $"ELSE LOTSTAT\n " +
                                                                        $"END\n " +
                                                                     $"FROM LOT WHERE LOTID = '{LOTID}'),\n " +
                                                          $"LOTEDDTTM = TO_DATE('{DateTime.Now:yyyy-MM-dd HH:mm:ss}', 'YYYY-MM-DD HH24:MI:SS')\n" +
                                                  $"WHERE LOTID = '{LOTID}'";
                oracleDb.cmd.ExecuteNonQuery();

                oracleDb.cmd.CommandText = $"update EQUIPMENT set eqptstats = 'D', workerid = 'wk001' where eqptid = '{EQPTID}'/*변수*/";
                oracleDb.cmd.ExecuteNonQuery();
                // Details 폼의 LotSelect() 메서드 호출
                details.LotSelect();
                timer1.Stop();
            }
            else if (num == 2)
            {
                pictureBox2.Image = Image.FromFile(pathPng);
                count = 0;

                oracleDb.cmd.CommandText = $"UPDATE LOT SET LOTSTAT = (SELECT CASE WHEN LOTSTAT = 'S' THEN 'E'\n " +
                                                                        $"ELSE LOTSTAT\n " +
                                                                        $"END\n " +
                                                                 $"FROM LOT WHERE LOTID = '{LOTID}'),\n " +
                                                          $"LOTEDDTTM = TO_DATE('{DateTime.Now:yyyy-MM-dd HH:mm:ss}', 'YYYY-MM-DD HH24:MI:SS')\n" +
                                                  $"WHERE LOTID = '{LOTID}'";
                oracleDb.cmd.ExecuteNonQuery();

                oracleDb.cmd.CommandText = $"update EQUIPMENT set eqptstats = 'D', workerid = 'wk001' where eqptid = '{EQPTID}'/*변수*/";
                oracleDb.cmd.ExecuteNonQuery();
                // Details 폼의 LotSelect() 메서드 호출
                details.LotSelect();
                timer2.Stop();
            }

        }
    }
}

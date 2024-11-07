using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace OracleWinFormsAppTest01
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form2_Load);
            
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            // 차트 초기화 및 시리즈 설정
            chart1.Series.Clear();
            chart1.Series.Add("Sales");

            // 차트 유형을 도넛형 차트로 설정
            chart1.Series["Sales"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;

            // 데이터 추가
            chart1.Series["Sales"].Points.AddXY("Product A", 30);
            chart1.Series["Sales"].Points.AddXY("Product B", 50);
            chart1.Series["Sales"].Points.AddXY("Product C", 20);
            chart1.Series["Sales"].Points.AddXY("Product D", 40);
            chart1.Series["Sales"].Points.AddXY("Product E", 10);

            // 데이터 라벨 표시
            chart1.Series["Sales"].IsValueShownAsLabel = true;
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}

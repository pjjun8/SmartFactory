//윈폼 2
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
    public partial class Form2 : Form
    {
        private string name;    //주문한 제품 이름
        private int ea;         // 주문한 제품 수량
        private int progressValue = 0;
        private int count = 1;
        Form1 frm1;     // HAS-A ((포함))
        Random random = new Random();
        public void GongZang1() //임의적 오류 발생 코드
        {
            int rand = random.Next(88, 113);

            if (!(rand > 90 && rand < 110))
            {
                timer1.Stop();
                MessageBox.Show($"권선이 {rand}번 감겨 오류입니다!");
            }
            else
            {
                MessageBox.Show($"성공!");
            }

        }
        public Form2() //디폴트 생성자
        {
            InitializeComponent();
        }
        //Form1에 접근하려면 Form1 속성 중 Modifiers를 private --> public으로 수정해 줘야 한다.

        public Form2(object form)       //생성자를 하나 더 만듦
        {
            InitializeComponent();
            frm1 = (Form1)form;
            name = frm1.ProductName;
            ea = frm1.ProductEa;

            //MessageBox.Show($"{name}, {ea}"); //확인용 메세지 박스
        }
        public Form2(string str, Form1 form1)
        {
            this.frm1 = form1;

        }

        private void Form2_Load(object sender, EventArgs e)
        {

            this.dataGridView1.Columns.Add("Name", "제품명");
            this.dataGridView1.Columns.Add("Ea", "제품 수량");

            for (int i = 1; i <= 8; i++)
            {
                // "progressBar" + i 라는 이름의 컨트롤을 찾습니다.
                Control[] controls = this.Controls.Find("progressBar" + i, true);
                if (controls.Length > 0 && controls[0] is ProgressBar)
                {
                    ProgressBar progressBar = (ProgressBar)controls[0];
                    progressBar.Minimum = 0;
                    progressBar.Maximum = 100;
                    progressBar.Value = 0;
                }
            }
            timer1.Interval = 10; // 100 milliseconds
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Add($"{name}", $"{ea}"); // 오류 사항! 버튼 누르면 계속 같은 같이 나옴 수정 필요??
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Control[] controls = this.Controls.Find("progressBar" + count, true);
            if (controls.Length > 0 && controls[0] is ProgressBar)
            {
                ProgressBar progressBar = (ProgressBar)controls[0];
                progressValue += 1;

                if (progressValue <= 100)
                {
                    progressBar.Value = progressValue;

                }
                else
                {
                    count++;
                    progressValue = 0;
                    GongZang1();

                }
                if (count > 8)
                {
                    timer1.Stop();
                    MessageBox.Show("진행완료!");
                }
            }
        }

        private void 주문화면ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Hide(); // 약간의 오류 있음 Hide 하면 종료해도 백그라운드로 계속 돌아감 
            Form1 frm1 = new Form1();
            frm1.Show(); //모달
            //frm1.Show(); //모달리스
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            Form3 frm3 = new Form3();
            Form4 frm4 = new Form4();
            //frm1.Show();
            frm1.Dispose();
            frm3.Dispose();
            frm4.Dispose();
            this.Dispose();
        }

        private void 제품판매통계ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 frm5 = new Form5();
            frm5.Show(); //모달
        }

        private void 재고관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();
            frm6.Show(); //모달
        }
    }
}

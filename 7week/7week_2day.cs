namespace TrackBarRGB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBarR.Value = 0;
            trackBarG.Value = 0;
            trackBarB.Value = 0;
            UPdateColor();
        }

        private void trackBarR_Scroll(object sender, EventArgs e)
        {
            UPdateColor();
        }

        private void trackBarG_Scroll(object sender, EventArgs e)
        {
            UPdateColor();
        }

        private void trackBarB_Scroll(object sender, EventArgs e)
        {
            UPdateColor();
        }
        //사용자 정의 함수
        private void UPdateColor()
        {
            int r = trackBarR.Value;
            int g = trackBarG.Value;
            int b = trackBarB.Value;
            pictureBox1.BackColor = Color.FromArgb(r, g, b);
        }
    }
}
--------------------------------------------------------------------
namespace ProgresssBar01
{
    public partial class Form1 : Form
    {
        private int progressValue = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            ProgressLabel.Text = "진행도 : 0%";

            timer1.Interval = 100;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressValue++;

            if(progressValue <= 100)
            {
                progressBar1.Value = progressValue;
                ProgressLabel.Text = $"진행도 : {progressValue}%";
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("진행완료!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressValue = 0;
            progressBar1.Value = 0;
            ProgressLabel.Text = "진행도 : 0%";
            timer1.Start();
        }
    }
}
------------------------------------------------------------------
namespace DataGridApp04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("NO", "번호");
            dataGridView1.Columns.Add("Name", "이름");
            dataGridView1.Columns.Add("HP", "전화번호");
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string no = textBoxNo.Text;
            string name = textBoxName.Text;
            string hp = textBoxHP.Text;

            if (!string.IsNullOrEmpty(no) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(hp))
            {
                dataGridView1.Rows.Add(no, name, hp);
            }
            else
            {
                MessageBox.Show("입력좀 해라");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.Remove(row);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchValue = textBoxSearch.Text;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Name"].Value != null && row.Cells["Name"].Value.ToString().Equals(searchValue))
                {
                    row.Selected = true;
                    MessageBox.Show($"있다임마");
                    return;
                }
            }
            MessageBox.Show("몰라임마");
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
---------------------------------------------------------------

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

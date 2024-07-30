박싱, 언박싱, 업캐스팅, 다운캐스팅
github.com
int number = 42;
Int32 boxed = number; //Boxing
int unboxed = boxed; //UnBoxing

object obj = number; //UpCasting, Boxing
int downed = (int)obj; //강제형변환, DownCasting
-------------------------------------------------------------
namespace DeepCopy
{
    class Myclass
    {
        public int MyField1;
        public int MyField2;
        //
        public void DeepCopy()
        {
            Myclass myclass = new Myclass();
            myclass.MyField1 = this.MyField1;
            myclass.MyField2 = this.MyField2;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("얉은 복사(Shallow Copy)");
            {
                Myclass source = new Myclass();

                source.MyField1 = 10;
                source.MyField2 = 20;

                Myclass target = source;    //얇은 복사??
                target.MyField2 = 30;
                Console.WriteLine(source.MyField2);
                Console.WriteLine(target.MyField2);
            }
            Console.WriteLine("깊은 복사(Deep Copy)");
            {
                Myclass source = new Myclass();

                source.MyField1 = 10;
                source.MyField2 = 20;

                Myclass target = source.DeepCopy();
                target.MyField2 = 30;

                Console.WriteLine(source.MyField2);
                Console.WriteLine(target.MyField2);
            }
        }//end of Main
    }//end of Program
}//end of DeepCopy
--------------------------------------------------------------
namespace WinFormChap10
{
    public partial class Form1 : Form
    {
        private int sajin1 = 1;
        private int sajin2 = 1;
        private int sajin3 = 1;
        private int sajin_Max1 = 5;
        private int sajin_Max2 = 6;
        private int sajin_Max3 = 8;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile(System.Environment.CurrentDirectory +
            "/재롱피우는 오버액션토끼/" + sajin1 + ".jpg");

            sajin1++;

            if (sajin1 == sajin_Max1)
                sajin1 = 1;

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox2.Image = Image.FromFile(System.Environment.CurrentDirectory +
            "/다가오는 코끼리 두마리/" + sajin2 + ".jpg");

            sajin2++;

            if (sajin2 == sajin_Max2)
                sajin2 = 1;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            pictureBox3.Image = Image.FromFile(System.Environment.CurrentDirectory +
            "/돌아서는 신랑신부/" + sajin3 + ".jpg");

            sajin3++;

            if (sajin3 == sajin_Max3)
                sajin3 = 1;
        }
        private void 빠르게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = 10; timer2.Interval = 10; timer3.Interval = 10;
        }
        private void 중간ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = 500; timer2.Interval = 500; timer3.Interval = 500;
        }
        private void 느리게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1000; timer2.Interval = 1000; timer3.Interval = 1000;
        }

        private void 종료ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void 전체중지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false; timer2.Enabled = false; timer3.Enabled = false;
        }

        private void 전체시작ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true; timer2.Enabled = true; timer3.Enabled = true;
        }

        private void 빠르게ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 10;
        }

        private void 중간ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 500;
        }

        private void 느리게ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
        }

        private void 빠르게ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            timer2.Interval = 10;
        }

        private void 중간ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            timer2.Interval = 500;
        }

        private void 느리게ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            timer2.Interval = 1000;
        }

        private void 빠르게ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            timer3.Interval = 10;
        }

        private void 중간ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            timer3.Interval = 500;
        }

        private void 느리게ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            timer3.Interval = 1000;
        }

        private void 개발자소개ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("안녕 내이름은 박상원");
        }

        private void ㅋㅋㅋㅋToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("강사님 사람합니다.");
        }

        private void 강사님에게하고싶은말ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("환경은 무슨 환경\n컴공강의실에서 했다 ㅋㅋ");
        }
    }
}

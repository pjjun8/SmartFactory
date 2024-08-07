//윈폼2 공정화면
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
        Form1 frm1;     // HAS-A ((포함))
        public Form2() //디폴트 생성자
        {
            InitializeComponent();
            Application.Run(new Form1());
        }
        //Form1에 접근하려면 Form1 속성 중 Modifiers를 private --> public으로 수정해 줘야 한다.

        public Form2(object form)       //생성자를 하나 더 만듦
        {
            InitializeComponent();

            frm1 = (Form1)form;
            name = frm1.ProductName;
            ea = frm1.ProductEa;
            MessageBox.Show($"{name}, {ea}");
        }
        public Form2(string str, Form1 form1)
        {
            this.frm1 = form1;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.dataGridView1.Columns.Add("Name", "제품명");
            this.dataGridView1.Columns.Add("Ea", "제품 수량");
            this.dataGridView1.Rows.Add($"{name}", $"{ea}");
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

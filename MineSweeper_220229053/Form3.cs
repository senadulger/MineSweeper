using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper_220229053
{
    public partial class Form3 : Form
    {
        public Form3(Skorboard skorboard)
        {
            InitializeComponent();

            loadScores(); 
        }

        private void loadScores()
        {
            Skorboard skorboard = Skorboard.GetInstance();
            var scores = skorboard.GetScores();

            for (int i = 0; i < scores.Count; i++)
            {
                listBox1.Items.Add($"{i + 1} - {scores[i].UserName} - {scores[i].Score} point");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

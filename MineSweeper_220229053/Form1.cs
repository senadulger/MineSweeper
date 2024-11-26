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
    public partial class Form1 : Form
    {
        private Skorboard globalSkorboard = new Skorboard();
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.ForeColor = Color.Black;
            textBox1.SelectionStart = 0;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox2.ForeColor = Color.Black;
            textBox2.SelectionStart = 0;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            textBox3.ForeColor = Color.Black;
            textBox3.SelectionStart = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            bool isGridValid = int.TryParse(textBox2.Text, out int gridNumber) && gridNumber > 0 && gridNumber <= 30;
            bool isMineValid = int.TryParse(textBox3.Text, out int mineNumber) && mineNumber >= 10 && mineNumber < (gridNumber * gridNumber);

            if (isGridValid && isMineValid)
            {
                Form2 form2 = new Form2(gridNumber, mineNumber, userName, globalSkorboard);
                form2.Show();
                this.Hide();
            }
            else
            {
                if (!(isGridValid))
                {
                    MessageBox.Show("The number of grids must be a number between 1 and 30.");
                }

                if (!(isMineValid))
                {
                    MessageBox.Show("The number of mines must be at least 10 and at most the same as the grid number.");
                }
            }
        }
    }
}

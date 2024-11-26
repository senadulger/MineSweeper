using MineSweeper_220229053;
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
    public partial class Form2 : Form
    {
        private Oyun oyun;
        private Timer gameTimer;
        int move = 0;
        int passedTime = 1;
        public Form2(int gridNumber, int mineNumber, string userName, Skorboard skorboard)
        {
            InitializeComponent();

            oyun = new Oyun(gridNumber, mineNumber, this, userName, skorboard);
            oyun.CreateGrid();
        }

        private void Form2_load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {            
            timerLabel.Text = passedTime.ToString();
            passedTime++;
        }

        public int getPassedTime()
        {
            return passedTime;
        }

        public void stopTimer()
        {
            timer1.Stop();
        }

        public void increaseMove() 
        {
            move++;
            moveLabel.Text = move.ToString();
        }
    }
}


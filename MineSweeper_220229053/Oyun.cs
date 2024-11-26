using System;
using System.Windows.Forms;

namespace MineSweeper_220229053
{
    internal class Oyun
    {
        private int gridNumber, mineNumber;
        private Button[,] buttons;
        private Form2 gameForm;
        private Random randomLoc;
        private Timer gameTimer;
        private string userName;
        private Skorboard skorboard;

        public Oyun(int gridNumber, int mineNumber, Form2 gameForm, string userName, Skorboard skorboard)
        {
            this.gridNumber = gridNumber;
            this.mineNumber = mineNumber;
            this.gameForm = gameForm;
            buttons = new Button[gridNumber, gridNumber];
            randomLoc = new Random();
            this.userName = userName;
            this.skorboard = skorboard;
        }

        public void CreateGrid()
        {
            int buttonSize = 26;

            gameForm.ClientSize = new System.Drawing.Size(gridNumber * buttonSize, 50 + (gridNumber * buttonSize));

            for (int i = 0; i < gridNumber; i++)
            {
                for (int j = 0; j < gridNumber; j++)
                {
                    Button gridButton = new Button();
                    gridButton.Width = buttonSize;
                    gridButton.Height = buttonSize;
                    gridButton.Margin = new Padding(0);
                    gridButton.Location = new System.Drawing.Point(j * buttonSize, 50 + (i * buttonSize));

                    gameForm.Controls.Add(gridButton);
                    buttons[i, j] = gridButton;

                    gridButton.MouseDown += GridButtonClick;
                }
            }
            LocMines();
        }

        private void LocMines()
        {
            int howManyMines = 0;

            while (howManyMines < mineNumber)
            {
                int x = randomLoc.Next(gridNumber);
                int y = randomLoc.Next(gridNumber);

                if (buttons[x, y].Tag == null)
                {
                    buttons[x, y].Tag = "mine";
                    howManyMines++;
                }
            }
        }

        private void OpenGrid(Button button)
        {
            if (!button.Enabled || button.Text == "🚩")
            {
                return;
            }

            if (button.Tag != null && button.Tag.ToString() == "mine")
            {
                ShowAllMines();
                MessageBox.Show("You stepped on a mine! Game Over :(");
                EndGame();
            }
            else
            {
                ShowNeighboringMines(button);
            }
        }

        private void OpenEmptyGrid(int row, int col)
        {
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < gridNumber && j >= 0 && j < gridNumber && buttons[i, j].Enabled)
                    {
                        buttons[i, j].Enabled = false;
                        int emptyMinesAround = CalculateNeighboringMines(buttons[i, j]);

                        if (emptyMinesAround > 0)
                        {
                            buttons[i, j].Text = emptyMinesAround.ToString();
                        }
                        else
                        {
                            buttons[i, j].Text = "";
                            OpenEmptyGrid(i, j);
                        }
                    }
                }
            }
        }

        private int CalculateNeighboringMines(Button button)
        {
            int mineCount = 0;
            int row = -1, col = -1;

            for (int i = 0; i < gridNumber; i++)
            {
                for (int j = 0; j < gridNumber; j++)
                {
                    if (buttons[i, j] == button)
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
            }

            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < gridNumber && j >= 0 && j < gridNumber && buttons[i, j].Tag != null && buttons[i, j].Tag.ToString() == "mine")
                    {
                        mineCount++;
                    }
                }
            }
            return mineCount;
        }

        private void ShowNeighboringMines(Button button)
        {
            int mineCount = CalculateNeighboringMines(button);
            int row = -1, col = -1;

            for (int i = 0; i < gridNumber; i++)
            {
                for (int j = 0; j < gridNumber; j++)
                {
                    if (buttons[i, j] == button)
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
            }

            if (mineCount > 0)
            {
                button.Text = mineCount.ToString();
                button.BackColor = System.Drawing.Color.PaleGreen;
            }
            else
            {
                OpenEmptyGrid(row, col);
            }

            button.Enabled = false;
        }

        private void AddFlag(Button button)
        {
            if (button.Text == "🚩")
            {
                if (button.Tag.ToString().Contains("flag"))
                {
                    button.Tag = button.Tag.ToString().Replace("flag-", "");
                }
                button.Text = "";
                button.BackColor = System.Drawing.Color.Gainsboro;
            }
            else
            {
                if (button.Tag != null && button.Tag.ToString() == "mine")
                {
                    button.Tag = "flag-mine";
                }
                else
                {
                    button.Tag = "flag-empty";
                }
                button.Text = "🚩";
                button.BackColor = System.Drawing.Color.Black;
            }
            CorrectFlags();
        }

        private void CorrectFlags()
        {
            int correctFlags = 0;
            int totalFlags = 0;

            for (int i = 0; i < gridNumber; i++)
            {
                for (int j = 0; j < gridNumber; j++)
                {
                    if (buttons[i, j].Text == "🚩")
                    {
                        totalFlags++;
                        if (buttons[i, j].Tag != null && buttons[i, j].Tag.ToString() == "flag-mine")
                        {
                            correctFlags++;
                        }
                    }
                }
            }

            if (correctFlags == mineNumber && totalFlags == mineNumber)
            {
                gameForm.stopTimer();
                MessageBox.Show("Congratulations! You won the game :)");
                EndGame();
            }
        }

        private void ShowAllMines()
        {
            for (int i = 0; i < gridNumber; i++)
            {
                for (int j = 0; j < gridNumber; j++)
                {
                    if (buttons[i, j].Tag != null && buttons[i, j].Tag.ToString() == "mine")
                    {
                        buttons[i, j].Text = "💣";
                        buttons[i, j].BackColor = System.Drawing.Color.Red;
                    }
                }
            }
            gameForm.stopTimer();
        }

        private void EndGame()
        {
            int correctFlags = 0;
            for (int i = 0; i < gridNumber; i++)
            {
                for (int j = 0; j < gridNumber; j++)
                {
                    if (buttons[i, j].Tag?.ToString() == "flag-mine")
                    {
                        correctFlags++;
                    }
                }
            }

            int passedTime = gameForm.getPassedTime();
            Skorboard.GetInstance().AddScore(userName, correctFlags, passedTime);

            Form3 form3 = new Form3(skorboard);
            form3.Show();
            gameForm.Close();
        }

        private void GridButtonClick(object sender, MouseEventArgs e)
        {
            Button minedGrid = sender as Button;

            if (e.Button == MouseButtons.Left)
            {
                OpenGrid(minedGrid);
                gameForm.increaseMove();
            }
            else if (e.Button == MouseButtons.Right)
            {
                AddFlag(minedGrid);
            }
        }
    }
}
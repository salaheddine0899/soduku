using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace soduku
{
    public partial class Form1 : Form
    {
        SodukuCell[,] buttons=new SodukuCell[9,9];
        List<SodukuCell> wrongCells = new List<SodukuCell>();

        private void createCells()
        {
            for (int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    buttons[i, j] = new SodukuCell();
                    buttons[i, j].Size = new Size(40,40);
                    buttons[i, j].Location = new Point(i * 40, j * 40);
                    buttons[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? SystemColors.Control : Color.LightGray;
                    buttons[i, j].FlatStyle=FlatStyle.Flat;
                    buttons[i,j].FlatAppearance.BorderColor = Color.Black;
                    buttons[i, j].KeyPress+=cell_keyPressed;
                    panel1.Controls.Add(buttons[i, j]);
                }
            }
            
        }

        private void cell_keyPressed(Object sender,KeyPressEventArgs e) {
            SodukuCell cell=sender as SodukuCell;
            if (cell.IsLocked)
                return;
            int value;
            if(int.TryParse(e.KeyChar.ToString(),out value))
            {
                if (value == 0)
                    cell.clear();
                else
                {
                    cell.Text = value.ToString();
                    cell.Value= value;

                }
                    
                cell.ForeColor = SystemColors.ControlDarkDark;
            }

        }

        private void StartNewGame()
        {
            foreach(var cell in buttons)
            {
                cell.Value= 0;
                cell.clear();
            }
            //FindValueForNextCel(0, -1);
            //ShowRandomValuesHints(45);
        }


        private bool IsValideNumber(int value,int x,int y)
        {
            for(int i = 0; i < 9; i++)
            {
                if ((i != x) && (buttons[x,i].Value==value))
                    return false;
                if ((i != y) && (buttons[y, i].Value == value))
                    return false;
            }
            var cellX = x - (x % 3);
            var cellY = y - (y % 3);
            for (int i = cellX; i < cellX + 3; i++)
            {
                for(int j = cellY; j < cellY + 3; j++)
                {
                    if ((i != x) && (j != y) && (buttons[i, j].Value == value))
                        return false;
                }
            }
            return true;
        }

        public Form1()
        {
            InitializeComponent();
            createCells();
            StartNewGame();
        }

        private void check_click(object sender, EventArgs e)
        {
            for (int i=0;i<9;i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!IsValideNumber(buttons[i,j].Value, i, j))
                    {
                        wrongCells.Add(buttons[i, j]);
                    }

                }

                if(wrongCells.Count > 0)
                {
                    wrongCells.ForEach(x =>x.ForeColor=Color.Red);
                    MessageBox.Show("wrong input");
                }
                
            }
        }
    }
}

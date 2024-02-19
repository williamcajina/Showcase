using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class modalDialog : Form
    {

    

        public int TimeInterval
        {
            get
            {
                return (int)numericUpDownTimeInterval.Value;
            }

            set
            {
                numericUpDownTimeInterval.Value = value;
            }
        }

        public int CellsX
        {
            get
            {
                return (int)numericUpDownUniverseWidth.Value;
            }

            set
            {
                numericUpDownUniverseWidth.Value = value;
            }
        }

        public int CellsY
        {
            get
            {
                return (int)numericUpDownUniverseHeight.Value;
            }

            set
            {
                numericUpDownUniverseHeight.Value = value;
            }
        }

        public Color GridColor
        {
            get
            {
                return buttonGridColor.BackColor;
            }

            set
            {
                buttonGridColor.BackColor = value;
            }
        }

        public Color LiveCellColor
        {
            get
            {
                return buttonLiveCellColor.BackColor;
            }

            set
            {
                buttonLiveCellColor.BackColor = value;
            }
        }

        public Color BackgroundColor
        {
            get
            {
                return buttonBackColor.BackColor;
            }

            set
            {
                buttonBackColor.BackColor = value;
            }
        }

        public string Boundary
        {
            get
            {   if (radioButtonToroidal.Checked)
                return "Toroidal";
                if (radioButtonFinite.Checked)
                    return "Finite";
                else return "Torodial";
            }

            set
            {
                if (value == "Toroidal")
                    radioButtonToroidal.Checked = true;
                if (value == "Finite")
                    radioButtonFinite.Checked = true;
            }
        }

        public modalDialog()
        {
           
            InitializeComponent();
            
           
         


        }

        private void buttonGridColor_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = ((Button)sender).BackColor;
            

            if(DialogResult.OK == dlg.ShowDialog())
            {
                ((Button)sender).BackColor = dlg.Color;
            }
        }

 
    }
}

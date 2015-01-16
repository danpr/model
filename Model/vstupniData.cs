using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Model
{
    public partial class vstupniData : Form
    {
        public vstupniData()
        {
            InitializeComponent();
            AcceptButton = buttonOk;
            CancelButton = buttonCancel;


            buttonOk.Enabled = false;
            testforOKButton();
        }

        private void numericUpDownGreaterZero_changeValue(object sender, EventArgs e)
        {
            if ((sender as NumericUpDown).Value <= 0)
            {
                (sender as NumericUpDown).Focus();
                Console.Beep();
            }
            testforOKButton();
        }

        private void numericUpDownGreaterEqZero_changeValue(object sender, EventArgs e)
        {
            if ((sender as NumericUpDown).Value < 0)
            {
                (sender as NumericUpDown).Focus();
                Console.Beep();
            }

            testforOKButton();
        }

        private void numericUpDownP0_ValueChanged(object sender, EventArgs e)
        {
            if ((sender as NumericUpDown).Value == 0)
            {
                (sender as NumericUpDown).Focus();
                Console.Beep();
            }

            testforOKButton();
        }

        public decimal DP
        {
            get { return numericUpDownDP.Value; }
            set { numericUpDownDP.Value = value; }
        }


        public decimal V
        {
            get { return numericUpDownV.Value; }
            set { numericUpDownV.Value = value; }
        }

        public decimal T
        {
            get { return numericUpDownT.Value; }
            set { numericUpDownT.Value = value; }
        }


        public decimal SS
        {
            get { return numericUpDownSS.Value; }
            set { numericUpDownSS.Value = value; }
        }

        public decimal E
        {
            get { return numericUpDownE.Value; }
            set { numericUpDownE.Value = value; }
        }


        public decimal PT
        {
            get { return numericUpDownPT.Value; }
            set { numericUpDownPT.Value = value; }
        }


        public decimal PS
        {
            get { return numericUpDownPS.Value; }
            set { numericUpDownPS.Value = value; }
        }


        public decimal PD
        {
            get { return numericUpDownPD.Value; }
            set { numericUpDownPD.Value = value; }
        }


        public decimal DD
        {
            get { return numericUpDownDD.Value; }
            set { numericUpDownDD.Value = value; }
        }


        public decimal P0
        {
            get { return numericUpDownP0.Value; }
            set { numericUpDownP0.Value = value; }
        }

        public decimal P2
        {
            get { return numericUpDownP2.Value; }
            set { numericUpDownP2.Value = value; }
        }

        public decimal P3
        {
            get { return numericUpDownP3.Value; }
            set { numericUpDownP3.Value = value; }
        }


        public decimal U
        {
            get { return numericUpDownU.Value; }
            set { numericUpDownU.Value = value; }
        }



        public decimal POZNEK
        {
            get { return numericUpDownPoznek.Value; }
            set { numericUpDownPoznek.Value = value; }
        }


        public decimal RT
        {
            get { return numericUpDownRt.Value; }
            set { numericUpDownRt.Value = value; }
        }

        public Boolean POVRCH
        {
            get { if (comboBoxPovrch.SelectedIndex == 0) return true; else return false; }
            set { if (value) comboBoxPovrch.SelectedIndex = 0; else comboBoxPovrch.SelectedIndex = 1; }
        }


        private Boolean dataIsOk()
        {
            if (numericUpDownDP.Value <= 0) return false;
            if (numericUpDownT.Value <= 0) return false;
            if (numericUpDownV.Value <= 0) return false;
            if (numericUpDownSS.Value < 0) return false;
            if (numericUpDownE.Value <= 0) return false;
            if (numericUpDownPT.Value <= 0) return false;
            if (numericUpDownPS.Value < 0) return false;
            if (numericUpDownPD.Value < 0) return false;
            if (numericUpDownDD.Value < 0) return false;
            if (numericUpDownP0.Value == 0) return false;

            return true;
        }

        private void testforOKButton()
        {
            if (dataIsOk())
            {
                buttonOk.Enabled = true;
            }
            else
            {
                buttonOk.Enabled = false;
            }
        }

    }
}

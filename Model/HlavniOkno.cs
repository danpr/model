using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace Model
{
    public partial class HlavniOkno : Form
    {
        private PracovniData pracData;

        public HlavniOkno()
        {
            InitializeComponent();
            pracData = new PracovniData();
            clearOutData();
            loadVstupniData();
            zobrazVstupniData();

            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


        }



        private void konecProgramuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void vloženiToolStripMenuItem_Click(object sender, EventArgs e)
        {

            vstupniData vstupDat = new vstupniData();
            vstupDat.DP = pracData.DP;
            vstupDat.V = pracData.V;
            vstupDat.T = pracData.T;
            vstupDat.SS = pracData.SS;
            vstupDat.E = pracData.E;
            vstupDat.PT = pracData.PT;
            vstupDat.PS = pracData.PS;
            vstupDat.PD = pracData.PD;
            vstupDat.DD = pracData.DD;

            vstupDat.P0 = pracData.P0;
            vstupDat.P2 = pracData.P2;
            vstupDat.P3 = pracData.P3;

            vstupDat.U = pracData.U;
            vstupDat.POZNEK = pracData.POZNEK;
            vstupDat.RT = pracData.RT;
            vstupDat.POVRCH = pracData.POVRCH;



            DialogResult result = vstupDat.ShowDialog();
            if (result == DialogResult.OK)
            {
                // muzu zpracovat data;
                pracData.DP = vstupDat.DP;
                pracData.V = vstupDat.V;
                pracData.T = vstupDat.T;
                pracData.SS = vstupDat.SS;
                pracData.E = vstupDat.E;
                pracData.PT = vstupDat.PT;
                pracData.PS = vstupDat.PS;
                pracData.PD = vstupDat.PD;
                pracData.DD = vstupDat.DD;

                pracData.P0 = vstupDat.P0;
                pracData.P2 = vstupDat.P2;
                pracData.P3 = vstupDat.P3;

                pracData.U = vstupDat.U;
                pracData.POZNEK = vstupDat.POZNEK;
                pracData.RT = vstupDat.RT;
                pracData.POVRCH = vstupDat.POVRCH;

                zobrazVstupniData();
                saveVstupniData();
            }
        }

        private void zobrazVstupniData()
        {
            labelDP.Text = Convert.ToString(pracData.DP);
            labelV.Text = Convert.ToString(pracData.V);
            labelT.Text = Convert.ToString(pracData.T);
            labelSS.Text = Convert.ToString(pracData.SS);
            labelE.Text = Convert.ToString(pracData.E);
            labelPT.Text = Convert.ToString(pracData.PT);
            labelPS.Text = Convert.ToString(pracData.PS);
            labelPD.Text = Convert.ToString(pracData.PD);
            labelDD.Text = Convert.ToString(pracData.DD);
            labelP0.Text = Convert.ToString(pracData.P0);
            labelP2.Text = Convert.ToString(pracData.P2);
            labelP3.Text = Convert.ToString(pracData.P3);
            setPText(pracData.P2, pracData.P3);
            labelU.Text = Convert.ToString(pracData.U);
            labelPozNek.Text = Convert.ToString(pracData.POZNEK);
            labelRT.Text = Convert.ToString(pracData.RT);
            if (pracData.POVRCH) labelPovrch.Text = "Vnější";
            else labelPovrch.Text = "Vnitřní";
        }

        private void setPText(decimal P2, decimal P3)
        {
            if ((P2 == 0) && (P3 == 0))
            {
                labelPText.Text = "Tvar rovnorameného přítlaku";
            }
            else
            {
                if ((P2 != 0) && (P3 == 0))
                {
                    labelPText.Text = "Elipsovitý tvar";
                }
                else
                {
                    if ((P2 > 0) && (P3 < 0))
                    {
                        labelPText.Text = "Hruškovitý tvar";
                    }
                    else
                    {
                        if ((P2 < 0) && (P3 > 0))
                        {
                            labelPText.Text = "Jablkovitý tvar";
                        }
                        else
                        {
                            labelPText.Text = "Nesprávné hodnoty P2 a P3";
                        }
                    }

                }

            }

        }

        private void výpočetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vypocet();
        }

        private void vypocet()
        {
            clearOutData();
            pracData.vypocet();
            labelMPrumer.Text = pracData.MalyPrumer.ToString("F3"); // Convert.ToString(pracData.MalyPrumer);
            labelVPrumer.Text = pracData.VelkyPrumer.ToString("F3"); // Convert.ToString(pracData.VelkyPrumer);
            labelNekruhovitost.Text = pracData.Nekruhovitost.ToString("F3"); // Convert.ToString(pracData.Nekruhovitost);
            showOutData();
        }


        private void clearOutData()
        {
            labelMPrumer.Text = "";
            labelVPrumer.Text = "";
            labelNekruhovitost.Text = "";
            dataGridView1.Rows.Clear();
        }

        private void showOutData()
        {

            //            IEnumerator dateEnumerator = pracData.GetEnumerator();

            Int32 i = 0;

            foreach (PracovniData.Item item in pracData)
            {
                i = dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = item.r.ToString("F3"); //  Convert.ToString(item.r);
                dataGridView1.Rows[i].Cells[1].Value = item.fi.ToString("F2"); //Convert.ToString(item.fi);
                dataGridView1.Rows[i].Cells[2].Value = item.re.ToString("F3");//Convert.ToString(item.re);
                dataGridView1.Rows[i].Cells[3].Value = item.fie.ToString("F2");//Convert.ToString(item.fie);
                dataGridView1.Rows[i].Cells[4].Value = item.xe.ToString("F3");//Convert.ToString(item.xe);
                dataGridView1.Rows[i].Cells[5].Value = item.ye.ToString("F3");//Convert.ToString(item.ye);
            }


        }

        private void buttonVypocet_Click(object sender, EventArgs e)
        {
            vypocet();
        }


        private void saveVstupniData()
        {
            Properties.Settings.Default.DP = pracData.DP;
            Properties.Settings.Default.V = pracData.V;
            Properties.Settings.Default.T = pracData.T;
            Properties.Settings.Default.SS = pracData.SS;
            Properties.Settings.Default.E = pracData.E;
            Properties.Settings.Default.PT = pracData.PT;
            Properties.Settings.Default.PS = pracData.PS;
            Properties.Settings.Default.PD = pracData.PD;
            Properties.Settings.Default.DD = pracData.DD;
            Properties.Settings.Default.P0 = pracData.P0;
            Properties.Settings.Default.P2 = pracData.P2;
            Properties.Settings.Default.P3 = pracData.P3;
            Properties.Settings.Default.U = pracData.U;
            Properties.Settings.Default.POZNEK = pracData.POZNEK;
            Properties.Settings.Default.RT = pracData.RT;
            Properties.Settings.Default.POVRCH = pracData.POVRCH;

            Properties.Settings.Default.Save();
        }


        private void loadVstupniData()
        {
            pracData.DP = Properties.Settings.Default.DP;
            pracData.V = Properties.Settings.Default.V;
            pracData.T = Properties.Settings.Default.T;
            pracData.E = Properties.Settings.Default.E;
            pracData.PT = Properties.Settings.Default.PT;
            pracData.PS = Properties.Settings.Default.PS;
            pracData.PS = Properties.Settings.Default.PD;
            pracData.PS = Properties.Settings.Default.DD;
            pracData.P0 = Properties.Settings.Default.P0;
            pracData.P2 = Properties.Settings.Default.P2;
            pracData.P3 = Properties.Settings.Default.P3;
            pracData.U = Properties.Settings.Default.U;
            pracData.POZNEK = Properties.Settings.Default.POZNEK;
            pracData.RT = Properties.Settings.Default.RT;
            pracData.POVRCH = Properties.Settings.Default.POVRCH;
        }

        private void zapsaniDoSouboruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // zapsani do souboru
            zapisDoSouboru();
        }


        private void zapisDoSouboru()
        {

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.DefaultExt = "txt";
            saveFile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            saveFile.AddExtension = true;
            if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ps = saveFile.FileName.Trim();
                if (saveFile.FileName.Trim() != "")
                {
                    
                    using (StreamWriter sw = new StreamWriter(saveFile.FileName.Trim()))
                    {
                        Int32 i = 30;
                        foreach (PracovniData.Item item in pracData)
                        {
                            i++;
                            sw.WriteLine(" {0:D} L X{1,-9:F3} Y{2,-9:F3}", i, item.xe, item.ye); //  Convert.ToString(item.r);
                        }
                        sw.Flush();
                    }
                }
            }
        }

    }

}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySQLdataAdapterApp
{
    public partial class invulForm : Form
    {

        public event EventHandler<List<string>> wijzigengenOpslaan;
        public event EventHandler<List<string>> nieuweRecordOpslaan;
        private int mode = -1;

        mainForm mainform;
        public invulForm()
        {
            InitializeComponent();
        }

        public void recordAanpassen(int rows, string productNaam, string stock, string beschikbaarheid)
        {
            mode = rows;
            textBox1.Text = productNaam;
            textBox2.Text = stock;
            if(beschikbaarheid == "1")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
        }
        public void recordToevoegen()
        {
            mode = -1;
            textBox1.Text = "";
            textBox2.Text = "";
            checkBox1.Checked = false;
        }
        protected virtual void onWijzigengenOpslaan(List<string> e)
        {
            wijzigengenOpslaan?.Invoke(this, e);
        }

        protected virtual void onNieuweRecordOpslaan(List<string> e)
        {
            nieuweRecordOpslaan?.Invoke(this, e);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int val;
            if (Int32.TryParse(textBox2.Text, out val))
            {
                List<string> temp = new List<string>();
                temp.Add(textBox1.Text);
                temp.Add(textBox2.Text);
                if (checkBox1.Checked)
                {
                    temp.Add("1");
                }
                else
                {
                    temp.Add("0");
                }

                if (mode != -1)
                {
                    temp.Add(mode.ToString());
                    onWijzigengenOpslaan(temp);
                }
                else
                {
                    onNieuweRecordOpslaan(temp);
                }
            }
            else
            {
                MessageBox.Show("Je moet een geldig getal ingeven", "NUMMER ONGELDIG");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void invulForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}

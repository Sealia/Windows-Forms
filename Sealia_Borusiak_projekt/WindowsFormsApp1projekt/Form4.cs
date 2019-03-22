using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1projekt
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            foreach(DataRow row in Global.DTable.Rows)
            {
                comboBox1.Items.Add(row["Data"]);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i=0;i<Global.DTable.Rows.Count;i++)
            {
                DataRow row = Global.DTable.NewRow();
                row = Global.DTable.Rows[i];
                if (row["Data"].ToString()==comboBox1.Text)
                {
                    Global.DTable.Rows.Remove(Global.DTable.Rows[i]);
                }
            }
        }
    }
}

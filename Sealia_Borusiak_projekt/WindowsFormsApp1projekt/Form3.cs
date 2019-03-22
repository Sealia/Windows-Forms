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
    public partial class Form3 : Form
    {

        public Form3()
        {
            InitializeComponent();
            for (int i = 0; i < Global.Kategorie.Count(); i++)
            {
                for (int j = 0; j < Global.Kategorie[i].cwiczenia.Count(); j++)
                {
                    comboBox1.Items.Add(Global.Kategorie[i].cwiczenia[j]);
                }

            }
            this.AcceptButton = button1;


        }



        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }



        private void button1_Click(object sender, EventArgs e)
        {
            string sDate = monthCalendar1.SelectionRange.Start.ToShortDateString();
            bool jest = false;

            for (int i = 0; i < Global.DTable.Rows.Count; i++)
            {
                foreach (var item in Global.DTable.Rows[i].ItemArray)
                {
                    if (item.ToString() == sDate)
                    {
                        jest = true;
                        DataRow row = Global.DTable.Rows[i];
                        row[comboBox1.Text] = textBox1.Text;
                        Global.DTable.Rows[i][comboBox1.Text] = textBox1.Text;


                    }
                }
            }
            if (jest == false)
            {
                DataRow row = Global.DTable.NewRow();
                row["Data"] = sDate;
                row[comboBox1.Text] = textBox1.Text;
                Global.DTable.Rows.Add(row);
            }




        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            string tString = textBox1.Text;
            if (tString.Trim() == "") return;
            for (int i = 0; i < tString.Length; i++)
            {
                if (!char.IsNumber(tString[i]))
                {
                    MessageBox.Show("Podaj liczbę");
                    textBox1.Text = "";
                    return;
                }

            }

        }
    }
}

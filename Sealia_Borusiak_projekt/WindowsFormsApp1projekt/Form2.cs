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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                bool ok = true;
                for(int i=0; i<Global.Kategorie.Count();i++)
                {
                    if(Global.Kategorie[i].nazwa==textBox1.Text)
                    {
                        ok = false;
                    }
                }
                if(ok==false)
                {
                    MessageBox.Show("Taka kategoria już istnieje!");
                }
                if(ok==true)
                {
                    Kategoria kat;
                    kat = new Kategoria();
                    if (Global.Kategorie.Count() == 0)
                    {
                        kat.id = 1;
                    }
                    else
                    {
                        kat.id = Global.Kategorie[Global.Kategorie.Count - 1].id + 1;
                    }
                    kat.nazwa = textBox1.Text;
                    Global.Kategorie.Add(kat);
                    Global.F2.Close();
                }
                
            }

            if(radioButton2.Checked==true)
            {
                bool ok = true;
                if(Global.Kategorie.Count()!=0)
                {
                    for (int j = 0; j < Global.Kategorie.Count(); j++)
                    {
                        if(Global.Kategorie[j].cwiczenia!=null)
                        {
                            for (int t = 0; t < Global.Kategorie[j].cwiczenia.Count(); t++)
                            {
                                if (Global.Kategorie[j].cwiczenia[t] == textBox1.Text)
                                {
                                    ok = false;
                                }
                            }
                        }
                        
                    }
                }
                else
                {
                    MessageBox.Show("Nie dodałeś jeszcze żadnych kategorii!");
                }

                for (int i=0;i<Global.Kategorie.Count();i++)
                {
                    if(Global.Kategorie[i].nazwa == textBox2.Text)
                    {

                        if(ok==false)
                        {
                            MessageBox.Show("Takie ćwiczenie już istnieje!");
                        }
                        else
                        {
                            string k = textBox1.Text;
                            Kategoria kat = new Kategoria();
                            if (Global.Kategorie[i].cwiczenia == null)
                            {
                                kat.cwiczenia = new List<string>();
                                kat.cwiczenia.Add(k);
                                kat.id = Global.Kategorie[i].id;
                                kat.nazwa = Global.Kategorie[i].nazwa;
                                Global.Kategorie[i] = kat;
                            }
                            else
                            {
                                Global.Kategorie[i].cwiczenia.Add(k);
                            }
                            int c = 0;
                            for (int b = 0; b < Global.Kategorie.Count(); b++)
                            {
                                if(Global.Kategorie[b].cwiczenia!=null)
                                {
                                    for (int j = 0; j < Global.Kategorie[b].cwiczenia.Count(); j++)
                                    {
                                        c++;
                                        if (Global.Kategorie[i].cwiczenia[Global.Kategorie[i].cwiczenia.Count() - 1] == Global.Kategorie[b].cwiczenia[j])
                                        {
                                            Global.DTable.Columns.Add(Global.Kategorie[i].cwiczenia[Global.Kategorie[i].cwiczenia.Count() - 1], typeof(string)).SetOrdinal(c - 1);
                                            break;
                                        }

                                    }
                                }

                            }
                            Global.F2.Close();
                            i = Global.Kategorie.Count() + 1;
                        }


                        Global.F2.Close();


                    }

                }
            }
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label1.Visible = true;
                textBox2.Visible = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                label1.Visible = false;
                textBox2.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked==true)
            {
                bool ok = false;
                DialogResult result=MessageBox.Show("Usunięcie kategorii spowoduje usunięcie wszystkich ćwiczeń oraz innych powiązanych danych. Czy mimo tego chcesz usunąć kategorię?","Form2",MessageBoxButtons.YesNo);
                switch (result)
                {
                    case DialogResult.Yes:
                        ok = true;
                        break;
                    case DialogResult.No:
                        break;
                }

                bool o = false;
                if(ok==true)
                {
                    if(Global.Kategorie.Count()!=0)
                    {
                        for(int i=0; i<Global.Kategorie.Count();i++)
                        {
                            if(Global.Kategorie[i].nazwa==textBox1.Text)
                            {
                                o = true;
                            }
                        }

                        if(o==true)
                        {
                            for (int i = 0; i < Global.Kategorie.Count(); i++)
                            {
                                if (Global.Kategorie[i].nazwa == textBox1.Text)
                                {
                                    if (Global.Kategorie[i].cwiczenia != null)
                                    {
                                        for (int j = 0; j < Global.Kategorie[i].cwiczenia.Count(); j++)
                                        {
                                            Global.DTable.Columns.Remove(Global.Kategorie[i].cwiczenia[j]);


                                        }
                                    }

                                    Global.Kategorie.Remove(Global.Kategorie[i]);
                                }
                            }
                        }
                        if(o==false)
                        {
                            MessageBox.Show("Nie istnieje kategoria " + textBox1.Text);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nie możesz usunąć kategori jeśli jeszcze żadnej nie dodałeś!");
                    }
                    
                }

            }
            if(radioButton2.Checked==true)
            {
                bool ok = true;
                if (Global.Kategorie.Count() != 0)
                {
                    for (int j = 0; j < Global.Kategorie.Count(); j++)
                    {
                        if (Global.Kategorie[j].cwiczenia != null)
                        {
                            for (int t = 0; t < Global.Kategorie[j].cwiczenia.Count(); t++)
                            {
                                if (Global.Kategorie[j].cwiczenia[t] == textBox1.Text)
                                {
                                    ok = false;
                                }
                            }
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Nie dodałeś jeszcze żadnych kategorii!");
                }
                for (int i = 0; i < Global.Kategorie.Count(); i++)
                {
                    if(textBox2.Text==Global.Kategorie[i].nazwa)
                    {
                        if (ok == false)
                        {
                            for (int j = 0; j < Global.Kategorie[i].cwiczenia.Count(); j++)
                            {
                                if (textBox1.Text == Global.Kategorie[i].cwiczenia[j])
                                {
                                    Global.DTable.Columns.Remove(Global.Kategorie[i].cwiczenia[j]);
                                    break;
                                }

                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("Takie ćwiczenie nie istnieje!");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Nie istnieje taka kategoria!");
                    }

                }
            }
            Global.F2.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

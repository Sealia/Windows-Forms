using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1projekt;
using Microsoft.VisualBasic;





public struct Kategoria
{
    public string nazwa;
    public int id;
    public List<String> cwiczenia;
}




namespace WindowsFormsApp1projekt
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
            Global.SBind.DataSource = Global.DTable;
            Global.dataGridView1.DataSource = Global.SBind;
            Global.DTable.Columns.Add("Data");
            dataGridView1.DataSource = Global.DTable;


        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            Global.F2.ShowDialog();
            comboBox1.Items.Clear();
            comboBox1.Items.Add("all");
            for (int i = 0; i < Global.Kategorie.Count(); i++)
            {

                comboBox1.Items.Add(Global.Kategorie[i].nazwa);


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 F3 = new Form3();
            F3.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            var fl = string.Empty;
            var currentDir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            var extension = string.Empty;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = currentDir;
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.AddExtension = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveFileDialog.FileName;
                    fl = saveFileDialog.FileName;
                    extension = Path.GetExtension(saveFileDialog.FileName);

                    using (StreamWriter writer = File.CreateText(fl + ".xml"))
                    {
                        writer.WriteLine("<xml version=\"1.0\" standalone=\"true\"?>");
                        writer.WriteLine("<NewDataSet>");
                        int numberOfColumns = Global.DTable.Columns.Count;
                        int c = 0;

                        foreach (DataRow row in Global.DTable.Rows)
                        {

                            writer.WriteLine("\t<Table" + c + ">");
                            writer.WriteLine("\t<" + row["Data"] + ">");
                            for (int i = 0; i < Global.Kategorie.Count(); i++)
                            {

                                writer.WriteLine("\t\t< " + Global.Kategorie[i].nazwa + " >");
                                for (int j = 0; j < Global.Kategorie[i].cwiczenia.Count(); j++)
                                {
                                    writer.WriteLine("\t\t\t< " + Global.Kategorie[i].cwiczenia[j] + " >" + row[Global.Kategorie[i].cwiczenia[j]] + "< /" + Global.Kategorie[i].cwiczenia[j] + ">");


                                }
                                writer.WriteLine("\t\t< /" + Global.Kategorie[i].nazwa + " >");
                            }
                            writer.WriteLine("\t< /" + row["Data"] + " >");
                            writer.WriteLine("\t</Table" + c + ">");
                            c++;
                        }
                        writer.WriteLine("</NewDataSet>");
                    }
                    using (StreamWriter writer = File.CreateText(fl + ".csv"))
                    {
                        foreach (DataColumn column in Global.DTable.Columns)
                        {
                            if (column.ColumnName != "Data")
                            {
                                writer.Write(";");
                                writer.Write(column.ColumnName);
                            }

                        }
                        foreach (DataRow row in Global.DTable.Rows)
                        {
                            writer.WriteLine();
                            writer.Write(row["Data"] + ";");
                            for (int i = 0; i < Global.DTable.Rows.Count; i++)
                            {
                                writer.Write(row[i] + ";");
                            }
                        }
                    }
                    using (StreamWriter writer = File.CreateText(fl + "_tagi.xml"))
                    {
                        foreach (Kategoria k in Global.Kategorie)
                        {
                            writer.WriteLine("<" + k.nazwa + ">");
                            foreach (string c in k.cwiczenia)
                            {
                                writer.WriteLine("\t<" + c + ">");
                            }
                            writer.WriteLine("</" + k.nazwa + ">");
                        }
                    }
                }
            }





        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form4 F4 = new Form4();
            F4.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Global.DTable.Clear();
            var f2 = string.Empty;
            var filePath = string.Empty;
            string f = null;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    f2 = openFileDialog.FileName;
                    DirectoryInfo di = new DirectoryInfo(System.IO.Path.GetDirectoryName(Application.ExecutablePath));
                    FileInfo[] fl = di.GetFiles("*.xml");
                    foreach (FileInfo fi in fl)
                    {
                        f = Path.GetFileNameWithoutExtension(fi.Name);
                        if (f2.IndexOf(f) != -1)
                        {
                            break;
                        }
                    }
                    using (StreamReader sr = File.OpenText(f + "_tagi.xml"))
                    {
                        string linia = null;
                        while ((linia = sr.ReadLine()) != null)
                        {
                            if (linia.IndexOf("\t") != -1)
                            {
                                Kategoria kat = new Kategoria();
                                if (Global.Kategorie[Global.Kategorie.Count() - 1].cwiczenia == null)
                                {
                                    kat.cwiczenia = new List<string>();
                                    kat.cwiczenia.Add(linia.Substring(2, linia.Length - 3));
                                    kat.id = Global.Kategorie[Global.Kategorie.Count() - 1].id;
                                    kat.nazwa = Global.Kategorie[Global.Kategorie.Count() - 1].nazwa;
                                    Global.Kategorie[Global.Kategorie.Count() - 1] = kat;
                                }
                                else
                                {
                                    Global.Kategorie[Global.Kategorie.Count() - 1].cwiczenia.Add(linia.Substring(2, linia.Length - 2));

                                }
                            }
                            else
                            {
                                if (Global.Kategorie.Count() == 0)
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
                                    kat.nazwa = linia.Substring(1, linia.Length - 2);
                                    Global.Kategorie.Add(kat);
                                }
                                else if (Global.Kategorie[Global.Kategorie.Count() - 1].nazwa != linia.Substring(2, linia.Length - 3))
                                {
                                    Kategoria kat;
                                    kat = new Kategoria();
                                    kat.id = Global.Kategorie[Global.Kategorie.Count - 1].id + 1;
                                    kat.nazwa = linia.Substring(1, linia.Length - 2);
                                    Global.Kategorie.Add(kat);
                                }
                            }
                        }
                    }
                }
            }

            using (StreamReader sr = File.OpenText(f + ".xml"))
            {
                sr.ReadLine();
                sr.ReadLine();
                sr.ReadLine();
                string linia = null;
                string sDate = null;
                int kat = 0;
                bool k = true;
                while ((linia = sr.ReadLine()) != null && k==true)
                {
                    bool ok = false;
                    if (sDate == null)
                    {
                        sDate = linia.Substring(2, linia.Length - 4);
                        bool jest = false;
                        for (int i = 0; i < Global.DTable.Rows.Count; i++)
                        {
                            foreach (var item in Global.DTable.Rows[i].ItemArray)
                            {
                                if (item.ToString() == sDate)
                                {
                                    jest = true;
                                }
                            }
                        }
                        if (jest == false)
                        {
                            DataRow row = Global.DTable.NewRow();
                            row["Data"] = sDate;
                            Global.DTable.Rows.Add(row);
                        }
                        continue;
                    }
                    if(linia.IndexOf("\t\t\t") == -1 && linia.IndexOf("/")==-1)
                    {
                        for (int i = 0; i < Global.Kategorie.Count(); i++)
                        {

                            if (linia.IndexOf(Global.Kategorie[i].nazwa) != -1)
                            {
                                ok = true;
                                kat = i;
                                continue;
                            }

                        }
                        if (ok == false)
                        {
                            string l = linia.Substring(linia.IndexOf("<")+1);
                            MessageBox.Show("Nie istnieje kategoria" +l.Substring(0,l.IndexOf(">")-1) +" . Błędny plik");
                            Global.DTable.Clear();
                            k = false;
                        }

                    }
                    else if(linia.IndexOf("/")!=-1 && linia.IndexOf(sDate)!=-1)
                    {
                        sr.ReadLine();
                        sr.ReadLine();
                        sDate = null;
                    }
                    else if(linia.IndexOf(Global.Kategorie[kat].nazwa)!=-1 && linia.IndexOf("/")!=-1)
                    {
                        continue;
                    }
                    else
                    {
                        bool o = false;
                            for(int j=0; j<Global.Kategorie[kat].cwiczenia.Count();j++)
                            {
                                if(linia.IndexOf(Global.Kategorie[kat].cwiczenia[j])!=-1)
                                {
                                bool w = false;
                                foreach(DataColumn column in Global.DTable.Columns)
                                {
                                    if(column.ColumnName== Global.Kategorie[kat].cwiczenia[j])
                                    {
                                        w = true;
                                    }
                                }
                                if(w==false)
                                {
                                    Global.DTable.Columns.Add(Global.Kategorie[kat].cwiczenia[j]);
                                }
                                
                                string l = linia.Substring(linia.IndexOf(">") + 1);
                                    Global.DTable.Rows[Global.DTable.Rows.Count - 1][Global.Kategorie[kat].cwiczenia[j]]= l.Substring(0,1);
                                o = true;
                                continue;
                                }
                            }
                            if(o==false)
                         {
                            string l = linia.Substring(linia.IndexOf("<")+1);
                            MessageBox.Show("Nie istnieje ćwiczenie "+l.Substring(0,l.IndexOf(">")-1) + " . Błędny plik");
                            Global.DTable.Clear();
                            k = false;
                        }
                    }
                    
                        
                    

                }

            }
            comboBox1.Items.Clear();
            comboBox1.Items.Add("all");
            for (int i = 0; i < Global.Kategorie.Count(); i++)
            {

                comboBox1.Items.Add(Global.Kategorie[i].nazwa);


            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text=="all")
            {
                for (int i = 1; i < Global.dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Columns[i].Visible = true;
                }

            }
            else
            {
                int kat = 0;
                for (int i = 0; i < Global.Kategorie.Count(); i++)
                {
                    if (Global.Kategorie[i].nazwa == comboBox1.Text)
                    {
                        kat = i;
                    }
                }

                dataGridView1.DataSource = Global.DTable;
                panel1.Controls.Add(Global.dataGridView1);
                for (int i = 1; i < Global.dataGridView1.ColumnCount; i++)
                {
                    dataGridView1.Columns[i].Visible = false;
                }

                for (int i = 0; i < Global.Kategorie[kat].cwiczenia.Count(); i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        if (dataGridView1.Columns[j].Name == Global.Kategorie[kat].cwiczenia[i])
                        {
                            dataGridView1.Columns[j].Visible = true;
                        }
                    }
                }
            }
            
        }
    }
}

public static class Global
{
    public static DataTable DTable = new DataTable();
    public static BindingSource SBind = new BindingSource();
    public static DataGridView dataGridView1 = new DataGridView();
    public static List<Kategoria> Kategorie = new List<Kategoria>();
    public static Form2 F2 = new Form2();

}

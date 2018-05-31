using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using peiwei;

namespace RollCall
{
    public partial class Form1 : Form
    {
        string[,] classData = new string[10000,10];
        int count, rand;
        public Form1()
        {
            InitializeComponent();
            setTitle();
            setClass();
        }
        private void setClass() 
        {
            string name1 = "setClass.txt";
            StreamReader sr = new StreamReader(name1, Encoding.Default);
            string content = sr.ReadToEnd();
            string[] str = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            if (str.Length != 0)
            { 
                for (int i = 0; i < str.Length; i++)
                    comboBox1.Items.Add(str[i]);
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf(str[0]);
            }
            
        }
        private void setTitle() 
        {
            string name1 = "setTitle.txt";
            StreamReader sr = new StreamReader(name1, Encoding.Default);
            string content = sr.ReadToEnd();
            string[] str = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            labelSource.Text = str[0];
            labelTeacher.Text = str[1];
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否导入名单！", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                string name = "Class"+ (comboBox1.SelectedIndex+1).ToString() +".csv";
                DataTable dt = new DataTable();
                dt = CSVFileHelper.OpenCSV(name);
                int col = dt.Columns.Count;
                count = dt.Rows.Count;
                string[,] array = new string[count, col];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        array[i, j] = dt.Rows[i][j].ToString().ToUpper().Trim();
                        classData[i,j]=dt.Rows[i][j].ToString().ToUpper().Trim();
                    }
                }

            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Random ran = new Random();
            int n = ran.Next(0, count);
            //textBox3.Text = Convert.ToString(n + 1);
            rand = n+1;
            labelNumber.Text = classData[n,0];
            labelName.Text = classData[n,1]; 
            labelClass.Text = classData[n,2];
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否将此同学列入翘课名单？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (labelNumber.Text != "")
                {
                    StreamWriter sw = File.AppendText("Result.txt");
                    //sw.Write(rand);
                    //sw.Write("\t");
                    //sw.WriteLine(name[rand]);

                    sw.Write(labelClass.Text);
                    sw.Write("\t");
                    sw.Write(labelNumber.Text);
                    sw.Write("\t");
                    sw.WriteLine(labelName.Text);
                    sw.Close();
                }
                else {
                    MessageBox.Show("保存错误，保存数据不能为空", "警告");
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random rd = new Random();
            int n = rd.Next(0,count);
            n = n + 1;
            labelNumber.Text = classData[n, 0];
            labelName.Text = classData[n, 1];
            labelClass.Text = classData[n, 2];
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace WindowsFormsApplication6
{
    public partial class Form3 : Form
    {
        string path = "";
        static string str = "data source=puneeth;initial catalog=sai;integrated security=true";
        SqlConnection con = new SqlConnection(str);
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            pictureBox1.Visible = true;
            string sql = "insert into mybank values (@acno,@acname,@balance,@name,@pic)";
            SqlCommand cmd = new SqlCommand(sql, con);
            byte[] imagebt = null;
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            imagebt = br.ReadBytes((int)fs.Length);
            cmd.Parameters.AddWithValue("acno", comboBox1.Text);
            cmd.Parameters.AddWithValue("name", textBox2.Text);
            cmd.Parameters.AddWithValue("acname", textBox3.Text);
            cmd.Parameters.AddWithValue("balance", textBox4.Text);
            cmd.Parameters.AddWithValue("pic", imagebt);
            cmd.ExecuteNonQuery();
            MessageBox.Show("record has been inserted", "insert", MessageBoxButtons.OK);
            string se = "select acno from mybank";

            SqlCommand cmd1 = new SqlCommand(se, con);
            SqlDataReader dr = cmd1.ExecuteReader();
            comboBox1.Items.Clear();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr.GetValue(0));

            }
            con.Close();
            comboBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            pictureBox1.Visible = false;
            comboBox1.Focus();



        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                path = openFileDialog1.FileName;
 
            }

        }
    }
}

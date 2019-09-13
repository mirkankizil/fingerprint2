using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Data.OleDb;

namespace phingerprint
{
    public partial class Form2 : Form
    {
        SerialPort sp1 = new SerialPort();
        int sonuc;
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string bilgi1 = "", bigi2 = "", bilgi3 = "";

            int veri = Int32.Parse(textBox1.Text);
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Database1.accdb"); con.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Tablo1] where id=" + veri, con);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                bilgi3 = dr["kullanim"].ToString();
                bilgi1 = dr["ad"].ToString();
                bigi2 = dr["soyad"].ToString();

            }
            con.Close();
            if (bilgi3 == "1")
            {
                textBox2.Text = bilgi1;
                textBox3.Text = bigi2;
            }
            else
            {
                MessageBox.Show("kayda ulaşılmıyor");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string HEXKOD = "";
            int veri = Int32.Parse(textBox1.Text);
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Database1.accdb"); con.Open();
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Tablo1] where id=" + veri, con);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MessageBox.Show(dr["parmak"].ToString());

            }
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                sp1.Open();
                sp1.Write("K");
                timer1.Start();

            }

            catch (Exception ex)
            {
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string gelenveri;

            if (sp1.IsOpen)
            {

                gelenveri = sp1.ReadLine();
                if (gelenveri == "1")
                {
                    MessageBox.Show("BU KAYIT GÖRÜNTÜLENEMEZ");
                }
                else if (gelenveri == "Eslesme Bulunamadi\r")
                {
                    richTextBox1.Text += gelenveri;
                    sp1.Close();
                    timer1.Stop();

                }
                else if (gelenveri == "sonuc\r")
                {
                    gelenveri = sp1.ReadLine();
                    sp1.Close();
                    timer1.Stop();
                    if (gelenveri == "1\r")
                    {
                        MessageBox.Show("BU KAYIT GÖRÜNTÜLENEMEZ");
                    }
                    else
                    {
                        sonuc = Convert.ToInt32(gelenveri);
                        textBox1.Text = Convert.ToString(sonuc);
                    }

                }
                else if (gelenveri == "HATA\r")
                {
                    richTextBox1.Text += gelenveri;
                    timer1.Stop();
                    sp1.Close();
                    button1.PerformClick();

                }
                else
                {
                    richTextBox1.Text += gelenveri;
                }

            }
            else
            {
                sp1.Open();
                gelenveri = sp1.ReadLine();
                if (gelenveri == "1\r")
                {
                    MessageBox.Show("BU KAYIT GÖRÜNTÜLENEMEZ");
                }
                else if (gelenveri == "Eslesme Bulunamadi\r")
                {
                    richTextBox1.Text += gelenveri;
                    sp1.Close();
                    timer1.Stop();

                }
                else if (gelenveri == "sonuc\r")
                {
                    gelenveri = sp1.ReadLine();
                    sp1.Close();
                    timer1.Stop();
                    if (gelenveri == "1\r")
                    {
                        MessageBox.Show("BU KAYIT GÖRÜNTÜLENEMEZ");
                    }
                    else
                    {
                        sonuc = Convert.ToInt32(gelenveri);
                        textBox1.Text = Convert.ToString(sonuc);
                    }

                }
                else if (gelenveri == "HATA\r")
                {
                    richTextBox1.Text += gelenveri;
                    timer1.Stop();
                    sp1.Close();
                    button1.PerformClick();

                }
                else
                {
                    richTextBox1.Text += gelenveri;
                }
            }
        }
    }
}

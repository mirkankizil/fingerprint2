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
using System.Threading;

namespace phingerprint
{
    public partial class Form3 : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Database1.accdb");
        OleDbDataAdapter da;
        OleDbCommand cmd;
        SerialPort sp1 = new SerialPort();
        int syc = 0;
        string hexveri = "";
        public Form3()
        {
            InitializeComponent();
        }

      
        private void Form3_Load(object sender, EventArgs e)
        {

            sp1.BaudRate = 9600;
            sp1.PortName = Class1.com;
            button3.Enabled = false;
            button1.Enabled = false;


        }

        void kaydisil()
        {
            int veri = Int32.Parse(textBox1.Text);
            string sayi = "0";
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Database1.accdb");
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Tablo1] where id=" + veri, con);
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update Tablo1 set kullanim='" + sayi + "' where id=" + textBox1.Text + "";
            cmd.ExecuteNonQuery();
            con.Close();
            button3.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = true;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("ID giriniz..");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("isim giriniz..");
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("soyisim giriniz..");
            }
            else
            {
                string durum = "";
                int veri = Int32.Parse(textBox1.Text);
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Database1.accdb"); con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Tablo1] where id=" + veri, con);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    durum = dr["kullanim"].ToString();


                }
                con.Close();

                if (durum == "1")
                {
                    MessageBox.Show("BU ID KULLANILMAKTADIR");
                }
                else
                {
                    string sayi = "1";
                    cmd = new OleDbCommand();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "update Tablo1 set ad='" + textBox2.Text + "',soyad='" + textBox3.Text + "',kullanim='" + sayi + "' where id=" + textBox1.Text + "";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    button3.Enabled = true;
                    button1.Enabled = false;
                    button2.Enabled = false;
                    sp1.Open();
                    sp1.Write("B");
                    sp1.Close();
                }



            }
            if (richTextBox2.Text != "")
            {
                richTextBox2.Clear();
                button4.PerformClick();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            sp1.Open();
            String data = textBox1.Text;
            sp1.Write(data);
            timer1.Start();



        }



        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            sp1.Close();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            #region asas
            string gelenveri;



            if (sp1.IsOpen)
            {

                gelenveri = sp1.ReadLine();
                richTextBox1.Text += gelenveri;

                if (gelenveri == "HEXKOD\r")
                {
                    gelenveri = sp1.ReadLine();
                    cmd = new OleDbCommand();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "update Tablo1 set parmak='" + gelenveri + "' where id=" + textBox1.Text + "";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    timer1.Stop();
                    button1.Enabled = true;
                    sp1.Close();

                }
                else if (gelenveri == "HATA\r")
                {
                    timer1.Stop();
                    button1.Enabled = true;
                    sp1.Close();
                    DialogResult secenek = MessageBox.Show("Kayıt başarılı olamadı lütfen tekrar deneyiniz", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (secenek == DialogResult.Yes)
                    {
                        button3.PerformClick();
                    }
                    else
                    {
                        kaydisil();
                    }
                }


                /*    else if (gelenveri == "HEXKOD\r")
                    {
                        while (gelenveri == "BITTI\r")
                        {
                            gelenveri = sp1.ReadLine();
                            hexveri += sp1.ReadLine();

                        }
                         if (gelenveri == "BITTI\r")
                            {

                            }
                    }*/
            }

            #endregion

            #region elsesi
            else
            {
                sp1.Open();
                gelenveri = sp1.ReadLine();
                richTextBox1.Text += gelenveri;
                if (gelenveri == "HEXKOD\r")
                {
                    gelenveri = sp1.ReadLine();
                    cmd = new OleDbCommand();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "update Tablo1 set parmak='" + gelenveri + "' where id=" + textBox1.Text + "";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    timer1.Stop();
                    button1.Enabled = true;
                    sp1.Close();

                }
                if (gelenveri == "HATA\r")
                {
                    timer1.Stop();
                    button1.Enabled = true;
                    sp1.Close();
                    MessageBox.Show("Kayıt başarılı olamadı lütfen tekrar deneyiniz ");
                    kaydisil();
                }
                /*
                                else if (gelenveri == "HEXKOD\r")
                                {
                                    while (gelenveri == "BITTI\r")
                                    {
                                        gelenveri = sp1.ReadLine();
                                            hexveri += sp1.ReadLine();


                                    }
                                      if (gelenveri == "BITTI\r")
                                        {
                                            cmd = new OleDbCommand();
                                            con.Open();
                                            cmd.Connection = con;
                                            cmd.CommandText = "update Tablo1 set parmak='" + hexveri + "' where id=" + textBox1.Text + "";
                                            cmd.ExecuteNonQuery();
                                            con.Close();
                                            timer1.Stop();
                                            button1.Enabled = true;
                                            sp1.Close();
                                        }

                                }*/
            }
        }

        #endregion



        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button1.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            string bilgi = "";
            string bosid = "";
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Database1.accdb"); con.Open();
            for (int veri = 1; veri < 128; veri++)
            {



                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Tablo1] where id=" + veri, con);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    bilgi = dr["kullanim"].ToString();
                    bosid = dr["id"].ToString();
                }
                if (bilgi == "0")
                {
                    richTextBox2.Text += bosid + "  "; ;
                }


            }
            con.Close();
        }



        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace phingerprint
{
    public partial class Form4 : Form
    {
        SerialPort sp1 = new SerialPort();
        public Form4()
        {
            InitializeComponent();
        }
        
        private void Form4_Load(object sender, EventArgs e)
        {
            sp1.PortName = Class1.com;
            sp1.BaudRate = 9600;
            button1.Text = "GÖRÜNTÜLE";

        }
        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                if (button1.Text == "sil")
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
                    sp1.Open();
                    sp1.Write("S");
                    sp1.Write(textBox1.Text);
                    sp1.Close();
                    button1.Text = "GÖRÜNTÜLE";
                    label2.Text = "......";
                    label3.Text = ".....";
                }
                else
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
                        label2.Text = bilgi1;
                        label3.Text = bigi2;
                        button1.Text = "sil";
                    }
                    else
                    {
                        MessageBox.Show("Kayıt Silinmiş");
                    }

                }

            }

            catch (Exception ex)
            {
            }



        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            sp1.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {

            DialogResult secenek = MessageBox.Show("Tüm kayıtları silmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (secenek == DialogResult.Yes)
            {
                OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=Database1.accdb"); con.Open();
                string sayi = "0";
                for (int i = 2; i < 128; i++)
                {
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Tablo1] where id=" + i, con);
                    cmd = new OleDbCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "update Tablo1 set kullanim='" + sayi + "' where id=" + i.ToString() + "";
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                sp1.Open();
                sp1.Write("T");
                sp1.Close();
            }


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

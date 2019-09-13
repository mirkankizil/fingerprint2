using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace phingerprint
{
    public partial class Form5 : Form
    {
        SerialPort sp1 = new SerialPort();
        public Form5()
        {
            InitializeComponent();
        }
        int sonuc;
        int sayac = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "")
                {
                    if (textBox1.Text == "0000")
                    {
                        this.Hide();
                        var fr = new Form4();
                        fr.ShowDialog();
                    }
                    else
                    {
                        textBox1.Text = "";
                        sayac += 1;
                        if (sayac == 3)
                        {
                            textBox1.Enabled = false;
                            textBox1.Text = "3 defa hata giriş yaptınız";
                        }
                    }
                }

                else
                {
                    sp1.Open();
                    sp1.Write("K");
                    timer1.Start();
                }
            }

            catch (Exception ex)
            {
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string gelenveri;

            if (sp1.IsOpen)
            {

                gelenveri = sp1.ReadLine();
                if (gelenveri == "1\r")
                {
                    sp1.Close();
                    timer1.Stop();
                    this.Hide();
                    var fr = new Form4();
                    fr.ShowDialog();
                }
                else if (gelenveri == "Eslesme Bulunamadi\r")
                {
                    textBox1.Text = "yetkiniz yok";
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
                        this.Hide();
                        var fr = new Form4();
                        fr.ShowDialog();
                    }
                    else
                    {
                        textBox1.Text = "yetkiniz yok";
                        sp1.Close();
                        timer1.Stop();
                    }

                }
                else if (gelenveri == "HATA\r")
                {
                    textBox1.Text = "yetkiniz yok";
                    timer1.Stop();
                    sp1.Close();

                }
                else
                {
                    textBox1.Text = "yetkiniz yok";
                    sp1.Close();
                    timer1.Stop();
                }

            }
            else
            {
                sp1.Open();
                gelenveri = sp1.ReadLine();
                if (gelenveri == "1\r")
                {
                    sp1.Close();
                    timer1.Stop();
                    this.Hide();
                    var fr = new Form4();
                    fr.ShowDialog();

                }
                else if (gelenveri == "Eslesme Bulunamadi\r")
                {
                    textBox1.Text = "yetkiniz yok";
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
                        sp1.Close();
                        timer1.Stop();
                        this.Hide();
                        var fr = new Form4();
                        fr.ShowDialog();
                    }
                    else
                    {
                        sp1.Close();
                        timer1.Stop();
                        textBox1.Text = "yetkiniz yok";
                    }

                }
                else if (gelenveri == "HATA\r")
                {
                    textBox1.Text = "yetkiniz yok";
                    timer1.Stop();
                    sp1.Close();

                }
                else
                {
                    sp1.Close();
                    timer1.Stop();
                    textBox1.Text = "yetkiniz yok";
                }
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            sp1.PortName = Class1.com;
            sp1.BaudRate = 9600;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sp1.Close();
            timer1.Stop();
            this.Hide();
            Form1 frm1 = new Form1();
            frm1.Show();
        }
    }
}
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
    public partial class Form1 : Form
    {
        int portsayisi = 0;
        SerialPort sp1 = new SerialPort();
        string[] portlar = SerialPort.GetPortNames();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label2.Hide();
            timer1.Interval = 200;
            button3.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            timer1.Start();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedValue = 1;
            button1.Enabled = true;
            button3.Enabled = true;
            button2.Enabled = true;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Class1.com = comboBox1.SelectedItem.ToString();
            Hide();
            Form2 frm2 = new Form2();
            frm2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Class1.com = comboBox1.SelectedItem.ToString();
            Hide();
            Form3 frm3 = new Form3();
            frm3.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            DialogResult secenek = MessageBox.Show("SADECE YÖNETİÇİLER GİRİŞ YAPABİLİR SİZ YÖNETİCİMİSİNİZ ?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (secenek == DialogResult.Yes)
            {

                Class1.com = comboBox1.SelectedItem.ToString();
                Hide();
                Form5 frm5 = new Form5();
                frm5.Show();
            }

        }
       

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            string[] portlar = SerialPort.GetPortNames();
            foreach (string port in portlar)
            {
                comboBox1.Items.Add(port);
            }
            comboBox1.SelectedIndex = 0;
            if (portlar.Length >= 2)
            {
                comboBox1.Show();
                if (comboBox1.Text == "COM4")
                {
                    comboBox1.Show();
                    label2.Hide();
                    comboBox1.SelectedIndex = 1;
                }
                else
                {
                    comboBox1.Text = "";
                }
            }
            if (comboBox1.Text == "COM4")
            {
                label2.Show();
                comboBox1.Hide();
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }
    }
}

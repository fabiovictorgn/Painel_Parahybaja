using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace ProjetoBajaFinal
{
    public partial class PainelDeConeccao : Form
    {
        public PainelDeConeccao()
        {
            InitializeComponent();
        }

        private void PainelDeConeccao_Load(object sender, EventArgs e)
        {

        }

        private void BtnConectar_Click(object sender, EventArgs e)
        {
            if (serialArduino.IsOpen == false)
            {
                try
                {
                    serialArduino.PortName = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                    serialArduino.Open();
                }
                catch
                {
                    return;
                }

                if (serialArduino.IsOpen)
                {
                    BtnConectar.Text = "Desconectar";
                    comboBox1.Enabled = false;
                    BtnConectar.BackColor = Color.Red;
                }
            }
            else
            {
                try
                {
                    serialArduino.Close();
                    comboBox1.Enabled = true;
                    BtnConectar.Text = "Conectar";
                    BtnConectar.BackColor = Color.Green;
                }
                catch
                {
                    return;
                }
            }
        }

        private void atualizaListaCOMs()
        {
            int i = 0;
            bool quantDiferente = false;

            if (comboBox1.Items.Count == SerialPort.GetPortNames().Length)
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    if (comboBox1.Items[i++].Equals(s) == false)
                    {
                        quantDiferente = true;
                    }
                }
            }
            else
            {
                quantDiferente = true;
            }

            if (quantDiferente == false)
            {
                return;
            }

            comboBox1.Items.Clear();

            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }

            comboBox1.SelectedIndex = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            atualizaListaCOMs();
            if (serialArduino.IsOpen == true)
            {
                serialArduino.Write("x");
                string recebe1 = serialArduino.ReadLine();
                textBox1.Text = recebe1;
                aGauge1.Value = Convert.ToInt16(recebe1);
                serialArduino.Write("y");
                string recebe2 = serialArduino.ReadLine();
                circularProgressBar1.Value = Convert.ToInt16(recebe2);
                textBox2.Text = recebe2;
                serialArduino.Write("z");
                string recebe3 = serialArduino.ReadLine();
                chart1.Series["RPM"].Points.AddY(Convert.ToInt16(recebe3));
                textBox3.Text = recebe3;
            }
        }
    }
}

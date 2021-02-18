﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pascal_AirMax.Analizador;
using Pascal_AirMax.Manejador;

namespace Pascal_AirMax
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("se clickeo el ejecutar");

            string entrada = richTextBox1.Text; 
            Sintactico sintactico= new Sintactico();
            bool salida = sintactico.Analizar(entrada);

            if (salida)
            {
                var result = MessageBox.Show("Analisis correcto");

                richTextBox2.Text = Maestra.getInstancia.getOutput();

            }
            else
            {
                var result = MessageBox.Show("Analisis incorrecto");
            }
        }
    }
}

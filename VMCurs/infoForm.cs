using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VMCurs
{
    public partial class infoForm : Form
    {
        public infoForm(bool devmode)
        {
            InitializeComponent();
            this.devmode = devmode;
            if (devmode == true)
            {
                button1.Text = "Off";
            }
            else button1.Text = "On";
            textBox1.Text += "Калькулятор определенных интегралов";
            textBox1.Text += Environment.NewLine;
            textBox1.Text += Environment.NewLine;
            textBox1.Text += "Описание работы программы";
            textBox1.Text += Environment.NewLine;
            textBox1.Text += Environment.NewLine;
            textBox1.Text += "Программа производит преобразование строки, " +
                "введенной пользователем в математическое выражение, затем производит расчет определенного интеграла методом парабол Симпсона с заданной точностью," +
                "по завершению расчета программа выводит результат и оценку погрешности по правилу Рунге в виде графика.";
            textBox1.Text += Environment.NewLine;
            textBox1.Text += "Поддерживаемые операции: +, -, *, /, ^.";
            textBox1.Text += Environment.NewLine;
            textBox1.Text += "Поддерживаемые функции: sin, cos, tg, ctg, arc, log, ln, sqrt.";
            textBox1.Text += Environment.NewLine;
            textBox1.Text += "Для точной работы анализатора используйте скобки для аргументов функции!";
        }

        public bool devmode;                                       //переключатель режима разработчика
         
        private void button1_Click(object sender, EventArgs e)     //включение режима разработчика
        {
            if (button1.Text == "On")
            {
                devmode = true;
                button1.Text = "Off";
            }
            else
            {
                devmode = false;
                button1.Text = "On";
            }
        }
    }
}

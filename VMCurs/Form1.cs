using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VMCurs
{
    public partial class MainForm : Form
    {
        private Controller controller;                                    //объект контроллера
        private string chain;                                             //строка пользователя
        private bool devMode;                                             //переключатель режима разработчика

        private Font drawFont = new Font("Arial", 8);                     //шрифт для графика
        private SolidBrush drawBrush = new SolidBrush(Color.Black);       //цвет для графика
        double xScale = 500;                                              //основной масштабный коэффициент абсциссы
        double yScale = (int)Math.Pow(10, 8);                             //основной масштабный коэффициент ординаты
        int num1 = 1, num2 = 1;                                           //дополнительные масштабные коэффициенты
        double h0, eps0;                                                  //

        public MainForm()
        {
            InitializeComponent();
            devMode = false;
        }

        private void EqvButton_Click(object sender, EventArgs e)          //по нажатию кнопки происходит вычисление интеграла
        {
            resBox.Text = "";
            chain = InputBox.Text;
            if (aBox.Text != "" && bBox.Text != "" && errBox.Text != "")
            {
                controller = new Controller(chain, aBox.Text, bBox.Text, errBox.Text, devMode);
                resBox.Text = controller.Analyze();
                if (controller.GetErrorMessage != null)
                {
                    InputBox.Focus();
                    InputBox.Select(controller.GetErrorPosition - 1, 1);
                }
                else
                {
                    resBox.Text = controller.GetResult;
                    numericUpDown1.Visible = true;
                    numericUpDown1.Value = 1;
                    numericUpDown2.Visible = true;
                    numericUpDown2.Value = 1;
                    if (controller.GetHs.First != null) h0 = controller.GetHs.First.Value;
                    if (controller.GetEps.First != null) eps0 = controller.GetEps.First.Value;
                    CalcCoefficient(aBox.Text, bBox.Text, controller.GetEps);
                    DrowGraphic(e, controller.GetHs, controller.GetEps);
                }
            }
            else resBox.Text = "Ошибка: пустые параметры!";
        }

        private void CalcCoefficient(string astr, string bstr, LinkedList<double> eps)     //производится расчет основых коэффициентов
        {
            double a = 0, b = 1;
            if (Double.TryParse(astr, out a) && Double.TryParse(bstr, out b) && eps != null)
            {
                if (b - a != 0 && eps.ElementAt(0) != 0)
                {
                    xScale = 2 * 380 / (b - a);
                    yScale = 240 / eps.ElementAt(0);
                }
            }
        }

        private void DrowGraphic(EventArgs e, LinkedList<double> hs, LinkedList<double> eps)  //отрисовка графика
        {
            Graphics g = Graphics.FromHwnd(graphicBox.Handle);
            g.Clear(Form.DefaultBackColor);
            g.DrawLine(Pens.Black, 10, 250, 400, 250);
            g.DrawLine(Pens.Black, 390, 255, 400, 250);
            g.DrawLine(Pens.Black, 390, 245, 400, 250);
            g.DrawString("h", drawFont, drawBrush, 380, 235);
            g.DrawLine(Pens.Black, 10, 250, 10, 0);
            g.DrawLine(Pens.Black, 5, 15, 10, 0);
            g.DrawLine(Pens.Black, 15, 15, 10, 0);
            g.DrawString("E", drawFont, drawBrush, 0, 15);
            g.DrawString("0", drawFont, drawBrush, 0, 250);
            for (int i = 1; i < 5; i++)
            {
                g.DrawLine(Pens.Black, (int)(h0 * xScale / (4*num2) * i), 248, (int)(h0 * xScale / (4*num2) * i), 252);
                g.DrawString((Math.Round(h0 / (4*num2) * i, 3)).ToString(), drawFont, drawBrush, (int)(h0 * xScale / (4*num2) * i), 250);
            }
            for (int i = 1; i < 4; i++)
            {
                g.DrawLine(Pens.Black, 8, 250 - (int)(eps0 * yScale / (3*num1) * i), 12, 250 - (int)(eps0 * yScale / (3 * num1) * i));
                g.DrawString((Math.Round(eps0 / (3 * num1) * i, 10)).ToString(), drawFont, drawBrush, 12, 250 - (int)(eps0 * yScale / (3 * num1) * i));
            }
            g.DrawRectangle(Pens.Black, (int)(hs.ElementAt(0) * xScale), 250 - (int)(eps.ElementAt(0) * yScale), 3, 3);
            for (int i = 1; i < hs.Count; i++)
            {
                g.DrawRectangle(Pens.Black, (int)(hs.ElementAt(i) * xScale), 250 - (int)(eps.ElementAt(i) * yScale), 3, 3);
                g.DrawLine(Pens.Black, (int)(hs.ElementAt(i - 1) * xScale), 250 - (int)(eps.ElementAt(i - 1) * yScale), (int)(hs.ElementAt(i) * xScale), 250 - (int)(eps.ElementAt(i) * yScale));
            }
        }

        private void refButton_Click(object sender, EventArgs e)      //открытие справочного окна
        {
            infoForm iform = new infoForm(devMode);
            iform.ShowDialog();
            this.devMode = iform.devmode;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)               //расчет основных масштабных коэффициентов через дополнительные
        {
            if (numericUpDown1.Value > num1)
            {
                yScale /= num1;
                yScale *= (double)(numericUpDown1.Value);
            }
            else if (numericUpDown1.Value < num1)
            {
                yScale /= num1;
                yScale *= (double)(numericUpDown1.Value);
            }
            num1 = (int)numericUpDown1.Value;
            DrowGraphic(e, controller.GetHs, controller.GetEps);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value > num2)
            {
                xScale /= num2;
                xScale *= (double)(numericUpDown2.Value);
            }
            else if (numericUpDown2.Value < num2)
            {
                xScale /= num2;
                xScale *= (double)(numericUpDown2.Value);
            }
            num2 = (int)numericUpDown2.Value;
            DrowGraphic(e, controller.GetHs, controller.GetEps);
        }
    }
}

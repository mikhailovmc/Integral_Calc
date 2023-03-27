using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace VMCurs
{
    public partial class devForm : Form
    {
        public devForm(LinkedList<string> functionlist, MathModel.Tree.PNode head)
        {
            InitializeComponent();
            this.head = head;
            for (int i = 0; i < functionlist.Count; i++)
                textBox1.Text += functionlist.ElementAt(i) + Environment.NewLine;
        }

        private MathModel.Tree.PNode head;                                            //вершина дерева
        private int reccount = 0;                                                     //вспомогательный счетчик для отрисовки
        private Font drawFont = new Font("Arial", 8);                                 //шрифт для отрисовки значений в узлах
        private SolidBrush drawBrush = new SolidBrush(Color.Black);                   //цвет для отрисовки значений в узлах

        internal void PrintTree(Graphics g, int count, MathModel.Tree.PNode p)        //метод вывода дерева на консоль
        {
            g.DrawString(p.Data, drawFont, drawBrush, count, 10 * reccount);
            if (p.Left != null)
            {
                reccount++;
                int pos = count - 300 / (int)Math.Pow(2, reccount + 1);
                g.DrawLine(Pens.Black, pos, reccount * 10, count, reccount * 10);
                PrintTree(g, pos, p.Left);
            }
            if (p.Right != null)
            {
                reccount++;
                int pos = count + 300 / (int)Math.Pow(2, reccount + 1);
                g.DrawLine(Pens.Black, count, reccount * 10, pos, reccount * 10);
                PrintTree(g, pos, p.Right);
            }
            reccount--;
        }

        private void pictureBox1_Click(object sender, EventArgs e)                    //отрисовка дерева по нажатию на область    
        {
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            PrintTree(g, 150, head);
        }
    }
}

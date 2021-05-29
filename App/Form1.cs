using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;
using System.Drawing.Drawing2D;

namespace WindowsFormsApp8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Paint += new PaintEventHandler(draw_line);
        }
        static int factor(int x)
        {
            if (x <= 0) return 1;
            return x * factor(x - 1);
        }
        double cos(double x, int degree)
        {
            double res = 0;
            for (int i = 1; i < degree + 1; i++)
            {
                res += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 2) / factor(2 * i - 2);
            }
            return res;
        }
        double sin(double x, int degree)
        {
            double res = 0;
            for (int i = 1; i < degree + 1; i++)
            {
                res += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 1) / factor(2 * i - 1);
            }
            return res;
        }
        double arctan(double x, int degree)
        {
            double res = 0;
            if (-1 <= x && x <= 1)
            {
                for (int i = 1; i < degree + 1; i++)
                {
                    res += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 1) / (2 * i - 1);
                }
            }
            else
            {
                if (x >= 1)
                {
                    res += PI / 2;
                    for (int i = 0; i < degree; i++)
                    {
                        res -= Pow(-1, i) / ((2 * i + 1) * Pow(x, 2 * i + 1));
                    }
                }
                else
                {
                    res -= PI / 2;
                    for (int i = 0; i < degree; i++)
                    {
                        res -= Pow(-1, i) / ((2 * i + 1) * Pow(x, 2 * i + 1));
                    }
                }
            }
            return res;
        }
        void draw_line(object sender, PaintEventArgs e)
        {
            double x1 = 20;
            double y1 = 30;
            double x2 = 500;
            double y2 = 150;
            double step = 1;
            int degree = 2;
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Red, 1);
            g.DrawEllipse(new Pen(Color.Black, 2), (int)x1, (int)y1, 2, 2);
            g.DrawEllipse(new Pen(Color.Black, 2), (int)x2, (int)y2, 2, 2);
            GraphicsState gs;
            gs = g.Save();
            double error = Abs(x1 - x2) + Abs(y1 - y2);
            double angle = arctan((y2 - y1) / (x1 - x2), degree);
            while (true)
            {
                y1 -= step * sin(angle, degree);
                x1 += step * cos(angle, degree);
                g.DrawEllipse(pen, (int)x1, (int)y1, 1, 1);
                if (Abs(x1 - x2) + Abs(y1 - y2) > error)
                {
                    g.DrawString("Точность: " + error.ToString(), new Font("Arial", 14), new SolidBrush(Color.Black), 300, 10);
                    break;
                }
                else
                {
                    error = Abs(x1 - x2) + Abs(y1 - y2);
                }
            }
            g.Restore(gs);
        }
    }
}

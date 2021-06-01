using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
            Thread.Sleep(2000);
            g.Clear(Color.White);
            g.DrawLine(new Pen(Color.Black, 2), 100, 400, 100, 0);
            g.DrawLine(new Pen(Color.Black, 2), 100, 400, 600, 400);
            for (int i = 0; i < 5; i++)
            {
                g.DrawLine(new Pen(Color.Black, 2), 90, 400 - (i + 1) * 100, 110, 400 - (i + 1) * 100);
                g.DrawLine(new Pen(Color.Black, 2), 100 + (i + 1) * 100, 390, 100 + (i + 1) * 100, 410);
            }
            g.DrawEllipse(new Pen(Color.Black, 2), 200, 10, 2, 2);
            double old_x = 200;
            double old_y = 10;
            for (int j = 1; j < 6; j++)
            {
                degree = j;
                x1 = 20;
                y1 = 30;
                error = Abs(x1 - x2) + Abs(y1 - y2);
                angle = arctan((y2 - y1) / (x1 - x2), degree);
                while (true)
                {
                    y1 -= step * sin(angle, degree);
                    x1 += step * cos(angle, degree);
                    if (Abs(x1 - x2) + Abs(y1 - y2) > error)
                    {
                        if (j != 1) 
                        {
                            g.DrawLine(pen, (float)old_x, (float)old_y, 100 + 100 * j, 400 - (float)error * 100);
                            old_x = 100 + 100 * j;
                            old_y = 400 - error * 100;
                            g.DrawEllipse(new Pen(Color.Black, 2), 100 + 100 * j, 400 - (float)error * 100, 2, 2);
                        }
                        break;
                    }
                    else
                    {
                        error = Abs(x1 - x2) + Abs(y1 - y2);
                    }
                }
            }
            gs = g.Save();
            g.Restore(gs);
        }
    }
}

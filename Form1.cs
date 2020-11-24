using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Graphics3DS;

namespace Ejercicio14.Dona3D
{
    public partial class Form1 : Form
    {
        Graphics g;
        Graphics3D g3;
        Point3DF[] fig;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fig = new Point3DF[4];

            // Dibujar los puntos para empezar a trazar las líneas
            fig[0] = new Point3DF(0, 30, 0);
            fig[1] = new Point3DF(10, 30, 0);
            fig[2] = new Point3DF(10, 25, 0);
            fig[3] = new Point3DF(8, 23, 0);
        }

        private void Whiteboard_Paint(object sender, PaintEventArgs e)
        {
            // Instanciar el objeto Graphics para dibujar sobre el PictureBox
            g = e.Graphics;
            // Efecto burbujas en el objeto a dibujar
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g3 = new Graphics3D(g);
            // Centrar la figura en el centro de la pantalla
            e.Graphics.TranslateTransform(Whiteboard.Width / 2, 100);
            // Variable para tomar el valor de los grados de rotación del campo numérico
            double grados_x = 0;
            double grados_y = 0; 

            for (int i = 0; i < Rotar.Value; i++)
            {
                fig[0] = RotarXY(new Point3DF(0, 100, 0), grados_x, grados_y);
                fig[1] = RotarXY(new Point3DF(60, 100, 0), grados_x, grados_y);
                fig[2] = RotarXY(new Point3DF(60, 200, 0), grados_x, grados_y);
                fig[3] = RotarXY(new Point3DF(0, 200, 0), grados_x, grados_y);

                g3.DrawBezier3D(new Pen(Color.Green), fig[0], fig[1], fig[2], fig[3]);
                grados_x += 3.6;
            }
        }

        private void Rotar_ValueChanged(object sender, EventArgs e)
        {
            Whiteboard.Refresh();
        }

        // Método de rotación
        private Point3DF RotarXY(Point3DF p1, double ang_x, double ang_y)
        {
            // Variables para rotar tanto en X como en Y
            Point3DF aux = new Point3DF();
            Point3DF aux2 = new Point3DF();
            double grados_x = (ang_x * Math.PI) / 180;
            double grados_y = (ang_y * Math.PI) / 180;

            //Rotacion y
            aux.X = Convert.ToSingle(p1.X * Math.Cos(grados_x) - p1.Z * Math.Sin(grados_x));
            aux.Y = p1.Y;
            aux.Z = 0; // Convert.ToSingle(p1.Z * Math.Cos(grados_x) + p1.X * Math.Sin(grados_x));
            //Rotación x
            aux2.X = aux.X;
            aux2.Y = Convert.ToSingle(aux.Y * Math.Cos(grados_y) - aux.Z * Math.Sin(grados_y));
            aux2.Z = 0; // Convert.ToSingle(aux.Z * Math.Cos(grados_y) + aux.Y * Math.Sin(grados_y));

            return aux2;
        }
    }
}

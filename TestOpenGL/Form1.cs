using System;
using System.Windows.Forms;

namespace TestOpenGL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        glGraphics glgraphics = new glGraphics();

        private void glControl1_Load(object sender, EventArgs e)
        {
            int texID = glgraphics.LoadTexture("../../picture.png");
            glgraphics.texturesIDs.Add(texID);
            glgraphics.Setup(glControl1.Width, glControl1.Height);
            Application.Idle += Application_Idle;
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            glgraphics.Update();
            glControl1.SwapBuffers();
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            float widthCoef = (e.X - glControl1.Width * 0.5f) / (float)glControl1.Width;
            float heightCoef = (-e.Y + glControl1.Height * 0.5f) / (float)glControl1.Height;
            glgraphics.latitude = heightCoef * 180;
            glgraphics.longitude = widthCoef * 360;
        }

        void Application_Idle(object sender, EventArgs e)
        {
            while (glControl1.IsIdle)
                glControl1.Refresh();
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                glgraphics.change_z++;
                glgraphics.Update();
            }
            if (e.Button == MouseButtons.Right)
            {
                glgraphics.change_z--;
                glgraphics.Update();
            }
        }

        private void glControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                glgraphics.radius -= e.Delta / 120;
                glgraphics.Update();
            }
            if (e.Delta < 0)
            {
                glgraphics.radius -= e.Delta / 120;
                glgraphics.Update();
            }
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                glgraphics.change_x--;
                glgraphics.Update();
            }
            if (e.KeyCode == Keys.D)
            {
                glgraphics.change_x++;
                glgraphics.Update();
            }
            if (e.KeyCode == Keys.W)
            {
                glgraphics.change_y++;
                glgraphics.Update();
            }
            if (e.KeyCode == Keys.S)
            {
                glgraphics.change_y--;
                glgraphics.Update();
            }
            if (e.KeyCode == Keys.Oemplus)
            {
                glgraphics.change_size *= 1.25f;
                glgraphics.Update();
            }
            if (e.KeyCode == Keys.OemMinus)
            {
                glgraphics.change_size /= 1.25f;
                glgraphics.Update();
            }
        }
    }
}

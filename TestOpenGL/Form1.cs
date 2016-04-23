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
            for(int i = 0; i < 6; i++)
                dataGridView1.Rows.Add();
            dataGridView1[0, 0].Value = "RADIUS";
            dataGridView1[0, 1].Value = "SIZE";
            dataGridView1[0, 2].Value = "X";
            dataGridView1[0, 3].Value = "Y";
            dataGridView1[0, 4].Value = "Z";
            dataGridView1[0, 5].Value = "LATITUDE";
            dataGridView1[0, 6].Value = "LONGITUDE";
            dataGridView1[1, 0].Value = glgraphics.radius;
            dataGridView1[1, 1].Value = glgraphics.change_size;
            dataGridView1[1, 2].Value = glgraphics.change_x;
            dataGridView1[1, 3].Value = glgraphics.change_y;
            dataGridView1[1, 4].Value = glgraphics.change_z;
            dataGridView1[1, 5].Value = glgraphics.latitude;
            dataGridView1[1, 6].Value = glgraphics.longitude;
            Application.Idle += Application_Idle;
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            glgraphics.Update();
            glControl1.SwapBuffers();
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                float widthCoef = (e.X - glControl1.Width * 0.5f) / (float)glControl1.Width;
                float heightCoef = (-e.Y + glControl1.Height * 0.5f) / (float)glControl1.Height;
                glgraphics.latitude = heightCoef * 180;
                glgraphics.longitude = widthCoef * 360;
                dataGridView1[1, 5].Value = glgraphics.latitude;
                dataGridView1[1, 6].Value = glgraphics.longitude;
            }
        }

        void Application_Idle(object sender, EventArgs e)
        {
            while (glControl1.IsIdle)
                glControl1.Refresh();
        }

        private void glControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            glgraphics.radius -= e.Delta / 120;
            dataGridView1[1, 0].Value = glgraphics.radius;
            glgraphics.Update();
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                glgraphics.change_x--;
                dataGridView1[1, 2].Value = glgraphics.change_x;
                glgraphics.Update();
            }
            if (e.KeyCode == Keys.D)
            {
                glgraphics.change_x++;
                dataGridView1[1, 2].Value = glgraphics.change_x;
                glgraphics.Update();
            }
            if (e.KeyCode == Keys.W)
            {
                glgraphics.change_y++;
                dataGridView1[1, 3].Value = glgraphics.change_y;
                glgraphics.Update();
            }
            if (e.KeyCode == Keys.S)
            {
                glgraphics.change_y--;
                dataGridView1[1, 3].Value = glgraphics.change_y;
                glgraphics.Update();
            }
            if (e.KeyCode == Keys.E)
            {
                glgraphics.change_z++;
                dataGridView1[1, 4].Value = glgraphics.change_z;
                glgraphics.Update();
            }
            if (e.KeyCode == Keys.Q)
            {
                glgraphics.change_z--;
                dataGridView1[1, 4].Value = glgraphics.change_z;
                glgraphics.Update();
            }
            if (e.KeyCode == Keys.Oemplus)
            {
                glgraphics.change_size *= 1.25f;
                dataGridView1[1, 1].Value = glgraphics.change_size;
                glgraphics.Update();
            }
            if (e.KeyCode == Keys.OemMinus)
            {
                glgraphics.change_size /= 1.25f;
                dataGridView1[1, 1].Value = glgraphics.change_size;
                glgraphics.Update();
            }
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Cursor = Cursors.SizeAll;
            }
        }

        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Cursor = Cursors.Default;
            }
        }
    }
}

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
            int texID0 = glgraphics.LoadTexture("../../planet_textures/sunmap.bmp");
            glgraphics.texturesIDs.Add(texID0);
            int texID1 = glgraphics.LoadTexture("../../planet_textures/mercurymap.bmp");
            glgraphics.texturesIDs.Add(texID1);
            int texID2 = glgraphics.LoadTexture("../../planet_textures/venusmap.bmp");
            glgraphics.texturesIDs.Add(texID2);
            int texID3 = glgraphics.LoadTexture("../../planet_textures/earthmap.bmp");
            glgraphics.texturesIDs.Add(texID3);
            int texID4 = glgraphics.LoadTexture("../../planet_textures/marsmap.bmp");
            glgraphics.texturesIDs.Add(texID4);
            int texID5 = glgraphics.LoadTexture("../../planet_textures/jupitermap.bmp");
            glgraphics.texturesIDs.Add(texID5);
            int texID6 = glgraphics.LoadTexture("../../planet_textures/saturnmap.bmp");
            glgraphics.texturesIDs.Add(texID6);
            int texID7 = glgraphics.LoadTexture("../../planet_textures/uranusmap.bmp");
            glgraphics.texturesIDs.Add(texID7);
            int texID8 = glgraphics.LoadTexture("../../planet_textures/neptunmap.bmp");
            glgraphics.texturesIDs.Add(texID8);
            glgraphics.Setup(glControl1.Width, glControl1.Height);
            for(int i = 0; i < 8; i++)
                dataGridView1.Rows.Add();
            dataGridView1[0, 0].Value = "Расстояние (а.е.м.)";
            dataGridView1[0, 1].Value = "Размер (отн. Земли)";
            dataGridView1[0, 2].Value = "Смещение по X";
            dataGridView1[0, 3].Value = "Смещение по Y";
            dataGridView1[0, 4].Value = "Смещение по Z";
            dataGridView1[0, 5].Value = "Угол наклона";
            dataGridView1[0, 6].Value = "Угол поворота";
            dataGridView1[0, 7].Value = "glVersion";
            dataGridView1[0, 8].Value = "glslVersion";
            dataGridView1[1, 0].Value = glgraphics.radius;
            dataGridView1[1, 1].Value = glgraphics.change_size;
            dataGridView1[1, 2].Value = glgraphics.change_x;
            dataGridView1[1, 3].Value = glgraphics.change_y;
            dataGridView1[1, 4].Value = glgraphics.change_z;
            dataGridView1[1, 5].Value = glgraphics.latitude;
            dataGridView1[1, 6].Value = glgraphics.longitude;
            dataGridView1[1, 7].Value = glgraphics.glVersion;
            dataGridView1[1, 8].Value = glgraphics.glslVersion;
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
            if (e.KeyCode == Keys.Add)
            {
                glgraphics.change_size *= 1.25f;
                dataGridView1[1, 1].Value = glgraphics.change_size;
                glgraphics.Update();
            }
            if (e.KeyCode == Keys.Subtract)
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

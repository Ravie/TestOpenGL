using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;


namespace TestOpenGL
{
    class glGraphics
    {
        Vector3 cameraPosition = new Vector3(2, 3, 4);
        Vector3 cameraDirecton = new Vector3(0, 0, 0);
        Vector3 cameraUp = new Vector3(0, 0, 1);

        public float latitude = 47.98f;
        public float longitude = 60.41f;
        public float radius = 15f;

        public float rotateAngle = 5.0f;
        public const float delta = 0.5f;

        public const float materialShininess = 100;

        public List<int> texturesIDs = new List<int>();

        public void Setup(int width, int height)
        {
            GL.ClearColor(Color.Aquamarine);
            GL.ShadeModel(ShadingModel.Flat);
            GL.Enable(EnableCap.DepthTest);

            Matrix4 perspectiveMat = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, width / (float)height, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiveMat);

            SetupLightning();
        }

        public void Update()
        {
            rotateAngle += delta;
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 viewMat = Matrix4.LookAt(cameraPosition, cameraDirecton, cameraUp);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref viewMat);
            Render();
            cameraPosition = new Vector3
            (
                (float)(radius * Math.Cos(Math.PI / 180.0f * latitude) * Math.Cos(Math.PI / 180.0f * longitude)),
                (float)(radius * Math.Cos(Math.PI / 180.0f * latitude) * Math.Sin(Math.PI / 180.0f * longitude)),
                (float)(radius * Math.Sin(Math.PI / 180.0f * latitude))
            );

        }

        private void drawTestQuad()
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Blue);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Color3(Color.Red);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Color3(Color.White);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Color3(Color.Green);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.End();
        }

        private void drawTexturedQuad()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texturesIDs[0]);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Blue);
            GL.TexCoord2(0.0, 0.0);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Color3(Color.Red);
            GL.TexCoord2(0.0, 1.0);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Color3(Color.White);
            GL.TexCoord2(1.0, 1.0);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Color3(Color.Green);
            GL.TexCoord2(1.0, 0.0);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }

        private void drawSphere(double r, int nx, int ny)
        {
            int ix, iy;
            double x, y, z;
            for (iy = 0; iy < ny; ++iy)
            {
                GL.Begin(PrimitiveType.QuadStrip);
                GL.Color3(Color.DarkMagenta);
                for (ix = 0; ix <= nx; ++ix)
                {
                    x = r * Math.Sin(iy * Math.PI / ny) * Math.Cos(2 * ix * Math.PI / nx);
                    y = r * Math.Sin(iy * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx);
                    z = r * Math.Cos(iy * Math.PI / ny);
                    GL.Normal3(x, y, z);
                    GL.Vertex3(x, y, z);

                    x = r * Math.Sin((iy + 1) * Math.PI / ny) * Math.Cos(2 * ix * Math.PI / nx);
                    y = r * Math.Sin((iy + 1) * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx);
                    z = r * Math.Cos((iy + 1) * Math.PI / ny);
                    GL.Normal3(x, y, z);
                    GL.Vertex3(x, y, z);
                }
                GL.End();
            }
        }
        
        private void drawPoint(int x, int y, int z)
        {
            GL.Begin(PrimitiveType.Points);
            GL.Color3(Color.Black);
            GL.Vertex3(x, y, z);
            GL.End();
        }
        
        private void drawLine(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex3(x1, y1, z1);
            GL.Vertex3(x2, y2, z2);
            GL.End();
        }

        private void drawCube(int x, int y, int z, int EdgeSize)
        {

            GL.Begin(PrimitiveType.QuadStrip);
            GL.Color3(Color.Gold);
            GL.Vertex3(x, y, z);
            GL.Vertex3(x, y, z + EdgeSize);
            GL.Vertex3(x + EdgeSize, y, z);
            GL.Vertex3(x + EdgeSize, y, z + EdgeSize);
            GL.Vertex3(x + EdgeSize, y + EdgeSize, z);
            GL.Vertex3(x + EdgeSize, y + EdgeSize, z + EdgeSize);
            GL.Vertex3(x, y + EdgeSize, z);
            GL.Vertex3(x, y + EdgeSize, z + EdgeSize);
            GL.Vertex3(x, y, z);
            GL.Vertex3(x, y, z + EdgeSize);
            GL.End();

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Gold);
            GL.Vertex3(x, y, z);
            GL.Vertex3(x + EdgeSize, y, z);
            GL.Vertex3(x + EdgeSize, y + EdgeSize, z);
            GL.Vertex3(x, y + EdgeSize, z);
            GL.End();

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Gold);
            GL.Vertex3(x, y + EdgeSize, z + EdgeSize);
            GL.Vertex3(x, y, z + EdgeSize);
            GL.Vertex3(x + EdgeSize, y, z + EdgeSize);
            GL.Vertex3(x + EdgeSize, y + EdgeSize, z + EdgeSize);
            GL.End();
        }

        private void drawTriangle(int x, int y, int z, int EdgeSize)
        {
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.Indigo);
            GL.Vertex3(x, y, z);
            GL.Vertex3(x + EdgeSize, y, z);
            GL.Vertex3(x, y + EdgeSize, z);
            GL.End();
        }

        private void drawTriangleStrip(double x, double y, double z, int EdgeSize)
        {
            GL.Begin(PrimitiveType.TriangleStrip);
            GL.Color3(Color.Khaki);
            GL.Vertex3(x, y, z);
            GL.Vertex3(x + EdgeSize, y, z);
            GL.Vertex3(x, y + EdgeSize, z);
            GL.End();
        }

        private void drawTriangleFan(double x, double y, double z, int EdgeSize)
        {
            GL.Begin(PrimitiveType.TriangleFan);
            GL.Color3(Color.Lavender);
            GL.Vertex3(x, y, z);
            GL.Vertex3(x + EdgeSize, y, z);
            GL.Vertex3(x, y + EdgeSize, z);
            GL.End();
        }

        public void Render()
        {
            //GL.LineWidth(2);
            GL.Color3(Color.Red);
            drawLine(0, 0, 0, 100, 0, 0);
            GL.Color3(Color.Green);
            drawLine(0, 0, 0, 0, 100, 0);
            GL.Color3(Color.Blue);
            drawLine(0, 0, 0, 0, 0, 100);

            drawSphere(1.0f, 30, 30);
            //drawTestQuad();
            
            GL.PointSize(5);
            drawPoint(2, 2, 2);
            
            drawTriangle(0, 0, 0, 2);
            //drawTriangleStrip(0.1, 0.1, 0.1, 2);
            //drawTriangleFan(0.2, 0.2, 0.2, 2);

            GL.PushMatrix();
            GL.Translate(-2, -2, -2);
            drawCube(0, 0, 0, 1);
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Rotate(rotateAngle, 0, 0, 1);
            GL.Scale(1.5f, 1.5f, 1f);
            drawTexturedQuad();
            GL.PopMatrix();

            GL.PushMatrix();
            GL.Rotate(rotateAngle, 1, 0, 0);
            drawPoint(1, 1, 1);
            GL.PopMatrix();
        }

        public int LoadTexture(String filePath)
        {
            try
            {
                Bitmap image = new Bitmap(filePath);
                int texID = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, texID);
                BitmapData data = image.LockBits
                (
                    new Rectangle(0, 0, image.Width, image.Height),
                    ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb
                );
                GL.TexImage2D
                (
                    TextureTarget.Texture2D, 0,
                    PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0
                );
                image.UnlockBits(data);
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
                return texID;
            }
            catch (System.IO.FileNotFoundException е)
            {
                return -1;
            }
        }
        
        public void SetupLightning()
        {
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Light1);
            GL.Enable(EnableCap.ColorMaterial);

            Vector4 lightPosition = new Vector4(1.0f, 1.0f, 4.0f, 0.0f);
            GL.Light(LightName.Light0, LightParameter.Position, lightPosition);
            Vector4 ambientColor = new Vector4(0.2f, 0.2f, 0.2f, 1.0f);
            GL.Light(LightName.Light0, LightParameter.Ambient, ambientColor);
            Vector4 diffuseColor = new Vector4(0.6f, 0.6f, 0.6f, 1.0f);
            GL.Light(LightName.Light0, LightParameter.Diffuse, diffuseColor);

            Vector4 lightPosition1 = new Vector4(1.0f, 4.0f, 1.0f, 0.0f);
            GL.Light(LightName.Light1, LightParameter.Position, lightPosition1);
            Vector4 ambientColor1 = new Vector4(0.1f, 0.1f, 0.1f, 1.0f);
            GL.Light(LightName.Light1, LightParameter.Ambient, ambientColor1);
            Vector4 diffuseColor1 = new Vector4(0.8f, 0.8f, 0.8f, 1.0f);
            GL.Light(LightName.Light1, LightParameter.Diffuse, diffuseColor1);

            Vector4 materialSpecular = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, materialSpecular);
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, materialShininess);

        }
    }
}

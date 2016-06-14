using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DG110117Upp4
{
    public class Plane
    {
        private VertexPositionColor[] vertices;
        private short[] indices;

        private VertexBuffer vb;
        private IndexBuffer ib;

        private Matrix objectWorld;
        private GraphicsDevice graphics;


        private short FUL = 0;
        private short FUR = 1;
        private short FLR = 2;
        private short FLL = 3;

        private short BLL = 4;
        private short BUL = 5;
        private short BUR = 6;
        private short BLR = 7;


        public Plane(GraphicsDevice graphicsDevice)
        {
            objectWorld = Matrix.Identity;
            this.graphics = graphicsDevice;

        }

        public void Initialize()
        {
            SetUpVertices();
            SetUpIndices();
        }

        private void SetUpVertices()
        {
            vertices = new VertexPositionColor[8];

            // Front Upper Left
            vertices[0] = new VertexPositionColor(new Vector3(0, 0, 0), Color.Yellow);
            // Front Upper Right
            vertices[1] = new VertexPositionColor(new Vector3(1, 0, 0), Color.Green);
            // Front Lower Right
            vertices[2] = new VertexPositionColor(new Vector3(1, -1, 0), Color.White);
            // Front Lower Left
            vertices[3] = new VertexPositionColor(new Vector3(0, -1, 0), Color.Gold);

            // Back Lower Left
            vertices[4] = new VertexPositionColor(new Vector3(0, -1, -1), Color.Black);
            // Back Upper Left
            vertices[5] = new VertexPositionColor(new Vector3(0, 0, -1), Color.Beige);
            // Back Upper Right
            vertices[6] = new VertexPositionColor(new Vector3(1, 0, -1), Color.Aquamarine);
            // Back Lower Right
            vertices[7] = new VertexPositionColor(new Vector3(1, -1, -1), Color.Cyan);

            vb = new VertexBuffer(graphics, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
            vb.SetData<VertexPositionColor>(vertices);
        }

        private void SetUpIndices()
        {
            indices = new short[36];

            // Top Top Left
            indices[0] = FUL;
            indices[1] = BUL;
            indices[2] = BUR;

            // Top Bottom Right
            indices[3] = BUR;
            indices[4] = FUR;
            indices[5] = FUL;
            // Front Top Left
            indices[6] = FUL;
            indices[7] = FUR;
            indices[8] = FLL;
            // Front Bottom Right
            indices[9] = FLL;
            indices[10] = FUR;
            indices[11] = FLR;
            // Right Top Left
            indices[12] = FUR;
            indices[13] = BUR;
            indices[14] = FLR;
            // Right Bottom Right
            indices[15] = FLR;
            indices[16] = BUR;
            indices[17] = BLR;
            // Back Top Left
            indices[18] = BUR;
            indices[19] = BUL;
            indices[20] = BLR;
            // Back Bottom Right
            indices[21] = BLR;
            indices[22] = BUL;
            indices[23] = BLL;
            // Left Top Left
            indices[24] = BUL;
            indices[25] = FUL;
            indices[26] = BLL;
            // Left Bottom Right
            indices[27] = BLL;
            indices[28] = FUL;
            indices[29] = FLL;
            // Bottom Top Left
            indices[30] = FLL;
            indices[31] = FLR;
            indices[32] = BLL;
            // Bottom Bottom Right
            indices[33] = BLL;
            indices[34] = FLR;
            indices[35] = BLR;
            /**/
            ib = new IndexBuffer(graphics, typeof(short), indices.Length, BufferUsage.WriteOnly);
            ib.SetData<short>(indices);
        }

        public void Update(GameTime gameTime)
        {
            this.objectWorld *= Matrix.CreateRotationX(MathHelper.PiOver4 / 20) * Matrix.CreateRotationY(MathHelper.PiOver4/50);
        }


        public void Draw(GameTime gametime, BasicEffect effect)
        {
            // set vertexbuffer and indices
            graphics.SetVertexBuffer(vb);
            graphics.Indices = ib;
            effect.World = objectWorld;

            effect.CurrentTechnique.Passes[0].Apply();
            graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertices.Length, 0, indices.Length/3);

        }


    }
}

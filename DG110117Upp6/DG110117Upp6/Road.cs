using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DG110117Upp6
{
    public class Road
    {
        private Game game;
        private Matrix objectWorld;
        private Texture2D texture;
        private VertexPositionTexture[] vertices;
        private short[] indices;

        private VertexBuffer vb;
        private IndexBuffer ib;

        private int size = 5;
        private int numberOfPicures = 50;


        public Road(Game game)
        {
            this.game = game;
            texture = game.Content.Load<Texture2D>("Pictures/setts");
        }

        public void Initialize()
        {
            InitializeVertices();
            InitializeIndices();
        }

        private void InitializeVertices()
        {
            vertices = new VertexPositionTexture[4];

            vertices[0] = new VertexPositionTexture(new Vector3(0, 0, 0), new Vector2(0, 0));
            vertices[1] = new VertexPositionTexture(new Vector3(0, 0, -size), new Vector2(0, numberOfPicures));
            vertices[2] = new VertexPositionTexture(new Vector3(size, 0, -size), new Vector2(numberOfPicures, numberOfPicures));
            vertices[3] = new VertexPositionTexture(new Vector3(size, 0, 0), new Vector2(numberOfPicures, 0));

            vb = new VertexBuffer(game.GraphicsDevice, typeof(VertexPositionTexture), vertices.Count(), BufferUsage.WriteOnly);
            vb.SetData<VertexPositionTexture>(vertices);
        }

        private void InitializeIndices()
        {
            indices = new short[6];

            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 2;
            indices[4] = 3;
            indices[5] = 0;

            ib = new IndexBuffer(game.GraphicsDevice, typeof(short), indices.Count(), BufferUsage.WriteOnly);
            ib.SetData<short>(indices);

        }


        public void Update(GameTime gameTime)
        {
        }

        public void Draw(BasicEffect basicEffect)
        {
            game.GraphicsDevice.SetVertexBuffer(vb);
            game.GraphicsDevice.Indices = ib;
            basicEffect.Texture = this.texture;
            basicEffect.CurrentTechnique.Passes[0].Apply();
            game.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertices.Count(), 0, indices.Count() / 3);
        }

        public void Draw(Matrix world, Matrix view, Matrix projection, BasicEffect basicEffect)
        {

            Draw(basicEffect);
        }
    }
}

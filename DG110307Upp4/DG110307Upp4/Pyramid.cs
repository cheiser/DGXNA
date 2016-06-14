using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace DG110307Upp4
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Pyramid : Microsoft.Xna.Framework.GameComponent
    {
        private VertexPositionColor[] vertices;
        private short[] indices;

        private Matrix objectWorld;

        private VertexBuffer vb;
        private IndexBuffer ib;

        private Game parent;

        public Pyramid(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            objectWorld = Matrix.Identity;
            parent = game;
            InitializeVertices();
            InitializeIndices();

            
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        public void InitializeVertices()
        {
            vertices = new VertexPositionColor[4];

            // left corner
            vertices[0] = new VertexPositionColor(new Vector3(0, 0, 0), Color.White);
            // right corner
            vertices[1] = new VertexPositionColor(new Vector3(2, 0, 0), Color.Red);
            // middle corner
            vertices[2] = new VertexPositionColor(new Vector3(1, 0, -2), Color.Blue);
            // top
            vertices[3] = new VertexPositionColor(new Vector3(1, 2, -1), Color.Green);

            vb = new VertexBuffer(parent.GraphicsDevice, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
            vb.SetData<VertexPositionColor>(vertices);
        }


        private void InitializeIndices()
        {
            indices = new short[12];

            // right left 
            indices[0] = 1;
            indices[1] = 0;
            indices[2] = 3;
            
            // left middle
            indices[3] = 0;
            indices[4] = 2;
            indices[5] = 3;

            // middle right
            indices[6] = 2;
            indices[7] = 1;
            indices[8] = 3;

            // lower
            indices[9] = 0;
            indices[10] = 1;
            indices[11] = 2;/**/

            ib = new IndexBuffer(parent.GraphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);
            ib.SetData<short>(indices);
        }


        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            Matrix tempWorld = objectWorld;

            objectWorld = objectWorld * Matrix.CreateRotationY(MathHelper.PiOver4 / 50) 
            * Matrix.CreateRotationZ(MathHelper.PiOver4 / 50) * Matrix.CreateRotationX(MathHelper.PiOver4 / 50);
            objectWorld = objectWorld * Matrix.CreateTranslation(new Vector3(0, 0, 0.0001f));




            base.Update(gameTime);
        }


        public void Draw(BasicEffect effect)
        {
            parent.GraphicsDevice.SetVertexBuffer(vb);
            parent.GraphicsDevice.Indices = ib;
            effect.World = objectWorld;
            effect.CurrentTechnique.Passes[0].Apply();

            parent.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertices.Length, 0, indices.Length / 3);
        }
    }
}

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

namespace DG110117Upp4
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Plane plane;
        private Plane2 plane2;

        private BasicEffect effect;


        private Matrix world = Matrix.Identity;
        private Matrix view;
        private Matrix projection;


        private Vector3 cameraPosition = new Vector3(0, 0, 10);
        private Vector3 cameraTarget = new Vector3(0, 0, 0);
        private Vector3 cameraUp = Vector3.Up;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            InitializeCamera();

            //RasterizerState rs = new RasterizerState();
            //rs.CullMode = CullMode.None;
            //GraphicsDevice.RasterizerState = rs;



            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            effect = new BasicEffect(GraphicsDevice);
            effect.VertexColorEnabled = true;
            plane = new Plane(GraphicsDevice);
            plane.Initialize();

            plane2 = new Plane2(GraphicsDevice);
            plane2.Initialize();
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            plane.Update(gameTime);
            plane2.Update(gameTime);
            base.Update(gameTime);
        }

        private void InitializeCamera()
        {
            float aspectRatio = GraphicsDevice.DisplayMode.AspectRatio;
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio,
                0.1f, 1000f);

            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, cameraUp);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            effect.View = view;
            effect.Projection = projection;
            effect.World = Matrix.Identity;

            plane.Draw(gameTime, effect);
            plane2.Draw(gameTime, effect);
            base.Draw(gameTime);
        }
    }
}

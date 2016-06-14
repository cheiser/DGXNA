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

namespace DG110117Upp6
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        Matrix world = Matrix.Identity;
        Matrix view;
        Matrix projection;

        Camera camera;
        Road road;
        SnowPlow snowPlow;
        BasicEffect basicEffect;
        private RasterizerState rs = new RasterizerState() { CullMode = CullMode.None, FillMode = FillMode.Solid };


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
            GraphicsDevice.RasterizerState = rs;
            camera = new Camera(this);
            camera.InitializeCamera(out view, out projection);
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
            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.TextureEnabled = true;
            // TODO: use this.Content to load your game content here
            road = new Road(this);
            road.Initialize();
            snowPlow = new SnowPlow(this);
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
            road.Update(gameTime);
            snowPlow.Update(gameTime);
            float time = (float)gameTime.TotalGameTime.TotalSeconds;
            snowPlow.PlowRotation = MathHelper.Clamp((float)Math.Sin(time * 2), -MathHelper.PiOver4, MathHelper.PiOver4);
            camera.UpdateCamera(gameTime, out view);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            basicEffect.World = world;
            basicEffect.View = view;
            basicEffect.Projection = projection;

            road.Draw(basicEffect);
            snowPlow.Draw(basicEffect);

            base.Draw(gameTime);
        }
    }
}

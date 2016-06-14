using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DG110117Upp6
{
    public class Camera
    {
        Vector3 cameraPosition = new Vector3(0, 1, 2);
        Vector3 cameraTarget = new Vector3(2, 0, -1);
        Vector3 cameraUp = Vector3.Up;

        Game game;
        public Camera(Game game)
        {
            this.game = game;
        }

        public void UpdateCamera(GameTime gameTime, out Matrix view)
        {
            HandleInput();
            UpdateView(out view);
        }



        public void UpdateCamera(GameTime gameTime, out Matrix view, out Matrix projection)
        {
            HandleInput();
            InitializeCamera(out view, out projection);
        }


        private void HandleInput()
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Up))
            {
                cameraPosition.Z -= 0.1f;
            }
            if (ks.IsKeyDown(Keys.Down))
            {
                cameraPosition.Z += 0.1f;
            }
            if (ks.IsKeyDown(Keys.Left))
            {
                cameraPosition.X -= 0.1f;
            }
            if (ks.IsKeyDown(Keys.Right))
            {
                cameraPosition.X += 0.1f;
            }

            if (ks.IsKeyDown(Keys.W))
            {
                cameraPosition.Y += 0.1f;
            }
            if (ks.IsKeyDown(Keys.S))
            {
                cameraPosition.Y -= 0.1f;
            }
        }

        private void UpdateView(out Matrix view)
        {
            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, cameraUp);
        }

        public void InitializeCamera(out Matrix view, out Matrix projection)
        {
            float aspectRatio = game.GraphicsDevice.Viewport.AspectRatio;

            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.1f, 100.0f);

            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, cameraUp);
        }
    }
}

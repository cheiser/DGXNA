using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SK120112Upp16.Managers;

namespace SK120112Upp16.GameObjects
{
    public class Ufo : GameObject
    {
        private float ufoSpeed = 0.1f;
        public Ufo(Game game, SpriteBatch spriteBatch):base(game, spriteBatch)
        {
            this.destRectangle = new Rectangle(0, 0, 15, 15);
            this.direction = new Vector2(1, 0);
            this.texture = game.Content.Load<Texture2D>("Ufo");
        }

        public Ufo(Game game, SpriteBatch spriteBatch, int xCord, int yCord)
            : base(game, spriteBatch)
        {
            this.destRectangle = new Rectangle(xCord, yCord, 15, 15);
            this.direction = new Vector2(1, 3);
            this.texture = game.Content.Load<Texture2D>("Ufo");
        }

        public override void Update(GameTime gameTime)
        {
            CheckInput();
        }

        private void CheckInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                this.direction.Y -= ufoSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this.direction.X -= ufoSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                this.direction.Y += ufoSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.direction.X += ufoSpeed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                this.direction = new Vector2(0, 0);
            }
        }


        public override void CollisionWith(GameObject gameObject)
        {
            if (gameObject is FireSprite)
            {
                ((SceneManager)parent.Services.GetService(typeof(SceneManager))).Score++;
            }
        }

    }
}

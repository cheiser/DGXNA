using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SK120112Upp16.GameObjects
{
    public class FireSprite : GameObject
    {
        public FireSprite(Game game, SpriteBatch spriteBatch):base(game, spriteBatch)
        {
            this.destRectangle = new Rectangle(0, 0, 15, 15);
            this.direction = new Vector2(1, 0);
            this.texture = game.Content.Load<Texture2D>("Fire");
        }


        public FireSprite(Game game, SpriteBatch spriteBatch, int xCord, int yCord)
            : base(game, spriteBatch)
        {
            this.destRectangle = new Rectangle(xCord, yCord, 15, 15);
            this.direction = new Vector2(0.5f, 0.5f);
            this.texture = game.Content.Load<Texture2D>("Fire");
            this.Scale = 4.0f;
        }


        public override void OutSideFrame()
        {
            // scale and increase speed
            this.Scale = 0.99f;
            if (this.direction.X > 0)
            {
                this.direction.X += 0.5f;
            }
            else
            {
                this.direction.X -= 0.5f;
            }
            if (this.direction.Y > 0)
            {
                this.direction.Y += 0.5f;
            }
            else
            {
                this.direction.Y -= 0.5f;
            }
            base.OutSideFrame();
        }



    }
}

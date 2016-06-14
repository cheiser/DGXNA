using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SK120112Upp16.GameObjects
{
    public abstract class GameObject
    {
        protected Texture2D texture;
        protected Vector2 direction;
        protected float scale = 1.0f;
        protected SpriteBatch spriteBatch;
        protected Game parent;
        protected Rectangle destRectangle;

        public GameObject(Game parent, SpriteBatch spriteBatch)
        {
            this.parent = parent;
            this.spriteBatch = spriteBatch;
        }

        public GameObject(Game parent, SpriteBatch spriteBatch, Texture2D texture, Rectangle destRectangle)
        {
            this.parent = parent;
            this.spriteBatch = spriteBatch;
            this.texture = texture;
            this.destRectangle = destRectangle;
        }

        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
            }
        }

        public Game Game
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }



        public Rectangle DestinationRectangle
        {
            get
            {
                return destRectangle;
            }
            set
            {
                destRectangle = value;
            }
        }

        public Vector2 Direction
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
            }
        }

        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                destRectangle = new Rectangle(destRectangle.X, destRectangle.Y,
                    (int)(destRectangle.Width * scale), (int)(destRectangle.Height * scale));
            }
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(texture, destRectangle, Color.White);
            spriteBatch.End();
        }

        public Rectangle BoundingRectangle
        {
            get
            {
                return destRectangle;
            }
            set
            {
                destRectangle = value;
            }
        }


        public virtual void CollisionWith(GameObject gameObject)
        {
            Console.Write("Collision");
        }

        public virtual void OutSideFrame()
        {
            if (parent == null)
            {
                return;
            }
            int width = parent.GraphicsDevice.Viewport.Width;
            int height = parent.GraphicsDevice.Viewport.Height;

            Vector2 inversedDirection = Vector2.Negate(this.direction);

            //Console.Write("\nWidth Height " + width + " " + height);
            //Console.Write("\nbounding X Y " + this.BoundingRectangle.X + " " + this.BoundingRectangle.Y);

            if ((this.BoundingRectangle.X < 0))
            {
                // increase X
                this.destRectangle.X = 0 + this.BoundingRectangle.Width;// (int)direction.X+1 * 3;
                // Console.Write("\nincrease X");
                this.direction.X = inversedDirection.X;
                
            }
            else if ((this.BoundingRectangle.X + this.BoundingRectangle.Width > (width)))
            {
                // decrease X
                this.destRectangle.X = width - this.BoundingRectangle.Width-2;
                // Console.Write("\ndecrease X");
                this.direction.X = inversedDirection.X;
            }


            if ((this.BoundingRectangle.Y < 0)) // up
            {
                // decrease Y
                this.destRectangle.Y = 0 + this.BoundingRectangle.Height;
                // Console.Write("\ndecrease Y");
                this.direction.Y = inversedDirection.Y;
            }
            else if ((this.BoundingRectangle.Y + this.BoundingRectangle.Height > (height))) // down
            {
                // increase Y
                this.destRectangle.Y = height - this.BoundingRectangle.Height-2;
                // Console.Write("\nincrease Y");
                this.direction.Y = inversedDirection.Y;
            }
        }
    }
}

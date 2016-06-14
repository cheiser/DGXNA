using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SK120112Upp16.GameObjects;
using Microsoft.Xna.Framework;

namespace SK120112Upp16.Managers
{
    public class PhysicsManager
    {
        private Game parent;

        private Vector2 windDirection = new Vector2(1, 0);
        private double windChangeIntervall = 10.0;
        private double lastWindChange = 0;
        private int windStrength = 3;

        public PhysicsManager(Game parent)
        {
            this.parent = parent;
        }

        public void Update(GameTime gameTime, List<GameObject> scene)
        {
            
            if ((gameTime.TotalGameTime.Seconds - lastWindChange) > windChangeIntervall)
            {
                lastWindChange = gameTime.TotalGameTime.Seconds;
                ChangeWindDirection();
            }
            int screenWidth = parent.GraphicsDevice.Viewport.Width;
            int screenHeight = parent.GraphicsDevice.Viewport.Height;

            for (int i = 0; i < scene.Count - 1; i++)
            {
                for (int j = i + 1; j < scene.Count; j++)
                {
                    if (scene.ElementAt(i).BoundingRectangle.Intersects(scene.ElementAt(j).BoundingRectangle))
                    {
                        // Check pixelperfect here
                        if(CheckPixelPerfect(scene.ElementAt(i), scene.ElementAt(j))){
                            scene.ElementAt(i).CollisionWith(scene.ElementAt(j));
                            scene.ElementAt(j).CollisionWith(scene.ElementAt(i));
                        }
                    }
                }
                
            }
            foreach (GameObject gameObject in scene)
            {
                Rectangle destRect = gameObject.DestinationRectangle;

                if (((gameObject.BoundingRectangle.X < 0) ||
                    (gameObject.BoundingRectangle.X > screenWidth - gameObject.BoundingRectangle.Width)) ||
                    ((gameObject.BoundingRectangle.Y < 0) ||
                    (gameObject.BoundingRectangle.Y > screenHeight - gameObject.BoundingRectangle.Height)))
                {
                    gameObject.OutSideFrame();
                    destRect = gameObject.DestinationRectangle;
                }
                else
                {
                    destRect.X += ((int)gameObject.Direction.X + (int)windDirection.X);
                    destRect.Y += ((int)gameObject.Direction.Y + (int)windDirection.Y);
                }
                gameObject.DestinationRectangle = destRect;
            }
        }

        // pixel perfect collision
        private bool CheckPixelPerfect(GameObject object1, GameObject object2)
        {
            Color[] color1 = new Color[object1.Texture.Width * object1.Texture.Height];
            Color[] color2 = new Color[object2.Texture.Width * object2.Texture.Height];
            object1.Texture.GetData(color1);
            object2.Texture.GetData(color2);


            Rectangle intersection = Rectangle.Intersect(object1.BoundingRectangle, object2.BoundingRectangle);

            for (int y = intersection.Top; y < intersection.Bottom; y++)
            {
                for (int x = intersection.Left; x < intersection.Right; x++)
                {
                    Color colorA = color1[(x - object1.BoundingRectangle.Left) +
                        (y - object1.BoundingRectangle.Top) * object1.BoundingRectangle.Height];

                    Color colorB = color1[(x - object2.BoundingRectangle.Left) +
                        (y - object2.BoundingRectangle.Top) * object2.BoundingRectangle.Height];

                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        return true;
                    }
                }
            }

            return false;


        }



        private void ChangeWindDirection()
        {
            // Console.Write("changing wind direction");
            Random random = new Random();
            
            if (random.Next(1) == 1)
            {
                windDirection.X = random.Next(windStrength);
            }
            else
            {
                windDirection.X = -random.Next(windStrength);
            }

        }
    }
}

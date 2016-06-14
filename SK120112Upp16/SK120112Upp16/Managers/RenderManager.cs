using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SK120112Upp16.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SK120112Upp16.Managers
{
    public class RenderManager
    {
        private Game parent;
        public RenderManager(Game parent)
        {
            this.parent = parent;
        }

        public void Update(GameTime gameTime, List<GameObject> scene)
        {
            foreach (GameObject sceneObject in scene)
            {
                sceneObject.Draw(gameTime);
            }
        }

        public void DrawScore(int score)
        {
            SpriteBatch spriteBatch = (SpriteBatch)parent.Services.GetService(typeof(SpriteBatch));
            spriteBatch.Begin();
            SpriteFont spriteFont = parent.Content.Load<SpriteFont>("Fonts/Temp");
            spriteBatch.DrawString(spriteFont, "" + score, new Vector2(20, 20), Color.White);
            spriteBatch.End();
        }

    }
}

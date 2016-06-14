using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SK120112Upp16.GameObjects;
using Microsoft.Xna.Framework;

namespace SK120112Upp16.Managers
{
    public class SceneManager
    {
        private List<GameObject> gameObjects = new List<GameObject>();
        private Game parent;
        private int score;

        public SceneManager(Game parent)
        {
            this.parent = parent;
            PhysicsManager psM = new PhysicsManager(parent);
            parent.Services.AddService(typeof(PhysicsManager), psM);
        }

        public void Update(GameTime gameTime)
        {
            ((PhysicsManager)parent.Services.GetService(typeof(PhysicsManager))).Update(gameTime, gameObjects);
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            ((RenderManager)parent.Services.GetService(typeof(RenderManager))).Update(gameTime, gameObjects);
            ((RenderManager)parent.Services.GetService(typeof(RenderManager))).DrawScore(score);
        }

        public void AddGameObject(GameObject gameObject)
        {
            this.gameObjects.Add(gameObject);
        }

        public List<GameObject> GameObjectList
        {
            get
            {
                return gameObjects;
            }
            set
            {
                gameObjects = value;
            }
        }

        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }
    }
}

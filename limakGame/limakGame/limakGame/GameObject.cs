using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace limakGame
{

    public enum GameObjectAction {
        STAND = 0,
        WALK,
        ATTACK,
        DIE
    }

    class GameObject : DrawableGameComponent
    {

        private GameObjectAction action;
        private SpriteAnimation animation;

        private Vector2 position;

        public GameObject(Game game, Vector2 position, int width, int height, SpriteAnimation animation) : base(game)
        {
            this.position = position;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public void setAction(GameObjectAction action)
        {
            this.action = action;
        }

    }
}

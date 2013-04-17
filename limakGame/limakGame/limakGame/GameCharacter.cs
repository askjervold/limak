using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics.Dynamics;

namespace limakGame
{
    class GameCharacter : GameObject
    {
        /*public GameCharacter(GameCharacter game, World world, Vector2 position, int width, int height, SpriteAnimation animation)
            : base(game, position, width, height, animation)
        {

        }*/

        public GameCharacter(Game game, World world, Vector2 position, Vector2 size, SpriteAnimation animation) : base(game, world, position, size, animation)
        {
            // TODO
        }
    }
}

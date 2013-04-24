using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;

namespace limakGame
{
    class GamePlayer : GameCharacter
    {
        private int score;
        private PlayerIndex playerIndex;


        public GamePlayer(Game game, World world, Vector2 position, Vector2 size, SpriteAnimation animation, PlayerIndex playerIndex) 
            : base(game, world, position, size, animation)
        {
            this.playerIndex = playerIndex;
        }


        //Get Set: score
        public void setScore(int score)
        {
            this.score = score;
        }
        public int getScore(){
            return this.score;
        }

    }
}

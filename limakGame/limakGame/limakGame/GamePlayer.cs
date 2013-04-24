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
        private void setScore(int score)
        {
            this.score = score;
        }
        public int getScore()
        {
            return this.score;
        }
        public void increaseScore(int amount)
        {
            this.score = this.score + amount;
        }


        public void Revive()
        {
            this.isDead = false;
            
            // Do more stuff to make the player alive again
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;

namespace limakGame
{
    class GameEnemy : GameCharacter
    {
        private Timer timer;
        private bool changeDirection;

        public GameEnemy(Game game, World world, Vector2 position, Vector2 size, SpriteAnimation animation)
            : base(game, world, position, size, animation, BodyType.Dynamic)
        {
            changeDirection = false;
            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(OnTimeEvent);
            Random random = new Random();
            timer.Interval = random.Next(200, 1000);
            base.moveForce = new Vector2(random.Next(100, 500), 0);
            timer.Enabled = true;
        }


        //Time event
        private void OnTimeEvent(object source, ElapsedEventArgs e)
        {
            changeDirection = true;
        }

        public void Update()
        {
            if (changeDirection)
            {
                if (this.Direction == GameObjectDirection.LEFT)
                {
                    this.Direction = GameObjectDirection.RIGHT;
                }
                else
                {
                    this.Direction = GameObjectDirection.LEFT;
                }
                changeDirection = false;
            }

            this.Action = GameObjectAction.WALK;
        }

        public override void Die()
        {
            base.Die();

            // We should probably do something about the timing so that we can view the death animation before we remove the enemy

            GameEnemy that = this;

            this.Dispose();
        }

    }
}

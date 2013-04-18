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
    public class GameCharacter : GameObject
    {

        private Vector2 moveLinearImpulse = new Vector2(1.0f, -0.1f);
        private Vector2 maxLinearVelocity = new Vector2(5.0f, 5.0f);

        public GameCharacter(Game game, World world, Vector2 position, Vector2 size, SpriteAnimation animation) 
            : base(game, world, position, size, animation)
        {
            // TODO
        }

        public override void Update(GameTime gameTime)
        {
            
            // TODO: extend logic for y direction

            if(this.Action == GameObjectAction.WALK) {

                Vector2 vel = this.body.LinearVelocity;

                if (Math.Abs(this.body.LinearVelocity.X) < this.maxLinearVelocity.X)
                {
                    this.body.ApplyLinearImpulse(
                        this.moveLinearImpulse * (this.Direction == GameObjectDirection.RIGHT ? 1.0f : -1.0f)
                    );
                }
            }

            base.Update(gameTime);
        }
    }
}

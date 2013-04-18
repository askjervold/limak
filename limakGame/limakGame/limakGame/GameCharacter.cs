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

        private Vector2 moveForce = new Vector2(500.0f, 0.0f);
        private Vector2 maxLinearVelocity = new Vector2(5.0f, 999.0f);

        public GameCharacter(Game game, World world, Vector2 position, Vector2 size, SpriteAnimation animation) 
            : base(game, world, position, size, animation)
        {
            // Don't allow characters to rotate
            this.body.FixedRotation = true;
        }

        public override void Update(GameTime gameTime)
        {
            
            // TODO: extend logic for y direction

            if (this.Action == GameObjectAction.WALK)
            {

                Vector2 vel = this.body.LinearVelocity;

                if (
                    (this.Direction == GameObjectDirection.LEFT && (this.body.LinearVelocity.X > -this.maxLinearVelocity.X))
                    || (this.Direction == GameObjectDirection.RIGHT && (this.body.LinearVelocity.X < this.maxLinearVelocity.X))
                )
                {
                    this.body.ApplyForce(
                        this.moveForce * (this.Direction == GameObjectDirection.RIGHT ? 1.0f : -1.0f)
                    );
                }
            }

            base.Update(gameTime);
        }

        public void Jump()
        {
            this.body.ApplyForce(new Vector2(0.0f, -2000.0f));
        }

    }
}

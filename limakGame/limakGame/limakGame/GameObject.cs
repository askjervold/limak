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
       

    /// <summary>
    /// Enumeration of game object actions.
    /// </summary>
    public enum GameObjectAction {
        STAND = 0,
        WALK,
        ATTACK,
        DIE
    }

    /// <summary>
    /// Working direction of the object
    /// </summary>
    public enum GameObjectDirection
    {
        LEFT = 0,
        RIGHT
    }

    public class GameObject : DrawableGameComponent
    {
        
        protected GameObjectAction action;
        private SpriteAnimation animation;

        protected Vector2 size;
        protected Body body;

        protected GameObjectDirection facingDirection;

        /// <summary>
        /// Base class for interactive game objects.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="position"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="animation"></param>
        public GameObject(Game game, World world, Vector2 position, Vector2 size, SpriteAnimation animation) : base(game)
        {
            this.animation = animation;

            this.Direction = GameObjectDirection.RIGHT;

            this.size = size;

            this.body = FarseerPhysics.Factories.BodyFactory.CreateRectangle(world, size.X, size.Y, 1.0f);
            this.body.BodyType = BodyType.Dynamic;
            this.body.Friction = 2.0f;
            this.body.Restitution = 0.0f;
            this.body.Position = position;

        }

        /// <summary>
        /// Updates the game object.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {

            TimeSpan elapsedTime = gameTime.ElapsedGameTime;

            // Update the active animation
            this.animation.Update(elapsedTime);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game object to the screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            Limak game = ((Limak)this.Game);
            SpriteBatch spriteBatch = game.spriteBatch;

            spriteBatch.Begin(
                SpriteSortMode.BackToFront, 
                null, 
                SamplerState.LinearClamp, 
                DepthStencilState.Default, 
                RasterizerState.CullNone,
                null,
                game.camera.Transform
            );

            // Draw the current animation frame
            this.animation.Draw(spriteBatch, this.body.Position, this.size * game.camera.DrawScale);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Getters & setters

        public GameObjectDirection Direction
        {
            set
            {
                if(this.facingDirection != value) {
                    this.facingDirection = value;
                    this.animation.Direction = (SpriteDirection)this.facingDirection;
                }
            }
            get
            {
                return this.facingDirection;
            }
        }

        public GameObjectAction Action
        {
            set { 
                // TODO: shouldn't be allowed to change action if dying
                if(this.action != value) {
                    this.action = value;
                    // Map GameObjectAction to SpriteAction. They're identical for now...
                    this.animation.Action = (SpriteAction)value;
                }
            }
            get { return this.action; }
        }

    }
}

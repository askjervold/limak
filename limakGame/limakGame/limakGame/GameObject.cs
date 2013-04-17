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

    class GameObject : DrawableGameComponent
    {
        
        private GameObjectAction action;
        private SpriteAnimation animation;

        private Vector2 position;

        private Rectangle boundingBox;

        private Body body;

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
            this.position = position;
            this.boundingBox = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            this.animation = animation;

            this.body = FarseerPhysics.Factories.BodyFactory.CreateRectangle(world, size.X, size.Y, 1.0f);
            
            this.body.BodyType = BodyType.Dynamic;
            
            this.body.Friction = 0.2f;
            this.body.Restitution = 0.2f;

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
            // Update the bounding box of our object

            // TODO: world coordinates => viewport coordinates
            this.boundingBox.X = (int)this.body.Position.X;
            this.boundingBox.Y = (int)this.body.Position.Y;

            // Draw the current animation frame

            SpriteBatch spriteBatch = ((Game1)this.Game).spriteBatch;
            
            this.animation.Draw(spriteBatch, this.boundingBox);

            base.Draw(gameTime);
        }

        // Getters & setters

        GameObjectAction Action
        {
            set { 
                this.action = value; 
                // Map GameObjectAction to SpriteAction. They're identical for now...
                this.animation.Action = (SpriteAction)value;
            }
            get { return this.action; }
        }

    }
}

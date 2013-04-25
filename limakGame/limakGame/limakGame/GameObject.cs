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
        JUMP,
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
        protected bool isDead { get; set; }

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

            this.body = FarseerPhysics.Factories.BodyFactory.CreateRectangle(world, size.X, size.Y, 1.5f);
            this.body.BodyType = BodyType.Dynamic;
            this.body.Friction = 1.0f;
            this.body.Restitution = 0.01f;
            //this.body.LinearDamping = 3.0f;
            //this.body.AngularDamping = 3.0f;
            this.body.Inertia = 5.0f;
            this.body.Position = position;
        }

        protected override void Dispose(bool disposing)
        {
            this.body.Dispose();
            base.Dispose(disposing);
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
                game.Camera.TransformMatrix
            );

            // Draw the current animation frame

            this.animation.Draw(spriteBatch, this.body.Position, Convert.ToPixels(this.size), this.body.Rotation);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Getters & setters

        public GameObjectDirection Direction
        {
            if (this.isDead) return;

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
                if (this.isDead) return;
                // TODO: shouldn't be allowed to change action if dying
                if(this.action != value) {
                    this.action = value;
                    // Map GameObjectAction to SpriteAction. They're identical for now...
                    this.animation.Action = (SpriteAction)value;

                    if (value == GameObjectAction.DIE) this.animation.Loop = false;
                }
            }
            get { return this.action; }
        }

        public Vector2 Position
        {
            get
            {
                return this.body.Position;
            }
        }

        public Vector2 Size
        {
            get
            {
                return this.size;
            }
        }

        public Fixture getFixture()
        {
            return body.FixtureList[0];
        }
    }
}

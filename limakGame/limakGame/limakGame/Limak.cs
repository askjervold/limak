using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FarseerPhysics.Dynamics;

namespace limakGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Limak : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        private SpriteFont font;

        public World world;
        public Camera2D camera;
        public CharacterInputController characterController;

        private GameCharacter character;

        public Limak()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            // TEST 
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // Physics world
            this.world = new World(new Vector2(0.0f, 10.00f));

            // New camera
            this.camera = new Camera2D(new Vector2(0.0f, 0.0f));

            // new controlller!
            this.characterController = new CharacterInputController(0, 
                new Keys[] {
                    Keys.Left, // WALK_LEFT
                    Keys.Right, // WALK_RIGHT
                    Keys.Up, // JUMP
                    Keys.Down, // CROUCH
                    Keys.Z, // ACTION1
                    Keys.X // ACTION2
                }
            );

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            font = this.Content.Load<SpriteFont>("SpriteFont1");

            Texture2D spriteSheetTest = this.Content.Load<Texture2D>("character2SampleNotAnimated");

            SpriteAnimation animation = new SpriteAnimation(spriteSheetTest, 120, 120, 4, 4);

            character = new GameCharacter(
                this, 
                this.world, 
                new Vector2(0.0f, 0.0f), // position (meter)
                new Vector2(1.0f, 1.0f), // size (meter)
                animation
            );
            
            //character.Action = GameObjectAction.WALK;

            this.Components.Add(character);

            // Bind this as our player 1 character
            this.characterController.BindCharacter(character);

            // Add a little ground
            Body ground = FarseerPhysics.Factories.BodyFactory.CreateRectangle(world, 60.0f, 1.0f, 1.0f);
            
            ground.BodyType = BodyType.Static;
            ground.Friction = 10.0f;
            ground.Position = new Vector2(-10.0f, 5.0f);

            /*animation.AnimationDelay = 200; // 100ms between each frame
            animation.Loop = false;
            animation.Direction = SpriteDirection.RIGHT;

            animation.OnLoopEnd = delegate()
            {

                if (animation.Direction == SpriteDirection.RIGHT)
                {
                    animation.Direction = SpriteDirection.LEFT;
                }
                else
                {
                    animation.Direction = SpriteDirection.RIGHT;
                }
                
                animation.Reset();
                animation.Loop = false;
            };*/

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            this.characterController.Update();

            this.world.Step(((float)gameTime.ElapsedGameTime.Milliseconds) / 1000.0f);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();

            this.spriteBatch.DrawString(this.font, "Action: " + character.Action.ToString(), new Vector2(5.0f, 0.0f), Color.White);
            this.spriteBatch.DrawString(this.font, "Direction: " + character.Direction.ToString(), new Vector2(5.0f, 20.0f), Color.White);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

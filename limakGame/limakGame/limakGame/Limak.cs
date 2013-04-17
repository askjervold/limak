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
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

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

            base.Initialize();
        }

        Texture2D spriteSheetTest;
        SpriteAnimation animation;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spriteSheetTest = this.Content.Load<Texture2D>("character2SampleNotAnimated");

            animation = new SpriteAnimation(spriteSheetTest, 120, 120, 4, 4);
            animation.AnimationDelay = 200; // 100ms between each frame
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
            };

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

        // Defines the speed of the game (60 ticks per second)
        private long deltaTicks = TimeSpan.TicksPerSecond / 60;

        private long gameTicks = 0;
        private long previousTicks = 0;

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

            // TODO: Add your update logic here
            TimeSpan delta = gameTime.ElapsedGameTime;

            animation.Update(delta);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            //this.animation.Draw(spriteBatch, new Rectangle(0, 0, 200, 200));
            
            Matrix m = new Matrix(
                1.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f
            );

            m = Matrix.CreateScale(1.0f);// *Matrix.CreateTranslation(0.0f, 0.0f, 0.0f);

            //m.Translation.X = 

            Rectangle sourceRect = new Rectangle(0, 0, 120, 120);
            Point spriteSize = new Point(120, 120);
            Vector2 bodySize = new Vector2(1.0f, 1.0f);
            Vector2 bodyPosition = new Vector2(0.0f, 0.0f);

            spriteBatch.Begin(
                SpriteSortMode.BackToFront, 
                null, 
                SamplerState.LinearClamp, 
                DepthStencilState.Default, 
                RasterizerState.CullNone,
                null,
                m
            );
            //this.spriteSheetTest
            //spriteBatch.Begin(spriteSortMode, BlendState, SamplerState, DepthStencilState, RasterizerState, Effect, Matrix)

            spriteBatch.Draw(
                spriteSheetTest,
                bodyPosition,
                sourceRect,
                Color.White,
                0.0f, // rotation
                new Vector2(0.0f, 0.0f), // origin
                1.0f, // scale
                SpriteEffects.None,
                1.0f // layerdepth
            );

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

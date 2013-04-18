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
        
        public World world;
        public Camera2d camera;


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

            // Create new Farseer world
            this.world = new World(new Vector2(0.0f, 9.82f));

            // New camera
            this.camera = new Camera2d(new Vector2(0.0f, 0.0f));

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

            float PixelsPerMeter = 60.0f;

            // Viewport offset in pixels (x, y)
            Vector2 viewportOffset = new Vector2(0.0f, 0.0f);

            Matrix m = new Matrix(
                1.0f * PixelsPerMeter, 0.0f, 0.0f, 0.0f,
                0.0f, 1.0f * PixelsPerMeter, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f,
                viewportOffset.X, viewportOffset.Y, 0.0f, 1.0f
            );

            // Size of individual sprite frame. Given when initializing the sprite animation
            Point spriteSize = new Point(120, 120);

            // Size of body in physics engine in meter.
            Vector2 bodySize = new Vector2(1.0f, 2.0f);

            // Current position of body in the physics engine
            Vector2 bodyPosition = new Vector2(3.0f, 1.0f);

            // Source rectangle in the sprite frame, units in pixels
            Rectangle sourceRect = new Rectangle(spriteSize.X, 0, spriteSize.X, spriteSize.Y);

            // Draw scale of sprite. Passed to sprite animation when drawing
            Vector2 drawScale = new Vector2(bodySize.X / PixelsPerMeter, bodySize.Y / PixelsPerMeter);

            // GameObject should do this with matrix from viewport
            spriteBatch.Begin(
                SpriteSortMode.BackToFront, 
                null, 
                SamplerState.LinearClamp, 
                DepthStencilState.Default, 
                RasterizerState.CullNone,
                null,
                m
            );
            
            // SpriteAnimation draws the frame
            spriteBatch.Draw(
                spriteSheetTest,
                bodyPosition,
                sourceRect,
                Color.White,
                0.0f, // rotation
                new Vector2(0.0f, 0.0f), // origin
                drawScale, // scale
                SpriteEffects.None,
                1.0f // layerdepth
            );

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

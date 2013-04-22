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
        //Model and view in MVC, menus/interfaces
        GameState gameState;
        View view;

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

            spriteSheetTest = this.Content.Load<Texture2D>("character1_animated");

            animation = new SpriteAnimation(spriteSheetTest, 120, 480, 4, 6);
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

            //GUI
            gameState = new GameState(this);
            view = new View(gameState, this.Content, this.GraphicsDevice);
            view.Load();

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

            //Common form P1 & P2
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || GamePad.GetState(PlayerIndex.Two).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.R))
                gameState.BackPressed();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed || GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                gameState.StartPressed();

            // PLAYER 1
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
            {//GAMEPAD#1
                if (gameState.getCurrentGameState() == State.Playing)
                {
                    //Inside game/ State.Playing
                    //TODO: 
                }
                else
                {
                    if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                        gameState.APressed();
                    if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
                        gameState.BPressed();
                    //if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
                    //if (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed)
                    if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < 0)
                    {
                        gameState.updateButtonChangeState(State.ButtonDown);
                    }
                    else if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0)
                    {
                        gameState.updateButtonChangeState(State.ButtonUp);
                    }
                    else if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < 0)
                    {
                        gameState.LeftPressed();
                    }
                    else if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0)
                    {
                        gameState.RightPressed();
                    }
                }
            }
            else
            {//KEYBOARD
                if (gameState.getCurrentGameState() == State.Playing)
                {
                    //Inside game/ State.Playing
                    //TODO: 
                }
                else
                {
                    if ( Keyboard.GetState().IsKeyDown(Keys.N))
                        gameState.APressed();
                    if ( Keyboard.GetState().IsKeyDown(Keys.M))
                        gameState.BPressed();
                    //if ( Keyboard.GetState().IsKeyDown(Keys.J))
                    //if ( Keyboard.GetState().IsKeyDown(Keys.K))
                    if ( Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        gameState.updateButtonChangeState(State.ButtonDown);
                    }
                    else if ( Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        gameState.updateButtonChangeState(State.ButtonUp);
                    }
                    else if ( Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        gameState.LeftPressed();
                    }
                    else if ( Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        gameState.RightPressed();
                    }
                }
            }

            // PLAYER 2
            if (GamePad.GetState(PlayerIndex.Two).IsConnected)
            {//GAMEPAD#2
                if (gameState.getCurrentGameState() == State.Playing)
                {
                    //Inside game/ State.Playing
                    //TODO: 
                }
                else
                {
                    if (GamePad.GetState(PlayerIndex.Two).Buttons.A == ButtonState.Pressed)
                        gameState.APressed2();
                    if (GamePad.GetState(PlayerIndex.Two).Buttons.B == ButtonState.Pressed)
                        gameState.BPressed();
                    //if (GamePad.GetState(PlayerIndex.Two).Buttons.X == ButtonState.Pressed)
                    //if (GamePad.GetState(PlayerIndex.Two).Buttons.Y == ButtonState.Pressed)
                    if (GamePad.GetState(PlayerIndex.Two).Buttons.Start == ButtonState.Pressed)
                        gameState.StartPressed();

                    if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y < 0)
                    {
                        gameState.updateButtonChangeState(State.ButtonDown);
                    }
                    else if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.Y > 0)
                    {
                        gameState.updateButtonChangeState(State.ButtonUp);
                    }
                    else if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X < 0)
                    {
                        gameState.LeftPressed2();
                    }
                    else if (GamePad.GetState(PlayerIndex.Two).ThumbSticks.Left.X > 0)
                    {
                        gameState.RightPressed2();
                    }
                }
            }
            else if (GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                if (gameState.getCurrentGameState() == State.Playing)
                {
                    //Inside game/ State.Playing
                    //TODO: 
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.N))
                        gameState.APressed2();
                    if (Keyboard.GetState().IsKeyDown(Keys.M))
                        gameState.BPressed();
                    //if (Keyboard.GetState().IsKeyDown(Keys.J))
                    //if (Keyboard.GetState().IsKeyDown(Keys.K))
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        gameState.updateButtonChangeState(State.ButtonDown);
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        gameState.updateButtonChangeState(State.ButtonUp);
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    {
                        gameState.LeftPressed2();
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        gameState.RightPressed2();
                    }
                }
            }
            else
            {//KEYBOARD
                if (gameState.getCurrentGameState() == State.Playing)
                {
                    //Inside game/ State.Playing
                    //TODO: 
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Tab))
                        gameState.APressed2();
                    if (Keyboard.GetState().IsKeyDown(Keys.Q))
                        gameState.BPressed();
                    //if (Keyboard.GetState().IsKeyDown(Keys.D1))  
                    //if (Keyboard.GetState().IsKeyDown(Keys.D2))
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        gameState.StartPressed();

                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        gameState.updateButtonChangeState(State.ButtonDown);
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        gameState.updateButtonChangeState(State.ButtonUp);
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        gameState.LeftPressed2();
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        gameState.RightPressed2();
                    }
                }
            }


            if (gameState.getCurrentGameState() != State.Pause)
            {   
                //Update logic while State.Playing
                 // TODO: Add your update logic here
                TimeSpan delta = gameTime.ElapsedGameTime;

                animation.Update(delta);

                base.Update(gameTime);
            }

           
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            switch (gameState.getCurrentGameState())
            {
                case State.Playing:
                    //All drawing while gameState = State.Playing
                    this.animation.Draw(spriteBatch, new Rectangle(0, 0, 120, 480));
                    
                    break;
                default:
                    //All other drawing (menu etc.)
                    spriteBatch.Begin();
                    view.Draw(spriteBatch);
                    spriteBatch.End();
                    break;
            }

            base.Draw(gameTime);
        }
    }
}
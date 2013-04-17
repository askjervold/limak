using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace limakGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TestLevelReader : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _batch;
        private KeyboardState _oldKeyState;
        private GamePadState _oldPadState;
        private SpriteFont _font;

        private World _world;

        private Body _goo;
        private Body _groundBody;

        private Texture2D _circleSprite;
        private Texture2D _groundSprite;

        // Simple camera controls
        private Matrix _view;
        private Vector2 _cameraPosition;
        private Vector2 _screenCenter;


        private LogicLevelReader _llr;

        private string testText; 
#if !XBOX360
        const string Text = "Press A or D to rotate the ball\n" +
                            "Press Space to jump\n" +
                            "Press Shift + W/S/A/D to move the camera";
#else
                const string Text = "Use left stick to move\n" +
                                    "Use right stick to move camera\n" +
                                    "Press A to jump\n";
#endif
        // Farseer expects objects to be scaled to MKS (meters, kilos, seconds)
        // 1 meters equals 64 pixels here
        // (Objects should be scaled to be between 0.1 and 10 meters in size)
        private const float MeterInPixels = 60f;

        public TestLevelReader()
        {
             
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 480;

            Content.RootDirectory = "Content";

            _world = new World(new Vector2(0, 0f));
            _llr = new LogicLevelReader(_world);
            _llr.readFile("level");
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Initialize camera controls
            _view = Matrix.Identity;
            _cameraPosition = Vector2.Zero;

            _screenCenter = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2f,
                                                _graphics.GraphicsDevice.Viewport.Height / 2f);

            _batch = new SpriteBatch(_graphics.GraphicsDevice);
            _font = Content.Load<SpriteFont>("text");

            // Load sprites
            _circleSprite = Content.Load<Texture2D>("goo"); //  96px x 96px => 1.5m x 1.5m
            _groundSprite = Content.Load<Texture2D>("groundBlock"); // 512px x 64px =>   8m x 1m

            /* Circle */
            // Convert screen center from pixels to meters
            Vector2 circlePosition = (_screenCenter / MeterInPixels) + new Vector2(0, -1.5f);

            // Create the circle fixture
            _goo = BodyFactory.CreateCircle(_world, 96f / (2f * MeterInPixels), 1f, circlePosition);
            _goo.BodyType = BodyType.Dynamic;

            // Give it some bounce and friction
            _goo.Restitution = 0.3f;
            _goo.Friction = 0.5f;

            

            /* Ground */
            Vector2 groundPosition = (_screenCenter / MeterInPixels) + new Vector2(0, 1.25f);

            _llr.createGround(groundPosition);
            // Get the ground.
            _groundBody = _llr.Ground;

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
//            HandleGamePad();
            HandleKeyboard();

            //We update the world
            _world.Step((float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f);

            base.Update(gameTime);
        }

        private void HandleGamePad()
        {
            
            /*GamePadState padState = GamePad.GetState(0);

            if (padState.IsConnected)
            {
                if (padState.Buttons.Back == ButtonState.Pressed)
                    Exit();

                if (padState.Buttons.A == ButtonState.Pressed && _oldPadState.Buttons.A == ButtonState.Released)
                    _goo.ApplyLinearImpulse(new Vector2(0, -10));

                _goo.ApplyForce(padState.ThumbSticks.Left);
                _cameraPosition.X -= padState.ThumbSticks.Right.X;
                _cameraPosition.Y += padState.ThumbSticks.Right.Y;

                _view = Matrix.CreateTranslation(new Vector3(_cameraPosition - _screenCenter, 0f)) * Matrix.CreateTranslation(new Vector3(_screenCenter, 0f));

                _oldPadState = padState;
            }*/
        }

        private void HandleKeyboard()
        {
            KeyboardState state = Keyboard.GetState();

            // Switch between circle body and camera control
            {
                // Move camera
                if (state.IsKeyDown(Keys.Left))
                    _cameraPosition.X += 1.5f;

                if (state.IsKeyDown(Keys.Right))
                    _cameraPosition.X -= 1.5f;

                if (state.IsKeyDown(Keys.Up))
                    _cameraPosition.Y += 1.5f;

                if (state.IsKeyDown(Keys.Down))
                    _cameraPosition.Y -= 1.5f;

                _view = Matrix.CreateTranslation(new Vector3(_cameraPosition - _screenCenter, 0f)) *
                        Matrix.CreateTranslation(new Vector3(_screenCenter, 0f));
            }
            {
                // We make it possible to rotate the circle body
                if (state.IsKeyDown(Keys.A) && _oldKeyState.IsKeyUp(Keys.A))
                    _goo.ApplyLinearImpulse(new Vector2(-10, 0)); ;
                    //_circleBody.ApplyTorque(-10);

                if (state.IsKeyDown(Keys.D) && _oldKeyState.IsKeyUp(Keys.D))
                    _goo.ApplyLinearImpulse(new Vector2(10, 0));

                if (state.IsKeyDown(Keys.W) && _oldKeyState.IsKeyUp(Keys.W))
                    _goo.ApplyLinearImpulse(new Vector2(0, -10));
                if (state.IsKeyDown(Keys.S)&& _oldKeyState.IsKeyUp(Keys.S))
                    _goo.ApplyLinearImpulse(new Vector2(0, 10));
            }

            if (state.IsKeyDown(Keys.Escape))
                Exit();

            _oldKeyState = state;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //rectangle to contain the tile of the ground
            Rectangle toDraw = new Rectangle(0, 0, 4020, 60);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            /* Circle position and rotation */
            // Convert physics position (meters) to screen coordinates (pixels)
            Vector2 circlePos = _goo.Position * MeterInPixels;
            float circleRotation = _goo.Rotation;

            /* Ground position and origin */
            Vector2 groundPos = _groundBody.Position * MeterInPixels;
            Vector2 groundOrigin = new Vector2(_groundSprite.Width / 2f, _groundSprite.Height / 2f);
            // Align sprite center to body position
            Vector2 circleOrigin = new Vector2(_circleSprite.Width / 2f, _circleSprite.Height / 2f);

            //Normal batch
            //_batch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _view);

            //tiled batch
            _batch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, _view);

            //Draw circle
            _batch.Draw(_circleSprite, circlePos, null, Color.White, circleRotation, circleOrigin, 1f, SpriteEffects.None, 0f);
            testText = "X: " + circlePos.X + " Y: " + circlePos.Y;
            //Normal draw
            //_batch.Draw(_groundSprite, groundPos, null, Color.White, 0f, groundOrigin, 1f, SpriteEffects.None, 0f);
            testText += "    GroundX: " + groundPos.X + " Y: " + groundPos.Y;

            //Tiled draw (sprite, position, rectangle, color, rotation, origin, scale, spriteeffects, layer depth);
            _batch.Draw(_groundSprite, groundPos, toDraw, Color.White, 0, groundOrigin, 1, SpriteEffects.None, 0);


            _batch.End();

            _batch.Begin();

            // Display instructions
            _batch.DrawString(_font, testText, new Vector2(14f, 14f), Color.Black);
            _batch.DrawString(_font, testText, new Vector2(12f, 12f), Color.White);

            _batch.End();

            base.Draw(gameTime);
        }
    }
}
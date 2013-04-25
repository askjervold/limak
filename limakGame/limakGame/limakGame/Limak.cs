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
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Common;


namespace limakGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Limak : Microsoft.Xna.Framework.Game
    {
        private CameraMan m_CameraMan;
        
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        //Model and view in MVC, menus/interfaces
        GameState gameState;
        View view;
        
        private SpriteFont font;

        private bool loaded;
        public World world;
        public CharacterInputController characterController, character1Controller, character2Controller;
        private MenuInputController menuInputController, menuInputController2;

        Map map;

        private GameCharacter character;
        private GamePlayer player1, player2;
        Texture2D player1T, player2T;
        SpriteAnimation player1Animation, player2Animation;

        Texture2D blackTexture;
        Texture2D background;

        private ScrollingBackground myBackground;

        public Limak()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 820;
            graphics.PreferredBackBufferHeight = 460;
            // TEST 
        }

        public Camera Camera
        {
            get { return m_CameraMan.Camera; }
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

            this.character1Controller = new CharacterInputController(PlayerIndex.One,
                new Keys[] {
                    Keys.Left, // WALK_LEFT
                    Keys.Right, // WALK_RIGHT
                    Keys.Up, // JUMP
                    Keys.Down, // CROUCH
                    Keys.N, // ACTION1
                    Keys.M // ACTION2
                }
            );
            this.character2Controller = new CharacterInputController(PlayerIndex.Two,
               new Keys[] {
                    Keys.A, // WALK_LEFT
                    Keys.D, // WALK_RIGHT
                    Keys.W, // JUMP
                    Keys.S, // CROUCH
                    Keys.Tab, // ACTION1
                    Keys.Q // ACTION2
                }
           );

            // Create all black texture
            blackTexture = new Texture2D(GraphicsDevice, 1, 1);
            blackTexture.SetData(new Color[] { Color.Black });

            //loaded: true if game has been loaded, used by StateChanged():gameLoading()
            this.loaded = false;

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
            myBackground = new ScrollingBackground();


            //GUI
            gameState = new GameState(this);
             //Adds this as listener to menu model: gameState
            gameState.StateChanged += new ChangedEventHandler( StateChanged );

            view = new View(gameState, this.Content, this.GraphicsDevice);
            view.Load();

            // TODO: use this.Content to load your game content here

            this.menuInputController = new MenuInputController(PlayerIndex.One,
                new Keys[] {
                    Keys.Left,
                    Keys.Right, 
                    Keys.Up,
                    Keys.Down, 
                    Keys.N, 
                    Keys.M,
                    Keys.Escape
                }, gameState);
            this.menuInputController2 = new MenuInputController(PlayerIndex.Two,
                new Keys[] {
                    Keys.A,
                    Keys.D, 
                    Keys.W,
                    Keys.S, 
                    Keys.Tab, 
                    Keys.Q,
                    Keys.Escape
                }, gameState);
        }
        
        //This will be called whenever gameState changes
        private void StateChanged(object sender, State state)
        {
            if (state == State.Playing )
            {
                if (!loaded)
                {
                    this.GameLoading();
                    loaded = true;
                }
                else
                {
                    //Show all objects
                    foreach (GameComponent component in this.Components)
                    {
                        if (component is DrawableGameComponent)
                            ((DrawableGameComponent)component).Visible = true;
                    }
                }

            }
            else if (state == State.Pause)
            {
                //Hide all objects
                foreach ( GameComponent component in this.Components)
                {
                    if(component is DrawableGameComponent)
                        ((DrawableGameComponent)component).Visible = false;
                }
                
            }
            else if (state == State.GameOver)
            {
    //TODO: Restart game
            }
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

            //Update menu input when inside menus
            if (gameState.getCurrentGameState() != State.Playing)
            {
                this.menuInputController.Update();
                this.menuInputController2.Update();

            }
            else
            {
                //Update logic while State.Playing


               // TimeSpan delta = gameTime.ElapsedGameTime;

                //animation.Update(delta);

                this.characterController.Update();
                this.character1Controller.Update();
                this.character2Controller.Update();

                this.world.Step(((float)gameTime.ElapsedGameTime.Milliseconds) / 1000.0f);

                myBackground.Update( player1.Position.X );


                if (player1.Position.Y > map.levelHeight) player1.Die();
                if (player2.Position.Y > map.levelHeight) player2.Die();
                if (player1.isDead && player2.isDead) gameState.updateState(State.GameOver);

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

                    


                    //this.animation.Draw(spriteBatch, new Rectangle(0, 0, 120, 480));
                     GraphicsDevice.Clear(Color.CornflowerBlue);
                    map.Draw(gameTime);
                    this.spriteBatch.Begin();
                    //this.spriteBatch.DrawString(this.font, "Action: " + character.Action.ToString(), new Vector2(5.0f, 0.0f), Color.White);
                    //this.spriteBatch.DrawString(this.font, "Direction: " + character.Direction.ToString(), new Vector2(5.0f, 20.0f), Color.White);

                    // Draw background
                    //spriteBatch.Draw(background, new Rectangle(0, 0, 800, 600), Color.White);
                    myBackground.Draw(spriteBatch);
                    // Draw ground
                    /*spriteBatch.Draw(blackTexture, new Rectangle(0, 5 * 60, 800, 60), Color.Black);*/

                    var score1 = "Player 1: " + player1.getScore().ToString();
                    var score2 = "Player 2: " + player2.getScore().ToString();

                    var pos1 = Vector2.Zero;
                    var pos2 = new Vector2(GraphicsDevice.Viewport.Width - font.MeasureString(score2).X, 0);

                    spriteBatch.DrawString(font, score1, pos1, Color.White);
                    spriteBatch.DrawString(font, score2, pos2, Color.White);
            
                    this.spriteBatch.End();
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

        /// <summary>
        /// Add a new game object to the map
        /// </summary>
        /// <param name="gameObject"></param>
        public void addGameObject(GameObject gameObject)
        {
            this.Components.Add(gameObject);
        }


        // This is called whenever gameState = Playing (when you enter the actually game)
        public void GameLoading()
        {
            // Setup the map
             this.map = new Map(this, "level.txt");

            this.Components.Add(this.map);
            

            // Setup misc graphics
            background = this.Content.Load<Texture2D>("bgtest");
            font = this.Content.Load<SpriteFont>("SpriteFont1");

        ////
            ////Texture2D spriteSheetTest = this.Content.Load<Texture2D>("character2SampleNotAnimated");
            //Texture2D bunny = this.Content.Load<Texture2D>("test2");

            //// Setup the game character
            //SpriteAnimation bunnyAnimation = new SpriteAnimation(bunny, 128, 128, 7, 7);
            //bunnyAnimation.AnimationDelay = 100;

            //character = new GameCharacter(
            //    this,
            //    this.world,
            //    new Vector2(0.0f, 0.0f), // position (meter)
            //    new Vector2(2.0f, 2.0f), // size (meter)
            //    bunnyAnimation
            //);

            //camera.AddCharacter(character);

            //this.Components.Add(character);

            //// Bind this as our player 1 character
            //this.characterController.BindCharacter(character);
        ////

            //Background
            Texture2D level1 = this.Content.Load<Texture2D>("bg_level");
            myBackground.Load( this.GraphicsDevice, level1);

            //Assign characters to players
            switch ( gameState.getCurrentCharacter1() )
            {
                case State.SralState:
                    player1T = this.Content.Load<Texture2D>("spriteSheetCharacter1");
                    break;
                case State.LimakState:
                    player1T = this.Content.Load<Texture2D>("spriteSheetCharacter2");
                    break;
                case State.NuduaState:
                    player1T = this.Content.Load<Texture2D>("spriteSheetCharacter3");
                    break;
                case State.DHState:
                    player1T = this.Content.Load<Texture2D>("spriteSheetCharacter4");
                    break;
                case State.BokajState:
                    player1T = this.Content.Load<Texture2D>("spriteSheetCharacter5");
                    break;
                default:
                    break;
            }
            switch (gameState.getCurrentCharacter2())
            {
                case State.SralState:
                    player2T = this.Content.Load<Texture2D>("spriteSheetCharacter1");
                    break;
                case State.LimakState:
                    player2T = this.Content.Load<Texture2D>("spriteSheetCharacter2");
                    break;
                case State.NuduaState:
                    player2T = this.Content.Load<Texture2D>("spriteSheetCharacter3");
                    break;
                case State.DHState:
                    player2T = this.Content.Load<Texture2D>("spriteSheetCharacter4");
                    break;
                case State.BokajState:
                    player2T = this.Content.Load<Texture2D>("spriteSheetCharacter5");
                    break;
                default:
                    break;
            }

            //Set players animation
            player1Animation = new SpriteAnimation(player1T, 120, 120, 4, 6);
            player1Animation.AnimationDelay = 200;
            player2Animation = new SpriteAnimation(player2T, 120, 120, 4, 6);
            player2Animation.AnimationDelay = 200;

            //Create players
            player1 = new GamePlayer(
                this,
                this.world,
                new Vector2(0.0f, 0.0f), // position (meter)
                new Vector2(2.0f, 2.0f), // size (meter)
                player1Animation,
                PlayerIndex.One
            );
            player2 = new GamePlayer(
                this,
                this.world,
                new Vector2(1.0f, 0.0f), // position (meter)
                new Vector2(2.0f, 2.0f), // size (meter)
                player2Animation,
                PlayerIndex.Two
            ); 

            //Add camera man
            m_CameraMan = new CameraMan(this, new Camera(), player1);
            this.Components.Add(m_CameraMan);

            //Add players
            this.Components.Add(player1);
            this.Components.Add(player2);

            // Bind players to characterInput
            this.character1Controller.BindCharacter(player1);
            this.character2Controller.BindCharacter(player2);

            //Collision detection
            player1.getFixture().OnCollision += player1.CollisionWithEnemy;
            player1.getFixture().OnCollision += player1.PickUpCoin;
            player1.getFixture().OnCollision += player1.PlayerPlayerCollision;
            player1.getFixture().OnCollision += player1.CollisionWithGround;
            player1.getFixture().OnCollision += player1.PlayerFinish;
            player2.getFixture().OnCollision += player2.CollisionWithEnemy;
            player2.getFixture().OnCollision += player2.PickUpCoin;
            player2.getFixture().OnCollision += player2.PlayerPlayerCollision;
            player2.getFixture().OnCollision += player2.CollisionWithGround;
            player2.getFixture().OnCollision += player2.PlayerFinish;

            

            // Add a little ground
            /*Body ground = FarseerPhysics.Factories.BodyFactory.CreateRectangle(world, 60.0f, 1.0f, 1.0f);

            ground.BodyType = BodyType.Static;
            ground.Friction = 10.0f;
            ground.Position = new Vector2(-10.0f, 8.0f);
            */
        }


        public void GameUnloading()
        {


        }

    }
}
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
        private DateTime timeBegin;

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

        private List<GamePlayer> players;
        public List<GamePlayer> getPlayers() { return players; }

        private GameCharacter character;
        private GamePlayer player1, player2;
        Texture2D player1T, player2T;
        SpriteAnimation player1Animation, player2Animation;

        Texture2D blackTexture;
        Texture2D background;

        private String endTime;
        private ScrollingBackground myBackground;

        public Limak()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 820;
            graphics.PreferredBackBufferHeight = 460;
        }

        public Camera Camera
        {
            get { return m_CameraMan.Camera; }
        }

        public CameraMan CameraMan
        {
            get { return m_CameraMan; }
            set
            {
                Components.Remove(m_CameraMan);
                m_CameraMan = value;
                Components.Add(m_CameraMan);
            }
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
            else if (state == State.Win)
            {
                endTime = DateTime.Now.Subtract(this.timeBegin).ToString();
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

                //Update enemies
                GameEnemy enemy;
                foreach (IGameComponent comp in this.Components)
                {
                    enemy = comp as GameEnemy;
                    if (enemy != null)
                    {
                        enemy.Update();
                    }
                }


                this.world.Step(((float)gameTime.ElapsedGameTime.Milliseconds) / 1000.0f);

                myBackground.Update( player1.Position.X );


                if (player1.Position.Y > map.levelHeight)
                {
                    player1.Die();
                }
                if (player2.Position.Y > map.levelHeight)
                {
                    player2.Die();
                }
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

                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    map.Draw(gameTime);
                    this.spriteBatch.Begin();
                   
                    myBackground.Draw(spriteBatch);

                    var score1 = "Player 1: " + player1.getScore().ToString();
                    var score2 = "Player 2: " + player2.getScore().ToString();

                    var pos1 = Vector2.Zero;
                    var pos2 = new Vector2(GraphicsDevice.Viewport.Width - font.MeasureString(score2).X, 0);

                    spriteBatch.DrawString(font, score1, pos1, Color.White);
                    spriteBatch.DrawString(font, score2, pos2, Color.White);
            
                    this.spriteBatch.End();
                    break;
                case State.Win:
                    var s1 = player1.getScore().ToString();
                    var s2 = player2.getScore().ToString();
                    this.spriteBatch.Begin();
                    spriteBatch.Draw(this.Content.Load<Texture2D>("winMenu"), new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
                    spriteBatch.DrawString(font, s1, new Vector2(200, 120), Color.White);
                    spriteBatch.DrawString(font, s2, new Vector2(500, 120), Color.White);

                    spriteBatch.DrawString(font, "Time: "+endTime, new Vector2(200, 200), Color.White);
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
            timeBegin = DateTime.Now;

            // Setup the map
            this.map = new Map(this, "level.txt");

            this.Components.Add(this.map);

            // Setup misc graphics
            background = this.Content.Load<Texture2D>("bgtest");
            font = this.Content.Load<SpriteFont>("SpriteFont1");

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
            players = new List<GamePlayer>();
            
            player1 = new GamePlayer(
                this,
                this.world,
                new Vector2(2.0f, 0.0f), // position (meter)
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
            m_CameraMan = new DoubleTrackingCameraMan(this, new Camera(), player1, player2);
            this.Components.Add(m_CameraMan);

            //Add players
            this.Components.Add(player1);
            this.Components.Add(player2);
            this.players.Add(player1);
            this.players.Add(player2);

            // Bind players to characterInput
            this.character1Controller.BindCharacter(player1);
            this.character2Controller.BindCharacter(player2);

            //Collision detection
            foreach (GamePlayer player in this.players) {
                player.getFixture().OnCollision += player.CollisionWithEnemy;
                player.getFixture().OnCollision += player.PickUpCoin;
                player.getFixture().OnCollision += player.PlayerPlayerCollision;
                player.getFixture().OnCollision += player.CollisionWithGround;
                player.getFixture().OnCollision += player.PlayerFinish;
            }

        }


        public void GameUnloading()
        {

            this.Components.Clear();
            this.world.Clear();
            loaded = false;
            

        }

        public void updateState(State state)
        {
            gameState.updateState(state);
        }

    }
}
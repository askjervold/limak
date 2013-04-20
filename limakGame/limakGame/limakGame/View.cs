using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace limakGame
{
    /*
     * View in MVC
     * 
     * */
    class View
    {
        private State currentState;
        private GameState model;
        private Button b_play, b_options, b_exit, b_exitPause, b_continue, playerMenuDone, player2MenuDone,
                       b_cLimak, b_cSral, b_cBokaj, b_cNudua, b_cDh, b_cLimak2, b_cSral2, b_cBokaj2, b_cNudua2, b_cDh2,
                       b_FrameLimak, b_FrameSral, b_FrameBokaj, b_FrameNudua, b_FrameDh,
                       b_FrameLimak2, b_FrameSral2, b_FrameBokaj2, b_FrameNudua2, b_FrameDh2;
        private ContentManager content;
        private GraphicsDevice graphics;
        private Vector2 posVec1, posVec2, posVec3, posVec4, sizeVec1, sizeVec2;

        public View(GameState model, ContentManager content, GraphicsDevice graphics)
        {
            this.model = model;
            this.content = content;
            this.graphics = graphics;
            this.model.StateChanged += new ChangedEventHandler( StateChanged );

            posVec4 = new Vector2(graphics.Viewport.Width - 600, graphics.Viewport.Height - 350);
            posVec3 = new Vector2(graphics.Viewport.Width - 300, graphics.Viewport.Height - 350);
            posVec2 = new Vector2(graphics.Viewport.Width - 575, graphics.Viewport.Height - 300);
            posVec1 = new Vector2(graphics.Viewport.Width - 275, graphics.Viewport.Height - 300);

            sizeVec2 = new Vector2(200, 40);
            sizeVec1 = new Vector2(150, 150);
        }


        //This will be called whenever state changes
        private void StateChanged( object sender, State state )
        {
            switch (state)
            {
                case State.ButtonUp:
                    if (currentState == State.MainMenu)
                    {
                        b_play.setIsChoosen(true);
                        b_options.setIsChoosen(false);
                        b_exit.setIsChoosen(false);
                    }
                    else if (currentState == State.Pause)
                    {
                        b_continue.setIsChoosen(true);
                        b_exitPause.setIsChoosen(false);
                    }
                    break;

                case State.ButtonMiddle:
                    if (currentState == State.MainMenu)
                    {
                        b_play.setIsChoosen(false);
                        b_options.setIsChoosen(true);
                        b_exit.setIsChoosen(false);
                    }
                    break;

                case State.ButtonDown:
                    if (currentState == State.MainMenu)
                    {
                        b_play.setIsChoosen(false);
                        b_options.setIsChoosen(false);
                        b_exit.setIsChoosen(true);
                    }
                    else if (currentState == State.Pause)
                    {
                        b_continue.setIsChoosen(false);
                        b_exitPause.setIsChoosen(true);
                    }
                    break;
                default:
                    currentState = state;
                    break;
                }
        }

        public void Load()
        {
            b_play = new Button(content.Load<Texture2D>("button_play_on"), content.Load<Texture2D>("button_play_off"), graphics, new Vector2(100, 40));
            b_play.setPosition(new Vector2( 150, 200 ));
            b_play.setIsChoosen(true);

            b_options = new Button(content.Load<Texture2D>("button_options_on"), content.Load<Texture2D>("button_options_off"), graphics, new Vector2(150, 40));
            b_options.setPosition(new Vector2(150, 250));

            b_exit = new Button(content.Load<Texture2D>("button_exit_on"), content.Load<Texture2D>("button_exit_off"), graphics, new Vector2(100, 40));
            b_exit.setPosition(new Vector2(150, 300));

            b_continue = new Button(content.Load<Texture2D>("button_continue_on"), content.Load<Texture2D>("button_continue_off"), graphics, sizeVec2);
            b_continue.setPosition(new Vector2(200, 250));
            b_continue.setIsChoosen(true);
            
            b_exitPause = new Button(content.Load<Texture2D>("button_exit_on"), content.Load<Texture2D>("button_exit_off"), graphics, new Vector2(100, 40));
            b_exitPause.setPosition(new Vector2(200, 300));

            playerMenuDone = new Button(content.Load<Texture2D>("playerMenuDoneTexture"), content.Load<Texture2D>("playerMenuDoneTexture"), graphics, new Vector2(graphics.Viewport.Width/3, graphics.Viewport.Height/3));
            playerMenuDone.setPosition(new Vector2(graphics.Viewport.Width / 4, graphics.Viewport.Height / 8));

            player2MenuDone = new Button(content.Load<Texture2D>("playerMenuDoneTexture"), content.Load<Texture2D>("playerMenuDoneTexture"), graphics, new Vector2(graphics.Viewport.Width / 3, graphics.Viewport.Height / 3));
            player2MenuDone.setPosition(new Vector2(graphics.Viewport.Width -320, graphics.Viewport.Height / 8));

          ////
            b_cLimak = new Button(content.Load<Texture2D>("cLimak_on"), content.Load<Texture2D>("cLimak"), graphics, sizeVec2);
            b_cLimak.setPosition(posVec4);

            b_cSral = new Button(content.Load<Texture2D>("cSral_on"), content.Load<Texture2D>("cSral"), graphics, sizeVec2);
            b_cSral.setPosition(posVec4);

            b_cBokaj = new Button(content.Load<Texture2D>("cBokaj_on"), content.Load<Texture2D>("cBokaj"), graphics, sizeVec2);
            b_cBokaj.setPosition(posVec4);

            b_cNudua = new Button(content.Load<Texture2D>("cNudua_on"), content.Load<Texture2D>("cNudua"), graphics, sizeVec2);
            b_cNudua.setPosition(posVec4);

            b_cDh = new Button(content.Load<Texture2D>("cDh_on"), content.Load<Texture2D>("cDh"), graphics, sizeVec2);
            b_cDh.setPosition(posVec4);

          ////
            b_cLimak2 = new Button(content.Load<Texture2D>("cLimak_on"), content.Load<Texture2D>("cLimak"), graphics, sizeVec2);
            b_cLimak2.setPosition(posVec3);

            b_cSral2 = new Button(content.Load<Texture2D>("cSral_on"), content.Load<Texture2D>("cSral"), graphics, sizeVec2);
            b_cSral2.setPosition(posVec3);

            b_cBokaj2 = new Button(content.Load<Texture2D>("cBokaj_on"), content.Load<Texture2D>("cBokaj"), graphics, sizeVec2);
            b_cBokaj2.setPosition(posVec3);

            b_cNudua2 = new Button(content.Load<Texture2D>("cNudua_on"), content.Load<Texture2D>("cNudua"), graphics, sizeVec2);
            b_cNudua2.setPosition(posVec3);

            b_cDh2 = new Button(content.Load<Texture2D>("cDh_on"), content.Load<Texture2D>("cDh"), graphics, sizeVec2);
            b_cDh2.setPosition(posVec3);

           
    //TODO: change texture2d when pictures added
            b_FrameLimak = new Button(content.Load<Texture2D>("characterFrameLimak"), content.Load<Texture2D>("characterFrameLimak"), graphics, sizeVec1);
            b_FrameLimak.setPosition(posVec2);

            b_FrameSral = new Button(content.Load<Texture2D>("characterFrameSral"), content.Load<Texture2D>("characterFrameSral"), graphics, sizeVec1);
            b_FrameSral.setPosition(posVec2);

            b_FrameBokaj = new Button(content.Load<Texture2D>("characterFrameSral"), content.Load<Texture2D>("characterFrameSral"), graphics, sizeVec1);
            b_FrameBokaj.setPosition(posVec2);

            b_FrameNudua = new Button(content.Load<Texture2D>("characterFrameSral"), content.Load<Texture2D>("characterFrameSral"), graphics, sizeVec1);
            b_FrameNudua.setPosition(posVec2);

            b_FrameDh = new Button(content.Load<Texture2D>("characterFrameSral"), content.Load<Texture2D>("characterFrameSral"), graphics, sizeVec1);
            b_FrameDh.setPosition(posVec2);

          ////
            b_FrameLimak2 = new Button(content.Load<Texture2D>("characterFrameLimak"), content.Load<Texture2D>("characterFrameLimak"), graphics, sizeVec1);
            b_FrameLimak2.setPosition(posVec1);

            b_FrameSral2 = new Button(content.Load<Texture2D>("characterFrameSral"), content.Load<Texture2D>("characterFrameSral"), graphics, sizeVec1);
            b_FrameSral2.setPosition(posVec1);

            b_FrameBokaj2 = new Button(content.Load<Texture2D>("characterFrameSral"), content.Load<Texture2D>("characterFrameSral"), graphics, sizeVec1);
            b_FrameBokaj2.setPosition(posVec1);

            b_FrameNudua2 = new Button(content.Load<Texture2D>("characterFrameSral"), content.Load<Texture2D>("characterFrameSral"), graphics, sizeVec1);
            b_FrameNudua2.setPosition(posVec1);

            b_FrameDh2 = new Button(content.Load<Texture2D>("characterFrameSral"), content.Load<Texture2D>("characterFrameSral"), graphics, sizeVec1);
            b_FrameDh2.setPosition(posVec1);

        }

        public void Draw( SpriteBatch spriteBatch)
        {
            switch (currentState)
            {
                case State.MainMenu:
                    spriteBatch.Draw( content.Load<Texture2D>("mainMenu"), new Rectangle(0, 0, graphics.Viewport.Width, graphics.Viewport.Height), Color.White );
                    b_play.Draw( spriteBatch );
                    b_options.Draw( spriteBatch );
                    b_exit.Draw( spriteBatch );
                    break;

                case State.Options:
                    spriteBatch.Draw(content.Load<Texture2D>("options"), new Rectangle(0, 0, graphics.Viewport.Width, graphics.Viewport.Height), Color.White);
                    break;

                case State.Pause:
                    spriteBatch.Draw(content.Load<Texture2D>("pause"), new Rectangle(0, 0, graphics.Viewport.Width, graphics.Viewport.Height), Color.White);
                    b_continue.Draw(spriteBatch);
                    b_exitPause.Draw(spriteBatch);
                    break;

                case State.PlayerMenu:
                    spriteBatch.Draw(content.Load<Texture2D>("playerMenu"), new Rectangle(0, 0, graphics.Viewport.Width, graphics.Viewport.Height), Color.White);
                    if( model.getPlayer1MenuDone() )
                        playerMenuDone.Draw( spriteBatch );
                    if( model.getPlayer2MenuDone() )
                        player2MenuDone.Draw( spriteBatch );

                    b_cLimak.setIsChoosen(false);b_cSral.setIsChoosen(false);b_cBokaj.setIsChoosen(false);b_cNudua.setIsChoosen(false);b_cDh.setIsChoosen(false);
                    b_cLimak2.setIsChoosen(false);b_cSral2.setIsChoosen(false);b_cBokaj2.setIsChoosen(false);b_cNudua2.setIsChoosen(false);b_cDh2.setIsChoosen(false);
                    break;

                case State.CharacterMenu:
                    spriteBatch.Draw(content.Load<Texture2D>("characterMenu"), new Rectangle(0, 0, graphics.Viewport.Width, graphics.Viewport.Height), Color.White);
                    if (model.getPlayer1CharacterDone())
                    {
                        b_cLimak.setIsChoosen(true);
                        b_cSral.setIsChoosen(true);
                        b_cBokaj.setIsChoosen(true);
                        b_cNudua.setIsChoosen(true);
                        b_cDh.setIsChoosen(true);
                    }
                    if (model.getPlayer2CharacterDone())
                    {
                        b_cLimak2.setIsChoosen(true);
                        b_cSral2.setIsChoosen(true);
                        b_cBokaj2.setIsChoosen(true);
                        b_cNudua2.setIsChoosen(true);
                        b_cDh2.setIsChoosen(true);
                    }
                    break;

                case State.GameOver:
                    spriteBatch.Draw(content.Load<Texture2D>("bg_gameOver"), new Rectangle(0, 0, graphics.Viewport.Width, graphics.Viewport.Height), Color.White);
                    b_continue.setIsChoosen(true);
                    b_exitPause.setIsChoosen(false);
                    break;
            }

            if (currentState == State.CharacterMenu)
            {
                switch ( model.getCurrentCharacter1() )
                {
                    case State.LimakState:
                        b_cLimak.Draw(spriteBatch);
                        b_FrameLimak.Draw(spriteBatch);
                        break;
                    case State.SralState:
                        b_cSral.Draw(spriteBatch);
                        b_FrameSral.Draw(spriteBatch);
                        break;
                    case State.BokajState:
                        b_cBokaj.Draw(spriteBatch);
                        b_FrameBokaj.Draw(spriteBatch);
                        break;
                    case State.NuduaState:
                        b_cNudua.Draw(spriteBatch);
                        b_FrameNudua.Draw(spriteBatch);
                        break;
                    case State.DHState:
                        b_cDh.Draw(spriteBatch);
                        b_FrameDh.Draw(spriteBatch);
                        break;

                    default:
                        break;
                }

                switch ( model.getCurrentCharacter2() )
                {
                    case State.LimakState:
                        b_cLimak2.Draw(spriteBatch);
                        b_FrameLimak2.Draw(spriteBatch);
                        break;
                    case State.SralState:
                        b_cSral2.Draw(spriteBatch);
                        b_FrameSral2.Draw(spriteBatch);
                        break;
                    case State.BokajState:
                        b_cBokaj2.Draw(spriteBatch);
                        b_FrameBokaj2.Draw(spriteBatch);
                        break;
                    case State.NuduaState:
                        b_cNudua2.Draw(spriteBatch);
                        b_FrameNudua2.Draw(spriteBatch);
                        break;
                    case State.DHState:
                        b_cDh2.Draw(spriteBatch);
                        b_FrameDh2.Draw(spriteBatch);
                        break;


                    default:
                        break;
                }
            }
        }

    }
}

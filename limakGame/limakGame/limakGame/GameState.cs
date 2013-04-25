using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace limakGame
{
    public enum State { MainMenu, CharacterMenu, PlayerMenu, Playing, Options, Pause, GameOver, 
                        ButtonUp, ButtonMiddle, ButtonDown,
                        LimakState, SralState, BokajState, NuduaState, DHState };
    public delegate void ChangedEventHandler(object sender, State state);

    /*
     * Model in MVC
     * It holds game states and updates View whenever a state is updated
     * 
     * */
    public class GameState
    {
        public event ChangedEventHandler StateChanged;
        private State currentGameState, currentCharacter1Chosen, currentCharacter2Chosen;
        private Limak limak;
        private Timer timer;
        private bool canReloadAfterElapsedTime, player1MenuDone, player2MenuDone, player1CharacterDone, player2CharacterDone;

        /*
         * MainMenu             PlayerMenu          Pause
         *  0: b_play           player1             Continue
         *  1: b_options               
         *  2: b_exit           player2             Exit
         *
         * */
        public int currentButtonChosen;
 

        public GameState(Limak limak)
        {
            currentGameState = State.MainMenu;
            currentButtonChosen = 0;
            currentCharacter1Chosen = State.LimakState;
            currentCharacter2Chosen = State.SralState; 
            this.limak = limak;
            player1MenuDone = false;
            player2MenuDone = false;
            player1CharacterDone = false;
            player2CharacterDone = false;
            
            timer = new System.Timers.Timer();

            timer.Elapsed += new ElapsedEventHandler(OnTimeEvent);
            timer.Interval = 250;
            timer.Enabled = true;
        }

        //Used by timer. Every time ElapsedTime=0
        public void OnTimeEvent(object source, ElapsedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Elapsed time ..");
            canReloadAfterElapsedTime = true;
        }

        //Used by event
        protected virtual void OnChanged( State state )
        {
            if (StateChanged != null)
            {
                StateChanged(this, state);
            }
        }

        /**
         * Methods below manages keyboard & gamepad-buttons and change to appropriate gameState
         * */
        //PLAYER 1
        public void APressed()
        {
            if (canReloadAfterElapsedTime)
            {
                switch (currentGameState)
                {
                    case State.MainMenu:
                        if (currentButtonChosen == 0)
                        {
                            updateState(State.PlayerMenu);
                        }
                        else if (currentButtonChosen == 1)
                        {
                            updateState(State.Options);
                        }
                        else
                        {
                            timer.Dispose();
                            limak.Exit();
                        }
                        break;
                    case State.PlayerMenu:
                        updateButtonChangeState(State.ButtonUp);
                        player1MenuDone = true;
                        break;
                    case State.CharacterMenu:
                        updateButtonChangeState(State.ButtonMiddle);
                        player1CharacterDone = true;

                        break;
                    case State.Pause:
                        if (currentButtonChosen == 0)
                        {
                            updateState(State.Playing);
                        }
                        else if (currentButtonChosen == 2)
                        {
                            updateState(State.GameOver);
                        }
                        break;
                    case State.GameOver:
                        updateState(State.MainMenu);
                        currentButtonChosen = 0;
                        player1MenuDone = false;
                        player2MenuDone = false;
                        player1CharacterDone = false;
                        player2CharacterDone = false;
                        break;
                    default:
                        break;
                }
                canReloadAfterElapsedTime = false;
            }
        }
        public void LeftPressed()
        {
            if (canReloadAfterElapsedTime && !player1CharacterDone)
            {
                switch (currentGameState)
                {

                    case State.CharacterMenu:
                        if (currentCharacter1Chosen == State.SralState)
                        {
                            //Change 1 left / choose limak
                            currentCharacter1Chosen = State.LimakState;
                            System.Diagnostics.Debug.WriteLine("P1: LIMAK_STATE");
                        }
                        else if (currentCharacter1Chosen == State.BokajState)
                        {
                            //Change 1 left / choose sral
                            currentCharacter1Chosen = State.SralState;
                        }
                        else if (currentCharacter1Chosen == State.NuduaState)
                        {
                            //Change 1 left / choose bokaj
                            currentCharacter1Chosen = State.BokajState;
                        }
                        else if (currentCharacter1Chosen == State.DHState)
                        {
                            //Change 1 left / choose nudua
                            currentCharacter1Chosen = State.NuduaState;
                        }
                        else if (currentCharacter1Chosen == State.LimakState)
                        {
                            //Change 1 left / choose DH
                            currentCharacter1Chosen = State.DHState;
                        }

                        break;
                    default:
                        break;
                }
                canReloadAfterElapsedTime = false;
            }
        }
        public void RightPressed()
        {
            if (canReloadAfterElapsedTime && !player1CharacterDone)
            {
                switch (currentGameState)
                {
                    case State.CharacterMenu:
                        if (currentCharacter1Chosen == State.LimakState)
                        {
                            //Change 1 right / choose sral
                            currentCharacter1Chosen = State.SralState;
                            System.Diagnostics.Debug.WriteLine("P1: SRAL_STATE");
                        }
                        else if (currentCharacter1Chosen == State.SralState)
                        {
                            //Change 1 right / choose Bokaj
                            currentCharacter1Chosen = State.BokajState;
                        }
                        else if (currentCharacter1Chosen == State.BokajState)
                        {
                            //Change 1 right / choose Nudua
                            currentCharacter1Chosen = State.NuduaState;
                        }
                        else if (currentCharacter1Chosen == State.NuduaState)
                        {
                            //Change 1 right / choose DH
                            currentCharacter1Chosen = State.DHState;
                        }
                        else if (currentCharacter1Chosen == State.DHState)
                        {
                            //Change 1 right / choose limak
                            currentCharacter1Chosen = State.LimakState;
                        }


                        break;
                    default:
                        break;
                }
                canReloadAfterElapsedTime = false;
            }
        }

        //PLAYER 2
        public void APressed2()
        {
            if (canReloadAfterElapsedTime)
            {
                switch (currentGameState)
                {
                    case State.MainMenu:
                        if (currentButtonChosen == 0)
                        {
                            updateState(State.PlayerMenu);
                        }
                        else if (currentButtonChosen == 1)
                        {
                            updateState(State.Options);
                        }
                        else
                        {
                            timer.Dispose();
                            limak.Exit();
                        }
                        break;
                    case State.PlayerMenu:
                        updateButtonChangeState(State.ButtonUp);
                        player2MenuDone = true;
                        break;
                    case State.CharacterMenu:
                        //TODO.. have to wain on player 2
                        //updateState(State.Playing);
                        updateButtonChangeState(State.ButtonMiddle);
                        player2CharacterDone = true;
                        break;
                    case State.Pause:
                        if (currentButtonChosen == 0)
                        {
                            updateState(State.Playing);
                        }
                        else if (currentButtonChosen == 2)
                        {
                            updateState(State.GameOver);
                        }
                        break;
                    case State.GameOver:
                        updateState(State.MainMenu);
                        currentButtonChosen = 0;
                        player1MenuDone = false;
                        player2MenuDone = false;
                        player1CharacterDone = false;
                        player2CharacterDone = false;
                        Console.WriteLine("PLAYER2 dead");
                        break;
                    default:
                        break;
                }
                canReloadAfterElapsedTime = false;
            }
        }
        public void LeftPressed2()
        {
            if (canReloadAfterElapsedTime && !player2CharacterDone)
            {
                switch (currentGameState)
                {

                    case State.CharacterMenu:
                        if (currentCharacter2Chosen == State.SralState)
                        {
                            //Change 1 left / choose limak
                            currentCharacter2Chosen = State.LimakState;
                            System.Diagnostics.Debug.WriteLine("P1: LIMAK_STATE");
                        }
                        else if (currentCharacter2Chosen == State.BokajState)
                        {
                            //Change 1 left / choose sral
                            currentCharacter2Chosen = State.SralState;
                        }
                        else if (currentCharacter2Chosen == State.NuduaState)
                        {
                            //Change 1 left / choose bokaj
                            currentCharacter2Chosen = State.BokajState;
                        }
                        else if (currentCharacter2Chosen == State.DHState)
                        {
                            //Change 1 left / choose nudua
                            currentCharacter2Chosen = State.NuduaState;
                        }
                        else if (currentCharacter2Chosen == State.LimakState)
                        {
                            //Change 1 left / choose DH
                            currentCharacter2Chosen = State.DHState;
                        }
                        
                        break;
                    default:
                        break;
                }
                canReloadAfterElapsedTime = false;
            }
        }
        public void RightPressed2()
        {
            if (canReloadAfterElapsedTime && !player2CharacterDone)
            {
                switch (currentGameState)
                {
                    case State.CharacterMenu:
                        if (currentCharacter2Chosen == State.LimakState)
                        {
                            //Change 1 right / choose sral
                            currentCharacter2Chosen = State.SralState;
                            System.Diagnostics.Debug.WriteLine("P1: SRAL_STATE");
                        }
                        else if (currentCharacter2Chosen == State.SralState)
                        {
                            //Change 1 right / choose Bokaj
                            currentCharacter2Chosen = State.BokajState;
                        }
                        else if (currentCharacter2Chosen == State.BokajState)
                        {
                            //Change 1 right / choose Nudua
                            currentCharacter2Chosen = State.NuduaState;
                        }
                        else if (currentCharacter2Chosen == State.NuduaState)
                        {
                            //Change 1 right / choose DH
                            currentCharacter2Chosen = State.DHState;
                        }
                        else if (currentCharacter2Chosen == State.DHState)
                        {
                            //Change 1 right / choose limak
                            currentCharacter2Chosen = State.LimakState;
                        }

                        
                        break;
                    default:
                        break;
                }
                canReloadAfterElapsedTime = false;
            }
        }

        //common
        public void BPressed()
        {
            if (canReloadAfterElapsedTime)
            {
                switch (currentGameState)
                {
                    case State.Options:
                        updateState(State.MainMenu);
                        break;
                    case State.Pause:
                        updateState(State.Playing);
                        break;
                    case State.PlayerMenu:
                        updateButtonChangeState(State.ButtonMiddle);
                        updateState(State.MainMenu);
                        player1MenuDone = false;
                        player2MenuDone = false;
                        currentButtonChosen = 0;
                        break;
                    case State.CharacterMenu:
                        updateState(State.PlayerMenu);
                        player1MenuDone = false;
                        player2MenuDone = false;
                        player1CharacterDone = false;
                        player2CharacterDone = false;
                        break;
                    case State.GameOver:
                        updateState(State.MainMenu);
                        currentButtonChosen = 0;
                        player1MenuDone = false;
                        player2MenuDone = false;
                        player1CharacterDone = false;
                        player2CharacterDone = false;
                        break;
                    default:
                        break;
                }
                canReloadAfterElapsedTime = false;
            }
        }
        public void StartPressed()
        {
            if (canReloadAfterElapsedTime)
            {
                switch (currentGameState)
                {
                    case State.Playing:
                        currentButtonChosen = 0;
                        updateState(State.Pause);
                        break;
                    //case State.Pause:
                    //    updateState(State.Playing);
                    //    //currentButtonChosen = 0;
                    //    break;
                }
                canReloadAfterElapsedTime = false;
            }
        }
        public void BackPressed()
        {
            //TODO: REMOVE
            updateState(State.MainMenu);
            timer.Start();
            player1MenuDone = false;
            player2MenuDone = false;
            player1CharacterDone = false;
            player2CharacterDone = false;
            currentButtonChosen = 0;
        }



        //Used by event
        public void updateState( State state )
        {
            currentGameState = state;
            OnChanged( currentGameState );
        }

        //Used by event (when menu button changes: up/down)
        public void updateButtonChangeState(State state)
        {
            if (currentGameState == State.MainMenu && canReloadAfterElapsedTime == true)
            {
                switch (currentButtonChosen)
                {
                    case 0:
                        if( state == State.ButtonDown )
                        {
                            currentButtonChosen = 1;
                            OnChanged(State.ButtonMiddle);
                            
                        }
                        break;
                    case 1:
                        if( state == State.ButtonUp )
                        {
                            currentButtonChosen = 0;
                            OnChanged(state);
                        }
                        else if (state == State.ButtonDown )
                        {
                            currentButtonChosen = 2;
                            OnChanged(state);
                        }
                        break;
                    case 2:
                        if( state == State.ButtonUp )
                        {
                            currentButtonChosen = 1;
                            OnChanged(State.ButtonMiddle);
                        }
                        break;
                }
                canReloadAfterElapsedTime = false;
            }
            else if (currentGameState == State.PlayerMenu && canReloadAfterElapsedTime == true)
            {
                if (player1MenuDone && player2MenuDone)
                {
                   updateState(State.CharacterMenu);
                }
                else
                {
                    OnChanged(state);
                }
                canReloadAfterElapsedTime = false;
            }
            else if (currentGameState == State.CharacterMenu && canReloadAfterElapsedTime == true)
            {
                if (player1CharacterDone && player2CharacterDone)
                {
                    updateState(State.Playing);
                }
                else
                {
                    OnChanged(state);
                }
                canReloadAfterElapsedTime = false;
            }
            else if (currentGameState == State.Pause && canReloadAfterElapsedTime == true)
            {
                if (state == State.ButtonDown)
                {
                    currentButtonChosen = 2;
                    OnChanged(State.ButtonDown);
                }
                else
                {
                    currentButtonChosen = 0;
                    OnChanged(State.ButtonUp);
                }
                canReloadAfterElapsedTime = false;
            }
            
        }

        //Returns current game state
        public State getCurrentGameState()
        {
            return currentGameState;
        }

        //Returns current character1
        public State getCurrentCharacter1()
        {
            return currentCharacter1Chosen;
        }
        //Returns current character2
        public State getCurrentCharacter2()
        {
            return currentCharacter2Chosen;
        }

        //Returns true if player 1 has pushed A/N
        public bool getPlayer1MenuDone()
        {
            return player1MenuDone;
        }
        //Returns true if player 2 has pushed A/Q
        public bool getPlayer2MenuDone()
        {
            return player2MenuDone;
        }

        //Returns true if player 1 has chosen character
        public bool getPlayer1CharacterDone()
        {
            return player1CharacterDone;
        }
        //Returns true if player 2 has chosen character
        public bool getPlayer2CharacterDone()
        {
            return player2CharacterDone;
        }
        
    }
}

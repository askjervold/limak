using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace limakGame
{
    class MenuInputController : InputController
    {
        private GameState gameState;
        private PlayerIndex playerIndex;

        public MenuInputController(PlayerIndex playerIndex, Keys[] keyMapping, GameState gameState) 
            : base(playerIndex, keyMapping)
        {
            this.playerIndex = playerIndex;
            this.gameState = gameState;
        }


        protected override void onInputChange(InputAction inputAction, bool state)
        {
                if (state && playerIndex == PlayerIndex.One)
                {
                    switch (inputAction)
                    {
                        case InputAction.A:
                            gameState.APressed();
                            break;
                        case InputAction.B:
                            gameState.BPressed();
                            break;
                        case InputAction.DOWN:
                            gameState.updateButtonChangeState(State.ButtonDown);
                            break;
                        case InputAction.UP:
                            gameState.updateButtonChangeState(State.ButtonUp);
                            break;
                        case InputAction.LEFT:
                            gameState.LeftPressed();
                            break;
                        case InputAction.RIGHT:
                            gameState.RightPressed();
                            break;

                        default:
                            break;
                    }
                }
                else if (state && playerIndex == PlayerIndex.Two)
                {
                    switch (inputAction)
                    {
                        case InputAction.A:
                            gameState.APressed2();
                            break;
                        case InputAction.B:
                            gameState.BPressed();
                            break;
                        case InputAction.DOWN:
                            gameState.updateButtonChangeState(State.ButtonDown);
                            break;
                        case InputAction.UP:
                            gameState.updateButtonChangeState(State.ButtonUp);
                            break;
                        case InputAction.LEFT:
                            gameState.LeftPressed2();
                            break;
                        case InputAction.RIGHT:
                            gameState.RightPressed2();
                            break;

                        default:
                            break;
                    }
                }
            }


        protected override void onInputChangeForPad()
        {
            if (playerIndex == PlayerIndex.One)
            {
                if (GamePad.GetState(playerIndex).Buttons.A == ButtonState.Pressed)
                    gameState.APressed();
                if (GamePad.GetState(playerIndex).Buttons.B == ButtonState.Pressed)
                    gameState.BPressed();
                if (GamePad.GetState(playerIndex).ThumbSticks.Left.Y < 0)
                {
                    gameState.updateButtonChangeState(State.ButtonDown);
                }
                else if (GamePad.GetState(playerIndex).ThumbSticks.Left.Y > 0)
                {
                    gameState.updateButtonChangeState(State.ButtonUp);
                }
                else if (GamePad.GetState(playerIndex).ThumbSticks.Left.X < 0)
                {
                    gameState.LeftPressed();
                }
                else if (GamePad.GetState(playerIndex).ThumbSticks.Left.X > 0)
                {
                    gameState.RightPressed();
                }
            }
            else if (playerIndex == PlayerIndex.Two)
            {
                if (GamePad.GetState(playerIndex).Buttons.A == ButtonState.Pressed)
                    gameState.APressed2();
                if (GamePad.GetState(playerIndex).Buttons.B == ButtonState.Pressed)
                    gameState.BPressed();
                if (GamePad.GetState(playerIndex).ThumbSticks.Left.Y < 0)
                {
                    gameState.updateButtonChangeState(State.ButtonDown);
                }
                else if (GamePad.GetState(playerIndex).ThumbSticks.Left.Y > 0)
                {
                    gameState.updateButtonChangeState(State.ButtonUp);
                }
                else if (GamePad.GetState(playerIndex).ThumbSticks.Left.X < 0)
                {
                    gameState.LeftPressed2();
                }
                else if (GamePad.GetState(playerIndex).ThumbSticks.Left.X > 0)
                {
                    gameState.RightPressed2();
                }
            }
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace limakGame
{

    public enum CharacterInputAction
    {
        WALK_LEFT = 0,
        WALK_RIGHT,
        JUMP,
        CROUCH,
        ACTION1,
        ACTION2
    }

    public class CharacterInputController : InputController
    {
        protected GameCharacter character = null;
        private PlayerIndex playerIndex;

        public CharacterInputController(PlayerIndex playerIndex, Keys[] keyMapping)
            : base(playerIndex, keyMapping)
        {
            // Unused as of yet
            this.playerIndex = playerIndex;
        }

        protected override void onInputChange(InputAction inputAction, bool state)
        {
            if(this.character == null) {
                // No use in doing anything!
                return;
            }
            CharacterInputAction action = (CharacterInputAction)inputAction;

            switch(action) 
            {
                case CharacterInputAction.WALK_LEFT:
                case CharacterInputAction.WALK_RIGHT:

                    CharacterInputAction mirrorAction;
                    GameObjectDirection direction;
                    GameObjectDirection mirrorDirection;
                
                    if(action == CharacterInputAction.WALK_LEFT)
                    {
                        direction = GameObjectDirection.LEFT;
                        mirrorDirection = GameObjectDirection.RIGHT;
                        mirrorAction = CharacterInputAction.WALK_RIGHT;
                    } else 
                    {
                        direction = GameObjectDirection.RIGHT;
                        mirrorDirection = GameObjectDirection.LEFT;
                        mirrorAction = CharacterInputAction.WALK_LEFT;
                    }
                    
                    if (state)
                    {
                        // Walk key was pressed, start walking in that direction
                        this.character.Direction = direction;
                        this.character.Action = GameObjectAction.WALK;
                    }
                    else
                    {
                        // Walk key unpressed
                        if(!this.keyStates[(int)mirrorAction]) {
                            // The other walk key is not pressed either, we can stop walking
                            this.character.Action = GameObjectAction.STAND;
                        }
                        else
                        {
                            // The other walk key is down, we need to continue walking but change direction
                            this.character.Direction = mirrorDirection;
                        }
                    }
                    break;
                
                case CharacterInputAction.JUMP:
                    if(state) {
                        this.character.Jump();
                    }
                    break;

                default:
                    break;
            }

        }


        protected override void onInputChangeForPad()
        {
            if(this.character == null) {
                // No use in doing anything!
                return;
            }
            bool state = true;

            if (playerIndex == PlayerIndex.One)
            {

                if (GamePad.GetState(playerIndex).Buttons.A == ButtonState.Pressed)
                    if(state) {
                        this.character.Jump();
                    }
                else if (GamePad.GetState(playerIndex).ThumbSticks.Left.X < 0)
                {

                }
                else if (GamePad.GetState(playerIndex).ThumbSticks.Left.X > 0)
                {
                }
            }
            else if (playerIndex == PlayerIndex.Two)
            {
                if (GamePad.GetState(playerIndex).Buttons.A == ButtonState.Pressed)
                    return;
                else if (GamePad.GetState(playerIndex).ThumbSticks.Left.X < 0)
                {
                }
                else if (GamePad.GetState(playerIndex).ThumbSticks.Left.X > 0)
                {
                }
            }
        }

        /// <summary>
        /// Bind this controller to a GameCharacter
        /// </summary>
        /// <param name="character"></param>
        public void BindCharacter(GameCharacter character)
        {
            this.character = character;
        }

    }
}

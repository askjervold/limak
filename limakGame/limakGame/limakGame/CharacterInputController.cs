using System;
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

        public CharacterInputController(PlayerIndex playerIndex, Keys[] keyMapping)
            : base(playerIndex, keyMapping)
        {

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
                
                default:
                    break;
            }

        }

        public void BindCharacter(GameCharacter character)
        {
            this.character = character;
        }

    }
}

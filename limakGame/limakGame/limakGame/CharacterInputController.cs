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
                    if (state)
                    {
                        this.character.Direction = GameObjectDirection.LEFT;
                        this.character.Action = GameObjectAction.WALK;
                    }
                    else
                    {
                        this.character.Action = GameObjectAction.STAND;
                    }
                    break;
                case CharacterInputAction.WALK_RIGHT:
                    if (state)
                    {
                        this.character.Direction = GameObjectDirection.RIGHT;
                        this.character.Action = GameObjectAction.WALK;
                    }
                    else
                    {
                        this.character.Action = GameObjectAction.STAND;
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

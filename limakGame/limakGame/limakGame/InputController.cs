using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace limakGame
{
    /// <summary>
    /// General input actions
    /// </summary>
    public enum InputAction {
        LEFT = 0,
        RIGHT,
        UP,
        DOWN,
        A,
        B
    }

    public abstract class InputController
    {
        private PlayerIndex playerIndex;
        
        protected Keys[] keyMapping;
        protected bool[] keyStates;

        /// <summary>
        /// Creates a new controller which can be bound to a to a controllable interface
        /// </summary>
        /// <param name="playerIndex">Player index</param>
        /// <param name="keyMapping">Key mapping</param>
        public InputController(PlayerIndex playerIndex, Keys[] keyMapping)
        {
            this.playerIndex = playerIndex;
            this.keyMapping = keyMapping;
            
            this.keyStates = new bool[keyMapping.Length];
            this.resetKeyStates();
        }

        /// <summary>
        /// Resets the key states to false
        /// </summary>
        protected void resetKeyStates() 
        {
            for (int i = 0; i < keyStates.Length; i++ )
            {
                this.keyStates[i] = false;
            }
        }

        /// <summary>
        /// Detects changes in input
        /// </summary>
        public void Update()
        {
            
            for(int i = 0; i < this.keyMapping.Length; i++) {
                
                bool keyState = Keyboard.GetState(this.playerIndex).IsKeyDown(this.keyMapping[i]);

                if (keyState != this.keyStates[i])
                {
                    this.onInputChange((InputAction)i, keyState);
                    this.keyStates[i] = keyState;
                }
                
            }
            
        }

        /// <summary>
        /// Override this function to add functionality!
        /// </summary>
        /// <param name="inputAction"></param>
        /// <param name="state"></param>
        protected abstract void onInputChange(InputAction inputAction, bool state);
    }
}

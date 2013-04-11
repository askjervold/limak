using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace limakGame
{

    public enum SpriteDirection {
        LEFT = 0,
        RIGHT
    }

    public enum SpriteAction {
        STAND = 0,
        WALK,
        ATTACK,
        DIE
    }

    class SpriteAnimation
    {
        private Texture2D sprite;
        private int width;
        private int height;
        private int nFrames;
        private int nActions;

        private SpriteAction action = SpriteAction.STAND;

        public SpriteAction Action
        {
            get { return this.action }
            set { this.action = value }
        }

        private SpriteDirection direction = SpriteDirection.LEFT;

        private int animationSpeeed = 60; // Milliseconds between each frame change

        private bool loop = true;
        private bool stopped = false;

        private int frameProgress = 0;

        /// <summary>
        /// Creates a new animation from a sprite sheet. Animations contain all the frames used in a 2D sprite animation. Each row defines
        /// a new action for the animation. Each row consists of a number of frames.
        /// </summary>
        /// <param name="spriteSheet">Sheet of sprites.</param>
        /// <param name="frameWidth">Width of each sprite frame of the sprite sheet. All sprite frames should use the same width.</param>
        /// <param name="frameHeight">Height of each row in the sprite sheet. All sprite frames should use the same height.</param>
        /// <param name="numRows">Number of rows in the sprite sheet. Each row defines a separate action for the animation. Numbering starts from 0.</param>
        /// <param name="numFrames">Number of frames per action animation.</param>
        public SpriteAnimation(Texture2D spriteSheet, int frameWidth, int frameHeight, int numRows, int numFrames)
        {
            this.sprite = spriteSheet;
            this.width = frameWidth;
            this.height = frameHeight;
            this.nFrames = numFrames;
            this.nActions = numRows;
        }

        public void draw(SpriteBatch spriteBatch)
        {

        }

        public void update()
        {
            this.frameProgress += time.g
        }

        /// <summary>
        /// Resets the animation. Used internally on action change.
        /// </summary>
        private void reset()
        {

        }



        public void setDirection(SpriteDirection direction) 
        {
            this.direction = direction;
        }

        public void setAction(int action) 
        {
            this.action = action;
        }

    }
}

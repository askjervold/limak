using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace limakGame
{
    /// <summary>
    /// Enumeration of the facing directions of the animation
    /// </summary>
    public enum SpriteDirection {
        LEFT = 0,
        RIGHT
    }

    /// <summary>
    /// Represents the names of the various animation action (may or may not be applicable to the current sprite sheet.)
    /// </summary>
    public enum SpriteAction {
        STAND = 0,
        WALK,
        ATTACK,
        DIE
    }

    /// <summary>
    /// Delegate for all sprite animation events.
    /// </summary>
    public delegate void OnSpriteAnimationEvent();
    
    class SpriteAnimation
    {
        private Texture2D sprite;
        private int width;
        private int height;
        private int nFrames;
        private int nActions;

        private SpriteAction action = SpriteAction.STAND;
        private SpriteEffects spriteEffect;
        private SpriteDirection direction;
        
        private Color color = Color.White;

        private int animationDelay = 60; // Milliseconds between each frame change
        
        private bool loop;
        private bool stopped;
        private int frame;
        private int timeElapsed;

        private Rectangle sourceRect;
        private Vector2 origin = new Vector2(0.0f, 0.0f);

        private OnSpriteAnimationEvent onLoopEnd = null;
        
        /// <summary>
        /// Update the source rectangle
        /// </summary>
        private void updateSourceRect() 
        {
            this.sourceRect.X = this.frame * this.width;
            this.sourceRect.Y = (int)(this.action) * this.height;
        }

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
            
            this.sourceRect = new Rectangle(0, 0, this.width, this.height);

            this.Direction = SpriteDirection.RIGHT;
            
            // Set the animation controls to initial state
            this.Reset();

        }

        /// <summary>
        /// Draws the active frame for the sprite animation.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="destinationRect">Rectangle in screen coordinates in which the animation should be drawn.</param>
        public void Draw(SpriteBatch spriteBatch, Rectangle destinationRect)
        {

            spriteBatch.Begin();
            
            spriteBatch.Draw(
                this.sprite, 
                destinationRect, 
                this.sourceRect, 
                this.color,
                0,
                this.origin, 
                this.spriteEffect,
                0.0f
            );

            spriteBatch.End();
        }

        /// <summary>
        /// Updates the sprite animation.
        /// </summary>
        /// <param name="time">Time span since last update.</param>
        public void Update(TimeSpan time)
        {

            if(this.stopped) {
                return;
            }

            this.timeElapsed += time.Milliseconds;

            if (this.timeElapsed >= this.animationDelay)
            {
                if (this.frame + 1 >= this.nFrames && !this.loop)
                {
                    // Stop animation if not looping
                    this.stopped = true;
                    
                    // Call event
                    if(this.onLoopEnd != null) {
                        this.onLoopEnd();
                    }

                }
                else
                {
                    // Update active frame
                    this.frame = (this.frame + 1) % this.nFrames;
                    this.updateSourceRect();

                }
                this.timeElapsed -= this.animationDelay;
            }
        }
        
        /// <summary>
        /// Resets the animation to default state.
        /// </summary>
        public void Reset()
        {
            this.loop = true;
            this.stopped = false;
            this.timeElapsed = 0;
            this.frame = 0;
            this.updateSourceRect();
        }

        // Getters and setters

        /// <summary>
        /// Display action of the animation
        /// </summary>
        public SpriteAction Action
        {
            get { return this.action; }
            set
            {
                this.action = value;
                this.Reset();
            }
        }

        /// <summary>
        /// Facing direction of the animation
        /// </summary>
        public SpriteDirection Direction
        {
            get { return this.direction; }
            set
            {
                this.direction = value;
                if (this.direction == SpriteDirection.LEFT)
                {
                    this.spriteEffect = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    this.spriteEffect = SpriteEffects.None;
                }
            }
        }

        /// <summary>
        /// Animation delay given in microseconds.
        /// </summary>
        public int AnimationDelay
        {
            get { return this.animationDelay; }
            set { this.animationDelay = value; }
        }

        /// <summary>
        /// Stop the animation from running.
        /// </summary>
        public bool Stopped
        {
            get;
            set;
        }

        /// <summary>
        /// Should animation loop?
        /// </summary>
        public bool Loop
        {
            get { return this.loop; }
            set { this.loop = value; }
        }

        /// <summary>
        /// End event for animation when it's done playing the current sub-animation.
        /// Is only called if animation is not looping!
        /// </summary>
        public OnSpriteAnimationEvent OnLoopEnd
        {
            set { this.onLoopEnd = value; }
        }

        
    }
}

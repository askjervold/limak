using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace limakGame
{
    interface Camera
    {
        /// <summary>
        /// Converts pixel resolution into meter for Farseer
        /// </summary>
        /// <param name="width">Width in pixels</param>
        /// <param name="height">Height in pixels</param>
        /// <returns>Size of object in meters</returns>
        Vector2 pixelsToMeter(int width, int height);

        /// <summary>
        /// Checks whether the game object is visible on the screen
        /// </summary>
        /// <param name="gameObject">Game object</param>
        /// <returns>True if the object is in the current viewport</returns>
        bool isVisible(GameObject gameObject);
    }

    public class Camera2d
    {
        private Vector2 m_Position;

        public Camera2d(Vector2 position)
        {
            m_Position = position;
        }

        public Vector2 Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public Matrix Transform
        {
            get
            {
                return Matrix.CreateScale(60) * Matrix.CreateTranslation(-m_Position.X, -m_Position.Y, 0);
            }
        }
    }
}

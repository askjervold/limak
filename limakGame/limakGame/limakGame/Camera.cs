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
        /// <summary>
        /// Number of pixels per meter
        /// </summary>
        private const float PixelsPerMeter = 60;

        private Vector2 m_Position;

        bool transformCached = false;
        Matrix _transform;

        /// <summary>
        /// Creates a new 2D camera
        /// </summary>
        /// <param name="position">Camera viewport offset in pixels</param>
        public Camera2d(Vector2 position)
        {
            m_Position = position;
        }

        /// <summary>
        /// Determines whether the game object is inside the current viewport
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public bool isVisible(GameObject gameObject)
        {
            return true;
        }

        /// <summary>
        /// Returns a scaling vector for graphics drawn in pixels
        /// </summary>
        public Vector2 DrawScale
        {
            get 
            {
                float s = 1.0f / PixelsPerMeter;
                return new Vector2(s, s);
            }
        }

        /// <summary>
        /// Sets the position of the camera
        /// </summary>
        public Vector2 Position
        {
            get { return m_Position; }
            set { 
                m_Position = value;
                // Update transform
                transformCached = false;
            }
        }

        /// <summary>
        /// Returns the transformation for graphics drawn in meters
        /// </summary>
        public Matrix Transform
        {
            get
            {
                if (!transformCached)
                {
                    transformCached = true;
                    // Scale() * Translate()
                    _transform = new Matrix(
                        1.0f * PixelsPerMeter, 0.0f, 0.0f, 0.0f,
                        0.0f, 1.0f * PixelsPerMeter, 0.0f, 0.0f,
                        0.0f, 0.0f, 1.0f, 0.0f,
                        m_Position.X, m_Position.Y, 0.0f, 1.0f
                    );
                }
                return _transform;
            }
        }
    }
}

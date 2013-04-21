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

    public class Camera2D : GameComponent
    {
        private const float PixelsPerMeter = 60;

        private Vector2 m_Position;
        private Viewport m_Viewport;
        private Matrix m_TransformMatrix;
        private bool m_TransformCached = false;

        /// <summary>
        /// Creates a new 2D camera
        /// </summary>
        /// <param name="position">The camera's position in world coordinates</param>
        public Camera2D(Game game, Vector2 position, Viewport viewport) : base(game)
        {
            m_Position = position;
            m_Viewport = viewport;
        }

        /// <summary>
        /// Determines if the game object is within the camera's viewport.
        /// </summary>
        public bool IsVisible(GameObject gameObject)
        {
            RectangleF visibleArea = new RectangleF(m_Position.X, m_Position.Y, m_Viewport.Width, m_Viewport.Height);
            RectangleF objectArea = new RectangleF(ToPixels(gameObject.Position), gameObject.Size);

            return visibleArea.Intersects(objectArea);
        }

        #region Static Methods

        /// <summary>
        /// Converts a float in pixels to meters.
        /// </summary>
        public static float ToMeters(float pixels)
        {
            return pixels / PixelsPerMeter;
        }

        /// <summary>
        /// Converts a Vector2 in pixels to meters.
        /// </summary>
        public static Vector2 ToMeters(Vector2 pixels)
        {
            return new Vector2(ToMeters(pixels.X), ToMeters(pixels.Y));
        }

        /// <summary>
        /// Converts a float in meters to pixels.
        /// </summary>
        public static float ToPixels(float meters)
        {
            return meters * PixelsPerMeter;
        }

        /// <summary>
        /// Converts a Vector2 in meters to pixels.
        /// </summary>
        public static Vector2 ToPixels(Vector2 meters)
        {
            return new Vector2(ToPixels(meters.X), ToPixels(meters.Y));
        }

        #endregion

        /// <summary>
        /// The camera's position in world coordinates.
        /// </summary>
        public Vector2 Position
        {
            get { return m_Position; }
            set
            {
                m_Position = value;
                m_TransformCached = false;
            }
        }

        /// <summary>
        /// Returns the transformation for graphics drawn in meters
        /// </summary>
        public Matrix TransformMatrix
        {
            get
            {
                if (!m_TransformCached)
                {
                    m_TransformMatrix = new Matrix(
                        PixelsPerMeter, 0.0f, 0.0f, 0.0f,
                        0.0f, PixelsPerMeter, 0.0f, 0.0f,
                        0.0f, 0.0f, 1.0f, 0.0f,
                        -m_Position.X, -m_Position.Y, 0.0f, 1.0f
                    );

                    m_TransformCached = true;
                }
                return m_TransformMatrix;
            }
        }

        List<GameObject> characters = new List<GameObject>();

        public void AddCharacter(GameObject character)
        {
            characters.Add(character);
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 average = Vector2.Zero;
            foreach (GameObject character in characters)
            {
                average += character.Position;
            }

            average /= characters.Count;
            average = ToPixels(average);

            Vector2 cameraPosition = new Vector2(average.X - m_Viewport.Width / 2, average.Y - m_Viewport.Height / 2);
            Position = cameraPosition;
        }
    }

    public struct RectangleF
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public float Left
        {
            get { return this.X; }
        }

        public float Right
        {
            get { return this.X + this.Width; }
        }

        public float Top
        {
            get { return this.Y; }
        }

        public float Bottom
        {
            get { return this.Y + this.Height; }
        }

        public RectangleF(Vector2 position, Vector2 size)
            : this(position.X, position.Y, size.X, size.Y)
        {

        }

        public RectangleF(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public bool Intersects(RectangleF other)
        {
            return this.Left <= other.Right && other.Left <= this.Right && this.Top <= other.Bottom && other.Top <= this.Bottom;
        }
    }
}

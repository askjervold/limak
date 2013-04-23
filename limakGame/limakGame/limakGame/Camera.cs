using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace limakGame
{
    public class Camera : GameComponent
    {
        private Vector2 m_Position;
        private Viewport m_Viewport;
        private Matrix m_TransformMatrix;
        private bool m_TransformCached = false;

        /// <summary>
        /// Creates a new 2D camera
        /// </summary>
        /// <param name="position">The camera's position in world coordinates</param>
        public Camera(Game game, Vector2 position, Viewport viewport) : base(game)
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
            RectangleF objectArea = new RectangleF(Convert.ToPixels(gameObject.Position.X), Convert.ToPixels(gameObject.Position.Y), gameObject.Size.X, gameObject.Size.Y);

            return visibleArea.Intersects(objectArea);
        }

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
                        Convert.PixelsPerMeter, 0.0f, 0.0f, 0.0f,
                        0.0f, Convert.PixelsPerMeter, 0.0f, 0.0f,
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
            average = Convert.ToPixels(average);

            Vector2 cameraPosition = new Vector2(average.X - m_Viewport.Width / 2, average.Y - m_Viewport.Height / 2);
            Position = cameraPosition;
        }
    }
}

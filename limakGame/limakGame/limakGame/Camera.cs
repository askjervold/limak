using Microsoft.Xna.Framework;
using System;

namespace limakGame
{
    public class Camera
    {
        private const float PixelsPerMeter = Convert.PixelsPerMeter;

        private RectangleF m_WorldViewport;
        private RectangleF m_ScreenViewport;

        public Camera()
        {
            m_ScreenViewport = new RectangleF(0.0f, 0.0f, 820.0f, 460.0f);
            m_WorldViewport = new RectangleF(0.0f, 0.0f, 30.0f, 15.0f);
        }

        public RectangleF ScreenViewport
        {
            get { return m_ScreenViewport; }
        }

        public RectangleF WorldViewport
        {
            get { return m_WorldViewport; }
            set { m_WorldViewport = value; }
        }

        public Matrix TransformMatrix
        {
            get
            {
                return Matrix.CreateScale(PixelsPerMeter, PixelsPerMeter, 0.0f) *
                    Matrix.CreateTranslation(-m_WorldViewport.X * PixelsPerMeter, -m_WorldViewport.Y * PixelsPerMeter, 0.0f) *
                    Matrix.CreateScale(m_ScreenViewport.Width / (m_WorldViewport.Width * PixelsPerMeter), m_ScreenViewport.Height / (m_WorldViewport.Height * PixelsPerMeter), 0.0f);
            }
        }
    }

    public class CameraMan : GameComponent
    {
        protected Camera m_Camera;
        protected GameObject m_Star1;
        protected int m_GroundLevel = 12;

        public CameraMan(Game game, Camera camera, GameObject star1)
            : base(game)
        {
            m_Camera = camera;
            m_Star1 = star1;
        }

        public Camera Camera
        {
            get { return m_Camera; }
        }

        public override void Update(GameTime gameTime)
        {
            float height = MathHelper.Clamp((m_GroundLevel - m_Star1.Position.Y) * 2.0f, 8.0f, 80.0f);
            float width = (Convert.ToMeters(m_Camera.ScreenViewport.Width) * height) / Convert.ToMeters(m_Camera.ScreenViewport.Height);
            float x = m_Star1.Position.X - (width / 2.0f);
            float y = m_Star1.Position.Y - (height / 2.0f);


            m_Camera.WorldViewport = new RectangleF(x, y, width, height);
        }
    }

    public class DoubleTrackingCameraMan : CameraMan
    {
        private GameObject m_Star2;

        public DoubleTrackingCameraMan(Game game, Camera camera, GameObject star1, GameObject star2)
            : base(game, camera, star1)
        {
            m_Star2 = star2;
        }

        public override void Update(GameTime gameTime)
        {
            float x = (m_Star1.Position.X + m_Star2.Position.X) / 2.0f;
            float y = (m_Star1.Position.Y + m_Star2.Position.Y) / 2.0f;
            float width = Math.Abs(m_Star2.Position.X - m_Star1.Position.X) + m_Star1.Size.X + 12.0f;
            float height = Math.Abs(m_Star2.Position.Y - m_Star1.Position.Y) + m_Star1.Size.Y + 12.0f;

            float screenAspectRatio = 820.0f / 460.0f;
            float aspectRatio = width / height;
            float a = screenAspectRatio * (height / width);

            if (a < 1)
                height /= a;
            else if (a > 1)
                width *= a;

            float minWidth = 20.0f;
            if (width < minWidth)
            {
                float scale = minWidth / width;
                width *= scale;
                height *= scale;
            }

            x -= (width / 2.0f);
            y -= (height / 2.0f);

            if (y > 11.0f - height)
                y -= (y + height - 11.0f);

            m_Camera.WorldViewport = new RectangleF(x, y, width, height);
        }
    }
}

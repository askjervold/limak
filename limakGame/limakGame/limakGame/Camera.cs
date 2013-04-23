using Microsoft.Xna.Framework;

namespace limakGame
{
    public class Camera
    {
        private const float PixelsPerMeter = Convert.PixelsPerMeter;
        
        private RectangleF m_WorldViewport;
        private RectangleF m_ScreenViewport;

        public Camera()
        {
            m_ScreenViewport = new RectangleF(0.0f, 0.0f, 800.0f, 480.0f);
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
        private Camera m_Camera;
        private GameObject m_Star;
        private int m_GroundLevel = 12;
        
        public CameraMan(Game game, Camera camera, GameObject star)
            : base(game)
        {
            m_Camera = camera;
            m_Star = star;
        }

        public override void Update(GameTime gameTime)
        {
            float height = MathHelper.Clamp((m_GroundLevel - m_Star.Position.Y) * 2.0f, 8.0f, 80.0f);
            float width = (Convert.ToMeters(m_Camera.ScreenViewport.Width) * height) / Convert.ToMeters(m_Camera.ScreenViewport.Height);
            float x = m_Star.Position.X - (width / 2.0f);
            float y = m_Star.Position.Y - (height / 2.0f);

            m_Camera.WorldViewport = new RectangleF(x, y, width, height);

            /*m_Camera.WorldViewport = new RectangleF(
                m_Star.Position.X - Convert.ToMeters(m_Camera.ScreenViewport.Width) / 2, 
                m_Star.Position.Y - Convert.ToMeters(m_Camera.ScreenViewport.Height) / 2,
                Convert.ToMeters(m_Camera.ScreenViewport.Width),
                Convert.ToMeters(m_Camera.ScreenViewport.Height));*/
        }
    }
}

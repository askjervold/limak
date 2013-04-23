using Microsoft.Xna.Framework;

namespace limakGame
{
    static class Convert
    {
        public const float PixelsPerMeter = 60;

        public static float ToMeters(float pixels)
        {
            return pixels / PixelsPerMeter;
        }

        public static Vector2 ToMeters(Vector2 pixels)
        {
            return new Vector2(ToMeters(pixels.X), ToMeters(pixels.Y));
        }

        public static float ToPixels(float meters)
        {
            return meters * PixelsPerMeter;
        }

        public static Vector2 ToPixels(Vector2 meters)
        {
            return new Vector2(ToPixels(meters.X), ToPixels(meters.Y));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace limakGame
{
    public struct RectangleF
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public RectangleF(float x, float y, float width, float height)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public Vector2 Center
        {
            get { return new Vector2(this.X + (this.Width / 2), this.Y + (this.Height / 2)); }
        }

        public Vector2 UpperLeft
        {
            get { return new Vector2(this.X, this.Y); }
        }

        public Vector2 LowerLeft
        {
            get { return new Vector2(this.X, this.Y + this.Height); }
        }

        public Vector2 UpperRight
        {
            get { return new Vector2(this.X + this.Width, this.Y); }
        }

        public Vector2 LowerRight
        {
            get { return new Vector2(this.X + this.Width, this.Y + this.Height); }
        }

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

        public bool Intersects(RectangleF other)
        {
            return this.Left <= other.Right && other.Left <= this.Right && this.Top <= other.Bottom && other.Top <= this.Bottom;
        }
    }
}

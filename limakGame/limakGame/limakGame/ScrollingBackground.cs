using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace limakGame
{
    class ScrollingBackground
    {
        private Vector2 origin, screenpos;
        private Texture2D texture;
        private int screenHeight, screenWidth, pos;

        public void Load(GraphicsDevice device, Texture2D backgroundTexture)
        {
            texture = backgroundTexture;
            screenHeight = device.Viewport.Height;
            screenWidth = device.Viewport.Width*2;
            pos = 0;
            origin = new Vector2(0, 0);
            screenpos = new Vector2(0, 0);

        }

        public void Update(float positionX )
        {
            if (positionX > 0 && positionX < 80)
                screenpos.X = -positionX * 5;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (pos < screenWidth)
            {
                //spriteBatch.Draw(texture, new Rectangle(screenpos, 0, screenWidth, screenHeight), Color.White);
                spriteBatch.Draw(texture, screenpos, null, Color.White, 0, origin, new Vector2( 0.6f, 0.6f), SpriteEffects.None, 0f);
            }
            //spriteBatch.Draw(texture, new Rectangle(screenpos + screenWidth, 0, screenWidth, screenHeight), Color.White);
        }
    }
}

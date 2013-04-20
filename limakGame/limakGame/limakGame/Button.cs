using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace limakGame
{
    class Button
    {
        Texture2D textureOn, textureOff;
        Vector2 position;

        private Vector2 size;
        private bool isChoosen;

        public Button(Texture2D textureOn, Texture2D textureOff, GraphicsDevice graphicsDevice, Vector2 size)
        {
            this.textureOn = textureOn;
            this.textureOff = textureOff;
            this.size = size;
            isChoosen = false;
        }

        public void setPosition(Vector2 position)
        {
            this.position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isChoosen)
            {
                spriteBatch.Draw(textureOn, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), Color.White);
            }
            else
            {
                spriteBatch.Draw(textureOff, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), Color.White);
            }
        }

        public void setIsChoosen(bool b)
        {
            isChoosen = b;
        }
    }
}

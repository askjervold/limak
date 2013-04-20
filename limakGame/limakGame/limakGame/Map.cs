using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace limakGame
{
    class Map : DrawableGameComponent
    {

        private LogicLevelReader level;

        public Map(Game game, string mapFile) : base(game)
        {
            this.level = new LogicLevelReader(((Limak)game).world);
            this.level.readFile(mapFile);

            // TODO: 
            // finish loading the level in LogicLevelReader
            // add the components to the game by using ((Limak)game).addGameObject(GameObject gameObject)

        }

        public override void Update(GameTime gameTime)
        {
            // if required, perform any updates the game map here
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the entire game map
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            
            Limak game = ((Limak)this.Game);

            game.spriteBatch.Begin(
                SpriteSortMode.BackToFront,
                null,
                SamplerState.LinearClamp,
                DepthStencilState.Default,
                RasterizerState.CullNone,
                null,
                game.camera.TransformMatrix
            );

            // Draw map here!

            /*game.spriteBatch.Draw(
                Texture2D sprite,
                Vector2 position, // position in farseer meter
                Rectangle sourceRect, // source rectangle in pixels
                Color color,
                rotation, // rotation
                Vector2 origin, // origin
                Camera2D.ToMeters(size), // scale
                SpriteEffect,
                0.0f // layerdepth
            );*/

            game.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

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


        public Map(Game game, string mapFile)
            : base(game)
        {
            this.level = new LogicLevelReader(((Limak)game).world);
            this.level.readFile(mapFile);

            // TODO: 
            // finish loading the level in LogicLevelReader
            // add the components to the game by using ((Limak)game).addGameObject(GameObject gameObject)
            //((Limak)game).addGameObject(GameObject gameObject)
            Texture2D goo = game.Content.Load<Texture2D>("goo");
            for (int i = 0; i < level.getEnemyPos.Count; i++)
            {
                GameObject noob = new GameObject(
                    ((Limak)game),
                    ((Limak)game).world,
                    level.getEnemyPos[i], // position (meter)
                    new Vector2(1f, 1f), // size (meter)
                    new SpriteAnimation(goo, 24, 100, 1, 1)
                );
                ((Limak)game).addGameObject(noob);
            }



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
            Random random = new Random();

            Limak game = ((Limak)this.Game);
            //Rectangle groundToDraw = new Rectangle(0,this.level.levelHeight, this.level.levelWidth,1);//level.Ground.Position.X,level.Ground.Position.Y,...);
            List<Rectangle> platformsToDraw = new List<Rectangle>();
            for (int i = 0; i < level.Platforms.Count; i++)
            {


                Rectangle platform = new Rectangle(0, 6, 4, 1);
                platformsToDraw.Add(platform);
            }
            Rectangle groundToDraw = new Rectangle((int)level.Ground.Position.X, (int)level.Ground.Position.Y, this.level.levelWidth + 1, 2);


            game.spriteBatch.Begin(
                SpriteSortMode.BackToFront,
                null,
                SamplerState.LinearClamp,
                DepthStencilState.Default,
                RasterizerState.CullNone,
                null,
                game.Camera.TransformMatrix
            );

            // Draw map here!
            game.spriteBatch.Draw(game.Content.Load<Texture2D>("groundBlock"), new Vector2(level.Ground.Position.X - level.levelWidth / 2, level.Ground.Position.Y - 0.5f), groundToDraw, Color.White, 0, new Vector2(0, 0), Convert.ToMeters(50), SpriteEffects.None, 0);
            /*for (int i = 0; i < platformsToDraw.Count; i++)
            {
                game.spriteBatch.Draw(game.Content.Load<Texture2D>("groundBlock"), new Vector2(0, i), groundToDraw, Color.White, 0, new Vector2(0, 0), Camera2D.ToMeters(50), SpriteEffects.None, 0);
            }
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

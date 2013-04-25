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

            createEnemies(game);


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
                game.Camera.TransformMatrix
            );


            for (int i = 0; i < level.Ground.Count; i++)
            {
                //credits to jakob:
                Texture2D texture = game.Content.Load<Texture2D>("groundBlock");
                Vector2 scale = new Vector2((Convert.ToPixels(level.groundWidths[i]) / texture.Width) / Convert.PixelsPerMeter, (Convert.ToPixels(1.0f) / texture.Height) / Convert.PixelsPerMeter);

                game.spriteBatch.Draw(texture, level.Ground[i].Position, null, Color.White, 0.0f, new Vector2(30, 30), scale, SpriteEffects.None, 0.0f);



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

        //returning certain properties that other classes might need.

        private void createEnemies(Game game)
        {

            Texture2D goo = game.Content.Load<Texture2D>("goo");
            for (int i = 0; i < level.getEnemyPos.Count; i++)
            {
                GameEnemy enemy = new GameEnemy(
                    ((Limak)game),
                    ((Limak)game).world,
                    level.getEnemyPos[i], // position (meter)
                    new Vector2(1f, 1f), // size (meter)
                    new SpriteAnimation(goo, 24, 24, 1, 1)
                );
                //Console.WriteLine("i:" + level.getEnemyPos[i].X + "  j:" + level.getEnemyPos[i].Y) ;
                ((Limak)game).addGameObject(enemy);
            }

        }


        private void createCoins(Game game)
        {

            Texture2D goo = game.Content.Load<Texture2D>("groundBlock");
            for (int i = 0; i < level.getCoinPos.Count; i++)
            {
                GameCoin coin = new GameCoin(
                    ((Limak)game),
                    ((Limak)game).world,
                    level.getEnemyPos[i], // position (meter)
                    new Vector2(1f, 1f), // size (meter)
                    new SpriteAnimation(goo, 24, 24, 1, 1)
                );
                //Console.WriteLine("i:" + level.getEnemyPos[i].X + "  j:" + level.getEnemyPos[i].Y) ;
                ((Limak)game).addGameObject(coin);
            }

        }


        public int levelWidth
        {
            get { return level.levelWidth; }
        }

        public int levelHeight
        {
            get { return level.levelHeight; }
        }


        public List<int> holes
        {
            get { return level.holes; }
        }
    }
}

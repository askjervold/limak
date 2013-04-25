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


            //creating stuff
            createEnemies(game);
            createCoins(game);
            createFlag(game);
    
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

            Texture2D enemyTexture = game.Content.Load<Texture2D>("enemy1_animated");
            Vector2 size = new Vector2(2, 2);
            
            for (int i = 0; i < level.getEnemyPos.Count; i++)
            {
                SpriteAnimation enemyAnimation = new SpriteAnimation(enemyTexture, (int)Convert.ToPixels(size.X), (int)Convert.ToPixels(size.Y), 2, 4);
                enemyAnimation.AnimationDelay = 400;
                GameEnemy enemy = new GameEnemy(
                    ((Limak)game),
                    ((Limak)game).world,
                    level.getEnemyPos[i], // position (meter)
                    size, // size (meter)
                    enemyAnimation

                );
                //Console.WriteLine("i:" + level.getEnemyPos[i].X + "  j:" + level.getEnemyPos[i].Y) ;
                ((Limak)game).addGameObject(enemy);
            }

        }


        private void createCoins(Game game)
        {

            Texture2D coinTexture = game.Content.Load<Texture2D>("coin");
            Vector2 size = new Vector2(1, 1);            
            for (int i = 0; i < level.getCoinPos.Count; i++)
            {
                GameCoin coin = new GameCoin(
                    ((Limak)game),
                    ((Limak)game).world,
                    level.getCoinPos[i], // position (meter)
                    size, // size (meter)
                    new SpriteAnimation(coinTexture, (int)Convert.ToPixels(size.X), (int)Convert.ToPixels(size.Y), 1, 1)
                );
                ((Limak)game).addGameObject(coin);
            }

        }

        private void createFlag(Game game)
        {
            Texture2D flagTexture = game.Content.Load<Texture2D>("finish");
            Vector2 size = new Vector2(2, 2);
            GameFlag flag = new GameFlag(
                    ((Limak)game),
                    ((Limak)game).world,
                    level.getFlagPos, // position (meter)
                    size, // size (meter)
                    new SpriteAnimation(flagTexture, (int)Convert.ToPixels(size.X), (int)Convert.ToPixels(size.Y), 1, 1)
                );
            ((Limak)game).addGameObject(flag);
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

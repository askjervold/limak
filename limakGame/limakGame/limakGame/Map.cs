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
                //Console.WriteLine("i:" + level.getEnemyPos[i].X + "  j:" + level.getEnemyPos[i].Y) ;
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

            Limak game = ((Limak)this.Game);
            //Rectangle groundToDraw = new Rectangle(0,this.level.levelHeight, this.level.levelWidth,1);//level.Ground.Position.X,level.Ground.Position.Y,...);
            List<Rectangle> platformsToDraw = new List<Rectangle>();
            List<Rectangle> groundsToDraw = new List<Rectangle>();
            for (int i = 0; i < level.Platforms.Count; i++)
            {

                Rectangle platformToDraw = new Rectangle((int)level.Platforms[i].Position.X + /*(int)(level.groundWidths[i] / 2)*/ + 1, (int)level.Platforms[i].Position.Y, 2, 2);

                
                platformsToDraw.Add(platformToDraw);
            }
            for (int i = 0; i < level.Ground.Count; i++)
            {
                Rectangle groundToDraw = new Rectangle((int)level.Ground[i].Position.X + (int)(level.groundWidths[i]/2)+1, (int)level.Ground[i].Position.Y, level.groundWidths[i]+2, 2);
                
                groundsToDraw.Add(groundToDraw);

            }
            //Rectangle groundToDraw = new Rectangle((int)level.Ground1.Position.X, (int)level.Ground1.Position.Y, (level.levelWidth), 2);

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
            for (int i = 0; i < groundsToDraw.Count; i++)
            {
                //this method adds the ground, it's not correct as of now, i need to get the ground width before it can be drawn correctly.
                game.spriteBatch.Draw(game.Content.Load<Texture2D>("groundBlock"), new Vector2(level.Ground[i].Position.X - (level.groundWidths[i]/2)-1, level.Ground[i].Position.Y - 0.5f), groundsToDraw[i], Color.White, 0, new Vector2(0, 0), Camera.ToMeters(50), SpriteEffects.None, 0);



            }
            //game.spriteBatch.Draw(game.Content.Load<Texture2D>("groundBlock"), new Vector2(level.Ground.Position.X - (level.levelWidth / 2), level.Ground.Position.Y - 0.5f), groundToDraw, Color.White, 0, new Vector2(0, 0), Camera.ToMeters(50), SpriteEffects.None, 0);
            for (int i = 0; i < platformsToDraw.Count; i++)
            {
                game.spriteBatch.Draw(game.Content.Load<Texture2D>("groundBlock"), new Vector2(level.Platforms[i].Position.X, level.Platforms[i].Position.Y-0.5f), platformsToDraw[i], Color.White, 0, new Vector2(0, 0), Camera.ToMeters(50), SpriteEffects.None, 0);
            }
            /*game.spriteBatch.Draw(
                Texture2D sprite,
                Vector2 position, // position in farseer meter
                Rectangle sourceRect, // source rectangle in pixels
                Color color,
                rotation, // rotation
                Vector2 origin, // origin
                Camera.ToMeters(size), // scale
                SpriteEffect,
                0.0f // layerdepth
            );*/

            game.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

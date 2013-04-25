using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Common;

namespace limakGame
{
    class GamePlayer : GameCharacter
    {
        private int score;
        private PlayerIndex playerIndex;
        private Game game;


        public GamePlayer(Game game, World world, Vector2 position, Vector2 size, SpriteAnimation animation, PlayerIndex playerIndex) 
            : base(game, world, position, size, animation)
        {
            this.playerIndex = playerIndex;
            this.game = game;
        }


        //Get Set: score
        private void setScore(int score)
        {
            this.score = score;
        }
        public int getScore()
        {
            return this.score;
        }
        public void increaseScore(int amount)
        {
            this.score += amount;
        }


        public void Revive()
        {
            this.isDead = false;
            
            // Do more stuff to make the player alive again
        }


        // Collision handlers
        public bool CollisionWithEnemy(Fixture f1, Fixture f2, Contact contact)
        {
            Vector2 normal;
            FixedArray2<Vector2> points;
            contact.GetWorldManifold(out normal, out points);

            foreach (IGameComponent comp in this.game.Components)
            {
                GameEnemy enemy = comp as GameEnemy;
                if (enemy != null)
                {
                    if (enemy.getFixture() == f2)
                    {

                        if ((Math.Abs(normal.Y) > Math.Abs(normal.X)) && (normal.Y < 0))    // The contact is coming from above
                        {
                            enemy.Die();
                            this.increaseScore(10);
                            this.Jump();
                        }
                        else
                        {
                            this.Die();
                        }

                        break;
                    }
                }
            }


            return true;
        }

        public bool PickUpCoin(Fixture f1, Fixture f2, Contact contact)
        {
            foreach (IGameComponent comp in this.game.Components)
            {
                GameCoin coin = comp as GameCoin;
                if (coin != null)
                {
                    if (coin.getFixture() == f2)
                    {
                        coin.Dispose();
                        this.game.Components.Remove(coin);
                        this.increaseScore(1);
                    }
                }
            }

            return true;
        }

        public bool PlayerPlayerCollision(Fixture f1, Fixture f2, Contact contact)
        {
            Vector2 normal;
            FixedArray2<Vector2> points;
            contact.GetWorldManifold(out normal, out points);

            foreach (IGameComponent comp in this.game.Components)
            {
                GamePlayer player = comp as GamePlayer;
                if (player != null)
                {
                    if ((player.getFixture() == f2))
                    {
                        if ((Math.Abs(normal.Y) > Math.Abs(normal.X)) && (normal.Y < 0))
                        {
                            if (player.isDead)
                            {
                                player.Revive();
                            }
                        }
                    }
                }
            }

            /*
            if (player1.getFixture() == f2)
            {
                if ((Math.Abs(normal.Y) > Math.Abs(normal.X)) && (normal.Y < 0))
                {
                    if (player1.IsDead()) // Shouldn't this work with just "if (player1.isDead)"?
                    {
                        player1.Revive();
                    }
                }
            }

            else if (player2.getFixture() == f2)
            {
                if ((Math.Abs(normal.Y) > Math.Abs(normal.X)) && (normal.Y < 0))
                {
                    if (player2.IsDead())
                    {
                        player2.Revive();
                    }
                }
            }
            */
             
            return true;
        }
        
        /*public bool PlayerFinish(Fixture f1, Fixture f2, Contact contact)   // Change all references to coins into flag/finish
        {
            foreach (IGameComponent comp in this.game.Components)
            {
                GameCoin coin = comp as GameCoin;
                if (coin != null)
                {
                    if (coin.getFixture() == f2)
                    {
                        this.increaseScore(50);
                        // Should end the level
                    }
                }
            }

            return true;
        }*/
    }
}

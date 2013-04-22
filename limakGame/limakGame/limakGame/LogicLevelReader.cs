using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace limakGame
{
    class LogicLevelReader
    {
        //the world object to be passed in through the constructor
        World world;

        //variables to convert between units
        const float unitToPixel = 60.0f;
        const float pixelToUnit = 1 / unitToPixel;

        //height and length of level, found by readFile()
        private int _levelHeight;
        private int _levelWidth;

        //types of characters found in the .txt file
        private string _ground = "#";
        private string _platform = "_";
        private string _player = "|";
        private string _enemy = "@";

        //types of bodies created and returned to the draw class.
        private Body _groundBody; 
        private List<Body> _platforms;
        private List<GameObject> _enemies;
        private List<Vector2> _enemyPositions;
        private GameObject _player1Object;


        //sprite animations
        private SpriteAnimation _playerAnimation;
        private SpriteAnimation _enemyAnimation;
        
        //an array of string where each element is each line in the .txt file.
        string[] text;

        //the constructor should receive the world object
        public LogicLevelReader(World world)
        {
            this.world = world;

            _platforms = new List<Body>();
            _enemies = new List<GameObject>();
            _enemyPositions = new List<Vector2>();
                        
        }

        //this reads the given file, we can change which file by inputting a different parameter.
        public void readFile(string file)
        {
            bool includesTxt = file.EndsWith(".txt", System.StringComparison.OrdinalIgnoreCase);
            if(includesTxt)
                text = System.IO.File.ReadAllLines(file);
            else
                text = System.IO.File.ReadAllLines(file + ".txt");

            //createGround();

            //registers the dimensions of the level
            _levelHeight = text.Length;
            _levelWidth = text[_levelHeight - 1].Length;

//            createGround();
            createPlatforms(); //TODO
            createEnemies();
            createGround();
            Console.WriteLine("ENEMY POS");
            createEnemyPositions();
            Console.WriteLine("ENEMY POS DONE");
            
            
        }

        public void createPlayer()
        {
            Vector2 start = new Vector2(0, 0);
            Vector2 size = new Vector2(2, 2);
            // TODO:
            //_playerAnimation = new SpriteAnimation(120,120,4,7)
            //_playerObject = new GameObject(game, _world,  start,  size, animation);
        
        

        }

        public void createGround()
        //assumes no holes in the ground, creating a single body representing the entire ground floor
        {
            //this method assumes that the ground is always on the lowest point, which all map should be.
            int levelHeight = text.Length - 1;
            int first = text[levelHeight].IndexOf(_ground);
            int last = text[levelHeight].LastIndexOf(_ground);
            string str2 = text[levelHeight].Substring(first, last - first); // gets the width of level.
            //_groundSprite = Content.Load<Texture2D>("groundSprite"); // 512px x 64px =>   8m x 1m


            //create ground fixture, it assumes that the ground doesn't have any holes in it
            _groundBody = BodyFactory.CreateRectangle(world, str2.Length, 1, 1, new Vector2(0 / 2, levelHeight - 1));
            //_groundBody = BodyFactory.CreateRectangle(world, 60.0f, 1.0f, 1.0f);
            

            //non-movable object.
            _groundBody.BodyType = BodyType.Static;

            //some copy-paste code
            _groundBody.Restitution = 0.3f;
            _groundBody.Friction = 0.5f;
             
        }

        public void createPlatforms()
        {
            // iterates through the entire level file and checks for a "_" which indicates a platform. If one is found, a body is added to the platform list
            // Should be modified for GameObjects to note the position of the platform.
            bool onPlatform = false;
            for (int i = 0; i < _levelHeight; i++)
            {
                for (int j = 0; j < _levelWidth; j++)
                {
                        
                    try
                    {
                        Console.Write(text[i][j]);
                        if (text[i][j].Equals(_platform))
                        {
                            Console.WriteLine("gfdgfdgdhgfdyrthcv,juybgikbliyvbulyvkutvujvujtgvckutgvtgvkktf");
                            // GameObject platform = new GameObject(game, new Vector2(j*unitToPixel, i*unitToPixel), 1*unitToPixel, height*unitToPixel, animation);
                            onPlatform = true;
                            //CreateRectangle(world, width, height, density, positionVector);
                            Body platformTemp = BodyFactory.CreateRectangle(world, 4, 1, 1, new Vector2(j + 2, i));
                            Console.Write("hei");
                            _platforms.Add(platformTemp);

                        }
                        if (text[i][j].Equals(" "))
                            onPlatform = false;
                    }
                    catch
                    {
                        break;
                    }
                    

                    

                }
                Console.WriteLine();
                
            }
            Console.Write(_platforms.Count);

            
        }

        public void createEnemies()
        {
            /*
            for (int i = 0; i < _levelHeight; i++)
            {
                for (int j = 0; j < _levelWidth; j++)
                {
                    if (text[i][j].Equals(_enemy))
                    {
                        //GameObject enemyTemp = new GameObject(game, world, new Vector2(j,0),new Vector2(1,1),animation);
                        //_enemies.Add(enemyTemp);
                    }
                }


            }*/
        }

        public void createEnemyPositions()
        {
            for (int i = 0; i < _levelHeight; i++)
            {
                
                    try
                    {
                        int j = 0;
                        while ((i = text[i].IndexOf('@', j)) != -1 && j < _levelWidth)
                        {
                            Console.WriteLine("AAAA");
                            // C.
                            // Print out the substring.
                            _enemyPositions.Add(new Vector2(i, j));

                            // D.
                            // Increment the index.
                            j++;
                        }

                    }

                     /*   if (text[i][j].Equals("n")) Console.WriteLine("NEWLINE");
                        //Console.Write("\tI:" + i + " J:" + j + " tegn:" + text[i][j]);
                        if (text[i][j].Equals(_enemy))
                        {
                            _enemyPositions.Add(new Vector2(i, j));
                        }
                    }*/
                    catch
                    {
                    }
             


            }
            Vector2 a =new Vector2(0, 2f);
            _enemyPositions.Add(a);
            Console.WriteLine("\n\n\n\n\n"+_enemyPositions.Count);
        }

        public List<Vector2> getEnemyPos
        {
            get { return _enemyPositions; }
        }

        public Body Ground
        {
            get { return _groundBody; }
        }

        public GameObject player1
        {
            get { return _player1Object; }
        }


        public List<Body> Platforms
        {
            get { return _platforms; }
        }


        public List<GameObject> Enemies
        {
            get { return _enemies; }            
        }

        public int levelHeight //should be 12, but who knows
        {
            get { return _levelHeight; }
        }

        public int levelWidth
        {
            get { return _levelWidth; }
        }
    }
}

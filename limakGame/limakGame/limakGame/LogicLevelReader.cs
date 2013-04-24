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
        private char _ground = '#';
        private char _platform = '_';
        private char _player = '|';
        private char _enemy = '@';
        private char _empty = ' ';
        private char _end = 'n';


        //types of bodies created and returned to the draw class.
        private Body _groundBody1;
        private List<Body> _platforms;
        private List<Body> _groundBody;
        private List<int> _groundWidths;
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

            _groundBody = new List<Body>();
            _groundWidths = new List<int>();
            _platforms = new List<Body>();
            _enemies = new List<GameObject>();
            _enemyPositions = new List<Vector2>();


        }

        //this reads the given file, we can change which file by inputting a different parameter.
        public void readFile(string file)
        {
            bool includesTxt = file.EndsWith(".txt", System.StringComparison.OrdinalIgnoreCase);
            if (includesTxt)
                text = System.IO.File.ReadAllLines(file);
            else
                text = System.IO.File.ReadAllLines(file + ".txt");

            //registers the dimensions of the level
            _levelHeight = text.Length;
            _levelWidth = text[_levelHeight - 1].Length;

            createPlatforms();
            createGround();
            createEnemyPositions();


        }
        //this might not be used.
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
            
            int start = 0;
            bool lastFloor = false;


            for (int i = 0; i < _levelWidth; i++)
            {
                if (text[levelHeight][i] == _ground && !lastFloor)
                {
                    start = i;
                    lastFloor = true;
                }
                else if ((text[levelHeight][i] == _empty || text[levelHeight][i] == _end) && lastFloor)
                {
                    int width = i - start;
                    Body temp = BodyFactory.CreateRectangle(world, width, 1, 1, new Vector2((start), levelHeight));
                    //non-movable object.
                    Console.WriteLine("ground created @" + (start) + "," + i + " width:" + (width));

                    _groundWidths.Add(width);

                    temp.BodyType = BodyType.Static;

                    //some copy-paste code
                    temp.Restitution = 0.3f;
                    temp.Friction = 0.5f;
                    
                    _groundBody.Add(temp);
                    lastFloor = false;
                }

            }

            
            //old method for non-hole ground, isn't used anymore but I want to keep it if we might use it later.
            //int first = text[levelHeight].IndexOf(_ground);
            //int last = text[levelHeight].LastIndexOf(_ground);
            //string str2 = text[levelHeight].Substring(first, last - first); // gets the width of level.
            //_groundSprite = Content.Load<Texture2D>("groundSprite"); // 512px x 64px =>   8m x 1m



            ////create ground fixture, it assumes that the ground doesn't have any holes in it
            //_groundBody1 = BodyFactory.CreateRectangle(world, str2.Length, 1, 1, new Vector2(levelWidth/ 2, levelHeight - 1));
            ////_groundBody = BodyFactory.CreateRectangle(world, 60.0f, 1.0f, 1.0f);


            ////non-movable object.
            //_groundBody1.BodyType = BodyType.Static;

            ////some copy-paste code
            //_groundBody1.Restitution = 0.3f;
            //_groundBody1.Friction = 0.5f;

        }

        public void createPlatforms()
        {
           
            bool onPlatform = false;
            int start = 0;
            for (int i = 0; i < _levelHeight; i++)
            {
                for (int j = 0; j < _levelWidth; j++)
                {
                    if (text[i][j] == _platform && !onPlatform)
                    {
                        start = j;
                        onPlatform= true;
                    }
                    else if ((text[i][j] == _empty || text[i][j] == _end || text[i][j] == _enemy)&& onPlatform)
                    {
                        Body temp = BodyFactory.CreateRectangle(world, (j) - start, 1, 1, new Vector2((start), i));
                        //non-movable object.

                        temp.BodyType = BodyType.Static;

                        //some copy-paste code
                        temp.Restitution = 0.3f;
                        temp.Friction = 0.5f;

                        _platforms.Add(temp);
                        onPlatform= false;
                    }

                }

            }


        }


        public void createEnemyPositions()
        {
            for (int i = 0; i < _levelHeight; i++)
            {
                for(int j = 0; j<_levelWidth; j++)
                {
                    if(text[i][j] == _end)
                        break;
                    if(text[i][j] == _enemy)
                    {
                        _enemyPositions.Add(new Vector2(j, i));
                    }

                }
                

            

            }
            
        }

        public List<Vector2> getEnemyPos
        {
            get { return _enemyPositions; }
        }

        public Body Ground1
        {
            get { return _groundBody1; }
        }

        public List<int> groundWidths
        {
            get { return _groundWidths; }
        }

        public List<Body> Ground
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

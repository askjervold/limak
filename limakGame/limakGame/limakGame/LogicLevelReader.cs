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
        World _world;

        //variables to convert between units
        const float unitToPixel = 60.0f;
        const float pixelToUnit = 1 / unitToPixel;

        //height and length of level, found by readFile()
        private int levelHeight;
        private int levelWidth;

        //types of characters found in the .txt file
        private string _ground = "#";
        private string _platform = "_";
        private string _player = "|";
        private string _enemy = "@";

        //types of bodies created and returned to the draw class.
        private Body _groundBody; 
        private List<Body> _platforms;
        private List<Body> _enemies;
        
        //an array of string where each element is each line in the .txt file.
        string[] text;

        //the constructor should receive the world object
        public LogicLevelReader(World world)
        {
            // _world = world;

            // Temporary world for testing purposes
            _world = new World(new Vector2(0, 9.81f));

            _platforms = new List<Body>();
            _enemies = new List<Body>();
            
        }

        //this reads the given file, we can change which file by inputting a different parameter.
        public void readFile(string file)
        {
            bool includesTxt = file.EndsWith(".txt", System.StringComparison.OrdinalIgnoreCase);
            if(includesTxt)
                text = System.IO.File.ReadAllLines(file);
            else
                text = System.IO.File.ReadAllLines(file + ".txt");

            //registers the dimensions of the level
            levelHeight = text.Length;
            levelWidth = text[levelHeight - 1].Length;

            createGround();
            //createPlatforms(); //TODO
            createEnemies();
            
            
        }

        //assumes no holes in the ground, creating a single body representing the entire ground floor
        public void createGround()
        {
            //this method assumes that the ground is always on the lowest point, which all map should be.
            int levelHeight = text.Length - 1;
            int first = text[levelHeight].IndexOf(_ground);
            int last = text[levelHeight].LastIndexOf(_ground);
            string str2 = text[levelHeight].Substring(first, last - first); // gets the width of level.
            //_groundSprite = Content.Load<Texture2D>("groundSprite"); // 512px x 64px =>   8m x 1m


            //create ground fixture, it assumes that the ground doesn't have any holes in it
            _groundBody = BodyFactory.CreateRectangle(_world, str2.Length, 1, 1);

            //non-movable object.
            _groundBody.BodyType = BodyType.Static;

            //some copy-paste code
            _groundBody.Restitution = 0.3f;
            _groundBody.Friction = 0.5f;
             
        }

        public void createPlatforms()
        {
            // iterates through the entire level file and checks for a "_" which indicates a platform. If one is found, a body is added to the platform list
            for (int i = 0; i < levelHeight; i++)
            {
                for (int j = 0; j < levelWidth; j++)
                {
                    if (text[i][j].Equals("_"))
                    {
                        _platforms.Add(new Body(_world));
                    }
                    
                }

                
            }

            
        }

        public void createEnemies()
        {

        }

        public Body Ground
        {
            get { return _groundBody; }
        }


        public List<Body> Platforms
        {
            get { return _platforms; }
        }


        public List<Body> Enemies
        {
            get { return _enemies; }            
        }
    }
}

﻿using System;
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
        private List<Body> _enemies;
        private GameObject _playerObject;
        
        //an array of string where each element is each line in the .txt file.
        string[] text;

        //the constructor should receive the world object
        public LogicLevelReader(World world)
        {
            this.world = world;

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

            //createGround();

            //registers the dimensions of the level
            _levelHeight = text.Length;
            _levelWidth = text[_levelHeight - 1].Length;

//            createGround();
            //createPlatforms(); //TODO
            createEnemies();
            
            
        }

        public void createPlayer()
        {
            Vector2 start = new Vector2(0, 0);
            Vector2 size = new Vector2(2, 2);
            // TODO:
            //_playerObject = new GameObject(game, _world,  start,  size, animation);
        
        

        }

        public void createGround(Vector2 vector)
        //assumes no holes in the ground, creating a single body representing the entire ground floor
        {
            //this method assumes that the ground is always on the lowest point, which all map should be.
            int levelHeight = text.Length - 1;
            int first = text[levelHeight].IndexOf(_ground);
            int last = text[levelHeight].LastIndexOf(_ground);
            string str2 = text[levelHeight].Substring(first, last - first); // gets the width of level.
            //_groundSprite = Content.Load<Texture2D>("groundSprite"); // 512px x 64px =>   8m x 1m


            //create ground fixture, it assumes that the ground doesn't have any holes in it
            _groundBody = BodyFactory.CreateRectangle(world, str2.Length, 1f, 1f, vector);
            

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
            for (int i = 0; i < _levelHeight; i++)
            {
                for (int j = 0; j < _levelWidth; j++)
                {
                    if (text[i][j].Equals("_"))
                    {
                        // GameObject platform = new GameObject(game, new Vector2(j*unitToPixel, i*unitToPixel), 1*unitToPixel, height*unitToPixel, animation);
                        _platforms.Add(new Body(world));
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

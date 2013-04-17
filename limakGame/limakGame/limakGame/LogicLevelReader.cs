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
        //types of characters found in the .txt file
        private string _ground = "#";
        private string _player = "|";
        private string _enemy = "@";

        //types of bodies created and returned to the draw class.
        private Body _groundBody;
        private Body[] _platforms;
        private Body[] _enemies;
        
        //an array of string where each element is each line in the .txt file.
        string[] text;

        //this reads the given file, we can change which file by inputting a different parameter.
        public void readFile(string file)
        {
            bool includesTxt = file.EndsWith(".txt", System.StringComparison.OrdinalIgnoreCase);
            if(includesTxt)
                text = System.IO.File.ReadAllLines(file);
            else
                text = System.IO.File.ReadAllLines(file + ".txt");

            createGround();
            //createPlatforms(); //TODO
            createEnemies();
            
            
        }

        public void createGround(){
            //this method assumes that the ground is always on the lowest point, which all map should be.
            int first = text[text.Length-1].IndexOf(_ground);
            int last = text[text.Length-1].LastIndexOf(_ground);
            string str2 = text[text.Length-1].Substring(first, last - first); // gets the width of level.
            //_groundSprite = Content.Load<Texture2D>("groundSprite"); // 512px x 64px =>   8m x 1m


            //create ground fixture, it assumes that the ground doesn't have any holes in it
            _groundBody = BodyFactory.CreateRectangle(new World(new Vector2(0,9.81f)), str2.Length, 1, 1);

            //non-movable object.
            _groundBody.IsStatic = true;
            //some copy-paste code
            _groundBody.Restitution = 0.3f;
            _groundBody.Friction = 0.5f;
             
        }

        public void createEnemies()
        {

        }

        public Body Ground
        {
            get { return _groundBody; }
        }


        public Body[] Platforms
        {
            get { return _platforms; }
        }


        public Body[] Enemies
        {
            get { return _enemies; }            
        }
    }
}

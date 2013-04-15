using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace limakGame
{
    class LogicLevelReader
    {
        private string _ground = "#";
        private string player = "|";
        private Body _groundBody;
        private Texture2D _groundSprite;
        string[] text;

        public void readFile(string file)
        {
            text = System.IO.File.ReadAllLines(file + ".txt");
            createGround();
            
            
        }

        public void createGround(){
            //this method assumes that the ground is always on the lowest point.
            int first = text[text.Length-1].IndexOf(_ground);
            int last = text[text.Length-1].LastIndexOf(_ground);
            string str2 = text[text.Length-1].Substring(first, last - first);
            _groundSprite = Content.Load<Texture2D>("groundSprite"); // 512px x 64px =>   8m x 1m


            //create ground fixture.
            _groundBody = BodyFactory.CreateRectangle(_world, 800f / MeterInPixels, 64f / MeterInPixels, 1f, groundPosition);
            _groundBody.IsStatic = true;
            _groundBody.Restitution = 0.3f;
            _groundBody.Friction = 0.5f;
             
        }

        public Body getGround(){
            return _groundBody;

    }
}

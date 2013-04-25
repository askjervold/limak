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


        //height and length of level, found by readFile()
        private int _levelHeight;
        private int _levelWidth;

        //types of characters found in the .txt file
        private char _ground = '#';
        private char _enemy = '@';
        private char _empty = ' ';
        private char _end = 'n';
        private char _coin = 'c';

        //types of bodies created and returned to the draw class.
        private List<Body> _groundBody;
        private List<int> _groundWidths;
        private List<Vector2> _enemyPositions;
        private List<int> _holesInground;
        private List<Vector2> _coinsPosition;

        //an array of string where each element is each line in the .txt file.
        string[] text;

        //the constructor should receive the world object
        public LogicLevelReader(World world)
        {
            this.world = world;


            _groundBody = new List<Body>();
            _groundWidths = new List<int>();
            _enemyPositions = new List<Vector2>();
            _coinsPosition = new List<Vector2>();

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

            createGround(); //creates ground and platforms
            createCoinPositions(); //adds coinsposition, map is rendering and adding them to the map
            createEnemyPositions(); //same as coins but for enemies
            
            findHolesInGround(); //finds the holes in the ground so the ai can avoid them
        }



        private void createGround()
        {

            int start = 0;
            bool lastFloor = false;

            for (int h = 0; h < _levelHeight; h++)
            {
                for (int i = 0; i < _levelWidth; i++)
                {
                    if (text[h][i] == _ground && !lastFloor)
                    {
                        start = i;
                        lastFloor = true;
                    }
                    else if ((text[h][i] == _empty || text[h][i] == _end || text[h][i] == _enemy) && lastFloor)
                    {
                        int width = i - start;
                        Body temp = BodyFactory.CreateRectangle(world, width, 1, 1, new Vector2((start) + width / 2, h - 0.5f));
                        //non-movable object.
                        Console.WriteLine("ground created @" + (start) + "," + i + " width:" + (width));

                        _groundWidths.Add(width);

                        temp.BodyType = BodyType.Static;

                        //some copy-paste code
                        temp.Restitution = 0.3f;
                        temp.Friction = 0.5f;

                        _groundBody.Add(temp);
                        lastFloor = false;
                        if (text[h][i] == _end)
                            break;
                    }

                }
            }

            
        }


        private void findHolesInGround()
        {
            _holesInground = new List<int>();
            int levelHeight = _levelHeight - 1;
            for (int i = 0; i < _levelWidth; i++)
            {
                if (text[levelHeight][i] == _empty || text[levelHeight][i] == _end)
                {
                    _holesInground.Add(i);
                }
            }

        }



        private void createCoinPositions()
        {
            for (int i = 0; i < _levelHeight; i++)
            {
                for (int j = 0; j < _levelWidth; j++)
                {
                    if (text[i][j] == _end)
                        break;
                    if (text[i][j] == _coin)
                    {
                        _coinsPosition.Add(new Vector2(j, i));
                    }

                }

            }
        }



        private void createEnemyPositions()
        {
            for (int i = 0; i < _levelHeight; i++)
            {
                for (int j = 0; j < _levelWidth; j++)
                {
                    if (text[i][j] == _end)
                        break;
                    if (text[i][j] == _enemy)
                    {
                        _enemyPositions.Add(new Vector2(j, i));
                    }

                }




            }

        }





        //getters ahead
        public List<Vector2> getEnemyPos
        {
            get { return _enemyPositions; }
        }

        public List<Vector2> getCoinPos
        {
            get { return _coinsPosition; }
        }


        public List<int> groundWidths
        {
            get { return _groundWidths; }
        }

        public List<int> holes
        {
            get { return _holesInground; }
        }

        public List<Body> Ground
        {
            get { return _groundBody; }
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

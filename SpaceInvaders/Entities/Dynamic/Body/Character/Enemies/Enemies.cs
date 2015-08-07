using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SpaceInvaders
{
    class Enemies : Character
    {
        const int ROWS = 5;
        const int COLS = 11;

        //string isAlienAlive = "Yes";

        int count; //Timer for the invaders
        const int moveNow = 30; // How many seconds you want to move the invaders
        int step = 8; //How many pixels you want the invaders to move
        public Rectangle[,] m_recInvaders;

        public Enemies[,] m_Enemies;

        bool IsGoingLeft;

        public Enemies(World world, Vector2 pos, Vector2 size, Texture2D tex)
            : base(world, pos, size, tex) {

                m_recInvaders = new Rectangle[ROWS, COLS];

                m_Enemies = new Enemies[ROWS, COLS];

                for (int r = 0; r < ROWS; r += 1)
                    for (int c = 0; c < COLS; c += 1)
                    {
                        Enemy enemy;
                        switch(ROWS) {
                          case 0: {
                            enemy = new Enemy(m_world, new Vector2(30 * c, 25 * r), new Vector(28, 25), 0, 1, 6);
                          } break;

                          case 1: {
                            enemy = new Enemy(m_world, new Vector2(30 * c, 25 * r), new Vector(28, 25), 2, 3, 6);
                          } break;

                          case 2: {
                            enemy = new Enemy(m_world, new Vector2(30 * c, 25 * r), new Vector(28, 25), 2, 3, 6);
                          } break;

                          case 3: {
                            enemy = new Enemy(m_world, new Vector2(30 * c, 25 * r), new Vector(28, 25), 4, 5, 6);
                          } break;

                          case 4: {
                            enemy = new Enemy(m_world, new Vector2(30 * c, 25 * r), new Vector(28, 25), 4, 5, 6);
                          } break;
                        }

                        m_Enemies[r, c] = enemy;

                        // m_recInvaders[r, c].Width = m_world.m_texInvader1.Width;
                        // m_recInvaders[r, c].Height = m_world.m_texInvader1.Height;
                        // m_recInvaders[r, c].X = 30 * c;
                        // m_recInvaders[r, c].Y = 25 * r;
                    }
        }
    }
}

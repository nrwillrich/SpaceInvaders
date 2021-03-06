﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SpaceInvaders
{
    class Enemies
    {
        const int ROWS = 5;
        const int COLS = 11;

        World m_world;

        // mapeamento texturas InvadersSheet.jpg
        public Rectangle[] m_Enemy1Playing = new Rectangle[]
        {
            new Rectangle ( 0, 0, 28, 20),
            new Rectangle ( 28, 0, 28, 20)
        };

        public Rectangle[] m_Enemy2Playing = new Rectangle[]
        {
            new Rectangle ( 0, 20, 28, 20),
            new Rectangle ( 28, 20, 28, 20)
        };

        public Rectangle[] m_Enemy3Playing = new Rectangle[]
        {
            new Rectangle ( 0, 40, 28, 20),
            new Rectangle ( 28, 40, 28, 20)
        };

        public Rectangle[] m_EnemyDying = new Rectangle[]
        {
            new Rectangle ( 0, 60, 28, 20)
        };

        //string isAlienAlive = "Yes";

        int count; //Timer for the invaders
        const int moveNow = 30; // How many seconds you want to move the invaders
        int step = 8; //How many pixels you want the invaders to move
        // public Rectangle[,] m_recInvaders;

        public Enemy[,] m_enemies;

        bool IsGoingLeft;

        public Enemies(World world) {

            m_enemies = new Enemy[ROWS, COLS];
            m_world = world;

            for (int r = 0; r < ROWS; r += 1)
                for (int c = 0; c < COLS; c += 1)
                {
                    Enemy enemy = null;

                    Vector2 position = new Vector2(20, 125) + new Vector2(30 * (c + 1), 25 * (r + 1));

                    switch(r) {
                        case 0: {
                            enemy = new Enemy(world, position, new Vector2(28, 20), world.m_texInvadersSheet, m_Enemy3Playing, m_EnemyDying);
                            enemy.points = 30;
                        } break;

                        case 1: {
                            enemy = new Enemy(world, position, new Vector2(28, 20), world.m_texInvadersSheet, m_Enemy2Playing, m_EnemyDying);
                            enemy.points = 20;
                        } break;

                        case 2: {
                            enemy = new Enemy(world, position, new Vector2(28, 20), world.m_texInvadersSheet, m_Enemy2Playing, m_EnemyDying);
                            enemy.points = 20;
                        } break;

                        case 3: {
                            enemy = new Enemy(world, position, new Vector2(28, 20), world.m_texInvadersSheet, m_Enemy1Playing, m_EnemyDying);
                            enemy.points = 10;
                        } break;

                        case 4: {
                            enemy = new Enemy(world, position, new Vector2(28, 20), world.m_texInvadersSheet, m_Enemy1Playing, m_EnemyDying);
                            enemy.points = 10;
                        } break;
                    }
                    world.m_entities.Add(enemy);
                    m_enemies[r, c] = enemy;
                }
        }

        public int GetAliveCount() {
            int alive = 0;

            for (int r = 0; r < ROWS; r += 1)
                for (int c = 0; c < COLS; c += 1) {
                    if (m_enemies[r, c].m_isAlive) {
                        alive++;
                    }
                }

            return alive;
        }

        void Move() {
            int rightside = ((int) m_world.m_screenRes.X);
            int leftside = 25;

            bool changedirection = false;

            foreach (Dynamic dynamic in m_world.m_entities) {
                if (dynamic is Enemy) {
                    if (IsGoingLeft)
                        dynamic.m_pos.X -= step;
                    else
                        dynamic.m_pos.X += step;
                    ((Enemy) dynamic).ChangeFrame();
                }
            }

            foreach (Dynamic dynamic in m_world.m_entities) {
                if (dynamic is Enemy) {
                    if (dynamic.m_pos.X + 28 > rightside) {
                        IsGoingLeft = true;
                        changedirection = true;
                    }
                    if (dynamic.m_pos.X < leftside) {
                        IsGoingLeft = false;
                        changedirection = true;
                    }
                }
            }

            if (changedirection) {
                foreach (Dynamic dynamic in m_world.m_entities) {
                    if (dynamic is Enemy) {
                        dynamic.m_pos.Y = dynamic.m_pos.Y + 32;
                        if (IsGoingLeft)
                            dynamic.m_pos.X -= step;
                        else
                            dynamic.m_pos.X += step;
                    }
                }
            }
            count += 1;
        }

        public void Step() {
            Move();
        }
    }
}

﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders {
    public class BulletPlayer : Dynamic {
        const float m_bulletSpeed = 500.0f;

        public static int m_bulletCount = 0;

        public BulletPlayer(World world, Vector2 pos, Vector2 size, Texture2D tex)
            : base(world, pos, size, tex) {
            m_bulletCount++;
        }

        public override bool Update(GameTime gameTime) {
            m_pos.Y -= (float)gameTime.ElapsedGameTime.TotalSeconds * m_bulletSpeed;

            if (m_pos.Y < 0.0f) {
                m_bulletCount--;
                return false;
            }

            Vector2 myMin = m_pos - m_size * 0.5f;
            Vector2 myMax = m_pos + m_size * 0.5f;

            foreach (Entity e in m_world.m_entities) {
                if (e is Enemy) {
                    if (((Dynamic)e).TestOverlapRect(myMin, myMax)) {
                        ((Enemy)e).Kill();

                        m_bulletCount--;
                        return false;
                    }
                } else if (e is SpaceShip) {
                    if (((Dynamic)e).TestOverlapRect(myMin, myMax)) {
                        ((SpaceShip)e).WasKilled();

                        m_bulletCount--;
                        return false;
                    }
                } if (e is Barrier) {
                    if (((Dynamic)e).TestOverlapRect(myMin, myMax)) {
                        ((Barrier)e).BarrierHit();
                        
                        m_bulletCount--;
                        return false;
                    }
                }
            }

            return base.Update(gameTime);
        }
    }
}
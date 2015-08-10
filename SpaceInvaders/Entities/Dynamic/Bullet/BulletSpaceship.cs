using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    public class BulletSpaceship : Dynamic
    {
        const float m_bulletSpeed = 200.0f;
        public double m_bulletTime = 0.0f;

        public BulletSpaceship(World world, Vector2 pos, Vector2 size, Texture2D tex)
            : base(world, pos, size, tex)
        {
        }

        public override bool Update(GameTime gameTime)
        {
            m_pos.Y += (float)gameTime.ElapsedGameTime.TotalSeconds * m_bulletSpeed;

            if (m_pos.Y < 0.0f)
            {
                return false;
            }
                
   
            Vector2 myMin = m_pos - m_size * 0.5f;
            Vector2 myMax = m_pos + m_size * 0.5f;
            
            foreach (Entity e in m_world.m_entities)
            {
                if (e is Player)
                {
                    if (((Dynamic)e).TestOverlapRect(myMin, myMax))
                    {
                        ((Player)e).HitPlayer();
                        return false;
                    }
                }
            }

            return base.Update(gameTime);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SpaceInvaders
{
    class Barrier : Dynamic
    {
        //bool m_isAlive = true;

        public Barrier(World world, Vector2 pos, Vector2 size, Texture2D tex)
            : base(world, pos, size, tex) { }

        public override bool Update(GameTime gameTime)
        {
            //if (!m_isAlive)
            //    return false;

            //if (isVisible)
            //{

            //}

            return base.Update(gameTime);
        }

        //public void WasKilled() { m_isAlive = false; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //m_world.m_spriteBatch.Draw(m_world.m_texBarrier, m_pos, Color.White);
            m_world.m_spriteBatch.Draw(m_world.m_texBarrier, m_pos, null, Color.White, 0.0f, new Vector2((float)m_world.m_texBarrier.Width, (float)m_world.m_texBarrier.Height) * 0.5f, Vector2.One, SpriteEffects.None, 0.0f);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SpaceInvaders
{
    public class Dynamic : Entity
    {
        public Vector2   m_pos;  //center position of the entity
        public Vector2   m_size;
        public Texture2D m_tex;

        public Color m_color = Color.White;
        public bool isBulletVisible = false;

        public Dynamic(World world, Vector2 pos, Vector2 size, Texture2D tex) : base(world)
        {
            m_pos  = pos;
            m_size = size;
            m_tex  = tex;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {

            if (isVisible)
            {
                Vector2 texSize = new Vector2(m_tex.Width, m_tex.Height);

                Vector2 scale = m_size / texSize;

                Vector2 origin = texSize * 0.5f;

                spriteBatch.Draw(m_tex, m_pos, null, null, origin, 0.0f, scale, m_color);
            }
        }

        public bool TestOverlapRect(Vector2 otherMin, Vector2 otherMax)
        {
            bool overlap;

            Vector2 myMin = m_pos - m_size * 0.5f;
            Vector2 myMax = m_pos + m_size * 0.5f;

            overlap = ((myMin.X < otherMax.X) && (myMax.X > otherMin.X) && (myMin.Y < otherMax.Y) && (myMax.Y > otherMin.Y));

            return isVisible && overlap;
        }
    }
}

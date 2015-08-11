using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public enum EnemyState { Null, Alive, Dying };

namespace SpaceInvaders
{
    class Enemy : Dynamic {
        bool m_isAlive = true, m_firstFrame = true;

        Rectangle[] m_enemyAlive, m_enemyDying;

        public Enemy(World world, Vector2 pos, Vector2 size, Texture2D tex, Rectangle[] enemyAlive, Rectangle[] enemyDying)
            : base(world, pos, size, tex) {
            
            isVisible = true;

            m_enemyAlive = enemyAlive;
            m_enemyDying = enemyDying;
        }

        public void ChangeFrame() {
            m_firstFrame = !m_firstFrame;
        }

        public override bool Update(GameTime gameTime) {
            if (m_isAlive) {

            }

            return base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            Rectangle p = (m_firstFrame ? m_enemyAlive[0] : m_enemyAlive[1]);
            Rectangle d = m_enemyDying[0];

            if (m_isAlive) {
                m_world.m_spriteBatch.Draw(m_tex, m_pos, p, Color.White, 0.0f, new Vector2(p.Width, p.Height) * 0.5f,
                    Vector2.One, SpriteEffects.None, 0.0f);
            } else {
                m_world.m_spriteBatch.Draw(m_tex, m_pos, d, Color.White, 0.0f, new Vector2(d.Width, d.Height) * 0.5f,
                    Vector2.One, SpriteEffects.None, 0.0f);
            }
        }



    }
}

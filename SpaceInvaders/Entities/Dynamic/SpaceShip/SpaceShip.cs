using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders {
    public class SpaceShip : Dynamic {
        //bool m_isAlive = true;
        public bool m_inverse;
        BulletSpaceship bulletspaceship;
        Random rndBullet;

        public int points = 100;

        //const float m_playerSpeed = 200.0f;

        bool m_isAlive = true;

        public SpaceShip(World world, Vector2 pos, Vector2 size, Texture2D tex, bool inverse = false)
            : base(world, pos, size, tex) {
            m_inverse = inverse;
            rndBullet = new Random();
        }

        //Rectangle m_recSpaceship; 

        public override bool Update(GameTime gameTime) {
            if (!m_isAlive)
                return false;

            if (isVisible) {
                if (rndBullet.NextDouble() >= 0.9d && (m_pos.X >= 75 && m_pos.X <= 725)) {
                    if (bulletspaceship == null || !bulletspaceship.isVisible) {
                        bulletspaceship = new BulletSpaceship(m_world, this.m_pos + new Vector2(0.0f, 20.0f), new Vector2(2, 6), m_world.Content.Load<Texture2D>("bullet"));
                        m_world.m_entities.Add(bulletspaceship);
                    }
                }
            }

            //if (Keyboard.GetState().IsKeyDown(Keys.Z) && !m_world.m_prevKeyboardState.IsKeyDown(Keys.Z))
            //{
            //    m_world.m_entities.Add(new BulletSpaceship(m_world, m_pos, new Vector2(2.0f, 6.0f), m_world.m_texplayerBullet));
            //}

            return base.Update(gameTime);
        }

        public void WasKilled() { 
            m_isAlive = false; 
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            //m_world.m_spriteBatch.Draw(m_world.m_texSpaceship, new Vector2(m_world.m_screenRes.X * 0.5f, m_world.m_screenRes.Y * 0.5f), Color.White);
            m_world.m_spriteBatch.Draw(m_world.m_texSpaceship, m_pos, null, Color.White, 0.0f, new Vector2((float)m_world.m_texSpaceship.Width, (float)m_world.m_texSpaceship.Height) * 0.5f, Vector2.One, SpriteEffects.None, 0.0f);
        }
    }
}
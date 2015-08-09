using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    public class Player : Dynamic
    {
        bool m_isAlive = true;

        const float m_playerSpeed = 200.0f;


        //Bullet bullet = null;
        public Player(World world, Vector2 pos, Vector2 size, Texture2D tex)
            : base(world, pos, size, tex)
        {
            isVisible = true;
        }

        public Rectangle[] m_playerPlaying = new Rectangle[]
          {
                    new Rectangle ( 0, 0, 34, 21)
          };

        public Rectangle[] m_playerDayingAnim = new Rectangle[]
        {
                    new Rectangle ( 0, 20, 34, 21),
                    new Rectangle ( 0, 40, 34, 21)
        };

        public override bool Update(GameTime gameTime)
        {
            if (m_isAlive)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    m_pos.X += (float)gameTime.ElapsedGameTime.TotalSeconds * m_playerSpeed;

                    m_pos.X = Math.Min(m_pos.X, m_world.m_screenRes.X - 80);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    m_pos.X -= (float)gameTime.ElapsedGameTime.TotalSeconds * m_playerSpeed;

                    m_pos.X = Math.Max(m_pos.X, 80);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Space) && !m_world.m_prevKeyboardState.IsKeyDown(Keys.Space) )
                {
                    if (BulletPlayer.m_bulletCount == 0)
                        m_world.m_entities.Add(new BulletPlayer(m_world, m_pos, new Vector2(16.0f, 16.0f), m_world.m_texPlayer));

                    /*
                    bool found = false;

                    foreach (Entity e in m_world.m_entities)
                    {
                        if (e is BulletPlayer)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                        m_world.m_entities.Add( new BulletPlayer(m_world, m_pos, new Vector2(16.0f, 16.0f), m_world.m_texPlayer) );
                    */
                }

            }

            return base.Update(gameTime);
        }

        public void HitPlayer() { m_isAlive = false; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle p = m_playerPlaying[(int)m_world.m_playerFrame % m_playerPlaying.Length];
            Rectangle d = m_playerDayingAnim[(int)m_world.m_playerFrame % m_playerDayingAnim.Length];

            //m_spriteBatch.DrawString(m_font, "PLAYING", new Vector2(200.0f, 100.0f), Color.White);
            //m_spriteBatch.Draw(m_texPlayer, new Vector2(300.0f, 100.0f), Color.White);

            if (!m_isAlive)
                m_world.m_spriteBatch.Draw(m_world.m_texPlayer, m_pos, d, Color.White, 0.0f, new Vector2(d.Width, d.Height) * 0.5f,
                    Vector2.One, SpriteEffects.None, 0.0f);
            else
                m_world.m_spriteBatch.Draw(m_world.m_texPlayer, m_pos, p, Color.White, 0.0f, new Vector2(p.Width, p.Height) * 0.5f,
                    Vector2.One, SpriteEffects.None, 0.0f);

            {
                Rectangle r = m_playerDayingAnim[(int)m_world.m_playerFrame % m_playerDayingAnim.Length];

                m_world.m_spriteBatch.DrawString(m_world.m_font, "X:" + r.X + " Y:" + r.Y, new Vector2(10, 10), Color.White);
            }
        }
    }
}

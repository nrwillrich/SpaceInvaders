using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    public class SpaceShip : Dynamic
    {
        //bool m_isAlive = true;

        const float m_playerSpeed = 200.0f;

        bool m_isAlive = true;

        //Bullet bullet = null;
        public SpaceShip(World world, Vector2 pos, Vector2 size, Texture2D tex)
            : base(world, pos, size, tex)
        {
            isVisible = true;
        }

        //Rectangle m_recSpaceship; 

        public override bool Update(GameTime gameTime)
        {
            if (!m_isAlive)
                return false;

            if (Keyboard.GetState().IsKeyDown(Keys.Z) && !m_world.m_prevKeyboardState.IsKeyDown(Keys.Z))
                {
                    m_world.m_entities.Add(new BulletSpaceship(m_world, m_pos, new Vector2(16.0f, 16.0f), m_world.m_texSpaceship));
                }
            
            //{
            //    if (Keyboard.GetState().IsKeyDown(Keys.Right))
            //    {
            //        m_pos.X += (float)gameTime.ElapsedGameTime.TotalSeconds * m_playerSpeed;

            //        m_pos.X = Math.Min(m_pos.X, m_world.m_screenRes.X - 80);
            //    }

            //    if (Keyboard.GetState().IsKeyDown(Keys.Left))
            //    {
            //        m_pos.X -= (float)gameTime.ElapsedGameTime.TotalSeconds * m_playerSpeed;

            //        m_pos.X = Math.Max(m_pos.X, 80);
            //    }

            //    if (Keyboard.GetState().IsKeyDown(Keys.Space) && !m_world.m_prevKeyboardState.IsKeyDown(Keys.Space) )
            //    {
            //        m_world.m_entities.Add( new BulletPlayer(m_world, m_pos, new Vector2(16.0f, 16.0f), m_world.m_texPlayer) );
            //    }

            //}
               

            return base.Update(gameTime);
        }

        public void WasKilled() { m_isAlive = false;  }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //m_world.m_spriteBatch.Draw(m_world.m_texSpaceship, new Vector2(m_world.m_screenRes.X * 0.5f, m_world.m_screenRes.Y * 0.5f), Color.White);
            m_world.m_spriteBatch.Draw(m_world.m_texSpaceship, m_pos, null, Color.White, 0.0f, new Vector2((float)m_world.m_texSpaceship.Width, (float)m_world.m_texSpaceship.Height) * 0.5f, Vector2.One, SpriteEffects.None, 0.0f);
           
        
            //if (!m_isAlive)
            //    m_world.m_spriteBatch.Draw(m_world.m_texPlayer, m_pos, d, Color.White, 0.0f, new Vector2(d.Width, d.Height) * 0.5f,
            //        Vector2.One, SpriteEffects.None, 0.0f);
            //else
            //    m_world.m_spriteBatch.Draw(m_world.m_texPlayer, m_pos, p, Color.White, 0.0f, new Vector2(p.Width, p.Height) * 0.5f,
            //        Vector2.One, SpriteEffects.None, 0.0f);


        }

    }
}

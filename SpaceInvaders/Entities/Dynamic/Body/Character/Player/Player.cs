using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Player : Character
    {
        //Bullet bullet = null;

        public Player(World world, Vector2 pos, Vector2 size, Texture2D tex,
                   float maxVel = 200.0f, float accel = 1000.0f, float friction = 5.0f)
            : base(world, pos, size, tex, maxVel, accel, friction)
        { }

        //public void playerAnim()
        //{
        //    Rectangle[] m_playerAnim = new Rectangle[]
        //    {
        //        new Rectangle (0, 0, 34 ,21)
        //    };
        //}

        public override void Update(GameTime gameTime)
        {

            Moving();
            //m_dir = Vector2.Zero;

            //if (Keyboard.GetState().IsKeyDown(Keys.Right))
            //{
            //    if (m_pos.X < m_world.m_screenRes.X - 80)
            //        m_dir.X += 1.0f;
            //}
            //if (Keyboard.GetState().IsKeyDown(Keys.Left))
            //{
            //    if (m_pos.X > 80)
            //        m_dir.X -= 1.0f;
            //}
                        
            //if (Keyboard.GetState().IsKeyDown(Keys.Space) && !m_world.m_prevKeyboardState.IsKeyDown(Keys.Space))
            //{
            //    if (bullet == null || (bullet.isBulletVisible == "No"))
            //    {
            //        bullet = new Bullet(m_world, m_pos + new Vector2(0, -20.0f), new Vector2(4, 8), m_world.m_texBullet);
            //        bullet.isBulletVisible = "Yes";
            //        m_world.m_entities.Add(bullet);
            //    }

            //    //if (m_dir.Y - m_world.m_texInvader1.Height < 0)
            //    //    isBulletVisible = "No";
            //}

            base.Update(gameTime);
        }

        public void Moving()
        {
            m_dir = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (m_pos.X < m_world.m_screenRes.X - 80)
                    m_dir.X += 1.0f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (m_pos.X > 80)
                    m_dir.X -= 1.0f;
            }
        }
    }
}

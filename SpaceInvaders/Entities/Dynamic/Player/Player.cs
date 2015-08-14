using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum PlayerState { Null, Alive, Dying };

namespace SpaceInvaders {
    public class Player : Dynamic {
        bool m_isAlive = true;

        const float m_playerSpeed = 200.0f;

        float m_dyingTimer = 750.0f;
        
        PlayerState m_state = PlayerState.Alive;

        public Player(World world, Vector2 pos, Vector2 size, Texture2D tex)
            : base(world, pos, size, tex) {
            isVisible = true;
        }

        public Rectangle[] m_playerPlaying = new Rectangle[]
        {
            new Rectangle ( 0, 0, 34, 21)
        };

        public Rectangle[] m_playerDyingAnim = new Rectangle[]
        {
            new Rectangle ( 0, 20, 34, 21),
            new Rectangle ( 0, 40, 34, 21)
        };

        public void EnterState(PlayerState newState) {
            LeaveState();

            m_state = newState;

            switch (m_state) {
                case PlayerState.Alive: {

                    } break;

                case PlayerState.Dying: {

                    } break;
            }
        }

        public void LeaveState() {
            switch (m_state) {
                case PlayerState.Alive: {
                    } break;

                case PlayerState.Dying: {
                    } break;
            }
        }

        public void UpdateState(GameTime gameTime) {
            switch (m_state) {
                case PlayerState.Alive: {
                    if (m_isAlive) {
                        //Movimentação do Player----------------------------------------------------
                        if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                            m_pos.X += (float)gameTime.ElapsedGameTime.TotalSeconds * m_playerSpeed;

                            m_pos.X = Math.Min(m_pos.X, m_world.m_screenRes.X - 70);
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                            m_pos.X -= (float)gameTime.ElapsedGameTime.TotalSeconds * m_playerSpeed;

                            m_pos.X = Math.Max(m_pos.X, 70);
                        }
                        //Tiro do Player------------------------------------------------------------------------------------------------------
                        if (Keyboard.GetState().IsKeyDown(Keys.Space) && !m_world.m_prevKeyboardState.IsKeyDown(Keys.Space)) {
                            if (BulletPlayer.m_bulletCount == 0)
                                m_world.m_entities.Add(new BulletPlayer(m_world, m_pos, new Vector2(2.0f, 6.0f), m_world.m_texplayerBullet));
                        }

                    }
                } break;

                case PlayerState.Dying: {
                        m_dyingTimer -= gameTime.ElapsedGameTime.Milliseconds;
                        if (m_dyingTimer < 0) {
                            m_isAlive = false;
                        }
                    } break;
            }
        }

        public void DrawState(GameTime gameTime) {
            switch (m_state) {
                case PlayerState.Alive: {
                    Rectangle p = m_playerPlaying[(int)m_world.m_playerFrame % m_playerPlaying.Length];
                    m_world.m_spriteBatch.Draw(m_world.m_texPlayer, m_pos, p, Color.White, 0.0f, new Vector2(p.Width, p.Height) * 0.5f,
                        Vector2.One, SpriteEffects.None, 0.0f);
                } break;

                case PlayerState.Dying: {
                    if (m_isAlive) {
                        Rectangle d = m_playerDyingAnim[(int)m_world.m_playerFrame % m_playerDyingAnim.Length];
                        m_world.m_spriteBatch.Draw(m_world.m_texPlayer, m_pos, d, Color.White, 0.0f, new Vector2(d.Width, d.Height) * 0.5f,
                            Vector2.One, SpriteEffects.None, 0.0f);
                    }
                } break;
            }
        }

        public override bool Update(GameTime gameTime) {
            UpdateState(gameTime);

            base.Update(gameTime);

            return m_isAlive;
        }

        public void HitPlayer() {
            EnterState(PlayerState.Dying);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            DrawState(gameTime);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public enum EnemyState { Null, Alive, Dying };

namespace SpaceInvaders
{
    class Enemy : Dynamic {
        public bool m_isAlive = true, m_firstFrame = true;

        EnemyState m_state = EnemyState.Alive;

        Rectangle[] m_enemyAlive, m_enemyDying;

        float m_dyingTimer = 250.0f;

        public Enemy(World world, Vector2 pos, Vector2 size, Texture2D tex, Rectangle[] enemyAlive, Rectangle[] enemyDying)
            : base(world, pos, size, tex) {
            
            isVisible = true;

            m_enemyAlive = enemyAlive;
            m_enemyDying = enemyDying;
        }

        public void EnterState(EnemyState newState) {
            LeaveState();

            m_state = newState;

            switch (m_state) {
                case EnemyState.Alive: {

                } break;

                case EnemyState.Dying: {

                } break;
            }
        }

        public void LeaveState() {
            switch (m_state) {
                case EnemyState.Alive: {
                } break;

                case EnemyState.Dying: {
                } break;
            }
        }

        public void UpdateState(GameTime gameTime) {
            switch (m_state) {
                case EnemyState.Alive: {
                } break;

                case EnemyState.Dying: {
                    m_dyingTimer -= gameTime.ElapsedGameTime.Milliseconds;
                    if (m_dyingTimer < 0) {
                        m_isAlive = false;
                    }
                } break;
            }
        }

        public void DrawState(GameTime gameTime) {
            switch (m_state) {
                case EnemyState.Alive: {
                    Rectangle p = (m_firstFrame ? m_enemyAlive[0] : m_enemyAlive[1]);
                    m_world.m_spriteBatch.Draw(m_tex, m_pos, p, Color.White, 0.0f, new Vector2(p.Width, p.Height) * 0.5f,
                        Vector2.One, SpriteEffects.None, 0.0f);
                } break;

                case EnemyState.Dying: {
                    if (m_isAlive) {
                        Rectangle d = m_enemyDying[0];
                        m_world.m_spriteBatch.Draw(m_tex, m_pos, d, Color.White, 0.0f, new Vector2(d.Width, d.Height) * 0.5f,
                            Vector2.One, SpriteEffects.None, 0.0f);
                    }
                } break;
            }
        }

        public void Kill() {
            EnterState(EnemyState.Dying);
        }

        public void ChangeFrame() {
            m_firstFrame = !m_firstFrame;
        }

        public override bool Update(GameTime gameTime) {
            UpdateState(gameTime);

            base.Update(gameTime);

            return m_isAlive; // se false, enemy morto, então ele é removido do m_entities lá no World
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            DrawState(gameTime);
        }
    }
}

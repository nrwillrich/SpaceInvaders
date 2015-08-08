using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public enum EnemyState { Null, Alive, Dying };

namespace SpaceInvaders
{
    class Enemy : Dynamic
    {
        int texPos1, texPos2, texPos3;
        
        public bool firstFrame = true;

        int m_dyingTimer;

        public EnemyState m_state = EnemyState.Alive;

        public Enemy(World world, Vector2 pos, Vector2 size, int texPos1, int texPos2, int texPos3) :
                base(world, pos, size, null) {

            this.texPos1 = texPos1;
            this.texPos2 = texPos2;
            this.texPos3 = texPos3;
        }

        public void EnterState(EnemyState newState)
        {
            LeaveState();

            m_state = newState;

            switch (m_state)
            {
                case EnemyState.Alive:
                    {
                    }
                    break;

                case EnemyState.Dying:
                    {
                        m_dyingTimer = 700; // milliseconds
                        m_tex = m_world.getSprite(texPos3);
                    }
                    break;
            }
        }

        public void LeaveState()
        {
            switch (m_state)
            {
                case EnemyState.Alive:
                    {
                    }
                    break;

                case EnemyState.Dying:
                    {
                    }
                    break;
            }
        }

        public void UpdateState(GameTime gameTime)
        {
            switch (m_state)
            {
                case EnemyState.Alive:
                    {
                        Vector2 myMin = m_pos - m_size * 0.5f;
                        Vector2 myMax = m_pos + m_size * 0.5f;

                        foreach (Entity e in m_world.m_entities)
                        {
                            if ((e is Dynamic) && (e != this)) // PlayerBullet && !this
                            {
                                if (((Dynamic)e).TestOverlapRect(myMin, myMax))
                                {
                                    EnterState(EnemyState.Dying);
                                    break;
                                }
                            }
                        }

                        if (firstFrame)
                        {
                            m_tex = m_world.getSprite(texPos1);
                        }
                        else
                        {
                            m_tex = m_world.getSprite(texPos2);
                        }
                    }
                    break;

                case EnemyState.Dying:
                    {
                        m_dyingTimer -= gameTime.ElapsedGameTime.Milliseconds; // demora um pouco com a textura de "destruído"
                        if (m_dyingTimer < 0.0) {
                            isVisible = false;
                        }
                    }
                    break;
            }
        }

        public void DrawState(GameTime gameTime)
        {
            switch (m_state)
            {
                case EnemyState.Alive:
                    {
                    }
                    break;

                case EnemyState.Dying:
                    {
                    }
                    break;
            }
        }

        public void ChangeFrame() {
            firstFrame = !firstFrame;
        }

        public override void Update(GameTime gameTime)
        {
            UpdateState(gameTime);

            base.Update(gameTime);
        }
    }
}

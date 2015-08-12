using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public enum GameState { Null, MainMenu, Playing, GameOver };

namespace SpaceInvaders
{
    public class World : Game
    {
        GraphicsDeviceManager m_graphics;
        public SpriteBatch m_spriteBatch;

        public List<Entity> m_entities = new List<Entity>();

        public KeyboardState m_prevKeyboardState;
        public MouseState m_prevMouseState;

        public Vector2 m_screenRes = new Vector2(448.0f, 512.0f);

        public SpriteFont m_font;

        public Texture2D m_texInvadersSheet;

        float m_frameNum = 0.0f;

        float m_stepInterval = 800.0f;

        int m_sheetColumns = 2;
        int m_sheetLines = 4;

        int m_spriteWidth;
        int m_spriteHeight;

        public Texture2D m_texPlayer;
        public Texture2D m_texSpaceship;
        public Texture2D m_texplayerBullet;

        int highScore = 0, p1Score = 0;

        GameState m_state = GameState.MainMenu;

        Enemies m_enemies;

        public Rectangle[] m_playerPlaying = new Rectangle[]
        {
            new Rectangle ( 0, 0, 34, 21)
        };

        public Rectangle[] m_playerDayingAnim = new Rectangle[]
        {
            new Rectangle ( 0, 20, 34, 21),
            new Rectangle ( 0, 40, 34, 21)
        };

        public float m_playerFrame = 0.0f;

        public void EnterState(GameState newState)
        {
            LeaveState();

            m_state = newState;

            switch (m_state)
            {
                case GameState.MainMenu:
                    {
                    }
                    break;

                case GameState.Playing:
                    {
                        m_entities.Add(new Player(this, new Vector2(m_screenRes.X * 0.5f, m_screenRes.Y - 80), new Vector2(32, 32), m_texPlayer));
                        
                        m_enemies = new Enemies(this);
                        
                        // m_entities.Add(new Enemy(this, new Vector2(30, 25), new Vector2(28, 20), 0, 1, 6));
                        
                        // m_entities.Add(new SpaceShip(this, new Vector2(m_screenRes.X * 0.5f, m_screenRes.Y * 0.5f), new Vector2(32, 32), m_texSpaceship));
                    }
                    break;

                case GameState.GameOver:
                    {
                    }
                    break;
            }
        }

        public void LeaveState()
        {
            switch (m_state)
            {
                case GameState.MainMenu:
                    {
                    }
                    break;

                case GameState.Playing:
                    {
                    }
                    break;

                case GameState.GameOver:
                    {
                    }
                    break;
            }
        }

        public void UpdateState(GameTime gameTime)
        {
            switch (m_state)
            {
                case GameState.MainMenu:
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter) &&
                            !m_prevKeyboardState.IsKeyDown(Keys.Enter))
                        {
                            EnterState(GameState.Playing);
                        }
                    }
                    break;

                case GameState.Playing:
                    {
                        m_stepInterval -= gameTime.ElapsedGameTime.Milliseconds;
                        if (m_stepInterval < 0.0f) {
                            m_enemies.Step();
                            SetIntervalStep();
                        }
                    }
                    break;

                case GameState.GameOver:
                    {
                        //if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                        //   m_prevMouseState.LeftButton != ButtonState.Pressed)
                        //{
                        //    EnterState(GameState.MainMenu);
                        //}
                    }
                    break;
            }
        }

        public void DrawState(GameTime gameTime)
        {
            switch (m_state)
            {
                case GameState.MainMenu:
                    {
                        MainMenu(gameTime);
                    }
                    break;

                case GameState.Playing:
                    {
                        m_spriteBatch.DrawString(m_font, "PLAYING", new Vector2(200.0f, 300.0f), Color.White);
                        //m_spriteBatch.Draw(getSprite(6), new Rectangle(new Point(50, 50), new Point(28, 20)), Color.White);
                    }
                    break;

                case GameState.GameOver:
                    {
                        //DrawBoard();

                        //m_spriteBatch.DrawString(m_font, m_board.GetBoardState('X').ToString(), new Vector2(100.0f, 300.0f), Color.White);
                    }
                    break;
            }
        }
        
        //float lengthPlay = 0.0f;

        private void SetIntervalStep() {
            int alive = m_enemies.GetAliveCount();
            m_stepInterval = 800.0f;

            if (alive <= 30) {
                m_stepInterval = 650.0f;
            } else if (alive <= 20) {
                m_stepInterval = 450.0f;
            } else if (alive <= 10) {
                m_stepInterval = 350.0f;
            } else if (alive <= 5) {
                m_stepInterval = 150.0f;
            }
        }

        private void MainMenu(GameTime gameTime)
        {
            // https://www.youtube.com/watch?v=axlx3o0codc
            // https://danieltian.wordpress.com/2008/12/24/xna-tutorial-typewriter-text-box-with-proper-word-wrapping-part-3/

            //String strPlay = "PLAY";

            m_spriteBatch.DrawString(m_font, "SCORE< 1 >    HI-SCORE    SCORE< 2 >", new Vector2(70.0f, 75.0F), Color.White);
            m_spriteBatch.DrawString(m_font, "  " + p1Score.ToString("D4") + "     " + highScore.ToString("D4") + "      0000", new Vector2(70.0f, 95.0F), Color.White);
            
            //m_spriteBatch.DrawString(m_font, strPlay.Substring(0, (int) lengthPlay), new Vector2(90.0f, 135.0F), Color.White);

            //if ((int ) lengthPlay < strPlay.Length)
            //{
            //    lengthPlay = lengthPlay + ((float) gameTime.ElapsedGameTime.TotalSeconds * 10);
            //}
            
            m_spriteBatch.DrawString(m_font, "PLAY", new Vector2(200.0f, 135.0F), Color.White);
            m_spriteBatch.DrawString(m_font, "SPACE INVADERS", new Vector2(150.0f, 175.0f), Color.White);
            m_spriteBatch.DrawString(m_font, "   *SCORE ADVANCE TABLE*", new Vector2(107.0f, 225.0f), Color.White);
            //m_spriteBatch.DrawString(m_font, "= ? mystery", new Vector2(190.0f, 190.0f), Color.White);
            //m_spriteBatch.DrawString(m_font, "= 30 points", new Vector2(190.0f, 230.0f), Color.White);
            //m_spriteBatch.DrawString(m_font, "= 20 points", new Vector2(190.0f, 270.0f), Color.White);
            //m_spriteBatch.DrawString(m_font, "= 10 points", new Vector2(190.0f, 310.0f), Color.White);
            //m_spriteBatch.DrawString(m_font, "Press Enter to Start", new Vector2(45.0f, 380.0f), Color.White);
            m_spriteBatch.DrawString(m_font, "   *TAITO CORPORATION*", new Vector2(130.0f, 425.0f), Color.Green);
        }

        public World()
        {
            m_graphics = new GraphicsDeviceManager(this);

            m_graphics.PreferredBackBufferWidth = (int) m_screenRes.X;
            m_graphics.PreferredBackBufferHeight = (int) m_screenRes.Y;

            m_graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        public Texture2D getSprite(int pos)
        {
            int frameInt = pos; // (int)m_frameNum;
            int sx = frameInt % m_sheetColumns;
            int sy = frameInt / m_sheetColumns;

            Rectangle sourceRectangle = new Rectangle(sx * m_spriteWidth, sy * m_spriteHeight, 28, 20);

            Texture2D cropTexture = new Texture2D(GraphicsDevice, sourceRectangle.Width, sourceRectangle.Height);
            Color[] data = new Color[sourceRectangle.Width * sourceRectangle.Height];
            m_texInvadersSheet.GetData(0, sourceRectangle, data, 0, data.Length);
            cropTexture.SetData(data);

            return cropTexture;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            
            m_texInvadersSheet = Content.Load<Texture2D>("InvadersSheet");

            m_texPlayer = Content.Load<Texture2D>("PlayerSheet");
            m_texSpaceship = Content.Load<Texture2D>("Spaceship");
            m_texplayerBullet = Content.Load<Texture2D>("Bullet");

            m_spriteWidth = m_texInvadersSheet.Width / m_sheetColumns;
            m_spriteHeight = m_texInvadersSheet.Height / m_sheetLines;

            m_font = Content.Load<SpriteFont>("Arial");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            List<Entity> tmp = new List<Entity>(m_entities);
            foreach (Entity e in tmp)
                if (e.Update(gameTime) == false)
                    m_entities.Remove(e);

            UpdateState(gameTime);

            base.Update(gameTime);

            m_prevKeyboardState = Keyboard.GetState();

            m_playerFrame += (float)gameTime.ElapsedGameTime.TotalSeconds * 8;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            m_spriteBatch.Begin(SpriteSortMode.BackToFront,
                                BlendState.AlphaBlend,
                                SamplerState.PointClamp);

            foreach (Entity e in m_entities)
                e.Draw(gameTime, m_spriteBatch);
                        
            DrawState(gameTime);

            m_spriteBatch.End();

            base.Draw(gameTime);
        }
        
    }
}

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

        public Texture2D m_texPlayer;
        public Texture2D m_texSpaceship;
        public Texture2D m_texplayerBullet;
        public Texture2D m_texBarrier;
        public Texture2D m_pixelBarrier;
        public Dynamic m_spaceShip;

        //public Texture2D m_texNPC;
        //public Texture2D m_texFood;
        
        int highScore = 0, p1Score = 0;

        public bool m_inverse = false;

        public double m_timeStart = 0.0f;
                
        GameState m_state = GameState.MainMenu;

        public Rectangle[] m_playerPlaying = new Rectangle[]
          {
                    new Rectangle ( 0, 0, 34, 21)
          };

        public Rectangle[] m_playerDayingAnim = new Rectangle[]
        {
                    new Rectangle ( 0, 20, 34, 21),
                    new Rectangle ( 0, 40, 34, 21)
        };

        public Vector2[] m_barrierPositions = new Vector2[]
        {
            //Barreira é 8 x 6 casas (42 x 30 pixels)
                                                      new Vector2(12,  0), new Vector2(18,  0), new Vector2(24,  0), new Vector2(30,  0),

                                 new Vector2( 6,  6), new Vector2(12,  6), new Vector2(18,  6), new Vector2(24,  6), new Vector2(30,  6), new Vector2(36,  6),

            new Vector2( 0, 12), new Vector2( 6, 12), new Vector2(12, 12), new Vector2(18, 12), new Vector2(24, 12), new Vector2(30, 12), new Vector2(36, 12), new Vector2(42, 12),

            new Vector2( 0, 18), new Vector2( 6, 18), new Vector2(12, 18),                                           new Vector2(30, 18), new Vector2(36, 18), new Vector2(42, 18),
                                                                                                                                      
            new Vector2( 0, 24), new Vector2( 6, 24),                                                                                     new Vector2(36, 24), new Vector2(42, 24),                                 

            new Vector2( 0, 30), new Vector2( 6, 30),                                                                                     new Vector2(36, 30), new Vector2(42, 30)

             //new Vector2(8,  0),
             //new Vector2(16, 0),
            
             //new Vector2(0, 8),
             //new Vector2(8,  8),
             //new Vector2(16, 8),
             //new Vector2(24, 8),
        };

        public float m_playerFrame = 0.0f;
        
        void CreateBarrier(float x, float y)
        {
            foreach (Vector2 v in m_barrierPositions)
                m_entities.Add(new Barrier(this, new Vector2(x + v.X, y + v.Y), new Vector2(6, 6), m_pixelBarrier));

            //m_entities.Add(new Barrier(this, new Vector2(x + 8,  y), new Vector2(8, 8), m_texBarrier));
            //m_entities.Add(new Barrier(this, new Vector2(x + 16, y), new Vector2(8, 8), m_texBarrier));

            //m_entities.Add(new Barrier(this, new Vector2(x,      y + 8), new Vector2(8, 8), m_texBarrier));
            //m_entities.Add(new Barrier(this, new Vector2(x + 8,  y + 8), new Vector2(8, 8), m_texBarrier));
            //m_entities.Add(new Barrier(this, new Vector2(x + 16, y + 8), new Vector2(8, 8), m_texBarrier));
            //m_entities.Add(new Barrier(this, new Vector2(x + 24, y + 8), new Vector2(8, 8), m_texBarrier));
        }
        
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

                        CreateBarrier(83.0f, 380.0f);

                        CreateBarrier(163.0f, 380.0f);

                        CreateBarrier(243.0f, 380.0f);
                    
                        CreateBarrier(323.0f, 380.0f);

                        /*

                        m_entities.Add(new Barrier(this, new Vector2(105, 380), new Vector2(44, 32), m_texBarrier));
                                                                         
                        m_entities.Add(new Barrier(this, new Vector2(185, 380), new Vector2(44, 32), m_texBarrier));
                                                                          
                        m_entities.Add(new Barrier(this, new Vector2(265, 380), new Vector2(44, 32), m_texBarrier));
                                                                         
                        m_entities.Add(new Barrier(this, new Vector2(345, 380), new Vector2(44, 32), m_texBarrier));
                         * */
                                                
                        m_spaceShip = new SpaceShip(this, new Vector2(-32f, m_screenRes.Y * 0.16f), new Vector2(32, 32), m_texSpaceship);
                        m_spaceShip.isVisible = true;
                        m_entities.Add(m_spaceShip);

                        m_timeStart = 35.0f;
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
                        
                        //    else if (Keyboard.GetState().IsKeyDown(Keys.D2) &&
                        //             !m_prevKeyboardState.IsKeyDown(Keys.D2))
                        //    {
                        //        EnterState(GameState.MachineTurn);
                        //    }
                    }
                    break;

                case GameState.Playing:
                    {
                        m_timeStart -= gameTime.ElapsedGameTime.TotalSeconds;

                        if (m_timeStart <= 0.0f)
                        {
                            Random rnd = new Random();
                            int spaceshipTime = rnd.Next(30, 40);
                            
                            //if (!m_spaceShip.isVisible) 
                            //    m_spaceShip.isVisible = true;

                            if (m_inverse)
                            {
                                if (m_spaceShip.m_pos.X > -m_spaceShip.m_size.X)
                                {
                                    m_spaceShip.m_pos -= (new Vector2(1.5f, 0));
                                }

                                else
                                {
                                    if (m_timeStart <= 0.0f)
                                    {
                                        //m_timeStart = Convert.ToDouble(spaceshipTime);
                                        m_timeStart = spaceshipTime;
                                        m_inverse = !m_inverse;
                                    }
                                }
                            }
                            else
                            {
                                if (m_spaceShip.m_pos.X < this.m_screenRes.X + m_spaceShip.m_size.X)
                                {
                                    m_spaceShip.m_pos += (new Vector2(1.5f, 0));
                                }
                                else
                                {
                                    if (m_timeStart <= 0.0f)
                                    {
                                        //m_timeStart = Convert.ToDouble(spaceshipTime);
                                          m_timeStart = spaceshipTime;
                                        m_inverse = !m_inverse;
                                    }
                                }
                            }
                        }

                        //Update(gameTime);
                        //m_stateTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                        //if (m_stateTimer <= 0.0)
                        //{
                        //    List<Board> ramifications = m_board.GetRamifications('O');

                        //    m_board = ramifications[m_randPlay.Next(ramifications.Count)];

                        //    if (m_board.GetBoardState('O') == BoardState.Playing)
                        //        EnterState(GameState.HumanTurn);
                        //    else
                        //        EnterState(GameState.GameOver);
                        //}
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
                        m_spriteBatch.DrawString(m_font, "SCORE< 1 >    HI-SCORE    SCORE< 2 >", new Vector2(70.0f, 25.0F), Color.White);
                        m_spriteBatch.DrawString(m_font, "  " + p1Score.ToString("D4") + "     " + highScore.ToString("D4") + "      0000", new Vector2(70.0f, 45.0F), Color.White);
                        m_spriteBatch.DrawString(m_font, "PLAYING", new Vector2(200.0f, 100.0f), Color.White);
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

        private void MainMenu(GameTime gameTime)
        {
            // https://www.youtube.com/watch?v=axlx3o0codc
            // https://danieltian.wordpress.com/2008/12/24/xna-tutorial-typewriter-text-box-with-proper-word-wrapping-part-3/

            //String strPlay = "PLAY";

            m_spriteBatch.DrawString(m_font, "SCORE< 1 >    HI-SCORE    SCORE< 2 >", new Vector2(70.0f, 25.0F), Color.White);
            m_spriteBatch.DrawString(m_font, "  " + p1Score.ToString("D4") + "     " + highScore.ToString("D4") + "      0000", new Vector2(70.0f, 45.0F), Color.White);
            
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

            m_graphics.PreferredBackBufferWidth = (int)m_screenRes.X;
            m_graphics.PreferredBackBufferHeight = (int)m_screenRes.Y;

            m_graphics.ApplyChanges();

            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            m_texPlayer = Content.Load<Texture2D>("PlayerSheet");
            m_texSpaceship = Content.Load<Texture2D>("Spaceship");
            m_texplayerBullet = Content.Load<Texture2D>("Bullet");
            m_texBarrier = Content.Load<Texture2D>("Barrier");
            m_pixelBarrier = Content.Load<Texture2D>("pixel_barrier");
            
            //m_texNPC = Content.Load<Texture2D>("Char14");
            //m_texFood = Content.Load<Texture2D>("Char09");

            m_font = Content.Load<SpriteFont>("Arial");

            //m_entities.Add(
            //    new Player(this, m_screenRes * 0.5f, new Vector2(32, 32), m_texPlayer)
            //);

            //m_entities.Add(
            //    new NPC(this, new Vector2(500.0f, 100.0f), new Vector2(64, 64), m_texNPC,
            //        100.0f, 500.0f, 5.0f)
            //);

            /*m_entities.Add(
                 new TestOverlap(this, new Vector2(500.0f, 100.0f), new Vector2(256, 64),
                                 Content.Load<Texture2D>("Misc05"))
            );*/
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

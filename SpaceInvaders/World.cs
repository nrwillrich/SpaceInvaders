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
        SpriteBatch m_spriteBatch;

        public List<Entity> m_entities = new List<Entity>();

        public KeyboardState m_prevKeyboardState;
        public MouseState m_prevMouseState;

        public Vector2 m_screenRes = new Vector2(448.0f, 512.0f);

        public SpriteFont m_font;

        //public Texture2D m_texPlayer;
        //public Texture2D m_texNPC;
        //public Texture2D m_texFood;

        int highScore = 0, p1Score = 0;

        GameState m_state = GameState.MainMenu;

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
                        //DrawBoard();
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

            //m_texPlayer = Content.Load<Texture2D>("Char19");
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
                e.Update(gameTime);

            UpdateState(gameTime);

            base.Update(gameTime);

            m_prevKeyboardState = Keyboard.GetState();

            m_prevMouseState = Mouse.GetState();
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

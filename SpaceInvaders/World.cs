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

        GameState m_state = GameState.Null;

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
                    //    if (Keyboard.GetState().IsKeyDown(Keys.D1) &&
                    //        !m_prevKeyboardState.IsKeyDown(Keys.D1))
                    //    {
                    //        EnterState(GameState.HumanTurn);
                    //    }
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
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed &&
                           m_prevMouseState.LeftButton != ButtonState.Pressed)
                        {
                            EnterState(GameState.MainMenu);
                        }
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
                        m_spriteBatch.DrawString(m_font, "Press 1 for Human, 2 for Computer", new Vector2(100.0f, 300.0f), Color.White);
                    }
                    break;

                case GameState.Playing:
                    {
                        //DrawBoard();
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

            //m_font = Content.Load<SpriteFont>("Arial");

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

            base.Update(gameTime);

            m_prevKeyboardState = Keyboard.GetState();

            m_prevMouseState = Mouse.GetState();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            m_spriteBatch.Begin(SpriteSortMode.BackToFront,
                                BlendState.AlphaBlend,
                                SamplerState.PointClamp);

            foreach (Entity e in m_entities)
                e.Draw(gameTime, m_spriteBatch);

            m_spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

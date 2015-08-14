using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public enum GameState { Null, MainMenu, Playing, PlayerDead, GameOver };

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
        Texture2D m_texLine;

        float m_stepInterval = 800.0f;
        float m_dyingTimer = 1800.0f;

        int m_sheetColumns = 2;
        int m_sheetLines = 4;

        int m_spriteWidth;
        int m_spriteHeight;

        public Texture2D m_texPlayer;
        public Texture2D m_texSpaceship;
        public Texture2D m_texplayerBullet;
        public Texture2D m_texBarrier;
        public Texture2D m_pixelBarrier;

        public Dynamic m_spaceShip;
        public double m_timeStart = 0.0f;

        public int m_highScore = 0, m_p1Score = 0;

        int m_lives = 6;

        public bool m_inverse = false;

        GameState m_state = GameState.MainMenu;

        Enemies m_enemies;

        public Vector2[] m_barrierPositions = new Vector2[]
        {
            //Barreira é 8 x 6 casas (42 x 30 pixels)
                                                      new Vector2(12,  0), new Vector2(18,  0), new Vector2(24,  0), new Vector2(30,  0),

                                 new Vector2( 6,  6), new Vector2(12,  6), new Vector2(18,  6), new Vector2(24,  6), new Vector2(30,  6), new Vector2(36,  6),

            new Vector2( 0, 12), new Vector2( 6, 12), new Vector2(12, 12), new Vector2(18, 12), new Vector2(24, 12), new Vector2(30, 12), new Vector2(36, 12), new Vector2(42, 12),

            new Vector2( 0, 18), new Vector2( 6, 18), new Vector2(12, 18),                                           new Vector2(30, 18), new Vector2(36, 18), new Vector2(42, 18),
                                                                                                                                      
            new Vector2( 0, 24), new Vector2( 6, 24),                                                                                     new Vector2(36, 24), new Vector2(42, 24),                                 

            new Vector2( 0, 30), new Vector2( 6, 30),                                                                                     new Vector2(36, 30), new Vector2(42, 30)
        };

        void CreateBarrier(float x, float y) {
            foreach (Vector2 v in m_barrierPositions)
                m_entities.Add(new Barrier(this, new Vector2(x + v.X, y + v.Y), new Vector2(6, 6), m_pixelBarrier));
        }

        public float m_playerFrame = 0.0f;

        public void EnterState(GameState newState)
        {
            LeaveState();

            m_state = newState;

            switch (m_state)
            {
                case GameState.MainMenu:
                {
                    m_lives = 6;
                    m_p1Score = 0;
                }
                break;

                case GameState.Playing: { 
                } break;

                case GameState.PlayerDead: {
                    m_dyingTimer = 1800.0f;
                } break;

                case GameState.GameOver:
                {
                    if (m_p1Score > m_highScore) {
                        m_highScore = m_p1Score;
                    }
                    m_entities.Clear();
                }
                break;
            }
        }

        public void LeaveState()
        {
            switch (m_state)
            {
                case GameState.MainMenu: {
                } break;

                case GameState.Playing: {
                } break;

                case GameState.GameOver: {
                } break;
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
                            PreloadObjects();
                            EnterState(GameState.Playing);
                        }
                    }
                    break;

                case GameState.Playing:
                    {
                        m_stepInterval -= gameTime.ElapsedGameTime.Milliseconds;
                        m_timeStart -= gameTime.ElapsedGameTime.TotalSeconds;

                        if (m_stepInterval < 0.0f) {
                            m_enemies.Step();
                            SetIntervalStep();
                        }
                        EnemyFire();

                        if (m_timeStart <= 0.0f) {
                            Random rnd = new Random();
                            int spaceshipTime = rnd.Next(30, 40);

                            if (m_inverse) {
                                if (m_spaceShip.m_pos.X > -m_spaceShip.m_size.X) {
                                    m_spaceShip.m_pos -= (new Vector2(1.5f, 0));
                                } else {
                                    if (m_timeStart <= 0.0f) {
                                        m_timeStart = spaceshipTime;
                                        m_inverse = !m_inverse;
                                    }
                                }
                            } else {
                                if (m_spaceShip.m_pos.X < this.m_screenRes.X + m_spaceShip.m_size.X) {
                                    m_spaceShip.m_pos += (new Vector2(1.5f, 0));
                                } else {
                                    if (m_timeStart <= 0.0f) {
                                        m_timeStart = spaceshipTime;
                                        m_inverse = !m_inverse;
                                    }
                                }
                            }
                        }
                    }
                    break;

                case GameState.PlayerDead: {
                    m_dyingTimer -= gameTime.ElapsedGameTime.Milliseconds;
                    if (m_dyingTimer < 0) {
                        m_lives--;
                        if (m_lives > 0) {
                            m_entities.Add(new Player(this, new Vector2(70, m_screenRes.Y - 80), new Vector2(32, 32), m_texPlayer));
                            EnterState(GameState.Playing);
                        } else {
                            EnterState(GameState.GameOver);
                        }
                    }
                } break;

                case GameState.GameOver:
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Space) &&
                            !m_prevKeyboardState.IsKeyDown(Keys.Space)) {
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
                        MainMenu(gameTime);
                    }
                    break;

                case GameState.Playing:
                    {
                        UpdateTopHud();
                        UpdateBottomHud();
                    }
                    break;

                case GameState.GameOver:
                    {
                        m_spriteBatch.DrawString(m_font, "GAME OVER", new Vector2(156.0f, 246.0F), Color.White);
                    }
                    break;
            }
        }

        void PreloadObjects() {
            m_entities.Add(new Player(this, new Vector2(70, m_screenRes.Y - 80), new Vector2(32, 32), m_texPlayer));

            m_enemies = new Enemies(this);

            CreateBarrier(83.0f, 380.0f);
            CreateBarrier(163.0f, 380.0f);
            CreateBarrier(243.0f, 380.0f);
            CreateBarrier(323.0f, 380.0f);

            m_spaceShip = new SpaceShip(this, new Vector2(-32f, m_screenRes.Y * 0.25f), new Vector2(32, 32), m_texSpaceship);
            m_spaceShip.isVisible = true;
            m_entities.Add(m_spaceShip);
            m_timeStart = 35.0f;
        }

        public void KillPlayer(Player player) {
            EnterState(GameState.PlayerDead);
            player.HitPlayer();
        }

        private void UpdateTopHud() {
            m_spriteBatch.DrawString(m_font, "SCORE< 1 >    HI-SCORE    SCORE< 2 >", new Vector2(70.0f, 75.0F), Color.White);
            m_spriteBatch.DrawString(m_font, "  " + m_p1Score.ToString("D4") + "     " + m_highScore.ToString("D4") + "      0000", new Vector2(70.0f, 95.0F), Color.White);
        }

        private void UpdateBottomHud() {
            DrawLine(m_spriteBatch, new Vector2(45.0f, 448.0f), new Vector2(403.0f, 448.0f));

            m_spriteBatch.DrawString(m_font, m_lives.ToString("D1"), new Vector2(70.0f, 455.0F), Color.White);

            Rectangle p = new Rectangle (0, 0, 34, 21);
            for (int x = 1; x <= m_lives; x++) {
                m_spriteBatch.Draw(m_texPlayer, new Vector2(70.0f + (p.Width * x), 463.0f), p, Color.White, 0.0f, new Vector2(p.Width, p.Height) * 0.5f,
                    Vector2.One, SpriteEffects.None, 0.0f);
            }

            m_spriteBatch.DrawString(m_font, "CREDITS 00", new Vector2(300.0f, 455.0F), Color.White);
        }

        private void EnemyFire() {
            // pegar os enemies ainda disponíveis no m_entities
            // ver qual deles tem o m_pos.Y maior (mais abaixo na tela)
            // chamar o .Fire();
            List<Enemy> enemies = new List<Enemy>();
            float posY = 0;
            foreach (Dynamic item in m_entities) {
                if (item is Enemy) {
                    if (item.m_pos.Y > posY) {
                        posY = item.m_pos.Y;
                        enemies.Clear();
                        enemies.Add((Enemy) item);
                    }
                    if (item.m_pos.Y == posY) {
                        enemies.Add((Enemy) item);
                    }
                }
            }
            if (enemies.Count == 0) {
                EnterState(GameState.GameOver);
            } else {
                Random rnd = new Random();
                int enemyIndex = rnd.Next(0, enemies.Count);

                enemies[enemyIndex].Fire();
            }
        }

        private void SetIntervalStep() {
            int alive = m_enemies.GetAliveCount();
            m_stepInterval = 800.0f;

            if (alive <= 40) {
                m_stepInterval = 400.0f;
            } else if (alive <= 30) {
                m_stepInterval = 250.0f;
            } else if (alive <= 20) {
                m_stepInterval = 150.0f;
            } else if (alive <= 10) {
                m_stepInterval = 50.0f;
            } else if (alive <= 5) {
                m_stepInterval = 25.0f;
            }
        }

        private void MainMenu(GameTime gameTime)
        {
            // https://www.youtube.com/watch?v=axlx3o0codc
            // https://danieltian.wordpress.com/2008/12/24/xna-tutorial-typewriter-text-box-with-proper-word-wrapping-part-3/

            //String strPlay = "PLAY";

            UpdateTopHud();
            
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

        void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end) {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);

            sb.Draw(m_texLine,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                Color.Green, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

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

            m_texBarrier = Content.Load<Texture2D>("Barrier");
            m_pixelBarrier = Content.Load<Texture2D>("pixel_barrier");

            m_spriteWidth = m_texInvadersSheet.Width / m_sheetColumns;
            m_spriteHeight = m_texInvadersSheet.Height / m_sheetLines;

            m_font = Content.Load<SpriteFont>("Arial");

            m_texLine = new Texture2D(GraphicsDevice, 1, 1);
            m_texLine.SetData<Color>(new Color[] { Color.White });
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

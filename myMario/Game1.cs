using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;


namespace myMario
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Player mario;

        Vector2 bgposition = new Vector2(0, 0);
       
        List<Ground> currentGroundList;
        List<MushroomGround> currentMushroomGroundList;
        List<Mushroom> currentMushroomList;
        List<Coin> currentCoinList;
        List<CoinGround> currentCoinGroundList;
        List<Monster> currentMonsterList;

        Texture2D background;
        Song song;
        SoundEffect hop;
        KeyboardState ks1;
        TimeSpan x = new(0, 0, 0);
        private SpriteFont hudFont;
        private SpriteFont hudGameOverFont;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            mario = new Player(Content);

            currentMushroomList = new List<Mushroom>();

            currentGroundList = new List<Ground>
            {
                new (Content, mario, "Texture/terrain", 0, 600),
                new (Content, mario, "Texture/terrain", 1280, 600),
                new (Content, mario, "Texture/terrain", 2560, 600),
                new (Content, mario, "Texture/kutular", 500, 440),
                new (Content, mario, "Texture/kutular", 1250, 320),
                new (Content, mario, "Texture/kutular", 1530, 440),
                new (Content, mario, "Texture/kutular", 1920, 400),
                new (Content, mario, "Texture/kutular", 2300, 370),
                new (Content, mario, "Texture/kutular", 2500, 320),
                new (Content, mario, "Texture/kutular", 2700, 270),
                new (Content, mario, "Texture/kutular", 3090, 350),
                new (Content, mario, "Texture/pipe", 900, 482),
                new (Content, mario, "Texture/pipe", 1100, 482),
                new (Content, mario, "Texture/pipe", 2970, 482),
                new (Content, mario, "Texture/pipe", 3400, 482),
                new (Content, mario, "Texture/pipe_mini", 2170, 514)
            };

            currentCoinList = new List<Coin>
            {
                new (Content, mario, "Texture/coin", 1020, 275),
                new (Content, mario, "Texture/coin", 1252, 275),
            };

            currentMushroomGroundList = new List<MushroomGround>
            {
                 new (Content, mario, "Texture/kutu", 596, 280),
                 new (Content, mario, "Texture/kutu", 2558, 170),
            };

            currentCoinGroundList = new List<CoinGround>
            {
                new (5, Content, mario, "Texture/kutu", 452, 440),
                new (3, Content, mario, "Texture/kutu", 548, 280),
                new (2, Content, mario, "Texture/kutu", 1968, 225),
                new (1, Content, mario, "Texture/kutu", 2016, 225),
                new (1, Content, mario, "Texture/kutu", 3282, 350),
            };

            currentMonsterList = new List<Monster>
            {
               new (Content, mario, "Texture/banzaibill_60", 1450, 540, "banzaibill"),
               new (Content, mario, "Texture/koopa_60", 1650, 540, "koopa"),
               new (Content, mario, "Texture/bigbooR", 1850, 540, "bigboo"),
               new (Content, mario, "Texture/blargg_120R", 2070, 480, "blargg"),   
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
           
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            background = Content.Load<Texture2D>("Texture/back");
            song = Content.Load<Song>("Songs/backmusic");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);
            MediaPlayer.Volume = (float)0.1;
            hop = Content.Load<SoundEffect>("Songs/jump");
            hudFont = Content.Load<SpriteFont>("Font/Hud");
            hudGameOverFont = Content.Load<SpriteFont>("Font/Hud_GameOver");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            Console.WriteLine("MarioX: " + mario.position.X + " y: " + mario.position.Y + " velx: " + mario.velocity.X);
            // Console.WriteLine("Ground: " + ground.position.X + " y: " + ground.position.Y);
            if (mario.position.X < 740)
            {
                mario.cameraRight = false;
            }

            foreach (var mgnd in currentMushroomGroundList)
            {
                if (mgnd.mushroomActivated() && !mgnd.addedToList)
                {
                    currentMushroomList.Add(mgnd.m);
                    mgnd.addedToList = true;
                }
            }

            foreach (var gnd in currentGroundList)
            {
                gnd.checkCollision();
                foreach (var mon in currentMonsterList)
                {
                    gnd.checkMonsterCollision(mon);
                }
            }

            foreach (var mgnd in currentMushroomGroundList)
            {
                mgnd.checkCollision();
                foreach (var mon in currentMonsterList)
                {
                    mgnd.checkMonsterCollision(mon);
                }
            }

            foreach (var m in currentMushroomList)
            {
                m.checkCollision();
            }

            foreach (var c in currentCoinList)
            {
                c.checkCollision();
            }
            foreach (var ck in currentCoinGroundList)
            {
                ck.checkCollision();
                foreach (var mon in currentMonsterList)
                {
                    ck.checkMonsterCollision(mon);
                }
            }
            foreach (var mon in currentMonsterList)
            {
                mon.checkCollision();
            }


            if (mario.position.X <= 0)
            {
                mario.position.X = 0;
                mario.velocity.X = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(Keyboard.GetState().IsKeyDown(Keys.R))
            {
                mario.position = new Vector2(0, 600);
                mario.lives = 3;
                mario.score = 0;
            }
            //prevent jump sound looping
            ks1 = Keyboard.GetState();
            if (ks1.IsKeyDown(Keys.W) && !mario.jumping)
            {
                mario.jump();
                hop.Play((float)0.1, 0, 0);
                mario.jumping = true;

            }
            if (ks1.IsKeyUp(Keys.W))
            {
                mario.jumping = false;
            }


            if (Keyboard.GetState().IsKeyDown(Keys.A) && !mario.collusingLeft)
            {
                mario.findState('l');

                if (mario.cameraRight)
                {
                    mario.cameraRight = false;
                    mario.velocity.X = 0;
                }

                if (mario.position.X <= 0)
                {
                    mario.position.X = 0;
                }
                else
                {

                    mario.setVelX(-1); //0.75
                }

            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) && !mario.collusingRight)
            {
                mario.findState('r');
                if (mario.position.X >= 740)
                {
                    mario.setVelX(1);
                    mario.cameraRight = true;
                }
                else
                {
                    mario.setVelX(1);
                }

            }

            if (mario.collusingRight == false && mario.cameraRight && Keyboard.GetState().IsKeyDown(Keys.D))
            {
                foreach (var gnd in currentGroundList)
                {
                    gnd.position.X -= mario.velocity.X;
                }
                foreach (var mgnd in currentMushroomGroundList)
                {
                    mgnd.position.X -= mario.velocity.X;
                }
                foreach (var m in currentMushroomList)
                {
                    m.position.X -= mario.velocity.X;
                }
                foreach (var c in currentCoinList)
                {
                    c.position.X -= mario.velocity.X;
                }
                foreach (var ck in currentCoinGroundList)
                {
                    ck.position.X -= mario.velocity.X;
                }
                foreach (var mon in currentMonsterList)
                {
                    mon.position.X -= mario.velocity.X;
                }

            }

            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                if (mario.size == 0)
                {
                    mario.size = 1;
                }
                else if (mario.size == 1)
                {
                    mario.size = 2;
                }
                else if (mario.size == 2)
                {
                    mario.size = 0;
                }
            }

            mario.findState('n');
           // mario.myState();
            mario.gravity();
            if (mario.velocity.X > 6)
            {
                mario.velocity.X = 6;
            }
            else if (mario.velocity.X < -6)
            {
                mario.velocity.X = -6;
            }
            foreach (var mon in currentMonsterList)
            {
                mon.move(mon.name);
            }

            mario.move();

            if (mario.position.Y > 720)
            {
                Console.WriteLine("GAME OVER!");
                mario.position = new Vector2(0, 600);
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);
            _spriteBatch.Begin();

            x += gameTime.ElapsedGameTime;
            if (x.Milliseconds == 100)
            {
                mario.animate();
                x = gameTime.ElapsedGameTime;
            }

            _spriteBatch.Draw(background, bgposition, Color.White);
            _spriteBatch.Draw(mario.currenttexture, mario.position, Color.White);

            foreach (Ground gnd in currentGroundList)
            {
                _spriteBatch.Draw(gnd.texture, gnd.position, Color.White);
            }
            foreach (var c in currentCoinList)
            {
                _spriteBatch.Draw(c.texture, c.position, Color.White);
            }
            foreach (var ck in currentCoinGroundList)
            {
                _spriteBatch.Draw(ck.texture, ck.position, Color.White);
            }
            foreach (var mgnd in currentMushroomGroundList)
            {
                _spriteBatch.Draw(mgnd.texture, mgnd.position, Color.White);
            }
            foreach (var mon in currentMonsterList)
            {
                SpriteEffects spriteEffects = SpriteEffects.None;
                if(mon.rotation == 1)
                {
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
                _spriteBatch.Draw(mon.texture, mon.position, null, Color.White, 0, Vector2.Zero, 1, spriteEffects, 0);
            }
            // TODO: Add your drawing code here
            DrawHud();
            _spriteBatch.End();
            base.Draw(gameTime);
        }


        private void DrawHud()
        {
            Rectangle titleSafeArea = GraphicsDevice.Viewport.TitleSafeArea;
            if (mario.lives == 0)
            {
                string gameOverText = "GAME OVER";
                Vector2 textSize = hudGameOverFont.MeasureString(gameOverText);
                Vector2 position = new Vector2((GraphicsDevice.Viewport.Width - textSize.X) / 2, (GraphicsDevice.Viewport.Height - textSize.Y) / 2);
                DrawShadowedString(hudGameOverFont, gameOverText, position, Color.Red);
            }
            // Draw Life
            DrawShadowedString(hudFont, "LIFE: " + mario.lives.ToString(), new Vector2(0, 0), Color.Yellow);
            // Draw score
            DrawShadowedString(hudFont, "SCORE: " + mario.score.ToString(), new Vector2(0,20), Color.Yellow);


        }
        private void DrawShadowedString(SpriteFont font, string value, Vector2 position, Color color)
        {
            _spriteBatch.DrawString(font, value, position + new Vector2(1.0f, 1.0f), Color.Black);
            _spriteBatch.DrawString(font, value, position, color);
        }
    }
}

﻿using Microsoft.Xna.Framework;
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
        List<Bullet> Bulletslist = new List<Bullet>();
        Texture2D background;
        Song song;
        SoundEffect hop;
        KeyboardState ks1;
        TimeSpan x = new(0, 0, 0);
        private SpriteFont hudFont;
        private SpriteFont hudGameOverFont;

        private float lastBulletTime = 0;
        private float bulletInterval = 0.5f;


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


            currentMushroomList = new List<Mushroom>
            {
               // new (Content, mario, "Texture/fungo_60C", 588, 220),
            };

            currentGroundList = new List<Ground>
            {
                new (Content, mario, "Texture/terrain", 0, 600),
                new (Content, mario, "Texture/terrain", 1280, 600),
                new (Content, mario, "Texture/terrain", 2560, 600),
                new (Content, mario, "Texture/terrain", 3840, 600),
                new (Content, mario, "Texture/kutular", 500, 440),
                new (Content, mario, "Texture/kutular", 1250, 320),
                new (Content, mario, "Texture/kutular", 1530, 440),
                new (Content, mario, "Texture/kutular", 1920, 400),
                new (Content, mario, "Texture/kutular", 2300, 370),
                new (Content, mario, "Texture/kutular", 2500, 320),
                new (Content, mario, "Texture/kutular", 2700, 270),
                new (Content, mario, "Texture/kutular", 3090, 350),
                new (Content, mario, "Texture/kutular", 3301, 250),
                new (Content, mario, "Texture/kutular", 3599, 290),
                new (Content, mario, "Texture/pipe", 900, 482),
                new (Content, mario, "Texture/pipe", 1100, 482),
                new (Content, mario, "Texture/pipe", 2970, 482),
                new (Content, mario, "Texture/pipe", 3400, 482),
                new (Content, mario, "Texture/pipe", 4130, 482),
                new (Content, mario, "Texture/pipe", 4950, 482),
                new (Content, mario, "Texture/pipe_mini", 2170, 514),
            };

            //coins on the ground
            currentCoinList = new List<Coin>();
            for (int i = 0; i< 17; i++)
            {
                Coin coin = new (Content, mario, "Texture/coin2", 50 * (i + 1), 551);
                currentCoinList.Add(coin);
            }
            for (int i = 0; i < 2; i++)
            {
                Coin coin = new (Content, mario, "Texture/coin2", 990 + (48 * i), 551);
                currentCoinList.Add(coin);
            }
            for (int i = 0; i < 5; i++)
            {
                Coin coin = new Coin(Content, mario, "Texture/coin2", 454 +(48 * i) , 397);
                currentCoinList.Add(coin);
            }
            for(int i = 0; i<4; i++)
            {
                Coin coin = new (Content, mario, "Texture/coin2", 1252 + (48 * i), 275);
                currentCoinList.Add(coin);
            }
            for (int i = 0; i < 4; i++)
            {
                Coin coin = new (Content, mario, "Texture/coin2", 1530 + (48 * i), 397);
                currentCoinList.Add(coin);
            }
            for (int i = 0; i < 4; i++)
            {
                Coin coin = new (Content, mario, "Texture/coin2", 1925 + (48 * i), 359);
                currentCoinList.Add(coin);
            }
            for (int i = 0; i < 4; i++)
            {
                Coin coin = new(Content, mario, "Texture/coin2", 2304 + (48 * i), 325);
                currentCoinList.Add(coin);
            }
            for (int i = 0; i < 4; i++)
            {
                Coin coin = new(Content, mario, "Texture/coin2", 2500 + (48 * i), 278);  
                currentCoinList.Add(coin);
            }
            //x più è alto il numero più è in alto il coin
            for (int i = 0; i < 4; i++)
            {
                Coin coin = new(Content, mario, "Texture/coin2", 2702 + (48 * i), 231);
                currentCoinList.Add(coin);
            }
            for (int i = 0; i < 15; i++)
            {
                Coin coin = new(Content, mario, "Texture/coin2", 2255 + (48 * i), 551);
                currentCoinList.Add(coin);
            }
            for (int i = 0; i < 7; i++)
            {
                Coin coin = new(Content, mario, "Texture/coin2", 3060 + (48 * i), 551);
                currentCoinList.Add(coin);
            }
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Coin coin = new(Content, mario, "Texture/coin2", 4220 + (48 * i), 551 - (60 * j));
                    currentCoinList.Add(coin);
                }
            }
            //special position coins
            currentCoinList.AddRange(new List<Coin>
            {
                new (Content, mario, "Texture/coin2", 549, 239),
                new (Content, mario, "Texture/coin2", 300, 552),
                new (Content, mario, "Texture/coin2", 1020, 275),
            });
           

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
               new (Content, mario, "Texture/blargg_120R", 4000, 480, "blargg"),
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
            float volume = 0.1f; // Metà volume
            hop = Content.Load<SoundEffect>("Songs/jump");
            hop.Play(volume, 0, 0);
            hudFont = Content.Load<SpriteFont>("Font/Hud");
            hudGameOverFont = Content.Load<SpriteFont>("Font/Hud_GameOver");

            // TODO: use this.Content to load your game content here
        }

        private void ResetGame()
        {
            Initialize();
            LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            Console.WriteLine("MarioX: " + mario.position.X + " y: " + mario.position.Y + " velx: " + mario.velocity.X);
            
             //Console.WriteLine("Ground: " + ground.position.X + " y: " + ground.position.Y);
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
                gnd.CheckCollision();
                foreach (var mon in currentMonsterList)
                {
                    gnd.CheckMonsterCollision(mon);
                }
            }

            foreach (var mgnd in currentMushroomGroundList)
            {
                mgnd.CheckCollision();
                foreach (var mon in currentMonsterList)
                {
                    mgnd.CheckMonsterCollision(mon);
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
                ck.CheckCollision();
                foreach (var mon in currentMonsterList)
                {
                    ck.CheckMonsterCollision(mon);
                }
            }
            foreach (var mon in currentMonsterList)
            {
                mon.checkCollision(mon.name);
            }


            if (mario.position.X <= 0)
            {
                mario.position.X = 0;
                mario.velocity.X = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.K))
                mario.lives = 0;

            if(mario.lives == 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    ResetGame();
                }
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

            if(mario.size == 2)
            {
                if ((gameTime.TotalGameTime.TotalSeconds - lastBulletTime) >= bulletInterval)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    {
                        // Sparare un proiettile
                        int direction;
                        if (mario.standingR || mario.walkingRight)
                        {
                            direction = 1;
                        }
                        else
                        {
                            direction = -1;
                        }
                        Bullet bullet = new Bullet(Content, mario, "Texture/fireball2Transparent", direction);
                        Bulletslist.Add(bullet);
                        lastBulletTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                }
                foreach (Bullet bullet in Bulletslist)
                {
                    bullet.Move();
                    bullet.Refresh();
                    foreach (var mon in currentMonsterList)
                    {
                        bullet.CheckMonsterCollision(mon);
                    }
                    foreach (var gnd in currentGroundList)
                    {
                        bullet.CheckPipeCollision(gnd);
                    }
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
                foreach (var mgnd in currentMushroomList)
                {
                    mgnd.position.X -= mario.velocity.X;
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
                mario.lives = 0;
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
           
            if (mario.lives > 0)
            {
                _spriteBatch.Draw(mario.currenttexture, mario.position, Color.White);
            }
            
            foreach (Ground gnd in currentGroundList)
            {
                _spriteBatch.Draw(gnd.texture, gnd.position, Color.White);
            }
            foreach (Mushroom msh in currentMushroomList)
            {
                _spriteBatch.Draw(msh.texture, msh.position, Color.White);
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

            if(Bulletslist != null)
            {
                foreach (var bullet in Bulletslist)
                {
                    _spriteBatch.Draw(bullet.texture, bullet.position, Color.White);
                }
            }
            
            // TODO: Add your drawing code here
            DrawHud();
            _spriteBatch.End();
            base.Draw(gameTime);
        }


        private void DrawHud()
        {
            string hudtext = "";
            Color textColor = Color.Yellow;
            if (mario.lives == 0)
            {
                hudtext = "     GAME OVER" + Environment.NewLine + "Press R to Restart";
                textColor = Color.Red;
                mario.dispose();
            }
            Vector2 textSize = hudGameOverFont.MeasureString(hudtext);
            Vector2 position = new Vector2((GraphicsDevice.Viewport.Width - textSize.X) / 2, (GraphicsDevice.Viewport.Height - textSize.Y) / 2);
            DrawShadowedString(hudGameOverFont, hudtext, position, textColor);
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

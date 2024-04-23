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
        Ground ground, ground2, ground3, kutular1, boru1, boru2, boru4, boru5, kutular2, kutular3, boru3,
               kutular4, kutular5, kutular6, kutular7, kutular8, miniboru1;

        Coin coin1, coin2;

        MushroomGround mantarkutu1, mantarkutu2;

        CoinGround coinkutu1, coinkutu2, coinkutu3, coinkutu4, coinkutu5;

        List<Ground> currentGroundList;
        
        List<MushroomGround> currentMushroomGroundList;
        List<Mushroom> currentMushroomList;
        List<Coin> currentCoinList;
        List<CoinGround> currentCoinGroundList;
        Texture2D background;
        Song song;
        SoundEffect hop;
        KeyboardState ks1, ks2;
        TimeSpan x = new TimeSpan(0, 0, 0);
        private SpriteFont hudFont;
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
            ground = new Ground(Content, mario, "Texture/terrain", 0, 600);
            ground2 = new Ground(Content, mario, "Texture/terrain", 1280, 600);
            ground3 = new Ground(Content, mario, "Texture/terrain", 2560, 600);

            coin1 = new Coin(Content, mario, "Texture/coin", 1020, 275);
            coin2 = new Coin(Content, mario, "Texture/coin", 1252, 275); 

            kutular1 = new Ground(Content, mario, "Texture/kutular", 500, 440);
            kutular2 = new Ground(Content, mario, "Texture/kutular", 1250, 320);
            kutular3 = new Ground(Content, mario, "Texture/kutular", 1530, 440);
            kutular4 = new Ground(Content, mario, "Texture/kutular", 1920, 400);
            kutular5 = new Ground(Content, mario, "Texture/kutular", 2300, 370);
            kutular6 = new Ground(Content, mario, "Texture/kutular", 2500, 320);
            kutular7 = new Ground(Content, mario, "Texture/kutular", 2700, 270);
            kutular8 = new Ground(Content, mario, "Texture/kutular", 3090, 350);

            mantarkutu1 = new MushroomGround(Content, mario, "Texture/kutu", 596, 280);
            mantarkutu2 = new MushroomGround(Content, mario, "Texture/kutu", 2558, 170);

            coinkutu1 = new CoinGround(5, Content, mario, "Texture/kutu", 452, 440);
            coinkutu2 = new CoinGround(3, Content, mario, "Texture/kutu", 548, 280);
            coinkutu3 = new CoinGround(2, Content, mario, "Texture/kutu", 1968, 225);
            coinkutu4 = new CoinGround(1, Content, mario, "Texture/kutu", 2016, 225);
            coinkutu5 = new CoinGround(1, Content, mario, "Texture/kutu", 3282, 350);

            boru1 = new Ground(Content, mario, "Texture/pipe", 900, 482);
            boru2 = new Ground(Content, mario, "Texture/pipe", 1100, 482);
            boru3 = new Ground(Content, mario, "Texture/pipe", 1800, 482);
            boru4 = new Ground(Content, mario, "Texture/pipe", 2970, 482);
            boru5 = new Ground(Content, mario, "Texture/pipe", 3400, 482);
            miniboru1 = new Ground(Content, mario, "Texture/pipe_mini", 2170, 514);

            currentMushroomList = new List<Mushroom>();

            currentGroundList = new List<Ground>
            {
                new Ground(Content, mario, "Texture/terrain", 0, 600),
                ground2,
                ground3,
                kutular1,
                kutular2,
                kutular3,
                kutular4,
                kutular5,
                kutular6,
                kutular7,
                kutular8,
                boru1,
                boru2,
                boru3,
                boru4,
                boru5,
                miniboru1
            };
            currentCoinList = new List<Coin>
            {
                coin1,
                coin2,

            };

            currentMushroomGroundList = new List<MushroomGround>
            {
                mantarkutu1,
                mantarkutu2
            };

            currentCoinGroundList = new List<CoinGround>
            {
                coinkutu1,
                coinkutu2,
                coinkutu3,
                coinkutu4,
                coinkutu5
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

            foreach (var gnd in currentGroundList)
            {
                gnd.checkCollision();
                
            }
            foreach (var c in currentCoinList)
            {
                c.checkCollision();
                
            }
            foreach (var mgnd in currentMushroomGroundList)
            {
                if (mgnd.mushroomActivated() && !mgnd.addedToList)
                {
                    currentMushroomList.Add(mgnd.m);
                    mgnd.addedToList = true;
                }
            }
            foreach (var mgnd in currentMushroomGroundList)
            {
                if (mgnd.mushroomActivated() && !mgnd.addedToList)
                {
                    currentMushroomList.Add(mgnd.m);
                    mgnd.addedToList = true;
                }
            }



            

            if (mario.position.X <= 0)
            {
                mario.position.X = 0;
                mario.velocity.X = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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

            ks2 = ks1;

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
            mario.myState();
            mario.gravity();
            if (mario.velocity.X > 6)
            {
                mario.velocity.X = 6;
            }
            else if (mario.velocity.X < -6)
            {
                mario.velocity.X = -6;
            }
           
            mario.move();

            if (mario.position.Y > 720)
            {
                Console.WriteLine("GAME OVER!");
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
            // TODO: Add your drawing code here
            DrawHud();
            _spriteBatch.End();
            base.Draw(gameTime);
        }


        private void DrawHud()
        {
            Rectangle titleSafeArea = GraphicsDevice.Viewport.TitleSafeArea;

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

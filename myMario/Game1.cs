using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace myMario
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Player mario;
        Vector2 bgposition = new Vector2(0, 0);
        Ground ground, ground2, ground3;
        Coin
        List<Ground> currentGroundList;
        Texture2D background;
        Song song;
        SoundEffect hop;
        KeyboardState ks1, ks2;
        TimeSpan x = new TimeSpan(0, 0, 0);
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
            currentGroundList = new List<Ground>
            {
                ground,
                ground2,
                ground3,
            };
            coin1 = new Coin(Content, mario, "coin", 1020, 275);
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

            if (mario.position.X <= 0)
            {
                mario.position.X = 0;
                mario.velocity.X = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ks1 = Keyboard.GetState();
            if (ks1.IsKeyDown(Keys.W) && ks2.IsKeyUp(Keys.W))
            {
                mario.jump();
                hop.Play((float)0.1, 0, 0);
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
            //mario.myState();
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
            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

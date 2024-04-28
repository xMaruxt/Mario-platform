using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace myMario
{
    class Monster
    {
        public ContentManager content;
        public Texture2D texture;
        public Vector2 position;
        public int width, height;
        public Rectangle bounds, right, left, top;
        public Player player;
        public Bullet bullet;
        public int rotation = 0;
        public bool collusingRight, collusingLeft;
        public int jumpCounter = 0;
        public string name;
        public bool hitByBullet = false;

        private readonly Dictionary<string, Tuple<int, int>> monsterAtrtb = new Dictionary<string, Tuple<int,int>>
        {
            { "banzaibill", new Tuple<int, int>(200, 5) },
            { "koopa", new Tuple<int, int>(400, 4) },
            { "bigboo", new Tuple<int, int>(800, 3) },
            { "blargg", new Tuple<int, int>(1600, 2) }
        };

        public Monster(ContentManager content, Player player, String tex, int x, int y, string name)
        {
            this.player = player;
            this.content = content;
            texture = content.Load<Texture2D>(tex);
            width = texture.Width;
            height = texture.Height;
            position = new Vector2(x, y);
            if(tex == "Texture/koopa_60")
            {
                bounds = new Rectangle((int)position.X, (int)position.Y, 60, 60);
            }
            else
            {
                bounds = new Rectangle((int)position.X, (int)position.Y, width, height);
            }
            left = new Rectangle((int)position.X, (int)position.Y + 5, 5, height - 10);
            top = new Rectangle((int)position.X, (int)position.Y, width, 10);
            right = new Rectangle((int)position.X + width - 5, (int)position.Y + 5, 5, height - 10);
            this.name = name;

        }

        public void refresh()
        {
            left.X = (int)position.X;
            left.Y = (int)position.Y + 5;
            right.X = (int)position.X + width - 5;
            right.Y = (int)position.Y + 5;
            top.X = (int)position.X;
            top.Y = (int)position.Y;
        }

        public void refbounds()
        {
            bounds.X = (int)position.X;
            bounds.Y = (int)position.Y;
        }

        //0 = right, 1 = left
        public void move(string name)
        {
            //change velocity by name
            if (rotation == 0)
            {
                position.X += monsterAtrtb[name].Item2;
            }
            if (rotation == 1)
            {
                position.X -= monsterAtrtb[name].Item2;
            }
        }


        public Rectangle getBounds()
        {
            refresh();
            return bounds;
        }

        public void checkCollision(string name)
        {
            refbounds();
            refresh();
            if (bounds.Intersects(player.getBounds()))
            {
                if (top.Intersects(player.getBot()))
                {
                    jumpCounter++;
                    if (jumpCounter >= 2)
                    {
                        //MONSTER DIED

                        player.position.Y -= 60;
                        position.X = -2000;
                        rotation = 3;
                        jumpCounter = 0;
                        player.lives++;
                        player.score += monsterAtrtb[name].Item1;
                    }
                    else
                    {
                        player.position.Y -= 80;
                    }
                   
                }
                else
                {
                    //mario take damage, could die
                    player.takeDamage();
                    if (rotation == 0)
                    {
                        rotation = 1;
                    }
                    else if (rotation == 1)
                    {
                        rotation = 0;
                    }
                }
            }
            if (hitByBullet)
            {
                position.X = -2000;
                player.score += 5;
                hitByBullet = false;
            }            
        }

        public Rectangle getLeft()
        {
            refresh();
            return left;
        }
        public Rectangle getRight()
        {
            refresh();
            return right;
        }
    }
}

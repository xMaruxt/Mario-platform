using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

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
            if (name == "banzaibill")
            {
                if (rotation == 0)
                {
                    position.X += 8;                    
                }
                if (rotation == 1)
                {
                    position.X -= 8;
                }
            }
            if (name == "koopa")
            {
                if (rotation == 0)
                {
                    position.X += 7;
                }
                if (rotation == 1)
                {
                    position.X -= 7;
                }
            }
            if (name == "bigboo")
            {
                if (rotation == 0)
                {
                    position.X += 5;
                }
                if (rotation == 1)
                {
                    position.X -= 5;
                }
            }
            if (name == "blargg")
            {
                if (rotation == 0)
                {
                    position.X += 4;
                }
                if (rotation == 1)
                {
                    position.X -= 4;
                }
            }
        }


        public Rectangle getBounds()
        {
            refresh();
            return bounds;
        }

        public void checkCollision()
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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myMario
{
    class Bullet
    {
        public ContentManager content;
        public Texture2D texture;
        public Vector2 position;
        public int width, height;
        public int speed = 10;
        public Rectangle boxCollider, right, left;
        public Player player;
        public Monster monster;

        public Bullet(ContentManager content, Player player, String tex)
        {
            this.player = player;
            this.content = content;
            texture = content.Load<Texture2D>(tex);
            width = texture.Width;
            height = texture.Height;
            position = new Vector2(player.position.X, player.position.Y);
            boxCollider = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public virtual void refresh()
        {
            boxCollider.X = (int)position.X;
            boxCollider.Y = (int)position.Y;
        }

        public void dispose()
        {
            position.X = -2000;
            position.Y = 0;
            refresh();
        }

        public void move()
        {
            position.X += speed;
            refresh();
        }
        public Rectangle getBounds()
        {
            refresh();
            return boxCollider;
        }
        public void checkMonsterCollision(Monster mon)
        {
            refresh();
            if (boxCollider.Intersects(mon.getLeft()))
            {
               mon.hitByBullet = true;
               dispose();
               speed = 0;
            }

            if (boxCollider.Intersects(mon.getRight()))
            {
                mon.hitByBullet = true;
                dispose();
                speed = 0;
            }
        }
    }
}

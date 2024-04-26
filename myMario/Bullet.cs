using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
        public Rectangle bounds;
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
            bounds = new Rectangle((int)position.X, (int)position.Y, width, height);
            speed = 10;
        }




        public void checkCollision()
        {
            refresh();
            if (bounds.Intersects(monster.getBounds()))
            {
                dispose();
            }
        }

        public virtual void refresh()
        {
            bounds.X = (int)position.X;
            bounds.Y = (int)position.Y;
        }

        public void dispose()
        {
            position.X = -1280;
            position.Y = 0;
            refresh();
        }

        public void move()
        {
            position.X += speed;
            refresh();
        }
        
    }
}

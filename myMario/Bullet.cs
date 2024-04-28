using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace myMario
{
    class Bullet
    {
        public ContentManager content;
        public Texture2D texture;
        public Vector2 position;
        public int width, height;
        public int speed = 10;
        public Rectangle boxCollider;
        public Player player;
        public Monster monster;
        public int direction = 1; // 1 = right, -1 = left

        public Bullet(ContentManager content, Player player, String tex, int direction)
        {
            this.player = player;
            this.content = content;
            texture = content.Load<Texture2D>(tex);
            width = texture.Width;
            height = texture.Height;
            position = new Vector2(player.position.X, player.position.Y+45);
            boxCollider = new Rectangle((int)position.X, (int)position.Y, width, height);

            this.direction = direction;
        }

        public virtual void Refresh()
        {
            boxCollider.X = (int)position.X;
            boxCollider.Y = (int)position.Y;
        }

        public void Dispose()
        {
            position.X = -2000;
            position.Y = 0;
            Refresh();
        }

        public void Move()
        {
            position.X += speed * direction;
            Refresh();
        }
        public Rectangle GetBounds()
        {
            Refresh();
            return boxCollider;
        }
        public void CheckMonsterCollision(Monster mon)
        {
            Refresh();
            if (boxCollider.Intersects(mon.getLeft()))
            {
               mon.hitByBullet = true;
               Dispose();
               speed = 0;
            }

            if (boxCollider.Intersects(mon.getRight()))
            {
                mon.hitByBullet = true;
                Dispose();
                speed = 0;
            }
        }

        public void CheckPipeCollision(Ground pipe)
        {
            Refresh();
            if (boxCollider.Intersects(pipe.GetLeft()))
            {
                Dispose();
                speed = 0;
            }

            if (boxCollider.Intersects(pipe.GetRight()))
            {
                Dispose();
                speed = 0;
            }
        }
    }
}

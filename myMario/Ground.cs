using System;
using System.Threading;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace myMario
{
    internal class Ground
    {
        public ContentManager content;
        public Player player;
        public Texture2D texture;
        public Vector2 position;
        public int width, height;
        public Rectangle boxCollider;

        public Rectangle bounds, right, left, top;


        public Ground(ContentManager content, Player player, String tex, int x, int y)
        {
            this.player = player;
            this.content = content;
            texture = content.Load<Texture2D>(tex);
            width = texture.Width;
            height = texture.Height;
            position = new Vector2(x, y);
            boxCollider = new Rectangle((int)position.X, (int)position.Y, width, height);
            if (tex.Contains("pipe"))
            {
                left = new Rectangle((int)position.X, (int)position.Y, 5, height);
                right = new Rectangle((int)position.X + width - 5, (int)position.Y, 5, height);
            }
        }

        public void Refresh()
        {
            boxCollider.X = (int)position.X;
            boxCollider.Y = (int)position.Y;
        }

        public void NewBot()
        {
            Refresh();
            if (boxCollider.Intersects(player.getBot()))
            {
                player.velocity.Y = 0;
                player.position.Y = boxCollider.Y - player.height;
                player.jumping = false;
                player.collusingBottom = true;
            }
            else
            {
                player.collusingBottom = false;
            }
        }

        public virtual void NewTop()
        {
            Refresh();
            if (boxCollider.Intersects(player.getT()))
            {
                if (!player.collusingTop)
                {
                    player.velocity.Y = 0;
                    player.jumping = true;
                    player.position.Y = boxCollider.Y + height;

                    player.collusingTop = true;
                }
            }
            else
            {
                player.collusingTop = false;
            }
        }

        public void NewLeft()
        {
            Refresh();

            if (boxCollider.Intersects(player.getL()))
            {

                player.velocity.X = 0;
                player.position.X = boxCollider.X + width;
                player.collusingLeft = true;

            }
            else
            {
                player.collusingLeft = false;
            }
        }

        public void NewRight()
        {
            Refresh();
            if (boxCollider.Intersects(player.getR()))
            {
                if (!player.collusingRight)
                {
                    player.velocity.X = 0;
                    player.position.X = boxCollider.X - player.currenttexture.Width;
                    player.collusingRight = true;
                }
            }
            else
            {
                player.collusingRight = false;
            }
        }
        public void CheckMonsterCollision(Monster mon)
        {
            Refresh();
            if (boxCollider.Intersects(mon.getLeft()))
            {
                mon.rotation = 0;
            }

            if (boxCollider.Intersects(mon.getRight()))
            {
                mon.rotation = 1;
            }
        }

        public void CheckCollision()
        {
            NewBot();
            NewTop();
            NewRight();
            NewLeft();
        }

        public Rectangle GetBounds()
        {
            Refresh();
            return bounds;
        }
        public Rectangle GetLeft()
        {
            Refresh();
            return left;
        }
        public Rectangle GetRight()
        {
            Refresh();
            return right;
        }
    }
}

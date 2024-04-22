using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace myMario
{
    class MushroomGround : Ground
    {
        public Mushroom m;
        int counter = 1;
        public bool addedToList = false;
        public Texture2D off;
        public MushroomGround(ContentManager content, Player player, string tex, int x, int y) : base(content, player, tex, x, y)
        {
            off = content.Load<Texture2D>("Texture/kutu_off");
        }

        public bool mushroomActivated()
        {
            if (m != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void newTop()
        {
            refresh();
            if (boxCollider.Intersects(player.getT()))
            {
                if (counter == 1 && m == null)
                {
                    counter--;
                    m = new Mushroom(content, player, "Texture/mantar", (int)position.X, (int)position.Y - 48);
                    texture = off;
                }
                if (!player.collusingTop)
                {
                    player.velocity.Y = 0;
                    player.jumping = true;
                    player.collusingTop = true;
                    player.position.Y = boxCollider.Y + height;
                }
            }
            else
            {
                player.collusingTop = false;
            }
        }


    }
}

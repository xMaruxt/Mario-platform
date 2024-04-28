using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace myMario
{
    class CoinGround : MushroomGround
    {
        int counter = 0;
        static SoundEffect altin;
        public CoinGround(int sayac, ContentManager content, Player player, string tex, int x, int y) : base(content, player, tex, x, y)
        {
            counter = sayac;
            altin = content.Load<SoundEffect>("Songs/altin");
        }
        public override void NewTop()
        {
            Refresh();
            if (boxCollider.Intersects(player.getT()))
            {
                if (counter > 0)
                {
                    counter--;
                    altin.Play(1, 0, 0);
                    player.collectedPoints += 10;
                    player.score += 10;
                    player.updatestats(player);
                    if (counter == 0)
                    {
                        texture = off;
                    }
                    
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

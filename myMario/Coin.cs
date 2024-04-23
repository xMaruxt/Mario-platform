using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myMario
{
    class Coin : Collectible
    {
        
        static SoundEffect altin;
        public readonly int PointValue = 10;
       
        public Coin(ContentManager content, Player player, string tex, int x, int y) : base(content, player, tex, x, y)
        {
            altin = content.Load<SoundEffect>("Songs/altin");
        }

        public override void collect()
        {
            altin.Play(1, 0, 0);
            player.collectedPoints += PointValue;
            player.score += PointValue;
            player.updatestats(player);
        }

    }
}

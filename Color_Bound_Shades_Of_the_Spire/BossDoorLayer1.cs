using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Color_Bound_Shades_Of_the_Spire
{
    public class BossDoorLayer1
    {
        public Texture2D T;
        public Rectangle R;

        public BossDoorLayer1(Texture2D t, Rectangle r)
        {
            T = t;
            R = r;
        }

        public void colision(Player player, Level level)
        {
            if (player.hasBlueKey && player.hasRedKey && player.hasYellowKey && player.rec.Intersects(R))
            {
                level.room++;
                level.initial = true;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(T, R, Color.White);
        }
    }
}

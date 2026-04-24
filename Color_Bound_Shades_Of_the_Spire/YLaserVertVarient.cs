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
    public class YLaserVertVarient
    {
        public Texture2D T;
        public Rectangle R;
        public bool isOn;
        public YLaserVertVarient(Texture2D t, Rectangle r)
        {
            T = t;
            R = r;
            isOn = true;
        }

        public void colision(Player player, Texture2D[] texs)
        {
            if (player.rec.Intersects(R) && isOn)
            {
                player.dead = true;
            }
            if (isOn == false)
            {
                T = texs[7];
            }
        }
    }
}

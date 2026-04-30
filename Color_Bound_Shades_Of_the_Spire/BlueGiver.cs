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
    class BlueGiver
    {
        Texture2D T;
        Rectangle R;
        public bool isTouched;

        public BlueGiver(Texture2D T, Rectangle R)
        {
            this.R = R;
            this.T = T;
        }

        public void colision(Player player, Level level)
        {
            if(player.rec.Intersects(R) && player.color == Color.Blue)
            {
                player.charged = true;
            }
            else
            {
                player.charged = false;
            }
        }

    }
}

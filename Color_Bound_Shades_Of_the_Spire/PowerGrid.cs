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
    public class PowerGrid
    {
        Texture2D T;
        Rectangle R;
        public int numReciever;

        public PowerGrid(Texture2D t, Rectangle r, Level level)
        {
            T = t;
            R = r;
            numReciever = level.YRList.Count;
        }

        public void colision(Player player, Level level)
        {
            int numOn = 0;
            if (player.rec.Intersects(R))
            {
                for (int i = 0; i < level.YRList.Count; i++)
                {
                    if (level.YRList[i].isOn)
                    {
                        numOn++;
                    }
                }
                if (numOn == numReciever)
                {
                    for (int i = 0; i < level.YLVVList.Count; i++)
                    {
                        level.YLVVList[i].isOn = false;
                    }
                }
            }
        }
    }
}

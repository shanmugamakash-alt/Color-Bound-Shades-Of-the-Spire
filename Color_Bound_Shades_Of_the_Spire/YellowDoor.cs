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
    public class YellowDoor
    {
        Texture2D T;
        Rectangle R;
        public bool isOpen;
        public YellowDoor(Texture2D t, Rectangle r)
        {
            T = t;
            R = r;
            isOpen = false;
        }

        public void colision(Player player, Level level)
        {
            bool allOn = true; 
            for (int i = 0; i < level.YRList.Count; i++)
            {
                if (!level.YRList[i].isOn)
                {
                    allOn = false;
                    break;
                }
            }
            if (allOn && player.rec.Intersects(R))
            {
                level.initial = true;
                level.room += 1;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(T, R, Color.White);
        }
    }
}

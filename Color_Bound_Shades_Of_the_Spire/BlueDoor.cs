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
    public class BlueDoor
    {
        Texture2D T;
        Rectangle R;
        public bool isOpen;

        public BlueDoor(Texture2D T, Rectangle R)
        {
            this.T = T;
            this.R = R;
            isOpen = false;
        }

        public void openDoor(Player player, Level level)
        {
            if(isOpen && player.rec.Intersects(R))
            {
                level.initial = true;
                level.room+= 1;
            }
            if (level.room == 0)
            {
                isOpen = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(T, R, Color.White);
        }
    }
}

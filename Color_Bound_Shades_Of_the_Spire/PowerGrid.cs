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
        public Texture2D T;
        public Rectangle R;
        public bool isDestroyed;

        public PowerGrid(Texture2D t, Rectangle r)
        {
            T = t;
            R = r;
            isDestroyed = false;
        }

        public void colision(Player player, Level level)
        {
            if (player.rec.Intersects(R) && player.ultraCharged)
            {
                for (int i = 0; i < level.YLVVList.Count; i++)
                {
                    level.YLVVList[i].isOn = false;
                }
                for (int i = 0; i < level.YLHVList.Count; i++)
                {
                    level.YLHVList[i].isOn = false;
                }
                isDestroyed = true;
            }
            if (isDestroyed)
            {
                T = level.Textures[34];
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(T, R, Color.White);
        }
    }
}

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
    public class YellowReciever
    {
        public Texture2D T;
        public Rectangle R;
        public bool isOn;
        public YellowGiver activatedBy;
        public YellowReciever(Texture2D t, Rectangle r) 
        {
            T = t;
            R = r;
            isOn = false;
        }
        public void colision(Player player, Texture2D[] textures, YellowGiver giver)
        {
            if (player.charged && player.rec.Intersects(R))
            {
                isOn = true;
                T = textures[14];
                activatedBy = giver;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(T, R, Color.White);
        }
    }
}

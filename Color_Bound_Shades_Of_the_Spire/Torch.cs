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
    public class Torch
    {
        Texture2D baseT;
        Texture2D litT;
        Rectangle rect;
        public bool lit;
        int litTimer;
        int litLimit;

        public Torch(Texture2D b, Texture2D l, Rectangle r)
        {
            baseT = b;
            litT = l;
            rect = r;
            lit = false;
            litTimer = 0;
            litLimit = 300;
        }


        public void Update(Player player)
        {
            if (player.rec.Intersects(rect) && player.color == Color.Red)
            {
                lit = true;
                litTimer = 0;
                Console.WriteLine(lit);
            }

            if (lit)
            {
                litTimer++;
                if (litTimer >= litLimit)
                {
                    lit = false;
                    litTimer = 0;
                    Console.WriteLine(lit);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (lit)
            {
                spriteBatch.Draw(litT, rect, Color.White);
            }
            else
            {
                spriteBatch.Draw(baseT, rect, Color.White);
            }
        }
    }



}

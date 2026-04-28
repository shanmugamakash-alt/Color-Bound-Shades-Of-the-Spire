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
    public class ColorCollectable
    {
        public Color color;
        Rectangle rect;
        Texture2D tex;
        Color ogColor;
        int pickupTimer;
        int cooldown;

        public ColorCollectable(Texture2D t, Rectangle r, Color c, int timer)
        {
            color = c;
            ogColor = c;
            tex = t;
            rect = r;
            cooldown = timer;
            pickupTimer = 0;
        }

        public void Update(Player player, Bar UI)
        {
            if (player.rec.Intersects(rect) && pickupTimer == 0)
            {
                if(color == Color.Red && UI.redSize <= UI.background.Width)
                {
                    UI.redSize += 30;
                    pickupTimer = cooldown;
                    UI.showColor(this);
                    color = Color.White;
                }
                else if (color == Color.Yellow && UI.yellowSize <= UI.background.Width)
                {
                    UI.yellowSize += 30;
                    pickupTimer = cooldown;
                    UI.showColor(this);
                    color = Color.White;
                    
                }
                else if (color == Color.Blue && UI.blueSize <= UI.background.Width)
                {
                    UI.blueSize += 30;
                    pickupTimer = cooldown;
                    UI.showColor(this);
                    color = Color.White;
                    
                }
            }

            if(pickupTimer > 0)
            {
                pickupTimer--;
            }
            else
            {
                color = ogColor;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, rect, color);
        }
    }
}

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
    class Player
    {
        Texture2D tex;
        Rectangle rec;
        int height, width;
        float velocity;
        float gravity;
        
        public Player(Texture2D t, Rectangle r, int maxwidth, int maxheight)
        {
            width = maxwidth;
            height = maxheight;
            tex = t;
            rec = r;
            velocity = 0f;
            gravity = 0f;
        }

        public void move(KeyboardState kb)
        {
            
            if(kb.IsKeyDown(Keys.Right))
            {
                velocity += 1f;
            }
            if (kb.IsKeyDown(Keys.Left))
            {

                velocity -= 1f;
            }
            int i = 0;
            if (kb.IsKeyDown(Keys.Up) && i == 0 && rec.Y >= 100)
            {
                gravity -= 50f;
                i = 1;
            }
            else if(kb.IsKeyUp(Keys.Up))
            {
                i = 0;
            }
            
            rec.Y -= (int)gravity;
            if (gravity < 100)
            {
                gravity *= 1.02f;
            }
            

            
            velocity *= 0.95f;
            rec.X += (int)velocity;
            if (rec.X + rec.Width >= width)
            {
                rec.X = width - rec.Width - 1;
                
            }

            if (rec.X < 0)
            {
                rec.X = 0;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(tex, rec, Color.Red);
        }
    }
}

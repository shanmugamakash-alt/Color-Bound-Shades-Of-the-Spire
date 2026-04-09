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
    class Bar
    {
        Rectangle bar;
        Rectangle background;
        double redSize;
        double blueSize;
        double yellowSize;
        Texture2D tex;
        Rectangle red;
        Rectangle blue;
        Rectangle yellow;

        public Bar(Texture2D tex)
        {
            bar = new Rectangle(10, 10, 75, 150);
            redSize = 150;
            blueSize = 150;
            yellowSize = 150;
            background = new Rectangle(10, 10, 75, 150);
            red = new Rectangle(10, 100, 50,50);
            yellow = new Rectangle(80, 100, 50, 50);
            blue = new Rectangle(150, 100, 50, 50);
        }

        public void Update(KeyboardState kb, KeyboardState oldKB, Player p)
        {
            if (kb.IsKeyDown(Keys.D1) && kb != oldKB)
            {
                if (p.color == Color.White)
                    p.ChangeColor(Color.Red);
                else
                    p.ChangeColor(Color.White);
            }

            if (kb.IsKeyDown(Keys.D2))
            {
                p.ChangeColor(Color.Yellow);
            }

            if (kb.IsKeyDown(Keys.D3))
            {
                p.ChangeColor(Color.Blue);
            }

            if (p.color != Color.White)
            {
                bar.Width--;
            }
            else if (bar.Width < 400)
            {
                bar.Width++;
            }

            if (bar.Width <= 0)
            {
                p.ChangeColor(Color.White);
            }

        }
    }
}

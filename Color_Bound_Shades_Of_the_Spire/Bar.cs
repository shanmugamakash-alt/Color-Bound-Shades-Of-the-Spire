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
        string currentColor;
        Texture2D tex;
        Texture2D barTex;
        Texture2D baseTex;
        Rectangle red;
        Rectangle blue;
        Rectangle yellow;

        public Bar(Texture2D tex, Texture2D baseTex)
        {
            bar = new Rectangle(10, 10, 150, 75);
            redSize = 300;
            blueSize = 300;
            yellowSize = 300;
            background = new Rectangle(10, 10, 150, 75);
            red = new Rectangle(10, 150, 50,50);
            yellow = new Rectangle(80, 150, 50, 50);
            blue = new Rectangle(150, 150, 50, 50);
            currentColor = "white";
            this.tex = tex;
            this.baseTex = baseTex;
            barTex = baseTex;
        }

        public void Draw(SpriteBatch spriteBatch, Player p)
        {
            spriteBatch.Draw(tex, background, Color.Black);
            spriteBatch.Draw(barTex, bar,p.color);
            spriteBatch.Draw(tex, red, Color.Red);
            spriteBatch.Draw(tex, yellow, Color.Yellow);
            spriteBatch.Draw(tex, blue, Color.Blue);
        }
        public void Update(KeyboardState kb, KeyboardState oldKB, Player p)
        {
            if (kb.IsKeyDown(Keys.D1) && kb != oldKB && redSize >= 10)
            {
                if (p.color != Color.Red)
                {
                    p.ChangeColor(Color.Red);
                    currentColor = "red";
                    barTex = tex;
                }
                else
                {
                    p.ChangeColor(Color.White);
                    currentColor = "white";
                    barTex = baseTex;
                }

            }

            if (kb.IsKeyDown(Keys.D2) && kb != oldKB && yellowSize >= 10)
            {
                if (p.color != Color.Yellow)
                {
                    p.ChangeColor(Color.Yellow);
                    currentColor = "yellow";
                    barTex = tex;
                }
                else
                {
                    p.ChangeColor(Color.White);
                    currentColor = "white";
                    barTex = baseTex;
                }

            }

            if (kb.IsKeyDown(Keys.D3) && kb != oldKB && blueSize >= 10)
            {
                if (p.color != Color.Blue)
                {
                    p.ChangeColor(Color.Blue);
                    currentColor = "blue";
                    barTex = tex;
                }
                else
                {
                    p.ChangeColor(Color.White);
                    currentColor = "white";
                    barTex = baseTex;
                }

            }

            switch (currentColor)
            {
                case "white":
                    redSize++;
                    yellowSize++;
                    blueSize++;

                    bar.Width = 250;
                    break;
                case "red":
                    redSize--;
                    yellowSize++;
                    blueSize++;
                    bar.Width = (int)redSize/2;
                    break;
                case "yellow":
                    redSize++;
                    yellowSize--;
                    blueSize++;
                    bar.Width = (int)yellowSize / 2;
                    break;
                case "blue":
                    redSize--;
                    yellowSize++;
                    blueSize--;
                    bar.Width = (int)blueSize / 2;
                    break;

            }
            if (redSize <= 0)
            {
                p.ChangeColor(Color.White);
                redSize = 0;
                currentColor = "white";
                barTex = baseTex;
            }

            if (blueSize <= 0)
            {
                p.ChangeColor(Color.White);
                blueSize = 0;
                currentColor = "white";
                barTex = baseTex;
            }

            if (yellowSize <= 0)
            {
                p.ChangeColor(Color.White);
                yellowSize = 0;
                currentColor = "white";
                barTex = baseTex;
            }

            if (redSize >= 300)
                redSize = 300;
            if ( yellowSize >= 300)
                yellowSize = 300;
            if (blueSize >= 300)
                blueSize = 300;


        }
    }

}

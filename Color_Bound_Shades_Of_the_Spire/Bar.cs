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
    public class Bar
    {
        Rectangle bar;
        public Rectangle background;
        public double redSize;
        public double blueSize;
        public double yellowSize;
        bool isShowing;
        int showTime;
        int limit;
        string currentColor;
        Texture2D tex;
        Texture2D baseTex;
        Rectangle red;
        Rectangle blue;
        Rectangle yellow;
        Color barColor;

        public Bar(Texture2D tex)
        {
            bar = new Rectangle(10, 10, 300, 75);
            redSize = 0;
            blueSize = 0;
            yellowSize = 0;
            background = new Rectangle(10, 10, 300, 75);
            red = new Rectangle(10, 100, 50,50);
            yellow = new Rectangle(80, 100, 50, 50);
            blue = new Rectangle(150, 100, 50, 50);
            currentColor = "white";
            this.tex = tex;
            isShowing = false;
            showTime = 60;
            limit = 60;
            barColor = Color.White;
        }


        public void Draw(SpriteBatch spriteBatch, Player p)
        {
            spriteBatch.Draw(tex, background, Color.Black);
            if(!isShowing)
                spriteBatch.Draw(tex, bar,p.color);
            else
                spriteBatch.Draw(tex, bar, barColor);
            spriteBatch.Draw(tex, red, Color.Red);
            spriteBatch.Draw(tex, yellow, Color.Yellow);
            spriteBatch.Draw(tex, blue, Color.Blue);
        }
        public void Update(KeyboardState kb, KeyboardState oldKB, Player p)
        {
            if(showTime < limit)
            {
                showTime++;
            }
            else
            {
                isShowing = false;
            }
            if (kb.IsKeyDown(Keys.D1) && kb != oldKB && redSize >= 10 && p.dead == false)
            {
                showTime = limit;
                isShowing = false;
                if (p.color != Color.Red)
                {
                    p.ChangeColor(Color.Red);
                    currentColor = "red";
                }
                else
                {
                    p.ChangeColor(Color.White);
                    currentColor = "white";
                }

            }

            if (kb.IsKeyDown(Keys.D2) && kb != oldKB && yellowSize >= 10 && p.dead == false)
            {
                showTime = limit;
                isShowing = false;
                if (p.color != Color.Yellow)
                {
                    p.ChangeColor(Color.Yellow);
                    currentColor = "yellow";
                }
                else
                {
                    p.ChangeColor(Color.White);
                    currentColor = "white";
                }

            }

            if (kb.IsKeyDown(Keys.D3) && kb != oldKB && blueSize >= 10 && p.dead == false)
            {
                showTime = limit;
                isShowing = false;
                if (p.color != Color.Blue)
                {
                    p.ChangeColor(Color.Blue);
                    currentColor = "blue";
                }
                else
                {
                    p.ChangeColor(Color.White);
                    currentColor = "white";
                }

            }

            switch (currentColor)
            {
                case "white":
                    if(isShowing == false)
                        bar.Width = 300;
                    break;
                case "red":
                    redSize--;
                    bar.Width = (int)redSize;
                    break;
                case "yellow":
                    yellowSize--;
                    bar.Width = (int)yellowSize;
                    break;
                case "blue":
                    blueSize--;
                    bar.Width = (int)blueSize;
                    break;

            }
            if (redSize < 0)
            {
                p.ChangeColor(Color.White);
                redSize = 0;
                currentColor = "white";
            }

            if (blueSize < 0)
            {
                p.ChangeColor(Color.White);
                blueSize = 0;
                currentColor = "white";
            }

            if (yellowSize < 0)
            {
                p.ChangeColor(Color.White);
                yellowSize = 0;
                currentColor = "white";
            }

            if (redSize >= 300)
                redSize = 300;
            if ( yellowSize >= 300)
                yellowSize = 300;
            if (blueSize >= 300)
                blueSize = 300;


        }

        public void showColor(ColorCollectable collectable)
        {
            isShowing = true;
            showTime = 0;
            barColor = collectable.color;
            if (barColor == Color.Red)
                bar.Width = (int)redSize;
            else if (barColor == Color.Yellow)
                bar.Width = (int)yellowSize;
            else if (barColor == Color.Blue)
                bar.Width = (int)blueSize;
        }
    }

}

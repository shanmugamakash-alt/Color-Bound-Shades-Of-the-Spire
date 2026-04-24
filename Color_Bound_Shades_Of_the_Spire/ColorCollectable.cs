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
    class ColorCollectable
    {
        Color color;
        Rectangle rect;
        Texture2D tex;
        Color ogColor;
        int pickupOrder;

        public ColorCollectable(Texture2D t, Rectangle r, Color c, int order)
        {
            color = c;
            ogColor = c;
            tex = t;
            rect = r;
            pickupOrder = order;
        }
    }
}

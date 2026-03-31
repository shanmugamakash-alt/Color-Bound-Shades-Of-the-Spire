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
    public class Tile
    {
        Texture2D T;
        Rectangle R;
        public Tile(Texture2D t, Rectangle r)
        {
            T = t;
            R = r;
        }

        public Texture2D GetTex()
        {
            return T;
        }
        public Rectangle GetRec()
        {
            return R;
        }
    }
}

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
        public enum TileType
        { 
            air,
            floor,
            spike,
            YLaserVert,
            YLaserHoriz,
            checkpoint,
            key,
            yellowKey,
            redKey,
            blueKey,
            bossDoor1,
            exit,
            keyDoor,
            start,
            LevelHub,
            RedEntrance,
            BlueEntrance,
            YellowEntrance,
            water,
            TextTrigger
        }
        Texture2D T;
        Rectangle R;
        TileType TT;
        public Tile(Texture2D t, Rectangle r, TileType tt)
        {
            T = t;
            R = r;
            TT = tt;
        }

        public Texture2D GetTex()
        {
            return T;
        }
        public Rectangle GetRec()
        {
            return R;
        }
        public void ChangeRec(Rectangle r)
        {
            R = r;
        }
        public TileType returnType()
        {
            return TT;
        }
        public void setTileType(TileType tt)
        {
            TT = tt;
        }
        public void setTex(Texture2D t)
        {
            T = t;
        }
    }
}

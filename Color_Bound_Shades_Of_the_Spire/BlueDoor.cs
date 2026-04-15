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
    class Bluedoor
    {
        Tile[,] tiles;
        public Bluedoor()
        {
            
        }


        public void nextRoom(Player player, Level level)
        {
            if(player.rec.Intersects(tiles[0, 1].GetRec()))
            {

            }
        }

        public void tile(Tile[,] t)
        {
            tiles = t;
        }
    }
}

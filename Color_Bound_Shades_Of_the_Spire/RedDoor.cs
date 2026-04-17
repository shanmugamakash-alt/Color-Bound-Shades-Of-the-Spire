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
    public class RedDoor
    {
        Texture2D T;
        Rectangle R;
        public bool isOpen;
        public RedDoor(Texture2D t, Rectangle r)
        {
            T = t;
            R = r;
            isOpen = false;
        }

        public void collision(Player player, Level level)
        {
            bool allLit = true;
            bool enemiesDead = true;
            for (int i = 0; i < level.torchList.Count; i++)
            {
                if (!level.torchList[i].lit)
                {
                    allLit = false;
                    break;
                }
            }
            if (level.EnemyList.Count == 0)
                enemiesDead = true;
            else
                enemiesDead = false;
            if (allLit && enemiesDead && player.rec.Intersects(R))
            {
                level.initial = true;
                level.room += 1;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(T, R, Color.White);
        }
    }
}


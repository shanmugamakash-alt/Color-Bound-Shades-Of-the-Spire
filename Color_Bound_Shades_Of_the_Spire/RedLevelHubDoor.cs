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
    public class RedLevelHubDoor
    {
        Texture2D T;
        Rectangle R;
        public bool isOpen;
        int cooldown;
        public RedLevelHubDoor(Texture2D t, Rectangle r)
        {
            T = t;
            R = r;
            isOpen = false;
            cooldown = 0;
        }

        public void collision(Player player, Level level, LevelLoader LL)
        {
            bool allLit = true;
            bool enemiesDead = true;
            if (cooldown > 0)
                cooldown--;
            for (int i = 0; i < level.torchList.Count; i++)
            {
                if (!level.torchList[i].lit)
                {
                    allLit = false;
                    break;
                }
            }
            for (int i = 0; i < level.EnemyList.Count; i++)
            {
                if (!level.EnemyList[i].dead)
                {
                    enemiesDead = false;
                    break;
                }
            }

            if (allLit && enemiesDead && player.rec.Intersects(R) && cooldown <= 0 && player.hasRedKey)
            {
                cooldown = 10;
                LL.CurrentLevel = (LevelLoader.currentLevel)5;
                LL.levels[4].initial = true;
                LL.levels[4].Hint = "";
                player.keyCount = 0;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(T, R, Color.White);
        }
    }
}
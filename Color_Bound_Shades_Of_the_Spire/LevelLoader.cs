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
    //this is for level controlls, so level selection would go here and passing other values
    public class LevelLoader
    {
        public List<Level> levels;
        public currentLevel CurrentLevel;
        public enum currentLevel
        { 
            level1 = 1,
            level2 = 2,
            level3 = 3,
            level4 = 4,
            levelHub = 5
        }
        public LevelLoader(string[][] fileNames, Texture2D[][] Textures, int level)
        {
            levels = new List<Level>();
            CurrentLevel = (currentLevel)level;
            for (int i = 0; i < fileNames.Length; i++)
            {
                levels.Add(new Level(fileNames[i], Textures[i]));
            }
        }
        public void Update(Player player, KeyboardState kb, KeyboardState oldKB)
        {
            levels[(int)CurrentLevel - 1].Update(player, kb, oldKB, this);
        }
        public void DrawAll(SpriteBatch spriteBatch, Player player)
        {
            levels[(int)CurrentLevel - 1].DrawAll(spriteBatch, player);
        }

    }
}

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
        List<Level> levels;
        public LevelLoader(string[] fileNames, Texture2D[][] Textures)
        {
            levels = new List<Level>();
            for (int i = 0; i < fileNames.Length; i++)
            {
                levels.Add(new Level(fileNames[i], Textures[i]));
            }
        }
        public void DrawAll(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                levels[i].DrawAll(spriteBatch);
            }
        }
    }
}

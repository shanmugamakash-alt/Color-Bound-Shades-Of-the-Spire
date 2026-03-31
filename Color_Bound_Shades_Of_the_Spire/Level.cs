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
using System.IO;

namespace Color_Bound_Shades_Of_the_Spire
{
    public class Level
    {
        string fileName;
        Tile[,] tiles;
        Texture2D[] Textures;
        public Level(string fileName, Texture2D[] textures)
        {
            this.fileName = fileName;
            tiles = new Tile[10, 10];
            Textures = new Texture2D[textures.Length];
            Textures = textures;
            LoadTiles(this.fileName);
        }

        public void LoadTiles(string fileName)
        {
            int x = 0;
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] tiles = line.Split(',');
                        for (int i = 0; i < tiles.Length; i++)
                        {
                            LoadTile(tiles[i], x, i);
                        }
                        x++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("file not read");
                Console.WriteLine(e.Message);
            }
        }
        public void LoadTile(string tile,int x,int y)
        {
            switch (tile)
            {
                case "f":
                    tiles[x,y] = new Tile(Textures[0], new Rectangle(y * 50, x * 50, 50, 50));
                    break;
                case "0":
                    tiles[x, y] = new Tile(Textures[1], new Rectangle(x * 50, y * 50, 50, 50));
                    break;
            }
        }

        public void DrawAll(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(0); j++)
                {
                    spriteBatch.Draw(tiles[i, j].GetTex(), tiles[i, j].GetRec(), Color.White);
                }
            }
        }
    }
}

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
        public int tileSize;
        public int offset, velocityX;
        public Level(string fileName, Texture2D[] textures)
        {
            this.fileName = fileName;
            tiles = new Tile[20, 50];
            tileSize = 100;
            offset = 0;
            velocityX = 0;
            Textures = textures;
            LoadTiles(this.fileName);
        }
        //add player as a paramter to check if its reached the middle of the screen, then move to the left (change what you have cuase its useless)
        public void Update(KeyboardState kb)
        {
            if (kb.IsKeyDown(Keys.Left))
            { 
                velocityX += 3;
            }
            if (kb.IsKeyDown(Keys.Right))
            {
                velocityX -= 3;
            }

            if (velocityX > 0) 
                velocityX--;
            if (velocityX < 0) 
                velocityX++;
            if (velocityX > 7)
                velocityX = 7;
            if (velocityX < -7)
                velocityX = -7;

            offset += velocityX;

            if (offset > 0)
            {
                offset = 0;
                velocityX = 0;
            }

            if (offset < -3100)
            {
                offset = -3100;
                velocityX = 0;
            }
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
                    tiles[x,y] = new Tile(Textures[0], new Rectangle(y * tileSize + offset, x * tileSize + offset, tileSize, tileSize), Tile.TileType.floor);
                    break;
                case "0":
                    tiles[x, y] = new Tile(Textures[1], new Rectangle(y * tileSize + offset, x * tileSize + offset, tileSize, tileSize), Tile.TileType.wall);
                    break;
                case "s":
                    tiles[x, y] = new Tile(Textures[2], new Rectangle(y * tileSize + offset, x * tileSize + offset, tileSize, tileSize), Tile.TileType.spike);
                    break;

            }
        }

        public void DrawAll(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    Rectangle r = tiles[i, j].GetRec();
                    Rectangle drawRect = new Rectangle( r.X + offset,r.Y,r.Width, r.Height);

                    spriteBatch.Draw(tiles[i, j].GetTex(), drawRect, Color.White);
                }
            }
        }
    }
}

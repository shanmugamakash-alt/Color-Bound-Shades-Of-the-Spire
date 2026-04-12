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
        string[] fileNames;
        Tile[,] tiles;
        Texture2D[] Textures;
        public int room;
        public int tileSize;
        public int offset, velocityX;
        public bool initial;
        public int checkpoint;
        public float scale;
        public Level(string[] fileNames, Texture2D[] textures)
        {
            this.fileNames = fileNames;
            tileSize = 100;
            offset = 0;
            velocityX = 0;
            room = 0;
            scale = 1;
            Textures = textures;
            initial = true;
            checkpoint = 0;
            LoadTiles(this.fileNames);
        }

        //add player as a paramter to check if its reached the middle of the screen, then move to the left (change what you have cuase its useless)
        public void Update(Player player, KeyboardState kb, LevelLoader LL)
        {
            player.collision(tiles, this, LL);
            if (room != player.room)
            {
                LoadTiles(fileNames);
                room = player.room;
            }
            player.move(kb, this);
            player.UpdateRectangle();
        }
        public void LoadTiles(string[] fileNames)
        {
            int x = 0;
            int y = 0;
            string path = fileNames[room];
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] tiles = line.Split(',');
                        if (x == 0)
                        {
                            this.tiles = new Tile[int.Parse(tiles[0]), int.Parse(tiles[1])];
                            scale = (float)int.Parse(tiles[2]) / 100f;
                            tileSize = int.Parse(tiles[2]);
                        }
                        else
                        {
                            for (int i = 0; i < tiles.Length; i++)
                            {
                                LoadTile(tiles[i], i, y);
                            }
                            y++;
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
                    tiles[x,y] = new Tile(Textures[0], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.floor);
                    break;
                case "0":
                    tiles[x, y] = new Tile(Textures[1], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.air);
                    break;
                case "s":
                    tiles[x, y] = new Tile(Textures[2], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.spike);
                    break;
                case "E":
                    tiles[x, y] = new Tile(Textures[2], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.exit);
                    break;
                case "S":
                    tiles[x, y] = new Tile(Textures[1], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.start);
                    break;
                case "c":
                    tiles[x, y] = new Tile(Textures[3], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.checkpoint);
                    break;
                case "k":
                    tiles[x, y] = new Tile(Textures[4], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.key);
                    break;

            }
        }
        public void DrawAll(SpriteBatch spriteBatch)
        {
            if (tiles == null) return;
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j] != null)
                    {
                        Rectangle r = tiles[i, j].GetRec();
                        Rectangle drawRect = new Rectangle(r.X + offset, r.Y, r.Width, r.Height);

                        if (tiles[i, j].GetTex() != null)
                        {
                            spriteBatch.Draw(tiles[i, j].GetTex(), drawRect, Color.White);
                        }
                        else
                            spriteBatch.Draw(Textures[1], drawRect, Color.White);
                    }
                }
            }
        }
    }
}

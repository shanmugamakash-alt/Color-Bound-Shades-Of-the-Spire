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
        public Texture2D[] Textures;
        public int room;
        public int tileSize;
        public int offset, velocityX;
        public bool initial;
        public bool playerInitial;
        public int checkpoint;
        public float scale;
        public bool levelComplete;
        public List<YellowGiver> YGList;
        public List<YellowReciever> YRList;
        public List<YellowDoor> YDList;
        public List<YLaserVertVarient> YLVVList;
        public List<YLaserHorizVarient> YLHVList;
        public List<Torch> torchList;
        public List<RedDoor> RDList;
        public List<Enemy> EnemyList;
        public Random rand = new Random();
        public Level(string[] fileNames, Texture2D[] textures)
        {
            this.fileNames = fileNames;
            levelComplete = false;
            tileSize = 100;
            offset = 0;
            velocityX = 0;
            scale = 1;
            room = 0;
            Textures = textures;
            initial = true;
            playerInitial = true;
            checkpoint = 0;
            YGList = new List<YellowGiver>();
            YRList = new List<YellowReciever>();
            YDList = new List<YellowDoor>();
            torchList = new List<Torch>();
            RDList = new List<RedDoor>();
            EnemyList = new List<Enemy>();
            YLVVList = new List<YLaserVertVarient>();
            YLHVList = new List<YLaserHorizVarient>();
            LoadTiles(this.fileNames);
        }

        public void Update(Player player, KeyboardState kb, LevelLoader LL)
        {
            player.move(kb, this);
            player.UpdateRectangle();
            player.collision(tiles, this, LL);
            for (int i = 0; i < YGList.Count; i++)
            {
                YGList[i].colision(player);
            }
            for (int i = 0; i < EnemyList.Count; i++)
            {
                EnemyList[i].Update(tiles, player, this);
            }
            for (int i = 0; i < torchList.Count; i++)
            {
                torchList[i].collision(player);
            }
            for (int i = 0; i < RDList.Count; i++)
            {
                RDList[i].collision(player,this);
            }
            for (int i = 0; i < YRList.Count; i++)
            {
                for (int j = 0; j < YGList.Count; j++)
                {
                    if (YGList[j].touched && YRList[i].activatedBy == null)
                        YRList[i].colision(player, Textures, YGList[j]);
                }
            }
            for (int i = 0; i < YDList.Count; i++)
            {
                YDList[i].colision(player, this);
            }
            if (initial)
            {
                playerInitial = true;
                YGList.Clear();
                YRList.Clear();
                YDList.Clear();
                torchList.Clear();
                RDList.Clear();
                EnemyList.Clear();

                LoadTiles(fileNames);
                initial = false;
            }
            if (levelComplete)
            {
                LL.CurrentLevel = (LevelLoader.currentLevel)5;
            }
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
                //basic items (dungeon)
                case "f":
                    tiles[x,y] = new Tile(Textures[0], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.floor);
                    break;
                case "0":
                    int texNum = rand.Next(8, 10);
                    tiles[x, y] = new Tile(Textures[texNum], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.air);
                    break;
                case "E":
                    tiles[x, y] = new Tile(Textures[2], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.exit);
                    break;
                case "S":
                    int texNum2 = rand.Next(8, 10);
                    tiles[x, y] = new Tile(Textures[texNum2], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.start);
                    break;

                //yellow level items
                case "Yf":
                    tiles[x, y] = new Tile(Textures[6], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.floor);
                    break;
                case "Y0":
                    tiles[x, y] = new Tile(Textures[7], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.air);
                    break;
                case "YDUL":
                    YDList.Add(new YellowDoor(Textures[8], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize)));
                    break;
                case "YDUR":
                    YDList.Add(new YellowDoor(Textures[9], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize)));
                    break;
                case "YDDL":
                    YDList.Add(new YellowDoor(Textures[10], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize)));
                    break;
                case "YDDR":
                    YDList.Add(new YellowDoor(Textures[11], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize)));
                    break;
                case "YR":
                    YRList.Add(new YellowReciever(Textures[13], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize)));
                    break;
                case "YG":
                    YGList.Add(new YellowGiver(Textures[12], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize)));
                    break;
                case "YS":
                    tiles[x, y] = new Tile(Textures[7], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.start);
                    break;
                case "YsL":
                    tiles[x, y] = new Tile(Textures[17], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.spike);
                    break;

                //yellow lasers and varients
                case "YLVT":
                    tiles[x, y] = new Tile(Textures[19], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.YLaserVert);
                    break;
                case "YLVM":
                    tiles[x, y] = new Tile(Textures[20], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.YLaserVert);
                    break;
                case "YLVB":
                    tiles[x, y] = new Tile(Textures[21], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.YLaserVert);
                    break;

                case "YLHL":
                    tiles[x, y] = new Tile(Textures[22], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.YLaserHoriz);
                    break;
                case "YLHM":
                    tiles[x, y] = new Tile(Textures[23], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.YLaserHoriz);
                    break;
                case "YLHR":
                    tiles[x, y] = new Tile(Textures[24], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.YLaserHoriz);
                    break;

                case "YLVVT":

                    break;
                case "YLVVM":

                    break;
                case "YLVVB":

                    break;

                case "YLVHT":

                    break;
                case "YLVHM":

                    break;
                case "YLVHB":

                    break;

                //tutorial items
                case "c":
                    tiles[x, y] = new Tile(Textures[3], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.checkpoint);
                    break;
                case "k":
                    tiles[x, y] = new Tile(Textures[4], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.key);
                    break;
                case "L":
                    tiles[x, y] = new Tile(Textures[4], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.LevelHub);
                    break;
                case "KD":
                    tiles[x, y] = new Tile(Textures[2], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.keyDoor);
                    break;
                case "sU":
                    tiles[x, y] = new Tile(Textures[2], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.spike);
                    break;
                case "sD":
                    tiles[x, y] = new Tile(Textures[5], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.spike);
                    break;
                case "sR":
                    tiles[x, y] = new Tile(Textures[6], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.spike);
                    break;
                case "sL":
                    tiles[x, y] = new Tile(Textures[7], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.spike);
                    break;

                //level hub items
                case "RE":
                    tiles[x, y] = new Tile(Textures[4], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.RedEntrance);
                    break;
                case "BE":
                    tiles[x, y] = new Tile(Textures[4], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.BlueEntrance);
                    break;
                case "YEU":
                    tiles[x, y] = new Tile(Textures[5], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.YellowEntrance);
                    break;
                case "YED":
                    tiles[x, y] = new Tile(Textures[6], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.YellowEntrance);
                    break;
                case "RDU":
                    RDList.Add(new RedDoor(Textures[5], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize)));
                    tiles[x, y] = new Tile(Textures[1], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.air);
                    break;
                case "RDD":
                    RDList.Add(new RedDoor(Textures[6], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize)));
                    break;
                //other things
                case "RT":
                    torchList.Add(new Torch(Textures[7], Textures[8], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize)));
                    tiles[x, y] = new Tile(Textures[1], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.air);
                    break;
                case "eR":
                    EnemyList.Add(new Enemy(Textures[9], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize),3,5));
                    tiles[x, y] = new Tile(Textures[1], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.air);
                    break;
                case "RA":
                    tiles[x, y] = new Tile(Textures[1], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.air);
                    break;
                case "RS":
                    tiles[x, y] = new Tile(Textures[1], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.start);
                    break;
                case "RF":
                    tiles[x, y] = new Tile(Textures[0], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), Tile.TileType.floor);
                    break;


            }
        }
        public void DrawAll(SpriteBatch spriteBatch, Player player)
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
            for (int i = 0; i < YGList.Count; i++)
            {
                YGList[i].Draw(spriteBatch, player, this);
            }
            for (int i = 0; i < YRList.Count; i++)
            {
                YRList[i].Draw(spriteBatch);
            }
            for (int i = 0; i < YDList.Count; i++)
            {
                YDList[i].Draw(spriteBatch);
            }
            for(int i = 0; i < RDList.Count; i++)
            {
                RDList[i].Draw(spriteBatch);
            }
            for(int i = 0; i < torchList.Count; i++)
            {
                torchList[i].Draw(spriteBatch);
            }
            for (int i = 0; i < EnemyList.Count; i++)
            {
                EnemyList[i].Draw(spriteBatch);
            }
        }
    }
}

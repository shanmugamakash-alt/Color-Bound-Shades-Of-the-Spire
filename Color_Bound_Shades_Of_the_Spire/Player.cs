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
    public class Player
    {
        Texture2D tex;
        Texture2D[] textures;
        Tile checkpointTile;
        public Rectangle rec;
        public Vector2 position;
        public Vector2 oldPosition;
        public Vector2 startPos;
        public Vector2 velocity;
        public float gravity;
        public Color color;
        public bool onGround;
        public bool checkPointReached;
        public bool dead;
        public bool isDashing;
        public bool charged;
        public bool ultraCharged;
        public bool hasYellowKey;
        public bool hasRedKey;
        public bool hasBlueKey;
        public bool inwater;
        public int deathTimer;
        public int double_jump;
        public int dash;
        public int dashTimer;
        public int dashDuration;
        public float maxJumpheight;
        public int keyCount;
        public int idleTime;
        int timer;
        KeyboardState oldkb;
        
        public Player(Texture2D[] t, Rectangle r)
        {
            tex = t[0];
            timer = 0;
            textures = t;
            idleTime = 30;
            rec = r;
            position = new Vector2(rec.X, rec.Y);
            startPos = position;
            dead = false;
            maxJumpheight = -20f;
            deathTimer = 45;
            velocity = Vector2.Zero;
            gravity = .75f;
            onGround = false;
            color = Color.White;
            oldkb = Keyboard.GetState();
            double_jump = 2;
            keyCount = 0;
            dash = 1;
            inwater = false;
            dashTimer = 90;
            charged = false;
            ultraCharged = false;
            isDashing = false;
            dashDuration = 12;
            checkPointReached = false;
            hasYellowKey = false;
            hasRedKey = false;
            hasBlueKey = false;
        }

        public void move(KeyboardState kb, Level level)
        {
            MouseState mouse = Mouse.GetState();
            
           
            if(inwater)
            {
                gravity = 0.1f * level.scale;
                maxJumpheight = -5f;
                //timer++;
            }
            else
            {
                gravity = 0.75f * level.scale;
                maxJumpheight = -20f;
                timer = 0;
            }

            if(timer / 60 == 2)
            {
                dead = true;
            }
            if (isDashing)
            {
                dashDuration--;
                velocity.Y = 0;
                velocity.X *= 1.05f;

                if (dashDuration <= 0)
                {
                    isDashing = false;
                }
            }
            if (!dead)
            {
                
                if (kb.IsKeyDown(Keys.Right))
                {
                    tex = textures[1];
                    velocity.X += 1f * level.scale;
                    if (kb.IsKeyDown(Keys.Space) && !oldkb.IsKeyDown(Keys.Space) && dash == 1)
                    {
                        isDashing = true;
                        dashDuration = 12;
                        dash = 0;
                        velocity.X = 25f * level.scale;
                        velocity.Y = 0;
                    }
                }
                if (kb.IsKeyDown(Keys.Left))
                {
                    tex = textures[2];
                    velocity.X -= 1f * level.scale;
                    if (kb.IsKeyDown(Keys.Space) && !oldkb.IsKeyDown(Keys.Space) && dash == 1)
                    {
                        isDashing = true;
                        dashDuration = 12;
                        dash = 0;
                        velocity.X = -25f * level.scale;
                        velocity.Y = 0;
                    }
                }
                if (kb.IsKeyDown(Keys.Up) && double_jump > 0 && !oldkb.IsKeyDown(Keys.Up))
                {
                    if (double_jump == 1)
                    {
                        velocity.Y -= 10f * level.scale;
                        double_jump -= 1;
                    }
                    velocity.Y -= 20f * level.scale;
                    double_jump -= 1;

                }
               
            }
            position += velocity;

            if (velocity.Y < 0)
                onGround = false;

            if (velocity.Y < maxJumpheight * level.scale)
                velocity.Y = maxJumpheight * level.scale;



            if (inwater)
            {
                velocity.X *= .7f;
            }
            else
            {
               velocity.X *= .9f;
            }

            if (!onGround && !isDashing)
               velocity.Y += gravity;
            if (!kb.IsKeyDown(Keys.Right) && !kb.IsKeyDown(Keys.Left))
            {
                idleTime--;
                if (idleTime == 0)
                {
                    tex = textures[0];
                    idleTime = 30;
                }
            }
            if (onGround)
            {
             double_jump = 2;
            }
            if (dash == 0)
            {
                dashTimer -= 1;
            }
            if (dashTimer <= 0)
            {
                dash = 1;
                dashTimer = 90;
            }
            oldkb = kb;
            oldPosition = position;
        }

        public void ChangeColor(Color newColor)
        {
            color = newColor;
        }
        public void collision(Tile[,] tiles, Level level, LevelLoader LL)
        {
            if (tiles == null)
            {

            }
            else
            {
                this.tiles = tiles;
                onGround = false;
                inwater = false;
                for (int i = 0; i < tiles.GetLength(0); i++)
                {
                    for (int j = 0; j < tiles.GetLength(1); j++)
                    {
                        if (tiles[i, j] == null)
                            continue;

                        if (tiles[i, j].returnType() == Tile.TileType.floor || tiles[i, j].returnType() == Tile.TileType.keyDoor)
                        {
                            if (LL.CurrentLevel == LevelLoader.currentLevel.level1 && tiles[i, j].returnType() == Tile.TileType.keyDoor)
                            {
                                if (keyCount == 2)
                                {
                                    tiles[i, j].setTileType(Tile.TileType.air);
                                    tiles[i, j].setTex(null);
                                    break;
                                }
                            }
                            if (LL.CurrentLevel == LevelLoader.currentLevel.level4 && tiles[i, j].returnType() == Tile.TileType.keyDoor)
                            {
                                if (keyCount == 4)
                                {
                                    tiles[i, j].setTileType(Tile.TileType.air);
                                    tiles[i, j].setTex(null);
                                    break;
                                }
                            }
                            Rectangle tileRec = tiles[i, j].GetRec();

                            if (position.X + rec.Width > tileRec.X && position.X < tileRec.X + tileRec.Width)
                            {
                                if (velocity.Y >= 0 && position.Y + rec.Height <= tileRec.Y + velocity.Y + 1f && position.Y + rec.Height >= tileRec.Y)
                                {
                                    position.Y = tileRec.Y - rec.Height;
                                    velocity.Y = 0;
                                    onGround = true;
                                }
                                else if (velocity.Y < 0 && position.Y <= tileRec.Y + tileRec.Height && position.Y >= tileRec.Y + tileRec.Height + velocity.Y - 1f)
                                {
                                    position.Y = tileRec.Y + tileRec.Height;
                                    velocity.Y = 0;
                                }
                            }
                            if (position.Y + rec.Height > tileRec.Y && position.Y < tileRec.Y + tileRec.Height)
                            {
                                if (velocity.X > 0 && position.X + rec.Width >= tileRec.X && position.X + rec.Width <= tileRec.X + velocity.X + 1f)
                                {
                                    position.X = tileRec.X - rec.Width;
                                    velocity.X = 0;
                                }
                                else if (velocity.X < 0 && position.X <= tileRec.X + tileRec.Width && position.X >= tileRec.X + tileRec.Width + velocity.X - 1f)
                                {
                                    position.X = tileRec.X + tileRec.Width;
                                    velocity.X = 0;
                                }
                            }

                        }
                        if (!dead)
                        {
                            if (tiles[i, j].returnType() == Tile.TileType.exit && rec.Intersects(tiles[i, j].GetRec()))
                            {
                                level.room += 1;
                                level.initial = true;
                                return;
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.start)
                            {
                                if (level.playerInitial || level.initial)
                                {
                                    position = new Vector2(tiles[i, j].GetRec().X, tiles[i, j].GetRec().Y);
                                    startPos = position;
                                    rec = tiles[i, j].GetRec();
                                    UpdateRectangle();
                                    level.initial = false;
                                    level.playerInitial = false;
                                }
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.spike)
                            {
                                Rectangle r = tiles[i, j].GetRec();
                                //fix the hitbox
                                if (!dead && rec.Intersects(new Rectangle(r.X + (int)(15 * level.scale), r.Y + (int)(15 * level.scale), r.Width - 30, r.Height - 30)))
                                {
                                    dead = true;
                                }
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.YLaserVert)
                            {
                                Rectangle r = tiles[i, j].GetRec();
                                if (!dead && rec.Intersects(new Rectangle(r.X + 15, r.Y, r.Width - 15, r.Height)))
                                {
                                    if (color != Color.Yellow)
                                    {
                                        dead = true;
                                    }
                                    else
                                        continue;
                                }
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.YLaserHoriz)
                            {
                                Rectangle r = tiles[i, j].GetRec();
                                if (!dead && rec.Intersects(new Rectangle(r.X, r.Y + 15, r.Width, r.Height - 15)))
                                {
                                    if (color != Color.Yellow)
                                    {
                                        dead = true;
                                    }
                                    else
                                        continue;
                                }
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.checkpoint)
                            {
                                if (tiles[i, j] != checkpointTile)
                                {
                                    checkPointReached = false;
                                }
                                if (rec.Intersects(tiles[i, j].GetRec()) && !checkPointReached)
                                {
                                    level.checkpoint += 1;
                                    checkPointReached = true;
                                    checkpointTile = tiles[i, j];
                                }

                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.key)
                            {
                                if (rec.Intersects(tiles[i, j].GetRec()))
                                {
                                    tiles[i, j].setTileType(Tile.TileType.air);
                                    tiles[i, j].setTex(null);
                                    keyCount += 1;
                                }
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.TextTrigger)
                            {
                                if (rec.Intersects(tiles[i, j].GetRec()))
                                {
                                    if (LL.CurrentLevel == LevelLoader.currentLevel.level1)
                                    {
                                        if (level.room == 1)
                                        {
                                            level.Hint = "Press up twice to double jump";
                                            level.Hintlocation = new Vector2(750, 120);
                                        }
                                        else if (level.room == 2)
                                        {
                                            level.Hint = "Press space to dash";
                                            level.Hintlocation = new Vector2(750, 120);
                                        }
                                        else if (level.room == 3)
                                        {
                                            level.Hint = "Keys are the key...";
                                            level.Hintlocation = new Vector2(450, 420);
                                        }
                                    }
                                    if (LL.CurrentLevel == LevelLoader.currentLevel.level4)
                                    {
                                        if (level.room == 0)
                                        {
                                            level.Hint = "Touch the generator and bring power to the recievers";
                                            level.Hintlocation = new Vector2(650, 120);
                                        }
                                        else if (level.room == 2)
                                        {
                                            level.Hint = "Power all recievers to turn on the overcharge box \n then connect the power to overload the power grid";
                                            level.Hintlocation = new Vector2(650, 120);
                                        }
                                    }

                                }
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.yellowKey && rec.Intersects(tiles[i, j].GetRec()))
                            {
                                tiles[i, j].setTileType(Tile.TileType.air);
                                tiles[i, j].setTex(null);
                                hasYellowKey = true;
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.blueKey && rec.Intersects(tiles[i, j].GetRec()))
                            {
                                tiles[i, j].setTileType(Tile.TileType.air);
                                tiles[i, j].setTex(null);
                                hasBlueKey = true;
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.redKey && rec.Intersects(tiles[i, j].GetRec()))
                            {
                                tiles[i, j].setTileType(Tile.TileType.air);
                                tiles[i, j].setTex(null);
                                hasRedKey = true;
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.LevelHub && rec.Intersects(tiles[i, j].GetRec()))
                            {
                                LL.CurrentLevel = (LevelLoader.currentLevel)5;
                                LL.levels[4].initial = true;
                                keyCount = 0;
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.RedEntrance && rec.Intersects(tiles[i, j].GetRec()))
                            {
                                LL.CurrentLevel = (LevelLoader.currentLevel)2;
                                LL.levels[1].initial = true;
                                keyCount = 0;
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.BlueEntrance && rec.Intersects(tiles[i, j].GetRec()))
                            {
                                LL.CurrentLevel = (LevelLoader.currentLevel)3;
                                LL.levels[2].initial = true;
                                keyCount = 0;
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.YellowEntrance && rec.Intersects(tiles[i, j].GetRec()))
                            {
                                LL.CurrentLevel = (LevelLoader.currentLevel)4;
                                LL.levels[3].initial = true;
                                keyCount = 0;
                            }
                        }
                        if (tiles[i, j].returnType() == Tile.TileType.water && rec.Intersects(tiles[i, j].GetRec()))
                        {
                            inwater = true;
                        }
                        
                    }
                }
                if (dead)
                {
                    deathTimer--;
                    charged = false;
                    ultraCharged = false;
                    if (deathTimer <= 0)
                    {
                        dead = false;
                        respawnCheckpoint(level.checkpoint, LL);
                        UpdateRectangle();
                        deathTimer = 45;
                    }
                }
                
            }
        }
        public void respawnCheckpoint(int checkpoint, LevelLoader LL)
        {
            switch (LL.CurrentLevel)
            {
                case LevelLoader.currentLevel.level1:
                    if (checkpoint == 0)
                    {
                        position = startPos;
                    }
                    else
                    {
                        position = new Vector2(checkpointTile.GetRec().X, checkpointTile.GetRec().Y);
                    }
                    break;

                case LevelLoader.currentLevel.level2:
                    if (checkpoint == 0)
                    {
                        position = startPos;
                    }
                    else
                    {
                        position = new Vector2(checkpointTile.GetRec().X, checkpointTile.GetRec().Y);
                    }
                    break;
                case LevelLoader.currentLevel.level3:
                    if (checkpoint == 0)
                    {
                        position = startPos;
                    }
                    else
                    {
                        position = new Vector2(checkpointTile.GetRec().X, checkpointTile.GetRec().Y);
                    }
                    break;

                case LevelLoader.currentLevel.level4:
                    if (checkpoint == 0)
                    {
                        position = startPos;
                    }
                    else
                    {
                        position = new Vector2(checkpointTile.GetRec().X, checkpointTile.GetRec().Y);
                    }
                    break;
            }
        }
        public void UpdateRectangle()
        {
            rec = new Rectangle((int)position.X,(int)position.Y,rec.Width, rec.Height);
        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(tex, rec, color);
        }
    }
}

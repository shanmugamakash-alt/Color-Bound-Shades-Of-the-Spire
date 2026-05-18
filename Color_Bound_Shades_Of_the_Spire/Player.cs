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
        Texture2D[][] textures;
        Tile checkpointTile;
        public Rectangle rec, oldRec;
        public Vector2 position, oldPosition, velocity, startPos;
        public float gravity;
        public Color color;
        public bool onGround, checkPointReached, dead, electricDeath, poppedDeath, 
            isDashing, hasYellowKey, hasRedKey, hasBlueKey, inAir, airAnimPlaying, charged, ultraCharged;
        public int animNum, directionNum, dashDirectionNum, dashAnimNum, dashAnimeTimer, 
            dashAnimTimer, deathTimer, poppedTimer, poppedNum, dash, dashTimer, dashDurtion, keyCount, idleTime, dashDuration, double_jump;
        KeyboardState oldkb;
        
        public Player(Texture2D[][] t, Rectangle r)
        {
            tex = t[0][0];
            textures = t;

            rec = r;
            oldRec = rec;

            position = new Vector2(rec.X, rec.Y);
            startPos = position;
            velocity = Vector2.Zero;
            color = Color.White;
            oldkb = Keyboard.GetState();

            idleTime = 30;
            animNum = 0;
            directionNum = 1;
            dash = 1;
            dashDirectionNum = 0;
            dashAnimNum = 0;
            dashAnimTimer = 5;
            dashTimer = 90;
            dashDuration = 12;
            double_jump = 2;
            poppedNum = 1;
            poppedTimer = 3;
            keyCount = 0;
            deathTimer = 45;

            gravity = .75f;

            dead = false;
            airAnimPlaying = false;
            inAir = false;
            onGround = false;
            charged = false;
            ultraCharged = false;
            isDashing = false;
            checkPointReached = false;
            hasBlueKey = false;
            hasRedKey = false;
            hasYellowKey = false;
            electricDeath = false;
            poppedDeath = false;
        }

        public void move(KeyboardState kb, Level level)
        {
            gravity = .75f * level.scale;
            if (dead)
            {
                oldkb = kb;
                return;
            }
            if (isDashing)
            {
                dashDuration--;
                dashAnimTimer--;
                velocity.Y = 0;
                velocity.X *= 1.05f;
                if (dashAnimTimer == 0 && dashAnimNum < 5)
                {
                    tex = textures[dashDirectionNum][dashAnimNum];
                    dashAnimNum++;
                    dashAnimTimer = 2;
                }

                if (dashDuration <= 0)
                {
                    isDashing = false;
                }
            }
                if ((kb.IsKeyDown(Keys.Right) || (kb.IsKeyDown(Keys.D))) && !isDashing)
                {
                    directionNum = 2;
                    dashDirectionNum = 4;
                    tex = textures[0][1];
                    velocity.X += 1f * level.scale;
                    if (kb.IsKeyDown(Keys.Space) && !oldkb.IsKeyDown(Keys.Space) && dash == 1)
                    {
                        isDashing = true;
                        dashDuration = 12;
                        dash = 0;
                        velocity.X = 35f * level.scale;
                        velocity.Y = 0;
                        dashAnimNum = 0;
                        dashAnimTimer = 2;
                    }
                }
                if ((kb.IsKeyDown(Keys.Left) || (kb.IsKeyDown(Keys.A))) && !isDashing)
                {
                    directionNum = 3;
                    dashDirectionNum = 5;
                    tex = textures[0][2];
                    velocity.X -= 1f * level.scale;
                    if (kb.IsKeyDown(Keys.Space) && !oldkb.IsKeyDown(Keys.Space) && dash == 1)
                    {
                        isDashing = true;
                        dashDuration = 12;
                        dash = 0;
                        velocity.X = -35f * level.scale;
                        velocity.Y = 0;
                        dashAnimNum = 0;
                        dashAnimTimer = 2;
                    }
                }
                if ((kb.IsKeyDown(Keys.Up) || (kb.IsKeyDown(Keys.W))) && double_jump > 0 && !(oldkb.IsKeyDown(Keys.Up) || oldkb.IsKeyDown(Keys.W)))
                {
                    if (double_jump == 1)
                    {
                        velocity.Y -= 10f * level.scale;
                        double_jump -= 1;
                    }
                    velocity.Y -= 20f * level.scale;
                    double_jump -= 1;
                    inAir = true;
                    airAnimPlaying = true;
                    animNum = 0;          // RESET

                }
                position += velocity;

                if (velocity.Y < 0)
                    onGround = false;

                if (velocity.Y < -20f * level.scale)
                    velocity.Y = -20f * level.scale;

                velocity.X *= .9f;

                if (!onGround && !isDashing)
                    velocity.Y += gravity;
            if ((!kb.IsKeyDown(Keys.Right) && !kb.IsKeyDown(Keys.Left)) && (!kb.IsKeyDown(Keys.D) && !kb.IsKeyDown(Keys.A)))
            {
                idleTime--;
                if (idleTime == 0)
                {
                    tex = textures[0][0];
                    idleTime = 30;
                    directionNum = 1;
                }
            }
            if (onGround)
            {
                double_jump = 2;
                inAir = false;
            }
            if (inAir && !isDashing)
            {
                if (airAnimPlaying)
                {
                    if (directionNum == 1)
                    {
                        if (animNum < 6)
                        {
                            tex = textures[directionNum][animNum];
                            animNum++;
                        }
                        else
                        {
                            airAnimPlaying = false;
                            animNum = 0;
                        }
                    }
                    else if (directionNum == 2)
                    {
                        if (animNum < 6)
                        {
                            tex = textures[directionNum][animNum];
                            animNum++;
                        }
                        else
                        {
                            airAnimPlaying = false;
                            animNum = 0;
                        }
                    }
                    else if (directionNum == 3)
                    {
                        if (animNum < 6)
                        {
                            tex = textures[directionNum][animNum];
                            animNum++;
                        }
                        else
                        {
                            airAnimPlaying = false;
                            animNum = 0;
                        }
                    }
                }
                else
                {
                    if (kb.IsKeyDown(Keys.Right) || kb.IsKeyDown(Keys.D))
                        tex = textures[0][1];
                    else if (kb.IsKeyDown(Keys.Left) || kb.IsKeyDown(Keys.A))
                        tex = textures[0][2];
                    else
                        tex = textures[0][0];
                }
                    
                   
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
            oldRec = rec;
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
                onGround = false;
                for (int i = 0; i < tiles.GetLength(0); i++)
                {
                    for (int j = 0; j < tiles.GetLength(1); j++)
                    {
                        if (tiles[i, j] == null)
                            continue;

                        if (tiles[i, j].returnType() == Tile.TileType.floor || tiles[i, j].returnType() == Tile.TileType.barrier || tiles[i, j].returnType() == Tile.TileType.floorUp || tiles[i, j].returnType() == Tile.TileType.keyDoor)
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
                                else if (velocity.Y < 0 && position.Y <= tileRec.Y + tileRec.Height && position.Y >= tileRec.Y + tileRec.Height + velocity.Y - 1f && tiles[i, j].returnType() != Tile.TileType.floorUp)
                                {
                                    position.Y = tileRec.Y + tileRec.Height;
                                    velocity.Y = 0;
                                }
                            }
                            if (tiles[i, j].returnType() != Tile.TileType.floorUp)
                            {
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

                        }
                        if (!dead)
                        {
                            //text
                            if (LL.CurrentLevel == LevelLoader.currentLevel.level1 && tiles[i, j].returnType() == Tile.TileType.TextTrigger)
                            {
                                if (level.room == 0)
                                {
                                    level.Hint = "Arrow keys to move";
                                    level.HintLocation = new Vector2(250, 300);
                                }
                                else if (rec.Intersects(tiles[i, j].GetRec()))
                                {
                                    if (level.room == 1)
                                    {
                                        level.Hint = "Press up twice to double jump";
                                        level.HintLocation = new Vector2(tiles[i, j].GetRec().X - 20, tiles[i, j].GetRec().Y - 30);
                                    }
                                    if (level.room == 2)
                                    {
                                        level.Hint = "Press Space to dash\n (may require some timing)";
                                        level.HintLocation = new Vector2(tiles[i, j].GetRec().X - 80, tiles[i, j].GetRec().Y - 50);
                                    }
                                    tiles[i, j].setTex(level.Textures[15]);
                                }
                                else
                                {
                                    level.Hint = "";
                                    level.HintLocation = new Vector2(-100, -100);
                                }
                            }
                            if (LL.CurrentLevel == LevelLoader.currentLevel.level3 && tiles[i, j].returnType() == Tile.TileType.TextTrigger)
                            {
                                if (level.room == 0)
                                {
                                    level.Hint = "Blue Level Placeholder, here are the keys:";
                                    level.HintLocation = new Vector2(250, 300);
                                }
                                else if (rec.Intersects(tiles[i, j].GetRec()))
                                {
                                    
                                }
                                else
                                {
                                    level.Hint = "";
                                    level.HintLocation = new Vector2(-100, -100);
                                }
                            }
                            if (LL.CurrentLevel == LevelLoader.currentLevel.level4 && tiles[i, j].returnType() == Tile.TileType.TextTrigger)
                            {
                                if (rec.Intersects(tiles[i, j].GetRec()))
                                {
                                    if (level.room == 0)
                                    {
                                        level.Hint = "Connect the Crystal in the top right to\n the empty ones on the left with your ability,\n collect the color to the right and press 2";
                                        level.HintLocation = new Vector2(tiles[i, j].GetRec().X - 100, tiles[i, j].GetRec().Y - 80);
                                    }
                                    if (level.room == 2)
                                    {
                                        level.Hint = "The power grid is what keeps the blue lasers on,\n the only way to destroy it is to overload it with energy \nvia the UltraCrystal, connect the Crystal to the power grid \nwith your ability to overload it";
                                        level.HintLocation = new Vector2(tiles[i, j].GetRec().X - 100, tiles[i, j].GetRec().Y - 250);
                                    }
                                    tiles[i, j].setTex(level.Textures[41]);
                                }
                                else
                                {
                                    level.Hint = "";
                                    level.HintLocation = new Vector2(-100, -100);
                                }
                            }
                            if (LL.CurrentLevel == LevelLoader.currentLevel.level2 && tiles[i, j].returnType() == Tile.TileType.TextTrigger)
                            {
                                if (rec.Intersects(tiles[i, j].GetRec()))
                                {
                                    if (level.room == 0)
                                    {
                                        level.Hint = "Collect paint to refill bar and press 1 to become red.\n While red, touch torches to light them up.\n Touch enemies to kill them. \n Torches extinguish after 5 seconds!";
                                        level.HintLocation = new Vector2(tiles[i, j].GetRec().X - 200, tiles[i, j].GetRec().Y - 400);
                                    }
                                    if (level.room == 2)
                                    {
                                        level.Hint = "Kill the enemies before lighting the torches!";
                                        level.HintLocation = new Vector2(tiles[i, j].GetRec().X - 100, tiles[i, j].GetRec().Y - 250);
                                    }
                                    tiles[i, j].setTex(level.Textures[12]);
                                }
                                else
                                {
                                    level.Hint = "";
                                    level.HintLocation = new Vector2(-100, -100);
                                }
                            }
                            //exit condition
                            else if (tiles[i, j].returnType() == Tile.TileType.exit && rec.Intersects(tiles[i, j].GetRec()))
                            {
                                level.room += 1;
                                level.initial = true;
                                return;
                            }
                            //start
                            else if (tiles[i, j].returnType() == Tile.TileType.start)
                            {
                                if (level.playerInitial || level.initial)
                                {
                                    position = new Vector2(tiles[i, j].GetRec().X, tiles[i, j].GetRec().Y);
                                    startPos = position;
                                    rec = tiles[i, j].GetRec();
                                    level.checkpoint = 0;
                                    UpdateRectangle();
                                    level.initial = false;
                                    level.playerInitial = false;
                                }
                            }
                            //spikes
                            else if (tiles[i, j].returnType() == Tile.TileType.spikeU)
                            {
                                Rectangle r = tiles[i, j].GetRec();
                                if (!dead && rec.Intersects(new Rectangle(r.X + (int)(15 * level.scale), r.Y + (int)((r.Height / 2) * level.scale), r.Width - (int)(2 * (15 * level.scale)), (int)((r.Height / 2) * level.scale))))
                                {
                                    dead = true;
                                    poppedDeath = true;
                                }
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.spikeR)
                            {
                                Rectangle r = tiles[i, j].GetRec();
                                //fix the hitbox
                                if (!dead && rec.Intersects(new Rectangle(r.X, r.Y + (int)(15 * level.scale), r.Width - (int)((r.Width / 2) - 3 * level.scale), (int)(15 * level.scale))))
                                {
                                    dead = true;
                                    poppedDeath = true;
                                }
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.spikeL)
                            {
                                Rectangle r = tiles[i, j].GetRec();
                                //fix the hitbox
                                if (!dead && rec.Intersects(new Rectangle(r.X + (int)((r.Width / 2) * level.scale), r.Y + (int)(15 * level.scale), r.Width, (int)(15 * level.scale))))
                                {
                                    dead = true;
                                    poppedDeath = true;
                                }
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.spikeD)
                            {
                                Rectangle r = tiles[i, j].GetRec();
                                //fix the hitbox
                                if (!dead && rec.Intersects(new Rectangle(r.X + (int)(15 * level.scale), r.Y, r.Width - (int)(2 * (15 * level.scale)), r.Height - (int)((r.Height / 2) * level.scale))))
                                {
                                    dead = true;
                                    poppedDeath = true;
                                }
                            }
                            //lasers
                            else if (tiles[i, j].returnType() == Tile.TileType.YLaserVert)
                            {
                                Rectangle r = tiles[i, j].GetRec();
                                if (!dead && rec.Intersects(new Rectangle(r.X + (int)(15 * level.scale), r.Y, r.Width - (int)(15 * level.scale), r.Height)))
                                {
                                    if (color != Color.Yellow)
                                    {
                                        dead = true;
                                        electricDeath = true;
                                    }
                                    else
                                        continue;
                                }
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.YLaserHoriz)
                            {
                                Rectangle r = tiles[i, j].GetRec();
                                if (!dead && rec.Intersects(new Rectangle(r.X, r.Y + (int)(15 * level.scale), r.Width, r.Height - (int)(15 * level.scale))))
                                {
                                    if (color != Color.Yellow)
                                    {
                                        dead = true;
                                        electricDeath = true;
                                    }
                                    else
                                        continue;
                                }
                            }
                            //checkpoint
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
                            //keys
                            else if (tiles[i, j].returnType() == Tile.TileType.key)
                            {
                                if (rec.Intersects(tiles[i, j].GetRec()))
                                {
                                    tiles[i, j].setTileType(Tile.TileType.air);
                                    tiles[i, j].setTex(null);
                                    keyCount += 1;
                                }
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.yellowKey)
                            {
                                if (rec.Intersects(tiles[i, j].GetRec()))
                                {
                                    tiles[i, j].setTileType(Tile.TileType.air);
                                    tiles[i, j].setTex(null);
                                    hasYellowKey = true;
                                }
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.blueKey)
                            {
                                if (rec.Intersects(tiles[i, j].GetRec()))
                                {
                                    tiles[i, j].setTileType(Tile.TileType.air);
                                    tiles[i, j].setTex(null);
                                    hasBlueKey = true;
                                }
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.redKey)
                            {
                                if (rec.Intersects(tiles[i, j].GetRec()))
                                {
                                    tiles[i, j].setTileType(Tile.TileType.air);
                                    tiles[i, j].setTex(null);
                                    hasRedKey = true;
                                }
                            }
                            //doors
                            else if (tiles[i, j].returnType() == Tile.TileType.bossDoor1 && rec.Intersects(tiles[i, j].GetRec()))
                            {
                                Console.WriteLine(hasYellowKey);
                                Console.WriteLine(hasBlueKey);
                                Console.WriteLine(hasRedKey);
                                if (hasYellowKey && hasRedKey && hasBlueKey)
                                {
                                    level.room++;
                                    level.initial = true;
                                }
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.LevelHub && rec.Intersects(tiles[i, j].GetRec()))
                            {
                                LL.CurrentLevel = (LevelLoader.currentLevel)5;
                                LL.levels[4].initial = true;
                                LL.levels[4].Hint = "";
                                keyCount = 0;

                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.RedEntrance && rec.Intersects(tiles[i, j].GetRec()) && !hasRedKey)
                            {
                                LL.CurrentLevel = (LevelLoader.currentLevel)2;
                                LL.levels[1].Hint = "";
                                keyCount = 0;
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.BlueEntrance && rec.Intersects(tiles[i, j].GetRec()) && !hasBlueKey)
                            {
                                LL.CurrentLevel = (LevelLoader.currentLevel)3;
                                LL.levels[2].Hint = "";
                                keyCount = 0;
                            }
                            else if (tiles[i, j].returnType() == Tile.TileType.YellowEntrance && rec.Intersects(tiles[i, j].GetRec()) && !hasYellowKey)
                            {
                                LL.CurrentLevel = (LevelLoader.currentLevel)4;
                                LL.levels[3].Hint = "";
                                keyCount = 0;
                            }
                        }
                    }
                }
                if (dead)
                {
                    deathTimer--;
                    if (electricDeath)
                        tex = textures[6][0];
                    if (poppedDeath)
                    {
                        poppedTimer--;
                        if (poppedTimer == 0)
                        {
                            if (poppedNum > 5)
                                poppedNum = 5;
                            tex = textures[6][poppedNum];
                            poppedTimer = 3;
                            poppedNum++;
                        }
                    }
                    if (deathTimer <= 0)
                    {
                        color = Color.White;
                        dead = false;
                        level.resetBar();
                        respawnCheckpoint(level.checkpoint, LL);
                        UpdateRectangle();
                        tex = textures[0][0];

                        electricDeath = false;
                        poppedDeath = false;
                        poppedTimer = 3;
                        poppedNum = 1;
                        deathTimer = 45;
                        velocity = Vector2.Zero;
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

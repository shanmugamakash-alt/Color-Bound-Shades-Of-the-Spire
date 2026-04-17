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
    public class Enemy
    {
        Texture2D tex;
        Rectangle rect;
        int speed;
        int tileDetectionRange;
        int dir;
        Vector2 velocity;
        float gravity;
        bool isOnGround;
        public bool dead;


        public Enemy(Texture2D t, Rectangle r, int s, int range)
        {
            tex = t;
            rect = r;
            speed = s;
            tileDetectionRange = range;
            dir = 1;
            velocity = new Vector2(1, 1);
            isOnGround = false;
            dead = false;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, rect, Color.White);
        }

        public void Update(Tile [,] tiles, Player p, Level level)
        {
            if (tiles != null)
            {
                gravity = .75f * level.scale;
                if (p.rec.X - rect.X < level.tileSize * tileDetectionRange && p.rec.X > rect.X)
                {
                    dir = 1;
                }
                else if (rect.X - p.rec.X < level.tileSize * tileDetectionRange && p.rec.X < rect.X)
                {
                    dir = -1;
                }
                else
                {
                    dir = 0;
                }

                    for (int x = 0; x < tiles.GetLength(0); x++)
                    {
                        for (int y = 0; y < tiles.GetLength(1); y++)
                        {
                            if(tiles[x,y] == null)
                            {

                            }
                            else if (tiles[x, y].returnType() == Tile.TileType.floor)
                            {
                                Rectangle tileRec = tiles[x, y].GetRec();

                                if (rect.X + rect.Width > tileRec.X && rect.X < tileRec.X + tileRec.Width)
                                {
                                    if (velocity.Y >= 0 && rect.Y + rect.Height <= tileRec.Y + velocity.Y + 1f && rect.Y + rect.Height >= tileRec.Y)
                                    {
                                        rect.Y = tileRec.Y - rect.Height;
                                        velocity.Y = 0;
                                        isOnGround = true;
                                    }
                                    else if (velocity.Y < 0 && rect.Y <= tileRec.Y + tileRec.Height && rect.Y >= tileRec.Y + tileRec.Height + velocity.Y - 1f)
                                    {
                                        rect.Y = tileRec.Y + tileRec.Height;
                                        velocity.Y = 0;
                                    }
                                }
                                if (rect.Y + rect.Height > tileRec.Y && rect.Y < tileRec.Y + tileRec.Height)
                                {
                                    if (velocity.X > 0 && rect.X + rect.Width >= tileRec.X && rect.X + rect.Width <= tileRec.X + velocity.X + 1f)
                                    {
                                        rect.X = tileRec.X - rect.Width;
                                        velocity.X = 0;
                                    }
                                    else if (velocity.X < 0 && rect.X <= tileRec.X + tileRec.Width && rect.X >= tileRec.X + tileRec.Width + velocity.X - 1f)
                                    {
                                        rect.X = tileRec.X + tileRec.Width;
                                        velocity.X = 0;
                                    }
                                }

                            }

                            if (tiles[x, y] == null)
                            {

                            }
                            else if (rect.Intersects(tiles[x, y].GetRec()))
                            {
                                bool onGround;
                                if (rect.Bottom == tiles[x, y].GetRec().Bottom)
                                    onGround = true;
                                else
                                    onGround = false;

                                if(tiles[x + dir,y] == null)
                                {

                                }

                                else if (tiles[x + dir, y].returnType() == Tile.TileType.floor)
                                {
                                    if (onGround)
                                        isOnGround = true;
                                    else
                                        isOnGround = false;
                                    if (isOnGround)
                                    {
                                        velocity.Y -= 20f * level.scale;
                                    }
                                }



                            }
                        }
                    }
                    rect.X += speed * dir;

                

                if (rect.Intersects(p.rec))
                {
                    if (p.color == Color.Red)
                    {
                        dead = true;
                    }
                    else
                    {
                        p.dead = true;
                    }
                }

                if (!isOnGround)
                    velocity.Y += gravity;
                rect.Y += (int)velocity.Y;
            }
        }
    }
}

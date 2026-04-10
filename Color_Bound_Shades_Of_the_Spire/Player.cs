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
        public Rectangle rec;
        public Vector2 position;
        Vector2 velocity;
        float gravity;
        public Color color;
        public bool onGround;
        public bool dead;
        public int deathTimer;
        public int room;
        public Vector2 startPos;
        int double_jump;
        KeyboardState oldkb;
        
        public Player(Texture2D t, Rectangle r)
        {

            tex = t;
            rec = r;
            room = 1;
            position = new Vector2(rec.X, rec.Y);
            startPos = position;
            dead = false;
            deathTimer = 60;
            velocity = Vector2.Zero;
            gravity = .75f;
            onGround = false;
            color = Color.White;
            oldkb = Keyboard.GetState();
            double_jump = 2;
        }

        public void move(KeyboardState kb, Level level)
        {
            MouseState mouse = Mouse.GetState();
            gravity = .75f * level.scale;
            if (!dead)
            {
                if (kb.IsKeyDown(Keys.Right))
                {
                    velocity.X += 1f * level.scale;
                    if (kb.IsKeyDown(Keys.Space) && kb != oldkb)
                    {
                        velocity.X += 15 * level.scale;
                    }
                }
                if (kb.IsKeyDown(Keys.Left))
                {
                    velocity.X -= 1f * level.scale;
                    if (kb.IsKeyDown(Keys.Space) && kb != oldkb)
                    {
                        velocity.X -= 15 * level.scale;
                    }
                }
                if (kb.IsKeyDown(Keys.Up) && double_jump > 0 && kb != oldkb)
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

                if (velocity.Y < -20f * level.scale)
                    velocity.Y = -20f * level.scale;

                velocity.X *= .9f;

                if (!onGround)
                    velocity.Y += gravity;

                if (onGround)
                {
                    double_jump = 2;
                }

                oldkb = kb;
            
        }

        public void ChangeColor(Color newColor)
        {
            color = newColor;
        }
        public void collision(Tile[,] tiles, Level level)
        {
            onGround = false;
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j] == null) 
                        continue;

                    if (tiles[i, j].returnType() == Tile.TileType.floor)
                    {
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
                    else if (tiles[i, j].returnType() == Tile.TileType.exit && rec.Intersects(tiles[i, j].GetRec()))
                    {
                        room += 1;
                        Console.WriteLine(room);
                        level.initial = true;
                        return;
                    }
                    else if (tiles[i, j].returnType() == Tile.TileType.start)
                    {
                        if (level.initial)
                        {
                            position = new Vector2(tiles[i, j].GetRec().X, tiles[i, j].GetRec().Y);
                            startPos = position;
                            rec = tiles[i, j].GetRec();
                            UpdateRectangle();
                            level.initial = false;
                        }
                    }
                    else if (tiles[i, j].returnType() == Tile.TileType.spike)
                    {
                        if (!dead && rec.Intersects(new Rectangle(tiles[i, j].GetRec().X + (int)(20 * level.scale), tiles[i, j].GetRec().Y + (int)(20 * level.scale), tiles[i, j].GetRec().Width - 40, tiles[i, j].GetRec().Height - 40)))
                        {
                            dead = true;
                        }
                    }
                }
            }
            if (dead)
            {
                deathTimer--;
                if (deathTimer <= 0)
                {
                    dead = false;
                    position = startPos;
                    UpdateRectangle();
                    deathTimer = 60;
                }
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

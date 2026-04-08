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
        public int room;
        
        public Player(Texture2D t, Rectangle r)
        {

            tex = t;
            rec = r;
            room = 1;
            position = new Vector2(rec.X, rec.Y);
            velocity = Vector2.Zero;
            gravity = .95f;
            onGround = false;
            color = Color.White;
        }

        public void move(KeyboardState kb)
        {

            if (kb.IsKeyDown(Keys.Right))
            {
                velocity.X += 1f;
            }
            if (kb.IsKeyDown(Keys.Left))
            {
                velocity.X -= 1f;
            }
            if (kb.IsKeyDown(Keys.Up) && onGround)
            {
                velocity.Y -= 25f;
            }
            position += velocity;

            if (velocity.Y < 0)
                onGround = false;
            if (velocity.Y < -25f)
                velocity.Y = -25f;

            velocity.X *= .9f;
            if (!onGround)
                velocity.Y += gravity;
        }

        public void ChangeColor(Color newColor)
        {
            color = newColor;
        }
        public void collision(Tile[,] tiles)
        {
            onGround = false;
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    if (tiles[i, j].returnType() == Tile.TileType.floor)
                    {

                        Rectangle tileRec = tiles[i, j].GetRec();

                        // horizontal overlap check
                        bool overlapX = position.X + rec.Width > tileRec.X &&
                                        position.X < tileRec.X + tileRec.Width;

                        // vertical check: is player's feet at or slightly above the tile?
                        bool overlapY = position.Y + rec.Height >= tileRec.Y &&
                                        position.Y + rec.Height <= tileRec.Y + velocity.Y + 1f;

                        if (overlapX && overlapY && velocity.Y >= 0)
                        {
                            // Snap to top
                            position.Y = tileRec.Y - rec.Height;
                            velocity.Y = 0;
                            onGround = true; // mark that we are on a tile
                        }
                    }
                    else if (tiles[i, j].returnType() == Tile.TileType.exit && position.X > tiles[i, j].GetRec().X)
                    {
                        room += 1;
                    }
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

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
        int double_jumpcnt;
        MouseState oldMouse;
        KeyboardState oldkb;
        //int grapple;

        public Player(Texture2D t, Rectangle r)
        {

            tex = t;
            rec = r;
            room = 1;
            position = new Vector2(rec.X, rec.Y);
            velocity = Vector2.Zero;
            gravity = .75f;
            onGround = false;
            color = Color.White;
            oldkb = Keyboard.GetState();
            oldMouse = Mouse.GetState();
            double_jumpcnt = 2;
            //grapple = 20;
            

        }

        public void move(KeyboardState kb)
        {
            MouseState mouse = Mouse.GetState();
            
            if (kb.IsKeyDown(Keys.Right))
            {
                velocity.X += 1f;
                if (kb.IsKeyDown(Keys.Space) && kb != oldkb)
                {
                    velocity.X += 15;
                }
            }
            if (kb.IsKeyDown(Keys.Left))
            {
                velocity.X -= 1f;
                if (kb.IsKeyDown(Keys.Space) && kb != oldkb)
                {
                    velocity.X -= 15;
                }
            }
            if (kb.IsKeyDown(Keys.Up) && double_jumpcnt > 0 && kb != oldkb)
            {
                if (double_jumpcnt == 1)
                {
                    velocity.Y -= 10f;
                    double_jumpcnt -= 1;
                }
                velocity.Y -= 20f;
                double_jumpcnt -= 1;
                
            }

            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                Vector2 target = new Vector2(mouse.X, mouse.Y);
                Vector2 direction = target - position;

                float distance = direction.Length();

                if(distance > 0)
                {
                    direction.Normalize();
                    float variableForce = distance * 0.10f;

                    variableForce = MathHelper.Clamp(variableForce, 5, 100);
                    velocity = direction * variableForce;
                }
            }

            position += velocity;

            //if (velocity.Y < 0)
            //    onGround = false;
            //if (mouse.LeftButton == ButtonState.Pressed && grapple >= 0)
            //{
            //    if (velocity.Y < -33f)
            //        velocity.Y = -33f;
            //    grapple--;
            //}
            //else
            //{
                if (velocity.Y < -20f)
                    velocity.Y = -20f;
            //}

            velocity.X *= .9f;
            if (!onGround)
                velocity.Y += gravity;

            if(onGround)
            {
                double_jumpcnt = 2;
                //grapple = 20;
            }

            oldkb = kb;
            oldMouse = mouse;
            
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

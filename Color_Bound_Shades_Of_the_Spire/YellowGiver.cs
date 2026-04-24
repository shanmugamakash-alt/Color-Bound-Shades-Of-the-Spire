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
    public class YellowGiver
    {
        Texture2D T;
        Rectangle R;
        public bool touched;
        public YellowGiver(Texture2D t, Rectangle r)
        {
            T = t;
            R = r;
            touched = false;
        }
        public void colision(Player player)
        {
            if (player.rec.Intersects(R) && player.color == Color.Yellow)
            {
                player.charged = true;
                touched = true;
            }
            else if (player.color != Color.Yellow)
            {
                player.charged = false;
                touched = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Player player, Level level)
        {
            for (int i = 0; i < level.YRList.Count; i++)
            {
                if (player.charged && !level.YRList[i].isOn && touched)
                {
                    Vector2 start = new Vector2(player.rec.Center.X, player.rec.Center.Y);
                    Vector2 end = new Vector2(R.Center.X, R.Center.Y);
                    Vector2 edge = end - start;
                    float angle = (float)Math.Atan2(edge.Y, edge.X);
                    float length = edge.Length();

                    spriteBatch.Draw(level.Textures[5], start, null, Color.White, angle, new Vector2(0, level.Textures[5].Height / 2f), new Vector2(length / level.Textures[5].Width, 1f), SpriteEffects.None, 0f);
                }
                else if (level.YRList[i].isOn && level.YRList[i].activatedBy == this)
                {
                    Vector2 start = new Vector2(level.YRList[i].R.Center.X, level.YRList[i].R.Center.Y);
                    Vector2 end = new Vector2(R.Center.X, R.Center.Y);
                    Vector2 edge = end - start;
                    float angle = (float)Math.Atan2(edge.Y, edge.X);
                    float length = edge.Length();

                    spriteBatch.Draw(level.Textures[18], start, null, Color.White, angle, new Vector2(0, level.Textures[18].Height / 2f), new Vector2(length / level.Textures[18].Width, 1f), SpriteEffects.None, 0f);
                }
            }
            spriteBatch.Draw(T, R, Color.White);
        }
    }
}

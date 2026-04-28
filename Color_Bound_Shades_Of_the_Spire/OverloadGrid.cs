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
    public class OverloadGrid
    {
        Texture2D T;
        Rectangle R;
        public bool isPowered;

        public OverloadGrid(Texture2D t, Rectangle r)
        {
            T = t;
            R = r;
            isPowered = false;
        }

        public void colision(Player player, Level level)
        {
            bool allOn = true;
            if (level.YRList.Count <= 0)
                allOn = false;
            for (int i = 0; i < level.YRList.Count; i++)
            {
                if (!level.YRList[i].isOn)
                {
                    allOn = false;
                    break;
                }
            }
            if (allOn)
            {
                isPowered = true;
                T = level.Textures[32];
            }
            if (player.rec.Intersects(R) && isPowered && player.color == Color.Yellow)
            {
                player.ultraCharged = true;
            }
            if (player.color != Color.Yellow)
            {
                player.ultraCharged = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch, Player player, Level level)
        {
            for (int i = 0; i < level.YRList.Count; i++)
            {
                if (player.ultraCharged && !level.PG.isDestroyed)
                {
                    Vector2 start = new Vector2(player.rec.Center.X, player.rec.Center.Y);
                    Vector2 end = new Vector2(R.Center.X, R.Center.Y);
                    Vector2 edge = end - start;
                    float angle = (float)Math.Atan2(edge.Y, edge.X);
                    float length = edge.Length();

                    spriteBatch.Draw(level.Textures[35], start, null, Color.White, angle, new Vector2(0, level.Textures[35].Height / 2f), new Vector2(length / level.Textures[35].Width, 1f), SpriteEffects.None, 0f);
                }
                else if (level.PG.isDestroyed)
                {
                    Vector2 start = new Vector2(level.PG.R.Center.X, level.PG.R.Center.Y);
                    Vector2 end = new Vector2(R.Center.X, R.Center.Y);
                    Vector2 edge = end - start;
                    float angle = (float)Math.Atan2(edge.Y, edge.X);
                    float length = edge.Length();

                    spriteBatch.Draw(level.Textures[36], start, null, Color.White, angle, new Vector2(0, level.Textures[35].Height / 2f), new Vector2(length / level.Textures[35].Width, 1f), SpriteEffects.None, 0f);
                }
            }
            spriteBatch.Draw(T, R, Color.White);
        }
    }
}

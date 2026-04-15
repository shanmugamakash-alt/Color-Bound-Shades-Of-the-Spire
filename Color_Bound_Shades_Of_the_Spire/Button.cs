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
    public class Button
    {
        Texture2D T;
        Rectangle R;
        ButtonType Type;
        public enum ButtonType
        { 
            Play,

        }
        public Button(Texture2D t, Rectangle r, ButtonType type)
        {
            T = t;
            R = r;
            Type = type;
        }

        public void isInteracting(Rectangle r, MouseState M, MouseState oldM, Game1 game1)
        {
            if (r.Intersects(R) && M.LeftButton == ButtonState.Pressed && oldM.LeftButton == ButtonState.Released && Type == ButtonType.Play)
            {
                game1.gameState = Game1.GameState.Game;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(T, R, Color.White);
        }
    }
}

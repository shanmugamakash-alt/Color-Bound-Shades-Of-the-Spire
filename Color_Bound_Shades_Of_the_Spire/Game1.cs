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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font1;
        Rectangle red;
        Rectangle yellow;
        Rectangle blue;
        Rectangle bar;
        Rectangle barWhite;
        Player p;
        Texture2D r;
        string[] fileNames;
        Texture2D[][] BlockTextures;
        LevelLoader levelLoader;
        Texture2D t;
        Texture2D barTex;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1900;
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            red = new Rectangle(50, 0, 75, 75);
            yellow = new Rectangle(150, 0, 75, 75);
            blue = new Rectangle(250, 0, 75, 75);
            bar = new Rectangle(0, 100, 400, 100);
            barWhite = new Rectangle(0, 100, 400, 100);
            fileNames = new string[1];
            fileNames[0] = "Content/level1.txt";
            BlockTextures = new Texture2D[1][];
            BlockTextures[0] = new Texture2D[3];
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            t = this.Content.Load<Texture2D>("Untitled");
            p = new Player(t, new Rectangle(100, 100, 100, 100), 1920, 1080);
            font1 = this.Content.Load<SpriteFont>("SpriteFont1");
            BlockTextures[0][0] = this.Content.Load<Texture2D>("Untitled");
            BlockTextures[0][1] = this.Content.Load<Texture2D>("Tile");
            BlockTextures[0][2] = this.Content.Load<Texture2D>("Spike");
            barTex = this.Content.Load<Texture2D>("bar");
            levelLoader = new LevelLoader(fileNames, BlockTextures, 1);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //replace kb with player call or movement or wtv
            levelLoader.Update(kb);
            if(kb.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if(kb.IsKeyDown(Keys.D1))
            {
                p.ChangeColor(Color.Red);
            }

            if (kb.IsKeyDown(Keys.D2))
            {
                p.ChangeColor(Color.Yellow);
            }

            if (kb.IsKeyDown(Keys.D3))
            {
                p.ChangeColor(Color.Blue);
            }
            p.move(kb);
            
            if(p.color != Color.White)
            {
                bar.Width--;
            }
            else if(bar.Width < 400)
            {
                bar.Width++;
            }

            if(bar.Width <= 0)
            {
                p.ChangeColor(Color.White);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            levelLoader.DrawAll(spriteBatch);
            p.Draw(spriteBatch);
            spriteBatch.Draw(t, red, Color.Red);
            spriteBatch.Draw(t, yellow, Color.Yellow);
            spriteBatch.Draw(t, blue, Color.Blue);
            spriteBatch.Draw(t, barWhite, Color.White);
            spriteBatch.Draw(t, bar, p.color);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

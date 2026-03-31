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
        Player p;
        Texture2D r;
        string[] fileNames;
        Texture2D[][] BlockTextures;
        LevelLoader levelLoader;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            p = new Player();
            fileNames = new string[1];
            fileNames[0] = "Content/level1.txt";
            BlockTextures = new Texture2D[1][];
            BlockTextures[0] = new Texture2D[2];
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
            r = this.Content.Load<Texture2D>("Untitled");
            font1 = this.Content.Load<SpriteFont>("SpriteFont1");
            BlockTextures[0][0] = this.Content.Load<Texture2D>("Untitled");
            BlockTextures[0][1] = this.Content.Load<Texture2D>("images");
            levelLoader = new LevelLoader(fileNames, BlockTextures);
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

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
            spriteBatch.Draw(r, new Rectangle(200, 100, 100, 100), Color.Blue);
            spriteBatch.Draw(r, new Rectangle(200, 200, 100, 100), Color.Red);
            spriteBatch.Draw(r, new Rectangle(200, 150, 100, 100), Color.Purple);
            spriteBatch.DrawString(font1, "I really am bound to these colors... I guess i'm COLORBOUND", new Vector2(0, 10), Color.White);
            spriteBatch.Draw(r, new Rectangle(200, 150, 100, 100), Color.Brown);
            spriteBatch.Draw(r, new Rectangle(300, 150, 100, 100), Color.Green);

            levelLoader.DrawAll(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

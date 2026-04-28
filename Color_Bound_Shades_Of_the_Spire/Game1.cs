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
        string[][] fileNames;
        Texture2D[][] BlockTextures;
        LevelLoader levelLoader;
        Texture2D[] t;
        Texture2D barTex;
        KeyboardState oldKB;
        MouseState oldM;
        Button PlayButton;
        public GameState gameState;
        public enum GameState
        {
            MainMenu,
            LevelSelect,
            Game,
            Pause,
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1900;
            IsMouseVisible = true;
            oldKB = Keyboard.GetState();
            oldM = Mouse.GetState();
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
            gameState = GameState.MainMenu;

            fileNames = new string[5][];
            //tutorial
            fileNames[0] = new string[4];
            fileNames[0][0] = "Content/level1TR1.txt";
            fileNames[0][1] = "Content/level1TR2.txt";
            fileNames[0][2] = "Content/level1TR3.txt";
            fileNames[0][3] = "Content/level1TR4.txt";
            //other levels
            fileNames[1] = new string[3];
            fileNames[1][0] = "Content/level1RR1.txt";
            fileNames[1][1] = "Content/level1RR2.txt";
            fileNames[1][2] = "Content/level1RR3.txt";

            fileNames[2] = new string[1];

            fileNames[3] = new string[3];
            fileNames[3][0] = "Content/level1YR1.txt";
            fileNames[3][1] = "Content/level1YR2.txt";
            fileNames[3][2] = "Content/level1YR3.txt";
            //level hub
            fileNames[4] = new string[1];
            fileNames[4][0] = "Content/levelHub.txt";

            BlockTextures = new Texture2D[5][];
            BlockTextures[0] = new Texture2D[11];
            BlockTextures[1] = new Texture2D[11];
            BlockTextures[2] = new Texture2D[5];
            BlockTextures[3] = new Texture2D[25];
            BlockTextures[4] = new Texture2D[11];
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
            t = new Texture2D[3];
            t[0] = this.Content.Load<Texture2D>("BlobIdle");
            t[1] = this.Content.Load<Texture2D>("BlobRight");
            t[2] = this.Content.Load<Texture2D>("BlobLeft");
            p = new Player(t, new Rectangle(100, 100, 100, 100));
            font1 = this.Content.Load<SpriteFont>("SpriteFont1");

            BlockTextures[0][0] = this.Content.Load<Texture2D>("DungeonTileFloor");
            BlockTextures[0][1] = this.Content.Load<Texture2D>("Tile"); // unused as of now
            BlockTextures[0][2] = this.Content.Load<Texture2D>("SpikeU");
            BlockTextures[0][3] = this.Content.Load<Texture2D>("checkpoint");
            BlockTextures[0][4] = this.Content.Load<Texture2D>("Key");
            BlockTextures[0][5] = this.Content.Load<Texture2D>("SpikeD (1)");
            BlockTextures[0][6] = this.Content.Load<Texture2D>("SpikeR (1)");
            BlockTextures[0][7] = this.Content.Load<Texture2D>("SpikeL (1)");
            BlockTextures[0][8] = this.Content.Load<Texture2D>("DungeonTileWall1");
            BlockTextures[0][9] = this.Content.Load<Texture2D>("DungeonTileWall2");
            BlockTextures[0][10] = this.Content.Load<Texture2D>("DungeonTileWall3");

            BlockTextures[1][0] = this.Content.Load<Texture2D>("Untitled");
            BlockTextures[1][1] = this.Content.Load<Texture2D>("Tile");
            BlockTextures[1][2] = this.Content.Load<Texture2D>("SpikeU");
            BlockTextures[1][3] = this.Content.Load<Texture2D>("checkpoint");
            BlockTextures[1][4] = this.Content.Load<Texture2D>("Key");
            BlockTextures[1][5] = this.Content.Load<Texture2D>("firedoorU");
            BlockTextures[1][6] = this.Content.Load<Texture2D>("firedoorD");
            BlockTextures[1][7] = this.Content.Load<Texture2D>("torch");
            BlockTextures[1][8] = this.Content.Load<Texture2D>("littorch");
            BlockTextures[1][9] = this.Content.Load<Texture2D>("enemy");
            BlockTextures[1][10] = this.Content.Load<Texture2D>("colorCollectable");

            BlockTextures[2][0] = this.Content.Load<Texture2D>("Untitled");
            BlockTextures[2][1] = this.Content.Load<Texture2D>("Tile");
            BlockTextures[2][2] = this.Content.Load<Texture2D>("SpikeU");
            BlockTextures[2][3] = this.Content.Load<Texture2D>("checkpoint");
            BlockTextures[2][4] = this.Content.Load<Texture2D>("Key");

            BlockTextures[3][0] = this.Content.Load<Texture2D>("Untitled");
            BlockTextures[3][1] = this.Content.Load<Texture2D>("Tile");
            BlockTextures[3][2] = this.Content.Load<Texture2D>("YSpikeU");
            BlockTextures[3][3] = this.Content.Load<Texture2D>("checkpoint");
            BlockTextures[3][4] = this.Content.Load<Texture2D>("Key");
            BlockTextures[3][5] = this.Content.Load<Texture2D>("Wire");
            BlockTextures[3][6] = this.Content.Load<Texture2D>("YellowFloor");
            BlockTextures[3][7] = this.Content.Load<Texture2D>("YellowBackground");
            BlockTextures[3][8] = this.Content.Load<Texture2D>("YellowDoorUL");
            BlockTextures[3][9] = this.Content.Load<Texture2D>("YellowDoorUR");
            BlockTextures[3][10] = this.Content.Load<Texture2D>("YellowDoorDL");
            BlockTextures[3][11] = this.Content.Load<Texture2D>("YellowDoorDR");
            BlockTextures[3][12] = this.Content.Load<Texture2D>("YellowGenerator");
            BlockTextures[3][13] = this.Content.Load<Texture2D>("YellowRecieverOff");
            BlockTextures[3][14] = this.Content.Load<Texture2D>("YellowRecieverOn");
            BlockTextures[3][15] = this.Content.Load<Texture2D>("YSpikeD");
            BlockTextures[3][16] = this.Content.Load<Texture2D>("YSpikeR");
            BlockTextures[3][17] = this.Content.Load<Texture2D>("YSpikeL");
            BlockTextures[3][18] = this.Content.Load<Texture2D>("WireCharged");
            BlockTextures[3][19] = this.Content.Load<Texture2D>("YLaserVertT");
            BlockTextures[3][20] = this.Content.Load<Texture2D>("YLaserVertM");
            BlockTextures[3][21] = this.Content.Load<Texture2D>("YLaserVertB");
            BlockTextures[3][22] = this.Content.Load<Texture2D>("YLaserHorizL");
            BlockTextures[3][23] = this.Content.Load<Texture2D>("YLaserHorizM");
            BlockTextures[3][24] = this.Content.Load<Texture2D>("YLaserHorizR");

            BlockTextures[4][0] = this.Content.Load<Texture2D>("DungeonTileFloor");
            BlockTextures[4][1] = this.Content.Load<Texture2D>("Tile");
            BlockTextures[4][2] = this.Content.Load<Texture2D>("SpikeU");
            BlockTextures[4][3] = this.Content.Load<Texture2D>("checkpoint");
            BlockTextures[4][4] = this.Content.Load<Texture2D>("Key");
            BlockTextures[4][5] = this.Content.Load<Texture2D>("YellowEntranceDoorU");
            BlockTextures[4][6] = this.Content.Load<Texture2D>("YellowEntranceDoorD");
            BlockTextures[4][8] = this.Content.Load<Texture2D>("DungeonTileWall1");
            BlockTextures[4][9] = this.Content.Load<Texture2D>("DungeonTileWall2");
            BlockTextures[4][10] = this.Content.Load<Texture2D>("DungeonTileWall3");

            barTex = this.Content.Load<Texture2D>("bar");
            levelLoader = new LevelLoader(fileNames, BlockTextures, 5);

            PlayButton = new Button(BlockTextures[3][0], new Rectangle(800, 400, 250, 100), Button.ButtonType.Play);
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
            MouseState mouse = Mouse.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //replace kb with player call or movement or wtv
            if (gameState == GameState.MainMenu)
            {
                PlayButton.isInteracting(new Rectangle(mouse.X, mouse.Y, 2, 2), mouse, oldM, this);
            }
            else if (gameState == GameState.Game)
            {
                levelLoader.Update(p, kb, oldKB);
                if (kb.IsKeyDown(Keys.Escape) && !oldKB.IsKeyDown(Keys.Escape))
                {
                    gameState = GameState.Pause;
                }
            }
            else if (gameState == GameState.Pause)
            {
                if (kb.IsKeyDown(Keys.Escape) && !oldKB.IsKeyDown(Keys.Escape))
                {
                    gameState = GameState.Game;
                }
            }

                base.Update(gameTime);
            oldKB = kb;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (gameState == GameState.MainMenu)
            {
                spriteBatch.Begin();
                PlayButton.Draw(spriteBatch);
                spriteBatch.DrawString(font1, "Play", new Vector2(903, 435), Color.Black);
                spriteBatch.End();
            }
            else if (gameState == GameState.Game)
            {
                spriteBatch.Begin();
                levelLoader.DrawAll(spriteBatch, p);
                p.Draw(spriteBatch);
                spriteBatch.End();
            }
            else if (gameState == GameState.Pause)
            {
                spriteBatch.Begin();
                levelLoader.DrawAll(spriteBatch, p);
                p.Draw(spriteBatch);
                spriteBatch.DrawString(font1, "Paused", new Vector2(903, 200), Color.Black);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}

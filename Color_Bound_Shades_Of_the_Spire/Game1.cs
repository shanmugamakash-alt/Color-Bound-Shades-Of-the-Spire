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
        SpriteFont font2;
        Player p;
        string[][] fileNames;
        Texture2D[][] BlockTextures;
        LevelLoader levelLoader;
        Texture2D[][] t;
        Texture2D barTex;
        Texture2D Logo;
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
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
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
            fileNames[1] = new string[4];
            fileNames[1][0] = "Content/level1RR1.txt";
            fileNames[1][1] = "Content/level1RR2.txt";
            fileNames[1][2] = "Content/level1RR3.txt";
            fileNames[1][3] = "Content/level1RR4.txt";

            fileNames[2] = new string[1];
            fileNames[2][0] = "Content/level1BR1.txt";

            fileNames[3] = new string[4];
            fileNames[3][0] = "Content/level1YR1.txt";
            fileNames[3][1] = "Content/level1YR2.txt";
            fileNames[3][2] = "Content/level1YR3.txt";
            fileNames[3][3] = "Content/level1YR4.txt";
            //level hub
            fileNames[4] = new string[2];
            fileNames[4][0] = "Content/levelHub.txt";
            fileNames[4][1] = "Content/BossRoom1.txt";


            BlockTextures = new Texture2D[5][];
            BlockTextures[0] = new Texture2D[16];
            BlockTextures[1] = new Texture2D[22];
            BlockTextures[2] = new Texture2D[16];
            BlockTextures[3] = new Texture2D[44];
            BlockTextures[4] = new Texture2D[20];
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
            t = new Texture2D[6][];
            t[0] = new Texture2D[3];
            t[1] = new Texture2D[6];
            t[2] = new Texture2D[6];
            t[3] = new Texture2D[6];
            t[4] = new Texture2D[5];
            t[5] = new Texture2D[5];

            t[0][0] = this.Content.Load<Texture2D>("BlobIdle");
            t[0][1] = this.Content.Load<Texture2D>("BlobRight");
            t[0][2] = this.Content.Load<Texture2D>("BlobLeft");

            t[1][0] = this.Content.Load<Texture2D>("BlobIdleDown");
            t[1][1] = this.Content.Load<Texture2D>("BlobIdleJump1");
            t[1][2] = this.Content.Load<Texture2D>("BlobIdleJump2");
            t[1][3] = this.Content.Load<Texture2D>("BlobIdleJump3");
            t[1][4] = this.Content.Load<Texture2D>("BlobIdleJump4");
            t[1][5] = this.Content.Load<Texture2D>("BlobIdleJump5");

            t[2][0] = this.Content.Load<Texture2D>("BlobRightDown");
            t[2][1] = this.Content.Load<Texture2D>("BlobRightJump1");
            t[2][2] = this.Content.Load<Texture2D>("BlobRightJump2");
            t[2][3] = this.Content.Load<Texture2D>("BlobRightJump3");
            t[2][4] = this.Content.Load<Texture2D>("BlobRightJump4");
            t[2][5] = this.Content.Load<Texture2D>("BlobRightJump5");

            t[3][0] = this.Content.Load<Texture2D>("BlobLeftDown");
            t[3][1] = this.Content.Load<Texture2D>("BlobLeftJump1");
            t[3][2] = this.Content.Load<Texture2D>("BlobLeftJump2");
            t[3][3] = this.Content.Load<Texture2D>("BlobLeftJump3");
            t[3][4] = this.Content.Load<Texture2D>("BlobLeftJump4");
            t[3][5] = this.Content.Load<Texture2D>("BlobLeftJump5");
            //dash right
            t[4][0] = this.Content.Load<Texture2D>("BlobRightDash1");
            t[4][1] = this.Content.Load<Texture2D>("BlobRightDash2");
            t[4][2] = this.Content.Load<Texture2D>("BlobRightDash3");
            t[4][3] = this.Content.Load<Texture2D>("BlobRightDash4");
            t[4][4] = this.Content.Load<Texture2D>("BlobRightDash5");

            t[5][0] = this.Content.Load<Texture2D>("BlobLeftDash1");
            t[5][1] = this.Content.Load<Texture2D>("BlobLeftDash2");
            t[5][2] = this.Content.Load<Texture2D>("BlobLeftDash3");
            t[5][3] = this.Content.Load<Texture2D>("BlobLeftDash4");
            t[5][4] = this.Content.Load<Texture2D>("BlobLeftDash5");

            p = new Player(t, new Rectangle(100, 100, 100, 100));
            font1 = this.Content.Load<SpriteFont>("SpriteFont1");
            font2 = this.Content.Load<SpriteFont>("SpriteFont2");
            Logo = this.Content.Load<Texture2D>("Logo");
            //tutorial
            BlockTextures[0][0] = this.Content.Load<Texture2D>("Untitled");
            BlockTextures[0][1] = this.Content.Load<Texture2D>("KeyDoor"); // unused as of now
            BlockTextures[0][2] = this.Content.Load<Texture2D>("SpikeU");
            BlockTextures[0][3] = this.Content.Load<Texture2D>("checkpoint");
            BlockTextures[0][4] = this.Content.Load<Texture2D>("Key");
            BlockTextures[0][5] = this.Content.Load<Texture2D>("SpikeD (1)");
            BlockTextures[0][6] = this.Content.Load<Texture2D>("SpikeR (1)");
            BlockTextures[0][7] = this.Content.Load<Texture2D>("SpikeL (1)");
            BlockTextures[0][8] = this.Content.Load<Texture2D>("DungeonTileWall1");
            BlockTextures[0][9] = this.Content.Load<Texture2D>("DungeonTileWall2");
            BlockTextures[0][10] = this.Content.Load<Texture2D>("DungeonTileWall3");
            BlockTextures[0][11] = this.Content.Load<Texture2D>("levelHubDoorU");
            BlockTextures[0][12] = this.Content.Load<Texture2D>("levelHubDoorD");
            BlockTextures[0][13] = this.Content.Load<Texture2D>("DungeonTileFloor");
            BlockTextures[0][14] = this.Content.Load<Texture2D>("SignUnread");
            BlockTextures[0][15] = this.Content.Load<Texture2D>("SignRead");
            //red
            BlockTextures[1][0] = this.Content.Load<Texture2D>("Untitled");
            BlockTextures[1][1] = this.Content.Load<Texture2D>("redAir");
            BlockTextures[1][2] = this.Content.Load<Texture2D>("redSpikeUp");
            BlockTextures[1][3] = this.Content.Load<Texture2D>("redCheckpoint");
            BlockTextures[1][4] = this.Content.Load<Texture2D>("Key");
            BlockTextures[1][5] = this.Content.Load<Texture2D>("firedoorU");
            BlockTextures[1][6] = this.Content.Load<Texture2D>("firedoorD");
            BlockTextures[1][7] = this.Content.Load<Texture2D>("torch");
            BlockTextures[1][8] = this.Content.Load<Texture2D>("littorch");
            BlockTextures[1][9] = this.Content.Load<Texture2D>("enemy");
            BlockTextures[1][10] = this.Content.Load<Texture2D>("colorCollectable");
            BlockTextures[1][11] = this.Content.Load<Texture2D>("SignUnread");
            BlockTextures[1][12] = this.Content.Load<Texture2D>("SignRead");
            BlockTextures[1][13] = this.Content.Load<Texture2D>("redBarrier");
            BlockTextures[1][14] = this.Content.Load<Texture2D>("redFloor");
            BlockTextures[1][15] = this.Content.Load<Texture2D>("redSpikeDown");
            BlockTextures[1][16] = this.Content.Load<Texture2D>("redSpikeLeft");
            BlockTextures[1][17] = this.Content.Load<Texture2D>("redSpikeRight");
            BlockTextures[1][18] = this.Content.Load<Texture2D>("redFloorUp");
            BlockTextures[1][19] = this.Content.Load<Texture2D>("redBossKey");
            BlockTextures[1][20] = this.Content.Load<Texture2D>("redBucket");
            BlockTextures[1][21] = this.Content.Load<Texture2D>("bottle");
            //blue
            BlockTextures[2][0] = this.Content.Load<Texture2D>("Untitled");
            BlockTextures[2][1] = this.Content.Load<Texture2D>("KeyDoor"); // unused as of now
            BlockTextures[2][2] = this.Content.Load<Texture2D>("SpikeU");
            BlockTextures[2][3] = this.Content.Load<Texture2D>("checkpoint");
            BlockTextures[2][4] = this.Content.Load<Texture2D>("Key");
            BlockTextures[2][5] = this.Content.Load<Texture2D>("SpikeD (1)");
            BlockTextures[2][6] = this.Content.Load<Texture2D>("SpikeR (1)");
            BlockTextures[2][7] = this.Content.Load<Texture2D>("SpikeL (1)");
            BlockTextures[2][8] = this.Content.Load<Texture2D>("DungeonTileWall1");
            BlockTextures[2][9] = this.Content.Load<Texture2D>("DungeonTileWall2");
            BlockTextures[2][10] = this.Content.Load<Texture2D>("DungeonTileWall3");
            BlockTextures[2][11] = this.Content.Load<Texture2D>("levelHubDoorU");
            BlockTextures[2][12] = this.Content.Load<Texture2D>("levelHubDoorD");
            BlockTextures[2][13] = this.Content.Load<Texture2D>("DungeonTileFloor");
            BlockTextures[2][14] = this.Content.Load<Texture2D>("SignUnread");
            BlockTextures[2][15] = this.Content.Load<Texture2D>("SignRead");
            //yellow
            BlockTextures[3][0] = this.Content.Load<Texture2D>("Untitled");
            BlockTextures[3][1] = this.Content.Load<Texture2D>("Tile");
            BlockTextures[3][2] = this.Content.Load<Texture2D>("YSpikeU");
            BlockTextures[3][3] = this.Content.Load<Texture2D>("checkpoint");
            BlockTextures[3][4] = this.Content.Load<Texture2D>("KeyY");
            BlockTextures[3][5] = this.Content.Load<Texture2D>("Wire");
            BlockTextures[3][6] = this.Content.Load<Texture2D>("YellowFloor");
            BlockTextures[3][7] = this.Content.Load<Texture2D>("YellowBackground");
            BlockTextures[3][8] = this.Content.Load<Texture2D>("YellowDoorUL");
            BlockTextures[3][9] = this.Content.Load<Texture2D>("YellowDoorUR");
            BlockTextures[3][10] = this.Content.Load<Texture2D>("YellowDoorDL");
            BlockTextures[3][11] = this.Content.Load<Texture2D>("YellowDoorDR");

            BlockTextures[3][12] = this.Content.Load<Texture2D>("YellowGenerator");
            BlockTextures[3][13] = this.Content.Load<Texture2D>("YellowRecieverOff");
            BlockTextures[3][14] = this.Content.Load<Texture2D>("YellowGenerator");

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
            BlockTextures[3][25] = this.Content.Load<Texture2D>("YLaserVertTVarient");
            BlockTextures[3][26] = this.Content.Load<Texture2D>("YLaserVertMVarient");
            BlockTextures[3][27] = this.Content.Load<Texture2D>("YLaserVertBVarient");
            BlockTextures[3][28] = this.Content.Load<Texture2D>("YLaserHorizLVarient");
            BlockTextures[3][29] = this.Content.Load<Texture2D>("YLaserHorizMVarient");
            BlockTextures[3][30] = this.Content.Load<Texture2D>("YLaserHorizRVarient");
            BlockTextures[3][31] = this.Content.Load<Texture2D>("YellowOverloadCrystalOff");
            BlockTextures[3][32] = this.Content.Load<Texture2D>("YellowOverloadCrystal");
            BlockTextures[3][33] = this.Content.Load<Texture2D>("PowerGridOn");
            BlockTextures[3][34] = this.Content.Load<Texture2D>("PowerGridDestroyed");
            BlockTextures[3][35] = this.Content.Load<Texture2D>("WireCharged");
            BlockTextures[3][36] = this.Content.Load<Texture2D>("levelHubDoorUY");
            BlockTextures[3][37] = this.Content.Load<Texture2D>("levelHubDoorD");
            BlockTextures[3][38] = this.Content.Load<Texture2D>("BossKeyY");
            BlockTextures[3][39] = this.Content.Load<Texture2D>("colorCollectable");
            BlockTextures[3][40] = this.Content.Load<Texture2D>("YellowSignUnread");
            BlockTextures[3][41] = this.Content.Load<Texture2D>("YellowSignRead");

            BlockTextures[3][42] = this.Content.Load<Texture2D>("bottle");
            BlockTextures[3][43] = this.Content.Load<Texture2D>("bucket");
            //level hub
            BlockTextures[4][0] = this.Content.Load<Texture2D>("Untitled");
            BlockTextures[4][1] = this.Content.Load<Texture2D>("Tile");
            BlockTextures[4][2] = this.Content.Load<Texture2D>("SpikeU");
            BlockTextures[4][3] = this.Content.Load<Texture2D>("checkpoint");
            BlockTextures[4][4] = this.Content.Load<Texture2D>("Key");
            BlockTextures[4][5] = this.Content.Load<Texture2D>("YellowEntranceDoorU");
            BlockTextures[4][6] = this.Content.Load<Texture2D>("YellowEntranceDoorD");
            BlockTextures[4][8] = this.Content.Load<Texture2D>("DungeonTileWall1");
            BlockTextures[4][9] = this.Content.Load<Texture2D>("DungeonTileWall2");
            BlockTextures[4][10] = this.Content.Load<Texture2D>("DungeonTileWall3");
            BlockTextures[4][11] = this.Content.Load<Texture2D>("BossDoorDL");
            BlockTextures[4][12] = this.Content.Load<Texture2D>("BossDoorDR");
            BlockTextures[4][13] = this.Content.Load<Texture2D>("BossDoorUL");
            BlockTextures[4][14] = this.Content.Load<Texture2D>("BossDoorUR");
            BlockTextures[4][15] = this.Content.Load<Texture2D>("DungeonTileFloor");
            BlockTextures[4][16] = this.Content.Load<Texture2D>("PlaceHolderBoss");
            BlockTextures[4][17] = this.Content.Load<Texture2D>("Stuff");
            BlockTextures[4][18] = this.Content.Load<Texture2D>("firedoorU");
            BlockTextures[4][19] = this.Content.Load<Texture2D>("firedoorD");

            barTex = this.Content.Load<Texture2D>("bar");
            levelLoader = new LevelLoader(fileNames, BlockTextures, 1);

            PlayButton = new Button(BlockTextures[3][0], new Rectangle(800, 500, 250, 100), Button.ButtonType.Play);
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

            if (kb.IsKeyDown(Keys.F11) && !oldKB.IsKeyDown(Keys.F11))
            {
                if (!graphics.IsFullScreen)
                {
                    graphics.PreferredBackBufferWidth = 1920;
                    graphics.PreferredBackBufferHeight = 1080;
                }
                else
                {
                    graphics.PreferredBackBufferWidth = 1280;
                    graphics.PreferredBackBufferHeight = 720;
                }

                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
            }

            float virtualWidth = 1900f;
            float virtualHeight = 1000f;

            float scaleX = GraphicsDevice.Viewport.Width / virtualWidth;
            float scaleY = GraphicsDevice.Viewport.Height / virtualHeight;
            float scale = Math.Min(scaleX, scaleY);

            float offsetX = (GraphicsDevice.Viewport.Width - virtualWidth * scale) / 2f;
            float offsetY = (GraphicsDevice.Viewport.Height - virtualHeight * scale) / 2f;

            Vector2 worldMouse = new Vector2((mouse.X - offsetX) / scale,(mouse.Y - offsetY) / scale);

            if (gameState == GameState.MainMenu)
            {
                PlayButton.isInteracting(new Rectangle((int)worldMouse.X, (int)worldMouse.Y, 2, 2), mouse, oldM, this);
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

            float scaleX = GraphicsDevice.Viewport.Width / 1900f;
            float scaleY = GraphicsDevice.Viewport.Height / 1000f;
            float scale = Math.Min(scaleX, scaleY);

            float offsetX = (GraphicsDevice.Viewport.Width - 1900 * scale) / 2f;
            float offsetY = (GraphicsDevice.Viewport.Height - 1000 * scale) / 2f;

            Matrix transform = Matrix.CreateScale(scale, scale, 1f) * Matrix.CreateTranslation(offsetX, offsetY, 0);

            GraphicsDevice.Clear(Color.Black);

            if (gameState == GameState.MainMenu)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, transform);
                PlayButton.Draw(spriteBatch);
                spriteBatch.Draw(Logo, new Rectangle(603, 50, 660, 450), Color.White);
                spriteBatch.DrawString(font1, "Play", new Vector2(903, 536), Color.Black);
                spriteBatch.DrawString(font2, "Press F11 to enter fullscreen", new Vector2(670, 850), Color.White);
                spriteBatch.End();
            }
            else if (gameState == GameState.Game)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, transform);
                levelLoader.DrawAll(spriteBatch, p, font1);
                p.Draw(spriteBatch);
                spriteBatch.End();
            }
            else if (gameState == GameState.Pause)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null, transform);
                levelLoader.DrawAll(spriteBatch, p, font1);
                p.Draw(spriteBatch);
                spriteBatch.DrawString(font1, "Paused", new Vector2(903, 200), Color.Black);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
    }
}

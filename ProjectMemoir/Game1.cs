using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectMemoir.Sprites;
using ProjectMemoir.Components;
using System.Collections.Generic;
using ProjectMemoir.Scenes;

namespace ProjectMemoir
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public PlayerStats ps;
        Texture2D transitionScreen;
        float talpha;
        public InputManager input;
        public SoundManager soundManager;
        

        public Scene currentScene, nextScene;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

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
            soundManager = new SoundManager(this.Content);
            ps = new PlayerStats();
            talpha = 0f;
            input = new InputManager();
            transitionScreen = Content.Load<Texture2D>("ForP");
            // TODO: use this.Content to load your game content here
            currentScene = new MainMenu(this, Content);
            currentScene.Load();
            nextScene = null;
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
        protected override void Update(GameTime _gameTime)
        {

            input.Update();
            if (nextScene == null)
            {
                if(talpha > 0)
                {
                    talpha -= 0.1f;
                }
                currentScene.Update(_gameTime);
            } else
            {
                {
                    if (talpha < 1)
                    {
                        talpha += 0.1f;
                    }
                    else
                    {
                        currentScene = nextScene;
                        nextScene = null;
                        currentScene.Load();
                    }
                }
            }
            base.Update(_gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            currentScene.Draw(spriteBatch, gameTime);
            spriteBatch.Begin();
            spriteBatch.Draw(transitionScreen, new Rectangle(0, 0, 1280, 720), new Rectangle(0,0,32,32),Color.White * talpha);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

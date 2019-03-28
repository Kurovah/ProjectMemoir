using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectMemoir.Sprites;
using System.Collections.Generic;

namespace ProjectMemoir
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Player player;
        private DummyEn den;
        private List<Sprite> spriteList;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 360;
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


            // TODO: use this.Content to load your game content here
            spriteList = new List<Sprite>();
            spriteList.Add(player = new Player(this.Content, new Vector2(40)));
            spriteList.Add(den = new DummyEn(this.Content, new Vector2(400,200), player));
            //solids to collide with
            spriteList.Add(new Solid(this.Content, new Vector2(0), new Vector2(3, 360)));
            spriteList.Add(new Solid(this.Content, new Vector2(0), new Vector2(640,3)));
            spriteList.Add(new Solid(this.Content, new Vector2(0,360), new Vector2(640, 3)));
            spriteList.Add(new Solid(this.Content, new Vector2(640,0), new Vector2(3, 360)));
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

            player.Update(_gameTime, spriteList);
            den.Update(_gameTime, spriteList);

            base.Update(_gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            spriteBatch.Begin();

            foreach(Sprite _s in spriteList) {
                _s.Draw(spriteBatch);
            }
            

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

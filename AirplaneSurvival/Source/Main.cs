using AirplaneSurvival.Source;
using AirplaneSurvival.Source.Engine;
using AirplaneSurvival.Source.Entities;
using AirplaneSurvival.Source.Entities.Powerups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace AirplaneSurvival
{
    public class Main : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static readonly Random random = new Random();

        public static Main MainGame { get; private set; }

        // The width and height dimensions of the screen.
        public const int screenWidth = 800,
                         screenHeight = 600;

        // Returns the screen rectangle.
        public static RectangleF ScreenRect => new RectangleF(0, 0, screenWidth, screenHeight);

        Component screen;

        public void ChangeScreen(Component newScreen)
        {
            /*
             * Changes the screen and disposes the last one,
             * stopping it from using up unwanted memory.
             */
            if (screen is IDisposable disposable) disposable.Dispose();
            screen = newScreen;
        }

        public Main()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = screenWidth,
                PreferredBackBufferHeight = screenHeight
            };

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Window.Title = "Airplane Survival";

            MainGame = this;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Input.LoadData(this);

            // Loads dynamic content (just a blank pixel for this project).
            SpecialContent.LoadContent(GraphicsDevice);

            /*
             * The sound effects for the powerups and the missiles
             * are loaded from the beginning, as loading them while
             * playing often creates lag spikes.
             */

            SpeedUp.LoadContent();
            Shield.LoadContent();
            Missile.LoadContent();

            // Loads the title screen.
            screen = new TitleScreen();
        }

        protected override void UnloadContent()
        {
            // Unloads the dynamic content.
            SpecialContent.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            // Closes if the Escape key is pressed.
            if (Input.KeyDown(Keys.Escape))
                Exit();

            // Updates the current screen.
            screen?.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Displays the current screen.
            screen?.Show(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
    }
}

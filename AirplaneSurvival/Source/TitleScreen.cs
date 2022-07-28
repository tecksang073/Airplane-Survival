using AirplaneSurvival.Source.Engine;
using AirplaneSurvival.Source.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirplaneSurvival.Source
{
    public class TitleScreen : ControlGroup
    {
        static readonly SpriteFont titleFont, buttonFont;

        static TitleScreen()
        {
            // Loads the title and button fonts from memory.
            titleFont = Main.MainGame.Content.Load<SpriteFont>("Fonts/TitleFont");
            buttonFont = Main.MainGame.Content.Load<SpriteFont>("Fonts/ButtonFont");
        }

        readonly Clouds clouds;

        readonly string title = "Airplane Survival Game";

        bool done = false;

        public TitleScreen()
        {
            // Loads the cloud background.
            clouds = new Clouds();

            // Adds the title of the game.
            components.Add(new Label(this)
            {
                Position = new Point(Main.screenWidth / 2, (int)(Main.screenHeight * (1f / 3))),
                Font = titleFont,
                TextAlign = Label.TextAlignEnum.CENTRE_CENTRE,
                Text = title,
                ForeColour = Color.Black
            });

            // Adds the play button.
            var playBtn = new Button(this)
            {
                Font = buttonFont,
                Text = "Play",
                Size = new Point(100, 50),
                Centre = new Point(Main.screenWidth / 2, (int)(Main.screenHeight * (2f / 3)))
            };

            playBtn.ClickEvent += s =>
            {
                // When the play button is pressed, the game is loaded.
                done = true;
            };

            components.Add(playBtn);
        }

        public override void Update(GameTime gameTime)
        {
            // Moves clouds to the right.
            clouds.Move(new Vector2(-gameTime.Multiplier(), 0));

            // Updates all components (title and button).
            base.Update(gameTime);

            /*
             * The game is loaded after all components are updated
             * so that there are no crashes when the screen
             * is disposed.
             */

            if (done)
                Main.MainGame.ChangeScreen(new GameScreen());
        }

        public override void Show(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.GraphicsDevice.Clear(World.backgroundColour);

            // Displays the cloud background.

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            clouds.Show(gameTime, spriteBatch);
            spriteBatch.End();

            // Displays all components.
            base.Show(gameTime, spriteBatch);
        }
    }
}

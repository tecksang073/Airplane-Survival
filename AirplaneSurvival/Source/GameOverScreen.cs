using AirplaneSurvival.Source.Engine;
using AirplaneSurvival.Source.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirplaneSurvival.Source
{
    public class GameOverScreen : ControlGroup
    {
        // The translucent background colour.
        static readonly Color overlayColour = new Color(0, 0, 0, 100);

        static readonly SpriteFont gameOverFont;

        static GameOverScreen()
        {
            // Loads the font for the game over text.
            gameOverFont = Main.MainGame.Content.Load<SpriteFont>("Fonts/GameOverFont");
        }

        readonly GameScreen gs;

        public GameOverScreen(GameScreen _gs)
        {
            // Assigns the instance of 'GameScreen' so that the game can be reset later.
            gs = _gs;

            // Adds the game over text.
            components.Add(new Label(this)
            {
                Position = new Point(Main.screenWidth / 2, Main.screenHeight / 2),
                TextAlign = Label.TextAlignEnum.CENTRE_CENTRE,
                Font = gameOverFont,
                Text = $"Game Over!{Environment.NewLine}{Environment.NewLine}Press R to Restart."
            });
        }

        public override void Update(GameTime gameTime)
        {
            // Updates the components (the text).
            base.Update(gameTime);

            if (Input.KeyPressed(Keys.R))
            {
                // Resets the game if R is pressed.
                gs.Reset();
            }
        }

        public override void Show(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draws the translucent dark overlay.
            spriteBatch.Begin();
            spriteBatch.Draw(SpecialContent.Pixel, Main.ScreenRect, overlayColour);
            spriteBatch.End();

            // Displays all components.
            base.Show(gameTime, spriteBatch);
        }
    }
}

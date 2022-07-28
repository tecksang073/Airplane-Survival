using AirplaneSurvival.Source.Engine;
using AirplaneSurvival.Source.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirplaneSurvival.Source.Entities
{
    public partial class Player
    {
        public class FrenzyGauge : Component
        {
            static readonly SpriteFont font;
            static readonly SoundEffect frenzyActivate;

            static FrenzyGauge()
            {
                // Loads the font for the text above the bar.
                font = Main.MainGame.Content.Load<SpriteFont>("Fonts/FrenzyGaugeFont");
                frenzyActivate = Main.MainGame.Content.Load<SoundEffect>("Sounds/Frenzy");
            }

            public FrenzyGauge()
            {
                // Instantiates the bar.
                frenzyBar = new ProgressBar()
                {
                    size = new Vector2(200, 20),
                    Max = 120,
                };

                // Positions the bar in the top-middle of the screen.
                frenzyBar.position = new Vector2((Main.screenWidth - frenzyBar.size.X) / 2, 25f);
            }

            readonly ProgressBar frenzyBar;

            Vector2 textPosition;
            string gaugeTitle = "";

            void SetText(string text)
            {
                // Assigns the text and re-positions it.
                gaugeTitle = text;
                Vector2 size = font.MeasureString(text);
                textPosition = new Vector2((Main.screenWidth - size.X) / 2, 5f);
            }

            public bool InFrenzy { get; private set; }

            public void Increment(int change)
            {
                // Adds to the bar value when it is not in frenzy.

                if (!InFrenzy)
                {
                    frenzyBar.Value += change;

                    if (frenzyBar.Value == frenzyBar.Max)
                    {
                        InFrenzy = true;
                        SetText("Frenzy Mode!");

                        frenzyActivate.Play();
                    }
                }
            }

            public override void Update(GameTime gameTime)
            {
                // Slowly decreases the bar value.
                frenzyBar.Value--;

                // Turns frenzy mode off when the bar is at 0.
                if (frenzyBar.Value == 0)
                {
                    InFrenzy = false;
                    SetText("Normal Mode");
                }
                
            }

            public override void Show(GameTime gameTime, SpriteBatch spriteBatch)
            {
                // Displays the bar.
                frenzyBar.Show(spriteBatch);

                // Displays the bar text,
                spriteBatch.DrawString(font, gaugeTitle, textPosition, Color.Black);
            }
        }
    }
    
}

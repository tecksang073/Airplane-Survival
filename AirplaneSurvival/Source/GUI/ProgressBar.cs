using AirplaneSurvival.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirplaneSurvival.Source.GUI
{
    public class ProgressBar
    {
        public Vector2 position, size;
        float max, value;

        public float Max
        {
            get
            {
                return max;
            }
            set
            {
                // Assigns the max value and limits the value to it.
                this.value = MathF.Min(value, max);
                max = value;
            }
        }

        public float Value
        {
            get
            {
                return value;
            }
            set
            {
                // Assigns the value and clamps it to the minimum and maximum values.
                this.value = MathHelper.Clamp(value, 0, max);
            }
        }

        public void Show(SpriteBatch spriteBatch)
        {
            // Assigns the width of the green bar depending on the value, maximum value and size.
            float barSize = size.X * (value / max);

            Texture2D tex = SpecialContent.Pixel;

            // Draws the green section of the bar.
            spriteBatch.Draw(tex,
                             position,
                             null,
                             Color.Lime,
                             0f,
                             Vector2.Zero,
                             new Vector2(barSize, size.Y) / new Vector2(tex.Width, tex.Height),
                             SpriteEffects.None,
                             0f);

            // Draws the red section of the bar.
            spriteBatch.Draw(tex,
                             position + new Vector2(barSize, 0),
                             null,
                             Color.Red,
                             0f,
                             Vector2.Zero,
                             new Vector2(size.X - barSize, size.Y) / new Vector2(tex.Width, tex.Height),
                             SpriteEffects.None,
                             0f);

            // Draws a black border around the bar.
            spriteBatch.DrawRectangle(new RectangleF(position, size), Color.Black);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirplaneSurvival.Source.GUI
{
    public class Label : Control
    {
        public Label(ControlGroup manager) : base(manager) { }

        private string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                // Assigns the text and re-positions it.
                text = value;
                
                SetTextPosition();
            }
        }

        public new Point Position { get { return base.Position; } set { base.Position = value; SetTextPosition(); } }

        Vector2 textSize;

        SpriteFont font;
        public SpriteFont Font
        {
            get
            {
                return font;
            }
            set
            {
                // Assigns the font and re-positions the text.

                font = value;
                SetTextPosition();
            }
        }

        TextAlignEnum textAlign;
        public TextAlignEnum TextAlign { get { return textAlign; } set { textAlign = value; SetTextPosition(); } }
        public enum TextAlignEnum
        {
            TOP_LEFT,
            TOP_CENTRE,
            TOP_RIGHT,
            CENTRE_LEFT,
            CENTRE_CENTRE,
            CENTRE_RIGHT,
            BOTTOM_LEFT,
            BOTTOM_CENTRE,
            BOTTOM_RIGHT
        }

        public Color ForeColour { get; set; } = Color.White;

        private Point textPos = Point.Zero;

        private void SetTextPosition()
        {
            // Sizes the text if the text and font are not null.
            if (text != null && Font != null) textSize = Font.MeasureString(text);

            // Positions the text depending on the alignment.
            Point offset = new Point();

            int h = (int)textAlign / 3;
            int w = (int)textAlign % 3;

            if (h == 1) offset -= new Point(0, (int)textSize.Y / 2);
            if (h == 2) offset -= new Point(0, (int)textSize.Y);

            if (w == 1) offset -= new Point((int)textSize.X / 2, 0);
            if (w == 2) offset -= new Point((int)textSize.X, 0);

            textPos = Position + offset;
        }

        protected override void UpdateControl(GameTime gameTime) { }

        protected override void ShowControl(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draws the text.

            spriteBatch.Begin(transformMatrix: manager.Matrix);
            spriteBatch.DrawString(Font, text, textPos.ToVector2(), ForeColour);
            spriteBatch.End();
        }
    }
}

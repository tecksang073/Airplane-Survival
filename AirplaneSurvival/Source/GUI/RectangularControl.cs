using AirplaneSurvival.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirplaneSurvival.Source.GUI
{
    public abstract class RectangularControl : Control
    {
        public RectangularControl(ControlGroup manager) : base(manager) { }

        public Point Size { get; set; }
        public Rectangle Rectangle => new Rectangle(Position, Size);

        public int Right { get => Position.X + Size.X; set { Position.X = value - Size.X; } }
        public int Bottom { get => Position.Y + Size.Y; set { Position.Y = value - Size.Y; } }

        public Point Centre
        {
            get
            {
                return Position + new Point(Size.X / 2, Size.Y / 2);
            }
            set
            {
                Position = value - new Point(Size.X / 2, Size.Y / 2);
            }
        }

        public bool ShowBorder { get; set; } = false;

        public Color BorderColor { get; set; } = Color.Black;
        public float BorderThickness { get; set; } = 1f;

        protected override void ShowControl(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (ShowBorder)
            {
                // Draws a border around the control if it is specified to.
                spriteBatch.DrawRectangle((RectangleF)Rectangle, BorderColor, BorderThickness);
            }
        }
    }
}

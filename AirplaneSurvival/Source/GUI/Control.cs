using System;
using AirplaneSurvival.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AirplaneSurvival.Source.GUI
{
    public abstract class Control : Component, IDisposable
    {
        protected ControlGroup manager;
        public Control(ControlGroup _manager) =>
            manager = _manager;

        public Point Position = Point.Zero;

        public int Left { get => Position.X; set { Position.X = value; } }
        public int Top { get => Position.Y; set { Position.Y = value; } }

        public bool active = true;

        public string Tag { get; set; }

        public override void Update(GameTime gameTime)
        {
            if (active)
            {
                // Updates the control if it's active.
                UpdateControl(gameTime);
            }
        }

        public override void Show(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (active)
            {
                // Displays the control if it's active.
                ShowControl(gameTime, spriteBatch);
            }
        }

        protected abstract void UpdateControl(GameTime gameTime);
        protected abstract void ShowControl(GameTime gameTime, SpriteBatch spriteBatch);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool IsDisposed { get; private set; }
        protected virtual void Dispose(bool disposing)
        {
            IsDisposed = true;
        }
    }


}

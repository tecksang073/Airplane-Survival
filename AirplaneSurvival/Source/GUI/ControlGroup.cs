using AirplaneSurvival.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirplaneSurvival.Source.GUI
{
    public abstract class ControlGroup : Component, IDisposable
    {
        protected List<Control> components = new List<Control>();

        public Matrix Matrix { get; set; } = Matrix.Identity;

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < components.Count; i++)
            {
                // Updates all components.
                components[i].Update(gameTime);
            }
        }

        public override void Show(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < components.Count; i++)
            {
                // Displays all components.
                components[i].Show(gameTime, spriteBatch);
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].Dispose();
            }

            components = null;
        }
    }
}

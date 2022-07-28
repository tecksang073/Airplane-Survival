using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirplaneSurvival.Source.Engine
{
    public abstract class Sprite : Component
    {
        public Vector2 position, size, offset, texMultiplier = Vector2.One;

        public float Left => position.X;
        public float Right => position.X + size.X;
        public float Top => position.Y;
        public float Bottom => position.Y + size.Y;
        public Vector2 Centre => position + size / 2;
        public void SetCentre(Vector2 set)
        {
            position = set - size / 2;
        }

        public RectangleF Rectangle => new RectangleF(position, size);
        public virtual Texture2D GetTexture => null;

        public SpriteEffects spriteEffects;
        public Color color = Color.White;
        public float rotation;
        public float layerDepth;

        public bool Visible { get; set; } = true;

        public override void Show(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                Texture2D tex = GetTexture;

                // Draws the texture if it exists.
                if (tex != null)
                {
                    spriteBatch.Draw(tex, 
                                     position + offset + size / 2, 
                                     null, 
                                     color, 
                                     rotation, 
                                     new Vector2(tex.Width, tex.Height) / 2,
                                     size * texMultiplier / new Vector2(tex.Width, tex.Height), 
                                     spriteEffects, 
                                     layerDepth);
                }
            }
            
        }
    }
}

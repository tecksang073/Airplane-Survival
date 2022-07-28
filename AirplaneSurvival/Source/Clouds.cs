using AirplaneSurvival.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirplaneSurvival.Source
{
    public class Clouds : Component
    {
        static readonly Texture2D tex;

        // Multiplies the size of the cloud texture when drawing.
        const int sizeMultiplier = 4;

        static int Width => tex.Width * sizeMultiplier;
        static int Height => tex.Height * sizeMultiplier;

        static Clouds()
        {
            // Loads the clouds texture.
            tex = Main.MainGame.Content.Load<Texture2D>("Clouds");
        }

        Vector2 position;

        public void Reset() => position = Vector2.Zero;

        public void Move(Vector2 move)
        {
            // Moves the position of the clouds.
            position += move;

            // Wraps the clouds to the other side of the screen when necessary.
            while (position.X < -Width)
                position.X += Width;

            while (position.X > 0)
                position.X -= Width;

            while (position.Y < -Height)
                position.Y += Height;

            while (position.Y > 0)
                position.Y -= Height;
        }

        public override void Show(GameTime gameTime, SpriteBatch spriteBatch)
        {
            /*
             * This code draws the clouds in repeating fashion
             * so that the screen is filled with them.
             */

            int x = (int)position.X;

            while(x < Main.screenWidth)
            {
                int y = (int)position.Y;
                while(y < Main.screenHeight)
                {
                    spriteBatch.Draw(tex, new Rectangle(x, y, Width, Height), Color.White);
                    y += Height;
                }

                x += Width;
            }
            
        }
    }
}

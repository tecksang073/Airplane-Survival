using AirplaneSurvival.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AirplaneSurvival.Source.Entities
{
    public class ColourParticle : Entity
    {
        const float speed = 4f;

        public ColourParticle(World world, Color c) : base(world, 0)
        {
            // Assigns the colour of the particle.
            startColor = c;
            color = c;

            size = Vector2.One * 4;

            // Uses RNG to calculate a random velocity to it to move in.
            double r = Main.random.NextDouble() * MathHelper.TwoPi;
            velocity = new Vector2((float)Math.Cos(r),
                                   (float)Math.Sin(r)) * speed;
        }

        public override Texture2D GetTexture => SpecialContent.Pixel;

        readonly Color startColor;
        readonly Vector2 velocity;

        const int liveLength = 15,
                  fadeLength = 5;

        float aliveDuration;

        public override void Update(GameTime gameTime)
        {
            // Moves the particle.
            position += velocity * gameTime.Multiplier();

            // Increments the 'aliveDuration' variable.
            aliveDuration += gameTime.Multiplier();

            if (aliveDuration > liveLength + fadeLength)
            {
                // Kills the particle if it has lasted long enough.
                Finished = true;
            }
            else if (aliveDuration > liveLength)
            {
                // The particle fades away after a certain amount of time.
                color = Color.Lerp(startColor, Color.Transparent, 1f - ((float)(aliveDuration - liveLength) / fadeLength));
            }
        }
    }
}

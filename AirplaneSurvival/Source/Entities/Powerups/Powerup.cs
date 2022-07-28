using AirplaneSurvival.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirplaneSurvival.Source.Entities.Powerups
{
    public abstract class Powerup : Entity
    {
        public static Powerup RandomPowerup(World world)
        {
            /*
             * Uses RNG to decide whether the powerup is a boost or a shield.
             * 
             * 5/6 chance of boost.
             * 1/6 chance of shield.
             */

            int r = Main.random.Next(6);

            if (r > 0)
            {
                return new SpeedUp(world);
            } else
            {
                return new Shield(world);
            }
        }

        const float minSpawnDistance = 128f,
                    maxSpawnDistance = 256f;

        CountdownTimer despawnCountdown;
        const float timeToDespawn = 240f;

        public Powerup(World world) : base(world, 16)
        {
            size = Vector2.One * 32;

            // Calculates a random direction for the powerup to spawn from.
            float theta = (float)Main.random.NextDouble() * MathHelper.TwoPi;

            // Calculates a random distance between the minimum and maximum constants specified.
            float dist = (float)Main.random.NextDouble() * (maxSpawnDistance - minSpawnDistance) + minSpawnDistance;

            // Positions the powerup according to these values.
            SetCentre(Main.ScreenRect.Center + new Vector2(MathF.Cos(theta), MathF.Sin(theta)) * dist);

            // Instantiates the despawn timer.
            despawnCountdown = new CountdownTimer(timeToDespawn);
        }

        public override Texture2D GetTexture => SpecialContent.Pixel;

        public override void Update(GameTime gameTime)
        {
            if (CollidingWith(world.player))
            {
                // The powerup is collected upon colliding with the player.
                Collect();
                Kill();
            }
            
            if (!OnScreen())
            {
                // Decrements the despawn timer only when it is offscreen.
                despawnCountdown.Decrement(gameTime.Multiplier());

                // Kills the powerup when the timer reaches 0.
                if (despawnCountdown.Ended)
                {
                    Kill();
                }
            } else
            {
                // Resets the despawn timer when the powerup is on-screen.
                despawnCountdown.Reset();
            }
        }

        protected abstract void Collect();
    }
}

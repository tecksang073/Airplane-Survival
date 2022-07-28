using AirplaneSurvival.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirplaneSurvival.Source.Entities
{
    public partial class PlayerShield : Component
    {
        readonly Vector2 centre;
        ShieldBall[] shieldItems;

        readonly World world;

        public PlayerShield(World _world, Vector2 _centre)
        {
            world = _world;

            centre = _centre;

            // Initializes an array of 3 shield balls.
            shieldItems = new ShieldBall[3];

            PositionShields();
        }

        float rot = 0f;
        const float rotSpeed = MathHelper.Pi / 16;

        const float radius = 96f;

        void PositionShields()
        {
            // Creates an positions 3 shield balls.
            for (int i = 0; i < 3; i++)
            {
                // Initializes a new shield ball.
                shieldItems[i] = new ShieldBall(world);

                // Assigns the angle of the ball as a fraction of a full circle in radians plus the rotation.
                float theta = rot + MathHelper.TwoPi / 3 * i;

                // Uses cosine and sine to set the position of the ball in the correct place.
                shieldItems[i].SetCentre(centre + new Vector2(MathF.Cos(theta),
                                                              MathF.Sin(theta)) * radius);
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Rotates the shield by the rotation speed.
            rot += rotSpeed * gameTime.Multiplier();

            // Repositions the shield balls.
            PositionShields();
            
            // Updates all of the shield balls.
            for (int i = 0; i < 3; i++)
                shieldItems[i].Update(gameTime);
        }

        public override void Show(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Displays all of the shield balls.
            for (int i = 0; i < 3; i++)
                shieldItems[i].Show(gameTime, spriteBatch);
        }
    }
}

using AirplaneSurvival.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirplaneSurvival.Source.Entities
{
    public partial class Player : Entity, IVulnerable
    {
        static readonly Texture2D tex, frenzyCircle;

        static Player()
        {
            tex = Main.MainGame.Content.Load<Texture2D>("Entities/Plane_0");
            frenzyCircle = Main.MainGame.Content.Load<Texture2D>("FrenzyCircle");
        }

        public override Texture2D GetTexture => tex;

        public Player(World world) : base(world, 32)
        {
            size = new Vector2(64);

            // Places player in the centre of the screen.
            SetCentre(Main.ScreenRect.Center);

            // Player begins facing upward.
            direction = new Vector2(0, -1);

            // Set frenzy gauge
            frenzyGauge = new FrenzyGauge();

            // Assigns the timers for frenzy, boost and shield durations.
            frenzyCountdown = new CountdownTimer(frenzyGaugeMax);
            boostCountdown = new CountdownTimer(boostLength, false);
            shieldCountdown = new CountdownTimer(shieldLength, false);
        }

        public Vector2 direction;

        const float moveSpeed = 4f;

        // Returns the speed, which is doubled if it is boosting.
        float TotalSpeed => moveSpeed * (!boostCountdown.Ended ? 2 : 1);

        CountdownTimer boostCountdown;
        const float boostLength = 120f;

        PlayerShield shield;
        CountdownTimer shieldCountdown;
        const float shieldLength = 600f;

        CountdownTimer frenzyCountdown;
        const float frenzyGaugeMax = 120f;

        const float frenzyRadius = 128f;

        public readonly FrenzyGauge frenzyGauge;

        public void Damage(Missile damager)
        {
            if (!frenzyGauge.InFrenzy)
            {
                // Kills the player if it is not in frenzy.
                Kill();
            } else
            {
                // Kills the missile if the player isn't frenzy.
                damager.Kill();
            }
        }

        public override void Kill()
        {
            base.Kill();

            // Explodes with orange particles on death.
            Explode(Color.Orange);

            // Game over when player dies.
            world.gs.GameOver();
        }

        public void Boost()
        {
            // Resets the boost countdown when the boost starts.
            boostCountdown.Reset();
        }

        public void BeginShield()
        {
            if (shield == null) shield = new PlayerShield(world, Centre);

            // Resets the shield countdown when the boost starts.
            shieldCountdown.Reset();
        }

        public override void Update(GameTime gameTime)
        {
            // Decrements the boost and shield timers.

            boostCountdown.Decrement(gameTime.Multiplier());
            shieldCountdown.Decrement(gameTime.Multiplier());

            // Removes the shield when the shield timer ends.
            if (shieldCountdown.Ended)
                shield = null;

            if (Input.LeftMouseDown())
            {
                // When the left mouse is down, the player faces in the direction of the mouse.
                direction = Input.MousePosition.ToVector2() - Centre;
                direction.Normalize();

                FaceDirection(direction);
            }

            // Moves the world in the opposite direction to the player's movement.
            world.Move(direction * TotalSpeed * gameTime.Multiplier());

            // Leaves smoke particles in the opposite direction.
            LeaveSmoke(-direction);

            /*
             * Increases the frenzy gauge when a missile is in
             * the frenzy range.
             */

            if (!frenzyGauge.InFrenzy && world.entities.Exists(x => x is Missile && Vector2.DistanceSquared(Centre, x.Centre) < frenzyRadius * frenzyRadius))
            {
                frenzyGauge.Increment(2);
            }

            // Updates the frenzy gauge (decreases it over time).
            frenzyGauge.Update(gameTime);

            // Updates the shield if it exists.
            shield?.Update(gameTime);
        }

        // The colour of the translucent circle around the player.
        static readonly Color frenzyCircleColour = new Color(0, 0, 0, 100);

        public override void Show(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Specifies the size of the circle with its diameter.
            var frenzyCircleSize = new Vector2(frenzyRadius * 2);

            // Draws the circle.
            spriteBatch.Draw(frenzyCircle, new Rectangle((Centre - frenzyCircleSize / 2).ToPoint(), frenzyCircleSize.ToPoint()), frenzyCircleColour);

            // Draws the player.
            base.Show(gameTime, spriteBatch);

            // Draws the shield if it exists.
            shield?.Show(gameTime, spriteBatch);

            // Draws the frenzy gauge.
            frenzyGauge.Show(gameTime, spriteBatch);
        }
    }
}

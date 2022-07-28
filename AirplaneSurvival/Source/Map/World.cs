using System;
using System.Collections.Generic;
using System.Text;
using AirplaneSurvival.Source.Entities;
using AirplaneSurvival.Source.Entities.Powerups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using AirplaneSurvival.Source.Engine;

namespace AirplaneSurvival.Source
{
    public class World : Component
    {
        // The background colours of the game, including the frenzy colour.
        public static readonly Color backgroundColour = new Color(70, 200, 255),
                                     frenzyColour     = new Color(255, 100, 0);

        public GameScreen gs;

        public World(GameScreen _gs)
        {
            gs = _gs;

            // Creates the cloud background.
            clouds = new Clouds();

            // Instantiates a player and a entity list.
            player = new Player(this);
            entities = new List<Entity>();

            // Creates the timers for missle and powerup spawns.
            missileSpawnCountdown = new CountdownTimer(missileSpawnDelay);
            powerupSpawnCountdown = new CountdownTimer(powerupSpawnDelay);

            // Resets the world state.
            Reset();
        }

        public void Reset()
        {
            // Resets the clouds to their normal position.
            clouds.Reset();

            // Reassigns the player and clears the entity list.
            player = new Player(this);
            entities.Clear();

            // Resets the missle and powerup spawn timers.
            missileSpawnCountdown.Reset();
            powerupSpawnCountdown.Reset();
        }

        readonly Clouds clouds;

        public Player player;
        public readonly List<Entity> entities;

        // Essentially returns a list of all alive entities including the player.
        public IEnumerable<Entity> AllEntities =>
            entities.Concat(new[] { player }).Where(x => !x.Finished);

        public void Move(Vector2 move)
        {
            /*
             * Instead of having a camera and having it
             * follow a player which moves around the world,
             * the player is locked in place and all other objects
             * move in the opposite direction of the player. This
             * gives the illusion of movement and makes a lot of
             * aspects of the game much easier to program.
             */

            foreach (var entity in entities)
                entity.position -= move;

            clouds.Move(-move);
        }

        CountdownTimer missileSpawnCountdown;
        const float missileSpawnDelay = 120;    // The length of time it takes for a new missile to be spawned.

        CountdownTimer powerupSpawnCountdown;
        const float powerupSpawnDelay = 360;    // The length of time it takes for a new powerup to be spawned.

        public override void Update(GameTime gameTime)
        {
            if (!player.Finished)
            {
                /*
                 * Updates the missle and powerup spawn timers. When they
                 * reach the end, they reset and the corresponding object
                 * spawns.
                 */

                missileSpawnCountdown.Decrement(gameTime.Multiplier());
                powerupSpawnCountdown.Decrement(gameTime.Multiplier());

                if (missileSpawnCountdown.IfEndedReset())
                {
                    var missile = new Missile(this);

                    entities.Add(missile);
                }

                if (powerupSpawnCountdown.IfEndedReset())
                {
                    var powerup = Powerup.RandomPowerup(this);

                    entities.Add(powerup);
                }

                // Update the player.
                player.Update(gameTime);
            }

            /*
             * Updates all the entities and removes those that are finished.
             * Updates them in opposite order so that those being removed
             * don't cause any conflict.
             */

            for (int i = entities.Count - 1; i >= 0; i--)
            {
                entities[i].Update(gameTime);
                if (entities[i].Finished)
                    entities.RemoveAt(i);
            }
        }

        public override void Show(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draws the background corresponding to whether the player is in frenzy or not.
            if (!player.frenzyGauge.InFrenzy)
                spriteBatch.GraphicsDevice.Clear(backgroundColour);
            else
                spriteBatch.GraphicsDevice.Clear(frenzyColour);


            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            // Draws the clouds.
            clouds.Show(gameTime, spriteBatch);

            // Draws the player if it isn't dead.
            if (!player.Finished) player.Show(gameTime, spriteBatch);

            // Draws all of the entities.
            foreach (var entity in entities)
                entity.Show(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}

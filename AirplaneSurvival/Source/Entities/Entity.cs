using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AirplaneSurvival.Source.Engine;

namespace AirplaneSurvival.Source.Entities
{
    public class Entity : Sprite
    {
        protected readonly World world;

        public readonly int colRadius;

        public Entity(World _world, int _colRadius)
        {
            world = _world;
            colRadius = _colRadius;
        }

        public bool Finished { get; protected set; }

        public virtual void Kill()
        {
            // Finishes the entity when killed. This can be overriden by other entities.
            Finished = true;
        }

        public void FaceDirection(Vector2 face)
        {
            // Rotates the sprite to face a direction.
            rotation = MathF.PI - MathF.Atan2(face.X, face.Y);
        }

        /*
         * Compares the distance between the two entities 
         * and checks if it is less than the sum of their 
         * radii to see if their collision circles are intersecting.
         */

        public bool CollidingWith(Entity other) =>
            Vector2.Distance(Centre, other.Centre) < colRadius + other.colRadius;

        public void Explode(Color c)
        {
            // Creates 8 coloured particles which move out in random directions at the centre.
            for (int i = 0; i < 8; i++)
            {
                var particle = new ColourParticle(world, c);
                particle.SetCentre(Centre);

                world.entities.Add(particle);
            }
        }

        protected void LeaveSmoke(Vector2 direction)
        {
            // Creates a smoke particle and places it behind the entity.
            var smoke = new Smoke(world);
            smoke.SetCentre(Centre + direction * size.Y / 2);
            world.entities.Add(smoke);
        }

        public bool OnScreen()
        {
            // Checks if the rectangle intersects the screen rectangle to see if it is on screen.
            return Rectangle.Intersects(Main.ScreenRect);
        }

        // Essentially returns a list of all other entities in the world.
        public IEnumerable<Entity> OtherEntities() =>
            world.AllEntities.Where(x => x != this);

        public override void Show(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Only renders if the entity is on screen.
            if (OnScreen())
                base.Show(gameTime, spriteBatch);
        }
    }
}

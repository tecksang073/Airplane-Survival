using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework.Audio;
using AirplaneSurvival.Source.Engine;

namespace AirplaneSurvival.Source.Entities
{
    // When an entity implements this, they can be hit by missiles.
    public interface IVulnerable
    {
        void Damage(Missile by);
    }

    public class Missile : Entity, IVulnerable
    {
        static SoundEffect explosion;
        public static void LoadContent()
        {
            // Loads the explosion sound effect.
            explosion = Main.MainGame.Content.Load<SoundEffect>("Sounds/Explosion");
        }

        static readonly Texture2D tex;

        static Missile()
        {
            // Loads the missile texture.
            tex = Main.MainGame.Content.Load<Texture2D>("Entities/Missile");
        }

        public override Texture2D GetTexture => tex;

        public Missile(World world) : base(world, 12)
        {
            size = new Vector2(16, 32);

            // Uses RNG for the side in which the missile spawns from.
            int side = Main.random.Next(4);

            switch (side)
            {
                // Spawns the missile at a random point of the side of the line specified by the random number.
                case 0:
                    
                    position.X = (float)Main.random.NextDouble() * Main.screenWidth - size.X;
                    position.Y = -size.Y;

                    break;
                case 1:
                    position.X = (float)Main.random.NextDouble() * Main.screenWidth - size.X;
                    position.Y = Main.screenHeight;

                    break;
                case 2:
                    position.X = -size.X;
                    position.Y = (float)Main.random.NextDouble() * Main.screenHeight - size.Y;

                    break;
                case 3:
                    position.X = Main.screenWidth;
                    position.Y = (float)Main.random.NextDouble() * Main.screenHeight - size.Y;

                    break;
            }

            // Faces in the direction of the player.
            direction = world.player.Centre - Centre;
            direction.Normalize();

            FaceDirection(-direction);
            moveRot = rotation;
        }

        float moveRot;

        const float moveSpeed = 5f,
                    turnSpeed = 0.05f;

        Vector2 direction;

        public bool aboutToDie;

        public override void Update(GameTime gameTime)
        {
            // Only turns to the player if it is alive.
            if (!world.player.Finished) TurnToPlayer();

            // Moves the missile.
            position += direction * moveSpeed * gameTime.Multiplier();

            // Leaves smoke behind the missile.
            LeaveSmoke(-direction);

            // Damages all vulnerable entities colliding with it.
            foreach (var entity in OtherEntities().Where(x => x is IVulnerable && CollidingWith(x)).ToArray())
            {
                (entity as IVulnerable).Damage(this);
                aboutToDie = true;
            }

            if (aboutToDie)
            {
                // Plays the explosion sound effect.
                explosion.Play();

                // Kills the missile.
                Kill();
            }
        }

        void TurnToPlayer()
        {
            // Calculates the direction it is aiming for.
            Vector2 desiredDir = world.player.position - position;
            desiredDir.Normalize();

            // Calculates how much it has to rotate by to reach the desired direction.
            float rotateAmount = Vector3.Cross(new Vector3(desiredDir, 0f), new Vector3(direction, 0f)).Z;

            // Rotates the missile towards the desired direction depending on the turn speed.
            moveRot += rotateAmount * turnSpeed;

            // Assigns the direction to move in.
            direction = new Vector2(MathF.Cos(-moveRot), MathF.Sin(-moveRot));

            // Assigns the sprite direction.
            rotation = -MathHelper.PiOver2 - moveRot;
        }

        public void Damage(Missile by)
        {
            // Kills the missile when hit by another missile.
            Kill();
        }

        public override void Kill()
        {
            // Increments the score if the player is alive.
            if (!world.player.Finished) world.gs.IncreaseScore(3);

            // Explodes in an orange-red colour upon death.
            Explode(Color.OrangeRed);

            base.Kill();
        }
    }
}

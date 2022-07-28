using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace AirplaneSurvival.Source.Entities
{
    public partial class PlayerShield
    {
        public class ShieldBall : Entity
        {
            static readonly Texture2D tex;
            static ShieldBall()
            {
                // Loads the texture for the shield ball.
                tex = Main.MainGame.Content.Load<Texture2D>("Entities/Shield_Ball");
            }

            public override Texture2D GetTexture => tex;

            public ShieldBall(World world) : base(world, 16)
            {
                // Assigns the size of the ball to 16x16.
                size = new Vector2(16);
            }

            public override void Update(GameTime gameTime)
            {
                // Kills all missiles that it is colliding with.
                foreach (var missile in world.entities.Where(x => x is Missile && CollidingWith(x)))
                {
                    (missile as Missile).aboutToDie = true;
                }
            }
        }
    }
}

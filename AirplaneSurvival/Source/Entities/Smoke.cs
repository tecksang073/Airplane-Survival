using AirplaneSurvival.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirplaneSurvival.Source.Entities
{
    public class Smoke : Entity
    {
        static readonly Texture2D[] texes;

        static Smoke()
        {
            // Loads the textures for the smoke animation.
            texes = new Texture2D[4];

            for (int i = 0; i < 4; i++)
                texes[i] = Main.MainGame.Content.Load<Texture2D>($"Entities/Smoke/Smoke_{4-i}");
        }

        readonly Animation animation;

        // Returns the current texture in the animation.
        public override Texture2D GetTexture => animation.GetTexture;

        public Smoke(World world) : base(world, 0)
        {
            animation = new Animation(texes, 5f);
            size = new Vector2(4);
        }

        public override void Update(GameTime gameTime)
        {
            // Increments the animation value. If the animation has looped the entity is finished.
            int last = animation.CurID;
            animation.Increment(gameTime.Multiplier());
            int current = animation.CurID;

            if (last != current && current < last)
                Finished = true;
            
        }
    }
}

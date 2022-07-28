using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirplaneSurvival.Source.Entities.Powerups
{
    public class SpeedUp : Powerup
    {
        static SoundEffect boost;

        public static void LoadContent()
        {
            // Loads the boost sound effect.
            boost = Main.MainGame.Content.Load<SoundEffect>("Sounds/Boost");
        }

        static readonly Texture2D tex;
        static SpeedUp()
        {
            // Loads the texture for the boost powerup.
            tex = Main.MainGame.Content.Load<Texture2D>("Entities/Boost");
        }

        public override Texture2D GetTexture => tex;

        public SpeedUp(World world) : base(world) { }

        protected override void Collect()
        {
            // Boosts the player and plays the sound effect.
            world.player.Boost();
            boost.Play();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirplaneSurvival.Source.Entities.Powerups
{
    public class Shield : Powerup
    {
        static SoundEffect shieldActivate;
        public static void LoadContent()
        {
            // Loads the shield activate sound effect.
            shieldActivate = Main.MainGame.Content.Load<SoundEffect>("Sounds/ShieldActivate");
        }

        static readonly Texture2D tex;
        static Shield()
        {
            // Loads the texture for the shield powerup.
            tex = Main.MainGame.Content.Load<Texture2D>("Entities/Shield");
        }

        public override Texture2D GetTexture => tex;

        public Shield(World world) : base(world) {}

        protected override void Collect()
        {
            // Activates the shield and plays the sound effect.
            world.player.BeginShield();
            shieldActivate.Play();
        }
    }
}

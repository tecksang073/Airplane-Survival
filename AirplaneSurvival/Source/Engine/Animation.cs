using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirplaneSurvival.Source.Engine
{
    public class Animation
    {
        readonly Texture2D[] textures;

        int currentID = 0;
        public int CurID { get => currentID; set { currentID = value; frameWait = 0; } }

        public Texture2D GetTexture => textures[CurID];

        readonly float frameTime;
        float frameWait = 0;

        public Animation(Texture2D[] _textures, int _frameTime)
        {
            textures = _textures;
            frameTime = _frameTime;
        }

        public Animation(Texture2D[] _textures, float _frameTime)
        {
            textures = _textures;
            frameTime = _frameTime;
        }

        public void Reset() => CurID = 0;

        public void Increment(float amount)
        {
            // Increase the wait duration by the amount.
            frameWait += amount;

            while (frameWait >= frameTime)
            {
                frameWait -= frameTime;

                /*
                 * Decreases the 'frameWait' variable by the length of one
                 * frame passing and increments the animation ID until
                 * the 'frameWait' variable is less than the length of a
                 * frame.
                 */

                currentID++;
                if (currentID == textures.Length)
                    currentID = 0;
            }
        }
    }
}

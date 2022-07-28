using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AirplaneSurvival.Source.Engine
{
    public static class SpecialContent
    {
        public static Texture2D Pixel { get; private set; }
        public static void LoadContent(GraphicsDevice graphicsDevice)
        {
            // Assigns the pixel variable to a singular white pixel.

            Pixel = new Texture2D(graphicsDevice, 1, 1);
            Pixel.SetData(new Color[] { Color.White });
        }

        public static void UnloadContent()
        {
            Pixel.Dispose();
        }
    }
}

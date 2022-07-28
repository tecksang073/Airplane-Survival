using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AirplaneSurvival.Source.Engine
{
    /*
     * An abstract class for objects in
     * the game which are updated and
     * displayed.
     */
    public abstract class Component
    {
        public virtual void Update(GameTime gameTime) { }
        public virtual void Show(GameTime gameTime, SpriteBatch spriteBatch) { }
    }
}

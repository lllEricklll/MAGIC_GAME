// IScreen.cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Magic.Content
{
    public interface IScreen
    {
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch, int selectedOption);
    }
}

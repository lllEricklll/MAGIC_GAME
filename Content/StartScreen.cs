using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Magic.Content
{
    public class StartScreen
    {
        private Texture2D startScreenTexture;
        private SoundEffect opcionCambiadaSound;
        private SoundEffect startSound;
        private SoundEffect exitSound;
        private SoundEffectInstance soundInstance;
        private Rectangle startScreenRect;
        private Vector2 startTextPosition;
        private Vector2 inicioPosition;
        private Vector2 exitTextPosition;
        private string startText = "Start";
        private string exitText = "Exit";
        private string InicioScreen = "The Magic World????";
        private SpriteFont font;


        public StartScreen(Texture2D texture, GraphicsDevice graphicsDevice, SpriteFont spriteFont) 
        {
            startScreenTexture = texture;
            font = spriteFont;

            // Asegurar que la imagen de inicio abarque toda la pantalla
            startScreenRect = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);

            inicioPosition = new Vector2(
                (graphicsDevice.Viewport.Width - font.MeasureString(InicioScreen).X) / 2,
                graphicsDevice.Viewport.Height / 4 - font.MeasureString(InicioScreen).Y / 1);

            // Calcular la posición para el texto "Start" en el centro superior
            startTextPosition = new Vector2(
                (graphicsDevice.Viewport.Width - font.MeasureString(startText).X) / 2,
                graphicsDevice.Viewport.Height * 2 / 4 - font.MeasureString(startText).Y / 2 + 150);

            // Calcular la posición para el texto "Exit" en el centro inferior
            exitTextPosition = new Vector2(
            (graphicsDevice.Viewport.Width - font.MeasureString(exitText).X) / 2,
            graphicsDevice.Viewport.Height * 3 / 4 - font.MeasureString(exitText).Y / 2 + 80);
        }

        public void SetOpcionCambiadaSound(SoundEffect sound)
        {
            opcionCambiadaSound = sound;
        }
        public void Update(GameTime gameTime)
        {
            // Lógica de actualización, si es necesario
            // Por ejemplo, para cambiar a la siguiente pantalla al presionar una tecla
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                // Lógica para avanzar a la siguiente pantalla
            }
        }

        public void Draw(SpriteBatch spriteBatch, int selectedOption)
        {
            spriteBatch.Begin();

            // Dibujar la imagen de inicio que abarca toda la pantalla
            spriteBatch.Draw(startScreenTexture, startScreenRect, Color.White);

            float titleScale = 1.5f; // Puedes ajustar este valor según sea necesario
            spriteBatch.DrawString(font, InicioScreen, inicioPosition, Color.White, 0f, Vector2.Zero, titleScale, SpriteEffects.None, 0f);

            // Dibujar el texto "Start" en el centro superior
            spriteBatch.DrawString(font, startText, startTextPosition, selectedOption == 0 ? Color.Yellow : Color.Black);

            // Dibujar el texto "Exit" en el centro inferior
            spriteBatch.DrawString(font, exitText, exitTextPosition, selectedOption == 1 ? Color.Yellow : Color.Black);

            spriteBatch.End();
        }
    }
}

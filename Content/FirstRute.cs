// FirstRute.cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Magic.Content
{
    public class FirstRute
    {
        private GraphicsDevice graphicsDevice;
        private SpriteFont font;
        private Texture2D startImage;
        private int selectedOptionIndex; // Índice de la opción seleccionada en el menú
        private bool isSelectingOption;   // Indica si el jugador está en la fase de selección de opciones
        private bool showMessageBox; // Nuevo campo para controlar si se debe mostrar el cuadro de mensaje // Nuevo campo para almacenar el mensaje
        private bool animateText;
        private float elapsedTextDisplayTime;
        private float textDisplayDuration = 0.05f; // Ajusta según la velocidad deseada de aparición del texto
        private int displayedCharacters;
        private List<string> textLines; // Lista para almacenar las líneas de texto
        private int currentLineIndex; // Índice de la línea de texto actual
        private bool displayFullText; // Variable para controlar si se debe mostrar todo el texto de una vez
        private bool showAdditionalBox;
        private string additionalBoxText;

        public FirstRute(GraphicsDevice graphicsDevice, SpriteFont font, Texture2D startImage)
        {
            this.graphicsDevice = graphicsDevice;
            this.font = font;
            this.startImage = startImage;
            this.showMessageBox = true; // Inicializa el cuadro de mensaje como visible
            this.animateText = true;
            this.showAdditionalBox = true; // Muestra el cuadro adicional como visible
            this.additionalBoxText = "????";
            textLines = new List<string>
            {
                "Eh?...... D..dddonde estoy?",
                "Qué es esto?",
                "Por qué estoy  en un bosque?",
                "No entiendo nada.",
                "Me duele, mucho la cabeza me eh de aver golpeado muy fuerte, que lugar para mas triste y solitario...",
                "Deberia levantarme y  buscar personas que me expliquen que es este lugar..."
            };
            this.elapsedTextDisplayTime = 0f;
            this.displayedCharacters = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (animateText && displayFullText)
            {
                elapsedTextDisplayTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (elapsedTextDisplayTime >= textDisplayDuration)
                {
                    displayedCharacters = Math.Min(displayedCharacters + 1, GetCurrentLine().Length);
                    elapsedTextDisplayTime = 0f;

                    if (displayedCharacters == GetCurrentLine().Length)
                    {
                        animateText = false;
                    }
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    int totalOptions = 2; // Número total de opciones en el menú
                                          // Verificar que no estemos en la primera opción antes de decrementar
                    if (selectedOptionIndex > 0)
                    {
                        selectedOptionIndex = (selectedOptionIndex - 1 + totalOptions) % totalOptions;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    int totalOptions = 2; // Número total de opciones en el menú
                                          // Verificar que no estemos en la última opción antes de incrementar
                    if (selectedOptionIndex < totalOptions - 1)
                    {
                        selectedOptionIndex = (selectedOptionIndex + 1) % totalOptions;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    if (!isSelectingOption)
                    {
                        isSelectingOption = true;
                        Console.WriteLine($"Opción seleccionada: {selectedOptionIndex + 1}");
                        // Agrega aquí la lógica adicional que se ejecutará al seleccionar una opción.
                    }
                }
                else
                {
                    if (isSelectingOption && Keyboard.GetState().IsKeyDown(Keys.Back))
                    {
                        isSelectingOption = false;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (!animateText && displayedCharacters == GetCurrentLine().Length)
                {
                    displayedCharacters = 0;
                    animateText = true;
                    currentLineIndex++;
                    if (currentLineIndex >= textLines.Count)
                    {
                        currentLineIndex = 0;
                    }
                    displayFullText = false;
                }
                else
                {
                    displayFullText = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, bool showStartImage)
        {
            spriteBatch.Begin();

            if (showStartImage)
            {
                spriteBatch.Draw(startImage, new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height), Color.White);
            }

            if (showMessageBox)
            {
                DrawMessageBox(spriteBatch);
                DrawMessage(spriteBatch);

                // Verifica si se ha completado la conversación
                if (!animateText && displayedCharacters == GetCurrentLine().Length && currentLineIndex == textLines.Count - 1)
                {
                    // Muestra el menú de opciones
                    DrawOptionsMenu(spriteBatch);
                }
            }

            spriteBatch.End();
        }

        private void DrawOptionsMenu(SpriteBatch spriteBatch)
        {
            // Configuración del menú de opciones
            Vector2 menuPosition = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
            Vector2 optionOffset = new Vector2(0, 30);

            // Pregunta central
            string question = "¿Qué vas a hacer?";
            Vector2 questionSize = font.MeasureString(question);
            Vector2 questionPosition = menuPosition - new Vector2(questionSize.X / 2, 0);
            spriteBatch.DrawString(font, question, questionPosition, Color.White);

            // Opciones
            string option1 = "1.- Levantarse";
            string option2 = "2.- Quedarse sentado";

            spriteBatch.DrawString(font, option1, menuPosition - new Vector2(0, optionOffset.Y), selectedOptionIndex == 0 ? Color.Red : Color.White);
            spriteBatch.DrawString(font, option2, menuPosition + new Vector2(0, optionOffset.Y), selectedOptionIndex == 1 ? Color.Red : Color.White);
        }

        private void DrawMessageBox(SpriteBatch spriteBatch)
        {
            Color boxColor = new Color(0, 0, 0, 128);
            int boxHeight = 100;
            Rectangle boxRectangle = new Rectangle(0, graphicsDevice.Viewport.Height - boxHeight, graphicsDevice.Viewport.Width, boxHeight);
            spriteBatch.Draw(startImage, boxRectangle, boxColor);

            if (showAdditionalBox)
            {
                Color additionalBoxColor = new Color(0, 0, 0); // Cambiado a negro
                int additionalBoxWidth = 100;
                int additionalBoxHeight = 50;
                Rectangle additionalBoxRectangle = new Rectangle(10, graphicsDevice.Viewport.Height - boxHeight - additionalBoxHeight, additionalBoxWidth, additionalBoxHeight);

                // Dibuja el cuadro adicional con color negro
                spriteBatch.Draw(startImage, additionalBoxRectangle, additionalBoxColor);

                // Ajusta la posición de "Math"
                Vector2 additionalBoxTextPosition = new Vector2(additionalBoxRectangle.X + 15, additionalBoxRectangle.Y + 10);
                spriteBatch.DrawString(font, additionalBoxText, additionalBoxTextPosition, Color.White);
            }
        }

        private string GetCurrentLine()
        {
            return textLines[currentLineIndex];
        }

        private void DrawMessage(SpriteBatch spriteBatch)
        {
            Vector2 messagePosition = new Vector2(
                10, graphicsDevice.Viewport.Height - 80);

            Console.WriteLine($"Message position: {messagePosition}");
            if (!string.IsNullOrWhiteSpace(GetCurrentLine()))
            {
                string visibleMessage = GetCurrentLine();

                if (displayedCharacters > 0 && displayedCharacters <= visibleMessage.Length)
                {
                    string partialMessage = visibleMessage.Substring(0, displayedCharacters);
                    if (AreAllGlyphsValid(partialMessage))
                    {
                        spriteBatch.DrawString(font, partialMessage, messagePosition, Color.White);
                    }
                    else
                    {
                        Console.WriteLine($"Glyph no válido en el mensaje parcial: {partialMessage}");
                    }
                }
                else if (displayedCharacters == 0)
                {
                    spriteBatch.DrawString(font, "", messagePosition, Color.White);
                }
            }
            else
            {
                Console.WriteLine("El mensaje es nulo o está vacío.");
            }
        }

        private bool AreAllGlyphsValid(string text)
        {
            // Mide la longitud del texto utilizando el SpriteFont
            float measuredLength = font.MeasureString(text).X;

            // Si la longitud medida es cero, significa que hay caracteres no válidos
            return measuredLength > 0;
        }
    }
}

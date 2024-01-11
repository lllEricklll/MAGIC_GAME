// Game1.cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Magic.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Magic
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private object currentScreen;
        private StartScreen startScreen;
        private FirstRute firstRute;
        private int selectedOption;
        private Song backgroundMusic;
        private SoundEffect opcionCambiadaSoundEffect;
        private Texture2D startImage;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        private enum GameState
        {
            StartScreen,
            FirstRute,
        }
        private GameState gameState;

        protected override void Initialize()
        {
            startScreen = new StartScreen(Content.Load<Texture2D>("start"), GraphicsDevice, Content.Load<SpriteFont>("Font"));

            startImage = Content.Load<Texture2D>("firstRuteForest");
            firstRute = new FirstRute(GraphicsDevice, Content.Load<SpriteFont>("Font"), startImage);

            currentScreen = startScreen;
            gameState = GameState.StartScreen;

            selectedOption = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;

            backgroundMusic = Content.Load<Song>("startMusic");
            MediaPlayer.Play(backgroundMusic);

            opcionCambiadaSoundEffect = Content.Load<SoundEffect>("startSoundMenu");

            startScreen = new StartScreen(Content.Load<Texture2D>("start"), GraphicsDevice, Content.Load<SpriteFont>("Font"));

            selectedOption = 0;

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (gameState == GameState.StartScreen)
            {
                startScreen.Update(gameTime);

                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    if (selectedOption != 0)
                    {
                        selectedOption = (selectedOption - 1 + 2) % 2;
                        SoundEffectInstance soundInstance = opcionCambiadaSoundEffect.CreateInstance();
                        soundInstance.Play();
                    }
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    if (selectedOption != 1)
                    {
                        selectedOption = (selectedOption + 1) % 2;
                        SoundEffectInstance soundInstance = opcionCambiadaSoundEffect.CreateInstance();
                        soundInstance.Play();
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    switch (selectedOption)
                    {
                        case 0:
                            MediaPlayer.Stop();
                            MediaPlayer.Play(Content.Load<Song>("firstRuteBackGround"));
                            gameState = GameState.FirstRute;
                            break;
                        case 1:
                            Exit();
                            break;
                    }
                }
            }
            else if (gameState == GameState.FirstRute)
            {
                firstRute.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (gameState == GameState.StartScreen)
            {
                startScreen.Draw(_spriteBatch, selectedOption);
            }
            else if (gameState == GameState.FirstRute)
            {
                firstRute.Draw(_spriteBatch, selectedOption == 0);
            }

            base.Draw(gameTime);
        }
    }
}

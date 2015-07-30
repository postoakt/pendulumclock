using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pendulum_Clock
{   


    public class Game1 : Microsoft.Xna.Framework.Game
    {
        const float WINDOWED_SCREEN_WIDTH = 1024;
        const float WINDOWED_SCREEN_HEIGHT = 768;


        GraphicsDeviceManager graphics;
        SpriteFont spritefont;
        SpriteBatch spriteBatch;
        KeyboardState keyboardstate;
        KeyboardState lastkeyboardstate;
        Texture2D background;
        Planets planets;
        Clock clock;
        Pendulum pendulum;

        public Game1()
        {   
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        } // Constructor

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = (int)WINDOWED_SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = (int)WINDOWED_SCREEN_HEIGHT;
            graphics.ApplyChanges();
            base.Initialize();
        } // Initialize

        protected override void LoadContent()
        {
            clock = new Clock(12, 0, 0, Content);
            background = Content.Load<Texture2D>("stars");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spritefont = Content.Load<SpriteFont>("SpriteFont");

            planets = new Planets(Content, graphics);
            pendulum = new Pendulum(Content);

        } // LoadContent


        protected override void Update(GameTime gameTime)
        {
            HandleInput();

            pendulum.Update(planets.GetCurrGrav(), gameTime);
            clock.Update(planets.GetCurrGrav(), gameTime);

            base.Update(gameTime);
        } // Update


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            planets.Draw(spriteBatch);

            pendulum.Draw(spriteBatch, spritefont);
            clock.Draw(spriteBatch, spritefont);

            spriteBatch.DrawString(spritefont, planets.GetPlanetName(), new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(spritefont, "g = " + Math.Round(planets.GetCurrGrav(), 4) + " m/s^2", new Vector2(0, 20), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        } // Draw

        private void HandleInput()
        {
            keyboardstate = Keyboard.GetState();

            planets.Update(keyboardstate, lastkeyboardstate);

            if (keyboardstate.IsKeyDown(Keys.Escape))
                this.Exit();

            if (keyboardstate.IsKeyDown(Keys.F5) && lastkeyboardstate.IsKeyUp(Keys.F5))
            {
                ToggleFullScreen();
            }

            lastkeyboardstate = keyboardstate;

        } //HandleInput

        private void ToggleFullScreen()
        {
            graphics.ToggleFullScreen();
        } // ToggleFullScreen
    }
}

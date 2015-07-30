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
    class Planets
    {
        const float WINDOWED_SCREEN_WIDTH = 1024;
        const float WINDOWED_SCREEN_HEIGHT = 768;

        const double G = 0.0000000000667f;

        const double MERC_RADIUS = 2439700f;
        const double MERC_MASS = 330220000000000000000000f;
        const double MERC_GRAV = (G * MERC_MASS) / (MERC_RADIUS * MERC_RADIUS);

        const double VEN_RADIUS = 6051800f;
        const double VEN_MASS = 4867600000000000000000000f;
        const double VEN_GRAV = (G * VEN_MASS) / (VEN_RADIUS * VEN_RADIUS);

        const double EARTH_RADIUS = 6371000f;
        const double EARTH_MASS = 5972190000000000000000000f;
        const double EARTH_GRAV = (G * EARTH_MASS) / (EARTH_RADIUS * EARTH_RADIUS);

        const double MOON_RADIUS = 1737100f;
        const double MOON_MASS = 73477000000000000000000f;
        const double MOON_GRAV = (G * MOON_MASS) / (MOON_RADIUS * MOON_RADIUS);

        const double MARS_RADIUS = 3396200f;
        const double MARS_MASS = 641850000000000000000000f;
        const double MARS_GRAV = (G * MARS_MASS) / (MARS_RADIUS * MARS_RADIUS);

        const double JUP_RADIUS =  	69911000f;
        const double JUP_MASS = 1898600000000000000000000000f;
        const double JUP_GRAV = (G * JUP_MASS) / (JUP_RADIUS * JUP_RADIUS);

        const double SAT_RADIUS = 60268000f;
        const double SAT_MASS = 568460000000000000000000000f;
        const double SAT_GRAV = (G * SAT_MASS) / (SAT_RADIUS * SAT_RADIUS);

        const double URAN_RADIUS = 25559000f;
        const double URAN_MASS = 86810000000000000000000000f;
        const double URAN_GRAV = (G * URAN_MASS) / (URAN_RADIUS * URAN_RADIUS);

        const double NEP_RADIUS = 24764000f;
        const double NEP_MASS = 102430000000000000000000000f;
        const double NEP_GRAV = (G * NEP_MASS) / (NEP_RADIUS * NEP_RADIUS);

        const double SUN_RADIUS = 696342000f;
        const double SUN_MASS = 1989100000000000000000000000000f;
        const double SUN_GRAV = (G * SUN_MASS) / (SUN_RADIUS * SUN_RADIUS);

        currentPlanet CurrentPlanet;
        ContentManager Content;
        Vector2 Position;
        double Curr_Grav;
        Texture2D CurrentTexture;
        GraphicsDeviceManager Graphics;
        bool ismoving;
        bool nextplanet;
        int index;

        public Planets(ContentManager content, GraphicsDeviceManager graphics)
        {
            Content = content;
            Graphics = graphics;

            ismoving = false;
            nextplanet = false;

            index = 3;

            CurrentPlanet = currentPlanet.Earth;

            Curr_Grav = EARTH_GRAV;

            CurrentTexture = Content.Load<Texture2D>("P_" + index);

            Position.Y = Graphics.PreferredBackBufferHeight - CurrentTexture.Height;
            Position.X = 0;

        } // Constructor Planets

        enum currentPlanet
        {
            Mercury,
            Venus,
            Earth,
            Moon,
            Mars,
            Jupiter,
            Saturn,
            Uranus,
            Neptune,
            Sun
        }; // enum currentPlanet

        public void Update(KeyboardState keyboard, KeyboardState lastkeyboard)
        {
            if (keyboard.IsKeyDown(Keys.Space) && lastkeyboard.IsKeyUp(Keys.Space))
            {
                ismoving = true;
            }

            if (ismoving)
            {
                MoveOffScreen(nextplanet);

                if (Position.Y > CurrentTexture.Height + WINDOWED_SCREEN_HEIGHT && nextplanet == false)
                {
                    Position.Y = Graphics.PreferredBackBufferWidth - CurrentTexture.Height;
                    incrementCurrPlanet();
                    nextplanet = true;
                }

                if (Position.Y < WINDOWED_SCREEN_HEIGHT - CurrentTexture.Height && nextplanet == true)
                {
                    Position.Y = WINDOWED_SCREEN_HEIGHT - CurrentTexture.Height;
                    ismoving = false;
                    nextplanet = false;
                }
            }
        } // Update

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CurrentTexture, Position, Color.White);
        } // Draw

        private void incrementCurrPlanet()
        {
            CurrentPlanet++;
            index++;
            if (CurrentPlanet > currentPlanet.Sun)
            {
                CurrentPlanet = currentPlanet.Mercury;
                index = 1;
            }

            CurrentTexture = Content.Load<Texture2D>("P_" + index);
            ChangeGrav();
        } // incrementCurrPlanet

        private void MoveOffScreen(bool NextPlanet)
        {
            if (!NextPlanet)
            {
                Position.Y += 50;
            }

            else
            {
                Position.Y -= 50;
            }
        } // MoveOffScreen

        public string GetPlanetName()
        {
            if (CurrentPlanet == currentPlanet.Moon)
            {
                return CurrentPlanet.ToString() + "(Earth)";
            }
            return CurrentPlanet.ToString();
        } // GetPlanetName

        public double GetCurrGrav()
        {
            return Curr_Grav;
        } // GetCurrGrav

        private void ChangeGrav()
        {
            switch (CurrentPlanet)
            {
                case currentPlanet.Mercury: Curr_Grav = MERC_GRAV;
                    break;

                case currentPlanet.Venus: Curr_Grav = VEN_GRAV;
                    break;

                case currentPlanet.Earth: Curr_Grav = EARTH_GRAV;
                    break;

                case currentPlanet.Moon: Curr_Grav = MOON_GRAV;
                    break;

                case currentPlanet.Mars: Curr_Grav = MARS_GRAV;
                    break;

                case currentPlanet.Jupiter: Curr_Grav = JUP_GRAV;
                    break;

                case currentPlanet.Saturn: Curr_Grav = SAT_GRAV;
                    break;

                case currentPlanet.Uranus: Curr_Grav = URAN_GRAV;
                    break;

                case currentPlanet.Neptune: Curr_Grav = NEP_GRAV;
                    break;

                case currentPlanet.Sun: Curr_Grav = SUN_GRAV;
                    break;

            }
        } // ChangeGrav

    } // Class Planets
} // namespace Pendulum_Clock

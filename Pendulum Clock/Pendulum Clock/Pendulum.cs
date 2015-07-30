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
    class Pendulum
    {
        const float WINDOWED_SCREEN_WIDTH = 1024;
        const float WINDOWED_SCREEN_HEIGHT = 768;
        const double MAX_ANGLE = Math.PI / 4;

        Texture2D pendulumTexture;
        float angle;
        float Elapsed;
        double Length;
        Vector2 position;
        Vector2 origin;

        public Pendulum(ContentManager content)
        {
            pendulumTexture = content.Load<Texture2D>("clock_pendulum");

            origin.X = 18;
            origin.Y = 4;
            angle = 0;

            Length = (1 / (Math.PI * Math.PI)) * 9.8;

            position.X = (WINDOWED_SCREEN_WIDTH / 2) ;
            position.Y = 105;
        }

        public void Update(double accel, GameTime gametime)
        {
            Elapsed += (float)gametime.ElapsedGameTime.TotalMilliseconds / 1000;
            angle = (float)MAX_ANGLE * ((float)Math.Sin((float)Math.Sqrt((float)accel / (float)Length) * (float)Elapsed));
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spritefont)
        {
            spriteBatch.Draw(pendulumTexture, position, null, Color.White, angle,
            origin, 1.0f, SpriteEffects.None, 0f);

            spriteBatch.DrawString(spritefont, "angle:" + Math.Round(angle, 3).ToString() + " rads", new Vector2(815, 40), Color.White);

        }
    }
}

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
    class SecondsHand
    {
        Texture2D SecondsTexture;

        int seconds;
        float angle;
        Vector2 origin;
        Vector2 screenpos;

        public SecondsHand(int start_time, Vector2 clockOrigin, ContentManager content)
        {
            SecondsTexture = content.Load<Texture2D>("SecondsHand");
            seconds = start_time;
            screenpos.X = clockOrigin.X;
            screenpos.Y = clockOrigin.Y;
            angle = (float)(seconds * Math.PI / 30);
            origin.Y = SecondsTexture.Height;
            origin.X = 0;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(SecondsTexture, screenpos, null, Color.White, angle,
            origin, 1.0f, SpriteEffects.None, 0f);
        }

        public void Increment()
        {
            seconds++;
            angle = (float)(seconds * (Math.PI / 30));

            if (seconds > 59)
            {
                angle = 0;
            }
        }

        public int GetSeconds()
        {
            return seconds;
        }

        public void ChangeSeconds(int newsec)
        {
            seconds = newsec;
        }
    }
}

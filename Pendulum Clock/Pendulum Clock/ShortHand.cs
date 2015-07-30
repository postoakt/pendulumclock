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
    class ShortHand
    {
        Texture2D HoursTexture;
        Vector2 screenpos;
        Vector2 origin;

        int hours;
        float angle;

        public ShortHand(int start_time, Vector2 clockOrigin, ContentManager content)
        {
            hours = start_time;
            HoursTexture = content.Load <Texture2D>("ShortHand");
            screenpos.X = clockOrigin.X;
            screenpos.Y = clockOrigin.Y;
            angle = (float)(hours * Math.PI / 6);
            origin.Y = 23;
            origin.X = HoursTexture.Width / 2;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(HoursTexture, screenpos, null, Color.White, angle,
            origin, 1.0f, SpriteEffects.None, 0f);
        }

        public void Increment()
        {
            hours++;
            angle += (float)Math.PI / 6;

            if (hours == 12)
            {
                angle = 0;
            }
        }

        public int GetHours()
        {
            return hours;
        }

        public void ChangeHours(int newHour)
        {
            hours = newHour;
        }
    }
}

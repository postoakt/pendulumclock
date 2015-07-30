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
    class LongHand
    {
        Texture2D MinutesTexture;
        Vector2 origin;
        Vector2 screenpos;

        int minutes;
        float angle;

        public LongHand(int start_time, Vector2 clockOrigin, ContentManager content)
        {
            MinutesTexture = content.Load<Texture2D>("LongHand");
            minutes = start_time;
            screenpos.X = clockOrigin.X;
            screenpos.Y = clockOrigin.Y;
            angle = (float)(minutes * Math.PI / 30);
            origin.Y = 38.5f;
            origin.X = MinutesTexture.Width / 2;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(MinutesTexture, screenpos, null, Color.White, angle,
            origin, 1.0f, SpriteEffects.None, 0f);
        }

        public void Increment()
        {
            minutes++;
            angle = (float)(minutes * (Math.PI / 30));
            if (minutes > 59)
            {
                angle = 0;
            }
        }

        public int GetMinutes()
        {
            return minutes;
        }

        public void ChangeMinutes(int newMinutes)
        {
            minutes = newMinutes;
        }
    }
}

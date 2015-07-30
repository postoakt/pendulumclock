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
    class Clock
    {
        const float WINDOWED_SCREEN_WIDTH = 1024;
        const float WINDOWED_SCREEN_HEIGHT = 768;

        SoundEffect clocktick;
        SoundEffect lowclocktick;
        SoundEffect lowestclocktick;

        string strTime;
        const double LENGTH = (1 / (Math.PI * Math.PI)) * 9.8;
        double Period;
        double Elapsed;
        LongHand longHand;
        ShortHand shortHand;
        SecondsHand secondsHand;
        Texture2D ClockFace;
        ContentManager content;
        Vector2 ClockPos;
        Vector2 ClockOrigin;
        string temphours, tempminutes, tempseconds;

        public Clock(int startHour, int startMinutes, int startSeconds, ContentManager Content)
        {
            content = Content;
            ClockFace = content.Load<Texture2D>("ClockFace");
            ClockOrigin.X = (WINDOWED_SCREEN_WIDTH / 2);
            ClockOrigin.Y = (ClockFace.Height / 2);

            clocktick = content.Load<SoundEffect>("clock-ticking");
            lowclocktick = content.Load<SoundEffect>("low_clock-ticking");
            lowestclocktick = content.Load<SoundEffect>("lowest_clock-ticking");

            shortHand = new ShortHand(startHour, ClockOrigin, content);
            longHand = new LongHand(startMinutes, ClockOrigin, content);
            secondsHand = new SecondsHand(startSeconds, ClockOrigin, Content);

            ClockPos.X = (WINDOWED_SCREEN_WIDTH / 2) - (ClockFace.Width / 2);
            ClockPos.Y = 0;
            Elapsed = 0;
        } //Constructor

        public void Update(double acceleration, GameTime gametime)
        {
            Period = ((Math.PI * 2) * Math.Sqrt(LENGTH / acceleration));

            Elapsed += gametime.ElapsedGameTime.TotalMilliseconds;

            HandleClockTicks(Elapsed);


            if (shortHand.GetHours() < 10)
            {
                temphours = "0" + shortHand.GetHours();
            }
            else
            {
                temphours = shortHand.GetHours().ToString();
            }

            if (longHand.GetMinutes() < 10)
            {
                tempminutes = "0" + longHand.GetMinutes();
            }
            else
            {
                tempminutes = longHand.GetMinutes().ToString();
            }

            if (secondsHand.GetSeconds() < 10)
            {
                tempseconds = "0" + secondsHand.GetSeconds();
            }
            else
            {
                tempseconds = secondsHand.GetSeconds().ToString();
            }

            strTime = "Time: " + temphours + ":"
                      + tempminutes + ":" +
                      tempseconds;

        } //Update

        public void Draw(SpriteBatch spriteBatch, SpriteFont spritefont)
        {
            spriteBatch.Draw(ClockFace, ClockPos, Color.White);
            spriteBatch.DrawString(spritefont, strTime, new Vector2(0, 40), Color.White);
            spriteBatch.DrawString(spritefont, "Period:" + Math.Round(Period, 3) + " s", new Vector2(815, 20), Color.White);
            spriteBatch.DrawString(spritefont, "Frequency:" + Math.Round(1 / Period, 3) + " hz", new Vector2(815, 0), Color.White);
            shortHand.Draw(spriteBatch);
            longHand.Draw(spriteBatch);
            secondsHand.Draw(spriteBatch);
        } // Draw

        private void HandleClockTicks(double elapsed)
        {
            if (elapsed >= (Period / 2) * (1000))
            {
                Elapsed = 0;

                secondsHand.Increment();

                clocktick.Play();

                if (secondsHand.GetSeconds() > 59)
                {
                    secondsHand.ChangeSeconds(0);

                    longHand.Increment();
                    lowclocktick.Play();

                    if (longHand.GetMinutes() > 59)
                    {
                        longHand.ChangeMinutes(0);

                        shortHand.Increment();
                        lowestclocktick.Play();

                        if (shortHand.GetHours() > 12)
                        {
                            shortHand.ChangeHours(1);
                        }

                    }
                }
            }
        } // HandleClockTicks

        public double GetElapsed()
        {
            return Elapsed;
        } // GetElapsed

        public double GetPeriod()
        {
            return Period;
        }// GetPeriod

    } //Class Clock
} //Namespace PendulumClock

using System;
using System.Collections.Generic;
using System.Text;

namespace AirplaneSurvival.Source.Engine
{
    public struct CountdownTimer
    {
        public readonly float maxTime;
        public float Time { get; private set; }

        public CountdownTimer(float _maxTime, bool atBeginning = true)
        {
            maxTime = _maxTime;

            // Assigns the current time to the beginning or end depending on the parameter.
            if (atBeginning)
                Time = maxTime;
            else
                Time = 0;
        }

        public void Reset() => Time = maxTime;

        public void SetToEnd() => Time = 0;

        public bool Ended => Time == 0;
        
        public bool IfEndedReset()
        {
            /*
             * Returns whether the countdown
             * has ended and resets it if
             * it has.
             */

            bool ret = Ended;
            if (ret) Reset();
            return ret;
        }

        public void Decrement(float amount)
        {
            // Decreases the time by the amount, capping it at 0.

            Time -= amount;
            if (Time < 0) Time = 0;
        }
    }
}

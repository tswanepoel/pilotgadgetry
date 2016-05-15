// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Controllers
{
    using System;
    using Biscuits.Devices;

    public class SpeedController : ISpeedController
    {
        private readonly IPwmController _pwm;

        public SpeedController(IPwmController pwm)
        {
            _pwm = pwm;
        }

        public void SetSpeed(int number, double speed)
        {
            if (speed < 0d || speed > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(speed));
            }

            _pwm.SetValue(number, 1d + speed);
        }

        public void SetSpeed(double speed1)
        {
            if (speed1 < 0d || speed1 > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(speed1));
            }

            _pwm.SetValue(1d + speed1);
        }

        public void SetSpeed(double speed1, double speed2)
        {
            if (speed1 < 0d || speed1 > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(speed1));
            }

            if (speed2 < 0d || speed2 > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(speed2));
            }

            _pwm.SetValue(1d + speed1, 1d + speed2);
        }

        public void SetSpeed(double speed1, double speed2, double speed3)
        {
            if (speed1 < 0d || speed1 > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(speed1));
            }

            if (speed2 < 0d || speed2 > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(speed2));
            }

            if (speed3 < 0d || speed3 > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(speed3));
            }

            _pwm.SetValue(1d + speed1, 1d + speed2, 1d + speed3);
        }

        public void SetSpeed(double speed1, double speed2, double speed3, double speed4)
        {
            if (speed1 < 0d || speed1 > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(speed1));
            }

            if (speed2 < 0d || speed2 > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(speed2));
            }

            if (speed3 < 0d || speed3 > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(speed3));
            }

            if (speed4 < 0d || speed4 > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(speed4));
            }

            _pwm.SetValue(1d + speed1, 1d + speed2, 1d + speed3, 1d + speed4);
        }
    }
}

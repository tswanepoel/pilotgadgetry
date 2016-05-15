// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Controllers
{
    public interface ISpeedController
    {
        void SetSpeed(int number, double speed);
        void SetSpeed(double speed1);
        void SetSpeed(double speed1, double speed2);
        void SetSpeed(double speed1, double speed2, double speed3);
        void SetSpeed(double speed1, double speed2, double speed3, double speed4);
    }
}

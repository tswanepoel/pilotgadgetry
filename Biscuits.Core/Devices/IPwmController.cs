// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    public interface IPwmController
    {
        void SetValue(int number, double value);
        void SetValue(double value1);
        void SetValue(double value1, double value2);
        void SetValue(double value1, double value2, double value3);
        void SetValue(double value1, double value2, double value3, double value4);
    }
}

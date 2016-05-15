// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Flight
{
    public interface IAircraft
    {
        void SetControl(double thrust, double pitch, double roll, double yaw);
    }
}

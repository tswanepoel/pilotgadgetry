// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Nmea
{
    public enum NmeaReadState
    {
        None = 0,
        StartMessage = 1,
        Address = 2,
        Data = 3
    }
}

// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits
{
    using System;

    public class GeoCoordinate : IEquatable<GeoCoordinate>
    {
        private readonly double _latitude;
        private readonly double _longitude;
        private readonly double _altitude;

        public double Latitude
        {
            get { return _latitude; }
        }

        public double Longitude
        {
            get { return _longitude; }
        }
        
        public double Altitude
        {
            get { return _altitude; }
        }

        public GeoCoordinate(double latitude, double longitude, double altitude)
        {
            _latitude = latitude;
            _longitude = longitude;
            _altitude = altitude;
        }

        public override int GetHashCode()
        {
            return _latitude.GetHashCode() ^ _longitude.GetHashCode() ^ _altitude.GetHashCode();
        }

        public bool Equals(GeoCoordinate other)
        {
            return
                _latitude.Equals(other._latitude) &&
                _longitude.Equals(other._longitude) &&
                _altitude.Equals(other._altitude);
        }
    }
}

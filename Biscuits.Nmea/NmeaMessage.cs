// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Nmea
{
    using System;

    public class NmeaMessage
    {
        private readonly string _address;
        private readonly string _data;

        public string Address
        {
            get { return _address; }
        }

        public string Data
        {
            get { return _data; }
        }

        protected NmeaMessage(string address, string data)
        {
            _address = address;
            _data = data;
        }

        public static NmeaMessage Create(string address, string data)
        {
            switch (address)
            {
                case "GPRMC":
                    NmeaGpRmcMessage gpRmcMessage;
                    
                    if (NmeaGpRmcMessage.TryParse(data, out gpRmcMessage))
                    {
                        return gpRmcMessage;
                    }

                    throw new FormatException("Failed to parse GPRMC message data.");

                case "GPGGA":
                    NmeaGpGgaMessage gpGgaMessage;

                    if (NmeaGpGgaMessage.TryParse(data, out gpGgaMessage))
                    {
                        return gpGgaMessage;
                    }

                    throw new FormatException("Failed to parse GPGGA message data.");

                default:
                    return new NmeaMessage(address, data);
            }
        }
    }
}

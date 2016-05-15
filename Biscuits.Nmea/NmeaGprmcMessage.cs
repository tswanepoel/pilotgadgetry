// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Nmea
{
    using System;
    using System.Globalization;

    public class NmeaGpRmcMessage : NmeaMessage
    {
        private readonly DateTime _dateTimeUtc;
        private readonly bool _statusIsWarning;
        private readonly decimal? _latitude;
        private readonly decimal? _longitude;
        private readonly decimal? _speedOverGround;
        private readonly decimal? _courseOverGround;
        
        public DateTime DateTimeUtc
        {
            get { return _dateTimeUtc; }
        }

        public bool StatusIsWarning
        {
            get { return _statusIsWarning; }
        }

        public decimal? Latitude
        {
            get { return _latitude; }
        }

        public decimal? Longitude
        {
            get { return _longitude; }
        }

        public decimal? SpeedOverGround
        {
            get { return _speedOverGround; }
        }

        public decimal? CourseOverGround
        {
            get { return _courseOverGround; }
        }

        private NmeaGpRmcMessage(string data, DateTime dateTimeUtc, bool statusIsWarning, decimal? latitude, decimal? longitude, decimal? speedOverGround, decimal? courseOverGround)
            : base("GPRMC", data)
        {
            _dateTimeUtc = dateTimeUtc;
            _statusIsWarning = statusIsWarning;
            _latitude = latitude;
            _longitude = longitude;
            _speedOverGround = speedOverGround;
            _courseOverGround = courseOverGround;
        }

        public static bool TryParse(string data, out NmeaGpRmcMessage message)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            string[] values = data.Split(',');

            if (values.Length != 12)
            {
                message = default(NmeaGpRmcMessage);
                return false;
            }

            string timeUtcString = values[0];
            string statusString = values[1];
            string latitudeString = values[2];
            string latitudeNS = values[3];
            string longitudeString = values[4];
            string longitudeEW = values[5];
            string speedOverGroundString = values[6];
            string courseOverGroundString = values[7];
            string dateUtcString = values[8];

            DateTime dateTimeUtc;
            bool statusIsWarning;
            decimal? latitude = null;
            decimal? longitude = null;
            decimal? speedOverGround = null;
            decimal? courseOverGround = null;

            if (!DateTime.TryParseExact(dateUtcString + timeUtcString, "ddMMyyHHmmss.ff", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dateTimeUtc))
            {
                message = default(NmeaGpRmcMessage);
                return false;
            }

            switch (statusString)
            {
                case "A":
                    statusIsWarning = false;
                    break;

                case "V":
                    statusIsWarning = true;
                    break;

                default:
                    message = default(NmeaGpRmcMessage);
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(latitudeString) &&
                !string.IsNullOrWhiteSpace(latitudeNS))
            {
                decimal latitudeValue;
                bool south;

                if (!decimal.TryParse(latitudeString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out latitudeValue))
                {
                    message = default(NmeaGpRmcMessage);
                    return false;
                }

                switch (latitudeNS)
                {
                    case "N":
                        south = false;
                        break;

                    case "S":
                        south = true;
                        break;

                    default:
                        message = default(NmeaGpRmcMessage);
                        return false;
                }

                decimal degrees = Math.Floor(latitudeValue / 100m);
                decimal minutes = latitudeValue % 100;
                latitude = (south ? -1m : 1m) * (degrees + minutes / 60m);
            }

            if (!string.IsNullOrWhiteSpace(longitudeString) &&
                !string.IsNullOrWhiteSpace(longitudeEW))
            {
                decimal longitudeValue;
                bool west;

                if (!decimal.TryParse(longitudeString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out longitudeValue))
                {
                    message = default(NmeaGpRmcMessage);
                    return false;
                }

                switch (longitudeEW)
                {
                    case "E":
                        west = false;
                        break;

                    case "W":
                        west = true;
                        break;

                    default:
                        message = default(NmeaGpRmcMessage);
                        return false;
                }

                decimal degrees = Math.Floor(longitudeValue / 100m);
                decimal minutes = longitudeValue % 100;
                longitude = (west ? -1m : 1m) * (degrees + minutes / 60m);
            }

            if (!string.IsNullOrWhiteSpace(speedOverGroundString))
            {
                decimal speedOverGroundValue;

                if (!decimal.TryParse(speedOverGroundString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out speedOverGroundValue))
                {
                    message = default(NmeaGpRmcMessage);
                    return false;
                }

                speedOverGround = speedOverGroundValue * 0.514444m;
            }

            if (!string.IsNullOrWhiteSpace(courseOverGroundString))
            {
                decimal courseOverGroundValue;

                if (!decimal.TryParse(courseOverGroundString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out courseOverGroundValue))
                {
                    message = default(NmeaGpRmcMessage);
                    return false;
                }

                courseOverGround = courseOverGroundValue;
            }

            message = new NmeaGpRmcMessage(data, dateTimeUtc, statusIsWarning, latitude, longitude, speedOverGround, courseOverGround);
            return true;
        }
    }
}

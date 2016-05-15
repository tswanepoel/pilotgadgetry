// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Nmea
{
    using System;
    using System.Globalization;

    public class NmeaGpGgaMessage : NmeaMessage
    {
        private readonly TimeSpan _timeUtc;
        private readonly decimal? _latitude;
        private readonly decimal? _longitude;
        private readonly bool _statusIsValid;
        private readonly int? _satellitesUsed;
        private readonly decimal? _horizontalDilutionOfPrecision;
        private readonly decimal? _altitude;
        private readonly decimal? _geoidSeparation;

        public TimeSpan TimeUtc
        {
            get { return _timeUtc; }
        }

        public decimal? Latitude
        {
            get { return _latitude; }
        }

        public decimal? Longitude
        {
            get { return _longitude; }
        }

        public bool StatusIsValid
        {
            get { return _statusIsValid; }
        }

        public int? SatellitesUsed
        {
            get { return _satellitesUsed; }
        }

        public decimal? HorizontalDilutionOfPrecision
        {
            get { return _horizontalDilutionOfPrecision; }
        }

        public decimal? Altitude
        {
            get { return _altitude; }
        }

        public decimal? GeoidSeparation
        {
            get { return _geoidSeparation; }
        }

        private NmeaGpGgaMessage(string data, TimeSpan timeUtc, bool statusIsValid, decimal? latitude, decimal? longitude, int? satelittesUsed, decimal? horizontalDilutionOfPrecision, decimal? altitude, decimal? geoidSeparation)
            : base("GPGGA", data)
        {
            _timeUtc = timeUtc;
            _statusIsValid = statusIsValid;
            _latitude = latitude;
            _longitude = longitude;
            _satellitesUsed = satelittesUsed;
            _horizontalDilutionOfPrecision = horizontalDilutionOfPrecision;
            _altitude = altitude;
            _geoidSeparation = geoidSeparation;
        }

        public static bool TryParse(string data, out NmeaGpGgaMessage message)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            string[] values = data.Split(',');

            if (values.Length != 14)
            {
                message = default(NmeaGpGgaMessage);
                return false;
            }

            string timeUtcString = values[0];
            string latitudeString = values[1];
            string latitudeNS = values[2];
            string longitudeString = values[3];
            string longitudeEW = values[4];
            string statusString = values[5];
            string satellitesUsedString = values[6];
            string horizontalDilutionOfPrecisionString = values[7];
            string altitudeString = values[8];
            string geoidSeparationString = values[10];

            DateTime dateTimeUtc;
            decimal? latitude = null;
            decimal? longitude = null;
            bool statusIsValid;
            int? satellitesUsed = null;
            decimal? horizontalDilutionOfPrecision = null;
            decimal? altitude = null;
            decimal? geoidSeparation = null;
            
            if (!DateTime.TryParseExact(timeUtcString, "HHmmss.ff", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dateTimeUtc))
            {
                message = default(NmeaGpGgaMessage);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(latitudeString) &&
                !string.IsNullOrWhiteSpace(latitudeNS))
            {
                decimal latitudeValue;
                bool south;

                if (!decimal.TryParse(latitudeString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out latitudeValue))
                {
                    message = default(NmeaGpGgaMessage);
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
                        message = default(NmeaGpGgaMessage);
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
                    message = default(NmeaGpGgaMessage);
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
                        message = default(NmeaGpGgaMessage);
                        return false;
                }

                decimal degrees = Math.Floor(longitudeValue / 100m);
                decimal minutes = longitudeValue % 100;
                longitude = (west ? -1m : 1m) * (degrees + minutes / 60m);
            }

            switch (statusString)
            {
                case "0":
                    statusIsValid = false;
                    break;

                case "1":
                    statusIsValid = true;
                    break;

                default:
                    message = default(NmeaGpGgaMessage);
                    return false;
            }

            if (!string.IsNullOrWhiteSpace(satellitesUsedString))
            {
                int satellitesUsedValue;

                if (!int.TryParse(satellitesUsedString, NumberStyles.Integer, CultureInfo.InvariantCulture, out satellitesUsedValue))
                {
                    message = default(NmeaGpGgaMessage);
                    return false;
                }

                satellitesUsed = satellitesUsedValue;
            }

            if (!string.IsNullOrWhiteSpace(horizontalDilutionOfPrecisionString))
            {
                decimal horizontalDilutionOfPrecisionValue;

                if (!decimal.TryParse(horizontalDilutionOfPrecisionString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out horizontalDilutionOfPrecisionValue))
                {
                    message = default(NmeaGpGgaMessage);
                    return false;
                }

                horizontalDilutionOfPrecision = horizontalDilutionOfPrecisionValue;
            }

            if (!string.IsNullOrWhiteSpace(altitudeString))
            {
                decimal altitudeValue;

                if (!decimal.TryParse(altitudeString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out altitudeValue))
                {
                    message = default(NmeaGpGgaMessage);
                    return false;
                }

                altitude = altitudeValue;
            }

            if (!string.IsNullOrWhiteSpace(geoidSeparationString))
            {
                decimal geoidSeparationValue;

                if (!decimal.TryParse(geoidSeparationString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out geoidSeparationValue))
                {
                    message = default(NmeaGpGgaMessage);
                    return false;
                }

                geoidSeparation = geoidSeparationValue;
            }

            message = new NmeaGpGgaMessage(data, dateTimeUtc.TimeOfDay, statusIsValid, latitude, longitude, satellitesUsed, horizontalDilutionOfPrecision, altitude, geoidSeparation);
            return true;
        }
    }
}

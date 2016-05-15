// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Nmea
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;

    public class NmeaReader : IDisposable
    {
        private readonly Stream _input;
        private NmeaReadState _state = NmeaReadState.None;
        private bool _disposed;

        public NmeaReadState State
        {
            get { return _state; }
        }

        public NmeaReader(Stream input)
        {
            _input = input;
        }

        public virtual NmeaMessage ReadMessage()
        {
            string address;
            string data;
            int checksum;
            ReadStartMessage();

            byte addressChecksum;
            address = ReadAddressCore(out addressChecksum);

            byte dataChecksum;
            data = ReadDataCore(out dataChecksum);
            
            ReadEndMessageCore(out checksum);

            if (checksum != (addressChecksum ^ 44/*comma*/ ^ dataChecksum))
            {
                throw new InvalidDataException("Cyclic redundancy check failed.");
            }

            return NmeaMessage.Create(address, data);
        }
        
        public virtual void ReadStartMessage()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(NmeaReader));
            }

            if (_state != NmeaReadState.None)
            {
                throw new InvalidOperationException();
            }

            int start = _input.ReadByte();

            while ((start = _input.ReadByte()) != 36/*$*/) { }
            _state = NmeaReadState.StartMessage;
        }

        public virtual string ReadAddress()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(NmeaReader));
            }

            byte checksum;
            return ReadAddressCore(out checksum);
        }

        private string ReadAddressCore(out byte checksum)
        {
            if (_state != NmeaReadState.StartMessage)
            {
                throw new InvalidOperationException();
            }

            var sb = new StringBuilder();
            int b;

            checksum = 0x0;

            while ((b = _input.ReadByte()) != 44/*comma*/)
            {
                sb.Append((char)b);
                checksum ^= (byte)b;
            }

            _state = NmeaReadState.Address;
            return sb.ToString();
        }

        public virtual string ReadData()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(NmeaReader));
            }
            
            byte checksum;
            return ReadDataCore(out checksum);
        }

        private string ReadDataCore(out byte checksum)
        {
            if (_state != NmeaReadState.Address)
            {
                throw new InvalidOperationException();
            }

            var sb = new StringBuilder();
            int b;

            checksum = 0x0;

            while ((b = _input.ReadByte()) != 0x2a/*asterisk*/)
            {
                sb.Append((char)b);
                checksum ^= (byte)b;
            }

            _state = NmeaReadState.Data;
            return sb.ToString();
        }

        public virtual void ReadEndNessage()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(NmeaReader));
            }

            int checksum;
            ReadEndMessageCore(out checksum);
        }

        private void ReadEndMessageCore(out int checksum)
        {
            if (_state != NmeaReadState.Data)
            {
                throw new InvalidOperationException();
            }

            var buffer = new byte[2];
            _input.Read(buffer, 0, 2);

            string checksumString = new string(new[] { (char)buffer[0], (char)buffer[1] });
            
            if (!int.TryParse(checksumString, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out checksum))
            {
                _state = NmeaReadState.None;
                throw new InvalidDataException();
            }

            int cr = _input.ReadByte();

            if (cr != 13/*CR*/)
            {
                _state = NmeaReadState.None;
                throw new InvalidDataException();
            }

            int LF = _input.ReadByte();

            if (LF != 10/*LF*/)
            {
                _state = NmeaReadState.None;
                throw new InvalidDataException();
            }

            _state = NmeaReadState.None;
        }
        
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _disposed = true;
            }
        }
    }
}

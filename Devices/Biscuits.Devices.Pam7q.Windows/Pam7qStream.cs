// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices.Pam7q
{
    using System;
    using System.IO;

    /// <summary>
    /// Provides a view of a sequence of bytes from PAM-7Q.
    /// </summary>
    public class Pam7qStream : Stream
    {
        private readonly Pam7q _pam7q;
        private readonly byte[] _buffer = new byte[2048/*2KB*/];
        private int _bufferLength = 0;
        private int _bufferPosition = 0;

        private Pam7qStream(Pam7q pam7q)
        {
            _pam7q = pam7q;
        }

        /// <summary>
        /// Gets the value indicating whether the stream supports reading.
        /// </summary>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the value indicating whether the stream supports seeking.
        /// </summary>
        public override bool CanSeek
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the value indicating whether the stream supports writing.
        /// </summary>
        public override bool CanWrite
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the length of the stream.
        /// </summary>
        public override long Length
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets the current position within the stream.
        /// </summary>
        public override long Position
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Clears all buffers for the stream and causes any buffer data to be written to the underlying device.
        /// </summary>
        public override void Flush()
        {
        }

        /// <summary>
        /// Reads a sequence of bytes from the stream and advances the current position within the stream by the number of bytes read.
        /// </summary>
        /// <param name="buffer">
        /// An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset
        /// and (offset + count - 1) replaced by the bytes read from the current source.
        /// </param>
        /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read.</param>
        /// <param name="count"></param>
        /// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested.</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (offset < 0 || offset >= count)
            {
                throw new ArgumentOutOfRangeException(nameof(offset));
            }

            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            int bufferRemaining = _bufferLength - _bufferPosition;

            if (bufferRemaining == 0)
            {
                bufferRemaining =
                    _bufferLength = _pam7q.Read(_buffer, 0, _buffer.Length);

                _bufferPosition = 0;
            }

            int length = Math.Min(bufferRemaining, count);
                
            Array.Copy(_buffer, _bufferPosition, buffer, offset, count);
            _bufferPosition += length;

            return length;
        }

        /// <summary>
        /// Sets the position within the stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the origin parameter.</param>
        /// <param name="origin">A value indicating the reference point used to obtain the new position.</param>
        /// <returns>The new position within the stream.</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Sets the length of the stream.
        /// </summary>
        /// <param name="value">The desired length of the stream in bytes.</param>
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Writes a sequence of bytes to the stream and advances the current position within the stream by the number of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies count bytes from the buffer to the stream.</param>
        /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the stream.</param>
        /// <param name="count">The number of bytes to be written to the stream.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        internal static Pam7qStream Create(Pam7q pam7q)
        {
            return new Pam7qStream(pam7q);
        }
    }
}

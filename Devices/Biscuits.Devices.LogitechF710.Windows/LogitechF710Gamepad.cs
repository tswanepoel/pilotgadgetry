// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;

    /// <summary>
    /// Represents the Logitech F710 gamepad.
    /// </summary>
    public class LogitechF710Gamepad : IGamepad, IDisposable
    {
        private static Lazy<LogitechF710Gamepad> _default = new Lazy<LogitechF710Gamepad>(CreateDefault, true);

        /// <summary>
        /// Occurs when a button is pressed.
        /// </summary>
        public event EventHandler<GamepadButtonEventArgs> ButtonPressed;

        /// <summary>
        /// Occurs when a thumbstick is changed.
        /// </summary>
        public event EventHandler<GamepadThumbstickEventArgs> ThumbstickChanged;
        
        private readonly LogitechF710 _logitechF710;

        /// <summary>
        /// Gets the default instance.
        /// </summary>
        public static LogitechF710Gamepad Default
        {
            get { return _default.Value; }
        }

        /// <summary>
        /// Initializes the gamepad.
        /// </summary>
        public LogitechF710Gamepad()
            : this(new LogitechF710())
        {
        }

        /// <summary>
        /// Initializes the gamepad.
        /// </summary>
        /// <param name="repeatDelayMilliseconds">The duration of time before presses repeat while a button is held down, in milliseconds.</param>
        /// <param name="repeatRateMilliseconds">The duration of time between presses as they repeat while a button is held down, in milliseconds.</param>
        public LogitechF710Gamepad(int repeatDelayMilliseconds, int repeatRateMilliseconds)
            : this(new LogitechF710(repeatDelayMilliseconds, repeatRateMilliseconds))
        {
        }

        private LogitechF710Gamepad(LogitechF710 logitechF710)
        {
            _logitechF710 = logitechF710;
            _logitechF710.ButtonPressed += LogitechF710ButtonPressed;
            _logitechF710.ThumbstickChanged += LogitechF710ThumbstickChanged;
        }

        private static LogitechF710Gamepad CreateDefault()
        {
            return new LogitechF710Gamepad();
        }

        private void LogitechF710ThumbstickChanged(object sender, LogitechF710ThumbstickEventArgs e)
        {
            GamepadThumbstick thumbstick;

            switch (e.Thumbstick)
            {
                case LogitechF710Thumbstick.Left:
                    thumbstick = GamepadThumbstick.Left;
                    break;

                case LogitechF710Thumbstick.Right:
                    thumbstick = GamepadThumbstick.Right;
                    break;

                default:
                    throw new NotSupportedException();
            }

            double horizontal = Math.Min(Math.Max((e.Horizontal - 128L) / 127d, -1d), 1d);
            double vertical = Math.Min(Math.Max((e.Vertical - 128L) / 127d, -1d), 1d);

            ThumbstickChanged?.Invoke(this, new GamepadThumbstickEventArgs(thumbstick, horizontal, vertical));
        }

        private void LogitechF710ButtonPressed(object sender, LogitechF710ButtonEventArgs e)
        {
            GamepadButton button;

            switch (e.Button)
            {
                case LogitechF710Button.X:
                    button = GamepadButton.X;
                    break;

                case LogitechF710Button.A:
                    button = GamepadButton.A;
                    break;

                case LogitechF710Button.B:
                    button = GamepadButton.B;
                    break;

                case LogitechF710Button.Y:
                    button = GamepadButton.Y;
                    break;

                case LogitechF710Button.LeftBumber:
                    button = GamepadButton.LeftBumber;
                    break;

                case LogitechF710Button.RightBumber:
                    button = GamepadButton.RightBumber;
                    break;

                case LogitechF710Button.LeftTrigger:
                    button = GamepadButton.LeftTrigger;
                    break;

                case LogitechF710Button.RightTrigger:
                    button = GamepadButton.RightTrigger;
                    break;

                case LogitechF710Button.Back:
                    button = GamepadButton.Back;
                    break;

                case LogitechF710Button.Start:
                    button = GamepadButton.Start;
                    break;

                case LogitechF710Button.LeftThumbstick:
                    button = GamepadButton.LeftThumbstick;
                    break;

                case LogitechF710Button.RightThumbstick:
                    button = GamepadButton.RightThumbstick;
                    break;

                case LogitechF710Button.Up:
                    button = GamepadButton.Up;
                    break;

                case LogitechF710Button.Right:
                    button = GamepadButton.Right;
                    break;

                case LogitechF710Button.Down:
                    button = GamepadButton.Down;
                    break;

                case LogitechF710Button.Left:
                    button = GamepadButton.Left;
                    break;

                default:
                    throw new NotSupportedException();
            }

            ButtonPressed?.Invoke(this, new GamepadButtonEventArgs(button));
        }
        
        /// <summary>
        /// Releases resources used by the Logitech F710 gamepad.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases resources used by the Logitech F710 gamepad.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _logitechF710.ButtonPressed -= LogitechF710ButtonPressed;
                _logitechF710.ThumbstickChanged -= LogitechF710ThumbstickChanged;

                _logitechF710.Dispose();
            }    
        }
    }
}

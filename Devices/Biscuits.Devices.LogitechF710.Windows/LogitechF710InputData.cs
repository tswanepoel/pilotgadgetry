// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    /// <summary>
    /// Represents input data for Logitech F710.
    /// </summary>
    public class LogitechF710InputData
    {
        private readonly bool _isXButtonDown;
        private readonly bool _isAButtonDown;
        private readonly bool _isBButtonDown;
        private readonly bool _isYButtonDown;
        private readonly bool _isLeftBumperButtonDown;
        private readonly bool _isRightBumperButtonDown;
        private readonly bool _isLeftTriggerButtonDown;
        private readonly bool _isRightTriggerButtonDown;
        private readonly bool _isBackButtonDown;
        private readonly bool _isStartButtonDown;
        private readonly bool _isLeftThumbstickButtonDown;
        private readonly bool _isRightThumbstickButtonDown;
        private readonly long _leftThumbstickHorizontal;
        private readonly long _leftThumbstickVertical;
        private readonly long _rightThumbstickHorizontal;
        private readonly long _rightThumbstickVertical;
        private readonly bool _isUpButtonDown;
        private readonly bool _isRightButtonDown;
        private readonly bool _isDownButtonDown;
        private readonly bool _isLeftButtonDown;

        /// <summary>
        /// Gets the value indicating whether the X-button is pressed down.
        /// </summary>
        public bool IsXButtonDown
        {
            get { return _isXButtonDown; }
        }

        /// <summary>
        /// Gets the value indicating whether the A-button is pressed down.
        /// </summary>
        public bool IsAButtonDown
        {
            get { return _isAButtonDown; }
        }

        /// <summary>
        /// Gets the value indicating whether the B-button is pressed down.
        /// </summary>
        public bool IsBButtonDown
        {
            get { return _isBButtonDown; }
        }

        /// <summary>
        /// Gets the value indicating whether the Y-button is pressed down.
        /// </summary>
        public bool IsYButtonDown
        {
            get { return _isYButtonDown; }
        }

        /// <summary>
        /// Gets the value indicating whether the left bumper-button is pressed down.
        /// </summary>
        public bool IsLeftBumperButtonDown
        {
            get { return _isLeftBumperButtonDown; }
        }

        /// <summary>
        /// Gets the value indicating whether the right bumper-button is pressed down.
        /// </summary>
        public bool IsRightBumperButtonDown
        {
            get { return _isRightBumperButtonDown; }
        }

        /// <summary>
        /// Gets the value indicating whether the left trigger-button is pressed down.
        /// </summary>
        public bool IsLeftTriggerButtonDown
        {
            get { return _isLeftTriggerButtonDown; }
        }

        /// <summary>
        /// Gets the value indicating whether the right trigger-button is pressed down.
        /// </summary>
        public bool IsRightTriggerButtonDown
        {
            get { return _isRightTriggerButtonDown; }
        }

        /// <summary>
        /// Gets the value indicating whether the back-button is pressed down.
        /// </summary>
        public bool IsBackButtonDown
        {
            get { return _isBackButtonDown; }
        }

        /// <summary>
        /// Gets the value indicating whether the start-button is pressed down.
        /// </summary>
        public bool IsStartButtonDown
        {
            get { return _isStartButtonDown; }
        }

        /// <summary>
        /// Gets the value indicating whether the left thumbstick-button is pressed down.
        /// </summary>
        public bool IsLeftThumbstickButtonDown
        {
            get { return _isLeftThumbstickButtonDown; }
        }

        /// <summary>
        /// Gets the value indicating whether the right thumbstick-button is pressed down.
        /// </summary>
        public bool IsRightThumbstickButtonDown
        {
            get { return _isRightThumbstickButtonDown; }
        }

        /// <summary>
        /// Gets the position of the left thumbstick along the horizontal-axis.
        /// </summary>
        public long LeftThumbstickHorizontal
        {
            get { return _leftThumbstickHorizontal; }
        }

        /// <summary>
        /// Gets the position of the left thumbstick along the vertical-axis.
        /// </summary>
        public long LeftThumbstickVertical
        {
            get { return _leftThumbstickVertical; }
        }
        
        /// <summary>
        /// Gets the position of the right thumbstick along the horizontal-axis.
        /// </summary>
        public long RightThumbstickHorizontal
        {
            get { return _rightThumbstickHorizontal; }
        }

        /// <summary>
        /// Gets the position of the right thumbstick along the vertical-axis.
        /// </summary>
        public long RightThumbstickVertical
        {
            get { return _rightThumbstickVertical; }
        }

        /// <summary>
        /// Gets the value indicating whether the up-button is pressed down.
        /// </summary>
        public bool IsUpButtonDown
        {
            get { return _isUpButtonDown; }
        }
        
        /// <summary>
        /// Gets the value indicating whether the right-button is pressed down.
        /// </summary>
        public bool IsRightButtonDown
        {
            get { return _isRightButtonDown; }
        }

        /// <summary>
        /// Gets the value indicating whether the down-button is pressed down.
        /// </summary>
        public bool IsDownButtonDown
        {
            get { return _isDownButtonDown; }
        }

        /// <summary>
        /// Gets the value indicating whether the left-button is pressed down.
        /// </summary>
        public bool IsLeftButtonDown
        {
            get { return _isLeftButtonDown; }
        }

        /// <summary>
        /// Initializes the input data for Logitech F710.
        /// </summary>
        /// <param name="isXButtonDown">The value indicating whether the X-button is pressed down.</param>
        /// <param name="isAButtonDown">The value indicating whether the A-button is pressed down.</param>
        /// <param name="isBButtonDown">The value indicating whether the B-button is pressed down.</param>
        /// <param name="isYButtonDown">The value indicating whether the Y-button is pressed down.</param>
        /// <param name="isLeftBumperButtonDown">The value indicating whether the left bumber-button is pressed down.</param>
        /// <param name="isRightBumperButtonDown">The value indicating whether the right bumper-button is pressed down.</param>
        /// <param name="isLeftTriggerButtonDown">The value indicating whether the left trigger-button is pressed down.</param>
        /// <param name="isRightTriggerButtonDown">The value indicating whether the right trigger-button is pressed down.</param>
        /// <param name="isBackButtonDown">The value indicating whether the back-button is pressed down.</param>
        /// <param name="isStartButtonDown">The value indicating whether the start-button is pressed down.</param>
        /// <param name="isLeftThumbstickButtonDown">The value indicating whether the left thumbstick-button is pressed down.</param>
        /// <param name="isRightThumbstickButtonDown">The value indicating whether the right thumbstick-button is pressed down.</param>
        /// <param name="leftThumbstickHorizontal">The position of the left thumbstick along the horizontal-axis.</param>
        /// <param name="leftThumbstickVertical">The position of the left thumbstick along the vertical-axis.</param>
        /// <param name="rightThumbstickHorizontal">The position of the right thumbstick along the horizontal-axis.</param>
        /// <param name="rightThumbstickVertical">The position of the right thumbstick along the vertical-axis.</param>
        /// <param name="isUpButtonDown">The value indicating whether the up-button is pressed down.</param>
        /// <param name="isRightButtonDown">The value indicating whether the right-button is pressed down.</param>
        /// <param name="isDownButtonDown">The value indicating whether the down-button is pressed down.</param>
        /// <param name="IsLeftButtonDown">The value indicating whether the left-button is pressed down.</param>
        public LogitechF710InputData(
            bool isXButtonDown,
            bool isAButtonDown,
            bool isBButtonDown,
            bool isYButtonDown,
            bool isLeftBumperButtonDown,
            bool isRightBumperButtonDown,
            bool isLeftTriggerButtonDown,
            bool isRightTriggerButtonDown,
            bool isBackButtonDown,
            bool isStartButtonDown,
            bool isLeftThumbstickButtonDown,
            bool isRightThumbstickButtonDown,
            long leftThumbstickHorizontal,
            long leftThumbstickVertical,
            long rightThumbstickHorizontal,
            long rightThumbstickVertical,
            bool isUpButtonDown,
            bool isRightButtonDown,
            bool isDownButtonDown,
            bool IsLeftButtonDown)
        {
            _isXButtonDown = isXButtonDown;
            _isAButtonDown = isAButtonDown;
            _isBButtonDown = isBButtonDown;
            _isYButtonDown = isYButtonDown;
            _isLeftBumperButtonDown = isLeftBumperButtonDown;
            _isRightBumperButtonDown = isRightBumperButtonDown;
            _isLeftTriggerButtonDown = isLeftTriggerButtonDown;
            _isRightTriggerButtonDown = isRightTriggerButtonDown;
            _isBackButtonDown = isBackButtonDown;
            _isStartButtonDown = isStartButtonDown;
            _isLeftThumbstickButtonDown = isLeftThumbstickButtonDown;
            _isRightThumbstickButtonDown = isRightThumbstickButtonDown;
            _leftThumbstickHorizontal = leftThumbstickHorizontal;
            _leftThumbstickVertical = leftThumbstickVertical;
            _rightThumbstickHorizontal = rightThumbstickHorizontal;
            _rightThumbstickVertical = rightThumbstickVertical;
            _isUpButtonDown = isUpButtonDown;
            _isRightButtonDown = isRightButtonDown;
            _isDownButtonDown = isDownButtonDown;
            _isLeftButtonDown = IsLeftButtonDown;
        }
    }
}

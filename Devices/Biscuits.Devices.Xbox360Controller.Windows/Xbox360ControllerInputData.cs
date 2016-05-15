// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    public class Xbox360ControllerInputData
    {
        private readonly bool _isAButtonDown;
        private readonly bool _isBButtonDown;
        private readonly bool _isXButtonDown;
        private readonly bool _isYButtonDown;
        private readonly bool _isLeftBumperButtonDown;
        private readonly bool _isRightBumperButtonDown;
        private readonly bool _isBackButtonDown;
        private readonly bool _isStartButtonDown;
        private readonly bool _isLeftThumbstickButtonDown;
        private readonly bool _isRightThumbstickButtonDown;
        private readonly long _leftThumbstickHorizontal;
        private readonly long _leftThumbstickVertical;
        private readonly long _trigger;
        private readonly long _rightThumbstickHorizontal;
        private readonly long _rightThumbstickVertical;
        private readonly bool _isUpButtonDown;
        private readonly bool _isRightButtonDown;
        private readonly bool _isDownButtonDown;
        private readonly bool _isLeftButtonDown;

        public bool IsAButtonDown
        {
            get { return _isAButtonDown; }
        }

        public bool IsBButtonDown
        {
            get { return _isBButtonDown; }
        }

        public bool IsXButtonDown
        {
            get { return _isXButtonDown; }
        }

        public bool IsYButtonDown
        {
            get { return _isYButtonDown; }
        }

        public bool IsLeftBumperButtonDown
        {
            get { return _isLeftBumperButtonDown; }
        }

        public bool IsRightBumperButtonDown
        {
            get { return _isRightBumperButtonDown; }
        }

        public bool IsBackButtonDown
        {
            get { return _isBackButtonDown; }
        }

        public bool IsStartButtonDown
        {
            get { return _isStartButtonDown; }
        }

        public bool IsLeftThumbstickButtonDown
        {
            get { return _isLeftThumbstickButtonDown; }
        }

        public bool IsRightThumbstickButtonDown
        {
            get { return _isRightThumbstickButtonDown; }
        }

        public long LeftThumbstickHorizontal
        {
            get { return _leftThumbstickHorizontal; }
        }

        public long LeftThumbstickVertical
        {
            get { return _leftThumbstickVertical; }
        }

        public long Trigger
        {
            get { return _trigger; }
        }

        public long RightThumbstickHorizontal
        {
            get { return _rightThumbstickHorizontal; }
        }

        public long RightThumbstickVertical
        {
            get { return _rightThumbstickVertical; }
        }

        public bool IsUpButtonDown
        {
            get { return _isUpButtonDown; }
        }
        
        public bool IsRightButtonDown
        {
            get { return _isRightButtonDown; }
        }

        public bool IsDownButtonDown
        {
            get { return _isDownButtonDown; }
        }

        public bool IsLeftButtonDown
        {
            get { return _isLeftButtonDown; }
        }

        public Xbox360ControllerInputData(
            bool isAButtonDown,
            bool isBButtonDown,
            bool isXButtonDown,
            bool isYButtonDown,
            bool isLeftBumperButtonDown,
            bool isRightBumperButtonDown,
            bool isBackButtonDown,
            bool isStartButtonDown,
            bool isLeftThumbstickButtonDown,
            bool isRightThumbstickButtonDown,
            long leftThumbstickHorizontal,
            long leftThumbstickVertical,
            long trigger,
            long rightThumbstickHorizontal,
            long rightThumbstickVertical,
            bool isUpButtonDown,
            bool isRightButtonDown,
            bool isDownButtonDown,
            bool IsLeftButtonDown)
        {
            _isAButtonDown = isAButtonDown;
            _isBButtonDown = isBButtonDown;
            _isXButtonDown = isXButtonDown;
            _isYButtonDown = isYButtonDown;
            _isLeftBumperButtonDown = isLeftBumperButtonDown;
            _isRightBumperButtonDown = isRightBumperButtonDown;
            _isBackButtonDown = isBackButtonDown;
            _isStartButtonDown = isStartButtonDown;
            _isLeftThumbstickButtonDown = isLeftThumbstickButtonDown;
            _isRightThumbstickButtonDown = isRightThumbstickButtonDown;
            _leftThumbstickHorizontal = leftThumbstickHorizontal;
            _leftThumbstickVertical = leftThumbstickVertical;
            _trigger = trigger;
            _rightThumbstickHorizontal = rightThumbstickHorizontal;
            _rightThumbstickVertical = rightThumbstickVertical;
            _isUpButtonDown = isUpButtonDown;
            _isRightButtonDown = isRightButtonDown;
            _isDownButtonDown = isDownButtonDown;
            _isLeftButtonDown = IsLeftButtonDown;
        }
    }
}

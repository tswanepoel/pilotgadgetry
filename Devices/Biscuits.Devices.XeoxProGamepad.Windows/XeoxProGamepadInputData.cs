// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    public class XeoxProGamepadInputData
    {
        private readonly bool _isSelect;
        private readonly bool _isL3;
        private readonly bool _isR3;
        private readonly bool _isStart;
        private readonly bool _isUp;
        private readonly bool _isRight;
        private readonly bool _isDown;
        private readonly bool _isLeft;
        private readonly bool _isLT;
        private readonly bool _isRT;
        private readonly bool _isLB;
        private readonly bool _isRB;
        private readonly bool _isTriangle;
        private readonly bool _isCircle;
        private readonly bool _isCross;
        private readonly bool _isSquare;
        private readonly bool _isHome;
        private readonly long _lX;
        private readonly long _lY;
        private readonly long _rX;
        private readonly long _rY;
        
        public bool IsSelect
        {
            get { return _isSelect; }
        }

        public bool IsL3
        {
            get { return _isL3; }
        }

        public bool IsR3
        {
            get { return _isR3; }
        }

        public bool IsStart
        {
            get { return _isStart; }
        }

        public bool IsUp
        {
            get { return _isUp; }
        }

        public bool IsRight
        {
            get { return _isRight; }
        }

        public bool IsDown
        {
            get { return _isDown; }
        }

        public bool IsLeft
        {
            get { return _isLeft; }
        }

        public bool IsLT
        {
            get { return _isLT; }
        }

        public bool IsRT
        {
            get { return _isRT; }
        }

        public bool IsLB
        {
            get { return _isLB; }
        }

        public bool IsRB
        {
            get { return _isRB; }
        }

        public bool IsTriangle
        {
            get { return _isTriangle; }
        }

        public bool IsCircle
        {
            get { return _isCircle; }
        }

        public bool IsCross
        {
            get { return _isCross; }
        }

        public bool IsSquare
        {
            get { return _isSquare; }
        }

        public bool IsHome
        {
            get { return _isHome; }
        }

        public long LX
        {
            get { return _lX; }
        }

        public long LY
        {
            get { return _lY; }
        }

        public long RX
        {
            get { return _rX; }
        }

        public long RY
        {
            get { return _rY; }
        }

        public XeoxProGamepadInputData(
            bool isSelect,
            bool isL3,
            bool isR3,
            bool isStart,
            bool isUp,
            bool isRight,
            bool isDown,
            bool isLeft,
            bool isLT,
            bool isRT,
            bool isLB,
            bool isRB,
            bool isTriangle,
            bool isCircle,
            bool isCross,
            bool isSquare,
            bool isHome,
            long lX,
            long lY,
            long rX,
            long rY)
        {
            _isSelect = isSelect;
            _isL3 = isL3;
            _isR3 = isR3;
            _isStart = isStart;
            _isUp = isUp;
            _isRight = isRight;
            _isDown = isDown;
            _isLeft = isLeft;
            _isLT = isLT;
            _isRT = isRT;
            _isLB = isLB;
            _isRB = isRB;
            _isTriangle = isTriangle;
            _isCircle = isCircle;
            _isCross = isCross;
            _isSquare = isSquare;
            _isHome = isHome;
            _lX = lX;
            _lY = lY;
            _rX = rX;
            _rY = rY;
        }
    }
}

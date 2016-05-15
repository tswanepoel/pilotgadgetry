// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("W={W}, X={X}, Y={Y}, Z={Z}, Magnitude={Magnitude}")]
    public struct Quaternion
    {
        private readonly double _w;
        private readonly double _x;
        private readonly double _y;
        private readonly double _z;
        private readonly double _magnitude;

        public double W
        {
            get { return _w; }
        }

        public double X
        {
            get { return _x; }
        }

        public double Y
        {
            get { return _y; }
        }

        public double Z
        {
            get { return _z; }
        }

        public double Magnitude
        {
            get { return _magnitude; }
        }

        public Quaternion(double w, Vector3 vector)
            : this(w, vector.X, vector.Y, vector.Z)
        {
        }

        public Quaternion(double w, double x, double y, double z)
        {
            _w = w;
            _x = x;
            _y = y;
            _z = z;
            _magnitude = Math.Sqrt(_w * _w + _x * _x + _y * _y + _z * _z);
        }

        public Quaternion Normalize()
        {
            if (_magnitude == 0d)
            {
                return new Quaternion(_w, _x, _y, _z);
            }

            return new Quaternion(
                w: _w / _magnitude,
                x: _x / _magnitude,
                y: _y / _magnitude,
                z: _z / _magnitude);
        }

        public static Quaternion FromAxisAngle(Vector3 w, double theta)
        {
            double cosThetaOverTwo = Math.Cos(theta / 2d);
            double sinThetaOverTwo = Math.Sin(theta / 2d);

            return new Quaternion(
                w: cosThetaOverTwo,
                x: sinThetaOverTwo * w.X,
                y: sinThetaOverTwo * w.Y,
                z: sinThetaOverTwo * w.Z);
        }

        public static Quaternion FromEulerAngles(EulerAngles angles)
        {
            double cosRoll = Math.Cos(angles.Roll / 2d);
            double sinRoll = Math.Sin(angles.Roll / 2d);
            double cosPitch = Math.Cos(angles.Pitch / 2d);
            double sinPitch = Math.Sin(angles.Pitch / 2d);
            double cosYaw = Math.Cos(angles.Yaw / 2d);
            double sinYaw = Math.Sin(angles.Yaw / 2d);

            return new Quaternion(
                w: cosRoll * cosPitch * cosYaw + sinRoll * sinPitch * sinYaw,
                x: sinRoll * cosPitch * cosYaw - cosRoll * sinPitch * sinYaw,
                y: cosRoll * sinPitch * cosYaw + sinRoll * cosPitch * sinYaw,
                z: cosRoll * cosPitch * sinYaw - sinRoll * sinPitch * cosYaw);
        }

        public static Quaternion operator +(Quaternion left, Quaternion right)
        {
            return new Quaternion(
                w: left._w + right._w,
                x: left._x + right._x,
                y: left._y + right._y,
                z: left._z + right._z);
        }

        public static Quaternion operator -(Quaternion left, Quaternion right)
        {
            return new Quaternion(
                w: left._w - right._w,
                x: left._x - right._x,
                y: left._y - right._y,
                z: left._z - right._z);
        }

        public static Quaternion operator *(Quaternion self, double value)
        {
            return new Quaternion(
                w: self._w * value,
                x: self._x * value,
                y: self._y * value,
                z: self._z * value);
        }

        public static Quaternion operator /(Quaternion self, double value)
        {
            return new Quaternion(
                w: self._w / value,
                x: self._x / value,
                y: self._y / value,
                z: self._z / value);
        }

        public Quaternion Conjugate()
        {
            return new Quaternion(
                w: _w,
                x: -_x,
                y: -_y,
                z: -_z);
        }

        public Quaternion Multiply(Quaternion value)
        {
            return new Quaternion(
                w: _w * value._w - _x * value._x - _y * value._y - _z * value._z,
                x: _w * value._x + _x * value._w + _y * value._z - _z * value._y,
                y: _w * value._y - _x * value._z + _y * value._w + _z * value._x,
                z: _w * value._z + _x * value._y - _y * value._x + _z * value._w);
        }

        public double DotProduct(Quaternion value)
        {
            return _w * value._w + _x * value._x + _y * value._y + _z * value._z;
        }

        public Quaternion Scale(double scale)
        {
            return new Quaternion(
                w: _w / _magnitude * scale,
                x: _x / _magnitude * scale,
                y: _y / _magnitude * scale,
                z: _z / _magnitude * scale);
        }

        public Quaternion Slerp(Quaternion end, double t)
        {
            Quaternion endAcute = end;
            double dot = DotProduct(end);

            if (dot < 0d)
            {
                endAcute = new Quaternion(
                    w: -end._w,
                    x: -end._x,
                    y: -end._y,
                    z: -end._z);
            }

            double theta = Math.Acos(dot);
            double slerpPitch = Math.Sin((1d - t) * theta);
            Quaternion q1 = this * slerpPitch;

            double slerpRoll = Math.Sin(t * theta);
            Quaternion q2 = endAcute * slerpRoll;

            Quaternion qt = q1 + q2;
            return qt * 1d / Math.Sin(theta);
        }

        public Quaternion SlerpAngle(Quaternion end, double omega)
        {
            Quaternion endAcute = end;
            double dot = DotProduct(end);

            if (dot < 0d)
            {
                endAcute = new Quaternion(
                    w: -end._w,
                    x: -end._x,
                    y: -end._y,
                    z: -end._z);
            }
            else if (dot > 1d)
            {
                dot = 1d;
            }

            double theta = Math.Acos(dot);

            if (theta == 0d)
            {
                return end;
            }

            double t = omega / theta;
            if (t > 1d) t = 1d;

            double slerpPitch = Math.Sin((1d - t) * theta);
            Quaternion q1 = this * slerpPitch;

            double slerpRoll = Math.Sin(t * theta);
            Quaternion q2 = endAcute * slerpRoll;

            Quaternion qt = q1 + q2;
            return qt * 1d / Math.Sin(theta);
        }

        public Vector3 GetAxis()
        {
            double scale = 1d / Math.Sqrt(1d - _w * _w);

            return new Vector3(
                x: _x * scale,
                y: _y * scale,
                z: _z * scale);
        }

        public double GetAngle()
        {
            return 2d * Math.Acos(_w);
        }

        public EulerAngles ToEulerAngles()
        {
            double sw = _w * _w;
            double sx = _x * _x;
            double sy = _y * _y;
            double sz = _z * _z;

            return new EulerAngles(
                pitch: Math.Asin(-2d * (_x * _z - _y * _w) / (sx + sy + sz + sw)),
                roll: Math.Atan2(2d * (_y * _z + _x * _w), -sx - sy + sz + sw),
                yaw: Math.Atan2(2d * (_x * _y + _z * _w), sx - sy - sz + sw));
        }
    }
}

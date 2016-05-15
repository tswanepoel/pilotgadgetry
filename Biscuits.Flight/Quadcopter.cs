// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Flight
{
    using Controllers;
    using System;

    public class Quadcopter : IAircraft
    {
        private readonly ISpeedController _controller;

        public Quadcopter(ISpeedController controller)
        {
            controller = _controller;
        }

        public void SetControl(double thrust, double pitch, double roll, double yaw)
        {
            if (thrust < 0d || thrust > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(thrust));
            }

            if (pitch < -1d || pitch > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(pitch));
            }

            if (roll < -1d || roll > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(roll));
            }

            if (yaw < -1d || yaw > 1d)
            {
                throw new ArgumentOutOfRangeException(nameof(yaw));
            }

            double effectivePitch = pitch;
            double effectiveRoll = roll;

            double effectiveThrust = thrust *
                (1d / (1d - Math.Abs(pitch) / 2d)) *
                (1d / (1d - Math.Abs(roll) / 2d)) *
                (1d / (1d - Math.Abs(yaw) / 2d));

            double effectiveYaw;

            if (effectiveThrust <= 1d)
            {
                effectiveYaw = yaw;

                SetControlCore(effectiveThrust, effectivePitch, effectiveRoll, effectiveYaw);
                return;
            }

            effectiveThrust = 1d;

            double requestedThrustForYaw = thrust * (1d / (1d - Math.Abs(yaw) / 2d)) - thrust;
            double availableThrustForYaw = 1d - (thrust * (1d / (1d - Math.Abs(pitch) / 2d)) * (1d / (1d - Math.Abs(roll) / 2d)));
            effectiveYaw = availableThrustForYaw > 0d ? yaw * availableThrustForYaw / requestedThrustForYaw : 0d;

            SetControlCore(effectiveThrust, effectivePitch, effectiveRoll, effectiveYaw);
        }

        private void SetControlCore(double thrust, double pitch, double roll, double yaw)
        {
            double frontLeftSpeed = thrust *
                (pitch > 0d ? 1d : 1d - -pitch) *
                (roll > 0d ? 1d : 1d - -roll) *
                (yaw > 0d ? 1d : 1d - -yaw);

            double frontRightSpeed = thrust *
                (pitch > 0d ? 1d : 1d - -pitch) *
                (roll < 0d ? 1d : 1d - roll) *
                (yaw < 0d ? 1d : 1d - yaw);

            double rearRightSpeed = thrust *
                (pitch < 0d ? 1d : 1d - pitch) *
                (roll < 0d ? 1d : 1d - roll) *
                (yaw > 0d ? 1d : 1d - -yaw);

            double rearLeftSpeed = thrust *
                (pitch < 0d ? 1d : 1d - pitch) *
                (roll > 0d ? 1d : 1d - -roll) *
                (yaw < 0d ? 1d : 1d - yaw);

            _controller.SetSpeed(frontLeftSpeed, frontRightSpeed, rearRightSpeed, rearLeftSpeed);
        }
    }
}

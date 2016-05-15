// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Controllers
{
    public class AngularPositionPidController
    {
        private readonly PidController _controllerX;
        private readonly PidController _controllerY;
        private readonly PidController _controllerZ;

        public AngularPositionPidController(double proportionalGain, double integralGain, double derivativeGain)
        {
            _controllerX = new PidController(proportionalGain, integralGain, derivativeGain);
            _controllerY = new PidController(proportionalGain, integralGain, derivativeGain);
            _controllerZ = new PidController(proportionalGain, integralGain, derivativeGain);
        }

        public AngularPositionPidController(double proportionalGain, double integralGain, double derivativeGain, double maxErrorCumulative)
        {
            _controllerX = new PidController(proportionalGain, integralGain, derivativeGain, maxErrorCumulative);
            _controllerY = new PidController(proportionalGain, integralGain, derivativeGain, maxErrorCumulative);
            _controllerZ = new PidController(proportionalGain, integralGain, derivativeGain, maxErrorCumulative);
        }

        public AngularPositionPidController(
            double proportionalGainPitch, double integralGainPitch, double derivativeGainPitch,
            double proportionalGainRoll, double integralGainRoll, double derivativeGainRoll,
            double proportionalGainYaw, double integralGainYaw, double derivativeGainYaw)
        {
            _controllerX = new PidController(proportionalGainPitch, integralGainPitch, derivativeGainPitch);
            _controllerY = new PidController(proportionalGainRoll, integralGainRoll, derivativeGainRoll);
            _controllerZ = new PidController(proportionalGainYaw, integralGainYaw, derivativeGainYaw);
        }

        public AngularPositionPidController(
            double proportionalGainPitch, double integralGainPitch, double derivativeGainPitch, double maxErrorCumulativeX,
            double proportionalGainRoll, double integralGainRoll, double derivativeGainRoll, double maxErrorCumulativeY,
            double proportionalGainYaw, double integralGainYaw, double derivativeGainYaw, double maxErrorCumulativeZ)
        {
            _controllerX = new PidController(proportionalGainPitch, integralGainPitch, derivativeGainPitch, maxErrorCumulativeX);
            _controllerY = new PidController(proportionalGainRoll, integralGainRoll, derivativeGainRoll, maxErrorCumulativeY);
            _controllerZ = new PidController(proportionalGainYaw, integralGainYaw, derivativeGainYaw, maxErrorCumulativeZ);
        }

        public EulerAngles Manipulate(EulerAngles processVariable, EulerAngles setpoint, double elapsedTime)
        {
            double manipulatedVariablePitch = _controllerX.Manipulate(processVariable.Pitch, setpoint.Pitch, elapsedTime);
            double manipulatedVariableRoll = _controllerX.Manipulate(processVariable.Roll, setpoint.Roll, elapsedTime);
            double manipulatedVariableYaw = _controllerX.Manipulate(processVariable.Yaw, setpoint.Yaw, elapsedTime);

            manipulatedVariablePitch += 360;
            manipulatedVariableRoll += 360;
            manipulatedVariableYaw += 360;

            if (manipulatedVariablePitch > 180d)
            {
                manipulatedVariablePitch -= 360d;
            }

            if (manipulatedVariableRoll > 180d)
            {
                manipulatedVariableRoll -= 360d;
            }

            if (manipulatedVariableYaw > 180d)
            {
                manipulatedVariableYaw -= 360d;
            }

            return new EulerAngles(manipulatedVariablePitch, manipulatedVariableRoll, manipulatedVariableYaw);
        }
    }
}

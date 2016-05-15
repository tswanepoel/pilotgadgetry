// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Controllers
{
    public class AngularVelocityPidController
    {
        private readonly PidController _controllerX;
        private readonly PidController _controllerY;
        private readonly PidController _controllerZ;

        public AngularVelocityPidController(double proportionalGain, double integralGain, double derivativeGain)
        {
            _controllerX = new PidController(proportionalGain, integralGain, derivativeGain);
            _controllerY = new PidController(proportionalGain, integralGain, derivativeGain);
            _controllerZ = new PidController(proportionalGain, integralGain, derivativeGain);
        }

        public AngularVelocityPidController(double proportionalGain, double integralGain, double derivativeGain, double maxErrorCumulative)
        {
            _controllerX = new PidController(proportionalGain, integralGain, derivativeGain, maxErrorCumulative);
            _controllerY = new PidController(proportionalGain, integralGain, derivativeGain, maxErrorCumulative);
            _controllerZ = new PidController(proportionalGain, integralGain, derivativeGain, maxErrorCumulative);
        }

        public AngularVelocityPidController(
            double proportionalGainX, double integralGainX, double derivativeGainX,
            double proportionalGainY, double integralGainY, double derivativeGainY,
            double proportionalGainZ, double integralGainZ, double derivativeGainZ)
        {
            _controllerX = new PidController(proportionalGainX, integralGainX, derivativeGainX);
            _controllerY = new PidController(proportionalGainY, integralGainY, derivativeGainY);
            _controllerZ = new PidController(proportionalGainZ, integralGainZ, derivativeGainZ);
        }

        public AngularVelocityPidController(
            double proportionalGainX, double integralGainX, double derivativeGainX, double maxErrorCumulativeX,
            double proportionalGainY, double integralGainY, double derivativeGainY, double maxErrorCumulativeY,
            double proportionalGainZ, double integralGainZ, double derivativeGainZ, double maxErrorCumulativeZ)
        {
            _controllerX = new PidController(proportionalGainX, integralGainX, derivativeGainX, maxErrorCumulativeX);
            _controllerY = new PidController(proportionalGainY, integralGainY, derivativeGainY, maxErrorCumulativeY);
            _controllerZ = new PidController(proportionalGainZ, integralGainZ, derivativeGainZ, maxErrorCumulativeZ);
        }

        public AngularVelocity3 Manipulate(AngularVelocity3 processVariable, AngularVelocity3 setpoint, double elapsedTime)
        {
            double manipulatedVariableX = _controllerX.Manipulate(processVariable.X, setpoint.X, elapsedTime);
            double manipulatedVariableY = _controllerX.Manipulate(processVariable.Y, setpoint.Y, elapsedTime);
            double manipulatedVariableZ = _controllerX.Manipulate(processVariable.Z, setpoint.Z, elapsedTime);
            
            return new AngularVelocity3(manipulatedVariableX, manipulatedVariableY, manipulatedVariableZ);
        }
    }
}

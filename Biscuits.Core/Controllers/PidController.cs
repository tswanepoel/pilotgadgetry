// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Controllers
{
    using System;

    /// <summary>
    /// Represents a proportional-integral-derivative (PID) controller.
    /// </summary>
    public class PidController
    {
        private readonly double _proportionalGain;
        private readonly double _integralGain;
        private readonly double _derivativeGain;
        private readonly double _maxErrorCumulative;
        private double _previousErrorCumulative = 0d;
        private double _previousError = 0d;

        /// <summary>
        /// Initializes the PID proportional-integral-derivative (PID) controller.
        /// </summary>
        /// <param name="proportionalGain">The proportional value (P) that depends on the present error.</param>
        /// <param name="integralGain">The integral value (I) that depends on the accumulation of previous errors.</param>
        /// <param name="derivativeGain">The derivative value (D) that depends on the prediction of future errors based on the current rate of change.</param>
        public PidController(double proportionalGain, double integralGain, double derivativeGain)
            : this(proportionalGain, integralGain, derivativeGain, double.MaxValue)
        {
        }

        /// <summary>
        /// Initializes the PID proportional-integral-derivative (PID) controller.
        /// </summary>
        /// <param name="proportionalGain">The proportional value (P) that depends on the present error.</param>
        /// <param name="integralGain">The integral value (I) that depends on the accumulation of previous errors.</param>
        /// <param name="derivativeGain">The derivative value (D) that depends on the prediction of future errors based on the current rate of change.</param>
        /// <param name="maxErrorCumulative">The maximum accumulation of previous errors.</param>
        public PidController(double proportionalGain, double integralGain, double derivativeGain, double maxErrorCumulative)
        {
            _proportionalGain = proportionalGain;
            _integralGain = integralGain;
            _derivativeGain = derivativeGain;
            _maxErrorCumulative = maxErrorCumulative;
        }

        /// <summary>
        /// Returns a manipulated variable (MV) for a measured process variable (PV) and desired setpoint (SP).
        /// </summary>
        /// <param name="processVariable">The measured process variable (PV).</param>
        /// <param name="setpoint">The desired setpoint (SP).</param>
        /// <param name="elapsedTime">The period of time since the previous measurement.</param>
        /// <returns>The manipulated variable (MV).</returns>
        public double Manipulate(double processVariable, double setpoint, double elapsedTime)
        {
            // proportional
            double error = setpoint - processVariable;
            double outputProportionalGain = _proportionalGain * error;

            // integral
            _previousErrorCumulative = Math.Min(Math.Max(_previousErrorCumulative + error * elapsedTime, -_maxErrorCumulative), _maxErrorCumulative);
            double outputIntegralGain = _integralGain * _previousErrorCumulative;

            // derivative
            double futureError = (error - _previousError) / elapsedTime;
            double outputDerivativeGain = _derivativeGain * futureError;
            _previousError = error;

            return outputProportionalGain + outputIntegralGain + outputDerivativeGain;
        }
    }
}

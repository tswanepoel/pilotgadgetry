// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Extensions
{
    using Controllers;
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    
    public static class EulerAngleExtensions
    {
        public static IObservable<EulerAngles> Manipulate(
            this IObservable<EulerAngles> processVariable, IObservable<EulerAngles> setpoint, 
            double proportionalGain, double integralGain, double derivativeGain)
        {
            return Manipulate(
                processVariable,
                setpoint,
                proportionalGain, integralGain, derivativeGain,
                proportionalGain, integralGain, derivativeGain,
                proportionalGain, integralGain, derivativeGain);
        }

        public static IObservable<EulerAngles> Manipulate(
            this IObservable<EulerAngles> processVariable, IObservable<EulerAngles> setpoint, 
            double proportionalGain, double integralGain, double derivativeGain, double maxErrorCumulative)
        {
            return Manipulate(
                processVariable,
                setpoint,
                proportionalGain, integralGain, derivativeGain, maxErrorCumulative,
                proportionalGain, integralGain, derivativeGain, maxErrorCumulative,
                proportionalGain, integralGain, derivativeGain, maxErrorCumulative);
        }

        public static IObservable<EulerAngles> Manipulate(
            this IObservable<EulerAngles> processVariable, IObservable<EulerAngles> setpoint, 
            double proportionalGainPitch, double integralGainPitch, double derivativeGainPitch,
            double proportionalGainRoll, double integralGainRoll, double derivativeGainRoll,
            double proportionalGainYaw, double integralGainYaw, double derivativeGainYaw)
        {
            var controllerPitch = new PidController(proportionalGainPitch, integralGainPitch, derivativeGainPitch);
            var controllerRoll = new PidController(proportionalGainRoll, integralGainRoll, derivativeGainRoll);
            var controllerYaw = new PidController(proportionalGainYaw, integralGainYaw, derivativeGainYaw);

            return Manipulate(processVariable, setpoint, controllerPitch, controllerRoll, controllerYaw);
        }

        public static IObservable<EulerAngles> Manipulate(
            this IObservable<EulerAngles> processVariable, IObservable<EulerAngles> setpoint,
            double proportionalGainPitch, double integralGainPitch, double derivativeGainPitch, double maxErrorCumulativeX,
            double proportionalGainRoll, double integralGainRoll, double derivativeGainRoll, double maxErrorCumulativeY,
            double proportionalGainYaw, double integralGainYaw, double derivativeGainYaw, double maxErrorCumulativeZ)
        {
            var controllerPitch = new PidController(proportionalGainPitch, integralGainPitch, derivativeGainPitch, maxErrorCumulativeX);
            var controllerRoll = new PidController(proportionalGainRoll, integralGainRoll, derivativeGainRoll, maxErrorCumulativeY);
            var controllerYaw = new PidController(proportionalGainYaw, integralGainYaw, derivativeGainYaw, maxErrorCumulativeZ);

            return Manipulate(processVariable, setpoint, controllerPitch, controllerRoll, controllerYaw);
        }

        public static IObservable<EulerAngles> Manipulate(
            this IObservable<EulerAngles> processVariable, IObservable<EulerAngles> setpoint,
            PidController controllerPitch, PidController controllerRoll, PidController controllerYaw)
        {
            return processVariable
                .CombineLatest(
                    setpoint,
                    (pv, sp) =>
                        new { ProcessVariable = pv, Setpoint = sp })
                .TimeInterval()
                .Select(t =>
                    {
                        double dt = t.Interval.TotalSeconds;

                        double manipulatedVariablePitch = controllerPitch.Manipulate(
                            processVariable: t.Value.ProcessVariable.Pitch,
                            setpoint: t.Value.Setpoint.Pitch,
                            elapsedTime: dt);

                        double manipulatedVariableRoll = controllerRoll.Manipulate(
                            processVariable: t.Value.ProcessVariable.Roll,
                            setpoint: t.Value.Setpoint.Roll,
                            elapsedTime: dt);

                        double manipulatedVariableYaw = controllerYaw.Manipulate(
                            processVariable: t.Value.ProcessVariable.Yaw,
                            setpoint: t.Value.Setpoint.Yaw,
                            elapsedTime: dt);

                        return new EulerAngles(
                            pitch: manipulatedVariablePitch,
                            roll: manipulatedVariableRoll,
                            yaw: manipulatedVariableYaw);
                    });
        }
    }
}

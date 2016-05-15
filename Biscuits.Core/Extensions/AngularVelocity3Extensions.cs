// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Extensions
{
    using Controllers;
    using Filters;
    using System;
    using System.Reactive.Linq;

    public static class AngularVelocity3Extensions
    {
        public static IObservable<AngularVelocity3> Filter(
            this IObservable<AngularVelocity3> source,
            double estimatedMeasurementNoiseCovariance,
            double estimatedProcessNoiseCovariance,
            double controlWeight,
            double initialEstimate,
            double initialErrorCovariance)
        {
            return Filter(
                source,
                estimatedMeasurementNoiseCovariance, estimatedProcessNoiseCovariance, controlWeight, initialEstimate, initialErrorCovariance,
                estimatedMeasurementNoiseCovariance, estimatedProcessNoiseCovariance, controlWeight, initialEstimate, initialErrorCovariance,
                estimatedMeasurementNoiseCovariance, estimatedProcessNoiseCovariance, controlWeight, initialEstimate, initialErrorCovariance);
        }

        public static IObservable<AngularVelocity3> Filter(
            this IObservable<AngularVelocity3> source,
            double estimatedMeasurementNoiseCovarianceX, double estimatedProcessNoiseCovarianceX, double controlWeightX, double initialEstimateX, double initialErrorCovarianceX,
            double estimatedMeasurementNoiseCovarianceY, double estimatedProcessNoiseCovarianceY, double controlWeightY, double initialEstimateY, double initialErrorCovarianceY,
            double estimatedMeasurementNoiseCovarianceZ, double estimatedProcessNoiseCovarianceZ, double controlWeightZ, double initialEstimateZ, double initialErrorCovarianceZ)
        {
            var filterX = new KalmanFilter(estimatedMeasurementNoiseCovarianceX, estimatedProcessNoiseCovarianceX, controlWeightX, initialEstimateX, initialErrorCovarianceX);
            var filterY = new KalmanFilter(estimatedMeasurementNoiseCovarianceY, estimatedProcessNoiseCovarianceY, controlWeightY, initialEstimateY, initialErrorCovarianceY);
            var filterZ = new KalmanFilter(estimatedMeasurementNoiseCovarianceZ, estimatedProcessNoiseCovarianceZ, controlWeightZ, initialEstimateZ, initialErrorCovarianceZ);

            return source
                .Select(angularVelocity =>
                    {
                        return new AngularVelocity3(
                            filterX.Filter(angularVelocity.X),
                            filterY.Filter(angularVelocity.Y),
                            filterZ.Filter(angularVelocity.Z));
                    });
        }

        public static IObservable<AngularVelocity3> Multiply(this IObservable<AngularVelocity3> source, double value)
        {
            return source.Select(angularVelocity => angularVelocity * value);
        }

        public static IObservable<AngularAcceleration3> Differentiate(this IObservable<AngularVelocity3> source)
        {
            return source
                .Scan(
                    new { Prior = default(AngularVelocity3), Current = default(AngularVelocity3) },
                    (accu, angularVelocity) => new { Prior = accu.Current, Current = angularVelocity })
                .Skip(1)
                .TimeInterval()
                .Select(t =>
                {
                    AngularVelocity3 dw = t.Value.Current - t.Value.Prior;
                    double dt = t.Interval.TotalSeconds;

                    return new AngularAcceleration3(
                        x: dw.X / dt,
                        y: dw.Y / dt,
                        z: dw.Z / dt);
                });
        }

        public static IObservable<Quaternion> ToRotation(this IObservable<AngularVelocity3> source)
        {
            return source
                .TimeInterval()
                .Select(angularVelocityTimed =>
                    {
                        AngularVelocity3 angularVelocity = angularVelocityTimed.Value;
                        double dt = angularVelocityTimed.Interval.TotalSeconds;

                        Vector3 w = angularVelocity.Normalize();
                        double theta = AngleConvert.ToRadians(angularVelocity.Magnitude) * dt;

                        return Quaternion.FromAxisAngle(w, -theta);
                    });
        }

        public static IObservable<AngularVelocity3> Manipulate(
            this IObservable<AngularVelocity3> processVariable, IObservable<AngularVelocity3> setpoint, 
            double proportionalGain, double integralGain, double derivativeGain)
        {
            return Manipulate(
                processVariable, setpoint,
                proportionalGain, integralGain, derivativeGain,
                proportionalGain, integralGain, derivativeGain,
                proportionalGain, integralGain, derivativeGain);
        }

        public static IObservable<AngularVelocity3> Manipulate(
            this IObservable<AngularVelocity3> processVariable, IObservable<AngularVelocity3> setpoint, 
            double proportionalGain, double integralGain, double derivativeGain, double maxErrorCumulative)
        {
            return Manipulate(
                processVariable, setpoint,
                proportionalGain, integralGain, derivativeGain, maxErrorCumulative,
                proportionalGain, integralGain, derivativeGain, maxErrorCumulative,
                proportionalGain, integralGain, derivativeGain, maxErrorCumulative);
        }

        public static IObservable<AngularVelocity3> Manipulate(
            this IObservable<AngularVelocity3> processVariable, IObservable<AngularVelocity3> setpoint, 
            double proportionalGainX, double integralGainX, double derivativeGainX,
            double proportionalGainY, double integralGainY, double derivativeGainY,
            double proportionalGainZ, double integralGainZ, double derivativeGainZ)
        {
            var controller = new AngularVelocityPidController(
                proportionalGainX, integralGainX, derivativeGainX,
                proportionalGainY, integralGainY, derivativeGainY,
                proportionalGainZ, integralGainZ, derivativeGainZ);

            return Manipulate(processVariable, setpoint, controller);
        }

        public static IObservable<AngularVelocity3> Manipulate(
            this IObservable<AngularVelocity3> processVariable, IObservable<AngularVelocity3> setpoint,
            double proportionalGainX, double integralGainX, double derivativeGainX, double maxErrorCumulativeX,
            double proportionalGainY, double integralGainY, double derivativeGainY, double maxErrorCumulativeY,
            double proportionalGainZ, double integralGainZ, double derivativeGainZ, double maxErrorCumulativeZ)
        {
            var controller = new AngularVelocityPidController(
                proportionalGainX, integralGainX, derivativeGainX, maxErrorCumulativeX,
                proportionalGainY, integralGainY, derivativeGainY, maxErrorCumulativeY,
                proportionalGainZ, integralGainZ, derivativeGainZ, maxErrorCumulativeZ);

            return Manipulate(processVariable, setpoint, controller);
        }

        public static IObservable<AngularVelocity3> Manipulate(
            this IObservable<AngularVelocity3> processVariable,
            IObservable<AngularVelocity3> setpoint,
            AngularVelocityPidController controller)
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
                        return controller.Manipulate(t.Value.ProcessVariable, t.Value.Setpoint, dt);
                    });
        }
    }
}

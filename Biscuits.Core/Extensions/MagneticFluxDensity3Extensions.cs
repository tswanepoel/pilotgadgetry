// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Extensions
{
    using Filters;
    using System;
    using System.Reactive.Linq;

    public static class MagneticFluxDensity3Extensions
    {
        public static IObservable<MagneticFluxDensity3> Filter(
            this IObservable<MagneticFluxDensity3> source, 
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

        public static IObservable<MagneticFluxDensity3> Filter(
            this IObservable<MagneticFluxDensity3> source,
            double estimatedMeasurementNoiseCovarianceX, double estimatedProcessNoiseCovarianceX, double controlWeightX, double initialEstimateX, double initialErrorCovarianceX,
            double estimatedMeasurementNoiseCovarianceY, double estimatedProcessNoiseCovarianceY, double controlWeightY, double initialEstimateY, double initialErrorCovarianceY,
            double estimatedMeasurementNoiseCovarianceZ, double estimatedProcessNoiseCovarianceZ, double controlWeightZ, double initialEstimateZ, double initialErrorCovarianceZ)
        {
            var filterX = new KalmanFilter(estimatedMeasurementNoiseCovarianceX, estimatedProcessNoiseCovarianceX, controlWeightX, initialEstimateX, initialErrorCovarianceX);
            var filterY = new KalmanFilter(estimatedMeasurementNoiseCovarianceY, estimatedProcessNoiseCovarianceY, controlWeightY, initialEstimateY, initialErrorCovarianceY);
            var filterZ = new KalmanFilter(estimatedMeasurementNoiseCovarianceZ, estimatedProcessNoiseCovarianceZ, controlWeightZ, initialEstimateZ, initialErrorCovarianceZ);

            return source
                .Select(magnetism =>
                    {
                        return new MagneticFluxDensity3(
                            filterX.Filter(magnetism.X),
                            filterY.Filter(magnetism.Y),
                            filterZ.Filter(magnetism.Z));
                    });
        }

        public static IObservable<Vector3> ToDirection(this IObservable<MagneticFluxDensity3> source, MagneticFluxDensity3 min, MagneticFluxDensity3 max)
        {
            return source
                .Select(magnetism =>
                    {
                        return new Vector3(
                            x: 2d * (magnetism.X - min.X) / (max.X - min.X) - 1d,
                            y: 2d * (magnetism.Y - min.Y) / (max.Y - min.Y) - 1d,
                            z: 2d * (magnetism.Z - min.Z) / (max.Z - min.Z) - 1d).Normalize();
                    });
        }
    }
}

// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Extensions
{
    using Filters;
    using System;
    using System.Reactive.Linq;
    
    public static class GForce3Extensions
    {
        public static IObservable<GForce3> Filter(
            this IObservable<GForce3> source, 
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

        public static IObservable<GForce3> Filter(
            this IObservable<GForce3> source,
            double estimatedMeasurementNoiseCovarianceX, double estimatedProcessNoiseCovarianceX, double controlWeightX, double initialEstimateX, double initialErrorCovarianceX,
            double estimatedMeasurementNoiseCovarianceY, double estimatedProcessNoiseCovarianceY, double controlWeightY, double initialEstimateY, double initialErrorCovarianceY,
            double estimatedMeasurementNoiseCovarianceZ, double estimatedProcessNoiseCovarianceZ, double controlWeightZ, double initialEstimateZ, double initialErrorCovarianceZ)
        {
            var filterX = new KalmanFilter(estimatedMeasurementNoiseCovarianceX, estimatedProcessNoiseCovarianceX, controlWeightX, initialEstimateX, initialErrorCovarianceX);
            var filterY = new KalmanFilter(estimatedMeasurementNoiseCovarianceY, estimatedProcessNoiseCovarianceY, controlWeightY, initialEstimateY, initialErrorCovarianceY);
            var filterZ = new KalmanFilter(estimatedMeasurementNoiseCovarianceZ, estimatedProcessNoiseCovarianceZ, controlWeightZ, initialEstimateZ, initialErrorCovarianceZ);

            return source
                .Select(acceleration =>
                    {
                        return new GForce3(
                            filterX.Filter(acceleration.X),
                            filterY.Filter(acceleration.Y),
                            filterZ.Filter(acceleration.Z));
                    });
        }

        public static IObservable<GForce3> Multiply(this IObservable<GForce3> source, double value)
        {
            return source.Select(acceleration => acceleration * value);
        }
    }
}

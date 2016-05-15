// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Filters
{
    /// <summary>
    /// Provides optimal estimates for a series of noisy measurements.
    /// </summary>
    /// <see href="http://bilgin.esme.org/BitsBytes/KalmanFilterforDummies.aspx" />
    public sealed class KalmanFilter
    {
        private readonly double _estimatedMeasurementNoiseCovariance;
        private readonly double _estimatedProcessNoiseCovariance;
        private readonly double _controlWeight;
        private double _priorEstimate;
        private double _priorErrorCovariance;

        /// <summary>
        /// Initializes the filter.
        /// </summary>
        /// <param name="estimatedMeasurementNoiseCovariance">The estimated measurement noise covariance (R).</param>
        /// <param name="estimatedProcessNoiseCovariance">The estimated process noise covariance (Q).</param>
        /// <param name="controlWeight">The weight (B) of new measurements (u<sub>k</sub>) for the linear stochastic equation to predict signal values (predicted x<sub>k</sub>).</param>
        public KalmanFilter(double estimatedMeasurementNoiseCovariance, double estimatedProcessNoiseCovariance, double controlWeight)
            : this(estimatedMeasurementNoiseCovariance, estimatedProcessNoiseCovariance, controlWeight, 0d, 1d)
        {
        }

        /// <summary>
        /// Initializes the filter.
        /// </summary>
        /// <param name="estimatedMeasurementNoiseCovariance">The estimated measurement noise covariance (R).</param>
        /// <param name="estimatedProcessNoiseCovariance">The estimated process noise covariance (Q).</param>
        /// <param name="controlWeight">The weight (B) of new measurements (u<sub>k</sub>) for the linear stochastic equation to predict signal values (predicted x<sub>k</sub>).</param>
        /// <param name="initialEstimate">The initial predicted signal value (predicted x<sub>0</sub>).</param>
        /// <param name="initialErrorCovariance">The initial predicted error covariance (predicted P<sub>0</sub>).</param>
        public KalmanFilter(double estimatedMeasurementNoiseCovariance, double estimatedProcessNoiseCovariance, double controlWeight, double initialEstimate, double initialErrorCovariance)
        {
            _estimatedMeasurementNoiseCovariance = estimatedMeasurementNoiseCovariance;
            _estimatedProcessNoiseCovariance = estimatedProcessNoiseCovariance;
            _controlWeight = controlWeight;
            _priorEstimate = initialEstimate;
            _priorErrorCovariance = initialErrorCovariance;
        }

        /// <summary>
        /// Returns an optimal estimate (x<sub>k</sub>) for the supplied measurement (z<sub>k</sub>).
        /// </summary>
        /// <param name="measurement">The measurement (z<sub>k</sub>).</param>
        /// <returns>An optimal estimate (x<sub>k</sub>).</returns>
        public double Filter(double measurement)
        {
            return Filter(measurement, measurement);
        }

        /// <summary>
        /// Returns an optimal estimate (x<sub>k</sub>) for the supplied control (u<sub>k</sub>) and measurement (z<sub>k</sub>).
        /// </summary>
        /// <param name="control">The control (u<sub>k</sub>).</param>
        /// <param name="measurement">The measurement (z<sub>k</sub>).</param>
        /// <returns>An optimal estimate (x<sub>k</sub>).</returns>
        public double Filter(double control, double measurement)
        {
            // prediction
            double estimate = PredictEstimate(1d - _controlWeight, _priorEstimate, _controlWeight, control);
            double errorCovariance = PredictErrorCovariance(1d - _controlWeight, _priorErrorCovariance, _estimatedProcessNoiseCovariance);

            // correction
            double gain = CalculateGain(errorCovariance, _estimatedMeasurementNoiseCovariance);
            double estimateCorrected = CorrectEstimate(estimate, gain, measurement);
            double errorCovarianceCorrected = CorrectErrorCovariance(gain, errorCovariance);

            _priorErrorCovariance = errorCovarianceCorrected;
            return _priorEstimate = estimateCorrected;
        }

        /// <summary>
        /// Returns a prediction of the optimal estimate (predicted x<sub>k</sub>) given the control (u<sub>k</sub>) using a linear stochastic equation.
        /// </summary>
        /// <param name="priorWeight">The weight (A) for the prior estimate (prior x<sub>k</sub>).</param>
        /// <param name="priorEstimate">The prior estimate (prior x<sub>k</sub>).</param>
        /// <param name="controlWeight">The weight (B) for the control (u<sub>k</sub>).</param>
        /// <param name="control">The control (u<sub>k</sub>).</param>
        /// <returns>A prediction of the optimal estimate (predicted x<sub>k</sub>).</returns>
        private static double PredictEstimate(double priorWeight, double priorEstimate, double controlWeight, double control)
        {
            return priorWeight * priorEstimate + controlWeight * control;
        }

        /// <summary>
        /// Returns a prediction of the error covariance estimate (predicted P<sub>k</sub>).
        /// </summary>
        /// <param name="priorWeight">The weight (A) of the prior estimate (prior x<sub>k</sub>).</param>
        /// <param name="priorErrorCovariance">The prior error covariance (prior P<sub>k</sub>).</param>
        /// <param name="estimatedProcessNoiseCovariance">The estimated process noise covariance (Q).</param>
        /// <returns>An prediction of the error covariance (predicted P<sub>k</sub>).</returns>
        private static double PredictErrorCovariance(double priorWeight, double priorErrorCovariance, double estimatedProcessNoiseCovariance)
        {
            return priorWeight * priorErrorCovariance + estimatedProcessNoiseCovariance;
        }

        /// <summary>
        /// Returns the Kalman Gain (K<sub>k</sub>) used to correct predicted estimates.
        /// </summary>
        /// <param name="errorCovariance">The predicted error covariance (predicted P<sub>k</sub>).</param>
        /// <param name="estimatedMeasurementNoiseCovariance">The estimated measurement noise covariance (R).</param>
        /// <returns>The Kalman Gain (K<sub>k</sub>).</returns>
        private static double CalculateGain(double errorCovariance, double estimatedMeasurementNoiseCovariance)
        {
            return errorCovariance / (errorCovariance + estimatedMeasurementNoiseCovariance);
        }

        /// <summary>
        /// Returns an optimal estimate (x<sub>k</sub>) of the measurement (z<sub>k</sub>).
        /// </summary>
        /// <param name="estimate">The predicted estimate (predicted x<sub>k</sub>).</param>
        /// <param name="gain">The Kalman Gain (K<sub>k</sub>).</param>
        /// <param name="measurement">The measurement (z<sub>k</sub>).</param>
        /// <returns>An optimal estimate (x<sub>k</sub>).</returns>
        private static double CorrectEstimate(double estimate, double gain, double measurement)
        {
            return estimate + gain * (measurement - estimate);
        }

        /// <summary>
        /// Returns an optimal error covariance estimate (P<sub>k</sub>).
        /// </summary>
        /// <param name="gain">The Kalman Gain (<sub>k</sub>).</param>
        /// <param name="errorCovariance">The predicted error covariance estimate (predicted P<sub>k</sub>).</param>
        /// <returns>An optimal error covariance estimate (P<sub>k</sub>).</returns>
        private static double CorrectErrorCovariance(double gain, double errorCovariance)
        {
            return (1d - gain) * errorCovariance;
        }
    }
}

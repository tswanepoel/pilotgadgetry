# C# .NET Quadcopter

This project is experimental. You shouldn't be using this if anyone's life or safety depends on it.

## Flight tests

| Date    | Title                                                         | Video |
|---------|---------------------------------------------------------------|-------|
| Mar '16 | [Flight test #4](https://www.youtube.com/watch?v=qiH9-Ixucz0) | [![Flight test #4](https://img.youtube.com/vi/qiH9-Ixucz0/0.jpg)](https://www.youtube.com/watch?v=qiH9-Ixucz0) |
| Mar '16 | [Flight test #3](https://www.youtube.com/watch?v=eUwYJJzWhso) | [![Flight test #4](https://img.youtube.com/vi/eUwYJJzWhso/0.jpg)](https://www.youtube.com/watch?v=eUwYJJzWhso) |
| Dec '15 | [Flight test #2](https://www.youtube.com/watch?v=wXpQVnmI2WU) | [![Flight test #4](https://img.youtube.com/vi/wXpQVnmI2WU/0.jpg)](https://www.youtube.com/watch?v=wXpQVnmI2WU) |
| Nov '15 | [Flight test #1](https://www.youtube.com/watch?v=5qz6GVNDpXA) | [![Flight test #4](https://img.youtube.com/vi/5qz6GVNDpXA/0.jpg)](https://www.youtube.com/watch?v=5qz6GVNDpXA) |

## Work-in-progress
- Gyroscope (L3GD20H), Accelerometer and Magnometer (LSM303D) interface
- Kalman filter
- Sensor Fusion / Attitude and Heading Reference System (AHRS)
- Logitech F710 Wireless Controller USB-HID
- PWM Controller (PCA9685) interface
- PID control for angular positions

## Experimental
- Barometer/Pressure Sensor and Thermometer (LPS25H) interface
- Ultrasonic/Proximity Sensor (SRF02) interface
- GPS (u-blox PAM-7Q) interface
- NMEA parser
- Linear acceleration estimation in Euclidean space
- Microsoft Xbox 360 Wireless Controller USB-HID
- Xeox Pro Gamepad Controller USB-HID

## References
- [Windows IoT Extensions for the UWP](https://msdn.microsoft.com/en-us/library/dn975273.aspx)
- [Reactive Extensions](http://reactivex.io/)

## Autopilot
```
var speedController = new SpeedController(Pca9685PwmController.Default);
var aircraft = new Quadcopter(speedController);

WindowsPilot.Create(
    aircraft,
    L3gd20hGyroscope.Default,
    Lsm303dAccelerometer.Default,
    Lsm303dMagnetometer.Default,
    FlightController.FromGamepad(LogitechF710Gamepad.Default),
    thrustMax: 0.8d,
    angularPositionPitchMax: AngleConvert.ToRadians(15d/*degrees*/),
    angularPositionRollMax: AngleConvert.ToRadians(15d/*degrees*/),
    angularVelocityYawMax: AngleConvert.ToRadians(90/*degrees-per-second*/));
```

## Nuget

| Package | Description |
|----|-------------|
| [Biscuits.Devices.L3gd20h.Windows](https://www.nuget.org/packages/Biscuits.Devices.L3gd20h.Windows/) | L3GD20H Gyroscope library for the Universal Windows Platform |
| [Biscuits.Devices.Lps25h.Windows](https://www.nuget.org/packages/Biscuits.Devices.Lps25h.Windows/) | LPS25H Pressure Sensor library for the Universal Windows Platform |
| [Biscuits.Devices.Lsm303d.Windows](https://www.nuget.org/packages/Biscuits.Devices.Lsm303d.Windows/)| LSM303D 3D Compass and Accelerometer library for the Universal Windows Platform |
| [Biscuits.Devices.Srf02.Windows](https://www.nuget.org/packages/Biscuits.Devices.Srf02.Windows/) | SRF02 Proximity Sensor library for the Universal Windows Platform |
| [Biscuits.Devices.Pca9685.Windows](https://www.nuget.org/packages/Biscuits.Devices.Pca9685.Windows/) | PCA9685 PWM Controller library for the Universal Windows Platform |
| [Biscuits.Devices.LogitechF710.Windows](https://www.nuget.org/packages/Biscuits.Devices.LogitechF710.Windows/) | Logitech F710 Gamepad library for the Universal Windows Platform |

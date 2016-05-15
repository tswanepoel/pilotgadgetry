// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Win10IoTDroneApp
{
    using Controllers;
    using Devices;
    using Flight;
    using System.Threading.Tasks;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Task.Factory.StartNew(Run);
        }

        public void Run()
        {
            var speedController = new SpeedController(Pca9685PwmController.Default);
            var aircraft = new Quadcopter(speedController);

            WindowsPilot.Create(
                aircraft,
                L3gd20hGyroscope.Default,
                Lsm303dAccelerometer.Default,
                Lsm303dMagnetometer.Default,
                FlightController.FromGamepad(LogitechF710Gamepad.Default),
                thrustMax: 0.8d,
                angularPositionPitchMax: AngleConvert.ToRadians(15d),
                angularPositionRollMax: AngleConvert.ToRadians(15d),
                angularVelocityYawMax: AngleConvert.ToRadians(90/*dps*/));
        }
    }
}

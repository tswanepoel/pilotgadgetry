// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Devices
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Windows.Devices.Enumeration;
    using Windows.Devices.HumanInterfaceDevice;
    using Windows.Storage;

    public class Xbox360Controller : IDisposable
    {
        public EventHandler<Xbox360ControllerInputEventArgs> InputChanged;
        public EventHandler<Xbox360ControllerThumbstickEventArgs> ThumbstickChanged;
        public EventHandler<Xbox360ControllerTriggerEventArgs> TriggerChanged;
        public EventHandler<Xbox360ControllerButtonEventArgs> ButtonDown;
        public EventHandler<Xbox360ControllerButtonEventArgs> ButtonUp;
        public EventHandler<Xbox360ControllerButtonEventArgs> ButtonPressed;

        private const ushort UsagePage = 0x0001;
        private const ushort UsageId = 0x0005;
        private const ushort VendorId = 0x045e;
        private const ushort ProductId = 0x02a1;
        private const int DefaultRepeatDelayMilliseconds = 250;
        private const int DefaultRepeatRateMilliseconds = 100;

        private HidDevice _device;
        private int _repeatDelayMilliseconds;
        private int _repeatRateMilliseconds;
        private Xbox360ControllerInputData _previousData;
        private CancellationTokenSource _aButtonPressedSource;
        private CancellationTokenSource _bButtonPressedSource;
        private CancellationTokenSource _xButtonPressedSource;
        private CancellationTokenSource _yButtonPressedSource;
        private CancellationTokenSource _leftBumperButtonPressedSource;
        private CancellationTokenSource _rightBumperButtonPressedSource;
        private CancellationTokenSource _backButtonPressedSource;
        private CancellationTokenSource _startButtonPressedSource;
        private CancellationTokenSource _leftThumbstickButtonPressedSource;
        private CancellationTokenSource _rightThumbstickButtonPressedSource;
        private CancellationTokenSource _upButtonPressedSource;
        private CancellationTokenSource _rightButtonPressedSource;
        private CancellationTokenSource _downButtonPressedSource;
        private CancellationTokenSource _leftButtonPressedSource;
        
        public Xbox360Controller()
            : this(DefaultRepeatDelayMilliseconds, DefaultRepeatRateMilliseconds)
        {
        }

        public Xbox360Controller(int repeatDelayMilliseconds, int repeatRateMilliseconds)
        {
            _repeatDelayMilliseconds = repeatDelayMilliseconds;
            _repeatRateMilliseconds = repeatRateMilliseconds;

            Initialize();
        }

        private void Initialize()
        { 
            string query = HidDevice.GetDeviceSelector(UsagePage, UsageId, VendorId, ProductId);
            DeviceInformationCollection deviceInfos = DeviceInformation.FindAllAsync(query).AsTask().Result;
            string deviceId = deviceInfos.FirstOrDefault()?.Id;

            if (deviceId == null)
            {
                throw new NotSupportedException();
            }

            _device = HidDevice.FromIdAsync(deviceId, FileAccessMode.Read).AsTask().Result;
            _device.InputReportReceived += DeviceInputReportReceived;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void DeviceInputReportReceived(HidDevice sender, HidInputReportReceivedEventArgs e)
        {
            bool isAButtonDown = false;
            bool isBButtonDown = false;
            bool isXButtonDown = false;
            bool isYButtonDown = false;
            bool isBackButtonDown = false;
            bool isStartButtonDown = false;
            bool isLeftBumperButtonDown = false;
            bool isRightBumperButtonDown = false;
            bool isLeftThumbstickButtonDown = false;
            bool isRightThumbstickButtonDown = false;

            foreach (HidBooleanControl control in e.Report.ActivatedBooleanControls)
            {
                if (!Enum.IsDefined(typeof(Xbox360ControllerButtonUsage), control.UsageId))
                {
                    continue;
                }

                var usage = (Xbox360ControllerButtonUsage)control.UsageId;

                switch (usage)
                {
                    case Xbox360ControllerButtonUsage.A:
                        isAButtonDown = control.IsActive;
                        break;

                    case Xbox360ControllerButtonUsage.B:
                        isBButtonDown = control.IsActive;
                        break;

                    case Xbox360ControllerButtonUsage.X:
                        isXButtonDown = control.IsActive;
                        break;

                    case Xbox360ControllerButtonUsage.Y:
                        isYButtonDown = control.IsActive;
                        break;

                    case Xbox360ControllerButtonUsage.Back:
                        isBackButtonDown = control.IsActive;
                        break;

                    case Xbox360ControllerButtonUsage.Start:
                        isStartButtonDown = control.IsActive;
                        break;

                    case Xbox360ControllerButtonUsage.LeftBumper:
                        isLeftBumperButtonDown = control.IsActive;
                        break;

                    case Xbox360ControllerButtonUsage.RightBumper:
                        isRightBumperButtonDown = control.IsActive;
                        break;

                    case Xbox360ControllerButtonUsage.LeftThumbstick:
                        isLeftThumbstickButtonDown = control.IsActive;
                        break;

                    case Xbox360ControllerButtonUsage.RightThumbstick:
                        isRightThumbstickButtonDown = control.IsActive;
                        break;
                }
            }

            const ushort usagePageGeneric = 0x01/*Generic*/;

            long leftThumbstickHorizontal = e.Report.GetNumericControl(usagePageGeneric, (ushort)Xbox360ControllerGenericUsage.X).Value;
            long leftThumbstickVertical = e.Report.GetNumericControl(usagePageGeneric, (ushort)Xbox360ControllerGenericUsage.Y).Value;
            long trigger = e.Report.GetNumericControl(usagePageGeneric, (ushort)Xbox360ControllerGenericUsage.Z).Value;
            long rightThumbstickHorizontal = e.Report.GetNumericControl(usagePageGeneric, (ushort)Xbox360ControllerGenericUsage.RX).Value;
            long rightThumbstickVertical = e.Report.GetNumericControl(usagePageGeneric, (ushort)Xbox360ControllerGenericUsage.RY).Value;
            long hatSwitch = e.Report.GetNumericControl(usagePageGeneric, (ushort)Xbox360ControllerGenericUsage.HatSwitch).Value;

            bool isUpButtonDown = hatSwitch == 1 || hatSwitch == 2 || hatSwitch == 8;
            bool isRightButtonDown = hatSwitch == 2 || hatSwitch == 3 || hatSwitch == 4;
            bool isDownButtonDown = hatSwitch == 4 || hatSwitch == 5 || hatSwitch == 6;
            bool isLeftButtonDown = hatSwitch == 6 || hatSwitch == 7 || hatSwitch == 8;

            var data = new Xbox360ControllerInputData(
                isAButtonDown,
                isBButtonDown, 
                isXButtonDown,
                isYButtonDown, 
                isLeftBumperButtonDown, 
                isRightBumperButtonDown, 
                isBackButtonDown, 
                isStartButtonDown, 
                isLeftThumbstickButtonDown, 
                isRightThumbstickButtonDown,
                leftThumbstickHorizontal, 
                leftThumbstickVertical, 
                trigger, 
                rightThumbstickHorizontal, 
                rightThumbstickVertical, 
                isUpButtonDown, 
                isRightButtonDown, 
                isDownButtonDown, 
                isLeftButtonDown);

            OnInputChanged(data);

            if (_previousData == null)
            {
                _previousData = data;
                return;
            }

            if (_previousData == null || isAButtonDown != _previousData.IsAButtonDown)
            {
                if (isAButtonDown)
                {
                    OnButtonDown(Xbox360ControllerButton.A);
                    OnButtonPressed(Xbox360ControllerButton.A);

                    _aButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(Xbox360ControllerButton.A, _repeatDelayMilliseconds, _repeatRateMilliseconds, _aButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _aButtonPressedSource.Cancel();
                    OnButtonUp(Xbox360ControllerButton.A);
                }
            }

            if (_previousData == null || isBButtonDown != _previousData.IsBButtonDown)
            {
                if (isBButtonDown)
                {
                    OnButtonDown(Xbox360ControllerButton.B);
                    OnButtonPressed(Xbox360ControllerButton.B);

                    _bButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(Xbox360ControllerButton.B, _repeatDelayMilliseconds, _repeatRateMilliseconds, _bButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _bButtonPressedSource.Cancel();
                    OnButtonUp(Xbox360ControllerButton.B);
                }
            }

            if (_previousData == null || isXButtonDown != _previousData.IsXButtonDown)
            {
                if (isXButtonDown)
                {
                    OnButtonDown(Xbox360ControllerButton.X);
                    OnButtonPressed(Xbox360ControllerButton.X);

                    _xButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(Xbox360ControllerButton.X, _repeatDelayMilliseconds, _repeatRateMilliseconds, _xButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _xButtonPressedSource.Cancel();
                    OnButtonUp(Xbox360ControllerButton.X);
                }
            }

            if (_previousData == null || isYButtonDown != _previousData.IsYButtonDown)
            {
                if (isYButtonDown)
                {
                    OnButtonDown(Xbox360ControllerButton.Y);
                    OnButtonPressed(Xbox360ControllerButton.Y);

                    _yButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(Xbox360ControllerButton.Y, _repeatDelayMilliseconds, _repeatRateMilliseconds, _yButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _yButtonPressedSource.Cancel();
                    OnButtonUp(Xbox360ControllerButton.Y);
                }
            }

            if (_previousData == null || isLeftBumperButtonDown != _previousData.IsLeftBumperButtonDown)
            {
                if (isLeftBumperButtonDown)
                {
                    OnButtonDown(Xbox360ControllerButton.LeftBumber);
                    OnButtonPressed(Xbox360ControllerButton.LeftBumber);

                    _leftBumperButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(Xbox360ControllerButton.LeftBumber, _repeatDelayMilliseconds, _repeatRateMilliseconds, _leftBumperButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _leftBumperButtonPressedSource.Cancel();
                    OnButtonUp(Xbox360ControllerButton.LeftBumber);
                }
            }

            if (_previousData == null || isRightBumperButtonDown != _previousData.IsRightBumperButtonDown)
            {
                if (isRightBumperButtonDown)
                {
                    OnButtonDown(Xbox360ControllerButton.RightBumber);
                    OnButtonPressed(Xbox360ControllerButton.RightBumber);

                    _rightBumperButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(Xbox360ControllerButton.RightBumber, _repeatDelayMilliseconds, _repeatRateMilliseconds, _rightBumperButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _rightBumperButtonPressedSource.Cancel();
                    OnButtonUp(Xbox360ControllerButton.RightBumber);
                }
            }

            if (_previousData == null || isBackButtonDown != _previousData.IsBackButtonDown)
            {
                if (isBackButtonDown)
                {
                    OnButtonDown(Xbox360ControllerButton.Back);
                    OnButtonPressed(Xbox360ControllerButton.Back);

                    _backButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(Xbox360ControllerButton.Back, _repeatDelayMilliseconds, _repeatRateMilliseconds, _backButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _backButtonPressedSource.Cancel();
                    OnButtonUp(Xbox360ControllerButton.Back);
                }
            }

            if (_previousData == null || isStartButtonDown != _previousData.IsStartButtonDown)
            {
                if (isStartButtonDown)
                {
                    OnButtonDown(Xbox360ControllerButton.Start);
                    OnButtonPressed(Xbox360ControllerButton.Start);

                    _startButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(Xbox360ControllerButton.Start, _repeatDelayMilliseconds, _repeatRateMilliseconds, _startButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _startButtonPressedSource.Cancel();
                    OnButtonUp(Xbox360ControllerButton.Start);
                }
            }

            if (_previousData == null || isLeftThumbstickButtonDown != _previousData.IsLeftThumbstickButtonDown)
            {
                if (isLeftThumbstickButtonDown)
                {
                    OnButtonDown(Xbox360ControllerButton.LeftThumbstick);
                    OnButtonPressed(Xbox360ControllerButton.LeftThumbstick);

                    _leftThumbstickButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(Xbox360ControllerButton.LeftThumbstick, _repeatDelayMilliseconds, _repeatRateMilliseconds, _leftThumbstickButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _leftThumbstickButtonPressedSource.Cancel();
                    OnButtonUp(Xbox360ControllerButton.LeftThumbstick);
                }
            }

            if (_previousData == null || isRightThumbstickButtonDown != _previousData.IsRightThumbstickButtonDown)
            {
                if (isRightThumbstickButtonDown)
                {
                    OnButtonDown(Xbox360ControllerButton.RightThumbstick);
                    OnButtonPressed(Xbox360ControllerButton.RightThumbstick);

                    _rightThumbstickButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(Xbox360ControllerButton.RightThumbstick, _repeatDelayMilliseconds, _repeatRateMilliseconds, _rightThumbstickButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _rightThumbstickButtonPressedSource.Cancel();
                    OnButtonUp(Xbox360ControllerButton.RightThumbstick);
                }
            }

            if (_previousData == null || isUpButtonDown != _previousData.IsUpButtonDown)
            {
                if (isUpButtonDown)
                {
                    OnButtonDown(Xbox360ControllerButton.Up);
                    OnButtonPressed(Xbox360ControllerButton.Up);

                    _upButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(Xbox360ControllerButton.Up, _repeatDelayMilliseconds, _repeatRateMilliseconds, _upButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _upButtonPressedSource.Cancel();
                    OnButtonUp(Xbox360ControllerButton.Up);
                }
            }

            if (_previousData == null || isRightButtonDown != _previousData.IsRightButtonDown)
            {
                if (isRightButtonDown)
                {
                    OnButtonDown(Xbox360ControllerButton.Right);
                    OnButtonPressed(Xbox360ControllerButton.Right);

                    _rightButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(Xbox360ControllerButton.Right, _repeatDelayMilliseconds, _repeatRateMilliseconds, _rightButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _rightButtonPressedSource.Cancel();
                    OnButtonUp(Xbox360ControllerButton.Right);
                }
            }

            if (_previousData == null || isDownButtonDown != _previousData.IsDownButtonDown)
            {
                if (isDownButtonDown)
                {
                    OnButtonDown(Xbox360ControllerButton.Down);
                    OnButtonPressed(Xbox360ControllerButton.Down);

                    _downButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(Xbox360ControllerButton.Down, _repeatDelayMilliseconds, _repeatRateMilliseconds, _downButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _downButtonPressedSource.Cancel();
                    OnButtonUp(Xbox360ControllerButton.Down);
                }
            }

            if (_previousData == null || isLeftButtonDown != _previousData.IsLeftButtonDown)
            {
                if (isLeftButtonDown)
                {
                    OnButtonDown(Xbox360ControllerButton.Left);
                    OnButtonPressed(Xbox360ControllerButton.Left);

                    _leftButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(Xbox360ControllerButton.Left, _repeatDelayMilliseconds, _repeatRateMilliseconds, _leftButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _leftButtonPressedSource.Cancel();
                    OnButtonUp(Xbox360ControllerButton.Left);
                }
            }

            if (_previousData != null)
            {
                if (leftThumbstickHorizontal != _previousData.LeftThumbstickHorizontal ||
                    leftThumbstickVertical != _previousData.LeftThumbstickVertical)
                {
                    OnThumbstickChanged(Xbox360ControllerThumbstick.Left, leftThumbstickHorizontal, leftThumbstickVertical);
                }

                if (rightThumbstickHorizontal != _previousData.RightThumbstickHorizontal ||
                    rightThumbstickVertical != _previousData.RightThumbstickVertical)
                {
                    OnThumbstickChanged(Xbox360ControllerThumbstick.Right, rightThumbstickHorizontal, rightThumbstickVertical);
                }

                if (trigger != _previousData.Trigger)
                {
                    OnTriggerChanged(trigger);
                }
            }

            _previousData = data;
        }

        protected virtual void OnInputChanged(Xbox360ControllerInputData data)
        {
            InputChanged?.Invoke(this, new Xbox360ControllerInputEventArgs(data));
        }

        protected virtual void OnButtonDown(Xbox360ControllerButton button)
        {
            ButtonDown?.Invoke(this, new Xbox360ControllerButtonEventArgs(button));
        }

        protected virtual void OnButtonUp(Xbox360ControllerButton button)
        {
            ButtonUp?.Invoke(this, new Xbox360ControllerButtonEventArgs(button));
        }

        protected virtual void OnButtonPressed(Xbox360ControllerButton button)
        {
            ButtonPressed?.Invoke(this, new Xbox360ControllerButtonEventArgs(button));
        }

        protected virtual void OnThumbstickChanged(Xbox360ControllerThumbstick thumbstick, long horizontal, long vertical)
        {
            ThumbstickChanged?.Invoke(this, new Xbox360ControllerThumbstickEventArgs(thumbstick, horizontal, vertical));
        }

        protected virtual void OnTriggerChanged(long value)
        {
            TriggerChanged?.Invoke(this, new Xbox360ControllerTriggerEventArgs(value));
        }

        private void RepeatButtonPressed(Xbox360ControllerButton button, int repeatDelay, int repeatRate, CancellationToken token)
        {
            Action action = null;

            action = new Action(() =>
            {
                if (token.IsCancellationRequested) { return; }

                try
                {
                    Task.Delay(repeatRate).Wait(token);
                }
                catch (OperationCanceledException) { }

                if (token.IsCancellationRequested) { return; }
                OnButtonPressed(button);

                Task.Run(action);
            });

            Action wrapper = new Action(() =>
            {
                if (token.IsCancellationRequested) { return; }

                try
                {
                    Task.Delay(repeatDelay).Wait(token);
                }
                catch (OperationCanceledException) { }

                if (token.IsCancellationRequested) { return; }
                Task.Run(action);
            });
            
            Task.Run(wrapper);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _device.InputReportReceived -= DeviceInputReportReceived;
                _device.Dispose();

                if (_aButtonPressedSource != null)
                {
                    _aButtonPressedSource.Cancel();
                    _aButtonPressedSource.Dispose();
                }

                if (_bButtonPressedSource != null)
                {
                    _bButtonPressedSource.Cancel();
                    _bButtonPressedSource.Dispose();
                }

                if (_xButtonPressedSource != null)
                {
                    _xButtonPressedSource.Cancel();
                    _xButtonPressedSource.Dispose();
                }

                if (_yButtonPressedSource != null)
                {
                    _yButtonPressedSource.Cancel();
                    _yButtonPressedSource.Dispose();
                }

                if (_leftBumperButtonPressedSource != null)
                {
                    _leftBumperButtonPressedSource.Cancel();
                    _leftBumperButtonPressedSource.Dispose();
                }

                if (_rightBumperButtonPressedSource != null)
                {
                    _rightBumperButtonPressedSource.Cancel();
                    _rightBumperButtonPressedSource.Dispose();
                }

                if (_backButtonPressedSource != null)
                {
                    _backButtonPressedSource.Cancel();
                    _backButtonPressedSource.Dispose();
                }

                if (_startButtonPressedSource != null)
                {
                    _startButtonPressedSource.Cancel();
                    _startButtonPressedSource.Dispose();
                }

                if (_leftThumbstickButtonPressedSource != null)
                {
                    _leftThumbstickButtonPressedSource.Cancel();
                    _leftThumbstickButtonPressedSource.Dispose();
                }

                if (_rightThumbstickButtonPressedSource != null)
                {
                    _rightThumbstickButtonPressedSource.Cancel();
                    _rightThumbstickButtonPressedSource.Dispose();
                }

                if (_upButtonPressedSource != null)
                {
                    _upButtonPressedSource.Cancel();
                    _upButtonPressedSource.Dispose();
                }

                if (_rightButtonPressedSource != null)
                {
                    _rightButtonPressedSource.Cancel();
                    _rightButtonPressedSource.Dispose();
                }

                if (_downButtonPressedSource != null)
                {
                    _downButtonPressedSource.Cancel();
                    _downButtonPressedSource.Dispose();
                }

                if (_leftButtonPressedSource != null)
                {
                    _leftButtonPressedSource.Cancel();
                    _leftButtonPressedSource.Dispose();
                }
            }
        }
    }
}

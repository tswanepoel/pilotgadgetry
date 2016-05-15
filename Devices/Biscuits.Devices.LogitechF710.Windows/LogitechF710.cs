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

    /// <summary>
    /// Provides a USB human interface device (USB-HID) interface to Logitech F710.
    /// </summary>
    public class LogitechF710 : IDisposable
    {
        /// <summary>
        /// Occurs when the input is changed.
        /// </summary>
        public event EventHandler<LogitechF710InputEventArgs> InputChanged;

        /// <summary>
        /// Occurs when a thumbstick input is changed.
        /// </summary>
        public event EventHandler<LogitechF710ThumbstickEventArgs> ThumbstickChanged;

        /// <summary>
        /// Occurs when a button goes down.
        /// </summary>
        public event EventHandler<LogitechF710ButtonEventArgs> ButtonDown;

        /// <summary>
        /// Occurs when a button goes up.
        /// </summary>
        public event EventHandler<LogitechF710ButtonEventArgs> ButtonUp;

        /// <summary>
        /// Occurs when a button is pressed or held down.
        /// </summary>
        public event EventHandler<LogitechF710ButtonEventArgs> ButtonPressed;

        private const ushort UsagePage = 0x0001;
        private const ushort UsageId = 0x0005;
        private const ushort VendorId = 0x046d;
        private const ushort ProductId = 0xc219;
        private const int DefaultRepeatDelayMilliseconds = 250;
        private const int DefaultRepeatRateMilliseconds = 100;

        private HidDevice _device;
        private int _repeatDelayMilliseconds;
        private int _repeatRateMilliseconds;
        private LogitechF710InputData _previousData;
        private CancellationTokenSource _xButtonPressedSource;
        private CancellationTokenSource _aButtonPressedSource;
        private CancellationTokenSource _bButtonPressedSource;
        private CancellationTokenSource _yButtonPressedSource;
        private CancellationTokenSource _leftBumperButtonPressedSource;
        private CancellationTokenSource _rightBumperButtonPressedSource;
        private CancellationTokenSource _leftTriggerButtonPressedSource;
        private CancellationTokenSource _rightTriggerButtonPressedSource;
        private CancellationTokenSource _backButtonPressedSource;
        private CancellationTokenSource _startButtonPressedSource;
        private CancellationTokenSource _leftThumbstickButtonPressedSource;
        private CancellationTokenSource _rightThumbstickButtonPressedSource;
        private CancellationTokenSource _upButtonPressedSource;
        private CancellationTokenSource _rightButtonPressedSource;
        private CancellationTokenSource _downButtonPressedSource;
        private CancellationTokenSource _leftButtonPressedSource;
        private bool _disposed = false;
        
        /// <summary>
        /// Initializes the USB-HID interface to Logitech F710.
        /// </summary>
        public LogitechF710()
            : this(DefaultRepeatDelayMilliseconds, DefaultRepeatRateMilliseconds)
        {
        }

        /// <summary>
        /// Initializes the USB-HID interface to Logitech F710.
        /// </summary>
        /// <param name="repeatDelayMilliseconds">The duration of time before presses repeat while a button is held down, in milliseconds.</param>
        /// <param name="repeatRateMilliseconds">The duration of time between presses as they repeat while a button is held down, in milliseconds.</param>
        public LogitechF710(int repeatDelayMilliseconds, int repeatRateMilliseconds)
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
        
        private void DeviceInputReportReceived(HidDevice sender, HidInputReportReceivedEventArgs e)
        {
            bool isXButtonDown = false;
            bool isAButtonDown = false;
            bool isBButtonDown = false;
            bool isYButtonDown = false;
            bool isLeftBumperButtonDown = false;
            bool isRightBumperButtonDown = false;
            bool isLeftTriggerButtonDown = false;
            bool isRightTriggerButtonDown = false;
            bool isBackButtonDown = false;
            bool isStartButtonDown = false;
            bool isLeftThumbstickButtonDown = false;
            bool isRightThumbstickButtonDown = false;

            foreach (HidBooleanControl control in e.Report.ActivatedBooleanControls)
            {
                if (!Enum.IsDefined(typeof(LogitechF710ButtonUsage), control.UsageId))
                {
                    continue;
                }

                var usage = (LogitechF710ButtonUsage)control.UsageId;

                switch (usage)
                {
                    case LogitechF710ButtonUsage.X:
                        isXButtonDown = control.IsActive;
                        break;

                    case LogitechF710ButtonUsage.A:
                        isAButtonDown = control.IsActive;
                        break;

                    case LogitechF710ButtonUsage.B:
                        isBButtonDown = control.IsActive;
                        break;

                    case LogitechF710ButtonUsage.Y:
                        isYButtonDown = control.IsActive;
                        break;

                    case LogitechF710ButtonUsage.LeftBumper:
                        isLeftBumperButtonDown = control.IsActive;
                        break;

                    case LogitechF710ButtonUsage.RightBumper:
                        isRightBumperButtonDown = control.IsActive;
                        break;

                    case LogitechF710ButtonUsage.LeftTigger:
                        isLeftTriggerButtonDown = control.IsActive;
                        break;

                    case LogitechF710ButtonUsage.RightTrigger:
                        isRightTriggerButtonDown = control.IsActive;
                        break;

                    case LogitechF710ButtonUsage.Back:
                        isBackButtonDown = control.IsActive;
                        break;

                    case LogitechF710ButtonUsage.Start:
                        isStartButtonDown = control.IsActive;
                        break;

                    case LogitechF710ButtonUsage.LeftThumbstick:
                        isLeftThumbstickButtonDown = control.IsActive;
                        break;

                    case LogitechF710ButtonUsage.RightThumbstick:
                        isRightThumbstickButtonDown = control.IsActive;
                        break;
                }
            }

            const ushort usagePageGeneric = 0x01/*Generic*/;

            long leftThumbstickHorizontal = e.Report.GetNumericControl(usagePageGeneric, (ushort)LogitechF710GenericUsage.X).Value;
            long leftThumbstickVertical = e.Report.GetNumericControl(usagePageGeneric, (ushort)LogitechF710GenericUsage.Y).Value;
            long rightThumbstickHorizontal = e.Report.GetNumericControl(usagePageGeneric, (ushort)LogitechF710GenericUsage.RX).Value;
            long rightThumbstickVertical = e.Report.GetNumericControl(usagePageGeneric, (ushort)LogitechF710GenericUsage.RY).Value;
            long hatSwitch = e.Report.GetNumericControl(usagePageGeneric, (ushort)LogitechF710GenericUsage.HatSwitch).Value;
            
            bool isUpButtonDown = hatSwitch == 7 || hatSwitch == 0 || hatSwitch == 1;
            bool isRightButtonDown = hatSwitch == 1 || hatSwitch == 2 || hatSwitch == 3;
            bool isDownButtonDown = hatSwitch == 3 || hatSwitch == 4 || hatSwitch == 5;
            bool isLeftButtonDown = hatSwitch == 5 || hatSwitch == 6 || hatSwitch == 7;

            var data = new LogitechF710InputData(
                isXButtonDown,
                isAButtonDown, 
                isBButtonDown,
                isYButtonDown, 
                isLeftBumperButtonDown, 
                isRightBumperButtonDown,
                isLeftTriggerButtonDown,
                isRightTriggerButtonDown,
                isBackButtonDown, 
                isStartButtonDown, 
                isLeftThumbstickButtonDown, 
                isRightThumbstickButtonDown,
                leftThumbstickHorizontal, 
                leftThumbstickVertical, 
                rightThumbstickHorizontal, 
                rightThumbstickVertical, 
                isUpButtonDown, 
                isRightButtonDown, 
                isDownButtonDown, 
                isLeftButtonDown);

            OnInputChanged(data);
            
            if (_previousData == null || isXButtonDown != _previousData.IsXButtonDown)
            {
                if (isXButtonDown)
                {
                    OnButtonDown(LogitechF710Button.X);
                    OnButtonPressed(LogitechF710Button.X);

                    _xButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(LogitechF710Button.X, _repeatDelayMilliseconds, _repeatRateMilliseconds, _xButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _xButtonPressedSource.Cancel();
                    OnButtonUp(LogitechF710Button.X);
                }
            }

            if (_previousData == null || isAButtonDown != _previousData.IsAButtonDown)
            {
                if (isAButtonDown)
                {
                    OnButtonDown(LogitechF710Button.A);
                    OnButtonPressed(LogitechF710Button.A);

                    _aButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(LogitechF710Button.A, _repeatDelayMilliseconds, _repeatRateMilliseconds, _aButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _aButtonPressedSource.Cancel();
                    OnButtonUp(LogitechF710Button.A);
                }
            }

            if (_previousData == null || isBButtonDown != _previousData.IsBButtonDown)
            {
                if (isBButtonDown)
                {
                    OnButtonDown(LogitechF710Button.B);
                    OnButtonPressed(LogitechF710Button.B);

                    _bButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(LogitechF710Button.B, _repeatDelayMilliseconds, _repeatRateMilliseconds, _bButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _bButtonPressedSource.Cancel();
                    OnButtonUp(LogitechF710Button.B);
                }
            }

            if (_previousData == null || isYButtonDown != _previousData.IsYButtonDown)
            {
                if (isYButtonDown)
                {
                    OnButtonDown(LogitechF710Button.Y);
                    OnButtonPressed(LogitechF710Button.Y);

                    _yButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(LogitechF710Button.Y, _repeatDelayMilliseconds, _repeatRateMilliseconds, _yButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _yButtonPressedSource.Cancel();
                    OnButtonUp(LogitechF710Button.Y);
                }
            }

            if (_previousData == null || isLeftBumperButtonDown != _previousData.IsLeftBumperButtonDown)
            {
                if (isLeftBumperButtonDown)
                {
                    OnButtonDown(LogitechF710Button.LeftBumber);
                    OnButtonPressed(LogitechF710Button.LeftBumber);

                    _leftBumperButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(LogitechF710Button.LeftBumber, _repeatDelayMilliseconds, _repeatRateMilliseconds, _leftBumperButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _leftBumperButtonPressedSource.Cancel();
                    OnButtonUp(LogitechF710Button.LeftBumber);
                }
            }

            if (_previousData == null || isRightBumperButtonDown != _previousData.IsRightBumperButtonDown)
            {
                if (isRightBumperButtonDown)
                {
                    OnButtonDown(LogitechF710Button.RightBumber);
                    OnButtonPressed(LogitechF710Button.RightBumber);

                    _rightBumperButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(LogitechF710Button.RightBumber, _repeatDelayMilliseconds, _repeatRateMilliseconds, _rightBumperButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _rightBumperButtonPressedSource.Cancel();
                    OnButtonUp(LogitechF710Button.RightBumber);
                }
            }

            if (_previousData == null || isLeftTriggerButtonDown != _previousData.IsLeftTriggerButtonDown)
            {
                if (isLeftTriggerButtonDown)
                {
                    OnButtonDown(LogitechF710Button.LeftTrigger);
                    OnButtonPressed(LogitechF710Button.LeftTrigger);

                    _leftTriggerButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(LogitechF710Button.LeftTrigger, _repeatDelayMilliseconds, _repeatRateMilliseconds, _leftTriggerButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _leftTriggerButtonPressedSource.Cancel();
                    OnButtonUp(LogitechF710Button.LeftTrigger);
                }
            }

            if (_previousData == null || isRightTriggerButtonDown != _previousData.IsRightTriggerButtonDown)
            {
                if (isRightTriggerButtonDown)
                {
                    OnButtonDown(LogitechF710Button.RightTrigger);
                    OnButtonPressed(LogitechF710Button.RightTrigger);

                    _rightTriggerButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(LogitechF710Button.RightTrigger, _repeatDelayMilliseconds, _repeatRateMilliseconds, _rightTriggerButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _rightTriggerButtonPressedSource.Cancel();
                    OnButtonUp(LogitechF710Button.RightTrigger);
                }
            }

            if (_previousData == null || isBackButtonDown != _previousData.IsBackButtonDown)
            {
                if (isBackButtonDown)
                {
                    OnButtonDown(LogitechF710Button.Back);
                    OnButtonPressed(LogitechF710Button.Back);

                    _backButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(LogitechF710Button.Back, _repeatDelayMilliseconds, _repeatRateMilliseconds, _backButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _backButtonPressedSource.Cancel();
                    OnButtonUp(LogitechF710Button.Back);
                }
            }

            if (_previousData == null || isStartButtonDown != _previousData.IsStartButtonDown)
            {
                if (isStartButtonDown)
                {
                    OnButtonDown(LogitechF710Button.Start);
                    OnButtonPressed(LogitechF710Button.Start);

                    _startButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(LogitechF710Button.Start, _repeatDelayMilliseconds, _repeatRateMilliseconds, _startButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _startButtonPressedSource.Cancel();
                    OnButtonUp(LogitechF710Button.Start);
                }
            }

            if (_previousData == null || isLeftThumbstickButtonDown != _previousData.IsLeftThumbstickButtonDown)
            {
                if (isLeftThumbstickButtonDown)
                {
                    OnButtonDown(LogitechF710Button.LeftThumbstick);
                    OnButtonPressed(LogitechF710Button.LeftThumbstick);

                    _leftThumbstickButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(LogitechF710Button.LeftThumbstick, _repeatDelayMilliseconds, _repeatRateMilliseconds, _leftThumbstickButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _leftThumbstickButtonPressedSource.Cancel();
                    OnButtonUp(LogitechF710Button.LeftThumbstick);
                }
            }

            if (_previousData == null || isRightThumbstickButtonDown != _previousData.IsRightThumbstickButtonDown)
            {
                if (isRightThumbstickButtonDown)
                {
                    OnButtonDown(LogitechF710Button.RightThumbstick);
                    OnButtonPressed(LogitechF710Button.RightThumbstick);

                    _rightThumbstickButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(LogitechF710Button.RightThumbstick, _repeatDelayMilliseconds, _repeatRateMilliseconds, _rightThumbstickButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _rightThumbstickButtonPressedSource.Cancel();
                    OnButtonUp(LogitechF710Button.RightThumbstick);
                }
            }

            if (_previousData == null || isUpButtonDown != _previousData.IsUpButtonDown)
            {
                if (isUpButtonDown)
                {
                    OnButtonDown(LogitechF710Button.Up);
                    OnButtonPressed(LogitechF710Button.Up);

                    _upButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(LogitechF710Button.Up, _repeatDelayMilliseconds, _repeatRateMilliseconds, _upButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _upButtonPressedSource.Cancel();
                    OnButtonUp(LogitechF710Button.Up);
                }
            }

            if (_previousData == null || isRightButtonDown != _previousData.IsRightButtonDown)
            {
                if (isRightButtonDown)
                {
                    OnButtonDown(LogitechF710Button.Right);
                    OnButtonPressed(LogitechF710Button.Right);

                    _rightButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(LogitechF710Button.Right, _repeatDelayMilliseconds, _repeatRateMilliseconds, _rightButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _rightButtonPressedSource.Cancel();
                    OnButtonUp(LogitechF710Button.Right);
                }
            }

            if (_previousData == null || isDownButtonDown != _previousData.IsDownButtonDown)
            {
                if (isDownButtonDown)
                {
                    OnButtonDown(LogitechF710Button.Down);
                    OnButtonPressed(LogitechF710Button.Down);

                    _downButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(LogitechF710Button.Down, _repeatDelayMilliseconds, _repeatRateMilliseconds, _downButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _downButtonPressedSource.Cancel();
                    OnButtonUp(LogitechF710Button.Down);
                }
            }

            if (_previousData == null || isLeftButtonDown != _previousData.IsLeftButtonDown)
            {
                if (isLeftButtonDown)
                {
                    OnButtonDown(LogitechF710Button.Left);
                    OnButtonPressed(LogitechF710Button.Left);

                    _leftButtonPressedSource = new CancellationTokenSource();
                    RepeatButtonPressed(LogitechF710Button.Left, _repeatDelayMilliseconds, _repeatRateMilliseconds, _leftButtonPressedSource.Token);
                }
                else if (_previousData != null)
                {
                    _leftButtonPressedSource.Cancel();
                    OnButtonUp(LogitechF710Button.Left);
                }
            }

            if (_previousData != null)
            { 
                if (leftThumbstickHorizontal != _previousData.LeftThumbstickHorizontal ||
                    leftThumbstickVertical != _previousData.LeftThumbstickVertical)
                {
                    OnThumbstickChanged(LogitechF710Thumbstick.Left, leftThumbstickHorizontal, leftThumbstickVertical);
                }

                if (rightThumbstickHorizontal != _previousData.RightThumbstickHorizontal ||
                    rightThumbstickVertical != _previousData.RightThumbstickVertical)
                {
                    OnThumbstickChanged(LogitechF710Thumbstick.Right, rightThumbstickHorizontal, rightThumbstickVertical);
                }
            }

            _previousData = data;
        }

        /// <summary>
        /// Raises the input changed event.
        /// </summary>
        /// <param name="data">The input data.</param>
        protected virtual void OnInputChanged(LogitechF710InputData data)
        {
            InputChanged?.Invoke(this, new LogitechF710InputEventArgs(data));
        }

        /// <summary>
        /// Raises the button down event.
        /// </summary>
        /// <param name="button">The button.</param>
        protected virtual void OnButtonDown(LogitechF710Button button)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(LogitechF710));
            }

            ButtonDown?.Invoke(this, new LogitechF710ButtonEventArgs(button));
        }

        /// <summary>
        /// Raises the button up event.
        /// </summary>
        /// <param name="button">The button.</param>
        protected virtual void OnButtonUp(LogitechF710Button button)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(LogitechF710));
            }

            ButtonUp?.Invoke(this, new LogitechF710ButtonEventArgs(button));
        }

        /// <summary>
        /// Raises the button pressed event.
        /// </summary>
        /// <param name="button">The button.</param>
        protected virtual void OnButtonPressed(LogitechF710Button button)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(LogitechF710));
            }

            ButtonPressed?.Invoke(this, new LogitechF710ButtonEventArgs(button));
        }

        /// <summary>
        /// Raises the thumbstick changed event.
        /// </summary>
        /// <param name="thumbstick">The thumbstick.</param>
        /// <param name="horizontal">The position along the horizontal-axis.</param>
        /// <param name="vertical">The position along the vertical-axis.</param>
        protected virtual void OnThumbstickChanged(LogitechF710Thumbstick thumbstick, long horizontal, long vertical)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(LogitechF710));
            }

            ThumbstickChanged?.Invoke(this, new LogitechF710ThumbstickEventArgs(thumbstick, horizontal, vertical));
        }

        private void RepeatButtonPressed(LogitechF710Button button, int repeatDelay, int repeatRate, CancellationToken token)
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

        /// <summary>
        /// Releases resources used by the USB-HID interface to Logitech F710.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases resources used by the USB-HID interface to Logitech F710.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _device.InputReportReceived -= DeviceInputReportReceived;
                _device.Dispose();
                
                if (_xButtonPressedSource != null)
                {
                    _xButtonPressedSource.Cancel();
                    _xButtonPressedSource.Dispose();
                }

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

                if (_leftTriggerButtonPressedSource != null)
                {
                    _leftTriggerButtonPressedSource.Cancel();
                    _leftTriggerButtonPressedSource.Dispose();
                }

                if (_rightTriggerButtonPressedSource != null)
                {
                    _rightTriggerButtonPressedSource.Cancel();
                    _rightTriggerButtonPressedSource.Dispose();
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

            _disposed = true;
        }
    }
}

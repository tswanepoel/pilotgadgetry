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
    using Windows.Devices.I2c;
    using Windows.Storage;

    public class XeoxProGamepad : IDisposable
    {
        private const ushort UsagePage = 0x0001;
        private const ushort UsageId = 0x0004;
        private const ushort VendorId = 0x054c;
        private const ushort ProductId = 0x0268;

        private HidDevice _device;
        private HidInputReport _report;
        private AutoResetEvent _reportEvent = new AutoResetEvent(false);
        private bool _disposed;

        public XeoxProGamepad()
        {
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
            _report = e.Report;
            _reportEvent.Set();
        }

        public XeoxProGamepadInputData Read()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(XeoxProGamepad));
            }

            _reportEvent.WaitOne();
            HidInputReport report = _report;
            
            bool isSelect = false;
            bool isL3 = false;
            bool isR3 = false;
            bool isStart = false;
            bool isUp = false;
            bool isRight = false;
            bool isDown = false;
            bool isLeft = false;
            bool isLT = false;
            bool isRT = false;
            bool isLB = false;
            bool isRB = false;
            bool isTriangle = false;
            bool isCircle = false;
            bool isCross = false;
            bool isSquare = false;
            bool isHome = false;

            foreach (HidBooleanControl control in report.ActivatedBooleanControls)
            {
                if (!Enum.IsDefined(typeof(XeoxProGamepadButtonUsage), control.UsageId))
                {
                    continue;
                }

                var usage = (XeoxProGamepadButtonUsage)control.UsageId;

                switch (usage)
                {
                    case XeoxProGamepadButtonUsage.Select:
                        isSelect = control.IsActive;
                        break;

                    case XeoxProGamepadButtonUsage.L3:
                        isL3 = control.IsActive;
                        break;

                    case XeoxProGamepadButtonUsage.R3:
                        isR3 = control.IsActive;
                        break;

                    case XeoxProGamepadButtonUsage.Start:
                        isStart = control.IsActive;
                        break;

                    case XeoxProGamepadButtonUsage.Up:
                        isUp = control.IsActive;
                        break;

                    case XeoxProGamepadButtonUsage.Right:
                        isRight = control.IsActive;
                        break;

                    case XeoxProGamepadButtonUsage.Down:
                        isDown = control.IsActive;
                        break;

                    case XeoxProGamepadButtonUsage.Left:
                        isLeft = control.IsActive;
                        break;

                    case XeoxProGamepadButtonUsage.LT:
                        isLT = control.IsActive;
                        break;

                    case XeoxProGamepadButtonUsage.RT:
                        isRT = control.IsActive;
                        break;

                    case XeoxProGamepadButtonUsage.LB:
                        isLB = control.IsActive;
                        break;

                    case XeoxProGamepadButtonUsage.RB:
                        isRB = control.IsActive;
                        break;

                    case XeoxProGamepadButtonUsage.Triangle:
                        isTriangle = control.IsActive;
                        break;

                    case XeoxProGamepadButtonUsage.Circle:
                        isCircle = control.IsActive;
                        break;

                    case XeoxProGamepadButtonUsage.Cross:
                        isCross = control.IsActive;
                        break;

                    case XeoxProGamepadButtonUsage.Square:
                        isSquare = control.IsActive;
                        break;

                    case XeoxProGamepadButtonUsage.Home:
                        isHome = control.IsActive;
                        break;
                }
            }

            const ushort usagePageGeneric = 0x01/*Generic*/;

            long lX = report.GetNumericControl(usagePageGeneric, (ushort)XeoxProGamepadGenericUsage.LX).Value;
            long lY = report.GetNumericControl(usagePageGeneric, (ushort)XeoxProGamepadGenericUsage.LY).Value;
            long rX = report.GetNumericControl(usagePageGeneric, (ushort)XeoxProGamepadGenericUsage.RX).Value;
            long rY = report.GetNumericControl(usagePageGeneric, (ushort)XeoxProGamepadGenericUsage.RY).Value;

            return new XeoxProGamepadInputData(isSelect, isL3, isR3, isStart, isUp, isRight, isDown, isLeft, 
                isLT, isRT, isLB, isRB, isTriangle, isCircle, isCross, isSquare, isHome, lX, lY, rX, rY);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                _device.InputReportReceived -= DeviceInputReportReceived;
                _device.Dispose();

                _disposed = true;
            }
        }
    }
}

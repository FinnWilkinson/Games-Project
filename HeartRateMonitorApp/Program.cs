using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace HeartRateMonitorApp
{
    enum ContactSensorStatus
    {
        NotSupported,
        NotSupported2,
        NoContact,
        Contact
    }

    class Program
    {
        private const int _heartRateMeasurementCharacteristicID = 0x2A37;
        public bool IsDisposed => _isDisposed;
        private static GattDeviceService _service;
        private static object _disposeSync = new object();
        private static bool _isDisposed;

        public static event HeartRateUpdateEventHandler HeartRateUpdated;

        public delegate void HeartRateUpdateEventHandler(ContactSensorStatus status, int bpm);

        static void Main(string[] args)
        {
            var heartRateSelector = GattDeviceService.GetDeviceSelectorFromUuid(GattServiceUuids.HeartRate);
            var devices = AsyncResult(DeviceInformation.FindAllAsync(heartRateSelector));
            var device = devices.FirstOrDefault();

            if (device == null)
            {
                throw new ArgumentNullException(nameof(device), "Unable to locate heart rate device");
            }

            GattDeviceService service;
            Cleanup();
            service = AsyncResult(GattDeviceService.FromIdAsync(device.Id));
            _service = service;

            var heartRate = service.GetCharacteristics(GattDeviceService.ConvertShortIdToUuid(_heartRateMeasurementCharacteristicID)).FirstOrDefault();

            if (heartRate == null)
            {
                throw new ArgumentOutOfRangeException($"Unable to locate heart ratte measurement on device {device.Name} ({device.Id}).");
            }

            var status = AsyncResult(heartRate.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify));
            heartRate.ValueChanged += HeartRate_ValueChanged;
            Debug.WriteLine($"Started {status}");

            Console.ReadLine();
        }

        public static void HeartRate_ValueChanged(
            GattCharacteristic sender,
            GattValueChangedEventArgs args)
        {
            var value = args.CharacteristicValue;

            if (value.Length == 0)
            {
                return;
            }

            using (var reader = DataReader.FromBuffer(value))
            {
                var bpm = -1;
                var flags = reader.ReadByte();
                var isshort = (flags & 1) == 1;
                var contactSensor = (ContactSensorStatus)((flags >> 1) & 3);
                var minLength = isshort ? 3 : 2;

                if (value.Length < minLength)
                {
                    Debug.WriteLine($"Buffer was too small. Got {value.Length}, expected {minLength}.");
                    return;
                }

                if (value.Length > 1)
                {
                    bpm = isshort
                        ? reader.ReadUInt16()
                        : reader.ReadByte();
                }

                Debug.WriteLine($"Read {flags:X} {contactSensor} {bpm}");

                HeartRateUpdated?.Invoke(contactSensor, bpm);

                Console.WriteLine(bpm);
            }
        }

        private static T AsyncResult<T>(IAsyncOperation<T> async)
        {
            while (true)
            {
                switch (async.Status)
                {
                    case AsyncStatus.Started:
                        Thread.Sleep(100);
                        continue;
                    case AsyncStatus.Completed:
                        return async.GetResults();
                    case AsyncStatus.Error:
                        throw async.ErrorCode;
                    case AsyncStatus.Canceled:
                        throw new TaskCanceledException();
                }
            }
        }

        public static void Cleanup()
        {
            var service = Interlocked.Exchange(ref _service, null);

            if (service == null)
            {
                return;
            }

            try
            {
                service.Dispose();
            }
            catch { }
        }

        public static void Dispose()
        {
            lock (_disposeSync)
            {
                _isDisposed = true;

                Cleanup();
            }
        }

    }
}

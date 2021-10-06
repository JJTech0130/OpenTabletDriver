using OpenTabletDriver.Plugin.Tablet;
using OpenTabletDriver.Tablet;

namespace OpenTabletDriver.Vendors.Wacom.IntuosLegacy
{
    public class IntuosLegacyReportParser : IReportParser<IDeviceReport>
    {
        public virtual IDeviceReport Parse(byte[] report)
        {
            return report[0] switch
            {
                0x02 => GetToolReport(report),
                _ => new DeviceReport(report)
            };
        }

        private IDeviceReport GetToolReport(byte[] report)
        {
            bool IsBitSet(byte b, int pos)
            {
                return (b & (1 << pos)) != 0;
            }

            if (!IsBitSet(report[1],4))
                return new DeviceReport(report);

            if (IsBitSet(report[1],6))
                return new DeviceReport(report);

            return new IntuosLegacyTabletReport(report);
        }
    }
} 
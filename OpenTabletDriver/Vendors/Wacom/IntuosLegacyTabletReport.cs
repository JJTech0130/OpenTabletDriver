using System.Numerics;
using System.Runtime.CompilerServices;
using OpenTabletDriver.Plugin.Tablet;

namespace OpenTabletDriver.Vendors.Wacom.IntuosLegacy
{
    public struct IntuosLegacyTabletReport : ITabletReport, IEraserReport
    {
        public IntuosLegacyTabletReport(byte[] report)
        {
            bool IsBitSet(byte b, int pos)
            {
                return (b & (1 << pos)) != 0;
            }

            Raw = report;
            ReportID = 0;
            Position = new Vector2
            {
                X = Unsafe.ReadUnaligned<ushort>(ref report[2]),
                Y = Unsafe.ReadUnaligned<ushort>(ref report[4])
            };
            Pressure = Unsafe.ReadUnaligned<ushort>(ref report[6]);

            PenButtons = new bool[]
            {
                IsBitSet(report[1],1),
                IsBitSet(report[1],2)
            };
            Eraser = IsBitSet(report[1],5);
        }

        public byte[] Raw { set; get; }
        public uint ReportID { set; get; }
        public Vector2 Position { set; get; }
        public uint Pressure { set; get; }
        public bool[] PenButtons { set; get; }
        public bool Eraser { set; get; }
    }
} 
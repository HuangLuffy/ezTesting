using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Util.ComBus
{
    public static class Operations
    {
        public static byte[] WriteDo(int addr, int io, bool openClose)
        {
            var src = new byte[8];
            src[0] = (byte)addr;
            src[1] = 0x05;
            src[2] = 0x00;
            src[3] = (byte)io;
            src[4] = (byte)((openClose) ? 0xff : 0x00);
            src[5] = 0x00;
            var crc = CalcRTU.CalculateCrc(src, 6);
            src[6] = (byte)(crc & 0xff);
            src[7] = (byte)(crc >> 8);
            return src;
        }
        public static byte[] ReadDo(int addr, int doNum)
        {
            var src = new byte[8];
            src[0] = (byte)addr;
            src[1] = 0x01;
            src[2] = 0x00;
            src[3] = 0x00;
            src[4] = 0x00;
            src[5] = (byte)doNum;
            var crc = CalcRTU.CalculateCrc(src, 6);
            src[6] = (byte)(crc & 0xff);
            src[7] = (byte)(crc >> 8);
            return src;
        }
    }
}

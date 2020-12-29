using System;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace CommonLib.Util.ComBus
{
    public class Com
    {
        private static SerialPort _comm = new SerialPort();
        private int _addr = 254;
        public void Load()
        {
            var comPorts = SerialPort.GetPortNames();
            Array.Sort(comPorts);

            if (_comm.IsOpen)
            {
                _comm.Close();
            }
            else
            {
                OpenSerialPort();
            }
        }
        public void OpenSerialPort()
        {
            //关闭时点击，则设置好端口，波特率后打开
            try
            {
                _comm.PortName = "COM3"; //串口名 COM1
                _comm.BaudRate = 9600; //波特率  9600
                _comm.DataBits = 8; // 数据位 8
                _comm.ReadBufferSize = 4096;
                _comm.StopBits = StopBits.One;
                _comm.Parity = Parity.None;
                _comm.Open();
            }
            catch (Exception e)
            {
                //_comm = new SerialPort();
                throw new Exception($"Failed to Open Serial Port. {e.Message}.");
                //MessageBox.Show(ex.Message);
                //return false;
            }
            //return true;
        }
        private void OpenDo(int io)
        {
            var info = Operations.WriteDo(Convert.ToInt16(_addr), io - 1, true);
            var rst = SendInfo(info);

        }
        private void CloseDo(int io)
        {
            var info = Operations.WriteDo(Convert.ToInt16(_addr), io - 1, false);
            var rst = SendInfo(info);
        }
        int errrcvcnt = 0;
        private byte[] SendInfo(byte[] info)
        {
            if (_comm == null)
            {
                _comm = new SerialPort();
                return null;
            }

            if (_comm.IsOpen == false)
            {
                OpenSerialPort();
                return null;
            }
            try
            {
                var data = new byte[2048];
                var len = 0;
                _comm.Write(info, 0, info.Length);
                try
                {
                    Thread.Sleep(50);
                    var ns = _comm.BaseStream;
                    ns.ReadTimeout = 50;
                    len = ns.Read(data, 0, 2048);
                    //DebugInfo("接收", data, len);
                }
                catch (Exception)
                {
                    return null;
                }
                errrcvcnt = 0;
                return AnalysisRcv(data, len);
            }
            catch (Exception)
            {

            }
            return null;
        }
        private byte[] AnalysisRcv(byte[] src, int len)
        {
            if (len < 6) return null;
            if (src[0] != Convert.ToInt16(_addr)) return null;

            switch (src[1])
            {
                case 0x01:
                    if (CalcRTU.CalculateCrc(src, src[2] + 5) == 0x00)
                    {
                        byte[] dst = new byte[src[2]];
                        for (int i = 0; i < src[2]; i++)
                            dst[i] = src[3 + i];
                        return dst;
                    }
                    break;
                case 0x02:
                    if (CalcRTU.CalculateCrc(src, src[2] + 5) == 0x00)
                    {
                        var dst = new byte[src[2]];
                        for (var i = 0; i < src[2]; i++)
                            dst[i] = src[3 + i];
                        return dst;
                    }
                    break;
                case 0x04:
                    if (CalcRTU.CalculateCrc(src, src[2] + 5) == 0x00)
                    {
                        var dst = new byte[src[2]];
                        for (var i = 0; i < src[2]; i++)
                            dst[i] = src[3 + i];
                        return dst;
                    }
                    break;
                case 0x05:
                    if (CalcRTU.CalculateCrc(src, 8) == 0x00)
                    {
                        var dst = new byte[1];
                        dst[0] = src[4];
                        return dst;
                    }
                    break;
                case 0x0f:
                    if (CalcRTU.CalculateCrc(src, 8) == 0x00)
                    {
                        var dst = new byte[1];
                        dst[0] = 1;
                        return dst;
                    }
                    break;
            }
            return null;
        }
    }
}

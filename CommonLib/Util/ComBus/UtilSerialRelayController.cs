using System;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace CommonLib.Util.ComBus
{
    public class UtilSerialRelayController
    {
        private static SerialPort _comm = new SerialPort();
        private readonly int _addr = 254;
        private string _workablePortName = "";
        public void Load()
        {
            var comPorts = SerialPort.GetPortNames();
            Array.Sort(comPorts);
            foreach (var portName in comPorts)
            {
                try
                {
                    OpenSerialPort(portName);
                }
                catch (Exception)
                {
                    //ignored.
                }
                if (!_workablePortName.Equals(""))
                {
                    return;
                }
            }
            throw new Exception($"No any Relay Controller found.");
        }
        public void OpenSerialPort(string portName = null)
        {
            portName = portName ?? _workablePortName;
            //关闭时点击，则设置好端口，波特率后打开
            try
            {
                if (_comm.IsOpen)
                {
                    _comm.Close();
                }
                _comm.PortName = portName; //串口名 COM1
                _comm.BaudRate = 9600; //波特率  9600
                _comm.DataBits = 8; // 数据位 8
                _comm.ReadBufferSize = 4096;
                _comm.StopBits = StopBits.One;
                _comm.Parity = Parity.None;
                _comm.Open();
                var info = Operations.ReadDo(Convert.ToInt16(_addr), Convert.ToInt16(1));
                var rst = SendInfo(info);
                if (rst == null)
                {
                    throw new Exception($"No any Relay Controller found. {_comm.PortName}.");
                }
                _workablePortName = portName;
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



        public void SendMockKeys(int io)
        {
            OpenDo(3);
            OpenDo(11);
            CloseDo(11);
            CloseDo(3);
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

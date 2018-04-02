using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Util.vm
{
    public interface IVmControl
    {
        void SetVM(string vmxFullPath);
        void PowerOn(int waitSecond = 10);
        void PowerOff(int waitSecond = 10);
        void RevertToSnapshot(string snapshotName);
        void TakeSnapshot(string snapshotName);
        bool IsSnapshotExisting(string snapshotName);
    }
}

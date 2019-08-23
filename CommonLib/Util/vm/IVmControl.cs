namespace CommonLib.Util.vm
{
    public interface IVmControl
    {
        void SetVm(string vmxFullPath);
        void PowerOn(int waitSeconds = 10);
        void PowerOff(int waitSeconds = 10);
        void RevertToSnapshot(string snapshotName);
        void TakeSnapshot(string snapshotName);
        bool IsSnapshotExisting(string snapshotName);
    }
}

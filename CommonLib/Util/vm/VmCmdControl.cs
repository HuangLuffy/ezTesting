using System;
using System.Diagnostics;
using System.Linq;

namespace CommonLib.Util.vm
{
    public class VmCmdControl : Vm, IVmControl
    {
        private string _vmxFullPath;
        public VmCmdControl()
        {

        }
        public VmCmdControl(string vmxFullPath)
        {
            _vmxFullPath = vmxFullPath;
        }
        public void SetVM(string vmxFullPath)
        {
            try
            {
                _vmxFullPath = vmxFullPath;
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage($"Failed to set VM [{vmxFullPath}].", new StackFrame(0).GetMethod().Name, ex.Message);
            }   
        }

        public bool IsSnapshotExisting(string snapshotName)
        {
            try
            {
                var snapshots = UtilProcess.StartProcessGetStrings(vmrunInstallFullPath,
                    $"listSnapshots \"{_vmxFullPath}\"");
                //foreach (var snapshot in snapshots)
                //{
                //    if (snapshot.Equals(snapshotName))
                //    {
                //        return true;
                //    }
                //}
                //return false;
                return snapshots.Any(snapshot => snapshot.Equals(snapshotName));
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage($"Failed to find Snapshot [{_vmxFullPath}] from VM [{snapshotName}].", new StackFrame(0).GetMethod().Name, ex.Message);
                return false;
            }
        }
        public bool IsVmRunning(string vmxFullPath)
        {
            try
            {
                var runningVMs = UtilProcess.StartProcessGetStrings(vmrunInstallFullPath, "list");
                return runningVMs.Any(vm => vm.Equals(vmxFullPath));
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to get if {vmxFullPath} was running.", new StackFrame(0).GetMethod().Name, ex.Message);
                return false;
            }
        }
        public void PowerOff(int waitSecond = 10)
        {
            try
            {
                if (!IsVmRunning(_vmxFullPath)) return;
                UtilProcess.StartProcessGetString(vmrunInstallFullPath,
                    $"-T ws stop \"{_vmxFullPath}\"");
                UtilTime.WaitTime(waitSecond);
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage($"Failed to power off VM [{_vmxFullPath}]", new StackFrame(0).GetMethod().Name, ex.Message);
            }
        }

        public void PowerOn(int waitSecond = 10)
        {
            try
            {
                var result = UtilProcess.StartProcessGetString(vmrunInstallFullPath,
                    $"-T ws start \"{_vmxFullPath}\"");
                if (!result.Equals(""))
                {
                    throw new Exception(result);       
                }
                UtilTime.WaitTime(waitSecond);
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage($"Failed to power on VM [{_vmxFullPath}].", new StackFrame(0).GetMethod().Name, ex.Message);
            }     
        }

        public void RevertToSnapshot(string revertSnapshotName)
        {
            try
            {
                var result = UtilProcess.StartProcessGetString(vmrunInstallFullPath,
                    $"revertToSnapshot \"{_vmxFullPath}\" \"{revertSnapshotName}\"");
                if (!result.Equals(""))
                {
                    throw new Exception(result);
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage($"Failed to revert Snapshot [{revertSnapshotName}] of VM [{_vmxFullPath}].", new StackFrame(0).GetMethod().Name, ex.Message);
            }
        }

        public void TakeSnapshot(string takeSnapshotName)
        {
            try
            {
                if (takeSnapshotName.Trim().Equals(""))
                {
                    throw new Exception("Snapshot was invalid.");
                }
                var result = UtilProcess.StartProcessGetString(vmrunInstallFullPath,
                    $"snapshot \"{_vmxFullPath}\" \"{takeSnapshotName}\"");
                if (!result.Equals(""))
                {
                    throw new Exception(result);
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage($"Failed to take Snapshot [{takeSnapshotName}] of VM [{_vmxFullPath}].", new StackFrame(0).GetMethod().Name, ex.Message);
            }
        }
    }
}

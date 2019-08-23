using System;
using System.Diagnostics;

namespace CommonLib.Util.vm
{
    public class VmCmdControl : Vm, IVmControl
    {
        public string vmxFullPath;
        public VmCmdControl()
        {

        }
        public VmCmdControl(string vmxFullPath)
        {
            this.vmxFullPath = vmxFullPath;
        }
        public void SetVM(string vmxFullPath)
        {
            try
            {
                this.vmxFullPath = vmxFullPath;
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
                string[] snapshots = UtilProcess.StartProcessGetStrings(vmrunInstallFullPath,
                    $"listSnapshots \"{vmxFullPath}\"");
                foreach (string snapshot in snapshots)
                {
                    if (snapshot.Equals(snapshotName))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage($"Failed to find Snapshot [{vmxFullPath}] from VM [{snapshotName}].", new StackFrame(0).GetMethod().Name, ex.Message);
                return false;
            }
        }
        public bool IsVmRunning(string vmxFullPath)
        {
            try
            {
                string[] runningVMs = UtilProcess.StartProcessGetStrings(vmrunInstallFullPath, "list");
                foreach (string vm in runningVMs)
                {
                    if (vm.Equals(vmxFullPath))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to get if {vmxFullPath} was running.", new StackFrame(0).GetMethod().Name, ex.Message);
                return true;
            }
        }
        public void PowerOff(int waitSecond = 10)
        {
            try
            {
                if (IsVmRunning(vmxFullPath))
                {
                    string strlist = UtilProcess.StartProcessGetString(vmrunInstallFullPath,
                        $"-T ws stop \"{vmxFullPath}\"");
                    UtilTime.WaitTime(waitSecond);
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage($"Failed to power off VM [{vmxFullPath}]", new StackFrame(0).GetMethod().Name, ex.Message);
            }
        }

        public void PowerOn(int waitSecond = 10)
        {
            try
            {
                string result = UtilProcess.StartProcessGetString(vmrunInstallFullPath,
                    $"-T ws start \"{vmxFullPath}\"");
                if (!result.Equals(""))
                {
                    throw new Exception(result);       
                }
                UtilTime.WaitTime(waitSecond);
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage($"Failed to power on VM [{vmxFullPath}].", new StackFrame(0).GetMethod().Name, ex.Message);
            }     
        }

        public void RevertToSnapshot(string revertSnapshotName)
        {
            try
            {
                string result = UtilProcess.StartProcessGetString(vmrunInstallFullPath,
                    $"revertToSnapshot \"{vmxFullPath}\" \"{revertSnapshotName}\"");
                if (!result.Equals(""))
                {
                    throw new Exception(result);
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage($"Failed to revert Snapshot [{revertSnapshotName}] of VM [{vmxFullPath}].", new StackFrame(0).GetMethod().Name, ex.Message);
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
                string result = UtilProcess.StartProcessGetString(vmrunInstallFullPath,
                    $"snapshot \"{vmxFullPath}\" \"{takeSnapshotName}\"");
                if (!result.Equals(""))
                {
                    throw new Exception(result);
                }
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage($"Failed to take Snapshot [{takeSnapshotName}] of VM [{vmxFullPath}].", new StackFrame(0).GetMethod().Name, ex.Message);
            }
        }
    }
}

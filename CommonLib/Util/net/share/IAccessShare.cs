namespace CommonLib.Util.net.share
{
    public interface IAccessShare
    {
        bool GetConnectState();
        void ConnectShare(bool isPersistent = true);
        string GetShareFolderFullPath();
    }
}

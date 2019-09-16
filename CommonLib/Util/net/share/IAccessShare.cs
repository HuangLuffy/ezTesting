namespace CommonLib.Util.Net.Share
{
    public interface IAccessShare
    {
        bool GetConnectState();
        void ConnectShare(bool isPersistent = true);
        string GetShareFolderFullPath();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Util.net.share
{
    public interface IAccessShare
    {
        bool GetConnectState();
        void ConnectShare(bool isPersistent = true);
        string GetShareFolderFullPath();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt.Project
{
    public abstract class AbsSWObj
    {
        public virtual string Name_MainWidow { get; }
        public virtual string ClassName_MainWidow { get; }
        public virtual string Name_CrashMainWidow { get; }
        public virtual string Button_CloseMainWindow { get; }
    }
}

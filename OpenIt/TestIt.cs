using ATLib;
using CommonLib.Util;
using OpenIt.Project.Portal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIt
{
    public class TestIt
    {
        PortalTestFlows _PortalTestFlows = new PortalTestFlows();
        Cmd _CMD = new Cmd();
        public void Run()
        {
            string[] selected = _CMD.WriteOptions(_PortalTestFlows.Options_Cmd);
            while (true)
            {
                try
                {
                    string s = Console.ReadLine();
                    if (s.Trim().Equals("1"))
                    {
                        _PortalTestFlows.Flow_LaunchTest();
                        break;
                    }
                    else if (s.Trim().Equals("2"))
                    {
                        _CMD.WriteOptions(_PortalTestFlows.Options_Cmd);
                        break;
                    }
                    else if (s.Trim().Equals("3"))
                    {
                        _CMD.WriteOptions(_PortalTestFlows.Options_Cmd);
                        break;
                    }
                    else if (s.Trim().Equals("4"))
                    {
                        _CMD.WriteOptions(_PortalTestFlows.Options_Cmd);
                    }
                }
                catch (Exception ex)
                {
                    Console.Title = ex.Message;
                    return;
                }
                Console.Title += " >>>>> Tested Done! PASS";
            }
        }
    }
}

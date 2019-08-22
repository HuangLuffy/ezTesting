using CommonLib.Util.net.share;
using System;
using System.IO;
using System.Management;

namespace CommonLib.Util.net
{
    public class ManagementScopeAccessShare : AccessShare , IAccessShare
    {
        public ManagementScopeAccessShare(string shareFolderFullPath, string userName = "", string password = "") : base(shareFolderFullPath, userName, password)
        {

        }
        public void test1()
        {
            try
            {
                string HostName = "localhost";
                ManagementScope Scope;

                if (!HostName.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                {
                    ConnectionOptions Conn = new ConnectionOptions();
                    Conn.Username = "";
                    Conn.Password = "";
                    Conn.Authority = "ntlmdomain:DOMAIN";
                    Scope = new ManagementScope(String.Format("\\\\{0}\\root\\CIMV2", HostName), Conn);
                }
                else
                    Scope = new ManagementScope(String.Format("\\\\{0}\\root\\CIMV2", HostName), null);

                Scope.Connect();
                string Drive = "c:";
                //look how the \ char is escaped. 
                string Path = "\\\\Windows\\\\System32\\\\";
                ObjectQuery Query = new ObjectQuery(string.Format("SELECT * FROM CIM_DataFile Where Drive='{0}' AND Path='{1}' ", Drive, Path));

                ManagementObjectSearcher Searcher = new ManagementObjectSearcher(Scope, Query);

                foreach (ManagementObject WmiObject in Searcher.Get())
                {
                    Console.WriteLine("{0}", (string)WmiObject["Name"]);// String
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(String.Format("Exception {0} Trace {1}", e.Message, e.StackTrace));
            }
            Console.WriteLine("Press Enter to exit");
            Console.Read();
        }
        public void GetShareFolderProperties()
        {
            ManagementClass _ManagementClass = null;
            ConnectionOptions _ConnectionOptions = new ConnectionOptions();
            _ConnectionOptions.Username = this.UserName;
            _ConnectionOptions.Password = this.Password;
            DirectoryInfo di = new DirectoryInfo(this.ShareFolderFullPath);
            ManagementScope scope = new ManagementScope(string.Format(@"\\{0}\root\cimv2", this.IpOrName), _ConnectionOptions);
            scope.Connect();
            
            try
            {
                DirectoryInfo di1 = new DirectoryInfo(this.ShareFolderFullPath);
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception("Access Denied, wrong username or password.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_ManagementClass != null)
                    _ManagementClass.Dispose();
            }
        }
        public void GetShareFolderProperties1()
        {
            ManagementPath _ManagementPath = new ManagementPath(string.Format(@"\\{0}\root\cimv2", this.IpOrName));
             ManagementClass _ManagementClass = null;
            ConnectionOptions _ConnectionOptions = new ConnectionOptions();
            _ConnectionOptions.Username = this.UserName;
            _ConnectionOptions.Password = this.Password;
            // co.Authority = "kerberos:celeb"; // use kerberos authentication
            // co.Authority = "NTLMDOMAIN:celeb"; // or NTLM
            //_ManagementPath.Server = @"\\10.10.53.26"; // use . for local server, else server name
            //_ManagementPath.NamespacePath = @"root\\CIMV2";
            _ManagementPath.RelativePath = @"Win32_Share";
            ManagementScope scope = new ManagementScope(_ManagementPath, _ConnectionOptions); // use (path) for local binds
            ObjectGetOptions options = new ObjectGetOptions(null, new TimeSpan(0, 0, 0, 5), true);
            try
            {
                _ManagementClass = new ManagementClass(scope, _ManagementPath, options);
                ManagementObjectCollection moc = _ManagementClass.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    this._ShareFolderProperties = new ShareFolderProperties();
                    this._ShareFolderProperties.Name = mo["Name"].ToString();
                    this._ShareFolderProperties.ParentPath = string.Format(@"\\{0}", this.IpOrName);
                    this._ShareFolderProperties.LocalPath = mo["Path"].ToString();
                    this._ShareFolderProperties.FullPath = Path.Combine(this._ShareFolderProperties.ParentPath, this._ShareFolderProperties.Name);
                    this._ShareFolderPropertiesList.Add(this._ShareFolderProperties);
                    if (this._ShareFolderProperties.Name.Equals("TP_GhostShare"))
                    {
                        Console.WriteLine("{0}", (string)mo["Caption"]);
                
                        Console.WriteLine("{0}", (string)mo["DriveType"]);
                        Console.WriteLine("{0}", (string)mo["FileSystem"]);
                        Console.WriteLine("{0}", (string)mo["PNPDeviceID"]);
                        Console.WriteLine("{0}", (string)mo["Status"]);
                    }
                }
                //Console.WriteLine("{0} - {1} - {2} ", mo["Name"],
                //    mo["Description"],
                //    mo["Path"]);
                ObjectQuery Query = new ObjectQuery(string.Format("SELECT * FROM CIM_DataFile Where Drive='{0}' AND Path='{1}' ", "D:", "\\\\TP_Test\\\\TP_GhostShare\\\\"));

                ManagementObjectSearcher Searcher = new ManagementObjectSearcher(scope, Query);

                foreach (ManagementObject WmiObject in Searcher.Get())
                {
                    Console.WriteLine("{0}", (string)WmiObject["Name"]);// String
                }
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception("Access Denied, wrong username or password.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (_ManagementClass != null)
                    _ManagementClass.Dispose();
            }
        }

        //string userdesktop = @"c:\Users\" + user + @"\Desktop";
        //string hdrivepath = @"\\dist-win-file-3\homes\" + user;
        //string SourcePath = userdesktop;
        //string DestinationPath = hdrivepath;
        //DirectoryCopy(computer, user, pass, SourcePath, DestinationPath, true);
        public void DirectoryCopy(string computer, string user, string pass, string SourcePath, string DestinationPath, bool Recursive)
        {
            try
            {
                ConnectionOptions connection = new ConnectionOptions();
                connection.Username = user;
                connection.Password = pass;
                connection.Impersonation = ImpersonationLevel.Impersonate;
                connection.EnablePrivileges = true;
                ManagementScope scope = new ManagementScope(
                    @"\\" + computer + @"\root\CIMV2", connection);
                scope.Connect();
                ManagementPath managementPath = new ManagementPath(@"Win32_Directory.Name='" + SourcePath + "'");
                ManagementObject classInstance = new ManagementObject(scope, managementPath, null);
                // Obtain in-parameters for the method
                ManagementBaseObject inParams = classInstance.GetMethodParameters("CopyEx");
                // Add the input parameters.
                inParams["FileName"] = DestinationPath.Replace("\\", "\\\\");
                inParams["Recursive"] = true;
                inParams["StartFileName"] = null;

                // Execute the method and obtain the return values.
                ManagementBaseObject outParams =
                    classInstance.InvokeMethod("CopyEx", inParams, null);
                // List outParams
                Console.WriteLine((outParams["ReturnValue"]).ToString());
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception("Access Denied, wrong username or password.");
            }

            catch (ManagementException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void test()
        {
            try
            {
                ConnectionOptions _ConnectionOptions = new ConnectionOptions();
                _ConnectionOptions.Impersonation = ImpersonationLevel.Impersonate;
                _ConnectionOptions.Authentication = AuthenticationLevel.Packet;
                _ConnectionOptions.EnablePrivileges = true;
                _ConnectionOptions.Username = "cz";
                _ConnectionOptions.Password = "111";
                //_ConnectionOptions.Authority = 
                //_ConnectionOptions.Authentication = AuthenticationLevel.PacketPrivacy;
                ManagementPath _ManagementPath = new ManagementPath(@"\\10.10.53.26\root\cimv2");
                ManagementScope Scope = new ManagementScope(_ManagementPath, _ConnectionOptions);
                Scope.Connect();
                //ObjectGetOptions _ObjectGetOptions = new ObjectGetOptions();
                //using (ManagementClass shares = new ManagementClass(@"\\10.10.53.26\root\cimv2", "Win32_Share", _ObjectGetOptions))
                //{
                //    foreach (ManagementObject share in shares.GetInstances())
                //    {
                //        Console.WriteLine(share["Name"]);
                //    }
                //}
                ObjectQuery Query = new ObjectQuery("SELECT * FROM Win32_LogicalShareSecuritySetting");
                ManagementObjectSearcher Searcher = new ManagementObjectSearcher(Scope, Query);
                ManagementObjectCollection QueryCollection = Searcher.Get();

                foreach (ManagementObject SharedFolder in QueryCollection)
                {
                    {
                        String ShareName = (String)SharedFolder["Name"];
                        String Caption = (String)SharedFolder["Caption"];
                        String LocalPath = String.Empty;
                        ManagementObjectSearcher Win32Share = new ManagementObjectSearcher("SELECT Path FROM Win32_share WHERE Name = '" + ShareName + "'");
                        foreach (ManagementObject ShareData in Win32Share.Get())
                        {
                            LocalPath = (String)ShareData["Path"];
                        }

                        ManagementBaseObject Method = SharedFolder.InvokeMethod("GetSecurityDescriptor", null, new InvokeMethodOptions());
                        ManagementBaseObject Descriptor = (ManagementBaseObject)Method["Descriptor"];
                        ManagementBaseObject[] DACL = (ManagementBaseObject[])Descriptor["DACL"];
                        foreach (ManagementBaseObject ACE in DACL)
                        {
                            ManagementBaseObject Trustee = (ManagementBaseObject)ACE["Trustee"];

                            // Full Access = 2032127, Modify = 1245631, Read Write = 118009, Read Only = 1179817
                            Console.WriteLine(ShareName);
                            Console.WriteLine(Caption);
                            Console.WriteLine(LocalPath);
                            Console.WriteLine((String)Trustee["Domain"]);
                            Console.WriteLine((String)Trustee["Name"]);
                            Console.WriteLine((UInt32)ACE["AccessMask"]);
                            Console.WriteLine((UInt32)ACE["AceType"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                //MessageBox.Show(ex.StackTrace, ex.Message);
            }
        }

        public void ConnectShare(bool isPersistent = true)
        {
            throw new NotImplementedException();
        }

        public string GetShareFolderFullPath()
        {
            throw new NotImplementedException();
        }
    } 
}

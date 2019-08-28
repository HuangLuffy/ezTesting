using System;
using System.IO;
using System.Management;

namespace CommonLib.Util.net.share
{
    public class ManagementScopeAccessShare : AccessShare , IAccessShare
    {
        public ManagementScopeAccessShare(string shareFolderFullPath, string userName = "", string password = "") : base(shareFolderFullPath, userName, password)
        {

        }
        public void Test1()
        {
            try
            {
                var hostName = "localhost";
                ManagementScope scope;

                if (!hostName.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                {
                    var conn = new ConnectionOptions {Username = "", Password = "", Authority = "ntlmdomain:DOMAIN"};
                    scope = new ManagementScope($"\\\\{hostName}\\root\\CIMV2", conn);
                }
                else
                    scope = new ManagementScope($"\\\\{hostName}\\root\\CIMV2", null);

                scope.Connect();
                var drive = "c:";
                //look how the \ char is escaped. 
                var path = "\\\\Windows\\\\System32\\\\";
                var query = new ObjectQuery(
                    $"SELECT * FROM CIM_DataFile Where Drive='{drive}' AND Path='{path}' ");

                var searcher = new ManagementObjectSearcher(scope, query);

                foreach (var wmiObject in searcher.Get())
                {
                    Console.WriteLine("{0}", (string)wmiObject["Name"]);// String
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception {e.Message} Trace {e.StackTrace}");
            }
            Console.WriteLine("Press Enter to exit");
            Console.Read();
        }
        public void GetShareFolderProperties()
        {
            ManagementClass managementClass = null;
            var connectionOptions = new ConnectionOptions {Username = UserName, Password = Password};
            var di = new DirectoryInfo(ShareFolderFullPath);
            var scope = new ManagementScope($@"\\{IpOrName}\root\cimv2", connectionOptions);
            scope.Connect();
            try
            {
                var di1 = new DirectoryInfo(ShareFolderFullPath);
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception("Access Denied, wrong username or password.");
            }
            finally
            {
                if (managementClass != null)
                    managementClass.Dispose();
            }
        }
        public void GetShareFolderProperties1()
        {
            var managementPath = new ManagementPath($@"\\{IpOrName}\root\cimv2");
             ManagementClass managementClass = null;
             var connectionOptions = new ConnectionOptions {Username = UserName, Password = Password};
             // co.Authority = "kerberos:celeb"; // use kerberos authentication
            // co.Authority = "NTLMDOMAIN:celeb"; // or NTLM
            //_ManagementPath.Server = @"\\10.10.53.26"; // use . for local server, else server name
            //_ManagementPath.NamespacePath = @"root\\CIMV2";
            managementPath.RelativePath = @"Win32_Share";
            ManagementScope scope = new ManagementScope(managementPath, connectionOptions); // use (path) for local binds
            ObjectGetOptions options = new ObjectGetOptions(null, new TimeSpan(0, 0, 0, 5), true);
            try
            {
                managementClass = new ManagementClass(scope, managementPath, options);
                ManagementObjectCollection moc = managementClass.GetInstances();
                foreach (var mo in moc)
                {
                    ShareFolderProperties = new ShareFolderProperties();
                    ShareFolderProperties.Name = mo["Name"].ToString();
                    ShareFolderProperties.ParentPath = $@"\\{IpOrName}";
                    ShareFolderProperties.LocalPath = mo["Path"].ToString();
                    ShareFolderProperties.FullPath = Path.Combine(ShareFolderProperties.ParentPath, ShareFolderProperties.Name);
                    ShareFolderPropertiesList.Add(ShareFolderProperties);
                    if (ShareFolderProperties.Name.Equals("TP_GhostShare"))
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
                var query = new ObjectQuery(
                    $"SELECT * FROM CIM_DataFile Where Drive='{"D:"}' AND Path='{"\\\\TP_Test\\\\TP_GhostShare\\\\"}' ");

                var searcher = new ManagementObjectSearcher(scope, query);

                foreach (var o in searcher.Get())
                {
                    var wmiObject = (ManagementObject) o;
                    Console.WriteLine("{0}", (string)wmiObject["Name"]);// String
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
                if (managementClass != null)
                    managementClass.Dispose();
            }
        }

        //string userdesktop = @"c:\Users\" + user + @"\Desktop";
        //string hdrivepath = @"\\dist-win-file-3\homes\" + user;
        //string SourcePath = userdesktop;
        //string DestinationPath = hdrivepath;
        //DirectoryCopy(computer, user, pass, SourcePath, DestinationPath, true);
        public void DirectoryCopy(string computer, string user, string pass, string sourcePath, string destinationPath, bool recursive)
        {
            try
            {
                var connection = new ConnectionOptions
                {
                    Username = user,
                    Password = pass,
                    Impersonation = ImpersonationLevel.Impersonate,
                    EnablePrivileges = true
                };
                var scope = new ManagementScope(
                    @"\\" + computer + @"\root\CIMV2", connection);
                scope.Connect();
                var managementPath = new ManagementPath(@"Win32_Directory.Name='" + sourcePath + "'");
                var classInstance = new ManagementObject(scope, managementPath, null);
                // Obtain in-parameters for the method
                var inParams = classInstance.GetMethodParameters("CopyEx");
                // Add the input parameters.
                inParams["FileName"] = destinationPath.Replace("\\", "\\\\");
                inParams["Recursive"] = true;
                inParams["StartFileName"] = null;

                // Execute the method and obtain the return values.
                var outParams =
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
        public void Test()
        {
            try
            {
                var connectionOptions = new ConnectionOptions
                {
                    Impersonation = ImpersonationLevel.Impersonate,
                    Authentication = AuthenticationLevel.Packet,
                    EnablePrivileges = true,
                    Username = "cz",
                    Password = "111"
                };
                //_ConnectionOptions.Authority = 
                //_ConnectionOptions.Authentication = AuthenticationLevel.PacketPrivacy;
                var managementPath = new ManagementPath(@"\\10.10.53.26\root\cimv2");
                var scope = new ManagementScope(managementPath, connectionOptions);
                scope.Connect();
                //ObjectGetOptions _ObjectGetOptions = new ObjectGetOptions();
                //using (ManagementClass shares = new ManagementClass(@"\\10.10.53.26\root\cimv2", "Win32_Share", _ObjectGetOptions))
                //{
                //    foreach (ManagementObject share in shares.GetInstances())
                //    {
                //        Console.WriteLine(share["Name"]);
                //    }
                //}
                var query = new ObjectQuery("SELECT * FROM Win32_LogicalShareSecuritySetting");
                var searcher = new ManagementObjectSearcher(scope, query);
                var queryCollection = searcher.Get();

                foreach (var o in queryCollection)
                {
                    var sharedFolder = (ManagementObject) o;
                    {
                        var shareName = (string)sharedFolder["Name"];
                        var caption = (string)sharedFolder["Caption"];
                        var localPath = string.Empty;
                        var win32Share = new ManagementObjectSearcher("SELECT Path FROM Win32_share WHERE Name = '" + shareName + "'");
                        foreach (var shareData in win32Share.Get())
                        {
                            localPath = (string)shareData["Path"];
                        }

                        var method = sharedFolder.InvokeMethod("GetSecurityDescriptor", null, new InvokeMethodOptions());
                        var descriptor = (ManagementBaseObject)method["Descriptor"];
                        var dacl = (ManagementBaseObject[])descriptor["DACL"];
                        foreach (var ace in dacl)
                        {
                            var trustee = (ManagementBaseObject)ace["Trustee"];

                            // Full Access = 2032127, Modify = 1245631, Read Write = 118009, Read Only = 1179817
                            Console.WriteLine(shareName);
                            Console.WriteLine(caption);
                            Console.WriteLine(localPath);
                            Console.WriteLine((string)trustee["Domain"]);
                            Console.WriteLine((string)trustee["Name"]);
                            Console.WriteLine((uint)ace["AccessMask"]);
                            Console.WriteLine((uint)ace["AceType"]);
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

using System;
using System.Diagnostics;

namespace CommonLib.Util.zip
{
    public class Zip7Z : IZip
    {
        public Zip7Z(string tool7Z)
        {
            Tool7Z = tool7Z;
        }

        private string Tool7Z { get; set; }

        public void ExtractZip(string source)
        {
            throw new NotImplementedException();
        }

        public void ExtractZip(string source, string destination)
        {
            //Shell(constPath7zEXE & " x """ & pathBuildZIP & "\" & fullBuildName & ".zip"" - y - o""" & pathATScript & "\" & strWhichVM & "\" & """")
            try
            {
                UtilProcess.StartProcessGetInt(Tool7Z, $"x \"{source}\" -y -o\"{destination}\"");
            }
            catch (Exception ex)
            {
                Logger.LogThrowMessage($"Failed to extract zip [{source}] to [{destination}].", new StackFrame(0).GetMethod().Name, ex.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib;
using Xunit;
using Xunit.Abstractions;
using XunitTest.ExecOrderers;
using XunitTest.Projects.TestPro.Cases;
using XunitTest.Handler;

namespace XunitTest.TestCases.TryX
{
    public class TryX
    {
        public class ShareInTryx : IDisposable
        {
            public string str = "a";
            public void Dispose()
            {
                str = string.Empty;
            }
        }

        [TestCaseOrderer("XunitTest.ExecOrderers.PriorityOrder", "XunitTest")]
        public class TestXunit : IClassFixture<ShareInTryx>
        {
            private readonly ShareInTryx _Share;
            private readonly ITestOutputHelper output;
            private Steps_VirtualBox _Steps_VirtualBox = new Steps_VirtualBox(@"D:\Dev\1\1.xml");
            public TestXunit(ITestOutputHelper output, ShareInTryx _Share)
            {
                this.output = output;
                this._Share = _Share;
            }
            public int Add(int a, int b)
            {
                return a + b;
            }
            [Fact, TestPriority(1)]
            public void ATestAdd2()
            {
                _Steps_VirtualBox.OpenVirtualBox();
                _Steps_VirtualBox.verifyIfVirtualBoxLaunchedSuccessfully();
            }
        }
    }
}

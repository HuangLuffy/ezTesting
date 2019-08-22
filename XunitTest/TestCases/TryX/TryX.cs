using System;
using Xunit;
using Xunit.Abstractions;
using XunitTest.ExecOrderers;
using XunitTest.Projects.TestPro.Cases;

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
            private readonly StepsVirtualBox _StepsVirtualBox = new StepsVirtualBox(@"D:\Dev\1\1.xml");
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
                _StepsVirtualBox.OpenVirtualBox();
                _StepsVirtualBox.verifyIfVirtualBoxLaunchedSuccessfully();
            }
        }
    }
}

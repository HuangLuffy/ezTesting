using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib;
using Xunit;
using Xunit.Abstractions;
using XunitTest.ExecOrderers;

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
            private TestStep testStep;
            public TestXunit(ITestOutputHelper output, ShareInTryx _Share)
            {
                this.output = output;
                this._Share = _Share;
                testStep = new TestStep();
            }
            public int Add(int a, int b)
            {
                return a + b;
            }
            [Fact, TestPriority(1)]
            public void ATestAdd5()
            {
                var a = testStep.Exec<int>(() => { return this.Add(2, 1); });
            }
            [Fact, TestPriority(2)]
            public void BTestAdd4()
            {
                Assert.Equal(3, this.Add(2, 1));
                output.WriteLine("1111111111");
                _Share.str += _Share.str;
            }
            [Fact(DisplayName = "Successful response Test2"), TestPriority(3)]
            [Trait("Description", "Happy Path.")]
            [Trait("ExpectedResult", "ok")]
            public void ATestAdd2()
            {
                Assert.Equal(3, this.Add(1, 2));
                output.WriteLine("22222222222");
                _Share.str += _Share.str;
            }
            [Fact, TestPriority(4)]
            public void ATestAdd3()
            {
                Assert.Equal(3, this.Add(1, 2));
                output.WriteLine("33333333333");
            }
        }
    }
}

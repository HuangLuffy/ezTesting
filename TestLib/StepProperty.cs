using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib
{
    public class StepProperty : Attribute
    {

    }
    public class Description : Attribute
    {
        public string[] des { set; get; }
        public Description(params string[] list)
        {
            this.des = list;
        }
    }
    public class ExpectedResult : Attribute
    {
        public string[] er { set; get; }
        public ExpectedResult(params string[] list)
        {
            this.er = list;
        }
    }
}

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
    public class Descriptions : Attribute
    {
        public string[] des { set; get; }
        public Descriptions(params string[] list)
        {
            this.des = list;
        }
    }
    public class ExpectedResults : Attribute
    {
        public string[] er { set; get; }
        public ExpectedResults(params string[] list)
        {
            this.er = list;
        }
    }
}

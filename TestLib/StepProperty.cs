using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLib
{
    public class StepProperty : Attribute
    {
        public string Description { get; set; }
        public string ExpectedResult { get; set; }
        public StepProperty(string description, string expectedResult)
        {
            this.Description = description;
            this.ExpectedResult = expectedResult;
        }
    }
}

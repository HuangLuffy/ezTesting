using System;

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
    public class DoNotBlock : Attribute
    {
        public DoNotBlock()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestLib;

namespace TestLib
{
    public class TestStep
    {
        public bool needToBlockTest = false;
        public const string blockedDescription = "This step is blocked since the previous step was failed.";
        public struct Result
        {
            public const string PASS = "Pass";
            public const string FAIL = "Fail";
            public const string BLOCK = "Block";
            public const string TBD = "Tbd";
        }

        protected T Exec<T>(Func<T> action)
        {
            T actualResult = action.Invoke();
            return actualResult;
        }
        protected void Exec(Action action)
        {
            action.Invoke();
        }
        //public T Rec<T>(Func<T> action)
        //{
        //    try
        //    {
        //        return Exec<T>(action);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {

        //    }
        //}
        //public void Rec(Action action)
        //{
        //    try
        //    {
        //        Exec(action);
        //    }
        //    catch (Exception)
        //    {
                
        //    }
        //    finally
        //    {

        //    }
        //}
        [Description("1","2")]
        [ExpectedResult("3", "4")]
        public string firstStep()
        {
            return "";
        }
    }
}

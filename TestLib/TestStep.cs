using System;
using System.IO;

namespace TestLib
{
    public class TestStep
    {
        protected T Execute<T>(Func<T> action)
        {
            T actualResult = action.Invoke();
            return actualResult;
        }
        protected void Execute(Action action)
        {
            action.Invoke();
        }

        private string GetTestFullPath()
        {
            return GetType().Assembly.Location;
        }
        public string GetTestParentPath()
        {
            return new DirectoryInfo(GetTestFullPath()).Parent?.ToString();
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
        [Descriptions("1","2")]
        [ExpectedResults("3", "4")]
        public string FirstStep()
        {
            return "";
        }
    }
}

﻿using System;
using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace XUnitTest.ExecOrderers
{
    public class InOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            foreach (TTestCase testCase in testCases)
                yield return testCase;
        }

        static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            TValue result;

            if (dictionary.TryGetValue(key, out result)) return result;

            result = new TValue();
            dictionary[key] = result;

            return result;
        }
    }
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class InOrderAttribute : Attribute
    {
        public InOrderAttribute()
        {

        }
    }
}

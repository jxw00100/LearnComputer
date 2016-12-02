using System;
using System.Linq;
using LearnComputer.CircuitInfrustructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.Assist
{
    public static class Extension
    {
        public static void AssertEquals(this IndicatorLight[] lights, String expectedResult)
        {
            if (lights == null || lights.Length <= 0 || lights.Any(l => l == null))
                throw new ArgumentException("Indicator switches are empty or have null object.");
            if(String.IsNullOrWhiteSpace(expectedResult)) throw new ArgumentException("Expected Result is empty.");
            if (expectedResult.Any(c => c != '1' && c != '0'))
                throw new ArgumentException("Expected Result contains invalid binaryValue (not 0 or 1)");

            var results = expectedResult.Select(c => (c == '1') ? true : false).Reverse();

            for (var i = 0; i < lights.Length; i++)
            {
                if (i < results.Count())
                {
                    Assert.AreEqual(lights[i].Lighting, results.ElementAt(i));
                }
                else
                {
                    Assert.IsFalse(lights[i].Lighting);
                }
            }
        }

        public static void Set(this Switches switches, String binaryValue)
        {
            if (switches == null) throw new ArgumentException("Switches are empty or have null object.");
            if (binaryValue.Any(c => c != '1' && c != '0'))
                throw new ArgumentException("Value contains invalid binaryValue (not 0 or 1)");

            if (String.IsNullOrWhiteSpace(binaryValue))
            {
                switches.Clear();
            }
            else
            {
                var switchesOn = binaryValue.Select(c => (c == '1') ? true : false).Reverse().ToArray();
                switches.Set(switchesOn);
            }
        }
    }
}

using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    public class TestUtil
    {
        public static void AssertVectorsApproximatelyEqual(Vector3 expected, Vector3 actual)
        {
            Assert.IsTrue(expected == actual, string.Format("Expected {0}, but received {1}", expected, actual));
        }

        public static void AssertApproximatelyEqual(float expected, float actual)
        {
            Assert.IsTrue(Mathf.Approximately(expected, actual), string.Format("Expected {0}, but got {1}", expected, actual));
        }
    }
}

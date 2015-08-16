using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    [TestFixture]
    public class LerpCurveTest
    {
        private LerpCurve testObj;

        [Test]
        [ExpectedException(ExpectedException=typeof(LerperException), ExpectedMessage="Lerper has no data")]
        public void ProbeThrowsExceptionWhenNoKeyPointsAreDefined()
        {
            testObj = new LerpCurve();
            testObj.Probe(.5f);
        }

        [Test]
        public void ProbeAlwaysReturnsDefinedValueWhenOnlyOneKeyPointWasDefined()
        {
            var expectedValue = 5f;
            testObj = new LerpCurve(new LerperPair(.5f, expectedValue));
            AssertInputMapsToOutput(0f, expectedValue);
            AssertInputMapsToOutput(.5f, expectedValue);
            AssertInputMapsToOutput(1f, expectedValue);
        }

        [Test]
        public void ProbeReturnsLinearlyInterpolatedValuesWhenTwoKeyPointsAreDefined()
        {
            testObj = new LerpCurve(new LerperPair(1f, 5f), new LerperPair(3f, 6f));
            AssertInputMapsToOutput(1f, 5f);
            AssertInputMapsToOutput(1.5f, 5.25f);
            AssertInputMapsToOutput(2f, 5.5f);
            AssertInputMapsToOutput(2.5f, 5.75f);
            AssertInputMapsToOutput(3f, 6f);
        }

        [Test]
        public void ProbeReturnsFirstOutputValueWhenInputIsBelowMinimum()
        {
            testObj = new LerpCurve(new LerperPair(10f, 5f), new LerperPair(20f, 10f));
            AssertInputMapsToOutput(1f, 5f);
        }

        [Test]
        public void ProbeReturnsLastOutputValueWhenInputIsAboveMaximum()
        {
            testObj = new LerpCurve(new LerperPair(10f, 5f), new LerperPair(20f, 10f));
            AssertInputMapsToOutput(25f, 10f);
        }

        [Test]
        public void ProbeReturnsLinearlyInterpolatedValuesWhenThreeKeyPointsAreDefined()
        {
            testObj = new LerpCurve(new LerperPair(1f, 5f), new LerperPair(3f, 6f), new LerperPair(5f, 8f));
            AssertInputMapsToOutput(1f, 5f);
            AssertInputMapsToOutput(2f, 5.5f);
            AssertInputMapsToOutput(3f, 6f);
            AssertInputMapsToOutput(4f, 7f);
            AssertInputMapsToOutput(5f, 8f);
        }

        private void AssertInputMapsToOutput(float input, float output)
        {
            AssertAreClose(output, testObj.Probe(input));
        }

        private void AssertAreClose(float expected, float actual)
        {
            Assert.AreEqual(expected, actual, float.Epsilon);
        }
    }
}

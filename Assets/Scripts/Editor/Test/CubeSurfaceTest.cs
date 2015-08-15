using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    [TestFixture]
    public class CubeSurfaceTest
    {
        private CubeSurface testObj;
        private Vector3 origin;
        private float sideLength;
        private Vector3 up;

        [SetUp]
        public void SetUp()
        {
            origin = new Vector3(0, 0, 0);
            sideLength = 10f;
            up = new Vector3(0, 1, 0);
            testObj = new CubeSurface(origin, sideLength, up);
        }

        [Test]
        public void GetInitialPointOnSurfaceReturnsArbitraryPointOnSurface()
        {
            Assert.AreEqual(origin + (.5f * sideLength * Vector3.forward), testObj.GetInitialPointOnSurface());
        }

        [Test]
        public void GetWorldUpVectorReturnsSpecifiedUpVector()
        {
            Assert.AreEqual(up, testObj.GetWorldUpVector());
        }

        [Test]
        public void GetNormalAtPointReturnsPositiveXUnitVectorWhenPointIsOnRightSide()
        {
            Assert.AreEqual(Vector3.right, testObj.GetNormalAtPoint(new Vector3(sideLength * .5f, 0f, 0f)));
        }

        [Test]
        public void GetNormalAtPointReturnsNegativeZUnitVectorWhenPointIsOnBackSide()
        {
            Assert.AreEqual(-Vector3.forward, testObj.GetNormalAtPoint(new Vector3(0f, 0f, -sideLength * .5f)));
        }

        [Test]
        public void GetNormalAtPointReturnsPositiveYUnitVectorWhenPointIsOnTopSide()
        {
            Assert.AreEqual(Vector3.up, testObj.GetNormalAtPoint(new Vector3(0f, sideLength * .5f, 0f)));
        }

        [Test]
        public void ClampPointToSurfaceAlwaysClampsLargerValuesToHalfSideLength(
            [NUnit.Framework.Values(-10.0, 10.0, -10.0)] double x,
            [NUnit.Framework.Values(-10.0, 10.0,  10.0)] double y,
            [NUnit.Framework.Values(  0.0, 10.0,  -3.0)] double z)
        {
            var pos = new Vector3((float)x, (float)y, (float)z);
            var expectedPos = new Vector3();
            expectedPos.x = Mathf.Clamp(pos.x, -.5f * sideLength, .5f * sideLength);
            expectedPos.y = Mathf.Clamp(pos.y, -.5f * sideLength, .5f * sideLength);
            expectedPos.z = Mathf.Clamp(pos.z, -.5f * sideLength, .5f * sideLength);
            Assert.AreEqual(expectedPos, testObj.ClampPointToSurface(pos));
        }

        [Test]
        public void ClampPointToSurfaceAlwaysIncreasesLargestComponentToHalfSideLengthWhenPointLiesInsideSurface()
        {
            origin = new Vector3(10f, 10f, 10f);
            testObj = new CubeSurface(origin, sideLength, up);

            AssertPointInsideCubeIsPushedToSurface(new Vector3(7f, 13.5f, 7f));
            AssertPointInsideCubeIsPushedToSurface(new Vector3(6f, 9.5f, 9.5f));
            AssertPointInsideCubeIsPushedToSurface(new Vector3(8f, 11f, 6.25f));
        }

        private void AssertPointInsideCubeIsPushedToSurface(Vector3 pos)
        {
            var diff = pos - origin;
            var expectedDelta = diff;
            var maxComponentMagnitude = Mathf.Max(Mathf.Max(Mathf.Abs(diff.x), Mathf.Abs(diff.y)), Mathf.Abs(diff.z));
            if (Mathf.Approximately(diff.x, maxComponentMagnitude))
                expectedDelta.x = .5f * sideLength * (diff.x / Mathf.Abs(diff.x));
            if (Mathf.Approximately(diff.y, maxComponentMagnitude))
                expectedDelta.y = .5f * sideLength * (diff.y / Mathf.Abs(diff.y));
            if (Mathf.Approximately(diff.z, maxComponentMagnitude))
                expectedDelta.z = .5f * sideLength * (diff.z / Mathf.Abs(diff.z));

            Assert.AreEqual(origin + expectedDelta, testObj.ClampPointToSurface(pos));
        }
    }
}

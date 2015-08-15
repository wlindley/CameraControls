using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    [TestFixture]
    public class PlaneSurfaceTest
    {
        private PlaneSurface testObj;
        private Vector3 origin;
        private Vector3 normal;
        private Vector3 up;

        [SetUp]
        public void SetUp()
        {
            origin = new Vector3(0, -10, 0);
            normal = new Vector3(0, 0, 1);
            up = new Vector3(0, 1, 0);
            testObj = new PlaneSurface(origin, normal, up);
        }

        [Test]
        public void GetInitialPointOnSurfaceReturnsSpecifiedOrigin()
        {
            Assert.AreEqual(origin, testObj.GetInitialPointOnSurface());
        }

        [Test]
        public void GetWorldUpVectorReturnsSpecifiedUpVector()
        {
            Assert.AreEqual(up, testObj.GetWorldUpVector());
        }

        [Test]
        public void NormalIsUnitVector()
        {
            testObj = new PlaneSurface(origin, normal * 10f, up);
            Assert.AreEqual(1f, testObj.GetNormalAtPoint(origin).magnitude);
        }

        [Test]
        public void GetNormalAtPointAlwaysReturnsSameNormal(
            [NUnit.Framework.Random(-10.0, 10.0, 3)] double x,
            [NUnit.Framework.Values(-10.0)] double y,
            [NUnit.Framework.Random(-10.0, 10.0, 3)] double z)
        {
            var pos = new Vector3((float)x, (float)y, (float)z);
            Assert.AreEqual(normal, testObj.GetNormalAtPoint(pos));
        }

        [Test]
        public void ClampPointToSurfaceForcesOffsetToOriginAlongNormal(
            [NUnit.Framework.Random(-10.0, 10.0, 3)] double x,
            [NUnit.Framework.Random(-10.0, 10.0, 3)] double y,
            [NUnit.Framework.Random(-10.0, 10.0, 3)] double z)
        {
            var pos = new Vector3((float)x, (float)y, (float)z);
            Assert.AreEqual(new Vector3((float)x, (float)y, origin.z), testObj.ClampPointToSurface(pos));
        }

        [Test]
        public void ClampPointToSurfaceForcesOffsetToOriginAlongNormalWhenPlaneIsTilted(
            [NUnit.Framework.Random(-10.0, 10.0, 3)] double x,
            [NUnit.Framework.Random(-10.0, 10.0, 3)] double y,
            [NUnit.Framework.Values(0.0)] double z)
        {
            normal = new Vector3(.3f, .7f, 0f);
            testObj = new PlaneSurface(origin, normal, up);
            var pos = new Vector3((float)x, (float)y, (float)z);

            var delta = pos - origin;
            var distanceFromSurface = Vector3.Dot(delta, normal.normalized);
            var pointOnSurface = pos - (distanceFromSurface * normal.normalized);

            Assert.AreEqual(pointOnSurface, testObj.ClampPointToSurface(pos));
        }
    }
}

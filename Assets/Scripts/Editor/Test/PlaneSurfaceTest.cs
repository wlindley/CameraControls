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
        private Vector3 origin = new Vector3(0, -10, 0);
        private Vector3 normal = new Vector3(0, 0, 1);
        private Vector3 up = new Vector3(0, 1, 0);

        [SetUp]
        public void SetUp()
        {
            testObj = new PlaneSurface(origin, normal, up);
        }

        [Test]
        public void GetOriginReturnsSpecifiedOrigin()
        {
            Assert.AreEqual(origin, testObj.GetOrigin());
        }

        [Test]
        public void GetWorldUpVectorReturnsSpecifiedUpVector()
        {
            Assert.AreEqual(up, testObj.GetWorldUpVector());
        }

        [Test]
        public void GetNormalAtPointAlwaysReturnsSamePoint(
            [NUnit.Framework.Random(-10.0, 10.0, 3)] double x,
            [NUnit.Framework.Values(-10.0)] double y,
            [NUnit.Framework.Random(-10.0, 10.0, 3)] double z)
        {
            var pos = new Vector3((float)x, (float)y, (float)z);
            Assert.AreEqual(normal, testObj.GetNormalAtPoint(pos));
        }
    }
}

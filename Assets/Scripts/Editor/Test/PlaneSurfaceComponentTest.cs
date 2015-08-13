using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    [TestFixture]
    public class PlaneSurfaceComponentTest
    {
        private PlaneSurfaceComponent testObj;
        private Vector3 testObjPos;

        [SetUp]
        public void SetUp()
        {
            testObjPos = new Vector3(50, 50, 50);

            var testObjGO = new GameObject();
            testObjGO.transform.position = testObjPos;

            testObj = testObjGO.AddComponent<PlaneSurfaceComponent>();
            testObj.renderSize = 500f;

            testObj.Awake();
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(testObj.gameObject);
            testObj = null;
        }

        [Test]
        public void GetOriginReturnsBoxPositionPlusCenter()
        {
            Assert.AreEqual(testObjPos, testObj.GetOrigin());
        }

        [Test]
        public void GetNormalAtPointAlwaysReturnsBoxUpVector(
            [NUnit.Framework.Random(-10.0, 10.0, 3)] double x,
            [NUnit.Framework.Values(-10.0)] double y,
            [NUnit.Framework.Random(-10.0, 10.0, 3)] double z)
        {
            var pos = new Vector3((float)x, (float)y, (float)z);
            Assert.AreEqual(testObj.transform.up, testObj.GetNormalAtPoint(pos));
        }

        [Test]
        public void GetWorldUpVectorReturnsBoxForwardVector()
        {
            Assert.AreEqual(testObj.transform.forward, testObj.GetWorldUpVector());
        }

        [Test]
        public void GetSurfaceHeightAtPointAlwaysReturnsZero(
            [NUnit.Framework.Random(-10.0, 10.0, 3)] double x,
            [NUnit.Framework.Values(-10.0)] double y,
            [NUnit.Framework.Random(-10.0, 10.0, 3)] double z)
        {
            var pos = new Vector3((float)x, (float)y, (float)z);
            Assert.AreEqual(0f, testObj.GetSurfaceHeightAtPoint(pos));
        }
    }
}

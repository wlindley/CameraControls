using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    [TestFixture]
    public class ColliderPlaneSurfaceTest
    {
        private ColliderPlaneSurface testObj;
        private BoxCollider collider;
        private Vector3 testObjPos;
        private Vector3 boxSize;
        private Vector3 boxCenter;

        [SetUp]
        public void SetUp()
        {
            testObjPos = new Vector3(50, 50, 50);
            boxSize = new Vector3(100, 0, 100);
            boxCenter = new Vector3(10, 0, 10);

            var testObjGO = new GameObject();
            testObjGO.transform.position = testObjPos;

            collider = testObjGO.AddComponent<BoxCollider>();
            collider.size = boxSize;
            collider.center = boxCenter;

            testObj = testObjGO.AddComponent<ColliderPlaneSurface>();

            testObj.Awake();
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(testObj.gameObject);
            testObj = null;
            collider = null;
        }

        [Test]
        public void GetOriginReturnsBoxPositionPlusCenter()
        {
            Assert.AreEqual(testObjPos + boxCenter, testObj.GetOrigin());
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
    }
}

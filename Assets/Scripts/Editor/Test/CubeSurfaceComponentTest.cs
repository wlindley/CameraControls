using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    [TestFixture]
    public class CubeSurfaceComponentTest
    {
        private CubeSurfaceComponent testObj;
        private float sideLength = 10f;

        [SetUp]
        public void SetUp()
        {
            var testObjGO = new GameObject();
            testObj = testObjGO.AddComponent<CubeSurfaceComponent>();
            testObj.sideLength = sideLength;

            testObj.Awake();
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(testObj.gameObject);
            testObj = null;
        }

        [Test]
        public void GetOriginReturnsSpecifiedOrigin()
        {
            Assert.AreEqual(testObj.transform.position, testObj.GetOrigin());
        }

        [Test]
        public void GetWorldUpVectorReturnsSpecifiedUpVector()
        {
            Assert.AreEqual(testObj.transform.up, testObj.GetWorldUpVector());
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
        public void GetSurfaceHeightAtPointAlwaysReturnsHalfSideLength(
            [NUnit.Framework.Values(5.0, 0.0, 0.0, -5.0, 0.0, 0.0)] double x,
            [NUnit.Framework.Values(0.0, 5.0, 0.0, 0.0, -5.0, 0.0)] double y,
            [NUnit.Framework.Values(0.0, 0.0, 5.0, 0.0, 0.0, -5.0)] double z)
        {
            var pos = new Vector3((float)x, (float)y, (float)z);
            Assert.AreEqual(.5f * sideLength, testObj.GetSurfaceHeightAtPoint(pos));
        }
    }
}

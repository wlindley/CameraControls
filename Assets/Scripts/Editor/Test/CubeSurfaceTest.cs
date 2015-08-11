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
        private Vector3 origin = new Vector3(0, 0, 0);
        private float sideLength = 10f;
        private Vector3 up = new Vector3(0, 1, 0);

        [SetUp]
        public void SetUp()
        {
            testObj = new CubeSurface(origin, sideLength, up);
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
    }
}

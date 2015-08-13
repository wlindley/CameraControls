﻿using UnityEngine;
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

        [Test]
        public void GetNormalAtPointReturnsAnyOneDimensionalUnitVectorWhenPointIsOrigin()
        {
            var normal = testObj.GetNormalAtPoint(testObj.GetOrigin());
            Assert.AreEqual(1f, normal.magnitude);
            Assert.That(normal.x, Is.EqualTo(1f) | Is.EqualTo(0f) | Is.EqualTo(-1f));
            Assert.That(normal.y, Is.EqualTo(1f) | Is.EqualTo(0f) | Is.EqualTo(-1f));
            Assert.That(normal.z, Is.EqualTo(1f) | Is.EqualTo(0f) | Is.EqualTo(-1f));
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

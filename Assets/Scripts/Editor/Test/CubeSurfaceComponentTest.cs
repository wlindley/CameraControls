﻿using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    [TestFixture]
    public class CubeSurfaceComponentTest
    {
        private CubeSurfaceComponent testObj;
        private CubeSurface surface;
        private Vector3 testObjPos;
        private float sideLength = 10f;

        [SetUp]
        public void SetUp()
        {
            surface = Substitute.For<CubeSurface>();
            CubeSurface.TestInstance = surface;

            testObjPos = new Vector3(50, 50, 50);

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
            CubeSurface.TestInstance = null;
        }

        private Vector3 GetRandomPoint()
        {
            return new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        }

        [Test]
        public void GetInitialPointOnSurfacePassesThroughToWrappedInstance()
        {
            surface.GetInitialPointOnSurface().Returns(testObjPos);
            Assert.AreEqual(testObjPos, testObj.GetInitialPointOnSurface());
        }

        [Test]
        public void GetWorldUpVectorPassesThroughToWrappedInstance()
        {
            surface.GetWorldUpVector().Returns(Vector3.up);
            Assert.AreEqual(Vector3.up, testObj.GetWorldUpVector());
        }

        [Test]
        public void GetNormalAtPointPassesThroughToWrappedInstance()
        {
            surface.GetNormalAtPoint(Arg.Any<Vector3>()).Returns(Vector3.forward);
            var pos = GetRandomPoint();
            Assert.AreEqual(Vector3.forward, testObj.GetNormalAtPoint(pos));
        }

        [Test]
        public void ClampPointToSurfacePassesThroughToWrappedInstance()
        {
            var expectedClampedPoint = GetRandomPoint();
            surface.ClampPointToSurface(Arg.Any<Vector3>()).Returns(expectedClampedPoint);
            var pos = GetRandomPoint();
            Assert.AreEqual(expectedClampedPoint, testObj.ClampPointToSurface(pos));
        }
    }
}

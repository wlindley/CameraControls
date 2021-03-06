﻿using UnityEngine;
using NUnit.Framework;
using NSubstitute;
using CamCon;

namespace CamConTest
{
    [TestFixture]
    public class SphereSurfaceTest
    {
        private SphereSurface testObj;
        private Vector3 origin;
        private float radius;
        private Vector3 up;

        [SetUp]
        public void SetUp()
        {
            origin = new Vector3(0, 10, 0);
            radius = 5f;
            up = new Vector3(0, 1, 0);
            testObj = new SphereSurface(origin, radius, up);
        }

        [Test]
        public void GetInitialPositionOnSurfaceReturnsArbitraryPointOnSurface()
        {
            var diff = testObj.GetInitialPointOnSurface() - origin;
            TestUtil.AssertApproximatelyEqual(radius, diff.magnitude);
        }

        [Test]
        public void GetUpVectorAtPointReturnsSpecifiedUpVector(
            [NUnit.Framework.Random(0.0, 2.0 * Mathf.PI, 2)] double theta,
            [NUnit.Framework.Random(0.0, 2.0 * Mathf.PI, 2)] double row,
            [NUnit.Framework.Values(5.0)] double radius)
        {
            var pos = origin + (Quaternion.Euler((float)theta, 0f, (float)row) * new Vector3((float)radius, 0f, 0f));
            var diff = (pos - origin).normalized;
            var left = Vector3.Cross(up, diff);
            var localUp = Vector3.Cross(diff, left);
            TestUtil.AssertApproximatelyEqual(localUp, testObj.GetUpVectorAtPoint(pos));
        }

        [Test]
        public void GetNormalAtPointReturnsUnitVectorPointingAtPoint(
            [NUnit.Framework.Random(0.0, 2.0 * Mathf.PI, 2)] double theta,
            [NUnit.Framework.Random(0.0, 2.0 * Mathf.PI, 2)] double row,
            [NUnit.Framework.Values(5.0)] double radius)
        {
            var pos = origin + (Quaternion.Euler((float)theta, 0f, (float)row) * new Vector3((float)radius, 0f, 0f));
            TestUtil.AssertApproximatelyEqual((float)radius, (pos - origin).magnitude);

            var normal = testObj.GetNormalAtPoint(pos);
            TestUtil.AssertApproximatelyEqual(1f, normal.magnitude);
            TestUtil.AssertApproximatelyEqual((pos - origin).normalized, normal);
        }

        [Test]
        public void ClampPointToSurfaceReturnsPointOnSurfaceAlongNormalWhenPointIsOutsideSphere(
            [NUnit.Framework.Random(0.0, 2.0 * Mathf.PI, 2)] double theta,
            [NUnit.Framework.Random(0.0, 2.0 * Mathf.PI, 2)] double row,
            [NUnit.Framework.Values(5.5, 10f)] double radiusToPoint)
        {
            var pos = origin + (Quaternion.Euler((float)theta, 0f, (float)row) * new Vector3((float)radiusToPoint, 0f, 0f));
            TestUtil.AssertApproximatelyEqual((float)radiusToPoint, (pos - origin).magnitude);

            var surfacePoint = testObj.ClampPointToSurface(pos);
            TestUtil.AssertApproximatelyEqual((float)radius, (surfacePoint - origin).magnitude);
            TestUtil.AssertApproximatelyEqual(origin + ((pos - origin).normalized * (float)radius), surfacePoint);
        }

        [Test]
        public void ClampPointToSurfaceReturnsPointOnSurfaceAlongNormalWhenPointIsInsideSphere(
            [NUnit.Framework.Random(0.0, 2.0 * Mathf.PI, 2)] double theta,
            [NUnit.Framework.Random(0.0, 2.0 * Mathf.PI, 2)] double row,
            [NUnit.Framework.Values(4.5, 1f)] double radiusToPoint)
        {
            var pos = origin + (Quaternion.Euler((float)theta, 0f, (float)row) * new Vector3((float)radiusToPoint, 0f, 0f));
            TestUtil.AssertApproximatelyEqual((float)radiusToPoint, (pos - origin).magnitude);

            var surfacePoint = testObj.ClampPointToSurface(pos);
            TestUtil.AssertApproximatelyEqual((float)radius, (surfacePoint - origin).magnitude);
            TestUtil.AssertApproximatelyEqual(origin + ((pos - origin).normalized * (float)radius), surfacePoint);
        }
    }
}
